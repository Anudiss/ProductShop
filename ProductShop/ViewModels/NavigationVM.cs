using ProductShop.Connection;
using ProductShop.Windows.Auth;
using ProductShop.Windows.Main;

namespace ProductShop.ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        //private static NavigationVM _instance;

        //public static NavigationVM Instance =>
        //    _instance ?? (_instance = new NavigationVM());

        //private object _currentView;

        //public object CurrentView
        //{
        //    get => _currentView;
        //    set
        //    {
        //        _currentView = value;
        //        OnPropertyChanged();
        //    }
        //}

        //#region Command
        //public RelayCommand Products { get; set; }
        //public RelayCommand Suppliers { get; set; }
        //public RelayCommand Orders { get; set; }
        //public RelayCommand Supplyes { get; set; }
        //public RelayCommand OpenAuth { get; set; }
        //#endregion

        //public NavigationVM()
        //{
        //    Products = new RelayCommand((arg) => CurrentView = new ProductsPageVM());
        //    Orders = new RelayCommand((arg) => CurrentView = new OrderPageVM());
        //    Suppliers = new RelayCommand((arg) => CurrentView = new SupplierPageVM());
        //    Supplyes = new RelayCommand((arg) => CurrentView = new SupplyPageVM());
        //    OpenAuth = new RelayCommand((arg) =>
        //    {
        //        new AuthWindow().Show();
        //        MainWindow.Instance.Close();
        //    });

        //    Products.Execute();
        //}
    }
}
