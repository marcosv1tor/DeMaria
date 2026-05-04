using System;
using System.IO;
using GestaoOS.Application.Interfaces;

namespace GestaoOS.Infrastructure.Logging
{
    public class FileAppLogger : IAppLogger
    {
        private readonly string _directory;

        public FileAppLogger()
            : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"))
        {
        }

        public FileAppLogger(string directory)
        {
            _directory = directory;
        }

        public void Info(string message)
        {
            Write("INFO", message, null);
        }

        public void Error(Exception exception, string message)
        {
            Write("ERROR", message, exception);
        }

        private void Write(string level, string message, Exception exception)
        {
            Directory.CreateDirectory(_directory);
            var path = Path.Combine(_directory, DateTime.Now.ToString("yyyyMMdd") + ".log");
            using (var writer = new StreamWriter(path, true))
            {
                writer.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} [{1}] {2}", DateTime.Now, level, message);
                if (exception != null)
                {
                    writer.WriteLine(exception);
                }
            }
        }
    }
}
