using System;
using System.ServiceProcess;

namespace PretoBoost.Services
{
    public static class ServiceManager
    {
        public static bool StopService(string serviceName)
        {
            try
            {
                LogService.Log($"Service: Parando {serviceName}");

                using ServiceController sc = new ServiceController(serviceName);
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                }

                LogService.LogSuccess($"Service Stop: {serviceName}");
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Service Stop: {serviceName}", ex.Message);
                return false;
            }
        }

        public static bool StartService(string serviceName)
        {
            try
            {
                LogService.Log($"Service: Iniciando {serviceName}");

                using ServiceController sc = new ServiceController(serviceName);
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                }

                LogService.LogSuccess($"Service Start: {serviceName}");
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Service Start: {serviceName}", ex.Message);
                return false;
            }
        }

        public static bool DisableService(string serviceName)
        {
            try
            {
                LogService.Log($"Service: Desabilitando {serviceName}");

                StopService(serviceName);

                // Usando PowerShell para alterar o tipo de inicialização
                var result = PowerShellService.ExecuteCommand($"Set-Service -Name '{serviceName}' -StartupType Disabled");

                if (result.Success)
                {
                    LogService.LogSuccess($"Service Disable: {serviceName}");
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Service Disable: {serviceName}", ex.Message);
                return false;
            }
        }

        public static bool EnableService(string serviceName, string startupType = "Automatic")
        {
            try
            {
                LogService.Log($"Service: Habilitando {serviceName}");

                var result = PowerShellService.ExecuteCommand($"Set-Service -Name '{serviceName}' -StartupType {startupType}");

                if (result.Success)
                {
                    LogService.LogSuccess($"Service Enable: {serviceName}");
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Service Enable: {serviceName}", ex.Message);
                return false;
            }
        }

        public static bool SetStartupType(string serviceName, string startupType)
        {
            try
            {
                LogService.Log($"Service: Alterando tipo de inicialização de {serviceName} para {startupType}");

                var result = PowerShellService.ExecuteCommand($"Set-Service -Name '{serviceName}' -StartupType {startupType}");
                return result.Success;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Service SetStartupType: {serviceName}", ex.Message);
                return false;
            }
        }

        public static ServiceControllerStatus? GetServiceStatus(string serviceName)
        {
            try
            {
                using ServiceController sc = new ServiceController(serviceName);
                return sc.Status;
            }
            catch
            {
                return null;
            }
        }
    }
}
