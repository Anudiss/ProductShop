using ProductShop.Connection;
using ProductShop.Windows.Main;

namespace ProductShop.ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        private static NavigationVM _instance;

        public static NavigationVM Instance =>
            _instance ?? (_instance = new NavigationVM());

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
        public RelayCommand EditProduct { get; set; }
        public RelayCommand CreateNewProduct { get; set; }
        #endregion

        public NavigationVM()
        {
            Products = new RelayCommand((arg) => CurrentView = new ProductsPageVM());
            EditProduct = new RelayCommand((arg) => CurrentView = new EditProductVM(arg as Product));
            CreateNewProduct = new RelayCommand((arg) => CurrentView = new EditProductVM(null));
        }
    }
}
