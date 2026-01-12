using NAudio.Wave;
using NIKA_CPS_V1.Codeplug;
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
using System.Text.RegularExpressions;
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

        public string radioVID = "";
        public string radioPID = "";

        public static CodeplugData CodeplugInternal;

        private string codeplugFileName;

        public bool isValidHex(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            // Проверяем, что строка состоит ровно из 4 шестнадцатеричных символов
            // (регистронезависимо, без префиксов и пробелов)
            return Regex.IsMatch(input.Trim(), @"^[0-9a-fA-F]{4}$");
        }

        public MainForm()
        {
            PRODUCT_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            InitializeComponent();
            if (RegistryOperations.getProfileIntWithDefault("ShowSplashScreen", 1) != 0)
            {
                new SplashScreen().ShowDialog();
            }
            playAudio = (RegistryOperations.getProfileIntWithDefault("AccessibilityOptions", 0) != 0);
            radioVID = RegistryOperations.getProfileStringWithDefault("DeviceVID", "05D0");
            radioPID = RegistryOperations.getProfileStringWithDefault("DevicePID", "0094");
        }

        //поиск в аргументах командной строки имени файла кодплага
        private string ValidCodeplugFilePath(string[] args)
        {
            if (args == null) return null;

            foreach (string arg in args)
            {
                if (string.IsNullOrWhiteSpace(arg)) continue;

                try
                {
                    // Проверяем расширение
                    if (!Path.GetExtension(arg).Equals(".ncf", StringComparison.OrdinalIgnoreCase))
                        continue;

                    // Проверяем, что это файл, а не папка
                    string fileName = Path.GetFileName(arg);
                    if (string.IsNullOrEmpty(fileName) || fileName == "." || fileName == "..")
                        continue;

                    // Получаем полный путь
                    string fullPath = Path.GetFullPath(arg);

                    // Возвращаем полный путь к файлу
                    return fullPath;
                }
                catch
                {
                    // Если возникла ошибка, переходим к следующему аргументу
                    continue;
                }
            }

            return null;
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
            //проверяем, нет ли в командной строке имени файла с кодплагом
            codeplugFileName = ValidCodeplugFilePath(Environment.GetCommandLineArgs().Skip(1).ToArray());
            if (codeplugFileName == null) //запуск без передачи параметров
            {
                //получаем из реестра последний файл кодплага, если сохранен, иначе болванку в папке с программой
                codeplugFileName = RegistryOperations.getProfileStringWithDefault("LastCodeplugFile", thisFilePath + "\\Nika_CPS_V1.ncf");

            }
            else //запуск с параметрами
            {
                tbConsole.AppendText("Загружен кодплаг из файла " + codeplugFileName + "\r\n");
            }
            // пытаемся открыть файл
            if (!File.Exists(codeplugFileName))
            {
                playMessage("file_not_found_error");
                MessageBox.Show("Файл " + codeplugFileName + " не найден по указанному адресу. Будет сгенерирован шаблонный кодплаг.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //создаем пустой объект и сериализуем его
                CodeplugInternal = new CodeplugData();
                try
                {
                    CodeplugSerialization serializer = new CodeplugSerialization(CodeplugInternal, thisFilePath + "\\Nika_CPS_V1.ncf");
                }
                catch
                {
                    playMessage("error_saving_template");
                    MessageBox.Show("Ошибка при попытке сохранения файла шаблона! Проверьте права доступа!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CodeplugDeserialization deSerializer = new CodeplugDeserialization();
                try
                {
                    CodeplugInternal = deSerializer.Deserialize(codeplugFileName);
                }
                catch
                {
                    playMessage("error_reading_codeplug");
                    MessageBox.Show("Ошибка при чтении файла кодплага. Будет сгенерирован шаблонный кодплаг.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CodeplugInternal = new CodeplugData();
                }
            }
            Text = "НИКА CPS  [Версия " + PRODUCT_VERSION + "]  " + codeplugFileName;
            Width = RegistryOperations.getProfileIntWithDefault("LastWindowWidth", 1000);
            Height = RegistryOperations.getProfileIntWithDefault("LastWindowHeight", 800);
            msMain.Visible = (RegistryOperations.getProfileIntWithDefault("MenuStringVisible", 0) != 0);
            tsbReadFromRadio.Enabled = false;
            tsbWriteToRadio.Enabled = false;
            if (RegistryOperations.getProfileStringWithDefault("AgreementConfirmed", "NO") == "NO")
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
                    RegistryOperations.WriteProfileString("AgreementConfirmed", "YES");
                }
            }
            tbConsole.AppendText("Программа загружена " + DateTime.Now.ToString() + "\r\n");

            //генерация дерева
            GenerateTree();

            pollingTimer.Interval = (RegistryOperations.getProfileIntWithDefault("UsingFastPolling", 1) == 1) ? 500 : 1000;
            pollingTimer.Start();
            
        }

        public void GenerateTree()
        {
            tvMain.NodeMouseClick += tvMain_NodeMouseClick;
            // Генерация узлов из контактов
            // Находим узел ContactsNode 
            TreeNode contactsNode = tvMain.Nodes.Find("ContactsNode", true).FirstOrDefault();

            // Проверяем, найден ли узел
            if (contactsNode != null && CodeplugInternal != null && CodeplugInternal.Contacts != null)
            {
                // Очищаем существующие дочерние узлы (если нужно)
                contactsNode.Nodes.Clear();

                // Добавляем узлы для каждого контакта
                foreach (Codeplug.Contact contact in CodeplugInternal.Contacts)
                {
                    string alias = contact.Alias ?? "Без имени";
                    TreeNode newNode = new TreeNode(alias);
                    newNode.Tag = contact;
                    contactsNode.Nodes.Add(newNode);

                }

                // Разворачиваем узел для отображения дочерних элементов, если настроено
                if (RegistryOperations.getProfileIntWithDefault("ExpandContacts", 0) != 0)
                    contactsNode.Expand();
            }
            
        }

        private void tvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Проверяем, что клик был по узлу контакта
            if (e.Node.Parent != null && e.Node.Parent.Name == "ContactsNode")
            {
                // Получаем текст узла (который является Alias)
                string alias = e.Node.Text;
                string dmrid = ((Codeplug.Contact)e.Node.Tag).DMR_ID.ToString();
                if (e.Button == MouseButtons.Left)
                {
                    Contact contactForm = new Contact((Codeplug.Contact)e.Node.Tag);
                    contactForm.ShowDialog();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    MessageBox.Show($"Кликнут правой контакт: {alias}");
                }
            }
            else if (e.Node.Parent != null && e.Node.Parent.Name == "GroupListsNode")
            {
                                                                                                                  
            }
        }

        private void tsbFirmware_Click(object sender, EventArgs e)
        {
            pollingTimer.Stop();
            new FirmwareUploader(this).Show();
            pollingTimer.Start();
        }

        private void tsbMenuToggle_Click(object sender, EventArgs e)
        {
            msMain.Visible = !msMain.Visible;
            if (msMain.Visible)
            {
                RegistryOperations.WriteProfileInt("MenuStringVisible", 1);
            }
            else
            {
                RegistryOperations.WriteProfileInt("MenuStringVisible", 0);
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            RegistryOperations.WriteProfileInt("LastWindowWidth", this.Width);
            RegistryOperations.WriteProfileInt("LastWindowHeight", this.Height);
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

        public void CleanupAudio()
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
                pollingTimer.Interval = (RegistryOperations.getProfileIntWithDefault("UsingFastPolling", 1) == 1) ? 500 : 1000;
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
            if (USBChecker.IsUsbDeviceConnected(radioVID, radioPID))
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

        private void msiCalibration_Click(object sender, EventArgs e)
        {
            new CalibrationForm(this).ShowDialog();
        }

        private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
