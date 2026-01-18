using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    public class CodeplugContact
    {

        public enum ContactType
        {
            [XmlEnum("PRIVATE")]
            PRIVATE,
            [XmlEnum("GROUP")]
            GROUP,
            [XmlEnum("ALLCALL")]
            ALL_CALL
        }

        public enum Timeslot
        {
            [XmlEnum("NONE")]
            NONE = 0,
            [XmlEnum("TS1")]
            TS1,
            [XmlEnum("TS2")]
            TS2
        }

        private ushort _number;
        private string _alias;
        private uint _dmrId;
        private string _userData;
        private ContactType _contactType;
        private Timeslot _slot;

        [XmlAttribute("Number")]
        public ushort Number
        {
            get => _number;
            set => _number = value;
        }
        [XmlAttribute("Alias")]
        public string Alias
        {
            get => _alias;
            set => _alias = ValidateLength(value, 16);
        }
        [XmlAttribute("DMRID")]
        public uint DMR_ID
        {
            get => _dmrId;
            set => _dmrId = value;
        }
        [XmlAttribute("UserData")]
        public string UserData
        {
            get => _userData;
            set => _userData = ValidateLength(value, 32);
        }
        [XmlAttribute("Type")]
        public ContactType Type
        {
            get => _contactType;    
            set => _contactType = value;
        }
        [XmlAttribute("Timeslot")]
        public Timeslot TimeSlot
        {
            get => _slot;
            set => _slot = value;
        }

        public CodeplugContact()
        {
            _number = 0;
            _alias = "Вызов всех";
            _dmrId = 16777215;
            _userData = "";
            _contactType = ContactType.ALL_CALL;
            _slot = Timeslot.NONE;
        }


        public CodeplugContact(ushort number, string alias, uint dmrId, string userData, ContactType type, Timeslot slot)
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
