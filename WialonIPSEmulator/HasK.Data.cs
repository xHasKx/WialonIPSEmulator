using System;
using System.Text;
using System.Threading;

namespace HasK.Data
{
    public delegate void BinDataQueueOperation(BinDataQueue queue, uint in_buffer_size);

    public class BinDataQueue
    {
        private readonly object _lock = new object();
        private byte[] _data;
        private uint _rpos, _wpos, _len;
        private bool _locked = false;

        /// <summary>
        /// Returns capasiti of data buffer
        /// </summary>
        public uint Capasiti { get; private set; }
        /// <summary>
        /// Returns amount of data in buffer
        /// </summary>
        public uint InBufferSize { get { return this._len; } }
        /// <summary>
        /// This event will be raised after writing data to buffer
        /// </summary>
        public event BinDataQueueOperation OnWrite;
        /// <summary>
        /// Create binary data queue wich specified buffer capasiti
        /// </summary>
        /// <param name="capasiti">Data buffer capasiti</param>
        public BinDataQueue(uint capasiti)
        {
            this.Capasiti = capasiti;
            this._data = new byte[capasiti];
            this._len = 0;
            this._rpos = 0;
            this._wpos = 0;
        }
        /// <summary>
        /// Create binary data queue wich default buffer capasiti
        /// </summary>
        public BinDataQueue() : this(1024 * 1024 * 4) { }
        /// <summary>
        /// Begin sequence of external writing. Lock buffer and returns position in buffer to write
        /// </summary>
        /// <returns>Returns position in buffer to write data</returns>
        public uint BeginLockWrite()
        {
            Monitor.Enter(this._lock);
            this._locked = true;
            return this._wpos;
        }
        /// <summary>
        /// Returns data array after BeginLockWrite call
        /// </summary>
        /// <returns></returns>
        public byte[] GetLockedData()
        {
            if (this._locked)
                return this._data;
            throw new Exception("Cannot return unlocked data. You should call BeginLockWrite first");
        }
        /// <summary>
        /// End sequence of external writing and unlock buffer.
        /// </summary>
        /// <param name="size"></param>
        public void EndLockWrite(uint size)
        {
            this._locked = false;
            if (size > 0)
            {
                this._wpos += size;
                this._len += size;
                Monitor.Exit(this._lock);
                if (this.OnWrite != null) this.OnWrite(this, this._len);
            } else
                Monitor.Exit(this._lock);
        }
        /// <summary>
        /// Write data to buffer and call OnWrite event after that
        /// </summary>
        /// <param name="data">Data which should be written to buffer</param>
        public void Push(byte[] data)
        {
            lock (this._lock)
            {
                data.CopyTo(this._data, _wpos);
                this._wpos += (uint)data.Length;
                this._len += (uint)data.Length;
            }
            if (this.OnWrite != null) this.OnWrite(this, this._len);
        }
        /// <summary>
        /// Write data to buffer as text
        /// </summary>
        /// <param name="text">Text which should be written to buffer</param>
        /// <param name="enc">Text encoding</param>
        public void Push(string text, Encoding enc)
        {
            var data = enc.GetBytes(text);
            this.Push(data);
        }
        /// <summary>
        /// Write data to buffer as text in UTF-8 encoding
        /// </summary>
        /// <param name="text">Text which should be written to buffer</param>
        public void Push(string text)
        {
            this.Push(text, Encoding.UTF8);
        }
        /// <summary>
        /// Read data from buffer as text up to specified string
        /// </summary>
        /// <param name="end">String to which you want to read the data from the buffer</param>
        /// <param name="enc">Encoding of text</param>
        /// <returns>Returns desired string. Returns all data as text if <paramref name="end"/> is empty string</returns>
        public string ReadText(string end, Encoding enc)
        {
            lock (this._lock)
            {
                if (this._len == 0)
                    return "";
                if (end == "" || end == null)
                {
                    // read all data as text
                    var res = enc.GetString(this._data, (int)this._rpos, (int)(this._len - this._rpos));
                    // reset
                    this._len = 0;
                    this._wpos = 0;
                    this._rpos = 0;
                    return res;
                } else
                {
                    // read data as text up to end string
                    var res = enc.GetString(this._data, (int)this._rpos, (int)(this._len - this._rpos));
                    var pos = res.IndexOf(end);
                    if (pos == -1)
                        return "";
                    uint len = (uint)(pos + end.Length);
                    res = res.Substring(0, (int)len);
                    this._rpos += (uint)enc.GetByteCount(res);
                    if (this._rpos == this._wpos)
                    {
                        // reset
                        this._len = 0;
                        this._wpos = 0;
                        this._rpos = 0;
                    }
                    return res;
                }
            }
        }
        /// <summary>
        /// Read data from buffer as text in UTF-8 up to specified string
        /// </summary>
        /// <param name="end">String to which you want to read the data from the buffer</param>
        /// <returns>Returns desired string. Returns all data as text if <paramref name="end"/> is empty string</returns>
        public string ReadText(string end)
        {
            return this.ReadText(end, Encoding.UTF8);
        }
        /// <summary>
        /// Read all data from buffer as text in UTF-8
        /// </summary>
        /// <returns></returns>
        public string ReadText()
        {
            return this.ReadText("");
        }
        /// <summary>
        /// Read from buffer data of specified size
        /// </summary>
        /// <param name="size">Size of data which must be read</param>
        /// <returns>Returns desired amount of data. Returns all data if <paramref name="size"/> parameter is 0</returns>
        public byte[] ReadBin(uint size)
        {
            lock (this._lock)
            {
                if (size == 0)
                {
                    var res = new byte[this._len];
                    this._data.CopyTo(res, this._len);
                    // reset
                    this._len = 0;
                    this._wpos = 0;
                    this._rpos = 0;
                    return res;
                } else
                {
                    if (size > this._len - this._rpos)
                        size = this._len - this._rpos;
                    var res = new byte[size];
                    Array.Copy(this._data, this._rpos, res, 0, size);
                    this._rpos += size;
                    if (this._rpos == this._wpos)
                    {
                        // reset
                        this._len = 0;
                        this._wpos = 0;
                        this._rpos = 0;
                    }
                    return res;
                }
            }
        }
    }

}