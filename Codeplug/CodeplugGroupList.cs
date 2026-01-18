using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    public class CodeplugGroupList
    {

        private byte _number;
        private string _name;
        private List<ushort> _contacts;

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

        [XmlElement("Contact")]
        public List<ushort> Contacts
        {
            get => _contacts;
            set => _contacts = value;
        }

        public CodeplugGroupList()
        {
            _number = 0;
            _name = "Список 1";
            _contacts = new List<ushort>();
        }

        public CodeplugGroupList(byte number, string name)
        {
            _number = number;
            _name = name;
            _contacts = new List<ushort>();
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
