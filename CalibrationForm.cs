using NIKA_CPS_V1.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using static NIKA_CPS_V1.Interfaces.FirmwareInterface;
using static NIKA_CPS_V1.DataTransfer;




namespace NIKA_CPS_V1
{
    public class CalibrationForm : Form
    {
        private const int BUFFER_SIZE = 1032;

        private const int CALIBRATIONS_ADDRESS = 0x0030;

        private const int CALIBRATION_TABLE_SIZE = 0x150;

        private const byte CPS_WRITE = 0x57;
        private const byte CPS_READ = 0x52;

        private byte[] dataBuffer;

        private IContainer components = null;

        private Button btnWrite;

        private Button btnReadFile;

        private Button btnReadFromRadio;
        private Button btnSaveCalibration;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CalibrationData));
        private TabControl tabs;
        private TabPage tabVHF;
        private TabPage tabUHF;
        private GroupBox gbCommons;
        private NumericUpDown nmRSSI120;
        private Label label5;
        private NumericUpDown nmRSSI70;
        private Label label6;
        private TableLayoutPanel tlpVHF;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Button btnRestoreFactory;
        private Label label16;
        private Label lblRadioType;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private NumericUpDown nmVHFOscRef;
        private Label label34;
        private TableLayoutPanel tlpUHF;
        private NumericUpDown nmUHFOscRef;
        private Label label35;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label7;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label45;
        private Label label46;
        private Label label47;
        private Label label48;
        private Label label49;
        private Label label50;
        private Label label51;
        private Label label52;
        private Label label53;
        private Label label54;
        private Button btnClearColors;
        private Button btnChart;
        private NumericUpDown nmDevFMVHF;
        private Label label1;
        private NumericUpDown nmDevFMNVHF;
        private Label label2;
        private Label label4;
        private NumericUpDown nmDevFMUHF;
        private Label label3;
        private NumericUpDown nmDevFMNUHF;
        public static CalibrationData CalData = new CalibrationData();

        public CalibrationForm()
        {
            InitializeComponent();
            base.Icon = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            dataBuffer = new byte[CALIBRATION_TABLE_SIZE];
            SetDoubleBuffered(tlpVHF);
            SetDoubleBuffered(tlpUHF);
            prepareTables();
        }

        private void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession) return;

            System.Reflection.PropertyInfo prop = typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            prop?.SetValue(control, true, null);
        }

        public static RadioBandlimits radioBandlimits = new();

        private static RadioBandlimits ByteArrayToRadioBandlimits(byte[] bytes)
        {
            GCHandle gCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (RadioBandlimits)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(RadioBandlimits));
            }
            finally
            {
                gCHandle.Free();
            }
        }

        private RadioBandlimits readBandlimits()
        {
            byte[] send = new byte[BUFFER_SIZE];
            byte[] receive = new byte[BUFFER_SIZE];
            byte[] raw = new byte[BUFFER_SIZE];
            int num = 0;
            if (!COMPort.SetupPort())
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Отсутствует заданный COM-порт");
                return null;
            }
            send[0] = CPS_READ;
            send[1] = (byte)DataTransfer.DataMode.ReadBandlimits;
            send[2] = 0;
            send[3] = 0;
            send[4] = 0;
            send[5] = 0;
            send[6] = 0;
            send[7] = 0;
            COMPort.Port.Write(send, 0, 8);
            while (COMPort.Port.BytesToWrite > 0)
            {
                Thread.Sleep(1);
            }
            while (COMPort.Port.BytesToRead == 0)
            {
                Thread.Sleep(5);
            }
            COMPort.Port.Read(receive, 0, COMPort.Port.BytesToRead);
            if (receive[0] == CPS_READ)
            {
                int num2 = (receive[1] << 8) + receive[2];
                for (int i = 0; i < num2; i++)
                {
                    raw[num++] = receive[i + 3];
                }
                COMPort.Port.Close();
                COMPort.Port = null;
                return ByteArrayToRadioBandlimits(raw);
            }
            return null;
        }



        public bool readDataFromRadio()
        {
            if (!COMPort.SetupPort())
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Отсутствует заданный COM-порт");
                return false;
            }
            DataTransfer COMData = new DataTransfer();
            SendCommand(COMPort.Port, CPSCommand.InitUI);
            SendCommand(COMPort.Port, CPSCommand.ClearScreen);
            SendCommand(COMPort.Port, CPSCommand.WriteString, 0, 0, 3, 1, 0, "Чтение");
            SendCommand(COMPort.Port, CPSCommand.WriteString, 0, 16, 3, 1, 0, "калибровок");
            SendCommand(COMPort.Port, CPSCommand.UpdateScreen);

            CalData = ReadCalibrations(COMPort.Port);
            Thread.Sleep(1000);
            SendCommand(COMPort.Port, CPSCommand.Finish, 3);
            SendCommand(COMPort.Port, CPSCommand.CloseUI);
            SendCommand(COMPort.Port, CPSCommand.RestartGPS);
            COMPort.Port.Close();
            COMPort.Port = null;

            buildVariablesFromCalData(CalData);
            return true;
        }



        private void btnWrite_Click(object sender, EventArgs e)
        {
            buildCalDataFromVariables(CalData);
            DataTransfer COMData = new DataTransfer();
            COMData.dataBuffer = new byte[CALIBRATION_TABLE_SIZE];
            dataBuffer = DataToByte(CalData);
            Array.Copy(dataBuffer, 0, COMData.dataBuffer, 0, CALIBRATION_TABLE_SIZE);
            if (!COMPort.SetupPort())
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Отсутствует заданный COM-порт");
                return;
            }
            SendCommand(COMPort.Port, CPSCommand.InitUI);
            SendCommand(COMPort.Port, CPSCommand.ClearScreen);
            SendCommand(COMPort.Port, CPSCommand.WriteString, 0, 0, 3, 1, 0, "Запись");
            SendCommand(COMPort.Port, CPSCommand.WriteString, 0, 16, 3, 1, 0, "калибровок");
            SendCommand(COMPort.Port, CPSCommand.UpdateScreen);
            SendCommand(COMPort.Port, CPSCommand.Finish, 4);
            COMData.mode = DataMode.WriteFlash;
            COMData.localAddress = 0;
            COMData.flashAddress = CALIBRATIONS_ADDRESS;
            COMData.transferLength = CALIBRATION_TABLE_SIZE;
            if (!WriteFlash(COMPort.Port, COMData, CALIBRATION_TABLE_SIZE))
            {
                MessageBox.Show("Ошибка при записи в последовательный порт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                COMData.responseCode = 1;
            }
            SendCommand(COMPort.Port, CPSCommand.Finish, 2);
            SendCommand(COMPort.Port, CPSCommand.Finish, 1);
            SendCommand(COMPort.Port, CPSCommand.CloseUI);
            COMPort.Port.Close();
            COMPort.Port = null;
        }


        public static byte[] DataToByte(CalibrationData calData)
        {
        
            int num = Marshal.SizeOf(typeof(CalibrationData));
            byte[] array = new byte[num];
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            Marshal.StructureToPtr(calData, intPtr, fDeleteOld: false);
            Marshal.Copy(intPtr, array, 0, num);
            Marshal.FreeHGlobal(intPtr);
            return array;
        }

        private void onFormLoad(object sender, EventArgs e)
        {

        }

        private string unknownElements = "";
        private bool hasUnknownElements = false;

        private void unknownElementEvent(object sender, XmlElementEventArgs e)
        {
            hasUnknownElements = true;
            unknownElements += ("\r\n" + e.Element.Name + ": " + e.Element.InnerXml);
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            string profileStringWithDefault = RegistryOperations.GetString("LastFilePath", "");
            string initialDirectory;
            try
            {
                initialDirectory = ((!(profileStringWithDefault == "")) ? Path.GetDirectoryName(profileStringWithDefault) : Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            }
            catch (Exception)
            {
                initialDirectory = "";
            }
        
            saveFileDialog.InitialDirectory = initialDirectory;
            if (openFileDialog.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(openFileDialog.FileName))
            {
                return;
            }
            try
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    xmlSerializer.UnknownElement += new XmlElementEventHandler(unknownElementEvent);
                    CalData = xmlSerializer.Deserialize(fs) as CalibrationData;
                    if (hasUnknownElements)
                    {
                        hasUnknownElements = false;
                        SystemSounds.Exclamation.Play();
                        MessageBox.Show("В блоке калибровок обнаружены неподдерживаемые поля.\r\nПоля, которые не могут быть распознаны:" + unknownElements, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        unknownElements = "";
                    }
                    else
                        showButtons();
                    buildVariablesFromCalData(CalData);
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть файл " + saveFileDialog.FileName + "\r\n" + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnReadFromRadio_Click(object sender, EventArgs e)
        {
            radioBandlimits = readBandlimits();
            if (radioBandlimits == null)
            {
                MessageBox.Show("Ограничения частот не считаны из рации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (readDataFromRadio())
            {
                showButtons();
            }
        

        }

        private void btnRestoreFactory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Радиостанция восстановит таблицу калибровок, исходя из заводских, хранящихся в защищенной области памяти. Все внесенные изменения будут сброшены.\r\nПоскольку заводская калибровка каждой конкретной рации де-факто на заводе не выполняется, расчетные значения для малых мощностей, опираясь на зашитые заводом данные, могут быть крайне неточными.\r\n\r\nВыполнить сброс?", "ВНИМАНИЕ!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (!COMPort.SetupPort())
                {
                    SystemSounds.Hand.Play();
                    MessageBox.Show("Отсутствует заданный COM-порт");
                    return;
                }
                SendCommand(COMPort.Port, CPSCommand.RestoreCalibrations);
                COMPort.Port.Close();
                COMPort.Port = null;
            }
        }

        private void showButtons()
        {
            btnWrite.Visible = true;
            btnSaveCalibration.Visible = true;
            btnChart.Visible = true;
            btnRestoreFactory.Visible = true;
        }

        private void btnSaveCalibration_Click(object sender, EventArgs e)
        {
            string profileStringWithDefault = RegistryOperations.GetString("LastFilePath", "");
            string initialDirectory;
            string radioType = "MD-9600_RT-90";

            try
            {
                initialDirectory = ((!(profileStringWithDefault == "")) ? Path.GetDirectoryName(profileStringWithDefault) : Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            }
            catch (Exception)
            {
                initialDirectory = "";
            }
                 
            saveFileDialog.FileName = "Калибровки_" + radioType + "_" + DateTime.Now.ToString("MMdd_HHmmss");
            saveFileDialog.InitialDirectory = initialDirectory;
            if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                buildCalDataFromVariables(CalData);
                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        xmlSerializer.Serialize(fs, CalData);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить файл " + saveFileDialog.FileName + "\r\n" + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    
        private TextBox[] frequenciesTxVHF = new TextBox[5];
        private NumericUpDown[] power8VHF = new NumericUpDown[5];
        private NumericUpDown[] power7VHF = new NumericUpDown[5];
        private NumericUpDown[] power6VHF = new NumericUpDown[5];
        private NumericUpDown[] power5VHF = new NumericUpDown[5];
        private NumericUpDown[] power4VHF = new NumericUpDown[5];
        private NumericUpDown[] power3VHF = new NumericUpDown[5];
        private NumericUpDown[] power2VHF = new NumericUpDown[5];
        private NumericUpDown[] power1VHF = new NumericUpDown[5];
        private NumericUpDown[] power0VHF = new NumericUpDown[5];
        private NumericUpDown[] rxTuningVHF = new NumericUpDown[5];
        private NumericUpDown[] iGainDMRVHF = new NumericUpDown[5];
        private NumericUpDown[] qGainDMRVHF = new NumericUpDown[5];
        private NumericUpDown[] iGainFMVHF = new NumericUpDown[5];
        private NumericUpDown[] qGainFMVHF = new NumericUpDown[5];
    
        private TextBox[] frequenciesTxUHF = new TextBox[9];
        private NumericUpDown[] power8UHF = new NumericUpDown[9];
        private NumericUpDown[] power7UHF = new NumericUpDown[9];
        private NumericUpDown[] power6UHF = new NumericUpDown[9];
        private NumericUpDown[] power5UHF = new NumericUpDown[9];
        private NumericUpDown[] power4UHF = new NumericUpDown[9];
        private NumericUpDown[] power3UHF = new NumericUpDown[9];
        private NumericUpDown[] power2UHF = new NumericUpDown[9];
        private NumericUpDown[] power1UHF = new NumericUpDown[9];
        private NumericUpDown[] power0UHF = new NumericUpDown[9];
        private NumericUpDown[] rxTuningUHF = new NumericUpDown[9];
        private NumericUpDown[] iGainDMRUHF = new NumericUpDown[9];
        private NumericUpDown[] qGainDMRUHF = new NumericUpDown[9];
        private NumericUpDown[] iGainFMUHF = new NumericUpDown[9];
        private NumericUpDown[] qGainFMUHF = new NumericUpDown[9];

        private Font commonFont = null;
        private Font selectionFont = null;
        private void prepareTables()
        {
            this.SuspendLayout();
            tlpVHF.SuspendLayout();
            tlpUHF.SuspendLayout();
            Padding margin = new Padding(0);

            for (int i = 0; i < 5; i++)
            {
                frequenciesTxVHF[i] = new TextBox();
                frequenciesTxVHF[i].Width = 74;
                frequenciesTxVHF[i].Height = 20;
                frequenciesTxVHF[i].Margin = margin;
                frequenciesTxVHF[i].ReadOnly = true;
                var powerControls = new[] { power0VHF, power1VHF, power2VHF, power3VHF,
                           power4VHF, power5VHF, power6VHF, power7VHF, power8VHF };
                for (int j = 0; j < 9; j++)
                {
                    powerControls[j][i] = new NumericUpDown();
                    powerControls[j][i].Width = 74;
                    powerControls[j][i].Increment = 1;
                    powerControls[j][i].Height = 20;
                    powerControls[j][i].Margin = margin;
                    powerControls[j][i].Minimum = 0;
                    powerControls[j][i].Maximum = 4095;
                    powerControls[j][i].ValueChanged += new EventHandler(nmValueChanged);
                }
                rxTuningVHF[i] = new NumericUpDown();
                rxTuningVHF[i].Width = 74;
                rxTuningVHF[i].Height = 20;
                rxTuningVHF[i].Margin = margin;
                rxTuningVHF[i].Minimum = 0;
                rxTuningVHF[i].Maximum = 255;
                rxTuningVHF[i].ValueChanged += new EventHandler(nmValueChanged);
                iGainDMRVHF[i] = new NumericUpDown();
                iGainDMRVHF[i].Width = 74;
                iGainDMRVHF[i].Height = 20;
                iGainDMRVHF[i].Margin = margin;
                iGainDMRVHF[i].Minimum = 0;
                iGainDMRVHF[i].Maximum = 255;
                iGainDMRVHF[i].ValueChanged += new EventHandler(nmValueChanged);
                qGainDMRVHF[i] = new NumericUpDown();
                qGainDMRVHF[i].Width = 74;
                qGainDMRVHF[i].Height = 20;
                qGainDMRVHF[i].Margin = margin;
                qGainDMRVHF[i].Minimum = 0;
                qGainDMRVHF[i].Maximum = 255;
                qGainDMRVHF[i].ValueChanged += new EventHandler(nmValueChanged);
                iGainFMVHF[i] = new NumericUpDown();
                iGainFMVHF[i].Width = 74;
                iGainFMVHF[i].Height = 20;
                iGainFMVHF[i].Margin = margin;
                iGainFMVHF[i].Minimum = 0;
                iGainFMVHF[i].Maximum = 255;
                iGainFMVHF[i].ValueChanged += new EventHandler(nmValueChanged);
                qGainFMVHF[i] = new NumericUpDown();
                qGainFMVHF[i].Width = 74;
                qGainFMVHF[i].Height = 20;
                qGainFMVHF[i].Margin = margin;
                qGainFMVHF[i].Minimum = 0;
                qGainFMVHF[i].Maximum = 255;
                qGainFMVHF[i].ValueChanged += new EventHandler(nmValueChanged);
            
                tlpVHF.Controls.Add(frequenciesTxVHF[i], i + 1, 0);
                tlpVHF.Controls.Add(power8VHF[i], i + 1, 1);
                tlpVHF.Controls.Add(power7VHF[i], i + 1, 2);
                tlpVHF.Controls.Add(power6VHF[i], i + 1, 3);
                tlpVHF.Controls.Add(power5VHF[i], i + 1, 4);
                tlpVHF.Controls.Add(power4VHF[i], i + 1, 5);
                tlpVHF.Controls.Add(power3VHF[i], i + 1, 6);
                tlpVHF.Controls.Add(power2VHF[i], i + 1, 7);
                tlpVHF.Controls.Add(power1VHF[i], i + 1, 8);
                tlpVHF.Controls.Add(power0VHF[i], i + 1, 9);
                tlpVHF.Controls.Add(rxTuningVHF[i], i + 1, 10);
                tlpVHF.Controls.Add(iGainDMRVHF[i], i + 1, 11);
                tlpVHF.Controls.Add(qGainDMRVHF[i], i + 1, 12);
                tlpVHF.Controls.Add(iGainFMVHF[i], i + 1, 13);
                tlpVHF.Controls.Add(qGainFMVHF[i], i + 1, 14);
            }
            for (int i = 0; i < 9; i++)
            {
                frequenciesTxUHF[i] = new TextBox();
                frequenciesTxUHF[i].Width = 74;
                frequenciesTxUHF[i].Height = 20;
                frequenciesTxUHF[i].Margin = margin;
                frequenciesTxUHF[i].ReadOnly = true;
                var powerControls = new[] { power0UHF, power1UHF, power2UHF, power3UHF,
           power4UHF, power5UHF, power6UHF, power7UHF, power8UHF };
                for (int j = 0; j < 9; j++)
                {
                    powerControls[j][i] = new NumericUpDown();
                    powerControls[j][i].Width = 74;
                    powerControls[j][i].Increment = 1;
                    powerControls[j][i].Height = 20;
                    powerControls[j][i].Margin = margin;
                    powerControls[j][i].Minimum = 0;
                    powerControls[j][i].Maximum = 4095;
                    powerControls[j][i].ValueChanged += new EventHandler(nmValueChanged);
                }
                rxTuningUHF[i] = new NumericUpDown();
                rxTuningUHF[i].Width = 74;
                rxTuningUHF[i].Height = 20;
                rxTuningUHF[i].Margin = margin;
                rxTuningUHF[i].Minimum = 0;
                rxTuningUHF[i].Maximum = 255;
                rxTuningUHF[i].ValueChanged += new EventHandler(nmValueChanged);
                iGainDMRUHF[i] = new NumericUpDown();
                iGainDMRUHF[i].Width = 74;
                iGainDMRUHF[i].Height = 20;
                iGainDMRUHF[i].Margin = margin;
                iGainDMRUHF[i].Minimum = 0;
                iGainDMRUHF[i].Maximum = 255;
                iGainDMRUHF[i].ValueChanged += new EventHandler(nmValueChanged);
                qGainDMRUHF[i] = new NumericUpDown();
                qGainDMRUHF[i].Width = 74;
                qGainDMRUHF[i].Height = 20;
                qGainDMRUHF[i].Margin = margin;
                qGainDMRUHF[i].Minimum = 0;
                qGainDMRUHF[i].Maximum = 255;
                qGainDMRUHF[i].ValueChanged += new EventHandler(nmValueChanged);
                iGainFMUHF[i] = new NumericUpDown();
                iGainFMUHF[i].Width = 74;
                iGainFMUHF[i].Height = 20;
                iGainFMUHF[i].Margin = margin;
                iGainFMUHF[i].Minimum = 0;
                iGainFMUHF[i].Maximum = 255;
                iGainFMUHF[i].ValueChanged += new EventHandler(nmValueChanged);
                qGainFMUHF[i] = new NumericUpDown();
                qGainFMUHF[i].Width = 74;
                qGainFMUHF[i].Height = 20;
                qGainFMUHF[i].Margin = margin;
                qGainFMUHF[i].Minimum = 0;
                qGainFMUHF[i].Maximum = 255;
                qGainFMUHF[i].ValueChanged += new EventHandler(nmValueChanged);
            
                tlpUHF.Controls.Add(frequenciesTxUHF[i], i + 1, 0);
                tlpUHF.Controls.Add(power8UHF[i], i + 1, 1);
                tlpUHF.Controls.Add(power7UHF[i], i + 1, 2);
                tlpUHF.Controls.Add(power6UHF[i], i + 1, 3);
                tlpUHF.Controls.Add(power5UHF[i], i + 1, 4);
                tlpUHF.Controls.Add(power4UHF[i], i + 1, 5);
                tlpUHF.Controls.Add(power3UHF[i], i + 1, 6);
                tlpUHF.Controls.Add(power2UHF[i], i + 1, 7);
                tlpUHF.Controls.Add(power1UHF[i], i + 1, 8);
                tlpUHF.Controls.Add(power0UHF[i], i + 1, 9);
                tlpUHF.Controls.Add(rxTuningUHF[i], i + 1, 10);
                tlpUHF.Controls.Add(iGainDMRUHF[i], i + 1, 11);
                tlpUHF.Controls.Add(qGainDMRUHF[i], i + 1, 12);
                tlpUHF.Controls.Add(iGainFMUHF[i], i + 1, 13);
                tlpUHF.Controls.Add(qGainFMUHF[i], i + 1, 14);
            }

            foreach (var item in tlpVHF.Controls.OfType<NumericUpDown>()
                .Concat(tlpUHF.Controls.OfType<NumericUpDown>()))
            {
                item.Click += nmClick;
            }

            commonFont = new Font(power8UHF[0].Font.Name, power8UHF[0].Font.Size, FontStyle.Regular);
            selectionFont = new Font(power8UHF[0].Font.Name, power8UHF[0].Font.Size, FontStyle.Bold);
            tlpVHF.ResumeLayout(false);
            tlpUHF.ResumeLayout(false);
            this.ResumeLayout(true);
        }

        private bool isReading = false;

        private void nmValueChanged(Object sender, EventArgs e)
        {
             buildCalDataFromVariables(CalData);
        }


        private NumericUpDown prevSelected = null;
        private void nmClick(Object sender, EventArgs e)
        {
            if (prevSelected != null)
            {
                prevSelected.Font = commonFont;
                prevSelected.BackColor = Color.LightSkyBlue;
                prevSelected.ForeColor = Color.Black;
            }
            NumericUpDown temp = sender as NumericUpDown;
            prevSelected = temp;
            temp.Font = selectionFont;
            temp.BackColor = Color.Red;
            temp.ForeColor = Color.White;
        }

        private void btnClearColors_Click(object sender, EventArgs e)
        {
            foreach (var item in tlpVHF.Controls)
            {
                if (item is NumericUpDown)
                {
                    ((NumericUpDown)item).Font = commonFont;
                    ((NumericUpDown)item).BackColor = Color.White;
                    ((NumericUpDown)item).ForeColor = Color.Black;
                }
            }
            foreach (var item in tlpUHF.Controls)
            {
                if (item is NumericUpDown)
                {
                    ((NumericUpDown)item).Font = commonFont;
                    ((NumericUpDown)item).BackColor = Color.White;
                    ((NumericUpDown)item).ForeColor = Color.Black;
                }
            }

            prevSelected = null;
        }

        private void buildVariablesFromCalData(CalibrationData c)
        {
            lblRadioType.Text = "Контрольная сумма: 0x";

            lblRadioType.Text += c.checksum.ToString("X8");
            isReading = true;
            nmRSSI120.Value = c.RSSI120;
            nmRSSI70.Value = c.RSSI70;
            nmVHFOscRef.Value = c.OscRefTuneVHF;
            nmUHFOscRef.Value = c.OscRefTuneVHF;
            nmDevFMNVHF.Value = c.DevFMNVHF;
            nmDevFMVHF.Value = c.DevFMVHF;
            nmDevFMNUHF.Value = c.DevFMNUHF;
            nmDevFMUHF.Value = c.DevFMUHF;
            //tlpVHF
            for (int i = 0; i < 5; i++)
            {

                frequenciesTxVHF[i].Text = ((radioBandlimits.VHFLowCal + (i * 950000)) / 100000.0f).ToString("N3");
                var powerControls = new[] { power0VHF, power1VHF, power2VHF, power3VHF,
                           power4VHF, power5VHF, power6VHF, power7VHF, power8VHF };

                for (int j = 0; j < 9; j++)
                {
                    powerControls[j][i].Value = c.GetPowerVHF(j, i);
                }
                rxTuningVHF[i].Value = c.RxTuneVHF[i];
                iGainDMRVHF[i].Value = c.IGainDMRVHF[i];
                qGainDMRVHF[i].Value = c.QGainDMRVHF[i];
                iGainFMVHF[i].Value = c.IGainVHF[i];
                qGainFMVHF[i].Value = c.QGainVHF[i];
            }
            for (int i = 0; i < 9; i++)
            {

                frequenciesTxUHF[i].Text = ((radioBandlimits.UHFLowCal + (i * 1000000)) / 100000.0f).ToString("N3");
                var powerControls = new[] { power0UHF, power1UHF, power2UHF, power3UHF,
                           power4UHF, power5UHF, power6UHF, power7UHF, power8UHF };

                for (int j = 0; j < 9; j++)
                {
                    powerControls[j][i].Value = c.GetPowerUHF(j, i);
                }
                rxTuningUHF[i].Value = c.RxTuneUHF[i];
                iGainDMRUHF[i].Value = c.IGainDMRUHF[i];
                qGainDMRUHF[i].Value = c.QGainDMRUHF[i];
                iGainFMUHF[i].Value = c.IGainUHF[i];
                qGainFMUHF[i].Value = c.QGainUHF[i];
            }
            isReading = false;
        }

        private void buildCalDataFromVariables(CalibrationData c)
        {
            if (!isReading)
            {
                c.RSSI120 = (ushort)(nmRSSI120.Value);
                c.RSSI70 = (ushort)(nmRSSI70.Value);
                c.OscRefTuneVHF = (byte)nmVHFOscRef.Value;
                c.OscRefTuneUHF = (byte)nmUHFOscRef.Value;
                c.DevFMNVHF = (byte)nmDevFMNVHF.Value;
                c.DevFMVHF = (byte)nmDevFMVHF.Value;
                c.DevFMNUHF = (byte)nmDevFMNUHF.Value;
                c.DevFMUHF = (byte)nmDevFMUHF.Value;
                for (int i = 0; i < 5; i++)
                {
                    var powerControls = new[] { power0VHF, power1VHF, power2VHF, power3VHF,
                           power4VHF, power5VHF, power6VHF, power7VHF, power8VHF };
                    for (int j = 0; j < 9; j++)
                    {
                        c.SetPowerVHF(j, i, (ushort)powerControls[j][i].Value);
                    }
                    c.RxTuneVHF[i] = (byte)rxTuningVHF[i].Value;
                    c.IGainDMRVHF[i] = (byte)iGainDMRVHF[i].Value;
                    c.QGainDMRVHF[i] = (byte)qGainDMRVHF[i].Value;
                    c.IGainVHF[i] = (byte)iGainFMVHF[i].Value;
                    c.QGainVHF[i] = (byte)qGainFMVHF[i].Value;
                }
                for (int i = 0; i < 9; i++)
                {
                    var powerControls = new[] { power0UHF, power1UHF, power2UHF, power3UHF,
                           power4UHF, power5UHF, power6UHF, power7UHF, power8UHF };
                    for (int j = 0; j < 9; j++)
                    {
                        c.SetPowerUHF(j, i, (ushort)powerControls[j][i].Value);
                    }
                    c.RxTuneUHF[i] = (byte)rxTuningUHF[i].Value;
                    c.IGainDMRUHF[i] = (byte)iGainDMRUHF[i].Value;
                    c.QGainDMRUHF[i] = (byte)qGainDMRUHF[i].Value;
                    c.IGainUHF[i] = (byte)iGainFMUHF[i].Value;
                    c.QGainUHF[i] = (byte)qGainFMUHF[i].Value;
                }
                c.checksum = 0xDEFECA7E;
            }
        }

        public static string getVHFFreq(int i)
        {
            return ((radioBandlimits.VHFLowCal + (i * 950000)) / 100000.0f).ToString("N3") + "МГц";
        }

        public static string getUHFFreq(int i)
        {
            return ((radioBandlimits.UHFLowCal + (i * 1000000)) / 100000.0f).ToString("N3") + "МГц";
        }

        public static CalibrationData getActualCals()
        {
            return CalData;
        }

        private CalibrationCharts chartsForm = null;

        private void btnChart_Click(object sender, EventArgs e)
        {
            if (chartsForm == null || chartsForm.IsDisposed)
            {
                // Форма не создана или была закрыта - создаем новую
                chartsForm = new CalibrationCharts();
                chartsForm.Show();

                // Подписываемся на закрытие, чтобы обнулить ссылку
                chartsForm.FormClosed += (s, args) => chartsForm = null;
            }
            else
            {
                // Форма уже существует - активируем её
                chartsForm.Activate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnReadFromRadio = new System.Windows.Forms.Button();
            this.btnSaveCalibration = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabVHF = new System.Windows.Forms.TabPage();
            this.nmDevFMNVHF = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nmDevFMVHF = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nmVHFOscRef = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.tlpVHF = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.tabUHF = new System.Windows.Forms.TabPage();
            this.nmDevFMNUHF = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nmDevFMUHF = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nmUHFOscRef = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.tlpUHF = new System.Windows.Forms.TableLayoutPanel();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.gbCommons = new System.Windows.Forms.GroupBox();
            this.nmRSSI70 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nmRSSI120 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRestoreFactory = new System.Windows.Forms.Button();
            this.lblRadioType = new System.Windows.Forms.Label();
            this.btnClearColors = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tabVHF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMNVHF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMVHF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmVHFOscRef)).BeginInit();
            this.tlpVHF.SuspendLayout();
            this.tabUHF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMNUHF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMUHF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUHFOscRef)).BeginInit();
            this.tlpUHF.SuspendLayout();
            this.gbCommons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmRSSI70)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRSSI120)).BeginInit();
            this.SuspendLayout();
            // 
            // btnWrite
            // 
            this.btnWrite.BackColor = System.Drawing.Color.White;
            this.btnWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnWrite.Location = new System.Drawing.Point(877, 297);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(268, 25);
            this.btnWrite.TabIndex = 1;
            this.btnWrite.Text = "Записать в рацию";
            this.btnWrite.UseVisualStyleBackColor = false;
            this.btnWrite.Visible = false;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.BackColor = System.Drawing.Color.White;
            this.btnReadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReadFile.Location = new System.Drawing.Point(877, 262);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(268, 25);
            this.btnReadFile.TabIndex = 1;
            this.btnReadFile.Text = "Открыть файл калибровок";
            this.btnReadFile.UseVisualStyleBackColor = false;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnReadFromRadio
            // 
            this.btnReadFromRadio.BackColor = System.Drawing.Color.White;
            this.btnReadFromRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReadFromRadio.Location = new System.Drawing.Point(877, 227);
            this.btnReadFromRadio.Name = "btnReadFromRadio";
            this.btnReadFromRadio.Size = new System.Drawing.Size(268, 25);
            this.btnReadFromRadio.TabIndex = 1;
            this.btnReadFromRadio.Text = "Считать калибровки из рации";
            this.btnReadFromRadio.UseVisualStyleBackColor = false;
            this.btnReadFromRadio.Click += new System.EventHandler(this.btnReadFromRadio_Click);
            // 
            // btnSaveCalibration
            // 
            this.btnSaveCalibration.BackColor = System.Drawing.Color.White;
            this.btnSaveCalibration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveCalibration.Location = new System.Drawing.Point(877, 332);
            this.btnSaveCalibration.Name = "btnSaveCalibration";
            this.btnSaveCalibration.Size = new System.Drawing.Size(268, 25);
            this.btnSaveCalibration.TabIndex = 1;
            this.btnSaveCalibration.Text = "Сохранить файл калибровок";
            this.btnSaveCalibration.UseVisualStyleBackColor = false;
            this.btnSaveCalibration.Visible = false;
            this.btnSaveCalibration.Click += new System.EventHandler(this.btnSaveCalibration_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Файлы калибровок|*.ncal";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Файлы калибровки|*.ncal";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabVHF);
            this.tabs.Controls.Add(this.tabUHF);
            this.tabs.Location = new System.Drawing.Point(14, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(857, 422);
            this.tabs.TabIndex = 2;
            // 
            // tabVHF
            // 
            this.tabVHF.BackColor = System.Drawing.Color.White;
            this.tabVHF.Controls.Add(this.nmDevFMNVHF);
            this.tabVHF.Controls.Add(this.label2);
            this.tabVHF.Controls.Add(this.nmDevFMVHF);
            this.tabVHF.Controls.Add(this.label1);
            this.tabVHF.Controls.Add(this.nmVHFOscRef);
            this.tabVHF.Controls.Add(this.label34);
            this.tabVHF.Controls.Add(this.tlpVHF);
            this.tabVHF.Location = new System.Drawing.Point(4, 22);
            this.tabVHF.Name = "tabVHF";
            this.tabVHF.Padding = new System.Windows.Forms.Padding(3);
            this.tabVHF.Size = new System.Drawing.Size(849, 396);
            this.tabVHF.TabIndex = 0;
            this.tabVHF.Text = "Диапазон 2 м";
            // 
            // nmDevFMNVHF
            // 
            this.nmDevFMNVHF.Location = new System.Drawing.Point(590, 364);
            this.nmDevFMNVHF.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmDevFMNVHF.Name = "nmDevFMNVHF";
            this.nmDevFMNVHF.Size = new System.Drawing.Size(52, 20);
            this.nmDevFMNVHF.TabIndex = 13;
            this.nmDevFMNVHF.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(500, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Девиация FMN";
            // 
            // nmDevFMVHF
            // 
            this.nmDevFMVHF.Location = new System.Drawing.Point(432, 364);
            this.nmDevFMVHF.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmDevFMVHF.Name = "nmDevFMVHF";
            this.nmDevFMVHF.Size = new System.Drawing.Size(52, 20);
            this.nmDevFMVHF.TabIndex = 11;
            this.nmDevFMVHF.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Девиация FM";
            // 
            // nmVHFOscRef
            // 
            this.nmVHFOscRef.Location = new System.Drawing.Point(282, 364);
            this.nmVHFOscRef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmVHFOscRef.Name = "nmVHFOscRef";
            this.nmVHFOscRef.Size = new System.Drawing.Size(52, 20);
            this.nmVHFOscRef.TabIndex = 9;
            this.nmVHFOscRef.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(158, 368);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(112, 13);
            this.label34.TabIndex = 8;
            this.label34.Text = "Подстройка частоты";
            // 
            // tlpVHF
            // 
            this.tlpVHF.BackColor = System.Drawing.SystemColors.Window;
            this.tlpVHF.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpVHF.ColumnCount = 6;
            this.tlpVHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpVHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpVHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpVHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpVHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpVHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tlpVHF.Controls.Add(this.label8, 0, 0);
            this.tlpVHF.Controls.Add(this.label9, 0, 1);
            this.tlpVHF.Controls.Add(this.label10, 0, 2);
            this.tlpVHF.Controls.Add(this.label11, 0, 3);
            this.tlpVHF.Controls.Add(this.label12, 0, 4);
            this.tlpVHF.Controls.Add(this.label16, 0, 10);
            this.tlpVHF.Controls.Add(this.label30, 0, 11);
            this.tlpVHF.Controls.Add(this.label31, 0, 12);
            this.tlpVHF.Controls.Add(this.label32, 0, 13);
            this.tlpVHF.Controls.Add(this.label33, 0, 14);
            this.tlpVHF.Controls.Add(this.label45, 0, 5);
            this.tlpVHF.Controls.Add(this.label46, 0, 6);
            this.tlpVHF.Controls.Add(this.label47, 0, 7);
            this.tlpVHF.Controls.Add(this.label48, 0, 8);
            this.tlpVHF.Controls.Add(this.label49, 0, 9);
            this.tlpVHF.Location = new System.Drawing.Point(157, 29);
            this.tlpVHF.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVHF.Name = "tlpVHF";
            this.tlpVHF.RowCount = 16;
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVHF.Size = new System.Drawing.Size(526, 319);
            this.tlpVHF.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(1, 1);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Частота Tx";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(4, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Мощность 40 Вт";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(4, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Мощность 25 Вт";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(4, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "Мощность 10 Вт";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(4, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 20);
            this.label12.TabIndex = 5;
            this.label12.Text = "Мощность 5 Вт";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(4, 211);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(144, 20);
            this.label16.TabIndex = 6;
            this.label16.Text = "Настройка Rx";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Location = new System.Drawing.Point(4, 232);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(144, 20);
            this.label30.TabIndex = 15;
            this.label30.Text = "Усиление I (DMR)";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Location = new System.Drawing.Point(4, 253);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(144, 20);
            this.label31.TabIndex = 16;
            this.label31.Text = "Усиление Q (DMR)";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Location = new System.Drawing.Point(4, 274);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(144, 20);
            this.label32.TabIndex = 17;
            this.label32.Text = "Усиление I (FM)";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label33
            // 
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Location = new System.Drawing.Point(4, 295);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(144, 20);
            this.label33.TabIndex = 18;
            this.label33.Text = "Усиление Q (FM)";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Location = new System.Drawing.Point(4, 106);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(144, 20);
            this.label45.TabIndex = 19;
            this.label45.Text = "Мощность 1 Вт";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label46.Location = new System.Drawing.Point(4, 127);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(144, 20);
            this.label46.TabIndex = 20;
            this.label46.Text = "Мощность 750 мВт";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label47.Location = new System.Drawing.Point(4, 148);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(144, 20);
            this.label47.TabIndex = 21;
            this.label47.Text = "Мощность 500 мВт";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label48.Location = new System.Drawing.Point(4, 169);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(144, 20);
            this.label48.TabIndex = 22;
            this.label48.Text = "Мощность 250 мВт";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label49.Location = new System.Drawing.Point(4, 190);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(144, 20);
            this.label49.TabIndex = 23;
            this.label49.Text = "Мощность 100 мВт";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabUHF
            // 
            this.tabUHF.BackColor = System.Drawing.Color.White;
            this.tabUHF.Controls.Add(this.nmDevFMNUHF);
            this.tabUHF.Controls.Add(this.label4);
            this.tabUHF.Controls.Add(this.nmDevFMUHF);
            this.tabUHF.Controls.Add(this.label3);
            this.tabUHF.Controls.Add(this.nmUHFOscRef);
            this.tabUHF.Controls.Add(this.label35);
            this.tabUHF.Controls.Add(this.tlpUHF);
            this.tabUHF.Location = new System.Drawing.Point(4, 22);
            this.tabUHF.Name = "tabUHF";
            this.tabUHF.Padding = new System.Windows.Forms.Padding(3);
            this.tabUHF.Size = new System.Drawing.Size(849, 396);
            this.tabUHF.TabIndex = 1;
            this.tabUHF.Text = "Диапазон 70 см";
            // 
            // nmDevFMNUHF
            // 
            this.nmDevFMNUHF.Location = new System.Drawing.Point(458, 343);
            this.nmDevFMNUHF.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmDevFMNUHF.Name = "nmDevFMNUHF";
            this.nmDevFMNUHF.Size = new System.Drawing.Size(52, 20);
            this.nmDevFMNUHF.TabIndex = 20;
            this.nmDevFMNUHF.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(368, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Девиация FMN";
            // 
            // nmDevFMUHF
            // 
            this.nmDevFMUHF.Location = new System.Drawing.Point(293, 343);
            this.nmDevFMUHF.Name = "nmDevFMUHF";
            this.nmDevFMUHF.Size = new System.Drawing.Size(52, 20);
            this.nmDevFMUHF.TabIndex = 18;
            this.nmDevFMUHF.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 347);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Девиация FM";
            // 
            // nmUHFOscRef
            // 
            this.nmUHFOscRef.Location = new System.Drawing.Point(140, 343);
            this.nmUHFOscRef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmUHFOscRef.Name = "nmUHFOscRef";
            this.nmUHFOscRef.Size = new System.Drawing.Size(52, 20);
            this.nmUHFOscRef.TabIndex = 16;
            this.nmUHFOscRef.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(16, 347);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(112, 13);
            this.label35.TabIndex = 15;
            this.label35.Text = "Подстройка частоты";
            // 
            // tlpUHF
            // 
            this.tlpUHF.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpUHF.ColumnCount = 10;
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpUHF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tlpUHF.Controls.Add(this.label40, 0, 0);
            this.tlpUHF.Controls.Add(this.label41, 0, 1);
            this.tlpUHF.Controls.Add(this.label42, 0, 2);
            this.tlpUHF.Controls.Add(this.label43, 0, 3);
            this.tlpUHF.Controls.Add(this.label44, 0, 4);
            this.tlpUHF.Controls.Add(this.label7, 0, 10);
            this.tlpUHF.Controls.Add(this.label36, 0, 11);
            this.tlpUHF.Controls.Add(this.label37, 0, 12);
            this.tlpUHF.Controls.Add(this.label38, 0, 13);
            this.tlpUHF.Controls.Add(this.label39, 0, 14);
            this.tlpUHF.Controls.Add(this.label50, 0, 5);
            this.tlpUHF.Controls.Add(this.label51, 0, 6);
            this.tlpUHF.Controls.Add(this.label52, 0, 7);
            this.tlpUHF.Controls.Add(this.label53, 0, 8);
            this.tlpUHF.Controls.Add(this.label54, 0, 9);
            this.tlpUHF.Location = new System.Drawing.Point(12, 16);
            this.tlpUHF.Name = "tlpUHF";
            this.tlpUHF.RowCount = 16;
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUHF.Size = new System.Drawing.Size(827, 316);
            this.tlpUHF.TabIndex = 0;
            // 
            // label40
            // 
            this.label40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label40.Location = new System.Drawing.Point(4, 1);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(144, 20);
            this.label40.TabIndex = 1;
            this.label40.Text = "Частота Tx";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label41
            // 
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Location = new System.Drawing.Point(4, 22);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(144, 20);
            this.label41.TabIndex = 2;
            this.label41.Text = "Мощность 40 Вт";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Location = new System.Drawing.Point(4, 43);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(144, 20);
            this.label42.TabIndex = 3;
            this.label42.Text = "Мощность 25 Вт";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label43
            // 
            this.label43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label43.Location = new System.Drawing.Point(4, 64);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(144, 20);
            this.label43.TabIndex = 4;
            this.label43.Text = "Мощность 10 Вт";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Location = new System.Drawing.Point(4, 85);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(144, 20);
            this.label44.TabIndex = 5;
            this.label44.Text = "Мощность 5 Вт";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(4, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Настройка Rx";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Location = new System.Drawing.Point(4, 232);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(144, 20);
            this.label36.TabIndex = 18;
            this.label36.Text = "Усиление I (DMR)";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Location = new System.Drawing.Point(4, 253);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(144, 20);
            this.label37.TabIndex = 19;
            this.label37.Text = "Усиление Q (DMR)";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Location = new System.Drawing.Point(4, 274);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(144, 20);
            this.label38.TabIndex = 20;
            this.label38.Text = "Усиление I (FM)";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            this.label39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label39.Location = new System.Drawing.Point(4, 295);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(144, 20);
            this.label39.TabIndex = 21;
            this.label39.Text = "Усиление Q (FM)";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label50.Location = new System.Drawing.Point(4, 106);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(144, 20);
            this.label50.TabIndex = 22;
            this.label50.Text = "Мощность 1 Вт";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label51.Location = new System.Drawing.Point(4, 127);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(144, 20);
            this.label51.TabIndex = 23;
            this.label51.Text = "Мощность 750 мВт";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label52.Location = new System.Drawing.Point(4, 148);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(144, 20);
            this.label52.TabIndex = 24;
            this.label52.Text = "Мощность 500 мВт";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label53.Location = new System.Drawing.Point(4, 169);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(144, 20);
            this.label53.TabIndex = 25;
            this.label53.Text = "Мощность 250 мВт";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label54.Location = new System.Drawing.Point(4, 190);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(144, 20);
            this.label54.TabIndex = 26;
            this.label54.Text = "Мощность 100 мВт";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbCommons
            // 
            this.gbCommons.Controls.Add(this.nmRSSI70);
            this.gbCommons.Controls.Add(this.label6);
            this.gbCommons.Controls.Add(this.nmRSSI120);
            this.gbCommons.Controls.Add(this.label5);
            this.gbCommons.Location = new System.Drawing.Point(877, 12);
            this.gbCommons.Name = "gbCommons";
            this.gbCommons.Size = new System.Drawing.Size(268, 104);
            this.gbCommons.TabIndex = 3;
            this.gbCommons.TabStop = false;
            this.gbCommons.Text = "Общие настройки";
            // 
            // nmRSSI70
            // 
            this.nmRSSI70.Location = new System.Drawing.Point(184, 59);
            this.nmRSSI70.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.nmRSSI70.Name = "nmRSSI70";
            this.nmRSSI70.Size = new System.Drawing.Size(66, 20);
            this.nmRSSI70.TabIndex = 11;
            this.nmRSSI70.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Уровень RSSI -70 дБм:";
            // 
            // nmRSSI120
            // 
            this.nmRSSI120.Location = new System.Drawing.Point(184, 30);
            this.nmRSSI120.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.nmRSSI120.Name = "nmRSSI120";
            this.nmRSSI120.Size = new System.Drawing.Size(67, 20);
            this.nmRSSI120.TabIndex = 9;
            this.nmRSSI120.ValueChanged += new System.EventHandler(this.nmValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Уровень RSSI -120 дБм:";
            // 
            // btnRestoreFactory
            // 
            this.btnRestoreFactory.BackColor = System.Drawing.Color.White;
            this.btnRestoreFactory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestoreFactory.Location = new System.Drawing.Point(877, 367);
            this.btnRestoreFactory.Name = "btnRestoreFactory";
            this.btnRestoreFactory.Size = new System.Drawing.Size(268, 25);
            this.btnRestoreFactory.TabIndex = 5;
            this.btnRestoreFactory.Text = "Восстановить заводские калибровки";
            this.btnRestoreFactory.UseVisualStyleBackColor = false;
            this.btnRestoreFactory.Visible = false;
            this.btnRestoreFactory.Click += new System.EventHandler(this.btnRestoreFactory_Click);
            // 
            // lblRadioType
            // 
            this.lblRadioType.Location = new System.Drawing.Point(559, 5);
            this.lblRadioType.Name = "lblRadioType";
            this.lblRadioType.Size = new System.Drawing.Size(308, 22);
            this.lblRadioType.TabIndex = 9;
            this.lblRadioType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClearColors
            // 
            this.btnClearColors.BackColor = System.Drawing.Color.White;
            this.btnClearColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClearColors.Location = new System.Drawing.Point(877, 402);
            this.btnClearColors.Name = "btnClearColors";
            this.btnClearColors.Size = new System.Drawing.Size(268, 25);
            this.btnClearColors.TabIndex = 10;
            this.btnClearColors.Text = "Сбросить маркеры";
            this.btnClearColors.UseVisualStyleBackColor = false;
            this.btnClearColors.Click += new System.EventHandler(this.btnClearColors_Click);
            // 
            // btnChart
            // 
            this.btnChart.BackColor = System.Drawing.Color.White;
            this.btnChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnChart.Location = new System.Drawing.Point(877, 192);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(268, 25);
            this.btnChart.TabIndex = 11;
            this.btnChart.Text = "Калибровочные кривые";
            this.btnChart.UseVisualStyleBackColor = false;
            this.btnChart.Visible = false;
            this.btnChart.Click += new System.EventHandler(this.btnChart_Click);
            // 
            // CalibrationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1156, 454);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.btnClearColors);
            this.Controls.Add(this.lblRadioType);
            this.Controls.Add(this.btnRestoreFactory);
            this.Controls.Add(this.gbCommons);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.btnReadFromRadio);
            this.Controls.Add(this.btnSaveCalibration);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.btnWrite);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalibrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор калибровок";
            this.Load += new System.EventHandler(this.onFormLoad);
            this.tabs.ResumeLayout(false);
            this.tabVHF.ResumeLayout(false);
            this.tabVHF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMNVHF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMVHF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmVHFOscRef)).EndInit();
            this.tlpVHF.ResumeLayout(false);
            this.tlpVHF.PerformLayout();
            this.tabUHF.ResumeLayout(false);
            this.tabUHF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMNUHF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDevFMUHF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUHFOscRef)).EndInit();
            this.tlpUHF.ResumeLayout(false);
            this.tlpUHF.PerformLayout();
            this.gbCommons.ResumeLayout(false);
            this.gbCommons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmRSSI70)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRSSI120)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
