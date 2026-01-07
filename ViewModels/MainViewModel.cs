using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PretoBoost.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
        public void Execute(object? parameter) => _execute();
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private object? _currentPage;
        private string _currentPageName = "UNIVERSAL BOOST";
        private bool _isAdmin;

        public MainViewModel()
        {
            CheckAdminPrivileges();
            
            // ViewModels das páginas
            UniversalBoostVM = new UniversalBoostViewModel();
            Win10BoostVM = new Win10BoostViewModel();
            GpuBoostVM = new GpuBoostViewModel();
            CleaningVM = new CleaningViewModel();

            // Comandos de navegação
            NavigateToUniversalBoostCommand = new RelayCommand(() => NavigateTo("UNIVERSAL BOOST"));
            NavigateToWin10BoostCommand = new RelayCommand(() => NavigateTo("WIN10 BOOST"));
            NavigateToGpuBoostCommand = new RelayCommand(() => NavigateTo("GPU BOOST"));
            NavigateToCleaningCommand = new RelayCommand(() => NavigateTo("LIMPEZA"));

            // Iniciar na página Universal Boost
            CurrentPage = UniversalBoostVM;
        }

        public UniversalBoostViewModel UniversalBoostVM { get; }
        public Win10BoostViewModel Win10BoostVM { get; }
        public GpuBoostViewModel GpuBoostVM { get; }
        public CleaningViewModel CleaningVM { get; }

        public ICommand NavigateToUniversalBoostCommand { get; }
        public ICommand NavigateToWin10BoostCommand { get; }
        public ICommand NavigateToGpuBoostCommand { get; }
        public ICommand NavigateToCleaningCommand { get; }

        public object? CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); }
        }

        public string CurrentPageName
        {
            get => _currentPageName;
            set { _currentPageName = value; OnPropertyChanged(); }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; OnPropertyChanged(); }
        }

        public string AdminStatus => IsAdmin ? "✓ Administrador" : "⚠ Sem privilégios";

        private void NavigateTo(string pageName)
        {
            CurrentPageName = pageName;
            CurrentPage = pageName switch
            {
                "UNIVERSAL BOOST" => UniversalBoostVM,
                "WIN10 BOOST" => Win10BoostVM,
                "GPU BOOST" => GpuBoostVM,
                "LIMPEZA" => CleaningVM,
                _ => UniversalBoostVM
            };
        }

        private void CheckAdminPrivileges()
        {
            try
            {
                var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                IsAdmin = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            catch
            {
                IsAdmin = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
