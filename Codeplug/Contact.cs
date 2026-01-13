using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIKA_CPS_V1.Codeplug
{
    public class Contact
    {

        public enum ContactType
        {
            PRIVATE,
            GROUP,
            ALL_CALL
        }

        public enum Timeslot
        {
            TS1,
            TS2
        }

        private ushort _number;
        private string _alias;
        private uint _dmrId;
        private string _userData;
        private ContactType _contactType;
        private Timeslot _slot;


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

        public ContactType Type
        {
            get => _contactType;    
            set => _contactType = value;
        }

        public Timeslot TimeSlot
        {
            get => _slot;
            set => _slot = value;
        }

        public Contact()
        {
            _number = 0;
            _alias = "Вызов всех";
            _dmrId = 16777215;
            _userData = "";
            _contactType = ContactType.ALL_CALL;
            _slot = Timeslot.TS1;
        }


        public Contact(ushort number, string alias, uint dmrId, string userData, ContactType type, Timeslot slot)
        {
            Number = number;
            Alias = alias;
            DMR_ID = dmrId;
            UserData = userData;
            Type = type;    
            TimeSlot = slot;
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
