using ProductShop.Connection;
using ProductShop.Windows.Main;
using ProductShop.Windows.Supplier;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductShop.ViewModels
{
    public class SupplierPageVM : ViewModelBase
    {
        private RelayCommand _addSupplierCommand;

        public RelayCommand AddSupplierCommand =>
            _addSupplierCommand ?? (_addSupplierCommand = new RelayCommand((arg) => AddSupplier()));

        public IEnumerable<SupplierVM> Suppliers => DatabaseContext.Entities.Supplier.Local.Select(supplier => new SupplierVM(supplier));

        public SupplierPageVM()
        {
            DatabaseContext.Entities.Supplier.Load();

            OnPropertyChanged(nameof(Suppliers));
        }

        private void AddSupplier()
        {
            new SupplierEditWindow()
            {
                DataContext = new SupplierEditVM()
            }.ShowDialog();
            OnPropertyChanged(nameof(Suppliers));
        }
    }
}
