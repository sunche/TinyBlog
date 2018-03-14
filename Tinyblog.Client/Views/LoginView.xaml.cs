using System;
using System.Windows;
using Tinyblog.Client.ViewModels;

namespace Tinyblog.Client.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(o =>
            {
                DialogResult = true;
                Close();
            });
        }
    }
}
