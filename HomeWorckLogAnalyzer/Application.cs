using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorckLogAnalyzer
{
    public class Application
    {
        private LogAnalyzer _analyzer;
        private List<string> _keywords;

        public Application()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string logPath = Path.Combine(baseDir, "server.log");

            _analyzer = new LogAnalyzer(logPath);

            _keywords = new List<string>
            {
                "ERROR",
                "WARNING",
                "INFO",
                "DEBUG",
                "CRITICAL"
            };
        }

        public void Run()
        {

        }
    }
}
