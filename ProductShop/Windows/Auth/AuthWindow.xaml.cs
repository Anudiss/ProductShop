using ProductShop.Connection;
using ProductShop.Cookie;
using ProductShop.Permission;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ProductShop.Windows.Auth
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public const int MaxAuthorizationAttempts = 3;
        public static TimeSpan AuthorizationBlockTime = new TimeSpan(0, 1, 0);

        private bool IsRememberMe => RememberMe.IsChecked == true;
        #region Error
        public string Error
        {
            get { return (string)GetValue(ErrorProperty); }
            set { SetValue(ErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Error.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(string), typeof(AuthWindow));
        #endregion

        private int AuthorizationAttemptCount = 0;

        public AuthWindow()
        {
            #region Database tables loading
            DatabaseContext.Entities.User.Load();
            DatabaseContext.Entities.Role.Load();
            #endregion

            InitializeComponent();

            LoadLastUserLogin();
        }

        private void LoadLastUserLogin() =>
            Login.Text = AuthSettings.Default.Login;

        private void TryToBlockAuthorization()
        {
            if (++AuthorizationAttemptCount >= MaxAuthorizationAttempts)
                SetAuthorizationBlock();
        }

        private void SetAuthorizationBlock()
        {
            if (DateTime.Now - AuthSettings.Default.LastBlock <= AuthorizationBlockTime)
                return;

            AuthSettings.Default.LastBlock = DateTime.Now;
            AuthSettings.Default.Save();
        }

        private void TryToAuthorizateUser()
        {
            if (IsAuthorizationBlock())
            {
                BlockAuthorization();
                return;
            }

            Error = "";

            string login = Login.Text.Trim();
            string password = Password.Password.Trim();


            User authorizatedUser = DatabaseContext.Entities.User.Local.FirstOrDefault(user => user.Login == login && user.Password == password);
            if (authorizatedUser == null)
            {
                Error = "Неверный логин или пароль";
                TryToBlockAuthorization();
                return;
            }

            AuthorizateUser(authorizatedUser);
        }

        private static bool IsAuthorizationBlock() =>
            DateTime.Now - AuthSettings.Default.LastBlock <= AuthorizationBlockTime;

        private void BlockAuthorization()
        {
            Error = $"Вы использовали слишком много попыток ввода пароля, вы были заблокированы на 1 минуту\nОсталось {AuthorizationBlockTime - DateTime.Now.Subtract(AuthSettings.Default.LastBlock):%s}";
            AuthorizationAttemptCount = 0;
        }

        private void AuthorizateUser(User authorizatedUser)
        {
            TryToRememberUser();
            Session.Instance.User = authorizatedUser;
            MessageBox.Show("Вы авторизованы");
        }

        private void TryToRememberUser()
        {
            if (!IsRememberMe)
                return;

            AuthSettings.Default.Login = Login.Text.Trim();
            AuthSettings.Default.Save();
        }

        private void TryRegisterUser()
        {
            Error = "";
            string login = Login.Text.Trim();
            string password = Password.Password.Trim();

            if (DatabaseContext.Entities.User.Local.Any(user => user.Login == login))
            {
                Error = "Пользователь с таким логин уже существует";
                return;
            }

            RegisterUser(login, password);
        }

        private void RegisterUser(string login, string password)
        {
            try
            {
                ValidatePassword(password);

                User authorizatedUser = null;
                DatabaseContext.Entities.User.Local.Add(authorizatedUser = new User()
                {
                    Login = login,
                    Password = password,
                    Role_id = (int)UserRole.Customer
                });
                DatabaseContext.Entities.SaveChanges();

                AuthorizateUser(authorizatedUser);
            }
            catch (ArgumentException e)
            {
                Error = e.Message;
                return;
            }
        }

        private void ValidatePassword(string password)
        {
            if (password.Length < 6)
                throw new ArgumentException("Пароль должен быть не менее 6 символов");
            if (!password.Any(c => Char.IsUpper(c)))
                throw new ArgumentException("В пароле должна быть хотя бы одна прописная буква");
            if (!Regex.IsMatch(password, @"\d"))
                throw new ArgumentException("В пароле должна быть хотя бы одна цифра");
            if (!Regex.IsMatch(password, @"[\!\@\#\$\%\^]"))
                throw new ArgumentException("В пароле должен быть хотя бы одний из символов ! @ # $ % ^");
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e) =>
            TryToAuthorizateUser();

        private void OnRegisterButtonClick(object sender, RoutedEventArgs e) =>
            TryRegisterUser();
    }
}
