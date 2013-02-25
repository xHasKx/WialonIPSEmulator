using System;
using System.Threading;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using HasK.Net;

namespace WialonIPS
{
    /// <summary>
    /// Interface of error receiver
    /// </summary>
    public interface IErrorReporter
    {
        void OnException(string when, Exception e);
    }

    public delegate void MessageConnectorOperation(MessagesCommunicator comm);
    public delegate void MessageOperationDelegate(MessagesCommunicator comm, Message msg);



    /// <summary>
    /// Wialon IPS messages sender/receiver
    /// </summary>
    public class MessagesCommunicator : IDisposable
    {
        public string Host { get; private set; }
        public UInt16 Port { get; private set; }
        public bool IsConnected
        {
            get
            {
                if (this._client != null)
                    return this._client.Connected;
                return false;
            }
        }

        public event MessageConnectorOperation OnConnect;
        public event MessageConnectorOperation OnDisconnect;
        public event MessageOperationDelegate OnSent;
        public event MessageOperationDelegate OnReceive;
        
        private TCPClient _client = null;
        private IErrorReporter _err_reporter;
        private Message _last_sent_message;
        
        public MessagesCommunicator(string host, UInt16 port, IErrorReporter err_reporter)
        {
            this.Host = host;
            this.Port = port;
            this._err_reporter = err_reporter;
            this._client = new TCPClient(this.Host, this.Port);
            this._client.OnConnect += new TCPClient.ConnectDelegate(_client_OnConnect);
            this._client.OnDisconnect += new TCPClient.DisconnectDelegate(_client_OnDisconnect);
            this._client.OnReceive += new TCPClient.ReceiveDelegate(_client_OnReceive);
            this._client.OnSent += new TCPClient.SentDelegate(_client_OnSent);
            this._client.OnError += new TCPClient.ErrorDelegate(_client_OnError);
        }

        void _client_OnSent(TCPClient client, int sent)
        {
            if (this.OnSent != null) this.OnSent(this, this._last_sent_message);
            this._last_sent_message = null;
        }

        void _client_OnError(TCPClient client, System.Net.Sockets.SocketException exc)
        {
            this._err_reporter.OnException(String.Format("TCPClient {0} exception", client), exc);
        }

        void _client_OnReceive(TCPClient client, HasK.Data.BinDataQueue buffer)
        {
            var end = "\r\n";
            while (buffer.InBufferSize > 0)
            {
                var line = buffer.ReadText(end);
                if (this.OnReceive != null) this.OnReceive(this, Message.Parse(line));
            }
        }

        void _client_OnDisconnect(TCPClient client)
        {
            if (this.OnDisconnect != null) this.OnDisconnect(this);
        }

        public void Connect()
        {
            this._client.Connect();
        }

        void _client_OnConnect(TCPClient client)
        {
            if (this.OnConnect != null) this.OnConnect(this);
        }

        public void Disconnect()
        {
            this._client.Disconnect();
        }

        public bool Send(Message message)
        {
            if (this.IsConnected)
            {
                this._last_sent_message = message;
                this._client.Send(message.GetData());
                return true;
            }
            return false;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this._client != null)
                this._client.Dispose();
        }

        #endregion
    }

    public enum DoneReason { Success, ServerAnswerNotSuccess, Disconnect, UnknownError }

    public delegate void BatchSenderDone(BatchSender sender, DoneReason reason);
    public delegate void BatchSenderProcess(BatchSender sender, double process_stage);

    /// <summary>
    /// Base class for all batch senders
    /// </summary>
    public class BatchSender
    {
        private MessagesCommunicator _mc = null;
        private Message[] _messages = null;
        private Thread _wthread = null;
        private readonly object _lock = new object();
        private Message _last_received = null, _last_sent = null;
        private bool _done = false, _can_wait_for_answer = false;
        private int _sent_msg_index = 0;
        private DoneReason _reason = DoneReason.UnknownError;

        public int Interval { get; set; }

        public event BatchSenderDone OnDone;
        public event BatchSenderProcess OnProcess;

        public BatchSender(MessagesCommunicator mc)
        {
            this._mc = mc;
            this.Interval = 0;
        }

        public virtual void SetMessages(Message[] messages)
        {
            this._messages = messages;
        }

        void _mc_OnSent(MessagesCommunicator comm, Message msg)
        {
            lock (this._lock)
                this._last_sent = msg;
        }

        void _mc_OnDisconnect(MessagesCommunicator comm)
        {
            lock (this._lock)
            {
                this._done = true;
                this._reason = DoneReason.Disconnect;
            }
        }

        void _mc_OnReceive(MessagesCommunicator comm, Message msg)
        {
            lock (this._lock)
                this._last_received = msg;
        }

        public virtual void Start()
        {
            this._wthread = new Thread(new ThreadStart(this.Run));
            this._wthread.Start();
        }

        void Run()
        {
            this._mc.OnReceive += new MessageOperationDelegate(_mc_OnReceive);
            this._mc.OnSent += new MessageOperationDelegate(_mc_OnSent);
            this._mc.OnDisconnect += new MessageConnectorOperation(_mc_OnDisconnect);
            // send first message
            this._mc.Send(this._messages[this._sent_msg_index]);
            // wait for server answer
            while (!this._done)
            {
                lock (this._lock)
                {
                    if (this._last_sent != null)
                    {
                        if (this._messages[this._sent_msg_index] == this._last_sent)
                        {
                            // message sent, can wait for answer
                            this._can_wait_for_answer = true;
                            if (this.OnProcess != null) this.OnProcess(this, (this._sent_msg_index + 1) / (double)this._messages.Length);
                        }
                        this._last_sent = null;
                    }
                    if (this._can_wait_for_answer && this._last_received != null)
                    {
                        this._can_wait_for_answer = false;
                        if (this._last_received.Success)
                        {
                            // send next
                            this._sent_msg_index += 1;
                            if (this._sent_msg_index >= this._messages.Length)
                            {
                                // done
                                this._done = true;
                                this._reason = DoneReason.Success;
                                break;
                            } else
                                this._mc.Send(this._messages[this._sent_msg_index]);
                        } else
                        {
                            // server answer isn't success
                            this._done = true;
                            this._reason = DoneReason.ServerAnswerNotSuccess;
                            break;
                        }
                        this._last_received = null;
                    }
                }
                if (this.Interval > 0)
                    if (this._sent_msg_index < this._messages.Length - 1)
                        Thread.Sleep(this.Interval);
                else
                    Thread.Sleep(1);
            }
            if (this.OnDone != null) this.OnDone(this, this._reason);
            this._mc.OnDisconnect -= this._mc_OnDisconnect;
            this._mc.OnReceive -= this._mc_OnReceive;
            this._mc.OnSent -= this._mc_OnSent;
        }

    }

    /// <summary>
    /// All protocol message types
    /// </summary>
    public enum MessageType { Login, LoginAns, Data, DataAns, Ping, PingAns, ShortData, ShortDataAns,
        BBox, BBoxAns, Message, MessageAns, Image, ImageAns, Firmware, Software, Unknown }

    /// <summary>
    /// Base class for all messages of protocol
    /// </summary>
    public abstract class Message
    {
        public static DateTime? ParseTime(string date, string time)
        {
            if (date.Length == 6 && time.Length == 6)
            {
                byte day, month, year, hour, minute, second;
                if (byte.TryParse(date.Substring(0, 2), out day) &&
                    byte.TryParse(date.Substring(2, 2), out month) &&
                    byte.TryParse(date.Substring(4, 2), out year) &&
                    byte.TryParse(time.Substring(0, 2), out hour) &&
                    byte.TryParse(time.Substring(2, 2), out minute) &&
                    byte.TryParse(time.Substring(4, 2), out second))
                    return new DateTime(year + 2000, month, day, hour, minute, second, DateTimeKind.Utc);
            }
            return null;
        }
        public static string CalcCoordValue(double val, bool is_lon)
        {
            // 5544.6025;N
            var aval = Math.Abs(val);
            var grad = (int)aval;
            var min = (aval - grad) * 60.0;
            if (is_lon)
            {
                if (val > 180.0)
                    return "NA;E;";
                var sign = "E";
                if (val < 0)
                    sign = "W";
                return String.Format("{0:000}{1:00.00000};{2};", grad, min, sign).Replace(',', '.');
            } else
            {
                if (val > 90.0)
                    return "NA;N;";
                var sign = "N";
                if (val < 0)
                    sign = "S";
                return String.Format("{0:00}{1:00.00000};{2};", grad, min, sign).Replace(',', '.');
            }
        }
        public static double? ParseCoord(string coord, string sign)
        {
            if (coord == "NA")
                return null;
            else
            {
                double val;
                if (double.TryParse(coord.Replace('.', ','), out val))
                {
                    var grad = (int)(val / 100.0);
                    var min = (val - grad * 100);
                    val = grad + min / 60.0;
                    if (sign == "S" || sign == "W")
                        val = -val;
                    return val;
                }
            }

            return null;
        }
        public static UInt16? ParseUInt16(string value)
        {
            UInt16 val;
            if (UInt16.TryParse(value, out val))
                return val;
            return null;
        }
        public static UInt32? ParseUInt32(string value)
        {
            UInt32 val;
            if (UInt32.TryParse(value, out val))
                return val;
            return null;
        }
        public static Byte? ParseByte(string value)
        {
            Byte val;
            if (Byte.TryParse(value, out val))
                return val;
            return null;
        }
        public static Double? ParseDouble(string value)
        {
            Double val;
            if (Double.TryParse(value.Replace('.', ','), out val))
                return val;
            return null;
        }

        public static MessageType GetAnsMessageType(MessageType t)
        {
            switch (t)
            {
                case MessageType.Login:
                    return MessageType.LoginAns;
                case MessageType.BBox:
                    return MessageType.BBoxAns;
                case MessageType.Data:
                    return MessageType.DataAns;
                case MessageType.Image:
                    return MessageType.ImageAns;
                case MessageType.Message:
                    return MessageType.MessageAns;
                case MessageType.Ping:
                    return MessageType.PingAns;
                case MessageType.ShortData:
                    return MessageType.ShortDataAns;
                default:
                    return MessageType.Unknown;
            }
        }

        public static Message Parse(string line)
        {
            line = line.Trim();
            if (line == "" || line == "#")
                return new UnknownMessage(line);
            if (line[0] != '#')
                return new UnknownMessage(line);
            int pos = line.IndexOf('#', 1);
            if (pos == -1)
                return new UnknownMessage(line);
            var header = line.Substring(1, pos - 1);
            var content = line.Substring(pos + 1);
            if (header.Length >= 1)
            {
                switch (header) {
                    case "L":
                        return new LoginMessage(content);
                    case "AL":
                        return new LoginAnsMessage(content);
                    case "P":
                        return new PingMessage();
                    case "AP":
                        return new PingAnsMessage();
                    case "M":
                        return new MessageMessage(content);
                    case "AM":
                        return new MessageAnsMessage(content);
                    case "SD":
                        return new ShortDataMessage(content);
                    case "ASD":
                        return new ShortDataMessageAns(content);
                    case "D":
                        return new DataMessage(content);
                    case "AD":
                        return new DataMessageAns(content);
                    case "I":
                        return new ImageMessage(content);
                    case "AI":
                        return new ImageMessageAns(content);
                    default:
                        return new UnknownMessage(line);
                }
            } else
                return new UnknownMessage(line);
        }

        public override string ToString()
        {
            string res = this.GetTextData();
            if (this.Description != "")
                res += " (" + this.Description + ")";
            return res;
        }

        public bool Success { get; protected set; }
        public string Description { get; protected set; }
        public MessageType MsgType { get; protected set; }

        public virtual byte[] GetData()
        {
            var str = this.GetTextData() + "\r\n";
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        public abstract string GetTextData();
    }

    public class UnknownMessage : Message
    {
        private string _data;

        public UnknownMessage(string line)
        {
            this.Success = false;
            this.Description = "Unknown packet";
            this.MsgType = MessageType.Unknown;
            this._data = line.TrimEnd();
        }

        public override string GetTextData()
        {
            return this._data;
        }
    }

    public class LoginMessage : Message
    {
        public LoginMessage(string content)
        {
            this.MsgType = MessageType.Login;
            this.Description = "Login";
            var items = content.Split(';');
            this.Success = false;
            if (items.Length == 2)
            {
                this.login = items[0];
                this.password = items[1];
                this.Success = true;
            }
        }
        public LoginMessage(string login, string password)
        {
            this.Success = true;
            this.MsgType = MessageType.Login;
            this.Description = "Login";
            this.login = login;
            if (password != "")
                this.password = password;
            else
                this.password = "NA";
        }

        public override string GetTextData()
        {
            return "#L#" + this.login + ";" + this.password;
        }

        private string login, password;
    }

    public class LoginAnsMessage : Message
    {
        public LoginAnsMessage(string code)
        {
            this.MsgType = MessageType.LoginAns;
            this.code = code;
            this.Description = "";
            this.Success = false;
            if (code == "1")
            {
                this.Success = true;
                this.Description = "Login ok";
            }
            if (code == "0")
                this.Description = "Login failed";
            if (code == "01")
                this.Description = "Login failed, wrong password";
        }

        public override string GetTextData()
        {
            return "#AL#" + this.code;
        }

        private string code;
    }

    public class PingMessage : Message
    {
        public PingMessage()
        {
            this.Success = true;
            this.Description = "Ping";
            this.MsgType = MessageType.Ping;
        }

        public override string GetTextData()
        {
            return "#P#";
        }
    }

    public class PingAnsMessage : Message
    {
        public PingAnsMessage()
        {
            this.Success = true;
            this.Description = "Ping answer";
            this.MsgType = MessageType.PingAns;
        }

        public override string GetTextData()
        {
            return "#AP#";
        }
    }

    public class MessageMessage : Message
    {
        private string _text;

        public MessageMessage(string text)
        {
            // "74.47920564916546|71.13391499825951|название|текст"
            this.Success = true;
            this.Description = "Message to driver";
            this.MsgType = MessageType.Message;
            this._text = text;
            var items = text.Split('|');
            if (items.Length == 4)
            {
                double lat, lon;
                if (double.TryParse(items[0].Replace('.', ','), out lat) &&
                    double.TryParse(items[1].Replace('.', ','), out lon))
                {
                    this.Description += String.Format(". Coordinates: {0}, {1}", items[2], items[3]);
                }
            }
        }

        public string Text { get { return _text; } }

        public override string GetTextData()
        {
            return "#M#" + this._text;
        }
    }

    public class MessageAnsMessage : Message
    {
        private string _code;

        public MessageAnsMessage(string code)
        {
            this.Description = "Answer to message to driver";
            this.MsgType = MessageType.MessageAns;
            this._code = code.Trim();
            if (this._code == "1")
                this.Success = true;
            else
                this.Success = false;
        }

        public override string GetTextData()
        {
            return "#AM#" + this._code;
        }
    }

    public class ShortDataMessage : Message
    {
        protected DateTime _time;
        protected double? _lat, _lon;
        protected int? _alt;
        protected UInt16? _speed, _heading;
        protected byte? _sats;
        protected string[] _items;

        protected virtual void Init(DateTime time, double? lat, double? lon, int? alt, UInt16? speed, UInt16? heading, byte? sats)
        {
            this._time = time;
            this._lat = lat;
            this._lon = lon;
            this._alt = alt;
            this._speed = speed;
            this._heading = heading;
            this._sats = sats;
        }

        protected virtual bool InitFromString(string content)
        {
            this._items = content.Split(';');
            if (this._items.Length >= 10)
            {
                var time = ParseTime(this._items[0], this._items[1]);
                if (time.HasValue)
                {
                    this.Success = true;
                    this.Init(time.Value,
                        ParseCoord(this._items[2], this._items[3]),
                        ParseCoord(this._items[4], this._items[5]),
                        ParseUInt16(this._items[8]),
                        ParseUInt16(this._items[6]),
                        ParseUInt16(this._items[7]),
                        ParseByte(this._items[9]));
                    return true;
                } else
                    this.Description += ": wrong time";
            } else
                this.Description += ": wrong message format";
            return false;
        }

        protected ShortDataMessage()
        {
            this.MsgType = MessageType.ShortData;
            this.Description = "Short data packet";
        }

        public ShortDataMessage(string content) : this()
        {
            // #SD#090412;181127;5355.09260;N;02732.40990;E;0;0;300;7
            this.Success = false;
            this.InitFromString(content);
        }

        public ShortDataMessage(DateTime time, double? lat, double? lon, int? alt, UInt16? speed, UInt16? heading, byte? sats) : this()
        {
            this.Success = true;
            this.Init(time, lat, lon, alt, speed, heading, sats);
        }

        protected void AppendNullable<T>(StringBuilder sb, Nullable<T> val) where T: struct
        {
            if (val.HasValue) sb.Append(val.Value); else sb.Append("NA");
        }

        protected void AppendCommonTextData(StringBuilder res)
        {
            res.Append(this._time.ToString("ddMMyy;HHmmss"));
            res.Append(";");
            if (this._lat.HasValue)
            {
                res.Append(CalcCoordValue(this._lat.Value, false).Replace(',', '.'));
            } else
                res.Append("NA;N;");
            if (this._lon.HasValue)
            {
                res.Append(CalcCoordValue(this._lon.Value, true).Replace(',', '.'));
            } else
                res.Append("NA;E;");
            if (this._speed.HasValue) res.Append(this._speed.Value); else res.Append("NA");
            res.Append(";");
            if (this._heading.HasValue)
            {
                if (this._heading.Value > 360)
                    res.Append("NA");
                else
                    res.Append(this._heading.Value);
            } else res.Append("NA");
            res.Append(";");
            AppendNullable(res, this._alt);
            res.Append(";");
            if (this._sats.HasValue)
            {
                if (this._sats.Value > 255)
                    res.Append("NA");
                else
                    res.Append(this._sats.Value);
            } else res.Append("NA");
        }

        public override string GetTextData()
        {
            var res = new StringBuilder("#SD#");
            this.AppendCommonTextData(res);
            return res.ToString();
        }
    }

    public class ShortDataMessageAns : Message
    {
        private string _code;

        public ShortDataMessageAns(string code)
        {
            this._code = code.TrimEnd();
            this.Success = false;
            this.Description = "Short data packet answer";
            this.MsgType = MessageType.ShortDataAns;
            switch (this._code)
            {
                case "-1": this.Description += ": wrong packet structure"; break;
                case "0": this.Description += ": wrong time"; break;
                case "1": this.Description += ": success"; this.Success = true; break;
                case "10": this.Description += ": wrong coordinates"; break;
                case "11": this.Description += ": wrong speed, course or altitude"; break;
                case "12": this.Description += ": wrong sats number"; break;
            }
        }

        public override string GetTextData()
        {
            return "#ASD#" + this._code;
        }
    }

    public enum CustomParamType
    {
        Int = 1,
        Double = 2,
        Text = 3
    }

    public class CustomParameter
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public CustomParamType Type { get; set; }
        [XmlAttribute]
        public string Value { get; set; }

        public CustomParameter(string name, CustomParamType type, string value)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
        }

        public CustomParameter(CustomParameter source) : this(source.Name, source.Type, source.Value) { }

        public CustomParameter() : this("", CustomParamType.Int, "NA") { }

        public override string ToString()
        {
            return this.Name + ":" + (int)this.Type + ":" + this.Value;
        }

        public static CustomParameter Parse(string text)
        {
            var items = text.Split(':');
            if (items.Length == 3)
            {
                var name = items[0];
                byte type = 0;
                if (byte.TryParse(items[1], out type) && (type >= 1) && (type <= 3))
                {
                    var ttype = (CustomParamType)type;
                    var value = items[2];
                    return new CustomParameter(name, ttype, value);
                }
            }
            return null;
        }
    }

    public class DataMessage : ShortDataMessage
    {
        protected double? _hdop;
        protected string _ibutton;
        protected bool _alarm;
        protected UInt32? _inputs;
        protected UInt32? _outputs;
        protected double[] _adcs;
        protected CustomParameter[] _custom_params;

        protected void InitFull(double? hdop, string ibutton, bool alarm, UInt32? inputs, UInt32? outputs, double[] adcs, CustomParameter[] custom_params)
        {
            this._hdop = hdop;
            this._ibutton = ibutton;
            this._alarm = alarm;
            this._inputs = inputs;
            this._outputs = outputs;
            this._adcs = adcs;
            this._custom_params = custom_params;
        }

        protected DataMessage()
        {
            this.MsgType = MessageType.Data;
            this.Description = "Data packet";
        }

        public DataMessage(string content) : this()
        {
            //    0    1    2    3    4    5    6     7      8      9    10   11     12      13  14      15
            // #D#date;time;lat1;lat2;lon1;lon2;speed;course;height;sats;hdop;inputs;outputs;adc;ibutton;params\r\n
            this.Success = false;
            if (this.InitFromString(content))
            {
                double[] adcs = null;
                CustomParameter[] custom_params = null;
                bool alarm = false;
                var adc_items = this._items[13].Split(',');
                if (adc_items.Length > 0)
                {
                    adcs = new double[adc_items.Length];
                    for (var i = 0; i < adc_items.Length; i++)
                    {
                        double? val = ParseDouble(adc_items[i]);
                        if (val.HasValue)
                            adcs[i] = val.Value;
                        else
                            adcs[i] = 0;
                    }
                }
                var cp_items = this._items[15].Split(',');
                if (cp_items.Length > 0)
                {
                    custom_params = new CustomParameter[cp_items.Length];
                    for (var i = 0; i < cp_items.Length; i++)
                    {
                        var cp = CustomParameter.Parse(cp_items[i]);
                        custom_params[i] = cp;
                        if (cp.Name == "SOS" && cp.Type == CustomParamType.Int && cp.Value == "1")
                            alarm = true;
                    }
                }
                this.InitFull(ParseDouble(this._items[10]),
                    this._items[14],
                    alarm,
                    ParseUInt32(this._items[11]),
                    ParseUInt32(this._items[12]),
                    adcs,
                    custom_params);
            }
        }

        public DataMessage(DateTime time, double? lat, double? lon, int? alt, UInt16? speed, UInt16? heading, byte? sats,
            double? hdop, string ibutton, bool alarm, UInt32? inputs, UInt32? outputs, double[] adcs, CustomParameter[] custom_params) : this()
        {
            this.Success = true;
            this.Init(time, lat, lon, alt, speed, heading, sats);
            this.InitFull(hdop, ibutton, alarm, inputs, outputs, adcs, custom_params);
        }

        public override string GetTextData()
        {
            // #D#date;time;lat1;lat2;lon1;lon2;speed;course;height;sats;hdop;inputs;outputs;adc;ibutton;params\r\n
            var res = new StringBuilder("#D#");
            this.AppendCommonTextData(res);
            res.Append(";");
            if (this._hdop.HasValue) res.Append(this._hdop.Value.ToString().Replace(',', '.')); else res.Append("NA");
            res.Append(";");
            AppendNullable(res, this._inputs);
            res.Append(";");
            AppendNullable(res, this._outputs);
            res.Append(";");
            if (this._adcs != null)
            {
                var adcs = new string[this._adcs.Length];
                for (var i = 0; i < this._adcs.Length; i++)
                    adcs[i] = this._adcs[i].ToString().Replace(',', '.');
                res.Append(String.Join(",", adcs));
            }
            res.Append(";");
            res.Append(this._ibutton);
            res.Append(";");
            var alarm_added = false;
            if (this._custom_params != null)
            {
                var cps = new string[this._custom_params.Length];
                for (var i = 0; i < this._custom_params.Length; i++)
                {
                    if (this._custom_params[i].Name == "SOS" && this._custom_params[i].Type == CustomParamType.Int)
                    {
                        alarm_added = true;
                        if (this._alarm)
                            this._custom_params[i].Value = "1";
                    }
                    cps[i] = this._custom_params[i].ToString();
                }
                res.Append(String.Join(",", cps));
            }
            if (this._alarm && !alarm_added)
            {
                if (this._custom_params != null)
                    res.Append(",");
                res.Append("SOS:1:1");
            }
            return res.ToString();
        }
    }

    public class DataMessageAns : Message
    {
        private string _content;

        public DataMessageAns(string content)
        {
            this._content = content;
            this.MsgType = MessageType.DataAns;
            this.Success = false;
            this.Description = "Data packet answer";
            switch (content.Trim())
            {
                case "-1":
                    this.Description += ": wrong packet structure";
                    break;
                case "0":
                    this.Description += ": wrong time";
                    break;
                case "1":
                    this.Description += ": success";
                    this.Success = true;
                    break;
                case "10":
                    this.Description += ": success, undefined coordinates";
                    this.Success = true;
                    break;
                case "11":
                    this.Description += ": success, undefuned speed, heading or altitude";
                    this.Success = true;
                    break;
                case "12":
                    this.Description += ": success, undefined sats or hdop";
                    this.Success = true;
                    break;
                case "13":
                    this.Description += ": success, undefined inputs or outputs";
                    this.Success = true;
                    break;
                case "14":
                    this.Description += ": success, undefined adc";
                    this.Success = true;
                    break;
                case "15":
                    this.Description += ": success, undefined custom params";
                    this.Success = true;
                    break;
            }
        }

        public override string GetTextData()
        {
            return "#AD#" + this._content;
        }
    }

    public class ImageMessage : Message
    {
        private byte[] _data;
        private int _start, _len, _index, _total;
        private DateTime _time;
        private string _name;

        public ImageMessage(string content)
        {
            this.Success = false;
            this.MsgType = MessageType.Image;
            int len = 0, index = 0, total = 0;
            string name = "";
            var items = content.Split(';');
            if (items.Length == 6)
            {
                if (Int32.TryParse(items[0], out len) &&
                    Int32.TryParse(items[1], out index) &&
                    Int32.TryParse(items[2], out total))
                {
                    var t = Message.ParseTime(items[3], items[4]);
                    if (t.HasValue)
                    {
                        name = items[5];
                        this.Init(len, index, total, t.Value, name);
                    }
                }
            }
        }

        public ImageMessage(int len, int index, int total, DateTime time, string name)
        {
            this.MsgType = MessageType.Image;
            this.Init(len, index, total, time, name);
        }

        void Init(int len, int index, int total, DateTime time, string name)
        {
            this._len = len;
            this._index = index;
            this._total = total;
            this._name = name;
            this._time = time;
            this.Success = true;
            this.Description = String.Format("Part {0}/{1} of image", index, total);
        }

        public void SetImageDataSource(byte[] data, int start)
        {
            this._data = data;
            this._start = start;
        }

        public override string GetTextData()
        {
            return "#I#" + this._len + ";" + this._index + ";" + this._total + ";" + this._time.ToString("ddMMyy;HHmmss;") + this._name;
        }

        public override byte[] GetData()
        {
            var str = this.GetTextData() + "\r\n";
            var textlen = System.Text.Encoding.UTF8.GetByteCount(str);
            var data = new byte[textlen + this._len];
            Array.Copy(System.Text.Encoding.UTF8.GetBytes(str), data, textlen);
            Array.Copy(this._data, this._start, data, textlen, this._len);
            return data;
        }
    }

    public class ImageMessageAns : Message
    {
        private int _index, _result;

        public ImageMessageAns(string ans)
        {
            this.Success = false;
            this.MsgType = MessageType.ImageAns;
            this.Description = "Answer to image";
            var items = ans.Split(';');
            if (items.Length == 2)
            {
                if (int.TryParse(items[0], out this._index) && int.TryParse(items[1], out this._result))
                {
                    if (this._result == 1)
                        this.Success = true;
                    this.Description += " part #" + this._index;
                }
            } else if (items.Length == 1)
            {
                this._index = -1;
                if (int.TryParse(items[0], out this._result) && this._result == 1)
                {
                    this.Success = true;
                    this.Description = "Image received successfully";
                }
            }
        }

        public override string GetTextData()
        {
            if (this._index != -1)
                return "#AI#" + this._index + ";" + this._result;
            else
                return "#AI#" + this._result;
        }
    }

    public class ImagesBatch : BatchSender
    {
        public ImagesBatch(MessagesCommunicator mc, byte[] data, int chunk_size, string name)
            : base(mc)
        {
            var total = (int)Math.Ceiling(data.Length / (double)chunk_size);
            var messages = new ImageMessage[total];
            var time = DateTime.Now.ToUniversalTime();
            for (var i = 0; i < total; i++)
            {
                var len = chunk_size;
                if (i == total - 1)
                    len = data.Length - chunk_size * i;
                messages[i] = new ImageMessage(len, i, total - 1, time, name);
                messages[i].SetImageDataSource(data, chunk_size * i);
            }
            this.SetMessages(messages);
        }
    }

}