using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PretoBoost.Models;
using PretoBoost.Services;

namespace PretoBoost.ViewModels
{
    public class GpuBoostViewModel : INotifyPropertyChanged
    {
        public GpuBoostViewModel()
        {
            InitializeToggles();
            ApplyAllCommand = new RelayCommand(ApplyAll);
            RevertAllCommand = new RelayCommand(RevertAll);
        }

        public ObservableCollection<ToggleAction> NvidiaTweaksToggles { get; } = new();
        public ObservableCollection<ToggleAction> NvidiaDwordsToggles { get; } = new();
        public ObservableCollection<ToggleAction> GeforceToggles { get; } = new();
        public ObservableCollection<ToggleAction> AmdToggles { get; } = new();

        public ICommand ApplyAllCommand { get; }
        public ICommand RevertAllCommand { get; }

        private void InitializeToggles()
        {
            // NVIDIA Tweaks
            NvidiaTweaksToggles.Add(new ToggleAction
            {
                Name = "Delete Nvidia Telemetry",
                Category = "NVIDIA Tweaks",
                EnableAction = GpuTweaksService.DeleteNvidiaTelemetry,
                DisableAction = () => { } // Não pode reverter
            });

            NvidiaTweaksToggles.Add(new ToggleAction
            {
                Name = "Unrestricted Clock Policy",
                Category = "NVIDIA Tweaks",
                EnableAction = GpuTweaksService.SetUnrestrictedClockPolicy,
                DisableAction = GpuTweaksService.RevertClockPolicy
            });

            NvidiaTweaksToggles.Add(new ToggleAction
            {
                Name = "No ECC",
                Category = "NVIDIA Tweaks",
                EnableAction = GpuTweaksService.DisableECC,
                DisableAction = GpuTweaksService.EnableECC
            });

            NvidiaTweaksToggles.Add(new ToggleAction
            {
                Name = "Force P0-State (Eagle)",
                Category = "NVIDIA Tweaks",
                EnableAction = GpuTweaksService.ForceP0State,
                DisableAction = GpuTweaksService.RevertP0State
            });

            // NVIDIA Dwords
            NvidiaDwordsToggles.Add(new ToggleAction
            {
                Name = "Disable HDCP",
                Category = "NVIDIA Dwords",
                EnableAction = GpuTweaksService.DisableHDCP,
                DisableAction = GpuTweaksService.EnableHDCP
            });

            NvidiaDwordsToggles.Add(new ToggleAction
            {
                Name = "NVIDIA Preemption (experimental)",
                Category = "NVIDIA Dwords",
                EnableAction = GpuTweaksService.DisableNvidiaPreemption,
                DisableAction = GpuTweaksService.EnableNvidiaPreemption
            });

            NvidiaDwordsToggles.Add(new ToggleAction
            {
                Name = "NVIDIA Disable Logging",
                Category = "NVIDIA Dwords",
                EnableAction = GpuTweaksService.DisableNvidiaLogging,
                DisableAction = GpuTweaksService.EnableNvidiaLogging
            });

            NvidiaDwordsToggles.Add(new ToggleAction
            {
                Name = "Disable DMA Remapping",
                Category = "NVIDIA Dwords",
                EnableAction = GpuTweaksService.DisableDMARemapping,
                DisableAction = GpuTweaksService.EnableDMARemapping
            });

            // Geforce Experience
            GeforceToggles.Add(new ToggleAction
            {
                Name = "Don't Notify about Driver Update",
                Category = "Geforce Experience",
                EnableAction = GpuTweaksService.DisableDriverUpdateNotification,
                DisableAction = GpuTweaksService.EnableDriverUpdateNotification
            });

            GeforceToggles.Add(new ToggleAction
            {
                Name = "Nvidia Geforce Experience Telemetry",
                Category = "Geforce Experience",
                EnableAction = GpuTweaksService.DisableGeforceExperienceTelemetry,
                DisableAction = GpuTweaksService.EnableGeforceExperienceTelemetry
            });

            // AMD Tweaks
            AmdToggles.Add(new ToggleAction
            {
                Name = "AMD Best GPU Settings",
                Category = "AMD Tweaks",
                EnableAction = GpuTweaksService.ApplyAMDBestSettings,
                DisableAction = GpuTweaksService.RevertAMDBestSettings
            });

            AmdToggles.Add(new ToggleAction
            {
                Name = "Disable Gpu Energy Driver",
                Category = "AMD Tweaks",
                EnableAction = GpuTweaksService.DisableGpuEnergyDriver,
                DisableAction = GpuTweaksService.EnableGpuEnergyDriver
            });

            AmdToggles.Add(new ToggleAction
            {
                Name = "Prioritize AMD GPU",
                Category = "AMD Tweaks",
                EnableAction = GpuTweaksService.PrioritizeAMDGpu,
                DisableAction = GpuTweaksService.RevertAMDPriority
            });
        }

        private void ApplyAll()
        {
            foreach (var toggle in NvidiaTweaksToggles) toggle.IsEnabled = true;
            foreach (var toggle in NvidiaDwordsToggles) toggle.IsEnabled = true;
            foreach (var toggle in GeforceToggles) toggle.IsEnabled = true;
            foreach (var toggle in AmdToggles) toggle.IsEnabled = true;
            LogService.LogSuccess("Todos os tweaks GPU Boost aplicados");
        }

        private void RevertAll()
        {
            foreach (var toggle in NvidiaTweaksToggles) toggle.IsEnabled = false;
            foreach (var toggle in NvidiaDwordsToggles) toggle.IsEnabled = false;
            foreach (var toggle in GeforceToggles) toggle.IsEnabled = false;
            foreach (var toggle in AmdToggles) toggle.IsEnabled = false;
            LogService.LogSuccess("Todos os tweaks GPU Boost revertidos");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
