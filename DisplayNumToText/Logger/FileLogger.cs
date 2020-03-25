using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace DisplayNumToText.Logger
{
    class FileLogger
    {
        string filePath = string.Empty;
        private static FileLogger instance = null;
        protected readonly object lockObj = new object();

        private FileLogger() { }

        public static FileLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileLogger();
                }

                return instance;
            }
        }


        public void LogToFile(string logMessage)
        {
            var dirPath = $@"{ConfigurationManager.AppSettings["LogFileDir"].ToString()}";
            var fileName = ConfigurationManager.AppSettings["LogFileName"].ToString();

            lock (lockObj)
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                using (StreamWriter writer = File.AppendText(($@"{dirPath}\\{fileName}")))
                {
                    var message = $"{DateTime.Now}: {logMessage}";
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
        }
    }
}