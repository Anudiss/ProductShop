using ProductShop.Windows.Main;

namespace ProductShop.ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public RelayCommand Home { get; set; }
        public RelayCommand Products { get; set; }
        public RelayCommand Suppliers { get; set; }
        public RelayCommand Orders { get; set; }
        public RelayCommand Supplyes { get; set; }
        #endregion

        public NavigationVM()
        {
            Products = new RelayCommand((arg) => CurrentView = new ProductsPageVM());
        }
    }
}
