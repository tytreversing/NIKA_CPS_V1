using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace NIKA_CPS_V1
{
    [Serializable]
    [XmlRoot("NIKA_V1_Calibrations")]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14C)]
    public class CalibrationData
    {
        private const int VHF_ARRAY_SIZE = 5;
        private const int UHF_ARRAY_SIZE = 9;
        private const int POWERS_TOTAL = 9;
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

        [XmlIgnore]
        public ushort[][] PowersVHFAs2D
        {
            get
            {
                if (powersVHF == null) return null;
                var result = new ushort[POWERS_TOTAL][];
                for (int i = 0; i < POWERS_TOTAL; i++)
                {
                    result[i] = new ushort[VHF_ARRAY_SIZE];
                    Array.Copy(powersVHF, i * VHF_ARRAY_SIZE, result[i], 0, VHF_ARRAY_SIZE);
                }
                return result;
            }
            set
            {
                if (value == null)
                {
                    powersVHF = null;
                    return;
                }
                powersVHF = new ushort[POWERS_TOTAL * VHF_ARRAY_SIZE];
                for (int i = 0; i < POWERS_TOTAL; i++)
                {
                    if (value[i] != null && value[i].Length == VHF_ARRAY_SIZE)
                    {
                        Array.Copy(value[i], 0, powersVHF, i * VHF_ARRAY_SIZE, VHF_ARRAY_SIZE);
                    }
                }
            }
        }
        [XmlIgnore]
        public ushort[][] PowersUHFAs2D
        {
            get
            {
                if (powersUHF == null) return null;
                var result = new ushort[POWERS_TOTAL][];
                for (int i = 0; i < POWERS_TOTAL; i++)
                {
                    result[i] = new ushort[UHF_ARRAY_SIZE];
                    Array.Copy(powersUHF, i * UHF_ARRAY_SIZE, result[i], 0, UHF_ARRAY_SIZE);
                }
                return result;
            }
            set
            {
                if (value == null)
                {
                    powersUHF = null;
                    return;
                }
                powersUHF = new ushort[POWERS_TOTAL * UHF_ARRAY_SIZE];
                for (int i = 0; i < POWERS_TOTAL; i++)
                {
                    if (value[i] != null && value[i].Length == UHF_ARRAY_SIZE)
                    {
                        Array.Copy(value[i], 0, powersUHF, i * UHF_ARRAY_SIZE, UHF_ARRAY_SIZE);
                    }
                }
            }
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