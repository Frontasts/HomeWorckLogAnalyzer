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
        private const string _ReportFileName = "report.txt";

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
                        AnalyzeLog();
                        break;

                    case CreateSampleCommand:
                        SaveReport();
                        break;

                    case SaveReportCommand:
                        CreateSampleLog();
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

        private void AnalyzeLog()
        {
            Console.WriteLine("--- Анализ лог-файла ---");

            if (_analyzer.LogFileExists() == false)
            {
                Console.WriteLine("Лог-файл не найден. Сначала создайте пример (пункт 2).");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }

            List<string> lines = _analyzer.ReadLog();

            Console.WriteLine($"Всего строк в логе: {lines.Count}");
            Console.WriteLine();
            Console.WriteLine("Статистика по событиям:");

            Dictionary<string, int> stats = _analyzer.BuildStatistics(lines, _keywords);

            foreach (KeyValuePair<string, int> pair in stats)
            {
                Console.WriteLine($"  {pair.Key}: {pair.Value}");
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private void SaveReport()
        {
            Console.WriteLine("--- Сохранение отчёта ---");

            if (_analyzer.LogFileExists() == false)
            {
                Console.WriteLine("Лог-файл не найден. Сначала создайте пример (пункт 2).");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();

                return;
            }

            List<string> lines = _analyzer.ReadLog();
            Dictionary<string, int> stats = _analyzer.BuildStatistics(lines, _keywords);

            StringBuilder report = new StringBuilder();

            report.AppendLine("=== Отчёт по анализу логов ===");
            report.AppendLine($"Дата формирования отчёта: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine($"Всего строк: {lines.Count}");
            report.AppendLine();
            report.AppendLine("Статистика по событиям:");

            foreach (KeyValuePair<string, int> pair in stats)
            {
                report.AppendLine($"  {pair.Key}: {pair.Value}");
            }

            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _ReportFileName);

            _analyzer.SaveReport(reportPath, report.ToString());

            Console.WriteLine($"Отчёт сохранён в файл:");
            Console.WriteLine(reportPath);
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private void CreateSampleLog()
        {
            Console.WriteLine("--- Создание примера лог-файла ---");

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string logPath = Path.Combine(baseDir, "server.log");

            string[] sampleLines = new string[]
            {
                "2025-01-15 08:00:01 INFO Сервер запущен",
                "2025-01-15 08:00:05 INFO Загружена конфигурация",
                "2025-01-15 08:01:12 DEBUG Проверка соединения с БД",
                "2025-01-15 08:01:13 INFO Соединение с БД установлено",
                "2025-01-15 08:05:42 WARNING Медленный запрос: 2.5 сек",
                "2025-01-15 08:10:00 ERROR Не удалось обработать запрос клиента #1024",
                "2025-01-15 08:10:01 INFO Повторная попытка обработки",
                "2025-01-15 08:15:30 WARNING Превышен лимит подключений",
                "2025-01-15 08:20:11 ERROR Таймаут при обращении к внешнему API",
                "2025-01-15 08:25:00 INFO Плановое обслуживание завершено",
                "2025-01-15 08:30:45 CRITICAL Сбой диска на узле storage-2",
                "2025-01-15 08:30:46 ERROR Невозможно записать данные",
                "2025-01-15 08:31:00 INFO Переключение на резервный узел",
                "2025-01-15 08:35:22 WARNING Высокая загрузка CPU: 92%",
                "2025-01-15 08:40:00 INFO Система работает в штатном режиме"
            };

            File.WriteAllLines(logPath, sampleLines, Encoding.UTF8);

            Console.WriteLine($"Пример лог-файла создан:");
            Console.WriteLine(logPath);
            Console.WriteLine($"Строк: {sampleLines.Length}");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}