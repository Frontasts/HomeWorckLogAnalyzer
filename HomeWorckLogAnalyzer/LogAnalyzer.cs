using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorckLogAnalyzer
{
    public class LogAnalyzer
    {
        private string _logFilePath;

        public LogAnalyzer(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public bool LogFileExists()
        {
            return File.Exists(_logFilePath);
        }
    }
}
