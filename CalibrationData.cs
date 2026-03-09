using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace NIKA_CPS_V1
{
    [Serializable]
    [XmlRoot("NIKA_V1_Calibrations")]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x150)]
    public class CalibrationData
    {
        private const int VHF_ARRAY_SIZE = 5;
        private const int UHF_ARRAY_SIZE = 9;
        private const int POWERS_TOTAL = 9;
        // Полином STM32F4 (0x04C11DB7)
        private const uint STM32F4_POLYNOME = 0x04C11DB7;
        [XmlElement("Checksum")]
        [MarshalAs(UnmanagedType.U4)]
        public uint checksum;
        [XmlElement("OscRefTuneVHF")]
        [MarshalAs(UnmanagedType.U1)]
        public byte OscRefTuneVHF;
        [XmlElement("OscRefTuneUHF")]
        [MarshalAs(UnmanagedType.U1)]
        public byte OscRefTuneUHF;
        [XmlArray("RxTuneVHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = VHF_ARRAY_SIZE)]
        public byte[] RxTuneVHF;
        [XmlArray("RxTuneUHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UHF_ARRAY_SIZE)]
        public byte[] RxTuneUHF;
        [XmlArray("PowersVHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = POWERS_TOTAL * VHF_ARRAY_SIZE)]
        public ushort[] powersVHF;
        [XmlArray("PowersUHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = POWERS_TOTAL * UHF_ARRAY_SIZE)]
        public ushort[] powersUHF;
        [XmlArray("IGainDMRVHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = VHF_ARRAY_SIZE)]
        public byte[] IGainDMRVHF;
        [XmlArray("IGainDMRUHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UHF_ARRAY_SIZE)]
        public byte[] IGainDMRUHF;
        [XmlArray("QGainDMRVHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = VHF_ARRAY_SIZE)]
        public byte[] QGainDMRVHF;
        [XmlArray("QGainDMRUHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UHF_ARRAY_SIZE)]
        public byte[] QGainDMRUHF;
        [XmlArray("IGainVHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = VHF_ARRAY_SIZE)]
        public byte[] IGainVHF;
        [XmlArray("IGainUHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UHF_ARRAY_SIZE)]
        public byte[] IGainUHF;
        [XmlArray("QGainVHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = VHF_ARRAY_SIZE)]
        public byte[] QGainVHF;
        [XmlArray("QGainUHF")]
        [XmlArrayItem("Value")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = UHF_ARRAY_SIZE)]
        public byte[] QGainUHF;
        [XmlElement("RSSI120")]
        [MarshalAs(UnmanagedType.U2)]
        public ushort RSSI120;
        [XmlElement("RSSI70")]
        [MarshalAs(UnmanagedType.U2)]
        public ushort RSSI70;
        [XmlElement("DevFMVHF")]
        [MarshalAs(UnmanagedType.U1)]
        public byte DevFMVHF;
        [XmlElement("DevFMNVHF")]
        [MarshalAs(UnmanagedType.U1)]
        public byte DevFMNVHF;
        [XmlElement("DevFMUHF")]
        [MarshalAs(UnmanagedType.U1)]
        public byte DevFMUHF;
        [XmlElement("DevFMNUHF")]
        [MarshalAs(UnmanagedType.U1)]
        public byte DevFMNUHF;

        private void ValidateIndices(int powerIndex, int tuneIndex, int maxPower, int maxTune, string band)
        {
            if (powerIndex < 0 || powerIndex >= maxPower)
                throw new ArgumentOutOfRangeException(nameof(powerIndex),
                    $"Power индекс для {band} должен быть от 0 до {maxPower - 1}");

            if (tuneIndex < 0 || tuneIndex >= maxTune)
                throw new ArgumentOutOfRangeException(nameof(tuneIndex),
                    $"Tune индекс для {band} должен быть от 0 до {maxTune - 1}");
        }

        /// <summary>
        /// Получить значение из powersVHF по индексам [powerIndex, tuneIndex]
        /// </summary>
        public ushort GetPowerVHF(int powerIndex, int tuneIndex)
        {
            ValidateIndices(powerIndex, tuneIndex, POWERS_TOTAL, VHF_ARRAY_SIZE, "VHF");
            return powersVHF[powerIndex * VHF_ARRAY_SIZE + tuneIndex];
        }

        /// <summary>
        /// Установить значение в powersVHF по индексам [powerIndex, tuneIndex]
        /// </summary>
        public void SetPowerVHF(int powerIndex, int tuneIndex, ushort value)
        {
            ValidateIndices(powerIndex, tuneIndex, POWERS_TOTAL, VHF_ARRAY_SIZE, "VHF");
            powersVHF[powerIndex * VHF_ARRAY_SIZE + tuneIndex] = value;
        }

        /// <summary>
        /// Получить значение из powersUHF по индексам [powerIndex, tuneIndex]
        /// </summary>
        public ushort GetPowerUHF(int powerIndex, int tuneIndex)
        {
            ValidateIndices(powerIndex, tuneIndex, POWERS_TOTAL, UHF_ARRAY_SIZE, "UHF");
            return powersUHF[powerIndex * UHF_ARRAY_SIZE + tuneIndex];
        }

        /// <summary>
        /// Установить значение в powersUHF по индексам [powerIndex, tuneIndex]
        /// </summary>
        public void SetPowerUHF(int powerIndex, int tuneIndex, ushort value)
        {
            ValidateIndices(powerIndex, tuneIndex, POWERS_TOTAL, UHF_ARRAY_SIZE, "UHF");
            powersUHF[powerIndex * UHF_ARRAY_SIZE + tuneIndex] = value;
        }


        public CalibrationData()
        {
            checksum = 0xDEFECA7E; //указание прошивке регенерировать чексумму
            OscRefTuneVHF = 0;
            OscRefTuneUHF = 0;

            RxTuneVHF = new byte[VHF_ARRAY_SIZE];
            RxTuneUHF = new byte[UHF_ARRAY_SIZE];

            powersVHF = new ushort[POWERS_TOTAL * VHF_ARRAY_SIZE];
            powersUHF = new ushort[POWERS_TOTAL * UHF_ARRAY_SIZE];

            IGainDMRVHF = new byte[VHF_ARRAY_SIZE];
            IGainDMRUHF = new byte[UHF_ARRAY_SIZE];

            QGainDMRVHF = new byte[VHF_ARRAY_SIZE];
            QGainDMRUHF = new byte[UHF_ARRAY_SIZE];

            IGainVHF = new byte[VHF_ARRAY_SIZE];
            IGainUHF = new byte[UHF_ARRAY_SIZE];

            QGainVHF = new byte[VHF_ARRAY_SIZE];
            QGainUHF = new byte[UHF_ARRAY_SIZE];

            RSSI120 = 0;
            RSSI70 = 0;

            DevFMVHF = 0;
            DevFMNVHF = 0;
            DevFMUHF = 0;
            DevFMNUHF = 0;
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 24)]
    public class RadioBandlimits
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.U4)]
        public uint VHFLowCal;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U4)]
        public uint VHFLow;

        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.U4)]
        public uint VHFHigh;

        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.U4)]
        public uint UHFLowCal;

        [FieldOffset(16)]
        [MarshalAs(UnmanagedType.U4)]
        public uint UHFLow;

        [FieldOffset(20)]
        [MarshalAs(UnmanagedType.U4)]
        public uint UHFHigh;

        public RadioBandlimits()
        {
            VHFLowCal = 13600000;
            VHFLow = 12700000;
            VHFHigh = 17400000;
            UHFLowCal = 40000000;
            UHFLow = 38000000;
            UHFHigh = 48000000;
        }
    }



}