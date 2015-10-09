﻿using System.Windows;
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

            DataContext = new MainWindowViewModel();

            InitializeComponent();
        }

        public void CreateProduct(object sender, RoutedEventArgs e)
        {
            var window = new AddProductWindow();
            window.ShowDialog();
        }

        private void CloseMainWindowClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Er du nu HELT sikker?", "Advarsel", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}