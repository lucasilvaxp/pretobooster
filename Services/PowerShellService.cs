using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace PretoBoost.Services
{
    public static class PowerShellService
    {
        public static (bool Success, string Output, string Error) ExecuteCommand(string command, bool elevated = true)
        {
            try
            {
                LogService.Log($"Executando PowerShell: {command}");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                if (elevated)
                {
                    psi.Verb = "runas";
                }

                using Process? process = Process.Start(psi);
                if (process == null)
                {
                    return (false, "", "Falha ao iniciar o processo PowerShell");
                }

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit(30000); // 30 segundos timeout

                bool success = process.ExitCode == 0 && string.IsNullOrEmpty(error);

                if (success)
                {
                    LogService.LogSuccess($"PowerShell: {command.Substring(0, Math.Min(50, command.Length))}...");
                }
                else if (!string.IsNullOrEmpty(error))
                {
                    LogService.LogError($"PowerShell: {command.Substring(0, Math.Min(50, command.Length))}...", error);
                }

                return (success, output, error);
            }
            catch (Exception ex)
            {
                LogService.LogError("PowerShellService.ExecuteCommand", ex.Message);
                return (false, "", ex.Message);
            }
        }

        public static async Task<(bool Success, string Output, string Error)> ExecuteCommandAsync(string command, bool elevated = true)
        {
            return await Task.Run(() => ExecuteCommand(command, elevated));
        }

        public static bool ExecuteScript(string scriptPath)
        {
            try
            {
                var result = ExecuteCommand($"& '{scriptPath}'");
                return result.Success;
            }
            catch (Exception ex)
            {
                LogService.LogError($"ExecuteScript: {scriptPath}", ex.Message);
                return false;
            }
        }
    }
}
