using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIKA_CPS_V1.Codeplug
{
    public class CodeplugData
    {
        private List<Contact> _contacts;

        //константы
        public const int MAX_CONTACTS_COUNT = 256; //максимальное число контактов
        public List<Contact> Contacts
        {
            get => _contacts;
            set => _contacts = value;
        }
        public CodeplugData()
        {
            _contacts = new List<Contact>();
            AddContact(new Contact(GetFirstFreeNumber(), "Вызов всех", 16777215, "", Contact.ContactType.ALL_CALL, Contact.Timeslot.TS1));
            AddContact(new Contact(GetFirstFreeNumber(), "Россия", 2501, "", Contact.ContactType.GROUP, Contact.Timeslot.TS1));
        }

        public bool AddContact(Contact contact)
        {
            if (_contacts.Count <= MAX_CONTACTS_COUNT)
            {
                _contacts.Add(contact);
                return true;
            }
            else
                return false;
        }

        public void ClearContacts()
        {
            _contacts.Clear();
        }

        //удаление контактов с одинаковыми DMR ID
        public void DeleteDuplicateContacts()
        {
            HashSet<uint> uniqueIDs = new HashSet<uint>();
            List<Contact> uniqueContacts = new List<Contact>();

            foreach (var contact in _contacts)
            {
                if (contact != null && uniqueIDs.Add(contact.DMR_ID))
                {
                    // Если номер был успешно добавлен в HashSet (т.е. он уникальный),
                    // добавляем контакт в результат
                    uniqueContacts.Add(contact);
                }
            }

            _contacts = uniqueContacts;
        }

        // Сортировка по алфавиту по полю Alias
        public void SortContactsByAlias()
        {
            _contacts = _contacts
                .OrderBy(contact => contact?.Alias, StringComparer.OrdinalIgnoreCase)
                .ThenBy(contact => contact?.Alias)
                .ToList();
        }

        public void DeleteContactByDMRID(ushort dmrid)
        {
            _contacts.RemoveAll(c => c != null && c.DMR_ID == dmrid);
        }

        //поиск первого свободного номера контакта
        public ushort GetFirstFreeNumber()
        {
            if (_contacts.Count == 0)
            {
                return 0; //контактов нет
            }
            HashSet<ushort> usedNumbers = new HashSet<ushort>();
            foreach (var contact in _contacts)
            {
                if (contact != null)
                {
                    usedNumbers.Add(contact.Number);
                }
            }
            // Ищем первое свободное число, начиная с 0
            for (ushort i = 0; i < MAX_CONTACTS_COUNT; i++)
            {
                if (!usedNumbers.Contains(i))
                {
                    return i;
                }
            }
            // Если все числа от 0 до 65534 заняты, возвращаем 65535
            return ushort.MaxValue;
        }

        public void UpdateContactByID(ushort id, string alias, uint dmrid, string userdata, Contact.ContactType type, Contact.Timeslot slot)
        {
            if (_contacts == null) return;

            Contact contact = _contacts.FirstOrDefault(c => c.Number == id);

            if (contact != null)
            {
                contact.Alias = alias;
                contact.DMR_ID = dmrid;
                contact.UserData = userdata;
                contact.Type = type;    
                contact.TimeSlot = slot;
            }
        }

        public void DeleteContactByAlias(string alias)
        {
            if (_contacts == null) return;

            Contact contact = _contacts.FirstOrDefault(c => c.Alias == alias);

            if (contact != null)
            {
                _contacts.Remove(contact);
            }
            else
                MessageBox.Show("Контакт с алиасом " + alias + " не найден");
        }
    }
}
