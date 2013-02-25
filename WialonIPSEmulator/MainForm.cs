using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using WialonIPS;
using System.IO;
using System.Threading;

namespace WialonIPSEmulator
{
    public delegate void AddToTextBoxDelegate(string line);

    public partial class MainForm : Form, IErrorReporter
    {
        private System.Threading.Timer tmrPing;

        private int _ping_interval = 240000; // 4 minutes
        private int _image_part_size = 50 * 1024; // 50kb
        private int _max_image_size = 9 * 1024 * 1024; // 9 Mb;
        private int _autosend_sec = 0;
        private int _adcs_start_height = 0;
        private int _custom_params_start_height = 0;
        private bool _closed = false, _ready = false;
        private string _help_link = "https://github.com/xHasKx/WialonIPSEmulator/wiki/Help";

        private AdcParamControl[] _adc_params;
        private CustomParamControl[] _custom_params;

        private WialonIPS.Message send_data_message = null;

        private string _settings_file_name, _log_file_name;

        private CMessages Messages;

        private List<ServerInfo> _servers = new List<ServerInfo>();
        private List<DeviceInfo> _devices = new List<DeviceInfo>();

        private ServerInfo _selected_server;
        private DeviceInfo _selected_device;

        private MessagesCommunicator _mc = null;

        public AddToTextBoxDelegate AddToLog, AddToMessages;
        public CLog Log;
        public CSettings Settings { get; private set; }
        // =========================================================================================================
        public MainForm()
        {
            InitializeComponent();
            this.AddToLog = new AddToTextBoxDelegate(this.AddToLogMethod);
            this.AddToMessages = new AddToTextBoxDelegate(this.AddToMessagesMethod);
            this.Log = new CLog(this.tbLog, this.AddToLog);
            this.Messages = new CMessages(this.tbMessages, this.AddToMessages);
            var base_path = Environment.GetEnvironmentVariable("USERPROFILE");
            if (base_path == null)
                base_path = Environment.GetEnvironmentVariable("HOME");
            if (base_path == null)
                base_path = Path.GetDirectoryName(Application.ExecutablePath);
            this._settings_file_name = Path.Combine(base_path, ".wialon_ips_emulator.settings");
            this._log_file_name = Path.Combine(base_path, ".wialon_ips_emulator.log");
            this.tmrPing = new System.Threading.Timer(new TimerCallback(this.PingTimerCallback), null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private void PingTimerCallback(object state)
        {
            this._mc.Send(new WialonIPS.PingMessage());
        }

        private void AddToLogMethod(string line)
        {
            this.tbLog.AppendText(line);
            this.tbLog.AppendText(Environment.NewLine);
        }

        private void AddToMessagesMethod(string line)
        {
            this.tbMessages.AppendText(line.Replace("\n", Environment.NewLine));
            this.tbMessages.AppendText(Environment.NewLine);
        }

        #region IErrorReporter Members

        public void OnException(string when, Exception e)
        {
            this.Log.PostHead("Communication error", when + ": " + e);
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            this._adcs_start_height = this.btnADCsCount.Top + this.btnADCsCount.Height + 20;
            this._custom_params_start_height = this.btnEditCustomParams.Top + this.btnEditCustomParams.Height + 20;

            try
            {
                this.Settings = CSettings.Load(this._settings_file_name);
                this.Log.PostHead("Emulator", "Settings loaded successfully");
            }
            catch (Exception)
            {
                this.Log.PostHead("Emulator", "Cannot load settings");
                this.Settings = new CSettings();
            }
            // Two fixed servers
            this._servers.Add(new ServerInfo("193.193.165.165", 20332, "Wialon Hosting", true));
            this._servers.Add(new ServerInfo("193.193.165.166", 20332, "GPS Trace Orange", true));
            // Load servers
            this._servers.AddRange(this.Settings.Servers);
            this.cbServer.Items.AddRange(this._servers.ToArray());
            this.cbServer.SelectedIndex = 0;
            // Load devices
            this._devices.AddRange(this.Settings.Devices);
            this.cbDevice.Items.AddRange(this._devices.ToArray());
            if (this.cbDevice.Items.Count > 0)
                this.cbDevice.SelectedIndex = 0;

            if (this.Settings.SelectedServer >= 0 && this.cbServer.Items.Count > this.Settings.SelectedServer)
                this.cbServer.SelectedIndex = this.Settings.SelectedServer;
            if (this.Settings.SelectedDevice >= 0 && this.cbDevice.Items.Count > this.Settings.SelectedDevice)
                this.cbDevice.SelectedIndex = this.Settings.SelectedDevice;

            this.UpdateSettingsValues();

            this.ActivateConnectControls(true);
            this.UpdateAutoSendOptions(cbAutoSend.Checked);
            this._autosend_sec = (int)this.numAutoSendInterval.Value;
            this.UpdateParametersEnabled();

            this.Log.PostHead("Emulator", "Ready");
        }

        private void UpdateSettingsValues()
        {
            if (this.Settings.LastWindowRect.Size.Width > 0)
            {
                this.WindowState = this.Settings.LastWindowState;
                this.Size = this.Settings.LastWindowRect.Size;
                this.Location = this.Settings.LastWindowRect.Location;
            }

            var dev = this.cbDevice.SelectedItem as DeviceInfo;
            if (dev != null)
            {
                if (dev.Latitude.HasValue) this.tbLat.Text = dev.Latitude.Value.ToString();
                if (dev.Longitude.HasValue) this.tbLon.Text = dev.Longitude.Value.ToString();
                if (dev.Altitude.HasValue) this.tbAlt.Text = dev.Altitude.Value.ToString();
                if (dev.Heading.HasValue) this.tbHeading.Text = dev.Heading.Value.ToString();
                if (dev.Speed.HasValue) this.tbSpeed.Text = dev.Speed.Value.ToString();
                if (dev.Sats.HasValue) this.tbSats.Text = dev.Sats.Value.ToString();
                if (dev.Inputs.HasValue) this.ioInputs.Value = dev.Inputs.Value;
                if (dev.Outputs.HasValue) this.ioOutputs.Value = dev.Outputs.Value;
                if (dev.HDOP.HasValue) this.tbHdop.Text = dev.HDOP.Value.ToString();
                if (dev.IButton != "NA")
                {
                    this.tbIButton.Text = dev.IButton;
                    this.cbiButtonEnabled.Checked = true;
                } else
                {
                	this.cbiButtonEnabled.Checked = false;
                }
                this.cbAlarm.Checked = dev.Alarm;
            }
            this.UpdateAdcs();
            this.UpdateCustomParams();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnServersConfig_Click(object sender, EventArgs e)
        {
            var cform = new ServersConfigForm(this, this._servers.ToArray());
            if (cform.ShowConfig() == DialogResult.OK)
            {
                this._servers.Clear();
                this._servers.AddRange(cform.GetServers());
                this.cbServer.Items.Clear();
                this.cbServer.Items.AddRange(this._servers.ToArray());
                this.cbServer.SelectedIndex = 0;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._closed = true;
            if (this._mc != null)
            {
                if (this._mc.IsConnected)
                    this._mc.Disconnect();
            }

            this.Log.PostHead("Emulator", "Work is done");
            // Save servers
            this.Settings.Servers.Clear();
            for (int i = 2; i < this._servers.Count; i++)
                this.Settings.Servers.Add(this._servers[i]);
            // Save devices
            this.Settings.Devices.Clear();
            this.Settings.Devices.AddRange(this._devices);
            this.Settings.SelectedServer = this.cbServer.SelectedIndex;
            this.Settings.SelectedDevice = this.cbDevice.SelectedIndex;
            // Other settings
            this.Settings.LastWindowState = this.WindowState;
            this.Settings.LastWindowRect = new Rectangle(this.Location, this.Size);
            try
            {
                // Save all settings and log
                this.Settings.Save(this._settings_file_name);
                this.Log.PostHead("Emulator", "Settings saved");
                File.AppendAllText(this._log_file_name, this.tbLog.Text);
            }
            catch (Exception)
            {
                // Очень жаль...
            }
        }

        private void btnDevicesConfig_Click(object sender, EventArgs e)
        {
            this.ShowConfigDevicesDialog();
        }

        private void ShowConfigDevicesDialog()
        {
            var cform = new DevicesConfigForm(this, this._devices.ToArray());
            if (cform.ShowConfig() == DialogResult.OK)
            {
                this._devices.Clear();
                this._devices.AddRange(cform.GetDevices());
                this.cbDevice.Items.Clear();
                this.cbDevice.Items.AddRange(this._devices.ToArray());
                if (this.cbDevice.Items.Count > 0)
                    this.cbDevice.SelectedIndex = 0;
            }
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dlgSaveLog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(this.dlgSaveLog.FileName, this.tbLog.Text);
        }

        #region Connect/Disconnect/Select items/Activate controls logic and handlers
        private class DeviceNotSelected : Exception { };

        private delegate void ActivateConnectControlsFuncDelegate(bool state);

        void ActivateSendControls(bool state)
        {
            this.gbSendRaw.Enabled = state;
            this.tbSendRaw.Text = "";

            this.gbSendMessage.Enabled = state;
            this.tbSendMessage.Text = "";

            this.gbPos.Enabled = state;
            this.panParams.Enabled = state && this.rbFullData.Checked;
            this.gbSendPacket.Enabled = state;
        }

        void ActivateConnectControlsFunc(bool state)
        {
            this.cbServer.Enabled = state;
            this.cbDevice.Enabled = state;
            this.btnServersConfig.Enabled = state;
            this.btnDevicesConfig.Enabled = state;
            this.btnConnect.Enabled = state;

            this.btnDisconnect.Enabled = !state;

            this.ActivateSendControls(!state);

            if (gbPos.Enabled)
                this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());
        }

        void ActivateConnectControls(bool state)
        {
            try
            {
                this.cbServer.Invoke(new ActivateConnectControlsFuncDelegate(this.ActivateConnectControlsFunc), new object[] { state });
            }
            catch (Exception)
            {

            }
        }

        void ConnectClick()
        {
            try
            {
                var si = this.cbServer.SelectedItem as ServerInfo;
                var di = this.cbDevice.SelectedItem as DeviceInfo;
                if (di == null)
                    throw new DeviceNotSelected();

                if (this._mc != null)
                    this._mc.Dispose();

                this._mc = new MessagesCommunicator(si.Host, si.Port, this);
                this._mc.OnConnect += new MessageConnectorOperation(_mc_OnConnect);
                this._mc.OnDisconnect += new MessageConnectorOperation(_mc_OnDisconnect);
                this._mc.OnReceive += new MessageOperationDelegate(_mc_OnReceive);
                this._mc.OnSent += new MessageOperationDelegate(_mc_OnSent);
                this._mc.Connect();
            }
            catch (DeviceNotSelected)
            {
                MessageBox.Show("Please create device first", "Cannot connect");
                this.ShowConfigDevicesDialog();
            }
            catch (Exception exc)
            {
                this.ActivateConnectControls(true);
                this.Log.PostHead("Exception", "Connect: " + exc);
                MessageBox.Show(exc.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.ConnectClick();
        }

        void _mc_OnSent(MessagesCommunicator comm, WialonIPS.Message msg)
        {
            this.Log.PostHead("<<<", msg.ToString());
            if (msg.MsgType == MessageType.Message)
                this.Messages.Sent((msg as WialonIPS.MessageMessage).Text);
            if (this.Settings.SendPingPackets)
                this.tmrPing.Change(this._ping_interval, this._ping_interval);
        }

        void _mc_OnReceive(MessagesCommunicator comm, WialonIPS.Message msg)
        {
            this.Log.PostHead(">>>", msg.ToString());
            if (msg.MsgType == MessageType.Message)
                this.Messages.Received((msg as WialonIPS.MessageMessage).Text);
            if (msg.MsgType == MessageType.LoginAns && !(msg as LoginAnsMessage).Success)
                Disconnect();
        }

        void _mc_OnDisconnect(MessagesCommunicator comm)
        {
            this.Log.PostHead("Emulator", "Disconnected from " + this._selected_server);
            if (!this._closed) this.ActivateConnectControls(true);
            this.tmrPing.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        void _mc_OnConnect(MessagesCommunicator comm)
        {
            this.ActivateConnectControls(false);
            this.Log.PostHead("Emulator", "Connected to " + this._selected_server);
            this._mc.Send(new LoginMessage(this._selected_device.ID, this._selected_device.Password));
            if (this.Settings.SendPingPackets)
                this.tmrPing.Change(this._ping_interval, this._ping_interval);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        void Disconnect()
        {
            try
            {
                if (this._mc != null)
                {
                    if (this._mc.IsConnected)
                        this._mc.Disconnect();
                }
                this.ActivateConnectControls(true);
            }
            catch (Exception exc)
            {
                this.ActivateConnectControls(true);
                this.Log.PostHead("Exception", "Disconnect: " + exc);
                MessageBox.Show(exc.Message);
            }
        }

        private void cbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._selected_server = this.cbServer.SelectedItem as ServerInfo;
        }

        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._selected_device = this.cbDevice.SelectedItem as DeviceInfo;
            this.UpdateSettingsValues();
        }
        #endregion
        
        #region Send raw and driver message
        void SendRaw()
        {
            var text = this.tbSendRaw.Text.Trim();
            this.tbSendRaw.Focus();
            this.tbSendRaw.SelectAll();
            if (text != "")
            {
                var msg = WialonIPS.Message.Parse(text);
                if (msg.Success)
                    this._mc.Send(msg);
                else
                {
                    this.Log.PostHead("Emulator", "Unknown packet not sent: " + text);
                    System.Media.SystemSounds.Exclamation.Play();
                }
            }
        }

        private void btnSendRaw_Click(object sender, EventArgs e)
        {
            this.SendRaw();
        }

        private void tbSendRaw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                this.SendRaw();
            }
        }

        void SendMessage()
        {
            var text = this.tbSendMessage.Text.Trim();
            if (text != "")
            {
                this.tbSendMessage.Clear();
                this.tbSendMessage.Focus();
                var msg = new MessageMessage(text);
                this._mc.Send(msg);
            }
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void tbSendMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                this.SendMessage();
            }
        }
#endregion

        #region Send data packet
        private void UpdateAutoSendOptions(bool s)
        {
            numAutoSendInterval.Enabled = s;
            labTextSec.Enabled = s;
            labTextNextSend.Enabled = s;
            labNextSend.Enabled = s;
        }

        private void cbAutoSend_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateAutoSendOptions(cbAutoSend.Checked);
            this.tmrAutoSend.Enabled = cbAutoSend.Checked;
            if (cbAutoSend.Checked)
            {
                this._autosend_sec = (int)this.numAutoSendInterval.Value;
                this.labNextSend.Text = this._autosend_sec.ToString();
            }
        }

        private void tmrAutoSend_Tick(object sender, EventArgs e)
        {
            this._autosend_sec -= 1;
            if (this._autosend_sec <= 0)
                this.SendDataPacket();
            else
                this.labNextSend.Text = this._autosend_sec.ToString();
        }

        private void SendDataPacket()
        {
            // reset timer
            this._autosend_sec = (int)this.numAutoSendInterval.Value;
            this.labNextSend.Text = this._autosend_sec.ToString();
            // send data packet to server

            this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());

            if (this.send_data_message != null)
                this._mc.Send(this.send_data_message);
        }

        private void numAutoSendInterval_ValueChanged(object sender, EventArgs e)
        {
            this._autosend_sec = (int)this.numAutoSendInterval.Value;
            this.labNextSend.Text = this._autosend_sec.ToString();
            this.tmrAutoSend.Start();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            this.SendDataPacket();
        }
        #endregion

        private void UpdateParametersEnabled()
        {
            this.panParams.Enabled = this.rbFullData.Checked;
        }

        private void UpdateDataPacketText(DateTime time)
        {
            this.send_data_message = null;
            double? lat = null, lon = null;
            int? alt = null;
            UInt16? speed = null;
            UInt16? heading = null;
            byte? sats = null;
            double dvalue = 0;
            int ivalue = 0;
            UInt16 uivalue = 0;
            byte bvalue = 0;
            if (double.TryParse(this.tbLat.Text, out dvalue)) lat = dvalue;
            if (double.TryParse(this.tbLon.Text, out dvalue)) lon = dvalue;
            if (int.TryParse(this.tbAlt.Text, out ivalue)) alt = ivalue;
            if (UInt16.TryParse(this.tbSpeed.Text, out uivalue)) speed = uivalue;
            if (UInt16.TryParse(this.tbHeading.Text, out uivalue)) heading = uivalue;
            if (byte.TryParse(this.tbSats.Text, out bvalue)) sats = bvalue;

            var dev = (this.cbDevice.SelectedItem as DeviceInfo);
            dev.Latitude = lat;
            dev.Longitude = lon;
            dev.Altitude = alt;
            dev.Speed = speed;
            dev.Heading = heading;
            dev.Sats = sats;

            if (this.rbFullData.Checked)
            {
                // full data packet
                dev.HDOP = WialonIPS.Message.ParseDouble(this.tbHdop.Text);
                if (this.cbiButtonEnabled.Checked)
                    dev.IButton = this.tbIButton.Text;
                else
                    dev.IButton = "NA";
                dev.Alarm = this.cbAlarm.Checked;
                dev.Inputs = this.ioInputs.Value;
                dev.Outputs = this.ioOutputs.Value;
                if (this._adc_params.Length > 0)
                {
                    dev.Adcs = new double[this._adc_params.Length];
                    for (var i = 0; i < dev.Adcs.Length; i++)
                    {
                        if (this._adc_params[i] != null)
                        {
                            double val = 0;
                            if (double.TryParse(this._adc_params[i].Text.Text, out val))
                                dev.Adcs[i] = val;
                        } else
                            dev.Adcs[i] = 0;
                    }
                } else
                    dev.Adcs = null;
                if (dev.CustomParameters != null) {
                    for (var i = 0; i < dev.CustomParameters.Length; i++)
                    {
                    	var text = this._custom_params[i].Value.Text;
                    	if (((CustomParamType)this._custom_params[i].Type.SelectedItem) == CustomParamType.Double)
                    		text = text.Replace(',', '.');
                        dev.CustomParameters[i] = new CustomParameter(
                            this._custom_params[i].Name.Text,
                            (CustomParamType)this._custom_params[i].Type.SelectedItem,
                            text);
                        if (dev.CustomParameters[i].Name == "SOS" && dev.CustomParameters[i].Type == CustomParamType.Int)
                        {
                            dev.Alarm = (dev.CustomParameters[i].Value == "1");
                            this.cbAlarm.CheckedChanged -= this.UpdateCurrentPacketDataHandler;
                            this.cbAlarm.Checked = dev.Alarm;
                            this.cbAlarm.CheckedChanged += new EventHandler(this.UpdateCurrentPacketDataHandler);
                        }
                    }
                }
                this.send_data_message = new WialonIPS.DataMessage(time, lat, lon, alt, speed, heading, sats,
                    dev.HDOP, dev.IButton, dev.Alarm, dev.Inputs, dev.Outputs, dev.Adcs, dev.CustomParameters);
            } else
            {
                // short data packet
                this.send_data_message = new WialonIPS.ShortDataMessage(time, lat, lon, alt, speed, heading, sats);
            }
            if (this.send_data_message != null)
                this.tbResultPacket.Text = this.send_data_message.GetTextData();
        }

        private void UpdateCurrentPacketDataHandler(object sender, EventArgs e)
        {
            if (this._ready) // Workaround for fields default initialization problem
                this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());
        }

        private void rbShortData_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateParametersEnabled();
            this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());
        }

        private void rbFullData_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateParametersEnabled();
            this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());
        }

        private void tbResultPacket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                e.SuppressKeyPress = true;
                this.tbResultPacket.SelectAll();
            }
        }

        #region Work with images
        void ShowImageProgressBar(bool state)
        {
            this.pbSendImage.Invoke(new ShowImgProgressBarDelegate(ShowImgProgressBar), state);
        }

        delegate void ShowImgProgressBarDelegate(bool state);

        void ShowImgProgressBar(bool state)
        {
            if (state)
                this.pbSendImage.Value = this.pbSendImage.Minimum;
            this.pbSendImage.Visible = state;
            this.tbSendMessage.Visible = !state;
            this.btnSendMessage.Visible = !state;
            this.btnSendImage.Visible = !state;
            this.ActivateSendControls(!state);
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            if (this.dlgOpenImage.ShowDialog() == DialogResult.OK)
            {
                var fi = new FileInfo(this.dlgOpenImage.FileName);
                if (fi.Length <= this._max_image_size)
                {
                    var data = File.ReadAllBytes(this.dlgOpenImage.FileName);
                    var ib = new ImagesBatch(this._mc, data, this._image_part_size, Path.GetFileName(fi.FullName));
                    ib.Interval = 100;
                    ib.OnDone += new BatchSenderDone(ib_OnDone);
                    ib.OnProcess += new BatchSenderProcess(ib_OnProcess);
                    this.ShowImageProgressBar(true);
                    this.Log.PostHead("Emulator", "Start sending image");
                    ib.Start();
                } else
                    MessageBox.Show("Image is too large!");
            }
        }

        void ib_OnProcess(BatchSender sender, double process_stage)
        {
            this.SetImageSendPBValue((int)(process_stage * this.pbSendImage.Maximum));
        }

        private void SetImageSendPBValue(int p)
        {
            this.pbSendImage.Invoke(new SetProgressbarValue(SetImgSendPBValue), p);
        }

        delegate void SetProgressbarValue(int value);

        void SetImgSendPBValue(int value)
        {
            this.pbSendImage.Value = value;
        }

        void ib_OnDone(BatchSender sender, DoneReason reason)
        {
            this.Log.PostHead("Emulator", "Stop sending image, reason: " + reason);
            this.ShowImageProgressBar(false);
            if (reason == DoneReason.Success)
                this.Messages.Sent("Image sent success");
            else
                this.Messages.Sent("Cannot sent image: " + reason);
        }
        #endregion
        
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            
            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);
            
            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        
        private void btnADCsCount_Click(object sender, EventArgs e)
        {
            var dev = this.cbDevice.SelectedItem as DeviceInfo;
            var result = "0";
            if (dev.Adcs != null)
                result = dev.Adcs.Length.ToString();
            
            if (InputBox("Count of ADC parameters", "Please enter ADC parameters count:", ref result) != DialogResult.OK)
                return;
            
            byte count = 0;
            if (result != null && result != "")
            {
                if (byte.TryParse(result, out count))
                {
                    if (count <= 32)
                    {
                        var adcs = new double[count];
                        var adcs_len = 0;
                        if (dev.Adcs != null)
                            adcs_len = dev.Adcs.Length;
                        for (var i = 0; i < Math.Min(adcs_len, count); i++)
                            adcs[i] = dev.Adcs[i];
                        dev.Adcs = adcs;
                        this.UpdateAdcs();
                        this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());
                    } else
                        MessageBox.Show("Number of ADCs must be up to 32");
                } else
                    MessageBox.Show("Wrong input");
            }
        }

        private void UpdateAdcs()
        {
            var dev = this.cbDevice.SelectedItem as DeviceInfo;
            if (dev != null)
            {
                this.panParams.SuspendLayout();
                if (this._adc_params != null)
                    foreach (var a in this._adc_params)
                        a.Dispose();
                this._adc_params = new AdcParamControl[dev.Adcs.Length];
                for (var i = 0; i < dev.Adcs.Length; i++)
                {
                    this._adc_params[i] = new AdcParamControl(this.panParams, i + 1, this.btnADCsCount.Left, this._adcs_start_height, this.UpdateCurrentPacketDataHandler);
                    this._adc_params[i].Text.Text = dev.Adcs[i].ToString();
                }
                for (var i = 0; i < dev.Adcs.Length; i++)
                    this._adc_params[i].Text.TextChanged += new EventHandler(UpdateCurrentPacketDataHandler);
                this.panParams.ResumeLayout();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this._ready = true;
            if (this.Settings.AutoConnect)
                this.ConnectClick();
        }

        private void btnEditCustomParams_Click(object sender, EventArgs e)
        {
            var dev = this.cbDevice.SelectedItem as DeviceInfo;
            var result = "0";
            if (dev.CustomParameters != null)
                result = dev.CustomParameters.Length.ToString();
            
            if (InputBox("Count of custom parameters", "Please enter custom parameters count:", ref result) != DialogResult.OK)
                return;
            
            byte count = 0;
            if (result != null && result != "")
            {
                if (byte.TryParse(result, out count))
                {
                    if (count <= 32)
                    {
                        if (count > 0)
                        {
                            var cparams = new CustomParameter[count];
                            if (dev.CustomParameters != null)
                            {
                                if (count >= dev.CustomParameters.Length)
                                    for (var i = 0; i < Math.Max(dev.CustomParameters.Length, count); i++)
                                    {
                                        if (i < Math.Min(dev.CustomParameters.Length, count))
                                            cparams[i] = new CustomParameter(dev.CustomParameters[i]);
                                        else
                                            cparams[i] = new CustomParameter();
                                    } else
                                    for (var i = 0; i < count; i++)
                                        cparams[i] = new CustomParameter(dev.CustomParameters[i]);
                            } else
                                for (var i = 0; i < count; i++)
                                    cparams[i] = new CustomParameter();
                            dev.CustomParameters = cparams;
                        } else
                            dev.CustomParameters = null;
                        this.UpdateCustomParams();
                        this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());
                    } else
                        MessageBox.Show("Number of custom parameters must be up to 32");
                } else
                    MessageBox.Show("Wrong input");
            }
        }

        private void UpdateCustomParams()
        {
            var dev = this.cbDevice.SelectedItem as DeviceInfo;
            if (dev != null)
            {
                this.panParams.SuspendLayout();
                if (this._custom_params != null)
                    foreach (var p in this._custom_params)
                        p.Dispose();
                if (dev.CustomParameters != null)
                {
                    this._custom_params = new CustomParamControl[dev.CustomParameters.Length];
                    for (var i = 0; i < this._custom_params.Length; i++)
                    {
                        this._custom_params[i] = new CustomParamControl(this.panParams, i, this.btnEditCustomParams.Left, this._custom_params_start_height, this.UpdateCurrentPacketDataHandler, dev.CustomParameters[i]);
                    }
                }
                this.panParams.ResumeLayout();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm(this.Settings).ShowDialog();
            if (this._mc != null && this._mc.IsConnected)
            {
                if (!this.Settings.SendPingPackets)
                    this.tmrPing.Change(Timeout.Infinite, Timeout.Infinite);
                else
                    this.tmrPing.Change(0, this._ping_interval);
            }
        }

        private void contentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var si = new System.Diagnostics.ProcessStartInfo(this._help_link);
            si.UseShellExecute = true;
            System.Diagnostics.Process.Start(si);
        }
        
        void CbiButtonEnabledCheckedChanged(object sender, EventArgs e)
        {
        	this.tbIButton.Enabled = this.cbiButtonEnabled.Checked;
        	this.UpdateDataPacketText(DateTime.Now.ToUniversalTime());
        }
    }

    [XmlRoot("Settings")]
    public class CSettings
    {
        [XmlArray]
        public List<ServerInfo> Servers { get; set; }
        [XmlArray]
        public List<DeviceInfo> Devices { get; set; }

        public int SelectedServer { get; set; }
        public int SelectedDevice { get; set; }

        public FormWindowState LastWindowState { get; set; }
        public Rectangle LastWindowRect { get; set; }

        public bool AutoConnect { get; set; }
        public bool SendPingPackets { get; set; }

        #region Standard behavior
        public CSettings()
        {
            this.Servers = new List<ServerInfo>();
            this.Devices = new List<DeviceInfo>();
            this.SendPingPackets = true;
        }

        public void Save(string file_name)
        {
            XmlSerializer s = new XmlSerializer(typeof(CSettings));
            TextWriter w = new StreamWriter(file_name);
            s.Serialize(w, this);
            w.Close();
        }

        public static CSettings Load(string file_name)
        {
            CSettings obj;
            XmlSerializer s = new XmlSerializer(typeof(CSettings));
            TextReader r = new StreamReader(file_name);
            obj = (CSettings)s.Deserialize(r);
            r.Close();
            return obj;
        }
        #endregion
    }

    public class CLog
    {
        public CLog(TextBox output, AddToTextBoxDelegate add_to_log)
        {
            this._output = output;
            this._add_to_log = add_to_log;
        }

        public void Post(string line)
        {
            string res = DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss: ") + line;
            try
            {
                this._output.Invoke(this._add_to_log, new Object[] { res });
            }
            catch (Exception)
            {

            }
        }

        public void PostHead(string head, string line)
        {
            this.Post("[" + head + "] " + line);
        }

        private TextBox _output;
        private AddToTextBoxDelegate _add_to_log;
    }

    public class CMessages
    {
        public CMessages(TextBox output, AddToTextBoxDelegate add_to_messages)
        {
            this._output = output;
            this._add_to_messages = add_to_messages;
        }

        public void Sent(string text)
        {
            string res = DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss: [You] ") + text;
            this._output.Invoke(this._add_to_messages, new Object[] { res });
        }

        public void Received(string text)
        {
            string res = DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss: [Server] ") + text;
            this._output.Invoke(this._add_to_messages, new Object[] { res });
        }

        private TextBox _output;
        private AddToTextBoxDelegate _add_to_messages;

    }

    /// <summary>
    /// Server information (host+port+name);
    /// </summary>
    public class ServerInfo
    {
        [XmlAttribute]
        public string Host { get; set; }
        [XmlAttribute]
        public UInt16 Port { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlIgnore]
        public bool ReadOnly { get; private set; }

        public ServerInfo()
        {
            this.Host = "";
            this.Port = 0;
            this.Name = "";
            this.ReadOnly = false;
        }

        public ServerInfo(string host, UInt16 port, string name, bool read_only)
        {
            this.Host = host;
            this.Port = port;
            this.Name = name;
            this.ReadOnly = read_only;
        }

        public override string ToString()
        {
            string res = this.Host + ":" + this.Port;
            if (this.Name != "")
                res = this.Name + " (" + res + ")";
            return res;
        }
    }

    public class AdcParamControl : IDisposable
    {
        public Label Label { get; private set; }
        public TextBox Text { get; private set; }

        public AdcParamControl(Control parent, int num, int left, int top, EventHandler on_change)
        {
            this.Label = new Label();
            this.Text = new TextBox();

            this.Label.Parent = parent;
            this.Text.Parent = parent;

            this.Label.Width = 10;
            this.Label.AutoSize = true;
            this.Label.Text = "ADC" + num + ":";
            this.Label.Top = top + (this.Text.Height + 10) * (num - 1);
            this.Label.Left = left;

            this.Text.Top = this.Label.Top;
            this.Text.Left = this.Label.Left + this.Label.Width + 10;
            this.Text.TextChanged += new EventHandler(on_change);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Label.Dispose();
            this.Text.Dispose();
        }

        #endregion
    }

    public class CustomParamControl : IDisposable
    {

        public TextBox Name { get; private set; }
        public ComboBox Type { get; private set; }
        public TextBox Value { get; private set; }

        public CustomParamControl(Control parent, int index, int left, int top, EventHandler on_change, CustomParameter data)
        {
            this.Name = new TextBox();
            this.Type = new ComboBox();
            this.Value = new TextBox();

            this.Name.MaxLength = 15;

            this.Name.Parent = parent;
            this.Type.Parent = parent;
            this.Value.Parent = parent;

            this.Type.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Type.Items.AddRange(new object[] { CustomParamType.Int, CustomParamType.Double, CustomParamType.Text });

            if (data != null)
            {
                this.Name.Text = data.Name;
                this.Type.SelectedItem = data.Type;
                this.Value.Text = data.Value;
            }

            top += (index * (this.Name.Height + 10));
            this.Name.Location = new Point(left, top);
            this.Type.Location = new Point(left + this.Name.Width + 10, top);
            this.Value.Location = new Point(this.Type.Left + this.Type.Width + 10, top);

            this.Name.TextChanged += new EventHandler(on_change);
            this.Type.SelectedIndexChanged += new EventHandler(on_change);
            this.Value.TextChanged += new EventHandler(on_change);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Name.Dispose();
            this.Type.Dispose();
            this.Value.Dispose();
        }

        #endregion
    }

}
