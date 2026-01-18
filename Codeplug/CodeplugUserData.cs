using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    public class CodeplugUserData
    {

        private string _alias;
        private uint _dmrid;

        [XmlAttribute("Alias")]
        public string Alias
        {
            get => _alias;
            set => _alias = ValidateLength(value);
        }

        [XmlAttribute("DMRID")]
        public uint DMRID
        {
            get => _dmrid;
            set => _dmrid = value;
        }

        public CodeplugUserData()
        {
            _dmrid = 12345678;
            _alias = "Нет алиаса";
        }

        public CodeplugUserData(uint dmrid, string alias)
        {
            _dmrid = dmrid;
            _alias = ValidateLength(alias);
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
