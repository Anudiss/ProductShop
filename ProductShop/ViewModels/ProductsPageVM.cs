﻿using ProductShop.Connection;
using ProductShop.Windows.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Data;

namespace ProductShop.ViewModels
{
    public class ProductsPageVM : ViewModelBase
    {
        public static List<Filter> Filters { get; } = new List<Filter>()
        {
            new Filter("Все", (product) => true)
        };

        public static List<Sorting> Sortings { get; } = new List<Sorting>()
        {
            new Sorting("По наименование", "Name", ListSortDirection.Ascending),
            new Sorting("По дате добавления", "AddedDate", ListSortDirection.Descending)
        };

        public static List<ItemsPerPage> ItemsPerPageVariants { get; } = new List<ItemsPerPage>()
        {
            new ItemsPerPage("10", 10),
            new ItemsPerPage("50", 50),
            new ItemsPerPage("200", 200),
            new ItemsPerPage("Все", 0)
        };

        private ObservableCollection<ProductVM> _products;
        private Filter _filter = Filters[0];
        private Sorting _sorting = Sortings[0];
        private string _searchText;
        private int currentPage;
        private ItemsPerPage itemsPerPage = ItemsPerPageVariants[0];

        public ObservableCollection<ProductVM> Products
        {
            get => _products;
            set
            {
                _products = value;
                ProductsView = new CollectionViewSource() { Source = value }.View;
                ItemsPerPageVariants.Last().Value = value.Count;
                OnPropertyChanged();
            }
        }

        public ICollectionView ProductsView { get; set; }

        public Filter Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                DoViewOperation();
                OnPropertyChanged();
            }
        }
        public Sorting Sorting
        {
            get => _sorting;
            set
            {
                _sorting = value;
                DoViewOperation();
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get => _searchText?.ToLower().Trim();
            set
            {
                _searchText = value;
                DoViewOperation();
                OnPropertyChanged();
            }
        }
        public ItemsPerPage ItemsPerPageVariant
        {
            get => itemsPerPage;
            set
            {
                itemsPerPage = value;
                DoViewOperation();
                CurrentPage = 0;
                OnPropertyChanged();
            }
        }

        public int ItemsPerPage => ItemsPerPageVariant.Value;

        public int PagesCount => (int)Math.Floor((double)ProductsView.Cast<object>().Count() / ItemsPerPage);
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = Math.Max(0, Math.Min(PagesCount - 1, value));
                LoadPage();
                OnPropertyChanged();
            }
        }

        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }

        static ProductsPageVM()
        {
            DatabaseContext.Entities.UnitType.Load();
            foreach (var unitType in DatabaseContext.Entities.UnitType.Local)
                Filters.Add(new Filter($"{unitType.Name}", (product) => product.UnitType == unitType));
        }

        public ProductsPageVM()
        {
            DatabaseContext.Entities.Product.Load();
            IEnumerable<ProductVM> productViewModels = DatabaseContext.Entities.Product.Local.Select(product => new ProductVM(product));

            NextPageCommand = new RelayCommand((arg) => CurrentPage++);
            PreviousPageCommand = new RelayCommand((arg) => CurrentPage--);

            Products = new ObservableCollection<ProductVM>(productViewModels);

            DoViewOperation();
        }

        private void DoViewOperation()
        {
            if (Filter == null || Sorting == null)
                return;

            ProductsView.SortDescriptions.Clear();

            ProductsView.Filter = (arg) =>
            {
                ProductVM product = arg as ProductVM;
                return Filter.Predicate(product) &&
                      (SearchText == null ||
                       product.Name.ToLower().Trim().Contains(SearchText) ||
                       product.Description.ToLower().Trim().Contains(SearchText));
            };

            ProductsView.SortDescriptions.Add(Sorting.SortDescription);
            ProductsView.Refresh();

            CurrentPage = 0;
        }

        private void LoadPage()
        {
            IEnumerable<ProductVM> pageItems = ProductsView.Cast<ProductVM>()
                                                           .Skip(CurrentPage * ItemsPerPage)
                                                           .Take(ItemsPerPage);
            ProductsView.Filter += (arg) =>
            {
                ProductVM product = arg as ProductVM;
                return pageItems.Contains(product);
            };
        }
    }

    public class Filter
    {
        public string Name { get; }
        public Predicate<ProductVM> Predicate { get; }

        public Filter(string name, Predicate<ProductVM> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
    public class Sorting
    {
        public string Name { get; }
        public SortDescription SortDescription { get; }

        public Sorting(string name, string propertyName, ListSortDirection sortDirection)
        {
            Name = name;
            SortDescription = new SortDescription()
            {
                PropertyName = propertyName,
                Direction = sortDirection
            };
        }
    }
    public class ItemsPerPage
    {
        public string Name { get; }
        public int Value { get; set; }

        public ItemsPerPage(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}