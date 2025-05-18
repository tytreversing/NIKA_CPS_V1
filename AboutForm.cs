using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NIKA_CPS_V1
{
    public partial class AboutForm: Form
    {

        private const string FILE_PATH = "Help\\About.txt";

        public AboutForm()
        {
            InitializeComponent();
            LoadFileContent();
        }

        private void LoadFileContent()
        {
            try
            {
                if (File.Exists(FILE_PATH))
                {
                    tbAbout.Text = File.ReadAllText(FILE_PATH);
                }
                else
                {
                    tbAbout.Text = "Файл " + FILE_PATH + " не найден!";
                }
                
            }
            catch
            {
                Close();
            }
        }
    }
}
