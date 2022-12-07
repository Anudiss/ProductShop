using Microsoft.Win32;
using ProductShop.Connection;
using ProductShop.Windows.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ProductShop.ViewModels
{
    public class EditProductVM : ViewModelBase
    {
        public const int MaxImageSize = 15 * 1024;

        private RelayCommand _saveProductCommand;
        private RelayCommand _changeImageCommand;

        public IEnumerable<UnitType> UnitTypes => DatabaseContext.Entities.UnitType.Local;

        private Product Product { get; }
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
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Наименование не может быть пустым");
                if (Regex.IsMatch(value, @"[^\w\-\s]|\d"))
                    throw new ArgumentException("Наименование может содержать только буквы, пробел и дефис");
                Product.Name = value;
                OnPropertyChanged();
            }
        }
        public DateTime AddedDate
        {
            get => Product.AddedDate;
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

        public RelayCommand SaveProductCommand =>
            _saveProductCommand ?? (_saveProductCommand = new RelayCommand((arg) => Save()));
        public RelayCommand ChangeImageCommand =>
            _changeImageCommand ?? (_changeImageCommand = new RelayCommand((arg) => ChangeImage()));
        
        public EditProductVM(Product product) =>
            Product = product ?? new Product()
            {
                ID = DatabaseContext.Entities.Product.Local.Last().ID + 1,
                AddedDate = DateTime.Now,
                UnitType_id = 1
            };

        private void ChangeImage()
        {
            string filePath = OpenImageDialog();
            if (filePath == null)
                return;

            byte[] photo = File.ReadAllBytes(filePath);
            if (photo.Length >= MaxImageSize)
            {
                MessageBox.Show("Картинка не должна превышать 150 Кб");
                ChangeImage();
                return;
            }

            Photo = photo;
        }

        private string OpenImageDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Изображения|*.png;*.jpg",
                DefaultExt = "Изображения|*.png;*.jpg",
                CheckFileExists = true,
                Multiselect = false
            };
            if (fileDialog.ShowDialog() == true)
                return fileDialog.FileName;
            return null;
        }

        private void Save()
        {
            if (!DatabaseContext.Entities.Product.Local.Any(product => product.ID == ID))
                DatabaseContext.Entities.Product.Local.Add(Product);
            DatabaseContext.Entities.SaveChanges();
            NavigationVM.Instance.Products.Execute(null);
        }
    }
}
