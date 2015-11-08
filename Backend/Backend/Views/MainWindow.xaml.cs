using System.Windows;
using Backend.Communication;
using Backend.ViewModels;

namespace Backend.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var conn = LSC.Connection;
            //  conn.Connect("127.0.0.1", 7913); //TODO: Settings, Something to handle the no connection error
        }
    }
}