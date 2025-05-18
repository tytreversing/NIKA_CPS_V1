using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace NIKA_CPS_V1
{

    internal class STM_DFU_FwUpdate
    {
        public delegate void FirmwareMessageEventHandler(object sender, FirmwareUpdateMessageEventArgs e);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct DFU_Status
        {
            public byte bStatus;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] bwPollTimeout;

            public byte bState;

            public byte iString;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct DeviceInterfaceData
        {
            public int Size;

            public Guid InterfaceClassGuid;

            public int Flags;

            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct DeviceInterfaceDetailData
        {
            public int Size;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        private const int DIGCF_PRESENT = 2;

        private const int DIGCF_DEVICEINTERFACE = 16;

        private const uint STDFU_ERROR_OFFSET = 305397760u;

        private const uint STDFU_NOERROR = 305397760u;

        private const byte STATE_DFU_IDLE = 2;

        private byte[] firmwareBuf;

        private const ushort BLOCK_WRITE_SIZE = 1024;

        private IWin32Window parentWindow;

        private static byte[] MD9600_ENCODE_CIPHER = new byte[1024]
        {
        162, 250, 187, 75, 144, 143, 23, 32, 150, 54,
        67, 132, 247, 172, 78, 85, 234, 229, 180, 54,
        85, 185, 57, 226, 216, 218, 24, 192, 13, 9,
        93, 184, 14, 137, 144, 70, 56, 212, 147, 204,
        47, 142, 205, 45, 34, 183, 137, 151, 81, 36,
        152, 160, 204, 48, 62, 149, 125, 175, 76, 14,
        104, 35, 137, 198, 50, 51, 86, 170, 224, 88,
        146, 48, 226, 218, 188, 234, 80, 251, 87, 91,
        115, 113, 147, 9, 135, 26, 41, 211, 191, 236,
        135, 133, 138, 43, 45, 170, 21, 222, 87, 162,
        17, 131, 220, 244, 182, 2, 86, 229, 8, 224,
        131, 73, 89, 181, 235, 153, 15, 224, 195, 70,
        167, 121, 18, 77, 250, 135, 18, 12, 191, 115,
        217, 83, 82, 189, 56, 191, 180, 238, 228, 67,
        210, 206, 211, 8, 10, 214, 233, 119, 235, 232,
        212, 148, 60, 62, 53, 141, 64, 161, 0, 146,
        57, 219, 37, 232, 43, 110, 112, 57, 226, 134,
        173, 47, 54, 45, 17, 65, 142, 190, 213, 204,
        163, 156, 36, 101, 135, 35, 55, 110, 229, 223,
        191, 231, 138, 252, 131, 135, 36, 254, 74, 11,
        74, 179, 251, 207, 189, 101, 3, 155, 238, 83,
        247, 191, 192, 99, 122, 98, 142, 17, 98, 23,
        112, 171, 22, 177, 186, 192, 58, 89, 198, 214,
        143, 221, 244, 91, 20, 75, 238, 222, 114, 191,
        49, 127, 150, 121, 201, 164, 160, 50, 91, 238,
        252, 176, 105, 108, 206, 153, 210, 14, 148, 133,
        152, 92, 7, 86, 230, 103, 65, 204, 82, 0,
        37, 84, 95, 41, 252, 33, 70, 201, 92, 126,
        246, 164, 78, 99, 89, 137, 175, 70, 217, 205,
        215, 51, 35, 249, 121, 31, 42, 192, 202, 122,
        111, 52, 230, 3, 129, 57, 111, 224, 191, 57,
        119, 238, 101, 25, 160, 86, 199, 108, 129, 97,
        215, 231, 76, 141, 237, 21, 174, 224, 200, 76,
        247, 124, 208, 224, 123, 116, 157, 150, 56, 222,
        189, 92, 185, 41, 178, 55, 58, 177, 59, 124,
        12, 145, 213, 67, 59, 184, 128, 25, 111, 64,
        198, 245, 16, 251, 250, 110, 173, 78, 190, 42,
        159, 66, 199, 154, 233, 216, 229, 228, 99, 157,
        61, 33, 24, 127, 217, 201, 236, 223, 100, 107,
        130, 231, 46, 162, 92, 30, 119, 68, 68, 57,
        233, 220, 235, 53, 102, 91, 209, 162, 4, 10,
        100, 66, 86, 195, 108, 210, 238, 97, 166, 40,
        31, 117, 175, 126, 8, 59, 36, 14, 205, 204,
        8, 223, 40, 148, 102, 222, 33, 7, 55, 48,
        25, 144, 133, 199, 13, 202, 209, 51, 25, 243,
        179, 187, 59, 158, 192, 173, 90, 167, 176, 242,
        135, 108, 193, 229, 130, 58, 86, 102, 128, 6,
        228, 41, 43, 94, 14, 84, 235, 159, 15, 74,
        100, 103, 89, 193, 64, 77, 123, 27, 46, 208,
        72, 243, 42, 142, 54, 246, 0, 183, 4, 244,
        11, 192, 160, 54, 67, 92, 71, 19, 119, 168,
        238, 190, 214, 165, 225, 98, 180, 236, 170, 113,
        139, 157, 52, 57, 64, 153, 48, 184, 168, 241,
        184, 177, 75, 158, 50, 255, 104, 114, 120, 42,
        57, 78, 54, 56, 119, 150, 147, 197, 33, 226,
        19, 86, 122, 246, 187, 235, 81, 245, 119, 211,
        132, 209, 186, 196, 199, 6, 100, 43, 162, 136,
        232, 193, 185, 249, 174, 95, 80, 32, 182, 19,
        14, 151, 127, 115, 1, 195, 39, 49, 227, 9,
        211, 240, 156, 63, 81, 86, 7, 97, 252, 99,
        249, 134, 224, 1, 128, 18, 31, 220, 104, 44,
        148, 115, 4, 115, 181, 112, 43, 236, 190, 52,
        128, 63, 12, 183, 246, 36, 198, 143, 148, 24,
        195, 78, 118, 84, 168, 17, 21, 255, 81, 86,
        200, 163, 115, 14, 138, 222, 127, 244, 253, 90,
        201, 28, 175, 254, 233, 207, 156, 102, 97, 150,
        245, 145, 129, 149, 32, 218, 136, 26, 0, 42,
        12, 118, 118, 107, 156, 12, 40, 64, 163, 167,
        129, 243, 143, 17, 249, 175, 51, 225, 150, 239,
        106, 148, 178, 54, 254, 223, 0, 1, 200, 68,
        202, 249, 24, 228, 124, 110, 87, 148, 102, 1,
        234, 50, 190, 160, 90, 58, 228, 184, 178, 148,
        234, 165, 41, 176, 84, 110, 1, 213, 28, 175,
        175, 182, 250, 214, 60, 71, 226, 146, 235, 206,
        205, 137, 28, 61, 188, 74, 112, 191, 250, 130,
        46, 145, 162, 114, 230, 19, 98, 160, 84, 31,
        126, 205, 134, 153, 24, 40, 65, 71, 174, 193,
        162, 227, 228, 64, 1, 111, 132, 215, 26, 201,
        195, 117, 111, 127, 198, 61, 232, 228, 100, 54,
        189, 100, 46, 68, 149, 20, 172, 87, 240, 141,
        234, 226, 194, 251, 51, 143, 96, 113, 29, 49,
        160, 128, 198, 249, 60, 7, 92, 238, 120, 76,
        227, 151, 5, 76, 50, 250, 36, 80, 63, 203,
        15, 193, 157, 221, 148, 61, 67, 220, 3, 234,
        143, 62, 74, 11, 139, 119, 95, 209, 110, 108,
        222, 115, 102, 43, 244, 129, 148, 217, 123, 117,
        88, 235, 102, 139, 208, 154, 96, 210, 155, 144,
        176, 131, 227, 232, 96, 146, 154, 85, 158, 132,
        3, 161, 98, 128, 117, 90, 81, 168, 92, 200,
        226, 170, 128, 33, 191, 145, 138, 0, 110, 226,
        196, 20, 48, 228, 32, 21, 41, 63, 124, 253,
        194, 200, 36, 116, 76, 156, 152, 140, 230, 108,
        144, 174, 160, 23, 62, 213, 224, 126, 211, 249,
        5, 148, 68, 207, 75, 180, 78, 175, 238, 56,
        184, 213, 147, 71, 216, 205, 227, 238, 88, 41,
        121, 114, 58, 117, 254, 229, 26, 109, 146, 248,
        179, 109, 110, 16, 165, 40, 200, 156, 118, 157,
        247, 165, 214, 71, 216, 166, 39, 148, 112, 159,
        60, 153, 211, 101, 97, 4, 68, 60, 156, 82,
        157, 167, 51, 66, 242, 127, 110, 137, 113, 67,
        158, 199, 140, 175, 94, 186, 91, 144, 25, 177,
        59, 214, 205, 68, 188, 235, 14, 67, 186, 67,
        77, 236, 201, 53
        };

        private static byte[] MDUV380_ENCODE_CIPHER = new byte[1024]
        {
        0, 170, 137, 137, 31, 75, 236, 207, 66, 69,
        20, 84, 0, 101, 235, 102, 65, 125, 76, 136,
        73, 90, 33, 13, 242, 245, 200, 230, 56, 237,
        188, 185, 251, 53, 113, 51, 1, 10, 127, 158,
        59, 41, 3, 182, 73, 62, 66, 184, 63, 159,
        144, 189, 170, 58, 113, 70, 206, 205, 253, 24,
        50, 85, 137, 74, 95, 200, 131, 156, 228, 6,
        158, 10, 157, 13, 47, 161, 53, 109, 215, 146,
        234, 253, 99, 133, 144, 203, 240, 47, 217, 89,
        83, 39, 211, 6, 184, 245, 178, 202, 136, 108,
        208, 38, 145, 59, 242, 91, 97, 190, 205, 219,
        242, 26, 201, 253, 141, 136, 4, 244, 232, 241,
        154, 2, 146, 188, 36, 233, 144, 228, 126, 163,
        73, 77, 206, 82, 159, 88, 193, 122, 95, 181,
        24, 110, 219, 120, 100, 8, 213, 111, 13, 157,
        159, 179, 153, 48, 129, 126, 43, 230, 91, 60,
        74, 186, 139, 229, 228, 114, 17, 137, 147, 210,
        241, 45, 30, 15, 217, 213, 67, 135, 4, 226,
        180, 174, 94, 158, 95, 76, 233, 22, 242, 230,
        95, 40, 159, 121, 25, 220, 29, 111, 47, 248,
        239, 203, 225, 206, 232, 167, 54, 89, 239, 224,
        226, 136, 0, 16, 109, 218, 115, 188, 146, 43,
        129, 207, 230, 206, 4, 71, 186, 219, 126, 47,
        65, 202, 93, 205, 246, 65, 125, 28, 56, 43,
        239, 125, 55, 10, 249, 169, 20, 142, 93, 234,
        68, 102, 222, 139, 54, 86, 1, 140, 53, 138,
        17, 155, 143, 42, 101, 64, 247, 46, 230, 88,
        40, 116, 203, 196, 203, 15, 167, 98, 155, 227,
        166, 60, 199, 111, 19, 0, 151, 233, 30, 177,
        83, 144, 221, 155, 97, 62, 144, 140, 173, 60,
        41, 65, 78, 91, 12, 31, 102, 64, 19, 36,
        73, 0, 213, 28, 226, 237, 40, 24, 83, 175,
        228, 28, 220, 150, 234, 24, 254, 46, 101, 25,
        224, 20, 80, 193, 241, 9, 57, 245, 207, 69,
        68, 213, 104, 13, 114, 241, 95, 136, 35, 185,
        177, 207, 218, 54, 152, 67, 65, 248, 175, 35,
        109, 80, 87, 94, 98, 191, 90, 165, 218, 173,
        207, 197, 66, 94, 62, 52, 6, 35, 4, 233,
        14, 205, 248, 113, 137, 103, 78, 64, 233, 37,
        188, 69, 46, 151, 220, 193, 104, 34, 210, 88,
        119, 177, 46, 105, 22, 168, 20, 155, 24, 26,
        154, 184, 240, 59, 113, 191, 119, 24, 200, 52,
        234, 133, 109, 187, 50, 87, 53, 229, 105, 212,
        159, 74, 153, 104, 180, 216, 199, 154, 49, 106,
        48, 61, 232, 156, 210, 235, 100, 222, 46, 175,
        204, 200, 77, 2, 9, 174, 1, 249, 43, 115,
        109, 188, 9, 162, 199, 58, 40, 186, 93, 27,
        223, 202, 214, 246, 184, 62, 187, 197, 24, 249,
        54, 150, 35, 164, 25, 131, 218, 69, 33, 227,
        134, 19, 125, 194, 90, 137, 138, 143, 84, 185,
        225, 21, 100, 227, 147, 173, 208, 70, 179, 177,
        215, 54, 21, 51, 149, 111, 86, 239, 38, 169,
        28, 127, 14, 108, 159, 206, 216, 38, 105, 207,
        254, 123, 90, 111, 9, 220, 238, 200, 249, 91,
        195, 151, 231, 189, 85, 240, 233, 209, 12, 48,
        54, 1, 122, 52, 139, 39, 221, 200, 205, 162,
        236, 98, 239, 168, 208, 17, 22, 221, 112, 176,
        251, 37, 241, 95, 145, 183, 125, 52, 233, 116,
        68, 45, 82, 118, 193, 105, 196, 235, 63, 152,
        127, 36, 155, 177, 239, 233, 75, 227, 211, 16,
        159, 205, 158, 78, 71, 241, 29, 76, 22, 102,
        91, 253, 6, 206, 194, 48, 123, 136, 130, 97,
        204, 39, 55, 213, 255, 34, 198, 230, 212, 204,
        135, 155, 6, 135, 170, 123, 205, 53, 211, 163,
        167, 240, 8, 23, 88, 251, 205, 86, 47, 248,
        141, 49, 140, 91, 60, 220, 159, 30, 59, 70,
        114, 183, 124, 166, 42, 71, 230, 86, 138, 20,
        251, 229, 184, 57, 184, 104, 68, 156, 188, 16,
        102, 33, 173, 2, 135, 29, 216, 98, 3, 14,
        23, 177, 46, 137, 248, 90, 149, 115, 27, 135,
        134, 116, 220, 57, 210, 169, 50, 152, 209, 153,
        215, 136, 167, 107, 170, 124, 198, 86, 81, 143,
        180, 88, 34, 209, 15, 43, 68, 222, 206, 117,
        17, 182, 201, 63, 191, 200, 124, 168, 64, 80,
        7, 218, 102, 227, 122, 62, 75, 72, 80, 236,
        240, 138, 57, 102, 36, 75, 29, 133, 168, 91,
        93, 179, 144, 138, 92, 91, 236, 186, 62, 158,
        168, 56, 239, 72, 177, 76, 103, 2, 89, 14,
        45, 201, 253, 124, 26, 158, 229, 202, 96, 127,
        107, 249, 203, 151, 96, 171, 70, 178, 171, 54,
        160, 243, 51, 247, 144, 201, 0, 233, 247, 31,
        157, 117, 102, 211, 192, 140, 224, 106, 44, 244,
        225, 2, 215, 223, 158, 135, 72, 194, 143, 42,
        68, 100, 43, 15, 169, 54, 243, 70, 154, 226,
        177, 253, 220, 38, 2, 244, 128, 227, 18, 49,
        195, 113, 167, 244, 50, 54, 97, 237, 18, 119,
        64, 173, 254, 109, 102, 91, 210, 156, 30, 168,
        200, 96, 30, 4, 225, 201, 9, 19, 135, 168,
        56, 90, 112, 234, 186, 63, 197, 37, 153, 48,
        132, 113, 95, 34, 35, 121, 217, 61, 118, 210,
        27, 213, 210, 139, 196, 157, 115, 5, 132, 23,
        27, 4, 219, 79, 252, 7, 35, 201, 216, 213,
        208, 184, 103, 89, 247, 112, 249, 175, 13, 30,
        92, 127, 242, 183, 0, 138, 45, 46, 89, 130,
        122, 234, 133, 31, 130, 119, 47, 111, 233, 124,
        179, 110, 141, 237, 130, 214, 13, 129, 201, 56,
        137, 103, 77, 76, 169, 53, 153, 134, 225, 33,
        92, 233, 243, 115, 13, 32, 181, 58, 208, 203,
        20, 62, 157, 23, 89, 55, 159, 145, 171, 60,
        218, 60, 213, 126, 17, 224, 74, 54, 231, 166,
        102, 220, 68, 226, 247, 154, 250, 48, 252, 0,
        169, 194, 173, 249, 224, 248, 187, 254, 132, 49,
        216, 137, 118, 226
        };



        private Thread thread;

        private FirmwareUploader.OutputType _outputType;

        public event FirmwareMessageEventHandler DisplayMessage;

        public event FirmwareMessageEventHandler UploadCompleted;

        public void UpdateRadioFirmware(IWin32Window parent, byte[] decryptedBuf, FirmwareUploader.OutputType outputType)
        {
            parentWindow = parent;
            firmwareBuf = decryptedBuf;
            _outputType = outputType;
            thread = new Thread(DoRadioFirmwareUpdate);
            thread.Name = "Загрузчик прошивки";
            thread.CurrentCulture = new CultureInfo("ru-RU");
            thread.Start();
        }

        private void DoRadioFirmwareUpdate()
        {
            uint address = 134266880u;
            uint num = 0u;
            byte[] array = new byte[16]
            {
            79, 117, 116, 83, 101, 99, 117, 114, 105, 116,
            121, 66, 105, 110, 0, 0
            };
            uint[] array2 = new uint[10] { 134266880u, 134283264u, 134348800u, 134479872u, 134610944u, 134742016u, 134873088u, 135004160u, 135135232u, 135266304u };
            uint[] array3 = new uint[10] { 16384u, 65536u, 131072u, 131072u, 131072u, 131072u, 131072u, 131072u, 131072u, 131072u };
            try
            {
                this.DisplayMessage(this, new FirmwareUpdateMessageEventArgs(3f, "Соединение с рацией", isError: false));
                IntPtr hDevice = OpenDFU_Device();
                byte[] openFirmwareData = firmwareBuf;
                num = (uint)openFirmwareData.Length;
                bool flag = true;
                for (int i = 0; i < array.Length; i++)
                {
                    if (openFirmwareData[i] != array[i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    throw new Exception("Ошибка");
                }
                switch (_outputType)
                {
                    case FirmwareUploader.OutputType.OutputType_MD9600:
                        {
                            for (int num2 = 0; num2 < openFirmwareData.Length; num2++)
                            {
                                openFirmwareData[num2] ^= MD9600_ENCODE_CIPHER[num2 % 1024];
                            }
                            break;
                        }
                    case FirmwareUploader.OutputType.OutputType_MDUV380:
                    case FirmwareUploader.OutputType.OutputType_MD2017:
                        {
                            for (int m = 0; m < openFirmwareData.Length; m++)
                            {
                                openFirmwareData[m] ^= MDUV380_ENCODE_CIPHER[m % 1024];
                            }
                            break;
                        }
                }
                SendCustomInitSequence(hDevice);
                for (int num3 = 0; num3 < array2.Length; num3++)
                {
                    EraseSectors(hDevice, array2[num3], array3[num3]);
                }
                SetAddressPointer(hDevice, address);
                this.DisplayMessage(this, new FirmwareUpdateMessageEventArgs(100f, "Идет запись прошивки...", isError: false));
                for (uint num4 = 0u; num4 <= num / 1024; num4++)
                {
                    this.DisplayMessage(this, new FirmwareUpdateMessageEventArgs(20 + 80 * num4 * 1024 / num, "", isError: false));
                    byte[] array5 = openFirmwareData.Skip((int)(1024 * num4)).Take(1024).ToArray();
                    if (array5.Length < 1024)
                    {
                        Array.Resize(ref array5, 1024);
                        for (int num5 = array5.Length; num5 < 1024; num5++)
                        {
                            array5[num5] = byte.MaxValue;
                        }
                    }
                    WaitForDeviceIdle(hDevice);
                    STDFU_Dnload(ref hDevice, array5, (uint)array5.Length, (ushort)(num4 + 2));
                }
                WaitForDeviceIdle(hDevice);
                Detach(hDevice, address);
                this.DisplayMessage(this, new FirmwareUpdateMessageEventArgs(100f, "Прошивка рации завершена", isError: false));
                this.UploadCompleted(this, null);
            }
            catch (Exception ex)
            {
                this.DisplayMessage(this, new FirmwareUpdateMessageEventArgs(100f, ex.Message, isError: true));
                this.UploadCompleted(this, null);
            }
        }

        private IntPtr OpenDFU_Device()
        {
            uint nIndex = 0u;
            uint nRequiredSize = 0u;
            Guid gClass = new Guid(1072171435u, 64401, 19637, 166, 67, 105, 103, 13, 82, 54, 110);
            DeviceInterfaceData oInterfaceData = default(DeviceInterfaceData);
            oInterfaceData.Size = Marshal.SizeOf(oInterfaceData);
            DeviceInterfaceDetailData oDetailData = default(DeviceInterfaceDetailData);
            IntPtr intPtr = SetupDiGetClassDevs(ref gClass, null, IntPtr.Zero, 18u);
            IntPtr hDevice = IntPtr.Zero;
            try
            {
                if (intPtr == (IntPtr)(-1))
                {
                    throw new Exception("ОШИБКА: " + Marshal.GetLastWin32Error());
                }
                if (!SetupDiEnumDeviceInterfaces(intPtr, 0u, ref gClass, nIndex, ref oInterfaceData))
                {
                    throw new Exception("Рация не подключена");
                }
                SetupDiGetDeviceInterfaceDetail(intPtr, ref oInterfaceData, IntPtr.Zero, 0u, ref nRequiredSize, IntPtr.Zero);
                if (IntPtr.Size == 8)
                {
                    oDetailData.Size = 8;
                }
                else
                {
                    if (IntPtr.Size != 4)
                    {
                        throw new Exception("Операционная система не поддерживается");
                    }
                    oDetailData.Size = 5;
                }
                if (Marshal.SizeOf(oDetailData) < nRequiredSize)
                {
                    throw new Exception("Ошибка данных USB");
                }
                if (SetupDiGetDeviceInterfaceDetail(intPtr, ref oInterfaceData, ref oDetailData, nRequiredSize, ref nRequiredSize, IntPtr.Zero) && 305397760 == STDFU_Open(oDetailData.DevicePath.ToUpper(), out hDevice))
                {
                    this.DisplayMessage(this, new FirmwareUpdateMessageEventArgs(10f, "Соединено", isError: false));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                SetupDiDestroyDeviceInfoList(intPtr);
            }
            return hDevice;
        }

        private void EraseSectors(IntPtr hDevice, uint startAddress, uint firmwareSize)
        {
            int num = 0;
            uint num2 = startAddress + firmwareSize;
            uint[] array = new uint[10] { 134266880u, 134283264u, 134348800u, 134479872u, 134610944u, 134742016u, 134873088u, 135004160u, 135135232u, 135266304u };
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (num2 > array[i] && startAddress < array[i + 1])
                {
                    this.DisplayMessage(this, new FirmwareUpdateMessageEventArgs(4 + num++ * 2, "Стирание" + $" 0x{startAddress:X} - 0x{num2:X}", isError: false));
                    if (EraseSector(hDevice, array[i]) != 0)
                    {
                        throw new Exception("Ошибка стирания" + " " + array[i].ToString("X2"));
                    }
                }
            }
        }

        private void SendCustomInitSequence(IntPtr hDevice)
        {
            uint num = 0u;
            byte[] array = new byte[2] { 145, 49 };
            if ((num = STDFU_SelectCurrentConfiguration(ref hDevice, 0u, 0u, 0u)) == 305397760)
            {
                WaitForDeviceIdle(hDevice);
                if ((num = STDFU_Dnload(ref hDevice, array, (uint)array.Length, 0)) == 305397760)
                {
                    WaitForDeviceIdle(hDevice);
                    return;
                }
                throw new Exception("Ошибка инициализации" + " " + num.ToString("X8") + " (STDFU_Dnload)");
            }
            throw new Exception("Ошибка инициализации" + " " + num.ToString("X8") + " (STDFU_SelectCurrentConfiguration)");
        }

        private uint EraseSector(IntPtr hDevice, uint address)
        {
            uint num = 0u;
            byte[] array = BuildCommandBytesWithAddress(65, address);
            if ((num = STDFU_SelectCurrentConfiguration(ref hDevice, 0u, 0u, 0u)) == 305397760)
            {
                WaitForDeviceIdle(hDevice);
                if ((num = STDFU_Dnload(ref hDevice, array, (uint)array.Length, 0)) == 305397760)
                {
                    WaitForDeviceIdle(hDevice);
                    return 0u;
                }
                return num;
            }
            return num;
        }

        private void Detach(IntPtr hDevice, uint address)
        {
            byte[] array = BuildCommandBytesWithAddress(33, address);
            DFU_Status dfuStatus = default(DFU_Status);
            WaitForDeviceIdle(hDevice);
            STDFU_Dnload(ref hDevice, array, (uint)array.Length, 0);
            WaitForDeviceIdle(hDevice);
            STDFU_Dnload(ref hDevice, array, 0u, 0);
            STDFU_GetStatus(ref hDevice, ref dfuStatus);
            STDFU_ClrStatus(ref hDevice);
            STDFU_GetStatus(ref hDevice, ref dfuStatus);
        }

        private void SetAddressPointer(IntPtr hDevice, uint address)
        {
            byte[] array = BuildCommandBytesWithAddress(33, address);
            WaitForDeviceIdle(hDevice);
            STDFU_Dnload(ref hDevice, array, (uint)array.Length, 0);
            WaitForDeviceIdle(hDevice);
        }

        private byte[] BuildCommandBytesWithAddress(byte commandNumber, uint address)
        {
            return new byte[5]
            {
            commandNumber,
            (byte)(address & 0xFF),
            (byte)((address >> 8) & 0xFF),
            (byte)((address >> 16) & 0xFF),
            (byte)((address >> 24) & 0xFF)
            };
        }

        private bool WaitForDeviceIdle(IntPtr hDevice)
        {
            DFU_Status dfuStatus = default(DFU_Status);
            STDFU_GetStatus(ref hDevice, ref dfuStatus);
            while (dfuStatus.bState != 2)
            {
                STDFU_ClrStatus(ref hDevice);
                STDFU_GetStatus(ref hDevice, ref dfuStatus);
            }
            return true;
        }

        [DllImport("STDFU.dll", CharSet = CharSet.Ansi)]
        private static extern uint STDFU_Open([MarshalAs(UnmanagedType.LPStr)] string szDevicePath, out IntPtr hDevice);

        [DllImport("STDFU.dll", CharSet = CharSet.Ansi)]
        private static extern uint STDFU_SelectCurrentConfiguration(ref IntPtr hDevice, uint ConfigIndex, uint InterfaceIndex, uint AlternateSetIndex);

        [DllImport("STDFU.dll", CharSet = CharSet.Ansi)]
        private static extern uint STDFU_Dnload(ref IntPtr hDevice, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, uint nBytes, ushort nBlocks);

        [DllImport("STDFU.dll", CharSet = CharSet.Ansi, EntryPoint = "STDFU_Getstatus")]
        private static extern uint STDFU_GetStatus(ref IntPtr hDevice, ref DFU_Status dfuStatus);

        [DllImport("STDFU.dll", CharSet = CharSet.Ansi, EntryPoint = "STDFU_Clrstatus")]
        private static extern uint STDFU_ClrStatus(ref IntPtr hDevice);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern IntPtr SetupDiGetClassDevs(ref Guid gClass, [MarshalAs(UnmanagedType.LPStr)] string strEnumerator, IntPtr hParent, uint nFlags);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern int SetupDiDestroyDeviceInfoList(IntPtr lpInfoSet);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern bool SetupDiEnumDeviceInterfaces(IntPtr lpDeviceInfoSet, uint nDeviceInfoData, ref Guid gClass, uint nIndex, ref DeviceInterfaceData oInterfaceData);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr lpDeviceInfoSet, ref DeviceInterfaceData oInterfaceData, IntPtr lpDeviceInterfaceDetailData, uint nDeviceInterfaceDetailDataSize, ref uint nRequiredSize, IntPtr lpDeviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr lpDeviceInfoSet, ref DeviceInterfaceData oInterfaceData, ref DeviceInterfaceDetailData oDetailData, uint nDeviceInterfaceDetailDataSize, ref uint nRequiredSize, IntPtr lpDeviceInfoData);
    }
}
