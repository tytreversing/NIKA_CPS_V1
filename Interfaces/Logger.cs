using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace NIKA_CPS_V1
{
    public class Logger
    {
        private readonly StreamWriter _writer;
        private bool _disposed = false;

        /// <summary>
        /// Создает новый логгер с указанным именем файла
        /// </summary>
        /// <param name="fileName">Имя файла лога (без пути)</param>
        public Logger(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Имя файла не может быть пустым", nameof(fileName));

            // Получаем путь к папке с исполняемым файлом
            string logDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logPath = Path.Combine(logDirectory, fileName);

            // Создаем StreamWriter с перезаписью существующего файла
            _writer = new StreamWriter(logPath, false, Encoding.UTF8)
            {
                AutoFlush = false // Будем сами контролировать запись
            };

            WriteApplicationInfo();
        }

        private void WriteApplicationInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Полный путь к исполняемому файлу
            string appPath = assembly.Location;
            _writer.WriteLine($"Исполняемый файл: {appPath}");

            // Версия приложения
            string version = assembly.GetName().Version?.ToString() ?? "версия не определена";
            _writer.WriteLine($"Версия: {version}");

            // Дата и время запуска
            _writer.WriteLine($"Время запуска: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _writer.WriteLine(new string('=', 50));
        }

        public void Add(string message)
        {
            if (_disposed)
                throw new ObjectDisposedException("Logger", "Логгер уже закрыт");

            _writer.WriteLine(message);
        }

        /// <summary>
        /// Закрывает логгер и сохраняет файл
        /// </summary>
        public void Close()
        {
            if (!_disposed)
            {
                _writer.Flush();
                _writer.Close();
                _disposed = true;
            }
        }


        // Финализатор на случай, если забыли вызвать Dispose
        ~Logger()
        {
            Close();
        }
    }
}
