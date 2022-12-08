using ProductShop.ViewModels;
using ProductShop.Windows.Main;
using System;

namespace ProductShop.Windows.Auth.ViewModels
{
    public class AuthNavigation : ViewModelBase
    {
        private RelayCommand _openLoginFormCommand;
        private RelayCommand _openRegisterFormCommand;
        private RelayCommand _closeWindowCommand;

        public Action Close;

        public static AuthNavigation Instance { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenLoginFormCommand =>
            _openLoginFormCommand ?? (_openLoginFormCommand = new RelayCommand((arg) => CurrentView = new LoginVM()));
        public RelayCommand OpenRegisterFormCommand =>
            _openRegisterFormCommand ?? (_openRegisterFormCommand = new RelayCommand((arg) => CurrentView = new RegisterVM()));
        public RelayCommand CloseWindowCommand =>
            _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand((arg) => Close?.Invoke()));

        public AuthNavigation()
        {
            Instance = this;
            CurrentView = new LoginVM();
        }
    }
}
