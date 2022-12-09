using ProductShop.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ProductShop.Windows.OrderBook
{
    /// <summary>
    /// Логика взаимодействия для OrderBookingWindow.xaml
    /// </summary>
    public partial class OrderBookingWindow : Window
    {
        public object OrderView { get; set; }

        public OrderBookingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) =>
            (DataContext as OrderBookingVM).CloseWindow = Close;

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
