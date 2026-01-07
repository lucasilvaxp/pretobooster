using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PretoBoost.Models;
using PretoBoost.Services;

namespace PretoBoost.ViewModels
{
    public class UniversalBoostViewModel : INotifyPropertyChanged
    {
        public UniversalBoostViewModel()
        {
            InitializeToggles();
            ApplyAllCommand = new RelayCommand(ApplyAll);
            RevertAllCommand = new RelayCommand(RevertAll);
        }

        public ObservableCollection<ToggleAction> SystemToggles { get; } = new();
        public ObservableCollection<ToggleAction> WindowsUpdateToggles { get; } = new();
        public ObservableCollection<ToggleAction> PrivacyToggles { get; } = new();
        public ObservableCollection<ToggleAction> GamingToggles { get; } = new();
        public ObservableCollection<ToggleAction> TouchToggles { get; } = new();

        public ICommand ApplyAllCommand { get; }
        public ICommand RevertAllCommand { get; }

        private void InitializeToggles()
        {
            // Sistema
            SystemToggles.Add(new ToggleAction
            {
                Name = "Desativar Histórico de Acesso Rápido",
                Category = "Sistema",
                EnableAction = SystemTweaksService.DisableQuickAccessHistory,
                DisableAction = SystemTweaksService.EnableQuickAccessHistory
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desabilitar 'My People'",
                Category = "Sistema",
                EnableAction = SystemTweaksService.DisableMyPeople,
                DisableAction = SystemTweaksService.EnableMyPeople
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desativar verificação TPM 2.0",
                Category = "Sistema",
                EnableAction = SystemTweaksService.DisableTPMCheck,
                DisableAction = SystemTweaksService.EnableTPMCheck
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Serviços de Sensores",
                Category = "Sistema",
                EnableAction = SystemTweaksService.DisableSensorServices,
                DisableAction = SystemTweaksService.EnableSensorServices
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desinstalar OneDrive",
                Category = "Sistema",
                EnableAction = SystemTweaksService.UninstallOneDrive,
                DisableAction = () => { } // Não pode reverter
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Remover Transmissão de Miracast",
                Category = "Sistema",
                EnableAction = SystemTweaksService.DisableMiracast,
                DisableAction = SystemTweaksService.EnableMiracast
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Habilitar Mixer de Volume Clássico",
                Category = "Sistema",
                EnableAction = SystemTweaksService.EnableClassicVolumeMixer,
                DisableAction = SystemTweaksService.DisableClassicVolumeMixer
            });

            // Windows Update
            WindowsUpdateToggles.Add(new ToggleAction
            {
                Name = "Desativar Atualizações Automáticas",
                Category = "Windows Update",
                EnableAction = SystemTweaksService.DisableAutomaticUpdates,
                DisableAction = SystemTweaksService.EnableAutomaticUpdates
            });

            WindowsUpdateToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Atualizações de Recursos",
                Category = "Windows Update",
                EnableAction = SystemTweaksService.DisableFeatureUpdates,
                DisableAction = SystemTweaksService.EnableFeatureUpdates
            });

            WindowsUpdateToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Windows Insider Service",
                Category = "Windows Update",
                EnableAction = SystemTweaksService.DisableWindowsInsider,
                DisableAction = SystemTweaksService.EnableWindowsInsider
            });

            WindowsUpdateToggles.Add(new ToggleAction
            {
                Name = "Excluir Drivers de Atualizações",
                Category = "Windows Update",
                EnableAction = SystemTweaksService.ExcludeDriversFromUpdates,
                DisableAction = SystemTweaksService.IncludeDriversInUpdates
            });

            // Privacidade
            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desativar Serviços de Telemetria",
                Category = "Privacidade",
                EnableAction = SystemTweaksService.DisableTelemetry,
                DisableAction = SystemTweaksService.EnableTelemetry
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Assistência da Cortana",
                Category = "Privacidade",
                EnableAction = SystemTweaksService.DisableCortana,
                DisableAction = SystemTweaksService.EnableCortana
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Aumento de Privacidade",
                Category = "Privacidade",
                EnableAction = SystemTweaksService.EnhancePrivacy,
                DisableAction = SystemTweaksService.RevertPrivacy
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desativar anúncios do Menu Iniciar",
                Category = "Privacidade",
                EnableAction = SystemTweaksService.DisableStartMenuAds,
                DisableAction = SystemTweaksService.EnableStartMenuAds
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desativar telemetria de borda",
                Category = "Privacidade",
                EnableAction = SystemTweaksService.DisableEdgeTelemetry,
                DisableAction = SystemTweaksService.EnableEdgeTelemetry
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desativar descoberta de borda",
                Category = "Privacidade",
                EnableAction = SystemTweaksService.DisableEdgeDiscovery,
                DisableAction = SystemTweaksService.EnableEdgeDiscovery
            });

            // Jogos
            GamingToggles.Add(new ToggleAction
            {
                Name = "Ativar modo de jogo",
                Category = "Jogos",
                EnableAction = SystemTweaksService.EnableGameMode,
                DisableAction = SystemTweaksService.DisableGameMode
            });

            GamingToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Xbox Live",
                Category = "Jogos",
                EnableAction = SystemTweaksService.DisableXboxLive,
                DisableAction = SystemTweaksService.EnableXboxLive
            });

            GamingToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Xbox Game Bar",
                Category = "Jogos",
                EnableAction = SystemTweaksService.DisableXboxGameBar,
                DisableAction = SystemTweaksService.EnableXboxGameBar
            });

            // Toque
            TouchToggles.Add(new ToggleAction
            {
                Name = "Desabilitar app Windows Ink",
                Category = "Toque",
                EnableAction = SystemTweaksService.DisableWindowsInk,
                DisableAction = SystemTweaksService.EnableWindowsInk
            });

            TouchToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Spell Checking",
                Category = "Toque",
                EnableAction = SystemTweaksService.DisableSpellChecking,
                DisableAction = SystemTweaksService.EnableSpellChecking
            });

            TouchToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Cloud Clipboard",
                Category = "Toque",
                EnableAction = SystemTweaksService.DisableCloudClipboard,
                DisableAction = SystemTweaksService.EnableCloudClipboard
            });
        }

        private void ApplyAll()
        {
            foreach (var toggle in SystemToggles) toggle.IsEnabled = true;
            foreach (var toggle in WindowsUpdateToggles) toggle.IsEnabled = true;
            foreach (var toggle in PrivacyToggles) toggle.IsEnabled = true;
            foreach (var toggle in GamingToggles) toggle.IsEnabled = true;
            foreach (var toggle in TouchToggles) toggle.IsEnabled = true;
            LogService.LogSuccess("Todos os tweaks Universal Boost aplicados");
        }

        private void RevertAll()
        {
            foreach (var toggle in SystemToggles) toggle.IsEnabled = false;
            foreach (var toggle in WindowsUpdateToggles) toggle.IsEnabled = false;
            foreach (var toggle in PrivacyToggles) toggle.IsEnabled = false;
            foreach (var toggle in GamingToggles) toggle.IsEnabled = false;
            foreach (var toggle in TouchToggles) toggle.IsEnabled = false;
            LogService.LogSuccess("Todos os tweaks Universal Boost revertidos");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
