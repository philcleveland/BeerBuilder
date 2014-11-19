using BrewDay.NancyFX.API.Models;
using Nancy;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewDay.NancyFX.API.Modules
{
    public class RecipeModule : NancyModule
    {
        public RecipeModule(IDocumentSession session)
            : base("brewdayApi/recipes")
        {
            Get["/"] = p =>
            {
                //convert to collection+json
                //this resource can be used for creating via HTTP POST too
                //this resource can be used for deleting via HTTP DELETE too
                //this resource can be used for editing via HTTP PUT too
                //might be good to have action links to the calculators here
                var recipes = session.Query<Recipe>().ToList();
                return recipes;
            };
        }
    }
}