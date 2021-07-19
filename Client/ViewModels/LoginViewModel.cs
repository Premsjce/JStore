using Client.Commands;
using Client.Models;
using Client.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Constructor

        public LoginViewModel()
        {
            ErrorMessage = string.Empty;
            LoginCommand = new RelayCommand(exec => LoginCommandHandler(), canexec => CanLoginCommandExecute());
            CancelCommand = new RelayCommand(exec => CancelCommandHandler(), canexec => true);
            userService = UserService.GetUserService();
        }
        #endregion

        #region Fields and Constants
        private UserService userService;
        #endregion

        #region Properties

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value) return;
                _userName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }


        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            { 
                _errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public bool LoginSuccessfull { get; set; }
        public bool LoginScreenVisible => !LoginSuccessfull;
        #endregion


        #region CommandHandlers
        private void LoginCommandHandler()
        {
            var currentUser = new UserDetails()
            {
                UserName = UserName,
                Password = Password
            };

            if (userService.LoginUser(currentUser))
            {
                ErrorMessage = string.Empty;
                LoginSuccessfull = true;
                return;
            }

            ErrorMessage = "Invalid Credentials";
            LoginSuccessfull = false;
        }

        //Basic Validation for Login command
        private bool CanLoginCommandExecute() => !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password));

        private void CancelCommandHandler()
        {
            //Code to close the application
        }
        #endregion


        #region Public Methods
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
