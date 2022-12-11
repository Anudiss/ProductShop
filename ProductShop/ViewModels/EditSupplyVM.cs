using ProductShop.Connection;
using ProductShop.Windows.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProductShop.ViewModels
{
    public class EditSupplyVM : ViewModelBase
    {
        private RelayCommand _closeCommand;
        private RelayCommand _addCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _saveCommand;
        private Supply _supply;

        public Action CloseWindow;

        public RelayCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new RelayCommand((arg) => Close()));
        public RelayCommand AddCommand =>
            _addCommand ?? (_addCommand = new RelayCommand((arg) => AddProduct(arg as Product), (arg) => !IsLocked));
        public RelayCommand RemoveCommand =>
            _removeCommand ?? (_removeCommand = new RelayCommand((arg) => RemoveProduct(arg as SupplyProductVM), (arg) => !IsLocked));
        public RelayCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new RelayCommand((arg) => Save()));

        public bool IsLocked { get; set; }

        public int ID => _supply.ID;
        public DateTime Date => _supply.Date;
        public Supplier Supplier
        {
            get => _supply.Supplier;
            set
            {
                _supply.Supplier = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SupplyProductVM> SupplyProducts => 
            new ObservableCollection<SupplyProductVM>(DatabaseContext.Entities.Supply_Product.Local.Where(supplyProduct => supplyProduct.Supply == _supply)
                                                                                                   .Select(supplyProduct => new SupplyProductVM(supplyProduct)));
        public IEnumerable<Product> Products => DatabaseContext.Entities.Product.Local.Except(SupplyProducts.Select(s => s.Product));

        public EditSupplyVM(Supply supply)
        {
            _supply = supply ?? new Supply()
            {
                Date = DateTime.Now
            };

            IsLocked = supply != null;
            OnPropertyChanged(nameof(SupplyProducts));
            OnPropertyChanged(nameof(IsLocked));
        }

        private void AddProduct(Product product)
        {
            DatabaseContext.Entities.Supply_Product.Local.Add(new Supply_Product()
            {
                Product = product,
                Supply = _supply,
                Cost = product.Cost,
                Count = 1
            });
            OnPropertyChanged(nameof(SupplyProducts));
            OnPropertyChanged(nameof(Products));
        }

        private void RemoveProduct(SupplyProductVM supplyProduct)
        {
            DatabaseContext.Entities.Supply_Product.Local.Remove(supplyProduct.Supply_Product);
            OnPropertyChanged(nameof(SupplyProducts));
            OnPropertyChanged(nameof(Products));
        }

        private void Close()
        {
            if (DatabaseContext.Entities.ChangeTracker.Entries().Any(entry => entry.State == System.Data.Entity.EntityState.Modified))
            {
                switch (MessageBox.Show("Хотите сохранить?", "Уведомление", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning))
                {
                    case MessageBoxResult.Yes:
                        Save();
                        break;
                    case MessageBoxResult.No:
                        foreach (var entry in DatabaseContext.Entities.ChangeTracker.Entries().Where(entry => entry.State == System.Data.Entity.EntityState.Modified))
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }

            CloseWindow?.Invoke();
        }

        private void Save()
        {
            foreach (var product in SupplyProducts)
                product.Product.Count += (decimal)product.Count;
            DatabaseContext.Entities.SaveChanges();
            Close();
        }
    }
}
