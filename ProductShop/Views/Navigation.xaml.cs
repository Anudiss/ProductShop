using ProductShop.Windows.Main;
using System;
using System.Windows.Controls;

namespace ProductShop.Views
{
    /// <summary>
    /// Логика взаимодействия для Navigation.xaml
    /// </summary>
    public partial class Navigation : UserControl
    {
        public static RelayCommand ChangePageCommand = new RelayCommand(
            execute: (arg) => MainWindow.ChangePage((arg as Type).GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>()) as Page),
            canExeute: (arg) => arg is Type);

        public Navigation()
        {
            InitializeComponent();
        }
    }
}
