using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIKA_CPS_V1
{
    public partial class Settings : Form
    {

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            cbShowSplashScreen.Checked = (RegistryOperations.IsFlagSet("ShowSplashScreen"));
            cbUseVoiceHelp.Checked = (RegistryOperations.IsFlagSet("AccessibilityOptions", false));
            rbFastPolling.Checked = (RegistryOperations.IsFlagSet("UsingFastPolling"));
            rbSlowPolling.Checked = !rbFastPolling.Checked;
            tbRadioVID.Text = MainForm.radioVID;
            tbRadioPID.Text = MainForm.radioPID;
            cbConfirmExit.Checked = (RegistryOperations.IsFlagSet("ConfirmExit"));
            cbExpandContacts.Checked = (RegistryOperations.IsFlagSet("ExpandContacts", false));
            cbExpandChannels.Checked = (RegistryOperations.IsFlagSet("ExpandChannels", false));
            cbInfoBox.Checked = (RegistryOperations.IsFlagSet("ShowInfoBox"));
            if (MainForm.hasFullAccess)
            {
                lVID.Visible = true;
                lPID.Visible = true;    
                tbRadioPID.Visible = true;
                tbRadioVID.Visible = true;
                cbConfirmExit.Location = new Point(14, 193);
                cbInfoBox.Location = new Point(14, 210);
            }
        }

        private void bSaveAppSettings_Click(object sender, EventArgs e)
        {
            RegistryOperations.SetFlag("ShowSplashScreen", cbShowSplashScreen.Checked);
            RegistryOperations.SetFlag("AccessibilityOptions", cbUseVoiceHelp.Checked);
            RegistryOperations.SetFlag("ConfirmExit", cbConfirmExit.Checked);
            RegistryOperations.SetFlag("UsingFastPolling", rbFastPolling.Checked);
            RegistryOperations.SetFlag("ExpandContacts", cbExpandContacts.Checked);
            RegistryOperations.SetFlag("ExpandChannels", cbExpandChannels.Checked);
            RegistryOperations.SetFlag("ShowInfoBox", cbInfoBox.Checked);
            MainForm.playAudio = cbUseVoiceHelp.Checked;
            if (MainForm.isValidHex(tbRadioVID.Text))
            {
                RegistryOperations.WriteString("DeviceVID", tbRadioVID.Text);
                MainForm.radioVID = tbRadioVID.Text.ToUpper();
            }
            if (MainForm.isValidHex(tbRadioPID.Text))
            {
                RegistryOperations.WriteString("DevicePID", tbRadioPID.Text);
                MainForm.radioPID = tbRadioPID.Text.ToUpper();
            }
            Close();
        }


        private void Control_MouseEnter(object sender, EventArgs e)
        {
            // Получить имя элемента
            string controlName = (sender as dynamic)?.Name;
            if (string.IsNullOrEmpty(controlName)) return;
            MainForm.playMessage(controlName);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            MainForm.CleanupAudio();
            base.OnFormClosing(e);
        }


    }
}
