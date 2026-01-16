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
            cbShowSplashScreen.Checked = (RegistryOperations.getProfileIntWithDefault("ShowSplashScreen", 1) != 0);
            cbUseVoiceHelp.Checked = (RegistryOperations.getProfileIntWithDefault("AccessibilityOptions", 0) != 0);
            rbFastPolling.Checked = (RegistryOperations.getProfileIntWithDefault("UsingFastPolling", 1) != 0);
            rbSlowPolling.Checked = !rbFastPolling.Checked;
            tbRadioVID.Text = MainForm.radioVID;
            tbRadioPID.Text = MainForm.radioPID;
            cbConfirmExit.Checked = (RegistryOperations.getProfileIntWithDefault("ConfirmExit", 1) != 0);
            cbExpandContacts.Checked = (RegistryOperations.getProfileIntWithDefault("ExpandContacts", 0) != 0);
            cbExpandChannels.Checked = (RegistryOperations.getProfileIntWithDefault("ExpandChannels", 0) != 0);
        }

        private void bSaveAppSettings_Click(object sender, EventArgs e)
        {
            RegistryOperations.WriteProfileInt("ShowSplashScreen", (cbShowSplashScreen.Checked ? 1 : 0));
            RegistryOperations.WriteProfileInt("AccessibilityOptions", (cbUseVoiceHelp.Checked ? 1 : 0));
            RegistryOperations.WriteProfileInt("ConfirmExit", (cbConfirmExit.Checked ? 1 : 0));
            RegistryOperations.WriteProfileInt("UsingFastPolling", (rbFastPolling.Checked ? 1 : 0));
            RegistryOperations.WriteProfileInt("ExpandContacts", (cbExpandContacts.Checked ? 1 : 0));
            RegistryOperations.WriteProfileInt("ExpandChannels", (cbExpandChannels.Checked ? 1 : 0));
            MainForm.playAudio = cbUseVoiceHelp.Checked;
            if (MainForm.isValidHex(tbRadioVID.Text))
            {
                RegistryOperations.WriteProfileString("DeviceVID", tbRadioVID.Text);
                MainForm.radioVID = tbRadioVID.Text.ToUpper();
            }
            if (MainForm.isValidHex(tbRadioPID.Text))
            {
                RegistryOperations.WriteProfileString("DevicePID", tbRadioPID.Text);
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
