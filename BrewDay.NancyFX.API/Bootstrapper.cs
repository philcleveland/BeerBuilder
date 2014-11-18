using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using CollectionJson;
using BrewDay.NancyFX.API.Models;

namespace BrewDay.NancyFX.API
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            StaticConfiguration.DisableErrorTraces = false;
        }

        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<ICollectionJsonDocumentReader<Ingredient>, IngredientDocumentReader>();
            container.Register<ICollectionJsonDocumentWriter<Ingredient>, IngredientDocumentWriter>();
        }

        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
        }
    }
}