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
            AddContact(new Contact(GetFirstFreeNumber(), "Вызов всех", 16777215, ""));
            AddContact(new Contact(GetFirstFreeNumber(), "Россия", 2501, ""));
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

        public void DeleteByDMRID(ushort dmrid)
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
    }
}
