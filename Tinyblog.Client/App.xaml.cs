using System;
using System.Windows;
using Tinyblog.Client.ViewModels;
using Tinyblog.Client.Views;

namespace Tinyblog.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string CurrentUser { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainView = new MainView();
            MainWindow = mainView;

            var clientBuilder = new ClientBuilder();
            mainView.DataContext = clientBuilder.CreateApplication();
            mainView.Show();

            var loginWindow = new LoginView
            {
                ShowInTaskbar = false,
                Owner = mainView
            };

            loginWindow.ShowDialog();

            CurrentUser = ((LoginViewModel)loginWindow.DataContext).UserName;
        }
    }
}
