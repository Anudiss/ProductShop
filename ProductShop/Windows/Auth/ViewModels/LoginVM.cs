using ProductShop.Connection;
using ProductShop.Cookie;
using ProductShop.ViewModels;
using ProductShop.Windows.Main;
using System;
using System.Data.Entity;
using System.Linq;
using static ProductShop.Connection.DatabaseContext;

namespace ProductShop.Windows.Auth.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        public static readonly TimeSpan LockDuration = new TimeSpan(hours: 0, minutes: 1, seconds: 0);
        public static readonly int MaxAuthorizationAttempts = 3;

        private string _login = string.Empty;
        private string _password = string.Empty;
        private bool _rememberMe = true;
        private string _error;

        private RelayCommand _loginCommand;
        
        public RelayCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new RelayCommand(execute: (arg) => AuthorizateUser()));

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                OnPropertyChanged();
            }
        }
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        private int AuthorizationAttempt { get; set; } = 0;

        public LoginVM() =>
            InitializeLoginView();

        private void InitializeLoginView()
        {
            Entities.User.Load();
            LoadLastUser();
        }

        #region Загрузка последнего пользователя
        private bool HasLastUser => !string.IsNullOrEmpty(AuthSettings.Default.Login);
        private string LastUserLogin
        {
            get => AuthSettings.Default.Login;
            set => AuthSettings.Default.Login = value;
        }

        private void LoadLastUser()
        {
            if (!HasLastUser)
                return;

            Login = LastUserLogin;
        }
        #endregion
        #region Блокировка входа
        private DateTime LastLock
        {
            get => AuthSettings.Default.LastBlock;
            set
            {
                AuthSettings.Default.LastBlock = value;
                AuthSettings.Default.Save();
            }
        }
        private bool IsLocked => DateTime.Now.Subtract(LastLock) <= LockDuration;
        private TimeSpan RemainedTime => LockDuration - DateTime.Now.Subtract(LastLock);

        private void LockAuthorization()
        {
            if (IsLocked)
                return;

            LastLock = DateTime.Now;

        }
        private void UseAuthorizationAttempt()
        {
            if (IsLocked)
                return;

            if (++AuthorizationAttempt >= MaxAuthorizationAttempts)
            {
                LockAuthorization();
                AuthorizationAttempt = 0;
            }
        }
        #endregion
        #region Авторизация
        private void AuthorizateUser()
        {
            Error = "";
            if (IsLocked)
            {
                /* Тут вывод ошибки о том, что вход заблокирован */
                Error = $"До разблокировки {RemainedTime:mm\\:ss}";
                return;
            }

            User authorizedUser = Entities.User.Local.FirstOrDefault(predicate: user => user.Login == Login &&
                                                                                        user.Password == Password);
            if (authorizedUser == null)
            {
                UseAuthorizationAttempt();
                /* Тут должно выводить ошибку, такого пользователя нет или не верный логин или пароль */
                Error = "Неверный логин или пароль";
                return;
            }

            RememberUser();
            Session.Instance.User = authorizedUser;
            AuthNavigation.Instance.CloseWindowCommand.Execute();
        }
        #endregion
        #region Запоминание пользователя
        private void RememberUser()
        {
            if (!RememberMe)
                return;

            LastUserLogin = Login;
        }
        #endregion
    }
}
