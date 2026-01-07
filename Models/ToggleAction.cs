using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PretoBoost.Models
{
    public class ToggleAction : INotifyPropertyChanged
    {
        private bool _isEnabled;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _category = string.Empty;
        private Action? _enableAction;
        private Action? _disableAction;
        private bool _isApplying;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                    ExecuteAction();
                }
            }
        }

        public bool IsApplying
        {
            get => _isApplying;
            set { _isApplying = value; OnPropertyChanged(); }
        }

        public Action? EnableAction
        {
            get => _enableAction;
            set => _enableAction = value;
        }

        public Action? DisableAction
        {
            get => _disableAction;
            set => _disableAction = value;
        }

        private void ExecuteAction()
        {
            try
            {
                IsApplying = true;
                if (_isEnabled)
                {
                    _enableAction?.Invoke();
                }
                else
                {
                    _disableAction?.Invoke();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing action {Name}: {ex.Message}");
            }
            finally
            {
                IsApplying = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
