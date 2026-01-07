using System;
using Microsoft.Win32;

namespace PretoBoost.Services
{
    public static class Win10TweaksService
    {
        #region WIN10 BOOST - Sistema

        public static void EnablePerformanceTweaks()
        {
            RegistryService.SetValue(@"HKCU\Control Panel\Desktop\WindowMetrics", "MinAnimate", "0", RegistryValueKind.String);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarAnimations", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\PriorityControl", "Win32PrioritySeparation", 38);
            LogService.LogSuccess("Ajustes de desempenho habilitados");
        }

        public static void DisablePerformanceTweaks()
        {
            RegistryService.SetValue(@"HKCU\Control Panel\Desktop\WindowMetrics", "MinAnimate", "1", RegistryValueKind.String);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarAnimations", 1);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\PriorityControl", "Win32PrioritySeparation", 2);
            LogService.LogRevert("Ajustes de desempenho");
        }

        public static void DisableNetworkThrottling()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "NetworkThrottlingIndex", unchecked((int)0xFFFFFFFF));
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters", "DisableBandwidthThrottling", 1);
            LogService.LogSuccess("Limitacoes de rede desativadas");
        }

        public static void EnableNetworkThrottling()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "NetworkThrottlingIndex", 10);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 20);
            LogService.LogRevert("Limitacoes de rede");
        }

        public static void DisableErrorReporting()
        {
            ServiceManager.DisableService("WerSvc");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting", "Disabled", 1);
            LogService.LogSuccess("Servico de relatar erros desativado");
        }

        public static void EnableErrorReporting()
        {
            ServiceManager.EnableService("WerSvc", "Manual");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled", 0);
            LogService.LogRevert("Servico de relatar erros");
        }

        public static void DisableCompatibilityAssistant()
        {
            ServiceManager.DisableService("PcaSvc");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisablePCA", 1);
            LogService.LogSuccess("Assistente de Compatibilidade desativado");
        }

        public static void EnableCompatibilityAssistant()
        {
            ServiceManager.EnableService("PcaSvc");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisablePCA");
            LogService.LogRevert("Assistente de Compatibilidade");
        }

        public static void DisablePrintScreenService()
        {
            ServiceManager.DisableService("Spooler");
            LogService.LogSuccess("Servico de PrintScreen desabilitado");
        }

        public static void EnablePrintScreenService()
        {
            ServiceManager.EnableService("Spooler");
            LogService.LogRevert("Servico de PrintScreen");
        }

        public static void DisableFax()
        {
            ServiceManager.DisableService("Fax");
            LogService.LogSuccess("Servico de Fax desabilitado");
        }

        public static void EnableFax()
        {
            ServiceManager.EnableService("Fax", "Manual");
            LogService.LogRevert("Servico de Fax");
        }

        public static void DisableStickyKeys()
        {
            RegistryService.SetValue(@"HKCU\Control Panel\Accessibility\StickyKeys", "Flags", "506", RegistryValueKind.String);
            RegistryService.SetValue(@"HKCU\Control Panel\Accessibility\ToggleKeys", "Flags", "58", RegistryValueKind.String);
            RegistryService.SetValue(@"HKCU\Control Panel\Accessibility\Keyboard Response", "Flags", "122", RegistryValueKind.String);
            LogService.LogSuccess("Teclas de Aderencia desabilitadas");
        }

        public static void EnableStickyKeys()
        {
            RegistryService.SetValue(@"HKCU\Control Panel\Accessibility\StickyKeys", "Flags", "510", RegistryValueKind.String);
            LogService.LogRevert("Teclas de Aderencia");
        }

        public static void DisableSmartScreen()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "EnableSmartScreen", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "SmartScreenEnabled", "Off", RegistryValueKind.String);
            LogService.LogSuccess("SmartScreen desabilitado");
        }

        public static void EnableSmartScreen()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "EnableSmartScreen", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "SmartScreenEnabled", "On", RegistryValueKind.String);
            LogService.LogRevert("SmartScreen");
        }

        #endregion

        #region WIN10 BOOST - Unidades de Disco

        public static void DisableSystemRestore()
        {
            PowerShellService.ExecuteCommand("Disable-ComputerRestore -Drive 'C:\\'");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR", 1);
            LogService.LogSuccess("Recuperacao do Sistema desabilitada");
        }

        public static void EnableSystemRestore()
        {
            PowerShellService.ExecuteCommand("Enable-ComputerRestore -Drive 'C:\\'");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR", 0);
            LogService.LogRevert("Recuperacao do Sistema");
        }

        public static void DisableSuperfetch()
        {
            ServiceManager.DisableService("SysMain");
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnableSuperfetch", 0);
            LogService.LogSuccess("Superfetch desabilitado");
        }

        public static void EnableSuperfetch()
        {
            ServiceManager.EnableService("SysMain");
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnableSuperfetch", 3);
            LogService.LogRevert("Superfetch");
        }

        public static void DisableHibernation()
        {
            PowerShellService.ExecuteCommand("powercfg -h off");
            LogService.LogSuccess("Hibernacao desativada");
        }

        public static void EnableHibernation()
        {
            PowerShellService.ExecuteCommand("powercfg -h on");
            LogService.LogRevert("Hibernacao");
        }

        public static void DisableNTFSTimestamp()
        {
            PowerShellService.ExecuteCommand("fsutil behavior set disablelastaccess 1");
            LogService.LogSuccess("Carimbo de data/hora NTFS desativado");
        }

        public static void EnableNTFSTimestamp()
        {
            PowerShellService.ExecuteCommand("fsutil behavior set disablelastaccess 0");
            LogService.LogRevert("Carimbo de data/hora NTFS");
        }

        public static void DisableWindowsSearch()
        {
            ServiceManager.DisableService("WSearch");
            LogService.LogSuccess("Windows Search desativado");
        }

        public static void EnableWindowsSearch()
        {
            ServiceManager.EnableService("WSearch");
            LogService.LogRevert("Windows Search");
        }

        #endregion

        #region WIN10 BOOST - Aplicativos

        public static void DisableOfficeTelemetry()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Office\Common\ClientTelemetry", "DisableTelemetry", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Office\16.0\Common\ClientTelemetry", "DisableTelemetry", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Office\Common\ClientTelemetry", "VerboseLogging", 0);
            LogService.LogSuccess("Telemetria do Office 2016 desabilitada");
        }

        public static void EnableOfficeTelemetry()
        {
            RegistryService.DeleteValue(@"HKCU\Software\Microsoft\Office\Common\ClientTelemetry", "DisableTelemetry");
            RegistryService.DeleteValue(@"HKCU\Software\Microsoft\Office\16.0\Common\ClientTelemetry", "DisableTelemetry");
            LogService.LogRevert("Telemetria do Office 2016");
        }

        public static void DisableFirefoxTelemetry()
        {
            LogService.Log("Para desabilitar telemetria do Firefox, acesse about:config e desabilite toolkit.telemetry.enabled");
            LogService.LogSuccess("Instrucoes de telemetria do Firefox registradas");
        }

        public static void DisableChromeTelemetry()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Google\Chrome", "MetricsReportingEnabled", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Google\Chrome", "DeviceMetricsReportingEnabled", 0);
            LogService.LogSuccess("Telemetria do Chrome desabilitada");
        }

        public static void EnableChromeTelemetry()
        {
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Google\Chrome", "MetricsReportingEnabled");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Google\Chrome", "DeviceMetricsReportingEnabled");
            LogService.LogRevert("Telemetria do Chrome");
        }

        public static void DisableNvidiaTelemetry()
        {
            ServiceManager.DisableService("NvTelemetryContainer");
            LogService.LogSuccess("Telemetria da NVIDIA desabilitada");
        }

        public static void EnableNvidiaTelemetry()
        {
            ServiceManager.EnableService("NvTelemetryContainer", "Manual");
            LogService.LogRevert("Telemetria da NVIDIA");
        }

        public static void DisableVisualStudioTelemetry()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\VisualStudio\SQM", "OptIn", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\VisualStudio\Telemetry", "TurnOffSwitch", 1);
            LogService.LogSuccess("Telemetria do Visual Studio desabilitada");
        }

        public static void EnableVisualStudioTelemetry()
        {
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\VisualStudio\SQM", "OptIn");
            RegistryService.DeleteValue(@"HKCU\Software\Microsoft\VisualStudio\Telemetry", "TurnOffSwitch");
            LogService.LogRevert("Telemetria do Visual Studio");
        }

        #endregion

        #region WIN10 BOOST - Privacidade Extra

        public static void DisableTelemetryTasks()
        {
            PowerShellService.ExecuteCommand("schtasks /Change /TN 'Microsoft\\Windows\\Customer Experience Improvement Program\\Consolidator' /Disable");
            PowerShellService.ExecuteCommand("schtasks /Change /TN 'Microsoft\\Windows\\Application Experience\\Microsoft Compatibility Appraiser' /Disable");
            LogService.LogSuccess("Tarefas de telemetria desabilitadas");
        }

        public static void EnableTelemetryTasks()
        {
            PowerShellService.ExecuteCommand("schtasks /Change /TN 'Microsoft\\Windows\\Customer Experience Improvement Program\\Consolidator' /Enable");
            LogService.LogRevert("Tarefas de telemetria");
        }

        public static void DisableMediaSharing()
        {
            ServiceManager.DisableService("WMPNetworkSvc");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\WindowsMediaPlayer", "PreventLibrarySharing", 1);
            LogService.LogSuccess("Compartilhamento de midia desabilitado");
        }

        public static void EnableMediaSharing()
        {
            ServiceManager.EnableService("WMPNetworkSvc", "Manual");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\WindowsMediaPlayer", "PreventLibrarySharing");
            LogService.LogRevert("Compartilhamento de midia");
        }

        public static void DisableHomeGroup()
        {
            ServiceManager.DisableService("HomeGroupListener");
            ServiceManager.DisableService("HomeGroupProvider");
            LogService.LogSuccess("Grupo Home desabilitado");
        }

        public static void EnableHomeGroup()
        {
            ServiceManager.EnableService("HomeGroupListener", "Manual");
            ServiceManager.EnableService("HomeGroupProvider", "Manual");
            LogService.LogRevert("Grupo Home");
        }

        public static void DisableSMBv1()
        {
            PowerShellService.ExecuteCommand("Set-SmbServerConfiguration -EnableSMB1Protocol $false -Force");
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters", "SMB1", 0);
            LogService.LogSuccess("Protocolo SMBv1 desabilitado");
        }

        public static void EnableSMBv1()
        {
            PowerShellService.ExecuteCommand("Set-SmbServerConfiguration -EnableSMB1Protocol $true -Force");
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters", "SMB1", 1);
            LogService.LogRevert("Protocolo SMBv1");
        }

        public static void DisableSMBv2()
        {
            PowerShellService.ExecuteCommand("Set-SmbServerConfiguration -EnableSMB2Protocol $false -Force");
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters", "SMB2", 0);
            LogService.LogSuccess("Protocolo SMBv2 desabilitado");
        }

        public static void EnableSMBv2()
        {
            PowerShellService.ExecuteCommand("Set-SmbServerConfiguration -EnableSMB2Protocol $true -Force");
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters", "SMB2", 1);
            LogService.LogRevert("Protocolo SMBv2");
        }

        #endregion
    }
}
