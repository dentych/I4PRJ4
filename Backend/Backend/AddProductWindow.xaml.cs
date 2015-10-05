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
            backend = new AddProductCB(new PrjProtokol(), new Client("127.0.0.1",9000));
        }

        private void SaveProduct(object sender, RoutedEventArgs e)
        {

            List<TextBox> boxes = new List<TextBox>
            {
                textboxName,
                textboxBarcode,
                textboxPrice
            };
            bool valid = true;

            foreach (var box in boxes)
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                {
                    box.Background = new SolidColorBrush(Color.FromRgb(229, 177, 177));
                    valid = false;
                }
                else box.Background = Brushes.White;
            }


            if (valid)
            {
                var data = new Dictionary<string, string>
                {
                    ["NAME"] = this.textboxName.ToString(),
                    ["PRICE"] = textboxPrice.ToString(),
                    ["BARCODE"] = textboxBarcode.ToString()
                };


                MessageBox.Show(backend.CreateProduct(data)
                    ? "Produktet er oprettet!"
                    : "Der skete en fejl under oprettelse af produktet!");
                Close();

            }

            
        }

        private void textboxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ",")
            {
                    e.Handled = true;
            }
        }

        private void Annuller(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
