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

        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;

        private MainForm _parent;


        public Settings(MainForm parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            cbShowSplashScreen.Checked = (RegistryOperations.getProfileIntWithDefault("ShowSplashScreen", 1) != 0);
            cbUseVoiceHelp.Checked = (RegistryOperations.getProfileIntWithDefault("AccessibilityOptions", 0) != 0);
            rbFastPolling.Checked = (RegistryOperations.getProfileIntWithDefault("UsingFastPolling", 1) != 0);
            rbSlowPolling.Checked = !rbFastPolling.Checked;
            tbRadioVID.Text = _parent.radioVID;
            tbRadioPID.Text = _parent.radioPID;
        }

        private void bSaveAppSettings_Click(object sender, EventArgs e)
        {
            RegistryOperations.WriteProfileInt("ShowSplashScreen", (cbShowSplashScreen.Checked ? 1 : 0));
            RegistryOperations.WriteProfileInt("AccessibilityOptions", (cbUseVoiceHelp.Checked ? 1 : 0));
            RegistryOperations.WriteProfileInt("UsingFastPolling", (rbFastPolling.Checked ? 1 : 0));
            _parent.playAudio = cbUseVoiceHelp.Checked;
            if (_parent.isValidHex(tbRadioVID.Text))
            {
                RegistryOperations.WriteProfileString("DeviceVID", tbRadioVID.Text);
                _parent.radioVID = tbRadioVID.Text.ToUpper();
            }
            if (_parent.isValidHex(tbRadioPID.Text))
            {
                RegistryOperations.WriteProfileString("DevicePID", tbRadioPID.Text);
                _parent.radioPID = tbRadioPID.Text.ToUpper();
            }
            Close();
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


    }
}
