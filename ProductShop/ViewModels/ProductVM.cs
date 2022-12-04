using ProductShop.Connection;
using System;

namespace ProductShop.ViewModels
{
    public class ProductVM : ViewModelBase
    {
        public int ID { get; }
        public string Name { get; }
        public DateTime AddedDate { get; }
        public string Description { get; }
        public byte[] Photo { get; }
        public decimal Cost { get; }
        public decimal Count { get; }
        public UnitType UnitType { get; }
        public string UnitName => UnitType.Name;

        public ProductVM(Product product)
        {
            ID = product.ID;
            Name = product.Name;
            AddedDate = product.AddedDate;
            Description = product.Description;
            Photo = product.Photo ?? SystemImage.GetSystemImageBytes("noimage");
            Cost = product.Cost;
            Count = product.Count;
            UnitType = product.UnitType;
        }
    }
}
