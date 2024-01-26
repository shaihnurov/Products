using CommunityToolkit.Mvvm.Input;
using Products.Core;
using Products.MVVM.Model;
using System.Windows;
using System.Windows.Input;

namespace Products.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public DelegateCommand HomeViewCommand { get; set; }
        public DelegateCommand EditProductViewCommand { get; set; }
        public DelegateCommand SettingsViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public EditProductViewModel EditProductVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        private object? _cuttentView;
        public object? CurrentView
        {
            get { return _cuttentView; }
            set
            {
                _cuttentView = value;
                OnPropertyChanged();
            }
        }
        public ICommand ExitApplicationCommand { get; private set; }
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
        public MainViewModel()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
            }

            HomeVM = new HomeViewModel();
            EditProductVM = new EditProductViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new DelegateCommand(o =>
            {
                CurrentView = HomeVM;
            });

            EditProductViewCommand = new DelegateCommand(o =>
            {
                CurrentView = EditProductVM;
            });

            SettingsViewCommand = new DelegateCommand(o =>
            {
                CurrentView = SettingsVM;
            });

            ExitApplicationCommand = new RelayCommand(ExitApplication);
        }
    }
}
