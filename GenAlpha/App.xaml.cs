using GenAlpha.Core;
using System.Windows;

namespace GenAlpha
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup Ioc
            DI.Setup();

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
