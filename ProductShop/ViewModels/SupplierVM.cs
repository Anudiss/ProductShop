using ProductShop.Connection;
using ProductShop.Windows.Main;
using System;
using System.Linq;

namespace ProductShop.ViewModels
{
    public class SupplierVM : ViewModelBase
    {
        private Supplier _supplier;
        private RelayCommand _editCommand;

        public RelayCommand EditCommand =>
            _editCommand ?? (_editCommand = new RelayCommand((arg) => EditSupplier()));

        public int ID
        {
            get => _supplier.ID;
            set
            {
                _supplier.ID = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _supplier.Name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Название оставщика не может быть пустым");
                if (value.Length > 100)
                    throw new ArgumentException("Название поставщика не может быть больше 100 символов");
                _supplier.Name = value;
                OnPropertyChanged();
            }
        }

        public SupplierVM(Supplier supplier) =>
            _supplier = supplier;

        private void EditSupplier()
        {

        }

        public void Save()
        {
            if (!DatabaseContext.Entities.Supplier.Local.Any(supplier => supplier.ID == _supplier.ID))
                DatabaseContext.Entities.Supplier.Local.Add(_supplier);
            DatabaseContext.Entities.SaveChanges();
        }
    }
}
