using ProductShop.Connection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace ProductShop.ViewModels
{
    public class ProductsPageVM : ViewModelBase
    {
        private ObservableCollection<ProductVM> _products;

        public ObservableCollection<ProductVM> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ProductsPageVM()
        {
            DatabaseContext.Entities.Product.Load();
            IEnumerable<ProductVM> productViewModels = DatabaseContext.Entities.Product.Local.Select(product => new ProductVM(product));
            Products = new ObservableCollection<ProductVM>(productViewModels);
        }
    }
}
