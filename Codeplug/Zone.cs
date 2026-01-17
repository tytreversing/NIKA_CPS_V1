using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static NIKA_CPS_V1.Codeplug.Channel;

namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    public class Zone
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
        [XmlAttribute("Channels")]
        public List<ushort> Channels
        {
            get => _channels;
            set => _channels = value;
        }

        public Zone()
        {
            _number = 0;
            _name = "Зона 1";
            _channels = new List<ushort>();
        }

        public Zone(byte number, string name, List<ushort> channels)
        {
            _number = number;   
            _name = name;
            _channels = channels;
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
