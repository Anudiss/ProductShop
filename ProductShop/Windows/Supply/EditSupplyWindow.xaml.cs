using ProductShop.ViewModels;
using System.Windows;

namespace ProductShop.Windows.Supply
{
    /// <summary>
    /// Логика взаимодействия для EditSupplyWindow.xaml
    /// </summary>
    public partial class EditSupplyWindow : Window
    {
        public EditSupplyWindow()
        {
            InitializeComponent();
        }

        private void EditSupplyWindowRoot_Loaded(object sender, RoutedEventArgs e) =>
            (DataContext as EditSupplyVM).CloseWindow = Close;


        private void EditSupplyWindowRoot_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                DragMove();
        }
    }
}
