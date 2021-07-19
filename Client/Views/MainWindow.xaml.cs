using Client.Models;
using Client.Services;
using System.ComponentModel;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            userService = UserService.GetUserService();
            userService.UserLoggedIn += UserService_UserLoggedIn;

            IsLoginScreenVisible = Visibility.Visible;
            IsEstimationScreenVisible = Visibility.Collapsed;

        }

        #endregion

        #region Members
        private UserService userService;

        public event PropertyChangedEventHandler PropertyChanged;

        private void UserService_UserLoggedIn(UserDetails userDetails)
        {
            IsLoginScreenVisible = Visibility.Collapsed;
            IsEstimationScreenVisible = Visibility.Visible;
            RaisePropertyChanged(nameof(IsLoginScreenVisible));
            RaisePropertyChanged(nameof(IsEstimationScreenVisible));
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        public Visibility IsLoginScreenVisible { get; private set; }
        public Visibility IsEstimationScreenVisible { get; private set; }
        #endregion
    }
}
