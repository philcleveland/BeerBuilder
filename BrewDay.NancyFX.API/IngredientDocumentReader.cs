using BrewDay.NancyFX.API.Models;
using CollectionJson;
using System;

namespace BrewDay.NancyFX.API
{
    public class IngredientDocumentReader : ICollectionJsonDocumentReader<Ingredient>
    {
        public Ingredient Read(IWriteDocument document)
        {
            var template = document.Template;
            return new Ingredient()
            {
                Name = template.Data.GetDataByName("name").Value,
                Type = template.Data.GetDataByName("type").Value,
                PPG = double.Parse(template.Data.GetDataByName("ppg").Value),
                ColorLower = double.Parse(template.Data.GetDataByName("colorLower").Value),
                ColorUpper = double.Parse(template.Data.GetDataByName("colorUpper").Value)
            };
            
        }
    }
}
