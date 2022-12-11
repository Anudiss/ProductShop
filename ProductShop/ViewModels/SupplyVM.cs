using ProductShop.Connection;
using ProductShop.Windows.Main;
using ProductShop.Windows.Supply;
using System;

namespace ProductShop.ViewModels
{
    public class SupplyVM : ViewModelBase
    {
        private Supply _supply;
        private RelayCommand _openDocumentCommand;

        public RelayCommand OpenDocumentCommand =>
            _openDocumentCommand ?? (_openDocumentCommand = new RelayCommand((arg) => OpenDocument()));

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

        public SupplyVM(Supply supply)
        {
            _supply = supply;
            OnPropertyChanged(nameof(ID));
        }

        private void OpenDocument()
        {
            new EditSupplyWindow()
            {
                DataContext = new EditSupplyVM(_supply)
            }.ShowDialog();
        }
    }
}
