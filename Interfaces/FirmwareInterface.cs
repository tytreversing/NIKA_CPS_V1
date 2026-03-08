using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIKA_CPS_V1.Interfaces
{
    internal class FirmwareInterface
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RadioInfo
        {

            [MarshalAs(UnmanagedType.U1)]
            public byte radioType;

            [MarshalAs(UnmanagedType.U1)]
            public byte radioHardware;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string identifier;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string buildDateTime;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string serial;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string fwDate;
        }
        public static bool sendCommand(SerialPort port, int commandNumber, int x_or_command_option_number = 0, int y = 0, int iSize = 0, int alignment = 0, int isInverted = 0, string message = "")
        {
            byte[] array = new byte[64];
            array[0] = 67;
            array[1] = (byte)commandNumber;
            switch (commandNumber)
            {
                case 2:
                    array[3] = (byte)y;
                    array[4] = (byte)iSize;
                    array[5] = (byte)alignment;
                    array[6] = (byte)isInverted;
                    Encoding inEncoding = Encoding.Unicode;
                    Encoding outEncoding = Encoding.GetEncoding(1251);
                    byte[] sourceBuffer = inEncoding.GetBytes(message);
                    byte[] destBuffer = Encoding.Convert(inEncoding, outEncoding, sourceBuffer);
                    Buffer.BlockCopy(destBuffer, 0, array, 7, Math.Min(message.Length, 16));
                    break;
                case 6:
                    array[2] = (byte)x_or_command_option_number;
                    break;
            }
            port.Write(array, 0, 32);
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(0);
            }
            port.Read(array, 0, 64);
            return array[1] == commandNumber;
        }

        private static bool _readRadioInfo(SerialPort port, DataTransfer dataObj)
        {
            byte[] array = new byte[1032];
            byte[] array2 = new byte[1032];
            int num = 0;
            array[0] = 82;
            array[1] = (byte)dataObj.mode;
            array[2] = 0;
            array[3] = 0;
            array[4] = 0;
            array[5] = 0;
            array[6] = 0;
            array[7] = 0;
            port.Write(array, 0, 8);
            while (port.BytesToWrite > 0)
            {
                Thread.Sleep(1);
            }
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(5);
            }
            port.Read(array2, 0, port.BytesToRead);
            if (array2[0] == 82)
            {
                int num2 = (array2[1] << 8) + array2[2];
                for (int i = 0; i < num2; i++)
                {
                    dataObj.dataBuffer[num++] = array2[i + 3];
                }
                return true;
            }
            return false;
        }

        private static RadioInfo ByteArrayToRadioInfo(byte[] bytes)
        {
            GCHandle gCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (RadioInfo)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(RadioInfo));
            }
            finally
            {
                gCHandle.Free();
            }
        }

        public static RadioInfo ReadRadioInfo(SerialPort port)
        {
            DataTransfer COMData = new DataTransfer();
            COMData.mode = DataTransfer.DataMode.DataModeReadRadioInfo;
            COMData.localAddress = 0;
            COMData.transferLength = 0;
            COMData.dataBuffer = new byte[128];
            RadioInfo info = default(RadioInfo);
            if (_readRadioInfo(port, COMData))
            {
                info = ByteArrayToRadioInfo(COMData.dataBuffer);
            }
            return info;
        }
    }
}
