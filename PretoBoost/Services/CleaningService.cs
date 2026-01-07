using System;
using System.IO;

namespace PretoBoost.Services
{
    public static class CleaningService
    {
        private static readonly string TempPath = Path.GetTempPath();
        private static readonly string WindowsTemp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp");

        public static void CleanTempFiles()
        {
            try
            {
                LogService.Log("Iniciando limpeza de arquivos temporários...");
                
                int deletedCount = 0;
                
                // Limpar pasta Temp do usuário
                deletedCount += CleanDirectory(TempPath);
                
                // Limpar pasta Temp do Windows
                deletedCount += CleanDirectory(WindowsTemp);
                
                LogService.LogSuccess($"Arquivos temporários limpos: {deletedCount} arquivos removidos");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de Temp", ex.Message);
            }
        }

        public static void CleanWindowsLogs()
        {
            try
            {
                LogService.Log("Iniciando limpeza de logs do Windows...");
                
                int deletedCount = 0;
                
                // Limpar logs IIS
                string iisLogs = @"C:\inetpub\logs";
                if (Directory.Exists(iisLogs))
                {
                    deletedCount += CleanDirectory(iisLogs);
                }
                
                // Limpar logs do Windows
                string windowsLogs = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Logs");
                deletedCount += CleanDirectory(windowsLogs, "*.log");
                
                LogService.LogSuccess($"Logs do Windows limpos: {deletedCount} arquivos removidos");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de Logs", ex.Message);
            }
        }

        public static void CleanPrefetchCache()
        {
            try
            {
                LogService.Log("Iniciando limpeza do Prefetch...");
                
                string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
                int deletedCount = CleanDirectory(prefetchPath);
                
                LogService.LogSuccess($"Cache Prefetch limpo: {deletedCount} arquivos removidos");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de Prefetch", ex.Message);
            }
        }

        public static void CleanBSODMinidumps()
        {
            try
            {
                LogService.Log("Iniciando limpeza de minidumps BSOD...");
                
                string minidumpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Minidump");
                int deletedCount = CleanDirectory(minidumpPath);
                
                // Limpar memory.dmp se existir
                string memoryDmp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "MEMORY.DMP");
                if (File.Exists(memoryDmp))
                {
                    try { File.Delete(memoryDmp); deletedCount++; } catch { }
                }
                
                LogService.LogSuccess($"BSOD Minidumps limpos: {deletedCount} arquivos removidos");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de Minidumps", ex.Message);
            }
        }

        public static void CleanErrorReports()
        {
            try
            {
                LogService.Log("Iniciando limpeza de relatórios de erro...");
                
                int deletedCount = 0;
                
                // Relatórios de erro do usuário
                string userReports = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\Windows\\WER");
                deletedCount += CleanDirectory(userReports);
                
                // Relatórios de erro do sistema
                string systemReports = @"C:\ProgramData\Microsoft\Windows\WER";
                deletedCount += CleanDirectory(systemReports);
                
                LogService.LogSuccess($"Relatórios de erro limpos: {deletedCount} arquivos removidos");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de Error Reports", ex.Message);
            }
        }

        public static void EmptyRecycleBin()
        {
            try
            {
                LogService.Log("Esvaziando Lixeira...");
                
                PowerShellService.ExecuteCommand("Clear-RecycleBin -Force -ErrorAction SilentlyContinue");
                
                LogService.LogSuccess("Lixeira esvaziada");
            }
            catch (Exception ex)
            {
                LogService.LogError("Esvaziar Lixeira", ex.Message);
            }
        }

        public static void CleanMediaPlayersCache()
        {
            try
            {
                LogService.Log("Iniciando limpeza de cache de media players...");
                
                int deletedCount = 0;
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                
                // Windows Media Player
                string wmpCache = Path.Combine(localAppData, "Microsoft\\Media Player");
                deletedCount += CleanDirectory(wmpCache, "*.wmdb");
                
                // VLC
                string vlcCache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "vlc");
                if (Directory.Exists(vlcCache))
                {
                    string vlcArtCache = Path.Combine(vlcCache, "art");
                    deletedCount += CleanDirectory(vlcArtCache);
                }
                
                LogService.LogSuccess($"Cache de media players limpo: {deletedCount} arquivos removidos");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de Media Players Cache", ex.Message);
            }
        }

        public static void CleanUTorrentCache()
        {
            try
            {
                LogService.Log("Iniciando limpeza de cache do uTorrent...");
                
                int deletedCount = 0;
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                
                // uTorrent
                string utorrentPath = Path.Combine(appData, "uTorrent");
                if (Directory.Exists(utorrentPath))
                {
                    deletedCount += CleanDirectory(utorrentPath, "*.dat");
                    deletedCount += CleanDirectory(utorrentPath, "*.old");
                }
                
                // BitTorrent
                string bittorrentPath = Path.Combine(appData, "BitTorrent");
                if (Directory.Exists(bittorrentPath))
                {
                    deletedCount += CleanDirectory(bittorrentPath, "*.dat");
                    deletedCount += CleanDirectory(bittorrentPath, "*.old");
                }
                
                LogService.LogSuccess($"Cache do uTorrent limpo: {deletedCount} arquivos removidos");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de uTorrent Cache", ex.Message);
            }
        }

        public static void CleanFileZillaRecentServers()
        {
            try
            {
                LogService.Log("Iniciando limpeza de servidores recentes do FileZilla...");
                
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string filezillaPath = Path.Combine(appData, "FileZilla");
                
                if (Directory.Exists(filezillaPath))
                {
                    string recentServers = Path.Combine(filezillaPath, "recentservers.xml");
                    if (File.Exists(recentServers))
                    {
                        File.Delete(recentServers);
                        LogService.LogSuccess("Servidores recentes do FileZilla limpos");
                        return;
                    }
                }
                
                LogService.Log("FileZilla não encontrado ou sem servidores recentes");
            }
            catch (Exception ex)
            {
                LogService.LogError("Limpeza de FileZilla", ex.Message);
            }
        }

        public static void CleanAll()
        {
            LogService.Log("Iniciando limpeza completa...");
            
            CleanTempFiles();
            CleanWindowsLogs();
            CleanPrefetchCache();
            CleanBSODMinidumps();
            CleanErrorReports();
            EmptyRecycleBin();
            CleanMediaPlayersCache();
            CleanUTorrentCache();
            CleanFileZillaRecentServers();
            
            LogService.LogSuccess("Limpeza completa finalizada");
        }

        private static int CleanDirectory(string path, string pattern = "*")
        {
            int deletedCount = 0;
            
            if (!Directory.Exists(path)) return 0;
            
            try
            {
                // Deletar arquivos
                foreach (string file in Directory.GetFiles(path, pattern, SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Delete(file);
                        deletedCount++;
                    }
                    catch { /* Arquivo em uso, pular */ }
                }
                
                // Deletar pastas vazias
                foreach (string dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        if (Directory.GetFiles(dir).Length == 0 && Directory.GetDirectories(dir).Length == 0)
                        {
                            Directory.Delete(dir);
                        }
                    }
                    catch { /* Pasta em uso, pular */ }
                }
            }
            catch { /* Acesso negado ao diretório principal */ }
            
            return deletedCount;
        }
    }
}
