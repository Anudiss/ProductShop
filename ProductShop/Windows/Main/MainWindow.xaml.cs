using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ProductShop.Windows.Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
        }

        public static void ChangePage(Page page) =>
            Instance.PageContainer.Navigate(page);
    }

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExeute)
        {
            _execute = execute;
            _canExecute = canExeute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) == true;

        public void Execute(object parameter) => _execute?.Invoke(parameter);
    }
}
