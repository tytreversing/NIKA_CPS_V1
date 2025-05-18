using NIKA_CPS_V1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public MainForm()
        {
            PRODUCT_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            InitializeComponent();
            if (RegistryOperations.getProfileIntWithDefault("Setup", "ShowSplashScreen", 1) != 0)
            {
                new SplashScreen().ShowDialog();
            }
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
                MessageBox.Show("Программа установлена в папку " + thisFilePath + ", но не запущена от имени администратора. Автообновление файла локализации и ручное его скачивание через Загрузчик прошивки будет недоступно из-за ограничений Windows. Удалите программу и переустановите ее в другую папку, либо установите для исполняемого файла программы галочку запуска от имени администратора.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Text = "НИКА CPS  [Версия " + PRODUCT_VERSION + "]";
            Width = RegistryOperations.getProfileIntWithDefault("Setup", "LastWindowWidth", 1000);
            Height = RegistryOperations.getProfileIntWithDefault("Setup", "LastWindowHeight", 800);
            msMain.Visible = (RegistryOperations.getProfileIntWithDefault("Setup", "MenuStringVisible", 0) != 0);
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
            tbConsole.Text += "Программа загружена " + DateTime.Now.ToString() + "\r\n";
        }

        private void tsbFirmware_Click(object sender, EventArgs e)
        {
            new FirmwareUploader().ShowDialog();
        }

        private void tsbMenuToggle_Click(object sender, EventArgs e)
        {
            this.msMain.Visible = !this.msMain.Visible;
            if (this.msMain.Visible)
                RegistryOperations.WriteProfileInt("Setup", "MenuStringVisible", 1);
            else
                RegistryOperations.WriteProfileInt("Setup", "MenuStringVisible", 0);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            RegistryOperations.WriteProfileInt("Setup", "LastWindowWidth", this.Width);
            RegistryOperations.WriteProfileInt("Setup", "LastWindowHeight", this.Height);
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}
