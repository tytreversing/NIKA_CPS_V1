using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NIKA_CPS_V1.Interfaces
{
    internal class SetupAPI
    {
        // Импорт необходимых функций из SetupAPI
        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetupDiGetClassDevs(
            ref Guid classGuid,
            string enumerator,
            IntPtr hwndParent,
            int flags
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInfo(
            IntPtr deviceInfoSet,
            int memberIndex,
            ref SP_DEVINFO_DATA deviceInfoData
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiGetDeviceProperty(
            IntPtr deviceInfoSet,
            ref SP_DEVINFO_DATA deviceInfoData,
            ref DEVPROPKEY propertyKey,
            out int propertyType,
            IntPtr propertyBuffer,
            int propertyBufferSize,
            out int requiredSize,
            int flags
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        // Структуры
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            public int cbSize;
            public Guid classGuid;
            public int devInst;
            public IntPtr reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVPROPKEY
        {
            public Guid fmtid;
            public int pid;
        }

        // GUID для всех устройств нужного типа
        private static Guid GUID_DEVINTERFACE_USB_DEVICE = new Guid("4d36e978-e325-11ce-bfc1-08002be10318\r\n");

        // DEVPKEY_Device_BusReportedDeviceDesc
        private static readonly DEVPROPKEY DEVPKEY_Device_BusReportedDeviceDesc = new DEVPROPKEY
        {
            fmtid = new Guid("540b947e-8b40-45bc-a8a2-6a0b894cbda2"),
            pid = 4
        };

        private static int ParseHexString(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
                return -1;

            // Удаляем префикс "0x" или "0X", если есть
            string cleanHex = hexString.Trim();
            if (cleanHex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                cleanHex = cleanHex.Substring(2);
            }

            try
            {
                return Convert.ToInt32(cleanHex, 16);
            }
            catch
            {
                return -1;
            }
        }


        public static string GetBusReportedDeviceDesc(string _vid, string _pid)
        {
            IntPtr deviceInfoSet = IntPtr.Zero;
            SP_DEVINFO_DATA deviceInfoData = new SP_DEVINFO_DATA();
            deviceInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
            int vid = ParseHexString(_vid);
            int pid = ParseHexString(_pid);
            try
            {
                // Получаем список USB устройств
                deviceInfoSet = SetupDiGetClassDevs(
                    ref GUID_DEVINTERFACE_USB_DEVICE,
                    null,
                    IntPtr.Zero,
                    0x00000002 // DIGCF_PRESENT
                );

                if (deviceInfoSet == IntPtr.Zero)
                {
                    throw new Exception("Failed to get device list.");
                }

                int deviceIndex = 0;
                while (SetupDiEnumDeviceInfo(deviceInfoSet, deviceIndex, ref deviceInfoData))
                {
                    // Получаем Hardware ID для проверки VID/PID
                    string hardwareId = GetDeviceProperty(deviceInfoSet, deviceInfoData, 1); // SPDRP_HARDWAREID = 1

                    if (hardwareId != null &&
                        hardwareId.Contains($"VID_{vid:X4}") &&
                        hardwareId.Contains($"PID_{pid:X4}"))
                    {
                        // Нашли устройство, получаем BusReportedDeviceDesc
                        return GetDeviceProperty(deviceInfoSet, deviceInfoData, DEVPKEY_Device_BusReportedDeviceDesc);
                    }

                    deviceIndex++;
                }

                return null;
            }
            finally
            {
                if (deviceInfoSet != IntPtr.Zero)
                {
                    SetupDiDestroyDeviceInfoList(deviceInfoSet);
                }
            }
        }

        static string GetDeviceProperty(IntPtr deviceInfoSet, SP_DEVINFO_DATA deviceInfoData, DEVPROPKEY propertyKey)
        {
            int propertyType;
            int requiredSize;

            // Получаем необходимый размер буфера
            bool success = SetupDiGetDeviceProperty(
                deviceInfoSet,
                ref deviceInfoData,
                ref propertyKey,
                out propertyType,
                IntPtr.Zero,
                0,
                out requiredSize,
                0
            );

            if (!success && Marshal.GetLastWin32Error() != 122) // ERROR_INSUFFICIENT_BUFFER
            {
                return null;
            }

            IntPtr buffer = Marshal.AllocHGlobal(requiredSize);
            try
            {
                success = SetupDiGetDeviceProperty(
                    deviceInfoSet,
                    ref deviceInfoData,
                    ref propertyKey,
                    out propertyType,
                    buffer,
                    requiredSize,
                    out requiredSize,
                    0
                );

                if (success)
                {
                    return Marshal.PtrToStringUni(buffer);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return null;
        }

        // Альтернативный метод для получения стандартных свойств через SetupDiGetDeviceRegistryProperty
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr deviceInfoSet,
            ref SP_DEVINFO_DATA deviceInfoData,
            int property,
            out int propertyRegDataType,
            IntPtr propertyBuffer,
            int propertyBufferSize,
            out int requiredSize
        );

        static string GetDeviceProperty(IntPtr deviceInfoSet, SP_DEVINFO_DATA deviceInfoData, int property)
        {
            int propertyRegDataType;
            int requiredSize;

            // Получаем необходимый размер буфера
            bool success = SetupDiGetDeviceRegistryProperty(
                deviceInfoSet,
                ref deviceInfoData,
                property,
                out propertyRegDataType,
                IntPtr.Zero,
                0,
                out requiredSize
            );

            if (!success && Marshal.GetLastWin32Error() != 122) // ERROR_INSUFFICIENT_BUFFER
            {
                return null;
            }

            IntPtr buffer = Marshal.AllocHGlobal(requiredSize);
            try
            {
                success = SetupDiGetDeviceRegistryProperty(
                    deviceInfoSet,
                    ref deviceInfoData,
                    property,
                    out propertyRegDataType,
                    buffer,
                    requiredSize,
                    out requiredSize
                );

                if (success)
                {
                    return Marshal.PtrToStringAuto(buffer);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return null;
        }
    }
}
