using ProductShop.Connection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop.ViewModels
{
    public class SupplyVM : ViewModelBase
    {
        private Supply _supply;

        public int ID => _supply.ID;
        public DateTime Date
        {
            get => _supply.Date;
            set
            {
                _supply.Date = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Supply_Product> Products => DatabaseContext.Entities.Supply_Product.Local.Where(supply_product => supply_product.Supply == _supply);


        public SupplyVM()
        {
            
        }
    }
}
