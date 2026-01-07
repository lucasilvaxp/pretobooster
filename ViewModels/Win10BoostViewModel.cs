using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PretoBoost.Models;
using PretoBoost.Services;

namespace PretoBoost.ViewModels
{
    public class Win10BoostViewModel : INotifyPropertyChanged
    {
        public Win10BoostViewModel()
        {
            InitializeToggles();
            ApplyAllCommand = new RelayCommand(ApplyAll);
            RevertAllCommand = new RelayCommand(RevertAll);
        }

        public ObservableCollection<ToggleAction> SystemToggles { get; } = new();
        public ObservableCollection<ToggleAction> DiskToggles { get; } = new();
        public ObservableCollection<ToggleAction> AppToggles { get; } = new();
        public ObservableCollection<ToggleAction> PrivacyToggles { get; } = new();

        public ICommand ApplyAllCommand { get; }
        public ICommand RevertAllCommand { get; }

        private void InitializeToggles()
        {
            // Sistema
            SystemToggles.Add(new ToggleAction
            {
                Name = "Habilitar ajustes de desempenho",
                Category = "Sistema",
                EnableAction = Win10TweaksService.EnablePerformanceTweaks,
                DisableAction = Win10TweaksService.DisablePerformanceTweaks
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desativar limitações de rede",
                Category = "Sistema",
                EnableAction = Win10TweaksService.DisableNetworkThrottling,
                DisableAction = Win10TweaksService.EnableNetworkThrottling
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desativar serviço de relatar erros/problemas",
                Category = "Sistema",
                EnableAction = Win10TweaksService.DisableErrorReporting,
                DisableAction = Win10TweaksService.EnableErrorReporting
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desativar o Assistente de Compatibilidade",
                Category = "Sistema",
                EnableAction = Win10TweaksService.DisableCompatibilityAssistant,
                DisableAction = Win10TweaksService.EnableCompatibilityAssistant
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Serviço de PrintScreen",
                Category = "Sistema",
                EnableAction = Win10TweaksService.DisablePrintScreenService,
                DisableAction = Win10TweaksService.EnablePrintScreenService
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desabilitar serviço de Fax",
                Category = "Sistema",
                EnableAction = Win10TweaksService.DisableFax,
                DisableAction = Win10TweaksService.EnableFax
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desabilitar as Teclas de Aderência",
                Category = "Sistema",
                EnableAction = Win10TweaksService.DisableStickyKeys,
                DisableAction = Win10TweaksService.EnableStickyKeys
            });

            SystemToggles.Add(new ToggleAction
            {
                Name = "Desabilitar o SmartScreen",
                Category = "Sistema",
                EnableAction = Win10TweaksService.DisableSmartScreen,
                DisableAction = Win10TweaksService.EnableSmartScreen
            });

            // Unidades de Disco
            DiskToggles.Add(new ToggleAction
            {
                Name = "Desabilitar recuperação do sistema",
                Category = "Unidades de disco",
                EnableAction = Win10TweaksService.DisableSystemRestore,
                DisableAction = Win10TweaksService.EnableSystemRestore
            });

            DiskToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Superfetch",
                Category = "Unidades de disco",
                EnableAction = Win10TweaksService.DisableSuperfetch,
                DisableAction = Win10TweaksService.EnableSuperfetch
            });

            DiskToggles.Add(new ToggleAction
            {
                Name = "Desativar Hibernação",
                Category = "Unidades de disco",
                EnableAction = Win10TweaksService.DisableHibernation,
                DisableAction = Win10TweaksService.EnableHibernation
            });

            DiskToggles.Add(new ToggleAction
            {
                Name = "Desativar carimbo de data/hora NTFS",
                Category = "Unidades de disco",
                EnableAction = Win10TweaksService.DisableNTFSTimestamp,
                DisableAction = Win10TweaksService.EnableNTFSTimestamp
            });

            DiskToggles.Add(new ToggleAction
            {
                Name = "Desativar pesquisa",
                Category = "Unidades de disco",
                EnableAction = Win10TweaksService.DisableWindowsSearch,
                DisableAction = Win10TweaksService.EnableWindowsSearch
            });

            // Aplicativos
            AppToggles.Add(new ToggleAction
            {
                Name = "Desabilitar Telemetria para o Office 2016",
                Category = "Aplicativos",
                EnableAction = Win10TweaksService.DisableOfficeTelemetry,
                DisableAction = Win10TweaksService.EnableOfficeTelemetry
            });

            AppToggles.Add(new ToggleAction
            {
                Name = "Desativar a telemetria do Firefox",
                Category = "Aplicativos",
                EnableAction = Win10TweaksService.DisableFirefoxTelemetry,
                DisableAction = () => { }
            });

            AppToggles.Add(new ToggleAction
            {
                Name = "Desativar a telemetria do Chrome",
                Category = "Aplicativos",
                EnableAction = Win10TweaksService.DisableChromeTelemetry,
                DisableAction = Win10TweaksService.EnableChromeTelemetry
            });

            AppToggles.Add(new ToggleAction
            {
                Name = "Desativar a telemetria da NVIDIA",
                Category = "Aplicativos",
                EnableAction = Win10TweaksService.DisableNvidiaTelemetry,
                DisableAction = Win10TweaksService.EnableNvidiaTelemetry
            });

            AppToggles.Add(new ToggleAction
            {
                Name = "Desativar a telemetria do Visual Studio",
                Category = "Aplicativos",
                EnableAction = Win10TweaksService.DisableVisualStudioTelemetry,
                DisableAction = Win10TweaksService.EnableVisualStudioTelemetry
            });

            // Privacidade
            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desabilitar tarefas de telemetria",
                Category = "Privacidade",
                EnableAction = Win10TweaksService.DisableTelemetryTasks,
                DisableAction = Win10TweaksService.EnableTelemetryTasks
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desabilitar compartilhamento de mídia",
                Category = "Privacidade",
                EnableAction = Win10TweaksService.DisableMediaSharing,
                DisableAction = Win10TweaksService.EnableMediaSharing
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Desabilitar grupo Home",
                Category = "Privacidade",
                EnableAction = Win10TweaksService.DisableHomeGroup,
                DisableAction = Win10TweaksService.EnableHomeGroup
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Disable SMBv1 Protocol",
                Category = "Privacidade",
                EnableAction = Win10TweaksService.DisableSMBv1,
                DisableAction = Win10TweaksService.EnableSMBv1
            });

            PrivacyToggles.Add(new ToggleAction
            {
                Name = "Disable SMBv2 Protocol",
                Category = "Privacidade",
                EnableAction = Win10TweaksService.DisableSMBv2,
                DisableAction = Win10TweaksService.EnableSMBv2
            });
        }

        private void ApplyAll()
        {
            foreach (var toggle in SystemToggles) toggle.IsEnabled = true;
            foreach (var toggle in DiskToggles) toggle.IsEnabled = true;
            foreach (var toggle in AppToggles) toggle.IsEnabled = true;
            foreach (var toggle in PrivacyToggles) toggle.IsEnabled = true;
            LogService.LogSuccess("Todos os tweaks Win10 Boost aplicados");
        }

        private void RevertAll()
        {
            foreach (var toggle in SystemToggles) toggle.IsEnabled = false;
            foreach (var toggle in DiskToggles) toggle.IsEnabled = false;
            foreach (var toggle in AppToggles) toggle.IsEnabled = false;
            foreach (var toggle in PrivacyToggles) toggle.IsEnabled = false;
            LogService.LogSuccess("Todos os tweaks Win10 Boost revertidos");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
