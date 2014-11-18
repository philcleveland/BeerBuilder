using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewDay.NancyFX.API.Modules
{
    public class CalculatorModule : NancyModule
    {
        public CalculatorModule() 
            :base("brewdayApi/calculators")
        {
            Get["/"] = p =>
            {
                //just return a bunch of links to all the calculators here
                //SRM
                //ABV
                //SG
                //etc
                return "";
            };
        }
    }
}