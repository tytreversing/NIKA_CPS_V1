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
        }

        private void tsbFirmware_Click(object sender, EventArgs e)
        {
            new FirmwareUploader().ShowDialog();
        }
    }
}
