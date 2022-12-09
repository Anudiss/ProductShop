using ProductShop.Connection;
using System;

namespace ProductShop.ViewModels
{
    public class ProductOrderVM : ViewModelBase
    {
        private Product_Order _product_Order;
        public event Action Changed;

        public Product Product
        {
            get => _product_Order.Product;
            set
            {
                _product_Order.Product = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
                Changed?.Invoke();
            }
        }
        public Order Order
        {
            get => _product_Order.Order;
            set
            {
                _product_Order.Order = value;
                OnPropertyChanged();
            }
        }
        public decimal? Count
        {
            get => _product_Order.Count;
            set
            {
                if (value > Product.Count)
                    throw new ArgumentException("Количество не может быть больше количества продуктов");
                if (value < 1)
                    throw new ArgumentException("Количество не может быть отрицательным");
                _product_Order.Count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
                Changed?.Invoke();
            }
        }
        public decimal? Sum => Product.Cost * Count;

        public ProductOrderVM(Product_Order product_Order) =>
            _product_Order = product_Order;
    }
}
