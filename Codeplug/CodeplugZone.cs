using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    public class CodeplugZone
    {

        private byte _number;
        private string _name;
        private List<ushort> _channels;

        [XmlAttribute("Number")]
        public byte Number
        {
            get => _number;
            set => _number = value;
        }
        [XmlAttribute("Name")]
        public string Name
        {
            get => _name;
            set => _name = ValidateLength(value);
        }

        [XmlElement("Channel")]
        public List<ushort> Channels
        {
            get => _channels;
            set => _channels = value;
        }

        public CodeplugZone()
        {
            _number = 0;
            _name = "Зона 1";
            _channels = new List<ushort>();
        }

        public CodeplugZone(byte number, string name)
        {
            _number = number;   
            _name = name;
            _channels = new List<ushort>();
        }

        private string ValidateLength(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value.Length > 16)
            {
                return value.Substring(0, 16);
            }

            return value;
        }
    }
}
