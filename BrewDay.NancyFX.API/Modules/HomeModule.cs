using BrewDay.NancyFX.API.Models;
using Nancy;
using Raven.Client;
using System.Linq;

namespace BrewDay.NancyFX.API.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IDocumentSession _db;

        public HomeModule(IDocumentSession session)
            : base("/brewdayApi")
        {
            _db = session;

            Get["/"] = p =>
                {
                    //convert to return json-home document
                    //Tavis
                    return "";
                };
            
        }
    }
}