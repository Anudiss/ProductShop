using ProductShop.Connection;
using System;

namespace ProductShop.ViewModels
{
    public class ProductOrderVM : ViewModelBase
    {
        public Product_Order Product_Order { get; set; }
        public event Action Changed;

        public Product Product
        {
            get => Product_Order.Product;
            set
            {
                Product_Order.Product = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
                Changed?.Invoke();
            }
        }
        public Order Order
        {
            get => Product_Order.Order;
            set
            {
                Product_Order.Order = value;
                OnPropertyChanged();
            }
        }
        public decimal? Count
        {
            get => Product_Order.Count;
            set
            {
                if (value > Product.Count)
                    throw new ArgumentException("Количество не может быть больше количества продуктов");
                if (value < 1)
                    throw new ArgumentException("Количество не может быть отрицательным");
                Product_Order.Count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
                Changed?.Invoke();
            }
        }
        public decimal? Sum => Product.Cost * Count;

        public ProductOrderVM(Product_Order product_Order) =>
            Product_Order = product_Order;
    }
}
