using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Logic
{
    public class DiagnosticLogger : IDisposable
    {
        private static readonly BlockingCollection<string> _logs = new BlockingCollection<string>();
        private static readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appLog.json");
        private static Thread _logThread;
        private static bool _isRunning = false;
        private static readonly object _lock = new object();
        private static readonly System.Timers.Timer _timer;
        static DiagnosticLogger()
        {

            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "[]");
            }
            _timer = new System.Timers.Timer(10000);
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();

        }

        public static void Log(string message)
        {
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ff");
            _logs.Add($"{{\"timestamp\": \"{currentTime}\", \"message\": \"{message}\"}}\n");

        }

        private static void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (_lock)
            {
                if (!_isRunning)
                {
                    _isRunning = true;
                    _logThread = new Thread(WriteLog);
                    _logThread.Start();
                }
            }
        }

        private static void WriteLog()
        {
            while (_logs.TryTake(out var log))
            {
                try
                {


                    var jsonData = File.ReadAllText(_path);

                    // Przekształć zawartość pliku do JArray
                    var jsonArray = JArray.Parse(jsonData);


                    // Dodaj nowe obiekty do tablicy JSON
                    var jsonObject = JObject.Parse(log);

                    jsonArray.Add(jsonObject);


                    // Zapisz zaktualizowaną tablicę JSON do pliku
                    File.WriteAllText(_path, jsonArray.ToString(Formatting.Indented));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas zapisu logu do pliku: {ex.Message}");
                }
            }
            
            

            _isRunning = false;
        }

        public void Dispose()
        {
            _logs.CompleteAdding();
            _logThread?.Join();
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}
