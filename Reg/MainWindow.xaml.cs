using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Reg.Models;

namespace Reg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CarDetails carDetails = new CarDetails();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.carDetails;
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // pass the users vehicle registration with spaces removed
            var data = await RegInterface.GetRegDetails(this.vehicleRegistrationString.Text.Replace(" ",""));

            this.DataContext = null;

            if ((data != null) && (data.Any()))
            {
                this.DataContext = data.FirstOrDefault();
            }
        }
    }
}
