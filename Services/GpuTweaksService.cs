using System;
using Microsoft.Win32;

namespace PretoBoost.Services
{
    public static class GpuTweaksService
    {
        #region NVIDIA Tweaks

        public static void DeleteNvidiaTelemetry()
        {
            ServiceManager.DisableService("NvTelemetryContainer");
            PowerShellService.ExecuteCommand("schtasks /Delete /TN 'NvTmMon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}' /F");
            PowerShellService.ExecuteCommand("schtasks /Delete /TN 'NvTmRep_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}' /F");
            LogService.LogSuccess("Telemetria NVIDIA deletada");
        }

        public static void SetUnrestrictedClockPolicy()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "DisableDynamicPstate", 1);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PerfLevelSrc", 0x2222);
            LogService.LogSuccess("Politica de Clock Irrestrita aplicada");
        }

        public static void RevertClockPolicy()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "DisableDynamicPstate");
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PerfLevelSrc");
            LogService.LogRevert("Politica de Clock");
        }

        public static void DisableECC()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "RMDisableEccSupport", 1);
            LogService.LogSuccess("ECC desabilitado");
        }

        public static void EnableECC()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "RMDisableEccSupport");
            LogService.LogRevert("ECC");
        }

        public static void ForceP0State()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "DisableDynamicPstate", 1);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PowerMizerEnable", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PowerMizerLevel", 1);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PowerMizerLevelAC", 1);
            LogService.LogSuccess("P0-State forcado (Eagle)");
        }

        public static void RevertP0State()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "DisableDynamicPstate");
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PowerMizerEnable");
            LogService.LogRevert("P0-State");
        }

        #endregion

        #region NVIDIA Dwords

        public static void DisableHDCP()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "RMHdcpKeyglobZero", 1);
            LogService.LogSuccess("HDCP desabilitado");
        }

        public static void EnableHDCP()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "RMHdcpKeyglobZero");
            LogService.LogRevert("HDCP");
        }

        public static void DisableNvidiaPreemption()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler", "EnablePreemption", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "RmGpsPsEnablePerCpuCoreDpc", 1);
            LogService.LogSuccess("NVIDIA Preemption desabilitado (experimental)");
        }

        public static void EnableNvidiaPreemption()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler", "EnablePreemption");
            LogService.LogRevert("NVIDIA Preemption");
        }

        public static void DisableNvidiaLogging()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\Startup", "SendTelemetryData", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm", "DisableLogging", 1);
            LogService.LogSuccess("NVIDIA Logging desabilitado");
        }

        public static void EnableNvidiaLogging()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm", "DisableLogging");
            LogService.LogRevert("NVIDIA Logging");
        }

        public static void DisableDMARemapping()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\DmaSecurity", "AllowInternalDmaDevices", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\Kernel DMA Protection", "DeviceEnumerationPolicy", 0);
            LogService.LogSuccess("DMA Remapping desabilitado");
        }

        public static void EnableDMARemapping()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\DmaSecurity", "AllowInternalDmaDevices");
            LogService.LogRevert("DMA Remapping");
        }

        #endregion

        #region Geforce Experience

        public static void DisableDriverUpdateNotification()
        {
            RegistryService.SetValue(@"HKCU\Software\NVIDIA Corporation\NvTray", "StartOnLogin", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client", "OptInOrOutPreference", 0);
            RegistryService.SetValue(@"HKCU\Software\NVIDIA Corporation\Global\GFExperience", "NotificationsEnabled", 0);
            LogService.LogSuccess("Notificacoes de atualizacao de driver desabilitadas");
        }

        public static void EnableDriverUpdateNotification()
        {
            RegistryService.SetValue(@"HKCU\Software\NVIDIA Corporation\NvTray", "StartOnLogin", 1);
            RegistryService.SetValue(@"HKCU\Software\NVIDIA Corporation\Global\GFExperience", "NotificationsEnabled", 1);
            LogService.LogRevert("Notificacoes de atualizacao de driver");
        }

        public static void DisableGeforceExperienceTelemetry()
        {
            ServiceManager.DisableService("NvTelemetryContainer");
            RegistryService.SetValue(@"HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client", "OptInOrOutPreference", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\Startup", "SendTelemetryData", 0);
            LogService.LogSuccess("Telemetria do GeForce Experience desabilitada");
        }

        public static void EnableGeforceExperienceTelemetry()
        {
            ServiceManager.EnableService("NvTelemetryContainer", "Manual");
            RegistryService.SetValue(@"HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client", "OptInOrOutPreference", 1);
            LogService.LogRevert("Telemetria do GeForce Experience");
        }

        #endregion

        #region AMD Tweaks

        public static void ApplyAMDBestSettings()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "EnableUlps", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PP_ThermalAutoThrottlingEnable", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "KMD_EnableComputePreemption", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "DisableDrmdmaPowerGating", 1);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "DisablePowerGating", 1);
            LogService.LogSuccess("Melhores configuracoes AMD aplicadas");
        }

        public static void RevertAMDBestSettings()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "EnableUlps", 1);
            RegistryService.DeleteValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "PP_ThermalAutoThrottlingEnable");
            LogService.LogRevert("Configuracoes AMD");
        }

        public static void DisableGpuEnergyDriver()
        {
            ServiceManager.DisableService("AMD External Events Utility");
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "DisableBlockWrite", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "StutterMode", 0);
            LogService.LogSuccess("Driver de energia GPU desabilitado");
        }

        public static void EnableGpuEnergyDriver()
        {
            ServiceManager.EnableService("AMD External Events Utility", "Automatic");
            LogService.LogRevert("Driver de energia GPU");
        }

        public static void PrioritizeAMDGpu()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "KMD_FRTEnabled", 0);
            RegistryService.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", "KMD_DeLagEnabled", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\DirectX\UserGpuPreferences", "DirectXUserGlobalSettings", "GpuPreference=2;", RegistryValueKind.String);
            LogService.LogSuccess("GPU AMD priorizada");
        }

        public static void RevertAMDPriority()
        {
            RegistryService.DeleteValue(@"HKCU\Software\Microsoft\DirectX\UserGpuPreferences", "DirectXUserGlobalSettings");
            LogService.LogRevert("Prioridade GPU AMD");
        }

        #endregion
    }
}
