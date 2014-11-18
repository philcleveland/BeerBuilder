using BrewDay.NancyFX.API.Models;
using CollectionJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDay.NancyFX.API
{
    public class IngredientDocumentWriter : ICollectionJsonDocumentWriter<Ingredient>
    {
        public IReadDocument Write(IEnumerable<Ingredient> data)
        {
            throw new NotImplementedException();
        }
    }
}
