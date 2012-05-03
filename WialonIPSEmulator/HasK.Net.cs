using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HasK.Data;

namespace HasK.Net
{
    /// <summary>
    /// TCP Client based on non-blocking sockets
    /// </summary>
    public class TCPClient : IDisposable
    {
        private bool _working = false;
        private Socket _sock = null;
        private Thread _run = null;
        private BinDataQueue _buffer;
        private byte[] _send_buffer = null;

        public delegate void ConnectDelegate(TCPClient client);
        public delegate void DisconnectDelegate(TCPClient client);
        public delegate void ErrorDelegate(TCPClient client, SocketException exc);
        public delegate void ReceiveDelegate(TCPClient client, BinDataQueue buffer);
        public delegate void SentDelegate(TCPClient client, int sent);

        public string Host { get; private set; }
        public UInt16 Port { get; private set; }
        public bool Connected
        {
            get
            {
                if (this._sock != null)
                    return this._sock.Connected;
                return false;
            }
        }
        public SocketError ErrorCode { get; private set; }

        public event ConnectDelegate OnConnect;
        public event DisconnectDelegate OnDisconnect;
        public event ErrorDelegate OnError;
        public event ReceiveDelegate OnReceive;
        public event SentDelegate OnSent;

        public TCPClient(string host, UInt16 port)
        {
            this.Host = host;
            this.Port = port;
        }

        public void Connect()
        {
            if (!this.Connected)
            {
                if (this._sock != null)
                    this._sock.Close();
                this._buffer = new BinDataQueue();
                this._sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._run = new Thread(new ThreadStart(this.Run));
                this._run.Start();
            }
        }

        public void Disconnect()
        {
            if (this.Connected)
            {
                this._working = false;
            }
        }

        public void Send(byte[] data)
        {
            this._send_buffer = new byte[data.Length];
            data.CopyTo(this._send_buffer, 0);
        }

        private void Run()
        {
            try
            {
                byte[] temp = new byte[100];
                this._sock.Connect(this.Host, this.Port);
                if (this.Connected)
                {
                    this._sock.Blocking = false;
                    this._working = true;
                    if (this.OnConnect != null) this.OnConnect(this);
                    while (this._working)
                    {
                        try
                        {
                            if (!this._sock.Connected)
                                break;
                            // writing
                            if (this._send_buffer != null)
                            {
                                var sent = this._sock.Send(this._send_buffer);
                                this._send_buffer = null;
                                if (this.OnSent != null) this.OnSent(this, sent);
                            }
                            // reading
                            if (this._sock.Available > 0)
                            {
                                var pos = this._buffer.BeginLockWrite();
                                var data = this._buffer.GetLockedData();
                                var size = this._sock.Receive(data, (int)pos, (int)(this._buffer.Capasiti - this._buffer.InBufferSize), SocketFlags.None);
                                if (size > 0)
                                {
                                    this._buffer.EndLockWrite((uint)size);
                                    if (this.OnReceive != null) this.OnReceive(this, this._buffer);
                                } else
                                    this._buffer.EndLockWrite(0);
                            } else
                            {
                                SocketError errCode;
                                this._sock.Receive(temp, 0, 0, SocketFlags.None, out errCode);
                                if (errCode != SocketError.WouldBlock && errCode != SocketError.Success)
                                {
                                    this._working = false;
                                    this.ErrorCode = errCode;
                                }
                            }
                        }
                        catch (SocketException exc)
                        {
                            switch (exc.SocketErrorCode)
                            {
                                case SocketError.ConnectionReset:
                                    this._working = false;
                                    this.ErrorCode = exc.SocketErrorCode;
                                    break;
                                case SocketError.WouldBlock:
                                    break;
                                default:
                                    this._working = false;
                                    break;
                            }
                        }
                        Thread.Sleep(1);
                    }
                }
            }
            catch (SocketException exc)
            {
                this.ErrorCode = exc.SocketErrorCode;
                if (this.OnError != null) this.OnError(this, exc);
                switch (exc.SocketErrorCode)
                {
                    case SocketError.ConnectionRefused:
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        this.Disconnect();
                        break;
                }
            }
            this._sock.Close();
            if (this.OnDisconnect != null) this.OnDisconnect(this);
        }

        public override string ToString()
        {
            return String.Format("TCPClient({0}, {1})", this.Host, this.Port);
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this._sock != null)
                this._sock.Close();
        }

        #endregion
    }
}