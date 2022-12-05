using Microsoft.Win32;
using ProductShop.Connection;
using ProductShop.Windows.Main;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace ProductShop.ViewModels
{
    public class EditProductVM : ViewModelBase
    {
        private RelayCommand _saveProductCommand;
        private RelayCommand _changeImageCommand;

        public Product Product { get; set; }
        public int ID
        {
            get => Product.ID;
            set
            {
                Product.ID = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => Product.Name;
            set
            {
                Product.Name = value;
                OnPropertyChanged();
            }
        }
        public DateTime AddedDate
        {
            get => Product.AddedDate;
            set
            {
                Product.AddedDate = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => Product.Description;
            set
            {
                Product.Description = value;
                OnPropertyChanged();
            }
        }
        public byte[] Photo
        {
            get => Product.Photo ?? SystemImage.GetSystemImageBytes("noimage");
            set
            {
                Product.Photo = value;
                OnPropertyChanged();
            }
        }
        public decimal Cost
        {
            get => Product.Cost;
            set
            {
                Product.Cost = value;
                OnPropertyChanged();
            }
        }
        public decimal Count
        {
            get => Product.Count;
            set
            {
                Product.Count = value;
                OnPropertyChanged();
            }
        }
        public UnitType UnitType
        {
            get => Product.UnitType;
            set
            {
                Product.UnitType = value;
                OnPropertyChanged();
            }
        }
        public string UnitName => UnitType.Name;

        public RelayCommand SaveProductCommand =>
            _saveProductCommand ?? (_saveProductCommand = new RelayCommand((arg) => Save()));
        public RelayCommand ChangeImageCommand =>
            _changeImageCommand ?? (_changeImageCommand = new RelayCommand((arg) => ChangeImage()));

        public EditProductVM(Product product) =>
            Product = product ?? new Product()
            {
                ID = DatabaseContext.Entities.Product.Local.Last().ID + 1,
                AddedDate = DateTime.Now
            };

        private void ChangeImage()
        {
            string filePath = OpenImageDialog();
            if (filePath == null)
                return;

            Photo = File.ReadAllBytes(filePath);
        }

        private string OpenImageDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp",
                DefaultExt = "Изображения|*.png;*.jpg;*.jpeg;*.bmp",
                CheckFileExists = true,
                Multiselect = false
            };
            if (fileDialog.ShowDialog() == true)
                return fileDialog.FileName;
            return null;
        }

        private void Save()
        {
            if (!DatabaseContext.Entities.Product.Local.Any(product => product.ID == Product.ID))
                DatabaseContext.Entities.Product.Local.Add(Product);
            DatabaseContext.Entities.SaveChanges();
        }
    }
}
