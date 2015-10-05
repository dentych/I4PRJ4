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
            Close();
        }
    }
}