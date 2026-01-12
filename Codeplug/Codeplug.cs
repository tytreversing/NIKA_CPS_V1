using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIKA_CPS_V1.Codeplug
{
    public class CodeplugData
    {
        private List<Contact> _contacts;

        //константы
        private const int MAX_CONTACTS_COUNT = 256; //максимальное число контактов
        public List<Contact> Contacts
        {
            get => _contacts;
            set => _contacts = value;
        }
        public CodeplugData()
        {
            _contacts = new List<Contact>();
            AddContact(new Contact()); 
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
            HashSet<ushort> uniqueNumbers = new HashSet<ushort>();
            List<Contact> uniqueContacts = new List<Contact>();

            foreach (var contact in _contacts)
            {
                if (contact != null && uniqueNumbers.Add(contact.Number))
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
    }
}
