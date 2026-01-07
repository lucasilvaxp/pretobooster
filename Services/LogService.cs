using System;
using System.IO;

namespace PretoBoost.Services
{
    public static class LogService
    {
        private static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "tweaks.log");
        private static readonly object _lock = new object();

        static LogService()
        {
            var logDir = Path.GetDirectoryName(LogPath);
            if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }

        public static void Log(string message, string type = "INFO")
        {
            try
            {
                lock (_lock)
                {
                    string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}] {message}";
                    File.AppendAllText(LogPath, logEntry + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error writing to log: {ex.Message}");
            }
        }

        public static void LogSuccess(string action)
        {
            Log($"✓ {action} aplicado com sucesso", "SUCCESS");
        }

        public static void LogError(string action, string error)
        {
            Log($"✗ Erro ao aplicar {action}: {error}", "ERROR");
        }

        public static void LogRevert(string action)
        {
            Log($"↩ {action} revertido", "REVERT");
        }
    }
}
