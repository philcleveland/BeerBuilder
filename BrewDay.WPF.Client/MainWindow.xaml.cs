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

namespace BrewDay.WPF.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var ingrediants =new List<Tuple<double,double>>();
            ingrediants.Add(new Tuple<double,double>(2.0, 13.75));
            ingrediants.Add(new Tuple<double, double>(22.5, 0.25));
            ingrediants.Add(new Tuple<double, double>(23.75, 0.1875));

            var mcu = BeerBuilder.Functions.mcu(ingrediants.ToArray(), 5.0);
            var srm = (int)Math.Round(BeerBuilder.Functions.srm_color(mcu));

            var color = BeerBuilder.Functions.srmColorLookup.TryFind(srm);

            this.DataContext = new MainWindowViewModel(color.Value);
        }
    }
}
