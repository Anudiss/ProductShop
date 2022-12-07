using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using static ProductShop.Connection.DatabaseContext;

namespace ProductShop.ViewModels
{
    public class OrderPageVM : ViewModelBase
    {
        public ObservableCollection<OrderVM> Orders { get; set; }

        public OrderPageVM()
        {
            Entities.Order.Load();
            Orders = new ObservableCollection<OrderVM>(
                                collection: Entities.Order.Local.Select(selector: order => new OrderVM(order)));
        }
    }
}
