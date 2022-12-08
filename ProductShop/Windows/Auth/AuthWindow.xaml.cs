using ProductShop.Windows.Auth.ViewModels;
using ProductShop.Windows.Main;
using System.Windows;

namespace ProductShop.Windows.Auth
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            AuthNavigation.Instance.Close = () =>
            {
                new MainWindow().Show();
                Close();
            };
        }
    }
}
