using System;
using System.ComponentModel;

namespace ProductShop.Connection
{
    public partial class Product_Order : INotifyPropertyChanged
    {
        public decimal? Sum => Product?.Cost * Count;

        public decimal? ProductCount
        {
            get => Count;
            set
            {
                if (value < 1 || value > Product.Count)
                    throw new ArgumentException("Количество продуктов не может быть меньше 1 и больше запасов");
                Count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductCount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sum)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
