using CommunityToolkit.Mvvm.Input;
using Products.MVVM.Model;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace Products.MVVM.ViewModel
{
    internal class SettingsViewModel : ObservableObject
    {
        public ICommand? DeleteBaseProductCommand { get; private set; }
        private void DeleteDatabaseButton()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
        }
        public ICommand? DokumentationCommand { get; private set; }

        private void Dokumentation()
        {
            string url = "https://onedrive.live.com/view.aspx?resid=CF500793D5C4A000%213128&authkey=!AM8I8rBaCfEqHSw";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        public SettingsViewModel()
        {
            DeleteBaseProductCommand = new RelayCommand(DeleteDatabaseButton);
            DokumentationCommand = new RelayCommand(Dokumentation);
        }
    }
}
