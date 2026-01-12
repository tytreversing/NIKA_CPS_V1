using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIKA_CPS_V1.Codeplug
{
    internal class Contact
    {

        private ushort _number;
        private string _alias;
        private uint _dmrId;
        private string _userData;



        public ushort Number
        {
            get => _number;
            set => _number = value;
        }
        public string Alias
        {
            get => _alias;
            set => _alias = ValidateLength(value, 16);
        }

        public uint DMR_ID
        {
            get => _dmrId;
            set => _dmrId = value;
        }

        public string UserData
        {
            get => _userData;
            set => _userData = ValidateLength(value, 32);
        }


        public Contact()
        {
            _alias = string.Empty;
            _dmrId = 0;
            _userData = string.Empty;
        }


        public Contact(ushort number, string alias, uint dmrId, string userData)
        {
            Number = number;
            Alias = alias;
            DMR_ID = dmrId;
            UserData = userData;
        }


        private string ValidateLength(string value, int maxLength)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value.Length > maxLength)
            {
                return value.Substring(0, maxLength);
            }

            return value;
        }
    }
}
