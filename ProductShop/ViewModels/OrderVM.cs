using ProductShop.Connection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop.ViewModels
{
    public class OrderVM : ViewModelBase
    {
        private readonly Order _order;

        public int ID => _order.ID;
        public Customer Customer
        {
            get => _order.Customer;
            set
            {
                _order.Customer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CustomerFullName));
            }
        }
        public Employee Executor
        {
            get => _order.Employee;
            set
            {
                _order.Employee = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExecutorFullName));
            }
        }
        public OrderStage Stage
        {
            get => _order.OrderStage;
            set
            {
                _order.OrderStage = value;
                OnPropertyChanged();
            }
        }
        public List<Product_Order> Products => _order.Product_Order.ToList();
        public int SummaryProductsCount => Products.Sum(product_order => (int)product_order.Count);
        public decimal SummaryProductsCost => Products.Sum(product_order => product_order.Product.Cost);

        public string CustomerFullName => 
            $"{_order.Customer.Surname} {_order.Customer.Name[0]}. {_order.Customer.Patronymic[0]}.";
        public string ExecutorFullName =>
            $"{_order.Employee.Surname} {_order.Employee.Name[0]}. {_order.Employee.Patronymic[0]}.";
        public DateTime Date => _order.Date;

        public OrderVM(Order order) =>
            _order = order;
    }
}
