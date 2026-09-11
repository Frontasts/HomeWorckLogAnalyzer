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

        public List<string> ReadLog()
        {
            List<string> lines = new List<string>();
            string[] rawLines = File.ReadAllLines(_logFilePath, Encoding.UTF8);
            lines.AddRange(rawLines);

            return lines;
        }

        public bool LogFileExists()
        {
            return File.Exists(_logFilePath);
        }
    }
}
