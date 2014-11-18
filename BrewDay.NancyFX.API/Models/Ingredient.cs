using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewDay.NancyFX.API.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double PPG { get; set; }
        public double ColorLower { get; set; }
        public double ColorUpper { get; set; }
        public double ColorAverage
        {
            get
            {
                if (ColorLower + ColorUpper == 0.0) return 0.0;
                return (ColorLower + ColorUpper) / 2.0;
            }
        }
    }
}