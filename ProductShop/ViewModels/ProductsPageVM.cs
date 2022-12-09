using ProductShop.Connection;
using ProductShop.Windows.EditProduct;
using ProductShop.Windows.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private IEnumerable<ProductVM> _productViewPage;
        private bool _thisMonth = false;

        public ObservableCollection<ProductVM> Products
        {
            get => _products;
            set
            {
                _products = value;
                ProductsView = CollectionViewSource.GetDefaultView(value);
                ProductsView.Filter += (arg) =>
                {
                    ProductVM product = arg as ProductVM;
                    OnPropertyChanged(nameof(ProductsCount));
                    return Filter.Predicate(product) &&
                          (SearchText == null ||
                           product.Name.ToLower().Trim().Contains(SearchText) ||
                           product.Description.ToLower().Trim().Contains(SearchText)) &&
                          (!ThisMonth || product.AddedDate.Month == DateTime.Now.Month);
                };
                ItemsPerPageVariants.Last().Value = value.Count;
                DoViewOperation();
                OnPropertyChanged();
            }
        }

        public ICollectionView ProductsView { get; set; }
        public IEnumerable<ProductVM> ProductsViewPage
        {
            get => _productViewPage;
            set
            {
                _productViewPage = value;
                OnPropertyChanged();
            }
        }

        public int ProductsCount => ProductsView.Cast<object>().Count();

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
        public bool ThisMonth
        {
            get => _thisMonth;
            set
            {
                _thisMonth = value;
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
                CurrentPage = 0;
                OnPropertyChanged();
            }
        }

        public int ItemsPerPage => ItemsPerPageVariant.Value;

        public int PagesCount => Math.Max((int)Math.Floor(((double)ProductsView.Cast<object>().Count() - 1) / ItemsPerPage), 0);
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = Math.Max(0, Math.Min(PagesCount, value));
                LoadPage();
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentPageToBind));
            }
        }
        public int CurrentPageToBind => CurrentPage + 1;
        public int PagesCountToBind => PagesCount + 1;

        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand CreateNewProductCommand { get; }

        public static ProductsPageVM Instance { get; set; }

        static ProductsPageVM()
        {
            DatabaseContext.Entities.UnitType.Load();
            foreach (var unitType in DatabaseContext.Entities.UnitType.Local)
                Filters.Add(new Filter($"{unitType.Name}", (product) => product.UnitType == unitType));
        }
        public ProductsPageVM()
        {
            Instance = this;
            DatabaseContext.Entities.Product.Load();
            IEnumerable<ProductVM> productViewModels = DatabaseContext.Entities.Product.Local.Where(product => product.IsDeleted != true)
                                                                .Select(product => new ProductVM(product));

            NextPageCommand = new RelayCommand((arg) => CurrentPage++);
            PreviousPageCommand = new RelayCommand((arg) => CurrentPage--);
            CreateNewProductCommand = new RelayCommand((arg) =>
            {
                new EditProductWindow()
                {
                    DataContext = new EditProductVM(null)
                }.ShowDialog();
                Products = new ObservableCollection<ProductVM>(DatabaseContext.Entities.Product.Local.Where(product => product.IsDeleted != true)
                                                               .Select(product => new ProductVM(product)));
            });

            Products = new ObservableCollection<ProductVM>(productViewModels);

            DoViewOperation();
        }

        private void DoViewOperation()
        {
            if (Filter == null || Sorting == null)
                return;

            ProductsView.SortDescriptions.Clear();

            ProductsView.SortDescriptions.Add(Sorting.SortDescription);
            ProductsView.Refresh();

            CurrentPage = 0;
        }
        private void LoadPage()
        {
            ProductsViewPage = ProductsView.Cast<ProductVM>()
                                           .Skip(currentPage * ItemsPerPage)
                                           .Take(ItemsPerPage);
            OnPropertyChanged(nameof(PagesCountToBind));
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
