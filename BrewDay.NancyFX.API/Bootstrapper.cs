using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using CollectionJson;
using BrewDay.NancyFX.API.Models;
using Raven.Client;
using Nancy.TinyIoc;

namespace BrewDay.NancyFX.API
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            StaticConfiguration.DisableErrorTraces = false;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<Raven.Client.IDocumentStore>(GenerateRavenDocStore(container));
            container.Register<ICollectionJsonDocumentReader<Ingredient>, IngredientDocumentReader>();
            container.Register<ICollectionJsonDocumentWriter<Ingredient>, IngredientDocumentWriter>();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<IDocumentSession>(GenerateRavenSession(container));
        }

        private IDocumentStore GenerateRavenDocStore(TinyIoCContainer container)
        {
            var url = "http://localhost:8080";
            var docStore = new Raven.Client.Document.DocumentStore() { Url = url, DefaultDatabase="brewday" };
            docStore.Initialize();
            return docStore;
            
        }

        private IDocumentSession GenerateRavenSession(TinyIoCContainer container)
        {
            return container.Resolve<IDocumentStore>()
                    .OpenSession();
        }
    }
}