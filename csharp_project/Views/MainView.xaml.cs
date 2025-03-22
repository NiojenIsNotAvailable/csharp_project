using System.Windows;
using csharp_project.ViewModels;

namespace csharp_project.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
