using PaymentProcessingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingSystem.Infrastructure.Logging
{
    public class FileAppLogger : IAppLogger
    {
        private readonly string _filePath;

        private string _GetLogsFilePath()
        {
            //extract the logic to a methode 
            var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!
                                       .Parent!
                                       .Parent!
                                       .Parent!
                                       .FullName;
            var logDirectory = Path.Combine(projectRoot, "logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            return Path.Combine(logDirectory, "app.log");
        }
        public FileAppLogger()
        {
            _filePath = this._GetLogsFilePath();
        }

        
        public void LogInfo(string message)
        {
            WriteToFile("[INFO]", message);
        }

        public void LogWarning(string message)
        {
            WriteToFile("[WARNING]", message);
        }

        public void LogError(string message)
        {
            WriteToFile("[ERROR]", message);
        }

        private void WriteToFile(string level, string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {level} {message}";
            File.AppendAllText(_filePath, logMessage + Environment.NewLine);
        }
    }
}
