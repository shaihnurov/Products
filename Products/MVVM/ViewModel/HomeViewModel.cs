using CommunityToolkit.Mvvm.Input;
using Products.Core;
using Products.MVVM.Model;
using Products.MVVM.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Products.MVVM.ViewModel
{
    internal class HomeViewModel : ObservableObject
    {
        ApplicationContext db = new();

        private ObservableCollection<ProductsDB> _dataList;
        public ObservableCollection<ProductsDB> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged(nameof(DataList));
            }
        }

        private ProductsDB? _selectedItem;
        public ProductsDB? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private string? _ProductNameTitle;
        public string? ProductNameTitle
        {
            get { return _ProductNameTitle; }
            set
            {
                _ProductNameTitle = value;
                OnPropertyChanged(nameof(ProductNameTitle));
            }
        }

        public ICommand ProductCalculationCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    if (o is ProductsDB selectedProduct)
                    {
                        CalculationView calculationView = new();
                        var calculationViewModel = new CalculationViewModel
                        {
                            SelectedProduct = selectedProduct
                        };
                        calculationView.DataContext = calculationViewModel;

                        Window owner = Application.Current.MainWindow;
                        calculationView.Owner = owner;

                        calculationView.ShowDialog();
                    }
                });
            }
        }

        public HomeViewModel()
        {
            db.Database.EnsureCreated();

            ClickButtonGrainsCommand = new RelayCommand(ClickButtonGrains);
            ClickButtonBreadCommand = new RelayCommand(ClickButtonBread);
            ClickButtonSweetsCommand = new RelayCommand(ClickButtonSweets);
            ClickButtonFruitCommand = new RelayCommand(ClickButtonFruit);
            ClickButtonVegetableCommand = new RelayCommand(ClickButtonVegetable);
            ClickButtonDrinkCommand = new RelayCommand(ClickButtonDrink);
            ClickButtonMilkCommand = new RelayCommand(ClickButtonMilk);
            ClickButtonMeatCommand = new RelayCommand(ClickButtonMeat);
            ClickButtonOtherCommand = new RelayCommand(ClickButtonOther);
        }

        public ICommand ClickButtonGrainsCommand { get; private set; }

        private void ClickButtonGrains()
        {
            ProductNameTitle = "Крупы";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Крупа).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonBreadCommand { get; private set; }

        private void ClickButtonBread()
        {
            ProductNameTitle = "Хлебные изделия";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Хлеб).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonSweetsCommand { get; private set; }

        private void ClickButtonSweets()
        {
            ProductNameTitle = "Кондитерские изделия";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Сладости).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonFruitCommand { get; private set; }

        private void ClickButtonFruit()
        {
            ProductNameTitle = "Фрукты";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Фрукты).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonVegetableCommand { get; private set; }

        private void ClickButtonVegetable()
        {
            ProductNameTitle = "Овощи";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Овощи).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonDrinkCommand { get; private set; }

        private void ClickButtonDrink()
        {
            ProductNameTitle = "Напитки";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Напитки).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonMilkCommand { get; private set; }

        private void ClickButtonMilk()
        {
            ProductNameTitle = "Молочные изделия";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Молочка).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonMeatCommand { get; private set; }

        private void ClickButtonMeat()
        {
            ProductNameTitle = "Мясо и яйца";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Мясо).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }

        public ICommand ClickButtonOtherCommand { get; private set; }

        private void ClickButtonOther()
        {
            ProductNameTitle = "Прочее";
            var result = db.ProductsDBs.Where(p => p.UClass == UClassType.Прочее).ToList();
            DataList = new ObservableCollection<ProductsDB>(result);
        }
    }
}
