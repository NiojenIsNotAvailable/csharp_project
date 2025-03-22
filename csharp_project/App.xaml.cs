using System.Windows;
using csharp_project.Views;

namespace csharp_project
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainView mainView = new MainView();
            mainView.Show();
        }
    }
}
