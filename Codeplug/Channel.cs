using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    public class Channel
    {
        private ushort _number;
        private string _name;
        private uint _rxFrequency;
        [XmlAttribute("Number")]
        public ushort Number
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
        [XmlAttribute("Rx")]
        public uint RxFrequency
        {
            get => _rxFrequency;
            set => _rxFrequency = value;
        }
        public Channel()
        {
            _number = 0;
            _name = "Вызывной 2 м";
            _rxFrequency = 145500000;
        }

        public Channel (ushort number, string name, uint rxFrequency)
        {
            _number = number;   
            _name = name;
            _rxFrequency = rxFrequency;
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
