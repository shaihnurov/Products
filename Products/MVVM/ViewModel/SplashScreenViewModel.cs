using Products.Core;
using Products.MVVM.View;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace Products.MVVM.ViewModel
{
    class SplashScreenViewModel : ObservableObject
    {
        public SplashScreenViewModel()
        {
            BackgroundWorker worker = new()
            {
                WorkerReportsProgress = true
            };
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker)?.ReportProgress(i);
                Thread.Sleep(57);
            }
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            LoadingProgress = e.ProgressPercentage;
            LoadingText = $"Проверяем данные {e.ProgressPercentage}%";

            if (e.ProgressPercentage == 100)
                LoadingText = $"Запускаем приложение";
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            MainWindow mainWindow = new();
            MainViewModel mainViewModel = new();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();

            (Application.Current.MainWindow as SplashScreenView)?.Close();
        }

        private int _loadingProgress;
        public int LoadingProgress
        {
            get { return _loadingProgress; }
            set
            {
                if (_loadingProgress != value)
                {
                    _loadingProgress = value;
                    OnPropertyChanged(nameof(LoadingProgress));
                }
            }
        }

        private string? _loadingText;
        public string? LoadingText
        {
            get { return _loadingText; }
            set
            {
                if (_loadingText != value)
                {
                    _loadingText = value;
                    OnPropertyChanged(nameof(LoadingText));
                }
            }
        }
    }
}
