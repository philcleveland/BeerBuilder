using BrewDay.NancyFX.API.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BrewDay.NancyFX.API.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
            : base("/brewdayApi")
        {
            Get["/ingredients"] = p =>
                {
                    var data = System.Web.Hosting.HostingEnvironment.MapPath("~//App_Data//Malts.csv");
                    using (var fs = new FileStream(data, FileMode.Open, FileAccess.Read))
                    {
                        var malts = BeerBuilder.Malts.GetMalts(fs).Select(x => new Ingredient()
                        {
                            Name = x.Name,
                            Type = x.Type,
                            PPG = x.PPG,
                            ColorLower = x.ColorLower,
                            ColorUpper = x.ColorUpper
                        });
                        return malts;
                    }
                };
        }
    }
}