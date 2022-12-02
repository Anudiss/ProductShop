using ProductShop.Connection;
using System.Data.Entity;
using System.Windows.Controls;

namespace ProductShop.Windows.Main.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            DatabaseContext.Entities.Product.Load();
            InitializeComponent();

            ProductContainer.ItemsSource = DatabaseContext.Entities.Product.Local;
        }
    }
}
