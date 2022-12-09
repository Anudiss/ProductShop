using Microsoft.Win32;
using ProductShop.Connection;
using ProductShop.Cookie;
using ProductShop.Windows.Main;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ProductShop.ViewModels
{
    public class OrderBookingVM : ViewModelBase
    {
        private RelayCommand _close;
        private RelayCommand _addProductCommand;
        private RelayCommand _removeProductCommand;
        private RelayCommand _saveCommand;

        public Action CloseWindow;

        public RelayCommand Close =>
            _close ?? (_close = new RelayCommand((arg) => CloseView()));
        public RelayCommand AddProductCommand =>
            _addProductCommand ?? (_addProductCommand = new RelayCommand((arg) => AddProduct(arg as Product)));
        public RelayCommand RemoveProductCommand =>
            _removeProductCommand ?? (_removeProductCommand = new RelayCommand((arg) => RemoveProduct(arg as Product_Order)));
        public RelayCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new RelayCommand((arg) => Save()));

        private Order _order;

        public int ID
        {
            get => _order.ID;
            set
            {
                _order.ID = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => _order.Date;
            set
            {
                _order.Date = value;
                OnPropertyChanged();
            }
        }
        public Customer Customer
        {
            get => _order.Customer;
            set
            {
                _order.Customer = value ?? throw new ArgumentException("Поле заказчик не может быть пустым");
                OnPropertyChanged();
            }
        }
        public Employee Executor
        {
            get => _order.Employee;
            set
            {
                _order.Employee = value;
                OnPropertyChanged();
            }
        }
        public OrderStage OrderStage
        {
            get => _order.OrderStage;
            set
            {
                _order.OrderStage = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Product_Order> OrderProducts => DatabaseContext.Entities.Product_Order.Local.Where(product_order => product_order.Order == _order);
        public IEnumerable<Product> Products => DatabaseContext.Entities.Product.Local.Where(product => product.Count > 0)
                                                                                      .Except(OrderProducts.Select(orderProduct => orderProduct.Product));
        public decimal? Sum => OrderProducts.Sum(e => e.Sum);

        public OrderBookingVM(Order order) =>
            _order = order ?? new Order()
            {
                Date = DateTime.Now,
                Stage = Stage.New,
                Employee = Session.Instance.User.UserRole == Permission.UserRole.Manager ? Session.Instance.User.Executor : null,
                Customer = Session.Instance.User.SingleCustomer
            };

        private void AddProduct(Product product)
        {
            DatabaseContext.Entities.Product_Order.Local.Add(new Product_Order()
            {
                Order = _order,
                Product = product,
                Count = 1
            });
            UpdateProductEnumrables();
        }

        private void RemoveProduct(Product_Order product_Order)
        {
            DatabaseContext.Entities.Product_Order.Local.Remove(product_Order);
            UpdateProductEnumrables();
        }

        private void UpdateProductEnumrables()
        {
            OnPropertyChanged(nameof(OrderProducts));
            OnPropertyChanged(nameof(Products));
            OnPropertyChanged(nameof(Sum));
        }

        private void CloseView()
        {
            if (DatabaseContext.Entities.ChangeTracker.HasChanges())
            {
                var result = MessageBox.Show("Хотите сохранить?", "Уведомление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Save();
                        break;
                    case MessageBoxResult.No:
                        foreach (var entry in DatabaseContext.Entities.ChangeTracker.Entries().Where(entry => entry.State == System.Data.Entity.EntityState.Added))
                            entry.State = EntityState.Detached;
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            CloseWindow?.Invoke();
        }

        private void Save() =>
            DatabaseContext.Entities.SaveChanges();
    }
}
