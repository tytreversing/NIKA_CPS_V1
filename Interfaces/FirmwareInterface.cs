using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NIKA_CPS_V1.Interfaces
{
    internal class FirmwareInterface
    {
        public enum CPSCommand
        {
            InitUI = 0,
            ClearScreen = 1,
            WriteString = 2,
            UpdateScreen = 3,
            BacklightOn = 4,
            CloseUI = 5,
            Finish = 6,
            RestartGPS = 7,
            RestoreSettings = 77,
            Ping = 254
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RadioInfo
        {

            [MarshalAs(UnmanagedType.U1)]
            public byte radioType;

            [MarshalAs(UnmanagedType.U1)]
            public byte radioHardware;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string identifier;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string buildDateTime;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string serial;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string fwDate;
        }

        private static char writeCommandCharacter = 'W';

        /* public static bool sendCommand(SerialPort port, CPSCommand commandNumber, int x_or_command_option_number = 0, int y = 0, int iSize = 0, int alignment = 0, int isInverted = 0, string message = "")
         {
             byte[] array = new byte[64];
             array[0] = 67;
             array[1] = (byte)commandNumber;
             switch (commandNumber)
             {
                 case CPSCommand.WriteString:
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
                 case CPSCommand.Finish:
                     array[2] = (byte)x_or_command_option_number;
                     break;
             }
             port.Write(array, 0, 32);
             while (port.BytesToRead == 0)
             {
                 Thread.Sleep(0);
             }
             port.Read(array, 0, 64);
             return array[1] == (byte)commandNumber;
         }*/

        public static bool sendCommand(SerialPort port, CPSCommand commandNumber, int x_or_command_option_number = 0, int y = 0, int iSize = 0, int alignment = 0, int isInverted = 0, string message = "")
        {
            Encoding transcoder = Encoding.GetEncoding("windows-1251");
            int num = 100;
            byte[] array = new byte[1032];
            int num2 = 2;
            array[0] = 67;
            array[1] = (byte)commandNumber;
            switch (commandNumber)
            {
                case CPSCommand.WriteString:
                    array[3] = (byte)y;
                    array[4] = (byte)iSize;
                    array[5] = (byte)alignment;
                    array[6] = (byte)isInverted;
                    num2 += 5 + Math.Min(message.Length, 16);
                    Buffer.BlockCopy(transcoder.GetBytes(message), 0, array, 7, num2 - 7);
                    break;
                case CPSCommand.Finish:
                    array[2] = (byte)x_or_command_option_number;
                    num2++;
                    break;
                case CPSCommand.RestoreSettings:
                    array[2] = (byte)x_or_command_option_number;
                    num2++;
                    break;
            }
            port.Write(array, 0, num2);
            while (port.BytesToWrite > 0)
            {
                Thread.Sleep(1);
            }
            Thread.Sleep(50);
            while (port.BytesToRead == 0 && num-- > 0)
            {
                Thread.Sleep(5);
            }
            if (num != -1)
            {
                port.Read(array, 0, port.BytesToRead);
            }
            if (array[1] == (byte)commandNumber)
            {
                return num != -1;
            }
            return false;
        }

        public static bool flashWritePrepareSector(SerialPort port, char writeCharacter, int address, ref byte[] sendbuffer, ref byte[] readbuffer, DataTransfer dataObj)
        {
            int num = 100;
            dataObj.dataSector = address / 4096;
            sendbuffer[0] = (byte)writeCharacter;
            sendbuffer[1] = 1;
            sendbuffer[2] = (byte)((dataObj.dataSector >> 16) & 0xFF);
            sendbuffer[3] = (byte)((dataObj.dataSector >> 8) & 0xFF);
            sendbuffer[4] = (byte)(dataObj.dataSector & 0xFF);
            port.Write(sendbuffer, 0, 5);
            while (port.BytesToWrite > 0)
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

        public static bool flashSendData(SerialPort port, char writeCharacter, int address, int len, ref byte[] sendbuffer, ref byte[] readbuffer)
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
            while (port.BytesToWrite > 0)
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

        public static bool flashWriteSector(SerialPort port, char writeCharacter, ref byte[] sendbuffer, ref byte[] readbuffer, DataTransfer dataObj)
        {
            int num = 100;
            dataObj.dataSector = -1;
            sendbuffer[0] = (byte)writeCharacter;
            sendbuffer[1] = 3;
            port.Write(sendbuffer, 0, 2);
            while (port.BytesToWrite > 0)
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

        public static bool WriteFlash(SerialPort port, DataTransfer dataObj)
        {
            int num = 0;
            byte[] sendbuffer = new byte[1032];
            byte[] readbuffer = new byte[1032];
            int num2 = dataObj.flashAddress;
            int localDataBufferStartPosition = dataObj.localAddress;
            dataObj.dataSector = -1;
            for (int num3 = dataObj.flashAddress + dataObj.transferLength - num2; num3 > 0; num3 = dataObj.flashAddress + dataObj.transferLength - num2)
            {
                if (num3 > 1024)
                {
                    num3 = 1024;
                }
                if (dataObj.dataSector == -1 && !flashWritePrepareSector(port, writeCommandCharacter, num2, ref sendbuffer, ref readbuffer, dataObj))
                {
                    return false;
                }
                if (dataObj.mode != 0)
                {
                    int num4 = 0;
                    for (int i = 0; i < num3; i++)
                    {
                        sendbuffer[i + 8] = dataObj.dataBuffer[localDataBufferStartPosition++];
                        num4++;
                        if (dataObj.dataSector != (num2 + num4) / 4096)
                        {
                            break;
                        }
                    }
                    if (!flashSendData(port, writeCommandCharacter, num2, num4, ref sendbuffer, ref readbuffer))
                    {
                        return false;
                    }
                    int num5 = (num2 - dataObj.flashAddress) * 100 / dataObj.transferLength;
                    if (num != num5)
                    {
                        num = num5;
                        MainForm.setProgress(num5);
                    }
                    num2 += num4;
                    if (dataObj.dataSector != num2 / 4096 && !flashWriteSector(port, writeCommandCharacter, ref sendbuffer, ref readbuffer, dataObj))
                    {
                        return false;
                    }
                }
            }
            if (dataObj.dataSector != -1 && !flashWriteSector(port, writeCommandCharacter, ref sendbuffer, ref readbuffer, dataObj))
            {
                MessageBox.Show($"Error. Write stopped (write sector error at {num2:X8})");
                return false;
            }
            return true;
        }

        public bool WriteLoFlash(SerialPort port, DataTransfer dataObj)
        {
            int num = 0;
            byte[] array = new byte[1032];
            byte[] array2 = new byte[1032];
            int num2 = dataObj.flashAddress;
            int localDataBufferStartPosition = dataObj.localAddress;
            int num3 = dataObj.flashAddress + dataObj.transferLength - num2;
            while (num3 > 0)
            {
                if (num3 > 1024)
                {
                    num3 = 1024;
                }
                if (dataObj.dataSector == -1)
                {
                    dataObj.dataSector = num2 / 128;
                }
                int num4 = 0;
                for (int i = 0; i < num3; i++)
                {
                    array[i + 8] = dataObj.dataBuffer[localDataBufferStartPosition++];
                    num4++;
                    if (dataObj.dataSector != (num2 + num4) / 128)
                    {
                        dataObj.dataSector = -1;
                        break;
                    }
                }
                array[0] = (byte)writeCommandCharacter;
                array[1] = 4;
                array[2] = (byte)((num2 >> 24) & 0xFF);
                array[3] = (byte)((num2 >> 16) & 0xFF);
                array[4] = (byte)((num2 >> 8) & 0xFF);
                array[5] = (byte)(num2 & 0xFF);
                array[6] = (byte)((num4 >> 8) & 0xFF);
                array[7] = (byte)(num4 & 0xFF);
                port.Write(array, 0, num4 + 8);
                while (port.BytesToWrite > 0)
                {
                    Thread.Sleep(1);
                }
                Thread.Sleep(50);
                while (port.BytesToRead == 0)
                {
                    Thread.Sleep(5);
                }
                port.Read(array2, 0, port.BytesToRead);
                if (array2[0] == array[0] && array2[1] == array[1])
                {
                    int num5 = (num2 - dataObj.flashAddress) * 100 / dataObj.transferLength;
                    if (num != num5)
                    {
                        num = num5;
                        MainForm.setProgress(num5);
                    }
                    num2 += num4;
                    num3 = dataObj.flashAddress + dataObj.transferLength - num2;
                    continue;
                }
 
                return false;
            }
            return true;
        }

        public static bool ReadFlash(SerialPort port, DataTransfer dataObj)
        {
            int num = 0;
            byte[] array = new byte[1032];
            byte[] array2 = new byte[1032];
            int num2 = dataObj.flashAddress;
            int localDataBufferStartPosition = dataObj.localAddress;
            int num3 = dataObj.flashAddress + dataObj.transferLength - num2;
            while (num3 > 0)
            {
                int num4 = 100;
                if (num3 > 1024)
                {
                    num3 = 1024;
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
                while (port.BytesToWrite > 0)
                {
                    Thread.Sleep(1);
                }
                while (port.BytesToRead == 0 && num4-- > 0)
                {
                    Thread.Sleep(5);
                }
                if (num4 == -1)
                {
                    return false;
                }
                port.Read(array2, 0, port.BytesToRead);
                if (array2[0] == 82)
                {
                    int num5 = (array2[1] << 8) + array2[2];
                    for (int i = 0; i < num5; i++)
                    {
                        dataObj.dataBuffer[localDataBufferStartPosition++] = array2[i + 3];
                    }
                    int num6 = (num2 - dataObj.flashAddress) * 100 / dataObj.transferLength;
                    if (num != num6)
                    {
                        num = num6;
                        MainForm.setProgress(num6);
                    }
                    num2 += num5;
                    num3 = dataObj.flashAddress + dataObj.transferLength - num2;
                    continue;
                }
                return false;
            }
            return true;
        }

        public static void DataTask(DataTransfer dataObj)
        {
            try
            {
                MainForm.worker = new BackgroundWorker();
                MainForm.worker.DoWork += worker_DoWork;
                MainForm.worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                MainForm.worker.RunWorkerAsync(dataObj);
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message);
                MainForm.restoreUI();
            }
        }

        public static void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!(e.Result is DataTransfer tData))
            {
                return;
            }
            if (tData.action != 0)
            {
                if (tData.responseCode == 0)
                {
                    switch (tData.action)
                    {
                        case DataTransfer.CPSAction.BACKUP_FLASH:
                            {
                                SaveFileDialog sfdFlash = new SaveFileDialog();
                                sfdFlash.InitialDirectory = RegistryOperations.GetString("LastFlashBackupFolder", null);
                                sfdFlash.Filter = "Бинарные файлы (*.bin)|*.bin";
                                sfdFlash.FilterIndex = 1;
                                if (sfdFlash.ShowDialog() == DialogResult.OK)
                                {
                                    File.WriteAllBytes(sfdFlash.FileName, tData.dataBuffer);
                                    RegistryOperations.WriteString("LastFlashBackupFoldern", Path.GetDirectoryName(sfdFlash.FileName));
                                }
                                tData.action = DataTransfer.CPSAction.NONE;
                                MainForm.logMessage("Бэкап флеш-памяти завершен!");
                                MainForm.restoreUI();
                                break;
                            }
                    }
                }
                else
                {
                    SystemSounds.Hand.Play();
                    MessageBox.Show("Просмотрите данные в консоли вывода.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MainForm.restoreUI();
                }
            }
            MainForm.setProgress(0);
        }

        private static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTransfer tData = e.Argument as DataTransfer;
            if (COMPort.Port == null)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Ошибка при соединении с COM-портом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                switch (tData.action)
                {
                    
                    case DataTransfer.CPSAction.BACKUP_FLASH:
                        if (!sendCommand(COMPort.Port, 0))
                        {
                            MessageBox.Show("Ошибка при соединении с COM-портом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tData.responseCode = 1;
                            COMPort.Port = null;
                            break;
                        }
                        sendCommand(COMPort.Port, CPSCommand.ClearScreen);
                        sendCommand(COMPort.Port, CPSCommand.WriteString, 0, 0, 3, 1, 0, "Бэкап");
                        sendCommand(COMPort.Port, CPSCommand.WriteString, 0, 16, 3, 1, 0, "флеш-памяти");
                        sendCommand(COMPort.Port, CPSCommand.UpdateScreen);
                        sendCommand(COMPort.Port, CPSCommand.Finish, 3);
                        tData.mode = DataTransfer.DataMode.DataModeReadFlash;
                        tData.dataBuffer = new byte[16777216];
                        tData.localAddress = 0;
                        tData.flashAddress = 0;
                        tData.transferLength = tData.dataBuffer.Length;
                        MainForm.logMessage("Начато чтение флеша...");
                        if (!ReadFlash(COMPort.Port, tData))
                        {
                            MainForm.logMessage("Ошибка при чтении флеша!");
                            tData.responseCode = 1;
                        }
                        sendCommand(COMPort.Port, CPSCommand.CloseUI);
                        sendCommand(COMPort.Port, CPSCommand.RestartGPS);
                        COMPort.Port.Close();
                        COMPort.Port = null;
                        break;

                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message);
            }
            e.Result = tData;
        }

        private static bool _readRadioInfo(SerialPort port, DataTransfer dataObj)
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

        private static RadioInfo ByteArrayToRadioInfo(byte[] bytes)
        {
            GCHandle gCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (RadioInfo)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(RadioInfo));
            }
            finally
            {
                gCHandle.Free();
            }
        }

        public static RadioInfo ReadRadioInfo(SerialPort port)
        {
            DataTransfer COMData = new DataTransfer();
            COMData.mode = DataTransfer.DataMode.DataModeReadRadioInfo;
            COMData.localAddress = 0;
            COMData.transferLength = 0;
            COMData.dataBuffer = new byte[128];
            RadioInfo info = default(RadioInfo);
            if (_readRadioInfo(port, COMData))
            {
                info = ByteArrayToRadioInfo(COMData.dataBuffer);
            }
            return info;
        }
    }
}
