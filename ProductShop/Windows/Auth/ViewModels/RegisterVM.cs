using ProductShop.Connection;
using ProductShop.Cookie;
using ProductShop.Permission;
using ProductShop.ViewModels;
using ProductShop.Windows.Main;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using static ProductShop.Connection.DatabaseContext;

namespace ProductShop.Windows.Auth.ViewModels
{
    public class RegisterVM : ViewModelBase
    {
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phone;
        private string _email;
        private string _login;
        private string _password;
        private RelayCommand _registerCommand;

        public RelayCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new RelayCommand((arg) => RegisterCustomer()));

        public string Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                    throw new ArgumentException("Поле фамилия не может быть пустым");
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                    throw new ArgumentException("Поле имя не может быть пустым");
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged();
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                    throw new ArgumentException("Поле телефон не может быть пустым");
                if (!Regex.IsMatch(value.Replace(' ', '\0'), @"^(?:(?:\+7|8)\d{10})"))
                    throw new ArgumentException("Неверный формат телефона");
                _phone = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                    throw new ArgumentException("Поле email не может быть пустым");
                if (!Regex.IsMatch(value, @"^(?:[\w\W-[\s@]]+)@(?:[\w\W-[\s@]]+)\.(?:[\w\W-[\s@]]+)"))
                    throw new ArgumentException("Неверный формат почты");
                _email = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                    throw new ArgumentException("Поле логин не может быть пустым");
                if (Entities.User.Local.Any(user => user.Login == value))
                    throw new ArgumentException("Пользователь с таким логином уже существует");
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (value.Length < 6)
                    throw new ArgumentException("Пароль не может быть меньше 6 символов");
                if (!value.Any(c => Char.IsUpper(c)))
                    throw new ArgumentException("Пароль должен содержать хотя бы одну прописную букву");
                if (!value.Any(c => Char.IsDigit(c)))
                    throw new ArgumentException("Пароль должен содержать хотя бы одну цифру");
                if (!Regex.IsMatch(value, @"[\!\@\#\$\%\^]"))
                    throw new ArgumentException("Пароль должен содержать хотя бы один символ из набора ! @ # $ % ^");
                _password = value;
                OnPropertyChanged();
            }
        }

        private void RegisterCustomer()
        {
            if (string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Patronymic) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            User newUser = new User()
            {
                Login = Login,
                Password = Password,
                Role_id = (int)UserRole.Customer
            };

            Customer newCustomer = new Customer()
            {
                Surname = Surname,
                Name = Name,
                Patronymic = Patronymic,
                Phone = Phone,
                Email = Email,
                User = newUser
            };

            Entities.User.Add(newUser);
            Entities.Customer.Add(newCustomer);

            Entities.SaveChanges();

            Session.Instance.User = newUser;
            AuthNavigation.Instance.CloseWindowCommand.Execute();
        }
    }
}
