﻿using ProductShop.Windows.Main;
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

        public ObservableCollection<OrderVM> Orders => new ObservableCollection<OrderVM>(Entities.Order.Local.Select(selector: order => new OrderVM(order)));

        public OrderPageVM()
        {
            Entities.Order.Load();
        }
    }
}
