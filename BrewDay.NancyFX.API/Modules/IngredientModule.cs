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
        public IngredientModule(IAsyncDocumentSession session)
            :base("brewdayApi/ingredients")
        {
            Get["/", true] = async (x,y) =>
            {
                //return a doc with links to:
                //get
                //search
                //each ingredient should contains links with the following rel values:
                //self

                //convert to collection+json
                return await session.Query<Ingredient>().ToListAsync();
            };
        }
    }
}