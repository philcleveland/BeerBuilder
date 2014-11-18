using BrewDay.NancyFX.API.Models;
using Nancy;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewDay.NancyFX.API.Modules
{
    public class IngredientModule : NancyModule
    {
        public IngredientModule(IDocumentSession session)
            :base("brewdayApi/ingredients")
        {
            Get["/"] = p =>
            {
                //return a doc with links to:
                //get
                //search
                //each ingredient should contains links with the following rel values:
                //self

                //convert to collection+json
                var malts = session.Query<Ingredient>();
                return malts.ToList();
            };
        }
    }
}