using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tinyblog.Client.Views;

namespace Tinyblog.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new MainView();
            var  clientBuilder = new ClientBuilder();
            window.DataContext = clientBuilder.CreateApplication();
            window.Show();
        }
    }
}
