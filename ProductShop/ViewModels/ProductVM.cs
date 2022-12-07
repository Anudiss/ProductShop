using ProductShop.Connection;
using System;

namespace ProductShop.ViewModels
{
    public class ProductVM : ViewModelBase
    {
        public Product Product { get; set; }
        public int ID
        {
            get => Product.ID;
            set
            {
                Product.ID = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => Product.Name;
            set
            {
                Product.Name = value;
                OnPropertyChanged();
            }
        }
        public DateTime AddedDate
        {
            get => Product.AddedDate;
            set
            {
                Product.AddedDate = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => Product.Description;
            set
            {
                Product.Description = value;
                OnPropertyChanged();
            }
        }
        public byte[] Photo
        {
            get => Product.Photo ?? SystemImage.GetSystemImageBytes("noimage");
            set
            {
                Product.Photo = value;
                OnPropertyChanged();
            }
        }
        public decimal Cost
        {
            get => Product.Cost;
            set
            {
                Product.Cost = value;
                OnPropertyChanged();
            }
        }
        public decimal Count
        {
            get => Product.Count;
            set
            {
                Product.Count = value;
                OnPropertyChanged();
            }
        }
        public UnitType UnitType
        {
            get => Product.UnitType;
            set
            {
                Product.UnitType = value;
                OnPropertyChanged();
            }
        }
        public string UnitName => UnitType.Name;

        public ProductVM(Product product) =>
            Product = product ?? new Product();
    }
}
