using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using System.Windows;
using Products.MVVM.Model;

namespace Products.MVVM.ViewModel
{
    class CalculationViewModel : ObservableObject
    {
        private ProductsDB _selectedProduct;
        public ProductsDB SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private ProductsDB _Item;
        public ProductsDB Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        private ICommand? _closeWindowCommand;
        public ICommand ClickBack
        {
            get
            {
                return _closeWindowCommand ??= new RelayCommand<object>(CloseWindow);
            }
        }

        private void CloseWindow(object? obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private double _sliderValue;
        public double SliderValue
        {
            get { return _sliderValue; }
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    OnPropertyChanged(nameof(SliderValue));
                    UpdateProductValues();
                }
            }
        }

        private void UpdateProductValues()
        {
            if (SelectedProduct != null)
            {
                Item.Proteins = (float)Math.Round((SelectedProduct.Proteins * SliderValue), 2);
                Item.Fats = (float)Math.Round((SelectedProduct.Fats * SliderValue), 2);
                Item.Carbohy = (float)Math.Round((SelectedProduct.Carbohy * SliderValue), 2);
                Item.Kilocalories = Math.Round((SelectedProduct.Kilocalories * SliderValue), 2);

                OnPropertyChanged(nameof(Item));
            }
        }

        public CalculationViewModel()
        {
            Item = new ProductsDB();
        }
    }
}
