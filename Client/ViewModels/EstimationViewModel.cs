using Client.Commands;
using Client.Models;
using Client.PrintService;
using Client.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class EstimationViewModel : INotifyPropertyChanged
    {

        #region Fields and Constants
        private UserService userService;
        private PrintFactory printFactory;
        #endregion

        #region Constructors
        public EstimationViewModel()
        {
            CalculateCommand = new RelayCommand(o => CalculateCommandHandler());
            ClearCommand = new RelayCommand(o => ClearCommandHandler());

            PrintToFileCommand = new RelayCommand(o => PrintToFileCommandHandler());
            PrintToPaperCommand = new RelayCommand(o => PrintToPaperCommandHandler());
            PrintToScreenCommand = new RelayCommand(o => PrintToScreenCommandHandler());

            //Default discount is 2%
            Discount = 2;

            userService = UserService.GetUserService();
            userService.UserLoggedIn += UserService_UserLoggedIn;
            
            printFactory = new PrintFactory();
        }

        private void UserService_UserLoggedIn(UserDetails userDetails)
        {
            WelcomeMessage = $"Welcome : {userService.CurrentUser.UserName.ToUpper()} User";
            if (userService.CurrentUser.UserType == UserType.Privileged)
                IsPrivilegedUser = true;
        }


        #endregion

        #region Public Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        private int _goldPrice;
        public int GoldPrice
        {
            get { return _goldPrice; }
            set
            {
                if (_goldPrice == value) return;
                _goldPrice = value;
                RaisePropertyChanged(nameof(GoldPrice));
            }
        }

        
        private int _goldWeight;
        public int GoldWeight
        {
            get { return _goldWeight; }
            set
            {
                if (_goldWeight == value) return;
                _goldWeight = value;
                RaisePropertyChanged(nameof(GoldWeight));
            }
        }


        private float _totalValue;
        public float TotalValue
        {
            get { return _totalValue; }
            set
            {
                if (_totalValue == value) return;
                _totalValue = value;
                RaisePropertyChanged(nameof(TotalValue));
            }
        }

        
        private float _disount;
        public float Discount
        {
            get { return _disount; }
            set
            {
                if (_disount == value) return;
                _disount = value;
                RaisePropertyChanged(nameof(Discount));
            }
        }

        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                if (_welcomeMessage == value) return;
                _welcomeMessage = value;
                RaisePropertyChanged(nameof(WelcomeMessage));
            }
        }

        private bool _isPrivilegedUser;

        public bool IsPrivilegedUser
        {
            get { return _isPrivilegedUser; }
            private set 
            {
                if (_isPrivilegedUser == value) return;
                _isPrivilegedUser = value;
                RaisePropertyChanged(nameof(IsPrivilegedUser));
            }
        }

        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand PrintToScreenCommand { get; }
        public ICommand PrintToPaperCommand { get; }
        public ICommand PrintToFileCommand { get; }


        #endregion

        #region Command Handlers
        private void CalculateCommandHandler()
        {
            float total = GoldWeight * GoldPrice;
            if (IsPrivilegedUser)
                total -= (total * (Discount / 100));

            TotalValue = total;
        }

        private void ClearCommandHandler()
        {
            GoldPrice = 0;
            GoldWeight = 0;
            TotalValue = 0;
        }

        private void PrintToScreenCommandHandler()
        {
            var screenPrint = printFactory.GetPrintType(PrintType.Screen);
            screenPrint.Print($"Total Value is : {TotalValue}");
        }

        private void PrintToPaperCommandHandler()
        {
            var paperPrint = printFactory.GetPrintType(PrintType.Paper);
            paperPrint.Print($"Total Value is : {TotalValue}");
        }

        private void PrintToFileCommandHandler()
        {
            var filePrint = printFactory.GetPrintType(PrintType.File);
            filePrint.Print($"Total Value is : {TotalValue}");
        }
        #endregion

        #region Private Methods
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
