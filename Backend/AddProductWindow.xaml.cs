using Backend.AddProduct;
using Backend.Communication;
using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Backend
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private IAddProduct backend;

        public AddProductWindow()
        {
            InitializeComponent();
            backend = new AddProductCB(new PrjProtokol(), this);
        }

        private void SaveProduct(object sender, RoutedEventArgs e)
        {
            if (backend.CreateProduct())
            {
                MessageBox.Show("Produktet er oprettet!");
            }
            else
            {
                MessageBox.Show("Der skete en fejl under oprettelse af produktet!");
            }

            Close();
        }
    }
}
