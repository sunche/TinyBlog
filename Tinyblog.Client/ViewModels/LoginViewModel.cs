using System;
using Tinyblog.Client.Common;

namespace Tinyblog.Client.ViewModels
{
    /// <summary>
    /// ViewModel for LoginView.
    /// </summary>
    /// <seealso cref="Tinyblog.Client.ViewModels.ViewModelBase"/>
    public class LoginViewModel : ViewModelBase
    {
        private string userName;

        [Obsolete]
        public LoginViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="loginAction">The login action.</param>
        public LoginViewModel(Action<object> loginAction)
        {
            Login = loginAction;
            LoginCommand = new Command(CanLogin, Login);
        }

        /// <summary>
        /// Gets or sets the login command.
        /// </summary>
        public Command LoginCommand { get; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName
        {
            get => userName;
            set
            {
                if (userName == value)
                {
                    return;
                }

                userName = value;
                OnPropertyChanged();
            }
        }

        private Action<object> Login { get; set; }

        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
