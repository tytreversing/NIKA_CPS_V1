using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


internal static class COMPort
{
    public static SerialPort Port;
    public static bool setupCommPort()
    {
        if (Port != null)
        {
            try
            {
                if (Port.IsOpen)
                {
                    Port.Close();
                }
            }
            catch (Exception)
            {
            }
            Port = null;
        }
        try
        {
            string text = RegistryOperations.getProfileStringWithDefault("COMPort", "");

            if (text == null)
            {
                MessageBox.Show("COM-порт не выбран!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Port = new SerialPort(text, 115200, Parity.None, 8, StopBits.One);
                Port.ReadTimeout = 1000;
            }
        }
        catch (Exception)
        {
            Port = null;
            SystemSounds.Hand.Play();
            MessageBox.Show("Ошибка при соединении с COM-портом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            RegistryOperations.WriteProfileString("LastCommPort", "");
            return false;
        }
        try
        {
            Port.Open();
        }
        catch (Exception)
        {
            SystemSounds.Hand.Play();
            MessageBox.Show("COM-порт недоступен. Проверьте правильность соединения и корректность работы драйвера.", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Port = null;
            return false;
        }
        return true;
    }
}
