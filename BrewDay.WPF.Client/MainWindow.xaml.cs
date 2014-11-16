using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using BeerBuilder;

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
            
            var ingrediants =new List<BeerBuilder.Domain.Fermentable>();
            ingrediants.Add(new BeerBuilder.Domain.Fermentable(2.0, 13.75));
            ingrediants.Add(new BeerBuilder.Domain.Fermentable(22.5, 0.25));
            ingrediants.Add(new BeerBuilder.Domain.Fermentable(23.75, 0.1875));

            var color = BeerBuilder.Functions.GetSRMDisplayColor(ingrediants.ToArray(), 5.0);

            this.DataContext = new MainWindowViewModel(color.Value);
        }
    }
}
