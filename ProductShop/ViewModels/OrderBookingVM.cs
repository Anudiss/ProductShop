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
        private RelayCommand _acceptCommand;
        private RelayCommand _deniCommand;

        public Action CloseWindow;

        public RelayCommand Close =>
            _close ?? (_close = new RelayCommand((arg) => CloseView()));
        public RelayCommand AddProductCommand =>
            _addProductCommand ?? (_addProductCommand = new RelayCommand((arg) => AddProduct(arg as Product), (arg) => !IsProcessing));
        public RelayCommand RemoveProductCommand =>
            _removeProductCommand ?? (_removeProductCommand = new RelayCommand((arg) => RemoveProduct(arg as Product_Order), (arg) => !IsProcessing));
        public RelayCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new RelayCommand((arg) => Save(), (arg) => !IsProcessing));
        public RelayCommand AcceptCommand =>
            _acceptCommand ?? (_acceptCommand = new RelayCommand((arg) => AcceptOrder()));
        public RelayCommand DeniCommand =>
            _deniCommand ?? (_deniCommand = new RelayCommand((arg) => DeniOrder()));

        private Order _order;
        private bool _isProcessing = false;

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

        public IEnumerable<ProductOrderVM> OrderProducts
        {
            get
            {
                var orderProducts = DatabaseContext.Entities.Product_Order.Local.Where(product_order => product_order.Order == _order)
                                                                                .Select(product_order =>
                                                                                {
                                                                                    var productOrderVM = new ProductOrderVM(product_order);
                                                                                    productOrderVM.PropertyChanged += (sender, arg) => OnPropertyChanged(nameof(Sum));
                                                                                    return productOrderVM;
                                                                                });

                return orderProducts;
            }
        }

        public IEnumerable<Product> Products => DatabaseContext.Entities.Product.Local.Where(product => product.Count > 0 && product.IsDeleted != true)
                                                                                      .Except(OrderProducts.Select(orderProduct => orderProduct.Product));
        public decimal? Sum => OrderProducts.Sum(e => e.Sum);

        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                _isProcessing = value;
                OnPropertyChanged();
            }
        }

        public OrderBookingVM(Order order)
        {
            _order = order ?? new Order()
            {
                ID = DatabaseContext.Entities.Order.Local.Last().ID + 1,
                Date = DateTime.Now,
                Stage = Stage.New,
                Employee = Session.Instance.User.Executor,
                Customer = Session.Instance.User.SingleCustomer
            };

            if (order == null || Session.Instance.User.UserRole != Permission.UserRole.Manager)
                return;

            _order.Employee = Session.Instance.User.Executor;
            _order.Stage = Stage.Processing;
            IsProcessing = true;
        }

        private void AcceptOrder()
        {
            _order.Stage = Stage.ForPayment;
            Save();
            CloseWindow?.Invoke();
        }

        private void DeniOrder()
        {
            _order.Stage = Stage.Denied;
            Save();
            CloseWindow?.Invoke();
        }

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
                        foreach (var entry in DatabaseContext.Entities.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added))
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
