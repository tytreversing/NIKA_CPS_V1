using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;



namespace NIKA_CPS_V1
{
    public class CalibrationForm : Form
    {
        public const int MAX_TRANSFER_BUFFER_SIZE = 1032;

        public static int MEMORY_LOCATION_STM32 = 65536;

        public static int CALIBRATION_MEMORY_LOCATION_OFFICIAL_USB_PROTOCOL = 31744;

        public static int CALIBRATION_TABLE_SIZE = 0x14C;

        private char writeCommandCharacter = 'W';

        public static byte[] dataBuffer;

        private IContainer components;

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
        private Button btnReadFactoryFromRadio;
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
        CalibrationData CalData = new CalibrationData();

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

        private RadioBandlimits radioBandlimits = new();

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

        private static bool ReadRadioBandlimits(SerialPort port, DataTransfer dataObj)
        {
            byte[] array = new byte[1032];
            byte[] array2 = new byte[1032];
            int num = 0;
            array[0] = 82;
            array[1] = (byte)dataObj.mode;
            array[2] = 0;
            array[3] = 0;
            array[4] = 0;
            array[5] = 0;
            array[6] = 0;
            array[7] = 0;
            port.Write(array, 0, 8);
            while (port.BytesToWrite > 0)
            {
                Thread.Sleep(1);
            }
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(5);
            }
            port.Read(array2, 0, port.BytesToRead);
            if (array2[0] == 82)
            {
                int num2 = (array2[1] << 8) + array2[2];
                for (int i = 0; i < num2; i++)
                {
                    dataObj.dataBuffer[num++] = array2[i + 3];
                }
                return true;
            }
            return false;
        }
        private bool readBandlimits()
        {
            if (!COMPort.SetupPort())
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Нет соединения с портом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            DataTransfer COMData = new DataTransfer();
            COMData.mode = DataTransfer.DataMode.DataModeReadBandlimits;
            COMData.localAddress = 0;
            COMData.transferLength = 0;
            COMData.dataBuffer = new byte[128];
            radioBandlimits = default(RadioBandlimits);
            if (ReadRadioBandlimits(COMPort.Port, COMData))
            {
                radioBandlimits = ByteArrayToRadioBandlimits(COMData.dataBuffer);
            
            }
            COMPort.Port.Close();
            COMPort.Port = null;
        
            return true;

        }

        private bool sendCommand(SerialPort port, int commandNumber, int x_or_command_option_number = 0, int y = 0, int iSize = 0, int alignment = 0, int isInverted = 0, string message = "")
        {
            byte[] array = new byte[64];
            array[0] = 67;
            array[1] = (byte)commandNumber;
            switch (commandNumber)
            {
                case 2:
                    array[3] = (byte)y;
                    array[4] = (byte)iSize;
                    array[5] = (byte)alignment;
                    array[6] = (byte)isInverted;
                    Encoding inEncoding = Encoding.Unicode;
                    Encoding outEncoding = Encoding.GetEncoding(1251);
                    byte[] sourceBuffer = inEncoding.GetBytes(message);
                    byte[] destBuffer = Encoding.Convert(inEncoding, outEncoding, sourceBuffer);
                    Buffer.BlockCopy(destBuffer, 0, array, 7, Math.Min(message.Length, 16));
                    break;
                case 6:
                    array[2] = (byte)x_or_command_option_number;
                    break;
            }
            COMPort.Port.Write(array, 0, 32);
            while (COMPort.Port.BytesToRead == 0)
            {
                Thread.Sleep(0);
            }
            COMPort.Port.Read(array, 0, 64);
            return array[1] == commandNumber;
        }

        private void updateProgess(int progressPercentage)
        {
        }


        private bool ReadFlash(SerialPort port, DataTransfer dataObj)
        {
            int num = 0;
            byte[] array = new byte[512];
            byte[] array2 = new byte[512];
            bool result = true;
            int num2 = dataObj.flashAddress;
            int localDataBufferStartPosition = dataObj.localAddress;
            for (int num3 = dataObj.flashAddress + dataObj.transferLength - num2; num3 > 0; num3 = dataObj.flashAddress + dataObj.transferLength - num2)
            {
                if (num3 > 32)
                {
                    num3 = 32;
                }
                array[0] = 82;
                array[1] = (byte)dataObj.mode;
                array[2] = (byte)((num2 >> 24) & 0xFF);
                array[3] = (byte)((num2 >> 16) & 0xFF);
                array[4] = (byte)((num2 >> 8) & 0xFF);
                array[5] = (byte)(num2 & 0xFF);
                array[6] = (byte)((num3 >> 8) & 0xFF);
                array[7] = (byte)(num3 & 0xFF);
                port.Write(array, 0, 8);
                while (port.BytesToRead == 0)
                {
                    Thread.Sleep(0);
                }
                port.Read(array2, 0, 64);
                if (array2[0] == 82)
                {
                    int num4 = (array2[1] << 8) + array2[2];
                    for (int i = 0; i < num4; i++)
                    {
                        dataObj.dataBuffer[localDataBufferStartPosition++] = array2[i + 3];
                    }
                    int num5 = (num2 - dataObj.flashAddress) * 100 / dataObj.transferLength;
                    if (num != num5)
                    {
                        updateProgess(num5);
                        num = num5;
                    }
                    num2 += num4;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }



        private static CalibrationData ByteArrayToCalData(byte[] bytes)
        {
            GCHandle gCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (CalibrationData)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(CalibrationData));
            }
            finally
            {
                gCHandle.Free();
            }
        }

        public bool readDataFromRadio()
        {
            bool result = true;
            int num = Marshal.SizeOf(typeof(CalibrationData));
            byte[] array = new byte[num];
            if (!COMPort.SetupPort())
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Отсутствует заданный COM-порт");
                return false;
            }
            DataTransfer COMData = new DataTransfer();
            sendCommand(COMPort.Port, 0);
            sendCommand(COMPort.Port, 1);
            sendCommand(COMPort.Port, 2, 0, 0, 3, 1, 0, "CPS");
            sendCommand(COMPort.Port, 2, 0, 16, 3, 1, 0, "Чтение");
            sendCommand(COMPort.Port, 2, 0, 32, 3, 1, 0, "калибровок");
            sendCommand(COMPort.Port, 3);
            sendCommand(COMPort.Port, 6, 3);
            COMData.mode = DataTransfer.DataMode.DataModeReadFlash;
            COMData.dataBuffer = new byte[CALIBRATION_TABLE_SIZE];
            COMData.localAddress = 0;
            COMData.flashAddress = MEMORY_LOCATION_STM32;
            COMData.transferLength = CALIBRATION_TABLE_SIZE;
            if (!ReadFlash(COMPort.Port, COMData))
            {
                result = false;
                COMData.responseCode = 1;
            }
            else
            {
                SystemSounds.Exclamation.Play();
            }


            sendCommand(COMPort.Port, 5);
            sendCommand(COMPort.Port, 7);
            COMPort.Port.Close();
            COMPort.Port = null;
            CalData = ByteArrayToCalData(COMData.dataBuffer);
            buildVariablesFromCalData(CalData);
            return result;
        }



        private void btnWrite_Click(object sender, EventArgs e)
        {
            buildCalDataFromVariables(CalData);
            DataTransfer COMData = new DataTransfer();
            COMData.dataBuffer = new byte[CALIBRATION_TABLE_SIZE];
            dataBuffer = DataToByte(CalData);
            Array.Copy(dataBuffer, 0, COMData.dataBuffer, 0, CALIBRATION_TABLE_SIZE);
            sendCommand(COMPort.Port, 0);
            sendCommand(COMPort.Port, 1);
            sendCommand(COMPort.Port, 2, 0, 0, 3, 1, 0, "CPS");
            sendCommand(COMPort.Port, 2, 0, 16, 3, 1, 0, "Запись");
            sendCommand(COMPort.Port, 2, 0, 32, 3, 1, 0, "калибровок");
            sendCommand(COMPort.Port, 3);
            sendCommand(COMPort.Port, 6, 4);
            COMData.mode = DataTransfer.DataMode.DataModeWriteFlash;
            COMData.localAddress = 0;
            COMData.flashAddress = MEMORY_LOCATION_STM32 ;
            COMData.transferLength = CALIBRATION_TABLE_SIZE;
            if (!WriteFlash(COMPort.Port, COMData))
            {
                MessageBox.Show("Ошибка при записи в последовательный порт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                COMData.responseCode = 1;
            }
            sendCommand(COMPort.Port, 6, 2);
            sendCommand(COMPort.Port, 6, 1);
            COMPort.Port.Close();
            COMPort.Port = null;
        }

        private bool flashWriteSector(SerialPort port, char writeCharacter, ref byte[] sendbuffer, ref byte[] readbuffer, DataTransfer dataObj)
        {
            int num = 100;
            dataObj.dataSector = -1;
            sendbuffer[0] = (byte)writeCharacter;
            sendbuffer[1] = 3;
            port.Write(sendbuffer, 0, 2);
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(1);
            }
            Thread.Sleep(100);
            while (port.BytesToRead == 0 && num-- > 0)
            {
                Thread.Sleep(5);
            }
            port.Read(readbuffer, 0, port.BytesToRead);
            if (readbuffer[0] == sendbuffer[0] && readbuffer[1] == sendbuffer[1])
            {
                return num != -1;
            }
            return false;
        }

        private bool flashWritePrepareSector(SerialPort port, char writeCharacter, int address, ref byte[] sendbuffer, ref byte[] readbuffer, DataTransfer dataObj)
        {
            int num = 100;
            dataObj.dataSector = address / 4096;
            sendbuffer[0] = (byte)writeCharacter;
            sendbuffer[1] = 1;
            sendbuffer[2] = (byte)((dataObj.dataSector >> 16) & 0xFF);
            sendbuffer[3] = (byte)((dataObj.dataSector >> 8) & 0xFF);
            sendbuffer[4] = (byte)(dataObj.dataSector & 0xFF);
            port.Write(sendbuffer, 0, 5);
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(1);
            }
            Thread.Sleep(50);
            while (port.BytesToRead == 0 && num-- > 0)
            {
                Thread.Sleep(1);
            }
            if (num != -1)
            {
                port.Read(readbuffer, 0, port.BytesToRead);
            }
            if (readbuffer[0] == sendbuffer[0] && readbuffer[1] == sendbuffer[1])
            {
                return num != -1;
            }
            return false;
        }

        private bool flashSendData(SerialPort port, char writeCharacter, int address, int len, ref byte[] sendbuffer, ref byte[] readbuffer)
        {
            int num = 100;
            sendbuffer[0] = (byte)writeCharacter;
            sendbuffer[1] = 2;
            sendbuffer[2] = (byte)((address >> 24) & 0xFF);
            sendbuffer[3] = (byte)((address >> 16) & 0xFF);
            sendbuffer[4] = (byte)((address >> 8) & 0xFF);
            sendbuffer[5] = (byte)(address & 0xFF);
            sendbuffer[6] = (byte)((len >> 8) & 0xFF);
            sendbuffer[7] = (byte)(len & 0xFF);
            port.Write(sendbuffer, 0, len + 8);
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(1);
            }
            Thread.Sleep(20);
            while (port.BytesToRead == 0 && num-- > 0)
            {
                Thread.Sleep(1);
            }
            if (num != -1)
            {
                port.Read(readbuffer, 0, port.BytesToRead);
            }
            if (readbuffer[0] == sendbuffer[0] && readbuffer[1] == sendbuffer[1])
            {
                return num != -1;
            }
            return false;
        }

        private bool WriteFlash(SerialPort port, DataTransfer dataObj)
        {/*
            int num = 0;
            byte[] sendbuffer = new byte[1032];
            byte[] readbuffer = new byte[1032];
            int num2 = dataObj.startDataAddressInTheRadio;
            int localDataBufferStartPosition = dataObj.localDataBufferStartPosition;
            dataObj.data_sector = -1;
            for (int num3 = dataObj.startDataAddressInTheRadio + dataObj.transferLength - num2; num3 > 0; num3 = dataObj.startDataAddressInTheRadio + dataObj.transferLength - num2)
            {
                if (num3 > OpenGD77Form.getUSBWriteBufferSize())
                {
                    num3 = OpenGD77Form.getUSBWriteBufferSize();
                }
                if (dataObj.data_sector == -1 && !flashWritePrepareSector(port, writeCommandCharacter, num2, ref sendbuffer, ref readbuffer, dataObj))
                {
                    return false;
                }
                if (dataObj.mode != 0)
                {
                    int num4 = 0;
                    for (int i = 0; i < num3; i++)
                    {
                        sendbuffer[i + 8] = dataObj.dataBuff[localDataBufferStartPosition++];
                        num4++;
                        if (dataObj.data_sector != (num2 + num4) / 4096)
                        {
                            break;
                        }
                    }
                    if (!flashSendData(port, writeCommandCharacter, num2, num4, ref sendbuffer, ref readbuffer))
                    {
                        return false;
                    }
                    int num5 = (num2 - dataObj.startDataAddressInTheRadio) * 100 / dataObj.transferLength;
                    if (num != num5)
                    {
                        updateProgess(num5);
                        num = num5;
                    }
                    num2 += num4;
                    if (dataObj.data_sector != num2 / 4096 && !flashWriteSector(port, writeCommandCharacter, ref sendbuffer, ref readbuffer, dataObj))
                    {
                        return false;
                    }
                }
            }
            if (dataObj.data_sector != -1 && !flashWriteSector(port, writeCommandCharacter, ref sendbuffer, ref readbuffer, dataObj))
            {
                Console.WriteLine($"Error. Write stopped (write sector error at {num2:X8})");
                return false;
            }*/
            return true;
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
            string profileStringWithDefault = RegistryOperations.getProfileStringWithDefault("LastFilePath", "");
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
            if (!readBandlimits())
            {
                MessageBox.Show("Ограничения частот не считаны из рации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (readDataFromRadio())
            {
                showButtons();
            }
        

        }

        private void btnReadFactoryFromRadio_Click(object sender, EventArgs e)
        {
            if (!COMPort.SetupPort())
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Ошибка при соединении с COM-портом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataTransfer COMData = new DataTransfer();
            COMData.dataBuffer = new byte[CALIBRATION_TABLE_SIZE];
            dataBuffer = DataToByte(CalData);
            Array.Copy(dataBuffer, 0, COMData.dataBuffer, 0, CALIBRATION_TABLE_SIZE);
            sendCommand(COMPort.Port, 0);
            sendCommand(COMPort.Port, 1);
            sendCommand(COMPort.Port, 2, 0, 0, 3, 1, 0, "CPS");
            sendCommand(COMPort.Port, 2, 0, 16, 3, 1, 0, "Восстановление");
            sendCommand(COMPort.Port, 2, 0, 32, 3, 1, 0, "калибровок");
            sendCommand(COMPort.Port, 3);
            sendCommand(COMPort.Port, 6, 4);
            COMData.mode = DataTransfer.DataMode.DataModeWriteFlash;
            COMData.localAddress = 0;
            COMData.flashAddress = MEMORY_LOCATION_STM32;
            COMData.transferLength = CALIBRATION_TABLE_SIZE;
            if (!WriteFlash(COMPort.Port, COMData))
            {
                MessageBox.Show("Ошибка при восстановлении!");
                COMData.responseCode = 1;
            }
            sendCommand(COMPort.Port, 6, 2);
            sendCommand(COMPort.Port, 6, 1);
            COMPort.Port.Close();
            COMPort.Port = null;
            MessageBox.Show("После перезагрузки рация перестроит таблицы, исходя из заводских калибровок, сохраненных в защищенной памяти.", "Сброс калибровок", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showButtons()
        {
            btnWrite.Visible = true;
            btnSaveCalibration.Visible = true;
            btnChart.Visible = true;
            btnReadFactoryFromRadio.Visible = true;
        }

        private void btnSaveCalibration_Click(object sender, EventArgs e)
        {
            string profileStringWithDefault = RegistryOperations.getProfileStringWithDefault("LastFilePath", "");
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

                power8VHF[i] = new NumericUpDown();
                power8VHF[i].Width = 74;
                power8VHF[i].Increment = 16;
                power8VHF[i].Height = 20;
                power8VHF[i].Margin = margin;
                power8VHF[i].Minimum = 0;
                power8VHF[i].Maximum = 4095;
                power8VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power7VHF[i] = new NumericUpDown();
                power7VHF[i].Width = 74;
                power7VHF[i].Increment = 16;
                power7VHF[i].Height = 20;
                power7VHF[i].Margin = margin;
                power7VHF[i].Minimum = 0;
                power7VHF[i].Maximum = 4095;
                power7VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power6VHF[i] = new NumericUpDown();
                power6VHF[i].Width = 74;
                power6VHF[i].Increment = 16;
                power6VHF[i].Height = 20;
                power6VHF[i].Margin = margin;
                power6VHF[i].Minimum = 0;
                power6VHF[i].Maximum = 4095;
                power6VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power5VHF[i] = new NumericUpDown();
                power5VHF[i].Width = 74;
                power5VHF[i].Increment = 16;
                power5VHF[i].Height = 20;
                power5VHF[i].Margin = margin;
                power5VHF[i].Minimum = 0;
                power5VHF[i].Maximum = 4095;
                power5VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power4VHF[i] = new NumericUpDown();
                power4VHF[i].Width = 74;
                power4VHF[i].Increment = 1;
                power4VHF[i].Height = 20;
                power4VHF[i].Margin = margin;
                power4VHF[i].Minimum = 0;
                power4VHF[i].Maximum = 4095;
                power4VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power3VHF[i] = new NumericUpDown();
                power3VHF[i].Width = 74;
                power3VHF[i].Increment = 1;
                power3VHF[i].Height = 20;
                power3VHF[i].Margin = margin;
                power3VHF[i].Minimum = 0;
                power3VHF[i].Maximum = 4095;
                power3VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power2VHF[i] = new NumericUpDown();
                power2VHF[i].Width = 74;
                power2VHF[i].Increment = 1;
                power2VHF[i].Height = 20;
                power2VHF[i].Margin = margin;
                power2VHF[i].Minimum = 0;
                power2VHF[i].Maximum = 4095;
                power2VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power1VHF[i] = new NumericUpDown();
                power1VHF[i].Width = 74;
                power1VHF[i].Increment = 1;
                power1VHF[i].Height = 20;
                power1VHF[i].Margin = margin;
                power1VHF[i].Minimum = 0;
                power1VHF[i].Maximum = 4095;
                power1VHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power0VHF[i] = new NumericUpDown();
                power0VHF[i].Width = 74;
                power0VHF[i].Increment = 1;
                power0VHF[i].Height = 20;
                power0VHF[i].Margin = margin;
                power0VHF[i].Minimum = 0;
                power0VHF[i].Maximum = 4095;
                power0VHF[i].ValueChanged += new EventHandler(nmValueChanged);
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
            
                power8UHF[i] = new NumericUpDown();
                power8UHF[i].Width = 74;
                power8UHF[i].Increment = 16;
                power8UHF[i].Height = 20;
                power8UHF[i].Margin = margin;
                power8UHF[i].Minimum = 0;
                power8UHF[i].Maximum = 4095;
                power8UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power7UHF[i] = new NumericUpDown();
                power7UHF[i].Width = 74;
                power7UHF[i].Increment = 16;
                power7UHF[i].Height = 20;
                power7UHF[i].Margin = margin;
                power7UHF[i].Minimum = 0;
                power7UHF[i].Maximum = 4095;
                power7UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power6UHF[i] = new NumericUpDown();
                power6UHF[i].Width = 74;
                power6UHF[i].Increment = 16;
                power6UHF[i].Height = 20;
                power6UHF[i].Margin = margin;
                power6UHF[i].Minimum = 0;
                power6UHF[i].Maximum = 4095;
                power6UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power5UHF[i] = new NumericUpDown();
                power5UHF[i].Width = 74;
                power5UHF[i].Increment = 16;
                power5UHF[i].Height = 20;
                power5UHF[i].Margin = margin;
                power5UHF[i].Minimum = 0;
                power5UHF[i].Maximum = 4095;
                power5UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power4UHF[i] = new NumericUpDown();
                power4UHF[i].Width = 74;
                power4UHF[i].Increment = 1;
                power4UHF[i].Height = 20;
                power4UHF[i].Margin = margin;
                power4UHF[i].Minimum = 0;
                power4UHF[i].Maximum = 4095;
                power4UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power3UHF[i] = new NumericUpDown();
                power3UHF[i].Width = 74;
                power3UHF[i].Increment = 1;
                power3UHF[i].Height = 20;
                power3UHF[i].Margin = margin;
                power3UHF[i].Minimum = 0;
                power3UHF[i].Maximum = 4095;
                power3UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power2UHF[i] = new NumericUpDown();
                power2UHF[i].Width = 74;
                power2UHF[i].Increment = 1;
                power2UHF[i].Height = 20;
                power2UHF[i].Margin = margin;
                power2UHF[i].Minimum = 0;
                power2UHF[i].Maximum = 4095;
                power2UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power1UHF[i] = new NumericUpDown();
                power1UHF[i].Width = 74;
                power1UHF[i].Increment = 1;
                power1UHF[i].Height = 20;
                power1UHF[i].Margin = margin;
                power1UHF[i].Minimum = 0;
                power1UHF[i].Maximum = 4095;
                power1UHF[i].ValueChanged += new EventHandler(nmValueChanged);
                power0UHF[i] = new NumericUpDown();
                power0UHF[i].Width = 74;
                power0UHF[i].Increment = 1;
                power0UHF[i].Height = 20;
                power0UHF[i].Margin = margin;
                power0UHF[i].Minimum = 0;
                power0UHF[i].Maximum = 4095;
                power0UHF[i].ValueChanged += new EventHandler(nmValueChanged);
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

            foreach (var item in tlpVHF.Controls)
            {
                if (item is NumericUpDown)
                {
                    ((NumericUpDown)item).Click += new EventHandler(nmClick);
                }
            }
            foreach (var item in tlpUHF.Controls)
            {
                if (item is NumericUpDown)
                {
                    ((NumericUpDown)item).Click += new EventHandler(nmClick);
                }
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
            lblRadioType.Text = "Тип рации: ";

            lblRadioType.Text += "TYT MD-9600/Retevis RT-90";
            isReading = true;
            nmRSSI120.Value = c.RSSI120 * 16;
            nmRSSI70.Value = c.RSSI70 * 16;
            nmVHFOscRef.Value = c.OscRefTuneVHF;
            nmUHFOscRef.Value = c.OscRefTuneVHF;
            //tlpVHF
            var vhfPowers = c.PowersVHFAs2D;
            var uhfPowers = c.PowersUHFAs2D;
            for (int i = 0; i < 5; i++)
            {

                frequenciesTxVHF[i].Text = ((radioBandlimits.VHFLowCal + (i * 950000)) / 100000.0f).ToString("N3");
                power8VHF[i].Value = vhfPowers[8][i];
                power7VHF[i].Value = vhfPowers[7][i];
                power6VHF[i].Value = vhfPowers[6][i];
                power5VHF[i].Value = vhfPowers[5][i];
                power4VHF[i].Value = vhfPowers[4][i];
                power3VHF[i].Value = vhfPowers[3][i];
                power2VHF[i].Value = vhfPowers[2][i];
                power1VHF[i].Value = vhfPowers[1][i];
                power0VHF[i].Value = vhfPowers[0][i];
                rxTuningVHF[i].Value = c.RxTuneVHF[i];
                iGainDMRVHF[i].Value = c.IGainDMRVHF[i];
                qGainDMRVHF[i].Value = c.QGainDMRVHF[i];
                iGainFMVHF[i].Value = c.IGainVHF[i];
                qGainFMVHF[i].Value = c.QGainVHF[i];
            }
            for (int i = 0; i < 9; i++)
            {

                frequenciesTxUHF[i].Text = ((radioBandlimits.UHFLowCal + (i * 1000000)) / 100000.0f).ToString("N3");
                power8UHF[i].Value = uhfPowers[8][i];
                power7UHF[i].Value = uhfPowers[7][i];
                power6UHF[i].Value = uhfPowers[6][i];
                power5UHF[i].Value = uhfPowers[5][i];
                power4UHF[i].Value = uhfPowers[4][i];
                power3UHF[i].Value = uhfPowers[3][i];
                power2UHF[i].Value = uhfPowers[2][i];
                power1UHF[i].Value = uhfPowers[1][i];
                power0UHF[i].Value = uhfPowers[0][i];
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
                c.RSSI120 = (byte)(nmRSSI120.Value / 16);
                c.RSSI70 = (byte)(nmRSSI70.Value / 16);
                c.OscRefTuneVHF = (byte)nmVHFOscRef.Value;
                c.OscRefTuneUHF = (byte)nmUHFOscRef.Value;
                for (int i = 0; i < 5; i++)
                {
                    c.PowersUHFAs2D[8][i] = (ushort)power8VHF[i].Value;
                    c.PowersUHFAs2D[7][i] = (ushort)power7VHF[i].Value;
                    c.PowersUHFAs2D[6][i] = (ushort)power6VHF[i].Value;
                    c.PowersUHFAs2D[5][i] = (ushort)power5VHF[i].Value;
                    c.PowersUHFAs2D[4][i] = (ushort)power4VHF[i].Value;
                    c.PowersUHFAs2D[3][i] = (ushort)power3VHF[i].Value;
                    c.PowersUHFAs2D[2][i] = (ushort)power2VHF[i].Value;
                    c.PowersUHFAs2D[1][i] = (ushort)power1VHF[i].Value;
                    c.PowersUHFAs2D[0][i] = (ushort)power0VHF[i].Value;
                    c.RxTuneVHF[i] = (byte)rxTuningVHF[i].Value;
                    c.IGainDMRVHF[i] = (byte)iGainDMRVHF[i].Value;
                    c.QGainDMRVHF[i] = (byte)qGainDMRVHF[i].Value;
                    c.IGainVHF[i] = (byte)iGainFMVHF[i].Value;
                    c.QGainVHF[i] = (byte)qGainFMVHF[i].Value;
                }
                for (int i = 0; i < 9; i++)
                {
                    c.PowersUHFAs2D[8][i] = (ushort)power8UHF[i].Value;
                    c.PowersUHFAs2D[7][i] = (ushort)power7UHF[i].Value;
                    c.PowersUHFAs2D[6][i] = (ushort)power6UHF[i].Value;
                    c.PowersUHFAs2D[5][i] = (ushort)power5UHF[i].Value;
                    c.PowersUHFAs2D[4][i] = (ushort)power4UHF[i].Value;
                    c.PowersUHFAs2D[3][i] = (ushort)power3UHF[i].Value;
                    c.PowersUHFAs2D[2][i] = (ushort)power2UHF[i].Value;
                    c.PowersUHFAs2D[1][i] = (ushort)power1UHF[i].Value;
                    c.PowersUHFAs2D[0][i] = (ushort)power0UHF[i].Value;
                    c.RxTuneUHF[i] = (byte)rxTuningUHF[i].Value;
                    c.IGainDMRUHF[i] = (byte)iGainDMRUHF[i].Value;
                    c.QGainDMRUHF[i] = (byte)qGainDMRUHF[i].Value;
                    c.IGainUHF[i] = (byte)iGainFMUHF[i].Value;
                    c.QGainUHF[i] = (byte)qGainFMUHF[i].Value;
                }
                c.checksum = 0;
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
         /*   CalibrationCharts chartsForm = new CalibrationCharts(CalData);
            chartsForm.ShowDialog();*/
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
            this.btnReadFactoryFromRadio = new System.Windows.Forms.Button();
            this.lblRadioType = new System.Windows.Forms.Label();
            this.btnClearColors = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tabVHF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmVHFOscRef)).BeginInit();
            this.tlpVHF.SuspendLayout();
            this.tabUHF.SuspendLayout();
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
            this.btnWrite.Font = new System.Drawing.Font("Arial", 8F);
            this.btnWrite.Location = new System.Drawing.Point(878, 258);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(268, 23);
            this.btnWrite.TabIndex = 1;
            this.btnWrite.Text = "Записать в рацию";
            this.btnWrite.UseVisualStyleBackColor = false;
            this.btnWrite.Visible = false;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.BackColor = System.Drawing.Color.White;
            this.btnReadFile.Font = new System.Drawing.Font("Arial", 8F);
            this.btnReadFile.Location = new System.Drawing.Point(878, 216);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(268, 23);
            this.btnReadFile.TabIndex = 1;
            this.btnReadFile.Text = "Открыть файл калибровок";
            this.btnReadFile.UseVisualStyleBackColor = false;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnReadFromRadio
            // 
            this.btnReadFromRadio.BackColor = System.Drawing.Color.White;
            this.btnReadFromRadio.Font = new System.Drawing.Font("Arial", 8F);
            this.btnReadFromRadio.Location = new System.Drawing.Point(878, 177);
            this.btnReadFromRadio.Name = "btnReadFromRadio";
            this.btnReadFromRadio.Size = new System.Drawing.Size(268, 23);
            this.btnReadFromRadio.TabIndex = 1;
            this.btnReadFromRadio.Text = "Считать калибровки из рации";
            this.btnReadFromRadio.UseVisualStyleBackColor = false;
            this.btnReadFromRadio.Click += new System.EventHandler(this.btnReadFromRadio_Click);
            // 
            // btnSaveCalibration
            // 
            this.btnSaveCalibration.BackColor = System.Drawing.Color.White;
            this.btnSaveCalibration.Font = new System.Drawing.Font("Arial", 8F);
            this.btnSaveCalibration.Location = new System.Drawing.Point(878, 303);
            this.btnSaveCalibration.Name = "btnSaveCalibration";
            this.btnSaveCalibration.Size = new System.Drawing.Size(268, 23);
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
            // nmVHFOscRef
            // 
            this.nmVHFOscRef.Location = new System.Drawing.Point(282, 364);
            this.nmVHFOscRef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmVHFOscRef.Name = "nmVHFOscRef";
            this.nmVHFOscRef.Size = new System.Drawing.Size(89, 20);
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
            this.label9.Text = "Уровень мощности 9";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(4, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Уровень мощности 8";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(4, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "Уровень мощности 7";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(4, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 20);
            this.label12.TabIndex = 5;
            this.label12.Text = "Уровень мощности 6";
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
            this.label45.Text = "Уровень мощности 5";
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
            this.label46.Text = "Уровень мощности 4";
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
            this.label47.Text = "Уровень мощности 3";
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
            this.label48.Text = "Уровень мощности 2";
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
            this.label49.Text = "Уровень мощности 1";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabUHF
            // 
            this.tabUHF.BackColor = System.Drawing.Color.White;
            this.tabUHF.Controls.Add(this.nmUHFOscRef);
            this.tabUHF.Controls.Add(this.label35);
            this.tabUHF.Controls.Add(this.tlpUHF);
            this.tabUHF.Location = new System.Drawing.Point(4, 22);
            this.tabUHF.Name = "tabUHF";
            this.tabUHF.Padding = new System.Windows.Forms.Padding(3);
            this.tabUHF.Size = new System.Drawing.Size(849, 381);
            this.tabUHF.TabIndex = 1;
            this.tabUHF.Text = "Диапазон 70 см";
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
            this.nmUHFOscRef.Size = new System.Drawing.Size(89, 20);
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
            this.label41.Text = "Уровень мощности 9";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Location = new System.Drawing.Point(4, 43);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(144, 20);
            this.label42.TabIndex = 3;
            this.label42.Text = "Уровень мощности 8";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label43
            // 
            this.label43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label43.Location = new System.Drawing.Point(4, 64);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(144, 20);
            this.label43.TabIndex = 4;
            this.label43.Text = "Уровень мощности 7";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Location = new System.Drawing.Point(4, 85);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(144, 20);
            this.label44.TabIndex = 5;
            this.label44.Text = "Уровень мощности 6";
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
            this.label50.Text = "Уровень мощности 5";
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
            this.label51.Text = "Уровень мощности 4";
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
            this.label52.Text = "Уровень мощности 3";
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
            this.label53.Text = "Уровень мощности 2";
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
            this.label54.Text = "Уровень мощности 1";
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
            // btnReadFactoryFromRadio
            // 
            this.btnReadFactoryFromRadio.BackColor = System.Drawing.Color.White;
            this.btnReadFactoryFromRadio.Location = new System.Drawing.Point(878, 345);
            this.btnReadFactoryFromRadio.Name = "btnReadFactoryFromRadio";
            this.btnReadFactoryFromRadio.Size = new System.Drawing.Size(268, 23);
            this.btnReadFactoryFromRadio.TabIndex = 5;
            this.btnReadFactoryFromRadio.Text = "Считать заводские калибровки рации";
            this.btnReadFactoryFromRadio.UseVisualStyleBackColor = false;
            this.btnReadFactoryFromRadio.Visible = false;
            this.btnReadFactoryFromRadio.Click += new System.EventHandler(this.btnReadFactoryFromRadio_Click);
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
            this.btnClearColors.Location = new System.Drawing.Point(880, 392);
            this.btnClearColors.Name = "btnClearColors";
            this.btnClearColors.Size = new System.Drawing.Size(266, 23);
            this.btnClearColors.TabIndex = 10;
            this.btnClearColors.Text = "Сбросить маркеры";
            this.btnClearColors.UseVisualStyleBackColor = false;
            this.btnClearColors.Click += new System.EventHandler(this.btnClearColors_Click);
            // 
            // btnChart
            // 
            this.btnChart.BackColor = System.Drawing.Color.White;
            this.btnChart.Location = new System.Drawing.Point(877, 132);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(267, 23);
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
            this.Controls.Add(this.btnReadFactoryFromRadio);
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
            ((System.ComponentModel.ISupportInitialize)(this.nmVHFOscRef)).EndInit();
            this.tlpVHF.ResumeLayout(false);
            this.tlpVHF.PerformLayout();
            this.tabUHF.ResumeLayout(false);
            this.tabUHF.PerformLayout();
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
