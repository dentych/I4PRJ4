using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Backend.Views
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        public void SaveSettings(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        public void CancelSettings(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reload();
            Close();
        }

        public void TestConnection(object sender, RoutedEventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(Properties.Settings.Default.CSIP, Properties.Settings.Default.CSPort);
                client.Close();

                MessageBox.Show(this, "Forbindelse oprettet med succes!", "Test af forbindelse");
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Der kunne ikke oprettes forbindelse!", "Test af forbindelse");
            }
        }
    }
}
