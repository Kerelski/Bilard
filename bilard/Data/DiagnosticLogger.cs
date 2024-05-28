using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Logic
{
    public class DiagnosticLogger : IDisposable
    {
        private static readonly BlockingCollection<string> _logs = new BlockingCollection<string>();
        private static readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appLog.json");
        private static Thread _logThread;
        private static bool _isRunning = false;
        private static readonly object _lock = new object();
        
        static DiagnosticLogger()
        {
             
                File.WriteAllText(_path, "");
            
        }

        public static void Log(string message)
        {
            _logs.Add(message);

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
            while(_logs.TryTake(out var log))
            {
                try
                {
                    using (StreamWriter file = File.AppendText(_path))
                    {
                        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Format daty i czasu
                        file.WriteLine($"{{\"timestamp\": \"{currentTime}\", \"message\": \"{log}\"}}");
                    }
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
        }
    }
}
