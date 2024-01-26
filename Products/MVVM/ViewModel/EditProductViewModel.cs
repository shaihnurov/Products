using Products.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using Products.Core;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using CommunityToolkit.Mvvm.Input;

namespace Products.MVVM.ViewModel
{
    internal class EditProductViewModel : ObservableObject
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

        private ProductsDB _newItem = new ProductsDB();
        public ProductsDB NewItem
        {
            get { return _newItem; }
            set
            {
                _newItem = value;
                OnPropertyChanged("NewItem");
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

        public ICommand DeleteCommand { get; }
        public ICommand EditBaseClick
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditProductDBClick();
                });
            }
        }
        public ICommand AddItemCommand { get; set; }

        public EditProductViewModel()
        {
            _dataList = new ObservableCollection<ProductsDB>();
            db.Database.EnsureCreated();
            db.ProductsDBs.Load();
            LoadDataFromDatabase();

            AddItemCommand = new RelayCommand(AddNewItem);

            DeleteCommand = new DelegateCommand(DeleteItemFromDB);
        }

        private void LoadDataFromDatabase()
        {
            DataList = new ObservableCollection<ProductsDB>(db.ProductsDBs.ToList());
            OnPropertyChanged(nameof(DataList));
        }

        private void EditProductDBClick()
        {
            try
            {
                foreach (var item in _dataList)
                {
                    var existingItem = db.ProductsDBs.FirstOrDefault(o => o.Id == item.Id);

                    if (existingItem != null)
                    {
                        existingItem.Name = item.Name;
                        existingItem.UClass = item.UClass;
                        existingItem.Proteins = item.Proteins;
                        existingItem.Fats = item.Fats;
                        existingItem.Carbohy = item.Carbohy;
                        existingItem.Kilocalories = item.Kilocalories;
                    }
                }
                db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(Application.Current.MainWindow, "Возникла ошибка при сохранении значения. Пожалуйста, проверьте данные", "Error in database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateConcurrencyException)
            {
                MessageBox.Show(Application.Current.MainWindow, "Данные не определенны", "Error in database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(Application.Current.MainWindow, "База данных не найдена", "Error in database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteItemFromDB(object parameter)
        {
            try
            {
                if (SelectedItem != null)
                {
                    db.ProductsDBs.Remove(SelectedItem);
                    db.SaveChanges();

                    DataList.Remove(SelectedItem);
                    OnPropertyChanged(nameof(DataList));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                MessageBox.Show(Application.Current.MainWindow, "Вы не выбрали элемент для удаления", "Error in database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(Application.Current.MainWindow, "Состояние объекта не определенно", "Error in database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(Application.Current.MainWindow, "База данных не найдена", "Error in database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewItem()
        {
            try
            {
                db.ProductsDBs.Add(NewItem);
                db.SaveChanges();

                LoadDataFromDatabase();

                NewItem = new ProductsDB();
                OnPropertyChanged(nameof(NewItem));
            }
            catch (DbUpdateConcurrencyException)
            {
                MessageBox.Show(Application.Current.MainWindow, "Данные не определенны", "Error in database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException)
            {
                db.Database.EnsureCreated();
                MessageBox.Show(Application.Current.MainWindow, "База успешно создана.\nТеперь вы можете добавить продукцию", "Database in created", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
