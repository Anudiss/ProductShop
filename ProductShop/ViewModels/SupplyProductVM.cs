using ProductShop.Connection;

namespace ProductShop.ViewModels
{
    public class SupplyProductVM : ViewModelBase
    {
        public Supply_Product Supply_Product;

        public Product Product
        {
            get => Supply_Product.Product;
            set
            {
                Supply_Product.Product = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
            }
        }
        public decimal? Count
        {
            get => Supply_Product.Count;
            set
            {
                Supply_Product.Count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
            }
        }
        public decimal? Cost
        {
            get => Supply_Product.Cost;
            set
            {
                Supply_Product.Cost = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
            }
        }
        public decimal? Sum => Count * Cost;

        public SupplyProductVM(Supply_Product supply_Product) =>
            Supply_Product = supply_Product;
    }
}
