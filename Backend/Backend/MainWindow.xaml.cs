using System.Windows;

namespace Backend
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CreateProduct(object sender, RoutedEventArgs e)
        {
            var window = new AddProductWindow();
            window.ShowDialog();
        }

        private void CloseMainWindowClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Er du nu HELT FUCKING sikker?", "Advarsel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
            
        }
    }
}