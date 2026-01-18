using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    public class CodeplugChannel
    {

        public enum ChannelType
        {
            [XmlEnum("ANALOG")]
            ANALOG,
            [XmlEnum("DIGITAL")]
            DIGITAL
        } 
        private ushort _number;
        private string _name;
        private ChannelType _type;
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
        [XmlAttribute("Type")]
        public ChannelType Type
        {
            get => _type;
            set => _type = value;
        }
        [XmlAttribute("Rx")]
        public uint RxFrequency
        {
            get => _rxFrequency;
            set => _rxFrequency = value;
        }
        public CodeplugChannel()
        {
            _number = 0;
            _name = "Вызывной 2 м";
            _type = ChannelType.ANALOG;
            _rxFrequency = 145500000;
        }

        public CodeplugChannel (ushort number, string name, ChannelType type, uint rxFrequency)
        {
            _number = number;   
            _name = name;
            _type = type;
            _rxFrequency = rxFrequency;
        }
        public override string ToString()
        {
            return _name ?? string.Empty;
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
