using System;
using System.IO;
using System.Windows.Forms;


namespace NIKA_CPS_V1
{
    public partial class AboutForm: Form
    {

        private const string FILE_PATH = "Help\\About.rtf";

        public AboutForm()
        {
            InitializeComponent();
            rbAbout.SelectionIndent = 20;
            LoadFileContent();
        }

        private void LoadFileContent()
        {
            try
            {
                if (File.Exists(FILE_PATH))
                {
                    rbAbout.LoadFile(FILE_PATH, RichTextBoxStreamType.RichText);
                    rbAbout.SelectionStart = 0;
                    rbAbout.SelectionLength = 0;
                    rbAbout.GotFocus += (sender, e) =>
                    {
                        // При получении фокуса сбрасываем выделение
                        rbAbout.SelectionStart = 0;
                        rbAbout.SelectionLength = 0;
                        this.ActiveControl = null;
                    };
                }
                else
                {
                    rbAbout.Text = "Файл " + FILE_PATH + " не найден!";
                }
                
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файла " + FILE_PATH + ". Возможно, он поврежден или не соответствует формату RTF.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
