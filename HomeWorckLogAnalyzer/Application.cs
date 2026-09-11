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
            const string AnalyzeCommand = "1";
            const string CreateSampleCommand = "2";
            const string SaveReportCommand = "3";
            const string ExitCommand = "4";

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Анализатор логов ===");
                Console.WriteLine($"{AnalyzeCommand}. Проанализировать лог-файл");
                Console.WriteLine($"{CreateSampleCommand}. Создать пример лог-файла");
                Console.WriteLine($"{SaveReportCommand}. Сохранить отчёт в файл");
                Console.WriteLine($"{ExitCommand}. Выход");
                Console.Write("Выберите действие: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case AnalyzeCommand:

                        break;

                    case CreateSampleCommand:

                        break;

                    case SaveReportCommand:

                        break;

                    case ExitCommand:
                        running = false;
                        Console.WriteLine("Программа завершена.");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
