using ProductShop.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace ProductShop.Windows.Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = NavigationVM.Instance;
        }
    }

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExeute = null)
        {
            _execute = execute;
            _canExecute = canExeute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute.Invoke(parameter) == true;

        public void Execute(object parameter) => _execute?.Invoke(parameter);
    }
}
