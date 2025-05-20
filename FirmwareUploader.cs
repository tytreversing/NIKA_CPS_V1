using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace NIKA_CPS_V1
{
    public partial class FirmwareUploader: Form
    {
        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;

        private MainForm _parent;

        private STM_DFU_FwUpdate fwUpdate;

        private bool firmwareVerified = false;

        public enum OutputType
        {
            OutputType_MD9600,
            OutputType_MDUV380,
            OutputType_MD2017
        }

        private OutputType outputType = OutputType.OutputType_MD9600;
        private byte[] decryptedFirmware = null;


        public FirmwareUploader(MainForm parent)
        {
            InitializeComponent();
            _parent = parent;
            string lastRadioType = RegistryOperations.getProfileStringWithDefault("Setup", "LastFlashedRadio", null);
            if (lastRadioType != "")
            {
                foreach (RadioButton control in gbRadioType.Controls)
                {
                    if (control.Text == lastRadioType)
                    {
                        control.Checked = true;
                    }
                }
            }
            fwUpdate = new STM_DFU_FwUpdate();
            fwUpdate.DisplayMessage += DisplayMessage;
            fwUpdate.UploadCompleted += UploadCompleted;

        }

        private void playMessage(string message)
        {
            if (_parent.playAudio)
            {
                // Остановить предыдущее воспроизведение
                waveOut?.Stop();
                waveOut?.Dispose();
                audioFileReader?.Dispose();

                if (string.IsNullOrEmpty(message)) return;

                // Путь к файлу
                string soundPath = Path.Combine(
                    Application.StartupPath,
                    "Sounds",
                    $"{message}.mp3");

                // Проверка существования файла
                if (!File.Exists(soundPath)) return;

                try
                {
                    // Инициализация аудиопотока
                    audioFileReader = new AudioFileReader(soundPath);
                    waveOut = new WaveOutEvent();
                    waveOut.Init(audioFileReader);
                    waveOut.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка воспроизведения: {ex.Message}");
                    CleanupAudio();
                }
            }

        }


        private void Control_MouseEnter(object sender, EventArgs e)
        {
            if (_parent.playAudio)
            {
                // Остановить предыдущее воспроизведение
                waveOut?.Stop();
                waveOut?.Dispose();
                audioFileReader?.Dispose();

                // Получить имя элемента
                string controlName = (sender as dynamic)?.Name;
                if (string.IsNullOrEmpty(controlName)) return;

                // Путь к файлу
                string soundPath = Path.Combine(
                    Application.StartupPath,
                    "Sounds",
                    $"{controlName}.mp3");

                // Проверка существования файла
                if (!File.Exists(soundPath)) return;

                try
                {
                    // Инициализация аудиопотока
                    audioFileReader = new AudioFileReader(soundPath);
                    waveOut = new WaveOutEvent();
                    waveOut.Init(audioFileReader);
                    waveOut.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка воспроизведения: {ex.Message}");
                    CleanupAudio();
                }
            }

        }

        private void CleanupAudio()
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            audioFileReader?.Dispose();
            waveOut = null;
            audioFileReader = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CleanupAudio();
            base.OnFormClosing(e);
        }



        private byte[] DecryptRC4(byte[] data, byte[] key)
        {
            if (key.Length == 0)
                throw new ArgumentException("Ключ шифрования прошивки не может быть пустым!", nameof(key));

            // Инициализация S-блока
            byte[] S = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            // Алгоритм ключевого расписания (KSA)
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256;
                // Обмен значений S[i] и S[j]
                (S[i], S[j]) = (S[j], S[i]);
            }

            // Генерация ключевого потока и декодирование (PRGA)
            byte[] result = new byte[data.Length];
            int i2 = 0, j2 = 0;
            for (int k = 0; k < data.Length; k++)
            {
                i2 = (i2 + 1) % 256;
                j2 = (j2 + S[i2]) % 256;
                // Обмен значений S[i2] и S[j2]
                (S[i2], S[j2]) = (S[j2], S[i2]);
                // Получаем ключевой байт
                byte keyByte = S[(S[i2] + S[j2]) % 256];
                // Применяем XOR к данным
                result[k] = (byte)(data[k] ^ keyByte);
            }

            return result;
        }

        private string CalculateChecksum(byte[] data)
        {
            Int64 checksum = 0;
            foreach (byte b in data)
            {
                checksum += b;
            }
            return checksum.ToString("X8");
        }

        private void DisplayMessage(object sender, FirmwareUpdateMessageEventArgs e)
        {
            if (pbUploading.InvokeRequired)
            {
                Invoke(new EventHandler<FirmwareUpdateMessageEventArgs>(DisplayMessage), sender, e);
                return;
            }
            if (e.Message != "")
                tbConsole.AppendText(e.Message + "\r\n");
            if (e.IsError)
            {
                MessageBox.Show(e.Message, "Ошибка!");
                pbUploading.Value = 0;
            }
            else
            {
                pbUploading.Value = (int)e.Percentage;
            }
        }

        private void UploadCompleted(object sender, FirmwareUpdateMessageEventArgs e)
        {
            if (pbUploading.InvokeRequired)
            {
                Invoke(new EventHandler<FirmwareUpdateMessageEventArgs>(UploadCompleted), sender, e);
                return;
            }
        }

        private bool containsStringSequence(byte[] data, string marker)
        {
            // Преобразуем строку в байты с использованием UTF-8 кодировки
            byte[] searchBytes = Encoding.UTF8.GetBytes(marker);
            return containsSubsequence(data, searchBytes);
        }

        private static bool containsSubsequence(byte[] source, byte[] sequence)
        {
            // Обработка случая с пустой последовательностью
            if (sequence.Length == 0)
                return false; // Или true, если пустая строка считается найденной

            // Проверяем вхождение последовательности
            for (int i = 0; i <= source.Length - sequence.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < sequence.Length; j++)
                {
                    if (source[i + j] != sequence[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return true;
            }
            return false;
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            tbConsole.Text = "";
            if (rbMD9600.Checked)
            {
                RegistryOperations.WriteProfileString("Setup", "LastFlashedRadio", rbMD9600.Text);
                outputType = OutputType.OutputType_MD9600;
            }
            else if (rbMDUV380.Checked)
            {
                RegistryOperations.WriteProfileString("Setup", "LastFlashedRadio", rbMDUV380.Text);
                outputType = OutputType.OutputType_MDUV380;
            }
            pbUploading.Value = 0;
            ofdOpenFirmware.InitialDirectory = RegistryOperations.getProfileStringWithDefault("Setup", "FirmwareLocation", null);
            if (ofdOpenFirmware.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            RegistryOperations.WriteProfileString("Setup", "FirmwareLocation", Path.GetDirectoryName(ofdOpenFirmware.FileName));
            byte[] openFirmwareBuf = null;
            try
            {
                openFirmwareBuf = File.ReadAllBytes(ofdOpenFirmware.FileName);
                decryptedFirmware = DecryptRC4(openFirmwareBuf, Encoding.UTF8.GetBytes("OJ8ACqvn4ac6ci7WITh9bewW2igPfoJF2WnUh1q8Y0Nq5KmIUjplYgc4YLYrVDWo"));
            }
            catch
            {
                tbConsole.AppendText("Ошибка при открытии файла!\r\n");
                playMessage("firmwareReadingError");
                return;
            }
            finally
            {
                tbConsole.AppendText("Файл прошивки открыт и успешно считан\r\n");
                tsbUpdate.Enabled = true;
            }
            tbConsole.AppendText("Проверка валидности прошивки...  ");
            if (containsStringSequence(decryptedFirmware, "TYT MD-9600"))
            {
                tbConsole.AppendText("OK: прошивка НИКА для TYT MD-9600/Retevis RT-90\r\n");
                if (!rbMD9600.Checked)
                    if (MessageBox.Show("Загруженный файл прошивки предназначен для TYT MD-9600/Retevis RT-90, однако как целевая рация выбрана другая модель.\r\nУстановить эту рацию целевой моделью для программирования?", "ВНИМАНИЕ!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        rbMD9600.Checked = true;
                        tbConsole.AppendText("Целевая рация сменена\r\n");
                        RegistryOperations.WriteProfileString("Setup", "LastFlashedRadio", rbMD9600.Text);
                    }
            }
            else if (containsStringSequence(decryptedFirmware, "TYT MD-UV3xx"))
            {
                tbConsole.AppendText("OK: прошивка НИКА для раций семейства TYT MD-UV3xx/Retevis RT-3S\r\n");
                if (!rbMDUV380.Checked)
                    if (MessageBox.Show("Загруженный файл прошивки предназначен для TYT MD-UV380/390/Retevis RT-3S, однако как целевая рация выбрана другая модель.\r\nУстановить эту рацию целевой моделью для программирования?", "ВНИМАНИЕ!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        rbMDUV380.Checked = true;
                        tbConsole.AppendText("Целевая рация сменена\r\n");
                        RegistryOperations.WriteProfileString("Setup", "LastFlashedRadio", rbMDUV380.Text);
                    }
            }
            else
            {
                tbConsole.AppendText("ОШИБКА: Файл поврежден или не является файлом прошивки НИКА!\r\n");
                System.Media.SystemSounds.Hand.Play();
                playMessage("firmwareError");
                tsbUpdate.Enabled = false;
                return;
            }
            tbConsole.AppendText("Контрольная сумма файла прошивки: 0x" + CalculateChecksum(openFirmwareBuf) + "\r\n");
            tbConsole.AppendText("Контрольная сумма дешифрованной прошивки: 0x" + CalculateChecksum(decryptedFirmware) + "\r\n");
            tbConsole.AppendText("Теперь запишите прошивку в радиостанцию\r\n");
            playMessage("nowYouCanUpload");
            System.Media.SystemSounds.Asterisk.Play();
            flashTimer.Start();
            firmwareVerified = true;
            
        }

        private void tbHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help/Firmware.chm");
        }

        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            if (firmwareVerified)
            {
                fwUpdate.UpdateRadioFirmware(this, decryptedFirmware, outputType);
            }
        }

        private int flashCount = 0;
        private void flashTimer_Tick(object sender, EventArgs e)
        {
            if (flashCount < 20)
            {
                this.tsbUpdate.BackColor = (this.tsbUpdate.BackColor == Color.Transparent) ?
                    Color.Red : Color.Transparent;
                flashCount++; 
            }
            else
            {
                this.flashTimer.Stop();
                flashCount = 0;
            }
        }
    }
}
