using ProductShop.Connection;
using ProductShop.Cookie;
using ProductShop.Windows.Main;
using ProductShop.Windows.OrderBook;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop.ViewModels
{
    public class OrderVM : ViewModelBase
    {
        private readonly Order _order;
        private RelayCommand _openOrderCommand;
        private RelayCommand _payCommand;
        private RelayCommand _finishCommand;

        public RelayCommand OpenOrderCommand =>
            _openOrderCommand ?? (_openOrderCommand = new RelayCommand((arg) =>
            {
                new OrderBookingWindow()
                {
                    DataContext = new OrderBookingVM(_order)
                }.ShowDialog();
                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(Customer));
                OnPropertyChanged(nameof(Executor));
                OnPropertyChanged(nameof(Stage));
                OnPropertyChanged(nameof(SummaryProductsCount));
                OnPropertyChanged(nameof(SummaryProductsCost));
            }, (arg) => _order.Stage == Connection.Stage.New));
        public RelayCommand PayCommand =>
            _payCommand ?? (_payCommand = new RelayCommand((arg) => Pay()));
        public RelayCommand FinishCommand =>
            _finishCommand ?? (_finishCommand = new RelayCommand((arg) => Finish()));

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
        public bool IsPayment => _order.Stage == Connection.Stage.ForPayment;
        public bool IsNew => _order.Stage == Connection.Stage.New;
        public bool IsExecution => _order.Stage == Connection.Stage.Execution;

        public string CustomerFullName => 
            $"{_order.Customer.Surname} {_order.Customer.Name[0]}. {_order.Customer.Patronymic[0]}.";
        public string ExecutorFullName => Executor == null ? " нет" :
            $"{_order.Employee.Surname} {_order.Employee.Name[0]}. {_order.Employee.Patronymic[0]}.";
        public DateTime Date => _order.Date;

        public OrderVM(Order order) =>
            _order = order;

        private void Pay()
        {
            _order.Stage = Connection.Stage.Paid;
            Stage = DatabaseContext.Entities.OrderStage.First(orderStage => orderStage.ID == (int)Connection.Stage.Paid);
            OnPropertyChanged(nameof(Stage));
            OnPropertyChanged(nameof(IsPayment));
            OnPropertyChanged(nameof(IsNew));
            DatabaseContext.Entities.SaveChanges();
        }

        private void Finish()
        {
            _order.Stage = Connection.Stage.Done;
            Stage = DatabaseContext.Entities.OrderStage.First(orderStage => orderStage.ID == (int)Connection.Stage.Done);
            OnPropertyChanged(nameof(Stage));
            OnPropertyChanged(nameof(IsExecution));
            OnPropertyChanged(nameof(IsPayment));
            foreach (var order_product in _order.Product_Order)
                order_product.Product.Count -= (decimal)order_product.Count;

            DatabaseContext.Entities.SaveChanges();
        }
    }
}
