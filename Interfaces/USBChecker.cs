using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;


namespace NIKA_CPS_V1.Interfaces
{
    internal class USBChecker
    {
          public static bool IsUsbDeviceConnected(string vid, string pid)
          {
              try
              {
                  // Формируем искомую подстроку в формате VID_XXXX&PID_XXXX
                  string searchPattern = $"VID_{vid.ToUpper()}&PID_{pid.ToUpper()}";

                  // Запрос к WMI для получения активных PnP-устройств
                  using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                      "SELECT DeviceID, Status FROM Win32_PnPEntity WHERE Status = 'OK'"))
                  {
                      foreach (ManagementObject device in searcher.Get())
                      {
                          string deviceId = (device["DeviceID"] as string)?.ToUpper();

                          if (deviceId != null && deviceId.Contains(searchPattern))
                          {
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
