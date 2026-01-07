using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PretoBoost.Services;

namespace PretoBoost.ViewModels
{
    public class CleaningViewModel : INotifyPropertyChanged
    {
        private bool _cleanTemp;
        private bool _cleanWindowsLogs;
        private bool _cleanPrefetch;
        private bool _cleanMinidumps;
        private bool _cleanErrorReports;
        private bool _emptyRecycleBin;
        private bool _cleanMediaCache;
        private bool _cleanUTorrent;
        private bool _cleanFileZilla;
        private bool _selectAll;
        private bool _isCleaning;
        private string _statusMessage = "Selecione os itens para limpar";

        public CleaningViewModel()
        {
            CleanCommand = new RelayCommand(ExecuteClean, () => !IsCleaning && HasSelection());
            SelectAllCommand = new RelayCommand(ToggleSelectAll);
        }

        public ICommand CleanCommand { get; }
        public ICommand SelectAllCommand { get; }

        public bool CleanTemp
        {
            get => _cleanTemp;
            set { _cleanTemp = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool CleanWindowsLogs
        {
            get => _cleanWindowsLogs;
            set { _cleanWindowsLogs = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool CleanPrefetch
        {
            get => _cleanPrefetch;
            set { _cleanPrefetch = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool CleanMinidumps
        {
            get => _cleanMinidumps;
            set { _cleanMinidumps = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool CleanErrorReports
        {
            get => _cleanErrorReports;
            set { _cleanErrorReports = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool EmptyRecycleBin
        {
            get => _emptyRecycleBin;
            set { _emptyRecycleBin = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool CleanMediaCache
        {
            get => _cleanMediaCache;
            set { _cleanMediaCache = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool CleanUTorrent
        {
            get => _cleanUTorrent;
            set { _cleanUTorrent = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool CleanFileZilla
        {
            get => _cleanFileZilla;
            set { _cleanFileZilla = value; OnPropertyChanged(); UpdateSelectAll(); }
        }

        public bool SelectAll
        {
            get => _selectAll;
            set { _selectAll = value; OnPropertyChanged(); }
        }

        public bool IsCleaning
        {
            get => _isCleaning;
            set { _isCleaning = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        private void ToggleSelectAll()
        {
            bool newValue = !SelectAll;
            CleanTemp = newValue;
            CleanWindowsLogs = newValue;
            CleanPrefetch = newValue;
            CleanMinidumps = newValue;
            CleanErrorReports = newValue;
            EmptyRecycleBin = newValue;
            CleanMediaCache = newValue;
            CleanUTorrent = newValue;
            CleanFileZilla = newValue;
            SelectAll = newValue;
        }

        private void UpdateSelectAll()
        {
            SelectAll = CleanTemp && CleanWindowsLogs && CleanPrefetch && CleanMinidumps &&
                       CleanErrorReports && EmptyRecycleBin && CleanMediaCache && 
                       CleanUTorrent && CleanFileZilla;
        }

        private bool HasSelection()
        {
            return CleanTemp || CleanWindowsLogs || CleanPrefetch || CleanMinidumps ||
                   CleanErrorReports || EmptyRecycleBin || CleanMediaCache || 
                   CleanUTorrent || CleanFileZilla;
        }

        private async void ExecuteClean()
        {
            IsCleaning = true;
            StatusMessage = "Limpando...";

            await System.Threading.Tasks.Task.Run(() =>
            {
                if (CleanTemp) CleaningService.CleanTempFiles();
                if (CleanWindowsLogs) CleaningService.CleanWindowsLogs();
                if (CleanPrefetch) CleaningService.CleanPrefetchCache();
                if (CleanMinidumps) CleaningService.CleanBSODMinidumps();
                if (CleanErrorReports) CleaningService.CleanErrorReports();
                if (EmptyRecycleBin) CleaningService.EmptyRecycleBin();
                if (CleanMediaCache) CleaningService.CleanMediaPlayersCache();
                if (CleanUTorrent) CleaningService.CleanUTorrentCache();
                if (CleanFileZilla) CleaningService.CleanFileZillaRecentServers();
            });

            IsCleaning = false;
            StatusMessage = "✓ Limpeza concluída com sucesso!";
            LogService.LogSuccess("Limpeza do sistema concluída");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
