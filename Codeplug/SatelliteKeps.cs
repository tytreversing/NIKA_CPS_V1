using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIKA_CPS_V1.Codeplug
{
    public class SatelliteKeps
    {
        private static readonly NumberFormatInfo NumberFormat = CultureInfo.GetCultureInfo("en-US").NumberFormat;


        private string _catalogueNumber;
        private string _displayName;
        private uint _rx1;
        private uint _tx1;
        private ushort _txCTCSS1;
        private ushort _rxCTCSS1;
        private uint _rx2;
        private uint _tx2;
        private uint _rx3;
        private uint _tx3;
        private string _callsign;

        public string CatalogueNumber
        {
            get => _catalogueNumber;
            set => _catalogueNumber = value;
        }

        public string DisplayName
        {
            get => _displayName;
            set => _displayName = value;
        }

        public uint Rx1
        {
            get => _rx1;
            set => _rx1 = value;
        }

        public uint Tx1
        {
            get => _tx1;
            set => _tx1 = value;
        }

        public ushort TxCTCSS1
        {
            get => _txCTCSS1;
            set => _txCTCSS1 = value;
        }

        public ushort RxCTCSS1
        {
            get => _rxCTCSS1;
            set => _rxCTCSS1 = value;
        }

        public uint Rx2
        {
            get => _rx2;
            set => _rx2 = value;
        }

        public uint Tx2
        {
            get => _tx2;
            set => _tx2 = value;
        }

        public uint Rx3
        {
            get => _rx3;
            set => _rx3 = value;
        }

        public uint Tx3
        {
            get => _tx3;
            set => _tx3 = value;
        }

        public string Callsign
        {
            get => _callsign;
            set => _callsign = value;
        }

        public SatelliteKeps(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("Ошибка в формате файла данных спутников - пустая строка!");

            string[] parts = data.Split(',');

            if (parts.Length != 11)
                throw new ArgumentException("Ошибка в формате файла данных спутников!");

            try
            {
                _catalogueNumber = parts[0].Trim();
                _displayName = parts[1].Trim();
                _rx1 = ParseFrequency(parts[2]);
                _tx1 = ParseFrequency(parts[3]);
                _txCTCSS1 = ParseCTCSS(parts[4]);
                _rxCTCSS1 = ParseCTCSS(parts[5]);
                _rx2 = ParseFrequency(parts[6]);
                _tx2 = ParseFrequency(parts[7]);
                _rx3 = ParseFrequency(parts[8]);
                _tx3 = ParseFrequency(parts[9]);
                _callsign = parts[10].Trim();
            }
            catch (FormatException)
            {
                throw new FormatException("Ошибка парсинга данных!");
            }
        }

        public SatelliteKeps()
        {
            _catalogueNumber = "25544U";
            _displayName = "ISS";
            _rx1 = 437800000;
            _tx1 = 145990000;
            _txCTCSS1 = 670;
            _rxCTCSS1 = 0;
            _rx2 = 145825000;
            _tx2 = 145825000;
            _rx3 = 145800000;
            _tx3 = 0;
            _callsign = "RS0ISS";
        }

        private static uint ParseFrequency(string value)
        {
            float frequency = float.Parse(value.Trim(), NumberFormat);
            return (uint)(frequency * 1000000f);
        }

        private static ushort ParseCTCSS(string value)
        {
            float ctcss = float.Parse(value.Trim(), NumberFormat);
            return (ushort)(ctcss * 10f);
        }

        public static ushort ReverseBytes(ushort value)
        {
            return (ushort)((value << 8) | (value >> 8));
        }

        public static uint ReverseBytes(uint value)
        {
            value = ((value << 8) & 0xFF00FF00) | ((value >> 8) & 0x00FF00FF);
            return (value << 16) | (value >> 16);
        }
    }
}
