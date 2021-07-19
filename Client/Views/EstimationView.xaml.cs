using Client.ViewModels;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for EstimationView.xaml
    /// </summary>
    public partial class EstimationView : UserControl
    {
        public EstimationView()
        {
            InitializeComponent();
            DataContext = new EstimationViewModel();
        }
    }
}
