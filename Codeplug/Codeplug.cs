using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace NIKA_CPS_V1.Codeplug
{
    [Serializable]
    [XmlRoot("Codeplug")]
    public class CodeplugData
    {
        private UserData _userData;
        private List<Contact> _contacts;
        private List<Channel> _channels;
        private List<Zone> _zones;
        private List<SatelliteKeps> _satelliteKeps;

        //константы
        public const int MAX_CONTACTS_COUNT = 256;        //максимальное число контактов
        public const int MAX_CHANNELS_COUNT = 1024;       //максимальное число каналов
        public const int MAX_ZONES_COUNT = 16;            //максимальное количество зон
        public const int MAX_CHANNELS_IN_ZONE_COUNT = 80; //максимальное количенство каналов в зоне
        public const int MAX_SATELLITES_COUNT = 20;       //максимальное число спутников
        [XmlElement("User")]
        public UserData DMRID
        {
            get => _userData;
            set => _userData = value;
        }

        [XmlArray("Contacts")]
        [XmlArrayItem("Contact")]
        public List<Contact> Contacts
        {
            get => _contacts;
            set => _contacts = value;
        }
        [XmlArray("Channels")]
        [XmlArrayItem("Channel")]
        public List<Channel> Channels
        {
            get => _channels;
            set => _channels = value;
        }
        [XmlArray("Zones")]
        [XmlArrayItem("Zone")]
        public List<Zone> Zones
        {
            get => _zones;
            set => _zones = value;
        }
        [XmlArray("Satellites")]
        [XmlArrayItem("Satellite")]
        public List<SatelliteKeps> SatelliteKeps
        {
            get => _satelliteKeps;
            set => _satelliteKeps = value;
        }


        public CodeplugData()
        {
            _userData = new UserData();
            _contacts = new List<Contact>();
            _channels = new List<Channel>();
            _zones = new List<Zone>();
            _satelliteKeps = new List<SatelliteKeps>();
        }

        // XML-сериализация
        public void Serialize(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CodeplugData));
                using (TextWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сериализации кодплага: {ex.Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // XML-десериализация
        public CodeplugData Deserialize(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Файл не найден: {filePath}");
                    return new CodeplugData();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(CodeplugData));
                using (TextReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    return (CodeplugData)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка десериализации кодплага: {ex.Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new CodeplugData();
            }
        }

        public bool AddContact(Contact contact)
        {
            if (_contacts.Count < MAX_CONTACTS_COUNT)
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
        public ushort GetFirstContactFreeNumber()
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

        public bool AddChannel(Channel channel) 
        {
            if (_channels.Count < MAX_CHANNELS_COUNT)
            {
                _channels.Add(channel);
                return true;
            }
            else
                return false;
        }

        public void DeleteChannel(ushort n)
        {
            _channels.RemoveAll(c => c != null && c.Number == n);

            foreach (Zone zone in _zones) //удаляем вхождения этого канала в зоны
            {
                zone.Channels.RemoveAll(Channel => Channel == n);
            }
        }
        // Сортировка по алфавиту по полю Nmae
        public void SortChannelsByName()
        {
            _channels = _channels
                .OrderBy(channel => channel?.Name, StringComparer.OrdinalIgnoreCase)
                .ThenBy(channel => channel?.Name)
                .ToList();
        }

        //поиск первого свободного номера канала
        public ushort GetFirstChannelFreeNumber()
        {
            if (_channels.Count == 0)
            {
                return 0; //контактов нет
            }
            HashSet<ushort> usedNumbers = new HashSet<ushort>();
            foreach (var channel in _channels)
            {
                if (channel != null)
                {
                    usedNumbers.Add(channel.Number);
                }
            }
            // Ищем первое свободное число, начиная с 0
            for (ushort i = 0; i < MAX_CHANNELS_COUNT; i++)
            {
                if (!usedNumbers.Contains(i))
                {
                    return i;
                }
            }
            // Если все числа от 0 до 65534 заняты, возвращаем 65535
            return ushort.MaxValue;
        }

        public void ClearChannels()
        {
            _channels.Clear();
        }

        public bool AddZone(Zone zone)
        {
            if (_zones.Count < MAX_ZONES_COUNT)
            {
                _zones.Add(zone);
                return true;
            }
            else
                return false;
        }

        //поиск первого свободного номера зоны
        public byte GetFirstZoneFreeNumber()
        {
            if (_zones.Count == 0)
            {
                return 0; //зон нет
            }
            HashSet<byte> usedNumbers = new HashSet<byte>();
            foreach (var zone in _zones)
            {
                if (zone != null)
                {
                    usedNumbers.Add(zone.Number);
                }
            }
            // Ищем первое свободное число, начиная с 0
            for (byte i = 0; i < MAX_ZONES_COUNT; i++)
            {
                if (!usedNumbers.Contains(i))
                {
                    return i;
                }
            }
            // Если все числа заняты, возвращаем 255
            return byte.MaxValue;
        }

        public void UpdateZoneByNumber(byte number, string name, List<ushort> channels)
        {
            if (_zones == null) return;

            Zone zone = _zones.FirstOrDefault(c => c.Number == number);

            if (zone != null)
            {
                zone.Number = number;
                zone.Name = name;
                zone.Channels = channels;
            }
        }

        public void ClearZones()
        {
            _zones.Clear(); 
        }

        public bool AddSatellite(SatelliteKeps satellite)
        {
            if (_satelliteKeps.Count <= MAX_SATELLITES_COUNT)
            {
                _satelliteKeps.Add(satellite);
                return true;
            }
            else
                return false;
        }

        public void DeleteSatelliteByCatID(string catid)
        {
            if (_satelliteKeps == null) return;

            SatelliteKeps sat = _satelliteKeps.FirstOrDefault(c => c.CatalogueNumber == catid);

            if (sat != null)
            {
                _satelliteKeps.Remove(sat);
            }
            else
                MessageBox.Show("Данные о спутнике с каталожным номером " + catid + " не найдены");
        }

        public void ClearSatellites()
        {
            _satelliteKeps.Clear();
        }
    }
}
