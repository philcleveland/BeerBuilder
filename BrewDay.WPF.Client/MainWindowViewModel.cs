using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BrewDay.WPF.Client
{
    public class MainWindowViewModel
    {
        //public MainWindowViewModel()
        //{
        //    SRMColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f8a600")); 
        //}

        public MainWindowViewModel(int color)
        {
            var bytes = BitConverter.GetBytes(color);
            var dispColor = Color.FromRgb(bytes[3], bytes[2], bytes[1] );
            var colorString = string.Format("#{0:X2}{1:X2}{2:X2}", bytes[2], bytes[1], bytes[0]);
            this.SRMColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorString));
        }

        public SolidColorBrush SRMColor { get; set; }
    }
}
