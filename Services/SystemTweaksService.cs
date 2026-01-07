using System;
using Microsoft.Win32;

namespace PretoBoost.Services
{
    public static class SystemTweaksService
    {
        #region UNIVERSAL BOOST - Sistema

        public static void DisableQuickAccessHistory()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowRecent", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowFrequent", 0);
            LogService.LogSuccess("Historico de Acesso Rapido desativado");
        }

        public static void EnableQuickAccessHistory()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowRecent", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowFrequent", 1);
            LogService.LogRevert("Historico de Acesso Rapido");
        }

        public static void DisableMyPeople()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", "PeopleBand", 0);
            LogService.LogSuccess("My People desabilitado");
        }

        public static void EnableMyPeople()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", "PeopleBand", 1);
            LogService.LogRevert("My People");
        }

        public static void DisableTPMCheck()
        {
            RegistryService.SetValue(@"HKLM\SYSTEM\Setup\MoSetup", "AllowUpgradesWithUnsupportedTPMOrCPU", 1);
            LogService.LogSuccess("Verificacao TPM 2.0 desativada");
        }

        public static void EnableTPMCheck()
        {
            RegistryService.DeleteValue(@"HKLM\SYSTEM\Setup\MoSetup", "AllowUpgradesWithUnsupportedTPMOrCPU");
            LogService.LogRevert("Verificacao TPM 2.0");
        }

        public static void DisableSensorServices()
        {
            ServiceManager.DisableService("SensrSvc");
            ServiceManager.DisableService("SensorService");
            ServiceManager.DisableService("SensorDataService");
            LogService.LogSuccess("Servicos de Sensores desabilitados");
        }

        public static void EnableSensorServices()
        {
            ServiceManager.EnableService("SensrSvc", "Manual");
            ServiceManager.EnableService("SensorService", "Manual");
            ServiceManager.EnableService("SensorDataService", "Manual");
            LogService.LogRevert("Servicos de Sensores");
        }

        public static void UninstallOneDrive()
        {
            PowerShellService.ExecuteCommand("taskkill /f /im OneDrive.exe");
            PowerShellService.ExecuteCommand(@"if (Test-Path $env:systemroot\System32\OneDriveSetup.exe) { & $env:systemroot\System32\OneDriveSetup.exe /uninstall }");
            LogService.LogSuccess("OneDrive desinstalado");
        }

        public static void DisableMiracast()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\PlayToReceiver", "AutoEnabled", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\MiracastReceiver", "ConsentToast", 0);
            LogService.LogSuccess("Transmissao Miracast removida");
        }

        public static void EnableMiracast()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\PlayToReceiver", "AutoEnabled", 1);
            LogService.LogRevert("Transmissao Miracast");
        }

        public static void EnableClassicVolumeMixer()
        {
            RegistryService.SetValue(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", 0);
            LogService.LogSuccess("Mixer de Volume Classico habilitado");
        }

        public static void DisableClassicVolumeMixer()
        {
            RegistryService.SetValue(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", 1);
            LogService.LogRevert("Mixer de Volume Classico");
        }

        #endregion

        #region UNIVERSAL BOOST - Windows Update

        public static void DisableAutomaticUpdates()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "NoAutoUpdate", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "AUOptions", 2);
            ServiceManager.DisableService("wuauserv");
            LogService.LogSuccess("Atualizacoes Automaticas desativadas");
        }

        public static void EnableAutomaticUpdates()
        {
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "NoAutoUpdate");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "AUOptions");
            ServiceManager.EnableService("wuauserv");
            LogService.LogRevert("Atualizacoes Automaticas");
        }

        public static void DisableFeatureUpdates()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "TargetReleaseVersion", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "TargetReleaseVersionInfo", "22H2", RegistryValueKind.String);
            LogService.LogSuccess("Atualizacoes de Recursos desabilitadas");
        }

        public static void EnableFeatureUpdates()
        {
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "TargetReleaseVersion");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "TargetReleaseVersionInfo");
            LogService.LogRevert("Atualizacoes de Recursos");
        }

        public static void DisableWindowsInsider()
        {
            ServiceManager.DisableService("wisvc");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "ManagePreviewBuilds", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "ManagePreviewBuildsPolicyValue", 0);
            LogService.LogSuccess("Windows Insider Service desabilitado");
        }

        public static void EnableWindowsInsider()
        {
            ServiceManager.EnableService("wisvc", "Manual");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "ManagePreviewBuilds");
            LogService.LogRevert("Windows Insider Service");
        }

        public static void ExcludeDriversFromUpdates()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "ExcludeWUDriversInQualityUpdate", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\DriverSearching", "SearchOrderConfig", 0);
            LogService.LogSuccess("Drivers excluidos das atualizacoes");
        }

        public static void IncludeDriversInUpdates()
        {
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "ExcludeWUDriversInQualityUpdate");
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\DriverSearching", "SearchOrderConfig", 1);
            LogService.LogRevert("Drivers nas atualizacoes");
        }

        #endregion

        #region UNIVERSAL BOOST - Privacidade

        public static void DisableTelemetry()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry", 0);
            ServiceManager.DisableService("DiagTrack");
            ServiceManager.DisableService("dmwappushservice");
            LogService.LogSuccess("Servicos de Telemetria desativados");
        }

        public static void EnableTelemetry()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", 1);
            ServiceManager.EnableService("DiagTrack");
            LogService.LogRevert("Servicos de Telemetria");
        }

        public static void DisableCortana()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", 0);
            RegistryService.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent", 0);
            RegistryService.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "BingSearchEnabled", 0);
            LogService.LogSuccess("Assistencia da Cortana desabilitada");
        }

        public static void EnableCortana()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", 1);
            RegistryService.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent", 1);
            LogService.LogRevert("Assistencia da Cortana");
        }

        public static void EnhancePrivacy()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "EnableActivityFeed", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "PublishUserActivities", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "UploadUserActivities", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", 1);
            LogService.LogSuccess("Aumento de Privacidade aplicado");
        }

        public static void RevertPrivacy()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "EnableActivityFeed", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", 1);
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation");
            LogService.LogRevert("Configuracoes de Privacidade");
        }

        public static void DisableStartMenuAds()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338388Enabled", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-310093Enabled", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", 0);
            LogService.LogSuccess("Anuncios do Menu Iniciar desativados");
        }

        public static void EnableStartMenuAds()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", 1);
            LogService.LogRevert("Anuncios do Menu Iniciar");
        }

        public static void DisableEdgeTelemetry()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "MetricsReportingEnabled", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "SendSiteInfoToImproveServices", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "PersonalizationReportingEnabled", 0);
            LogService.LogSuccess("Telemetria do Edge desativada");
        }

        public static void EnableEdgeTelemetry()
        {
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "MetricsReportingEnabled");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "SendSiteInfoToImproveServices");
            LogService.LogRevert("Telemetria do Edge");
        }

        public static void DisableEdgeDiscovery()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "HubsSidebarEnabled", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "WebWidgetAllowed", 0);
            LogService.LogSuccess("Descoberta do Edge desativada");
        }

        public static void EnableEdgeDiscovery()
        {
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "HubsSidebarEnabled");
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Edge", "WebWidgetAllowed");
            LogService.LogRevert("Descoberta do Edge");
        }

        #endregion

        #region UNIVERSAL BOOST - Jogos

        public static void EnableGameMode()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\GameBar", "AllowAutoGameMode", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\GameBar", "AutoGameModeEnabled", 1);
            LogService.LogSuccess("Modo de Jogo ativado");
        }

        public static void DisableGameMode()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\GameBar", "AllowAutoGameMode", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\GameBar", "AutoGameModeEnabled", 0);
            LogService.LogRevert("Modo de Jogo");
        }

        public static void DisableXboxLive()
        {
            ServiceManager.DisableService("XblAuthManager");
            ServiceManager.DisableService("XblGameSave");
            ServiceManager.DisableService("XboxNetApiSvc");
            ServiceManager.DisableService("XboxGipSvc");
            LogService.LogSuccess("Xbox Live desabilitado");
        }

        public static void EnableXboxLive()
        {
            ServiceManager.EnableService("XblAuthManager", "Manual");
            ServiceManager.EnableService("XblGameSave", "Manual");
            ServiceManager.EnableService("XboxNetApiSvc", "Manual");
            LogService.LogRevert("Xbox Live");
        }

        public static void DisableXboxGameBar()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", 0);
            RegistryService.SetValue(@"HKCU\System\GameConfigStore", "GameDVR_Enabled", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", 0);
            LogService.LogSuccess("Xbox Game Bar desabilitado");
        }

        public static void EnableXboxGameBar()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", 1);
            RegistryService.SetValue(@"HKCU\System\GameConfigStore", "GameDVR_Enabled", 1);
            RegistryService.DeleteValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR");
            LogService.LogRevert("Xbox Game Bar");
        }

        #endregion

        #region UNIVERSAL BOOST - Toque

        public static void DisableWindowsInk()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\WindowsInkWorkspace", "AllowWindowsInkWorkspace", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\WindowsInkWorkspace", "AllowSuggestedAppsInWindowsInkWorkspace", 0);
            LogService.LogSuccess("Windows Ink desabilitado");
        }

        public static void EnableWindowsInk()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\WindowsInkWorkspace", "AllowWindowsInkWorkspace", 1);
            LogService.LogRevert("Windows Ink");
        }

        public static void DisableSpellChecking()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\TabletTip\1.7", "EnableSpellchecking", 0);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\TabletTip\1.7", "EnableTextPrediction", 0);
            LogService.LogSuccess("Spell Checking desabilitado");
        }

        public static void EnableSpellChecking()
        {
            RegistryService.SetValue(@"HKCU\Software\Microsoft\TabletTip\1.7", "EnableSpellchecking", 1);
            RegistryService.SetValue(@"HKCU\Software\Microsoft\TabletTip\1.7", "EnableTextPrediction", 1);
            LogService.LogRevert("Spell Checking");
        }

        public static void DisableCloudClipboard()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "AllowClipboardHistory", 0);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard", 0);
            LogService.LogSuccess("Cloud Clipboard desabilitado");
        }

        public static void EnableCloudClipboard()
        {
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "AllowClipboardHistory", 1);
            RegistryService.SetValue(@"HKLM\SOFTWARE\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard", 1);
            LogService.LogRevert("Cloud Clipboard");
        }

        #endregion
    }
}
