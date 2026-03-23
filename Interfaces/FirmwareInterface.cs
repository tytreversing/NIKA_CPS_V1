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
            RestoreCalibrations = 88,
            SetCodeplugWritten = 99,
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

        private const int  BUFFER_SIZE = 1032;
        private const byte CPS_WRITE = 0x57;
        private const byte CPS_READ = 0x52;
        private const byte CPS_COMMAND = 0x43;
        private const int  DEFAULT_BLOCK_SIZE = 1024;
        private const int  SECTOR_SIZE = 4096;
        private const int  HEADER_SIZE = 8;

        public static bool SendCommand(SerialPort port, CPSCommand commandNumber, int x_or_command_option_number = 0, int y = 0, int iSize = 0, int alignment = 0, int isInverted = 0, string message = "")
        {
            Encoding transcoder = Encoding.GetEncoding("windows-1251");
            int num = 100;
            byte[] buffer = new byte[BUFFER_SIZE];
            int num2 = 2;
            buffer[0] = (byte)CPS_COMMAND;
            buffer[1] = (byte)commandNumber;
            switch (commandNumber)
            {
                case CPSCommand.WriteString:
                    buffer[3] = (byte)y;
                    buffer[4] = (byte)iSize;
                    buffer[5] = (byte)alignment;
                    buffer[6] = (byte)isInverted;
                    num2 += 5 + Math.Min(message.Length, 16);
                    Buffer.BlockCopy(transcoder.GetBytes(message), 0, buffer, 7, num2 - 7);
                    break;
                case CPSCommand.Finish:
                case CPSCommand.RestoreSettings:
                case CPSCommand.RestoreCalibrations:
                    buffer[2] = (byte)x_or_command_option_number;
                    num2++;
                    break;

            }
            port.Write(buffer, 0, num2);
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
                port.Read(buffer, 0, port.BytesToRead);
            }
            if (buffer[1] == (byte)commandNumber)
            {
                return num != -1;
            }
            return false;
        }

        public static bool flashWritePrepareSector(SerialPort port, byte writeSign, int address, ref byte[] sendbuffer, ref byte[] readbuffer, DataTransfer dataObj)
        {
            int waitCounter = 100;
            dataObj.dataSector = address / SECTOR_SIZE;
            sendbuffer[0] = (byte)writeSign;
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
            while (port.BytesToRead == 0 && waitCounter-- > 0)
            {
                Thread.Sleep(1);
            }
            if (waitCounter != -1)
            {
                port.Read(readbuffer, 0, port.BytesToRead);
            }
            if (readbuffer[0] == sendbuffer[0] && readbuffer[1] == sendbuffer[1])
            {
                return waitCounter != -1;
            }
            return false;
        }

        public static bool flashSendData(SerialPort port, byte writeSign, int address, int len, ref byte[] sendbuffer, ref byte[] readbuffer)
        {
            int num = 100;
            sendbuffer[0] = (byte)writeSign;
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

        public static bool flashWriteSector(SerialPort port, byte writeSign, ref byte[] sendbuffer, ref byte[] readbuffer, DataTransfer dataObj)
        {
            int num = 100;
            dataObj.dataSector = -1;
            sendbuffer[0] = writeSign;
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

        public static bool WriteFlash(SerialPort port, DataTransfer dataObj, int dataBlockSize = DEFAULT_BLOCK_SIZE)
        {
            // Инициализация переменных с понятными именами
            int previousProgressPercent = 0;
            int currentFlashAddress = dataObj.flashAddress;
            int currentDataBufferPosition = dataObj.localAddress;

            byte[] sendBuffer = new byte[BUFFER_SIZE];
            byte[] receiveBuffer = new byte[BUFFER_SIZE];

            // Индикатор того, что сектор ещё не открыт
            dataObj.dataSector = -1;

            // Основной цикл записи - пока есть данные для отправки
            int remainingBytesToWrite = dataObj.flashAddress + dataObj.transferLength - currentFlashAddress;

            while (remainingBytesToWrite > 0)
            {
                // Определяем размер текущего блока (не больше максимального размера блока)
                int currentBlockSize = remainingBytesToWrite;
                if (currentBlockSize > dataBlockSize)
                {
                    currentBlockSize = dataBlockSize;
                }

                // Если сектор ещё не подготовлен - подготавливаем его
                if (dataObj.dataSector == -1)
                {
                    bool prepareSuccess = flashWritePrepareSector(
                        port, CPS_WRITE, currentFlashAddress,
                        ref sendBuffer, ref receiveBuffer, dataObj);

                    if (!prepareSuccess)
                    {
                        MessageBox.Show(
                            $"Ошибка: не удалось подготовить сектор по адресу {currentFlashAddress:X8}",
                            "Ошибка!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Пропускаем запись, если режим не позволяет
                if (dataObj.mode == 0)
                {
                    // Обновляем оставшееся количество байт для следующей итерации
                    remainingBytesToWrite = dataObj.flashAddress + dataObj.transferLength - currentFlashAddress;
                    continue;
                }

                // Копируем данные в буфер отправки, следя за границами сектора
                int bytesCopied = 0;
                int localBufferPosition = currentDataBufferPosition;

                for (int i = 0; i < currentBlockSize; i++)
                {
                    // Копируем байт данных в буфер (со смещением для заголовка)
                    sendBuffer[i + HEADER_SIZE] = dataObj.dataBuffer[localBufferPosition];
                    localBufferPosition++;
                    bytesCopied++;

                    // Проверяем, не перешли ли мы границу сектора (SECTOR_SIZE байт)
                    int nextAddress = currentFlashAddress + bytesCopied;
                    int newSectorNumber = nextAddress / SECTOR_SIZE;

                    if (dataObj.dataSector != newSectorNumber)
                    {
                        // Достигли границы сектора - останавливаем копирование
                        break;
                    }
                }

                // Отправляем скопированные данные
                bool sendSuccess = flashSendData(
                    port, CPS_WRITE, currentFlashAddress, bytesCopied,
                    ref sendBuffer, ref receiveBuffer);

                if (!sendSuccess)
                {
                    MessageBox.Show(
                        $"Ошибка: не удалось отправить данные по адресу {currentFlashAddress:X8} (размер: {bytesCopied} байт)",
                        "Ошибка!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }

                // Обновляем прогресс операции (проценты)
                int currentProgressPercent = (currentFlashAddress - dataObj.flashAddress) * 100 / dataObj.transferLength;

                if (previousProgressPercent != currentProgressPercent)
                {
                    previousProgressPercent = currentProgressPercent;
                    MainForm.setProgress(currentProgressPercent);
                }

                // Обновляем текущие позиции
                currentFlashAddress += bytesCopied;
                currentDataBufferPosition = localBufferPosition;

                // Проверяем, не перешли ли мы в новый сектор
                int currentSectorNumber = currentFlashAddress / SECTOR_SIZE;

                if (dataObj.dataSector != currentSectorNumber)
                {
                    // Перешли в новый сектор - завершаем запись текущего сектора
                    bool sectorWriteSuccess = flashWriteSector(
                        port, CPS_WRITE, ref sendBuffer, ref receiveBuffer, dataObj);

                    if (!sectorWriteSuccess)
                    {
                        MessageBox.Show(
                            $"Ошибка: не удалось записать сектор при переходе по адресу {currentFlashAddress:X8}",
                            "Ошибка!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Обновляем оставшееся количество байт для следующей итерации
                remainingBytesToWrite = dataObj.flashAddress + dataObj.transferLength - currentFlashAddress;
            }

            // Завершаем последний сектор, если он был открыт
            if (dataObj.dataSector != -1)
            {
                bool lastSectorWriteSuccess = flashWriteSector(
                    port, CPS_WRITE, ref sendBuffer, ref receiveBuffer, dataObj);

                if (!lastSectorWriteSuccess)
                {
                    MessageBox.Show(
                        $"Ошибка: запись прервана при завершении последнего сектора (адрес: {currentFlashAddress:X8})",
                        "Ошибка!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            return true; // Успешное завершение
        }

        public bool WriteLoFlash(SerialPort port, DataTransfer dataObj)
        {
            int num = 0;
            byte[] array = new byte[BUFFER_SIZE];
            byte[] array2 = new byte[BUFFER_SIZE];
            int num2 = dataObj.flashAddress;
            int localDataBufferStartPosition = dataObj.localAddress;
            int num3 = dataObj.flashAddress + dataObj.transferLength - num2;
            while (num3 > 0)
            {
                if (num3 > DEFAULT_BLOCK_SIZE)
                {
                    num3 = DEFAULT_BLOCK_SIZE;
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
                array[0] = CPS_WRITE;
                array[1] = (byte)DataTransfer.DataMode.WriteLoFlash;
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

 public static bool ReadFlash(SerialPort port, DataTransfer transferData)
{
    int lastProgressPercent = 0;
    byte[] sendBuffer = new byte[BUFFER_SIZE];
    byte[] readBuffer = new byte[BUFFER_SIZE];
    
    int currentFlashAddress = transferData.flashAddress;
    int localBufferPosition = transferData.localAddress;
    int remainingBytes = transferData.flashAddress + transferData.transferLength - currentFlashAddress;
    
    while (remainingBytes > 0)
    {
        int timeoutCounter = 100;
        int bytesToRead = 100;
        
        if (remainingBytes >= DEFAULT_BLOCK_SIZE)
        {
            bytesToRead = DEFAULT_BLOCK_SIZE;
        }

        MainForm.logMessage("Чтение по адресу 0x" + currentFlashAddress.ToString("X"));
        
        // Формирование команды чтения
        sendBuffer[0] = CPS_READ;
        sendBuffer[1] = (byte)transferData.mode;
        sendBuffer[2] = (byte)((currentFlashAddress >> 24) & 0xFF);
        sendBuffer[3] = (byte)((currentFlashAddress >> 16) & 0xFF);
        sendBuffer[4] = (byte)((currentFlashAddress >> 8) & 0xFF);
        sendBuffer[5] = (byte)(currentFlashAddress & 0xFF);
        sendBuffer[6] = (byte)((bytesToRead >> 8) & 0xFF);
        sendBuffer[7] = (byte)(bytesToRead & 0xFF);
        
        port.Write(sendBuffer, 0, 8);
        
        // Ожидание завершения отправки
        while (port.BytesToWrite > 0)
        {
            Thread.Sleep(1);
        }
        
        // Ожидание ответа с таймаутом
        while (port.BytesToRead == 0 && timeoutCounter-- > 0)
        {
            Thread.Sleep(5);
        }
        
        if (timeoutCounter == -1)
        {
            return false; // Таймаут
        }
        
        port.Read(readBuffer, 0, port.BytesToRead);
        
        if (readBuffer[0] == CPS_READ)
        {
            int receivedDataLength = (readBuffer[1] << 8) + readBuffer[2];
            
            // Копирование полученных данных
            for (int i = 0; i < receivedDataLength; i++)
            {
                transferData.dataBuffer[localBufferPosition++] = readBuffer[i + 3];
            }
            int currentProgress = (currentFlashAddress - transferData.flashAddress) * 100 / transferData.transferLength;
            if (lastProgressPercent != currentProgress)
            {
                lastProgressPercent = currentProgress;
                MainForm.setProgress(currentProgress);
            }
            
            currentFlashAddress += receivedDataLength;
            remainingBytes = transferData.flashAddress + transferData.transferLength - currentFlashAddress;
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
                                    RegistryOperations.WriteString("LastFlashBackupFolder", Path.GetDirectoryName(sfdFlash.FileName));
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
                        if (!SendCommand(COMPort.Port, 0))
                        {
                            MessageBox.Show("Ошибка при соединении с COM-портом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tData.responseCode = 1;
                            COMPort.Port = null;
                            break;
                        }
                        SendCommand(COMPort.Port, CPSCommand.ClearScreen);
                        SendCommand(COMPort.Port, CPSCommand.WriteString, 0, 0, 3, 1, 0, "Бэкап");
                        SendCommand(COMPort.Port, CPSCommand.WriteString, 0, 16, 3, 1, 0, "флеш-памяти");
                        SendCommand(COMPort.Port, CPSCommand.UpdateScreen);
                        SendCommand(COMPort.Port, CPSCommand.Finish, 3);
                        tData.mode = DataTransfer.DataMode.ReadFlash;
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
                        SendCommand(COMPort.Port, CPSCommand.CloseUI);
                        SendCommand(COMPort.Port, CPSCommand.RestartGPS);
                        COMPort.Port.Close();
                        COMPort.Port = null;
                        break;

                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MainForm.logMessage(ex.Message);
            }
            e.Result = tData;
        }

        private static bool _readRadioInfo(SerialPort port, DataTransfer dataObj)
        {
            byte[] send = new byte[BUFFER_SIZE];
            byte[] receive = new byte[BUFFER_SIZE];
            int num = 0;
            send[0] = CPS_READ;
            send[1] = (byte)dataObj.mode;
            send[2] = 0;
            send[3] = 0;
            send[4] = 0;
            send[5] = 0;
            send[6] = 0;
            send[7] = 0;
            port.Write(send, 0, 8);
            while (port.BytesToWrite > 0)
            {
                Thread.Sleep(1);
            }
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(5);
            }
            port.Read(receive, 0, port.BytesToRead);
            if (receive[0] == CPS_READ)
            {
                int num2 = (receive[1] << 8) + receive[2];
                for (int i = 0; i < num2; i++)
                {
                    dataObj.dataBuffer[num++] = receive[i + 3];
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
            COMData.mode = DataTransfer.DataMode.ReadRadioInfo;
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

        public static CalibrationData ReadCalibrations(SerialPort port)
        {
            byte[] cmd = new byte[BUFFER_SIZE];
            byte[] resp = new byte[BUFFER_SIZE];
            byte[] data = new byte[BUFFER_SIZE];
            int idx = 0;
            cmd[0] = CPS_READ;
            cmd[1] = (byte)DataTransfer.DataMode.ReadCalibrations;
            for (int i = 2; i < 8; i++)
            {
                cmd[i] = 0;
            }
            port.Write(cmd, 0, 8);
            while (port.BytesToWrite > 0)
            {
                Thread.Sleep(1);
            }
            while (port.BytesToRead == 0)
            {
                Thread.Sleep(5);
            }
            port.Read(resp, 0, port.BytesToRead);
            if (resp[0] == CPS_READ)
            {
                int len = (resp[1] << 8) + resp[2];
                for (int i = 0; i < len; i++)
                {
                    data[idx++] = resp[i + 3];
                }
                return ByteArrayToCalData(data);
            }
            return null;
        }
    }
}
