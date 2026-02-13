using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.LinkLabel;

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

        //конструктор из строки в CSV

        public CodeplugContact(CodeplugData codeplug, string data, char delimiter)
        {
            try
            {
                string[] values = data.Split(delimiter);

                for (int j = 0; j < values.Length; j++)
                {
                    values[j] = values[j].Trim().Trim('"');
                }

                if (values.Length == 6) //импорт из экспортированного в том же формате
                {
                    Number = ushort.Parse(values[0]);
                    if (codeplug.IsNumberUsedInContacts(Number))
                        Number = codeplug.GetFirstContactFreeNumber();
                    Alias = values[1];
                    DMR_ID = uint.Parse(values[2]);
                    UserData = values[3];
                    Type = values[4].ToUpper() switch
                    {
                        "PRIVATE" => CodeplugContact.ContactType.PRIVATE,
                        "GROUP" => CodeplugContact.ContactType.GROUP,
                        "ALLCALL" or "ALL_CALL" => CodeplugContact.ContactType.ALL_CALL,
                        _ => CodeplugContact.ContactType.PRIVATE
                    };


                    TimeSlot = values[5].ToUpper() switch
                    {
                        "NONE" => CodeplugContact.Timeslot.NONE,
                        "TS1" => CodeplugContact.Timeslot.TS1,
                        "TS2" => CodeplugContact.Timeslot.TS2,
                        _ => CodeplugContact.Timeslot.NONE
                    };
                }
                else if (values.Length == 4) //опенгдшный CSV
                {
                    Number = codeplug.GetFirstContactFreeNumber();
                    Alias = values[0];
                    DMR_ID = uint.Parse(values[1]);
                    UserData = "";
                    Type = values[2].ToUpper() switch
                    {
                        "PRIVATE" => CodeplugContact.ContactType.PRIVATE,
                        "GROUP" => CodeplugContact.ContactType.GROUP,
                        "ALLCALL" or "ALL_CALL" => CodeplugContact.ContactType.ALL_CALL,
                        _ => CodeplugContact.ContactType.PRIVATE
                    };


                    TimeSlot = values[3].ToUpper() switch
                    {
                        "DISABLED" => CodeplugContact.Timeslot.NONE,
                        "1" => CodeplugContact.Timeslot.TS1,
                        "2" => CodeplugContact.Timeslot.TS2,
                        _ => CodeplugContact.Timeslot.NONE
                    };
                }
            }
            catch 
            {
            }
        }

        public override string ToString()
        {
            return _alias ?? string.Empty;
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
