using System;
using System.Xml.Serialization;

namespace WialonIPSEmulator
{
    /// <summary>
    /// Represent a one-device information
    /// </summary>
    public class DeviceInfo
    {
        [XmlAttribute]
        public string ID { get; set; }
        [XmlAttribute]
        public string Password { get; set; }
        [XmlAttribute]
        public string Name { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public UInt16? Speed { get; set; }
        public UInt16? Heading { get; set; }
        public int? Altitude { get; set; }
        public byte? Sats { get; set; }
        public double? HDOP { get; set; }
        public bool Alarm { get; set; }
        public uint? Inputs { get; set; }
        public uint? Outputs { get; set; }

        [XmlArray]
        public Double[] Adcs { get; set; }

        public string IButton { get; set; }

        [XmlArray]
        public WialonIPS.CustomParameter[] CustomParameters { get; set; }

        public DeviceInfo()
        {
            this.ID = "";
            this.Password = "";
            this.Name = "";
            this.Adcs = new double[0];
            this.IButton = "NA";
        }

        public override string ToString()
        {
            var res = this.ID;
            if (this.Name != "")
                res = this.Name + " (" + res + ")";
            return res;
        }
    }
}