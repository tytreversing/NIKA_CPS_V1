using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace NIKA_CPS_V1.Interfaces
{
    internal class USBChecker
    {
        private static string _deviceDescription = "";

        public static string DeviceDescription()
        {
            return _deviceDescription;
        }

        public static string GetComPortFromString()
        {
            Match match = Regex.Match(_deviceDescription, @"COM\d{1,3}", RegexOptions.IgnoreCase);
            return match.Success ? match.Value.ToUpper() : null;
        }
        public static bool IsUsbDeviceConnected(string vid, string pid)
        {
            try
            {
                // Формируем искомую подстроку в формате VID_XXXX&PID_XXXX
                string searchPattern = $"VID_{vid.ToUpper()}&PID_{pid.ToUpper()}";

                // Запрос к WMI для получения активных PnP-устройств
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                      "SELECT DeviceID, Status, Name FROM Win32_PnPEntity WHERE Status = 'OK'"))
                {
                    foreach (ManagementObject device in searcher.Get())
                    {
                        string deviceId = (device["DeviceID"] as string)?.ToUpper();

                        if (deviceId != null && deviceId.Contains(searchPattern))
                        {
                            try
                            {
                                _deviceDescription = (device["Name"] as string);
                            }
                            catch
                            {

                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                // В случае ошибки возвращаем false
                return false;
            }
        }


    }
}
