using NAudio.Wave;
using NIKA_CPS_V1.Interfaces;
using NIKA_CPS_V1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NIKA_CPS_V1
{
    public partial class MainForm: Form
    {

        public static string PRODUCT_VERSION;

        public IWavePlayer waveOut;
        public AudioFileReader audioFileReader;

        public bool playAudio = false;
        public bool foundDFUDevice = false;
        public bool foundFlashedRadio = false;


        public MainForm()
        {
            PRODUCT_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            InitializeComponent();
            if (RegistryOperations.getProfileIntWithDefault("Setup", "ShowSplashScreen", 1) != 0)
            {
                new SplashScreen().ShowDialog();
            }
            playAudio = (RegistryOperations.getProfileIntWithDefault("Setup", "AccessibilityOptions", 0) != 0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            bool isElevated;
            string thisFilePath = Application.StartupPath;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (isElevated == false && thisFilePath.Contains("Program Files"))
            {
                MessageBox.Show("Программа установлена в папку " + thisFilePath + ", но не запущена от имени администратора. Часть функций программы может быть недоступной из-за ограничений Windows. Удалите программу и переустановите ее в другую папку, либо установите для исполняемого файла программы галочку запуска от имени администратора.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Text = "НИКА CPS  [Версия " + PRODUCT_VERSION + "]";
            Width = RegistryOperations.getProfileIntWithDefault("Setup", "LastWindowWidth", 1000);
            Height = RegistryOperations.getProfileIntWithDefault("Setup", "LastWindowHeight", 800);
            msMain.Visible = (RegistryOperations.getProfileIntWithDefault("Setup", "MenuStringVisible", 0) != 0);
            tsbReadFromRadio.Enabled = false;
            tsbWriteToRadio.Enabled = false;
            if (RegistryOperations.getProfileStringWithDefault("Setup", "AgreementConfirmed", "NO") == "NO")
            {
                if (MessageBox.Show("Программное обеспечение НИКА предоставляется бесплатно на условиях «КАК ЕСТЬ». Все действия, производимые с оборудованием и программным обеспечением, находятся исключительно на ответственности конечного пользователя. Разработчик не несет ответственности за возможный ущерб, причиненный действиями конечного пользователя программного обеспечения.\r\nСовместимость программного обеспечения с радиостанциями гарантируется в объеме, обеспеченном тестированием на момент публикации данной версии.\r\nЕсли Вы согласны с условиями предоставления программного обеспечения, нажмите «ДА».", "Пользовательское соглашение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    if (Application.MessageLoop)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        Environment.Exit(1);
                    }
                }
                else
                {
                    RegistryOperations.WriteProfileString("Setup", "AgreementConfirmed", "YES");
                }
            }
            tbConsole.AppendText("Программа загружена " + DateTime.Now.ToString() + "\r\n");
            pollingTimer.Interval = (RegistryOperations.getProfileIntWithDefault("Setup", "UsingFastPolling", 1) == 1) ? 500 : 1000;
            pollingTimer.Start();
        }

        private void tsbFirmware_Click(object sender, EventArgs e)
        {
            pollingTimer.Stop();
            new FirmwareUploader(this).ShowDialog();
            pollingTimer.Start();
        }

        private void tsbMenuToggle_Click(object sender, EventArgs e)
        {
            msMain.Visible = !msMain.Visible;
            if (msMain.Visible)
            {
                RegistryOperations.WriteProfileInt("Setup", "MenuStringVisible", 1);
            }
            else
            {
                RegistryOperations.WriteProfileInt("Setup", "MenuStringVisible", 0);
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            RegistryOperations.WriteProfileInt("Setup", "LastWindowWidth", this.Width);
            RegistryOperations.WriteProfileInt("Setup", "LastWindowHeight", this.Height);
        }

        private void playMessage(string message)
        {
            if (playAudio)
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
            if (playAudio) // если установлен глобальный флаг аудиоподсказок
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

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void msiDMRMon_Click(object sender, EventArgs e)
        {
            
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings(this);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                playMessage("settingsSaved");
                tbConsole.AppendText("Настройки программы сохранены\r\n");
                pollingTimer.Interval = (RegistryOperations.getProfileIntWithDefault("Setup", "UsingFastPolling", 1) == 1) ? 500 : 1000;
            }
                
        }

        private void pollingTimer_Tick(object sender, EventArgs e)
        {
            if (USBChecker.IsUsbDeviceConnected("0483", "DF11"))
            {
                if (!foundDFUDevice)
                {
                    tbConsole.AppendText("Обнаружен подключенный STM32-совместимый процессор в режиме DFU.\r\nИмя устройства: ");
                    tbConsole.AppendText(USBChecker.DeviceDescription() + "\r\n");
                    System.Media.SystemSounds.Asterisk.Play();
                    foundDFUDevice = true;
                }
            }
            else
                foundDFUDevice = false;
            if (USBChecker.IsUsbDeviceConnected("1FC9", "0094"))
            {
                if (!foundFlashedRadio)
                {
                    tbConsole.AppendText("Подключена рация с прошивкой OpenGD77 или OpenGD77 RUS. Работа с этими прошивками не поддерживается!\r\nИмя устройства: ");
                    tbConsole.AppendText(USBChecker.DeviceDescription() + "\r\n");
                    System.Media.SystemSounds.Hand.Play();
                    foundFlashedRadio = true;
                    tsbReadFromRadio.Enabled = true;
                    tsbWriteToRadio.Enabled = true;
                }
            }
            else
            {
                foundFlashedRadio = false;
                tsbReadFromRadio.Enabled = false;
                tsbWriteToRadio.Enabled = false;
            }

        }


    }
}
