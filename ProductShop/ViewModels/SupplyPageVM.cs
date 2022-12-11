using ProductShop.Connection;
using ProductShop.Windows.Main;
using ProductShop.Windows.Supply;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop.ViewModels
{
    public class SupplyPageVM : ViewModelBase
    {
        private RelayCommand _addDocumentCommand;

        public RelayCommand AddDocumentCommand =>
            _addDocumentCommand ?? (_addDocumentCommand = new RelayCommand((arg) => AddDocument()));

        public IEnumerable<SupplyVM> Supplies => DatabaseContext.Entities.Supply.Local.Select(supply => new SupplyVM(supply));

        public SupplyPageVM()
        {
            OnPropertyChanged(nameof(Supplies));
        }

        private void AddDocument()
        {
            new EditSupplyWindow()
            {
                DataContext = new EditSupplyVM(null)
            }.ShowDialog();
            OnPropertyChanged(nameof(Supplies));
        }
    }
}
