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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NET__ININ4_PR1._2_z1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
        }

        private void Cyfra(object sender, RoutedEventArgs e)
        {
            model.DopiszCyfrę(
                (string)((Button)sender).Content
                );
        }
        private void Przecinek(object sender, RoutedEventArgs e)
        {
            model.PrzełączUłamek();
        }

        private void Znak(object sender, RoutedEventArgs e)
        {
            model.PrzełączZnak();
        }

        private void Cofnij(object sender, RoutedEventArgs e)
        {
            model.CofnijZnak();
        }

        private void Skasuj(object sender, RoutedEventArgs e)
        {
            model.Skasuj();
        }

        private void Resetuj(object sender, RoutedEventArgs e)
        {
            model.Resetuj();
        }

        private void NoweZwykłeDziałanie(object sender, RoutedEventArgs e)
        {
            model.NoweDziałanie(
                (string)((Button)sender).Content
                );
        }

        private void RównaSię(object sender, RoutedEventArgs e)
        {
            model.ZwykłeDziałanie();
        }

        private void Procent(object sender, RoutedEventArgs e)
        {
            model.DziałanieProcentowe();
        }

        private void DziałanieJednoargumentowe(object sender, RoutedEventArgs e)
        {
            model.DziałanieJednoargumentowe(
                (string)((Button)sender).Content
                );
        }
    }
}
