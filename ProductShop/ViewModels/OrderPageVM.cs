using ProductShop.Connection;
using ProductShop.Cookie;
using ProductShop.Permission;
using ProductShop.Windows.Main;
using ProductShop.Windows.OrderBook;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using static ProductShop.Connection.DatabaseContext;

namespace ProductShop.ViewModels
{
    public class OrderPageVM : ViewModelBase
    {
        private RelayCommand _openOrderBookingCommand;

        public RelayCommand OpenOrderBookingCommand =>
            _openOrderBookingCommand ?? (_openOrderBookingCommand = new RelayCommand((arg) =>
            {
                OrderBookingWindow orderBookingWindow = new OrderBookingWindow()
                {
                    DataContext = new OrderBookingVM(null)
                };
                orderBookingWindow.ShowDialog();
                OnPropertyChanged(nameof(Orders));
            }));

        public ObservableCollection<OrderVM> Orders =>
            new ObservableCollection<OrderVM>(Entities.Order.Local.Where(predicate: order => OrderShowing(order))
                                                                  .Select(selector: order => new OrderVM(order)));

        public OrderPageVM()
        {
            Entities.Order.Load();
        }

        private bool OrderShowing(Order order)
        {
            Stage[] storeKeeperStages = new[] { Stage.Paid, Stage.Execution };
            UserRole role = Session.Instance.User.UserRole;
            switch (role)
            {
                case UserRole.Admin:
                    return true;
                case UserRole.Customer:
                    Customer customer = Session.Instance.User.SingleCustomer;
                    return order.Customer == customer;
                case UserRole.Manager:
                    Employee manager = Session.Instance.User.Executor;
                    return order.Stage == Stage.New && (order.Employee == manager || order.Employee == null);
                case UserRole.Storekeeper:
                    return storeKeeperStages.Contains(order.Stage);
            }
            return true;
        }
    }
}
