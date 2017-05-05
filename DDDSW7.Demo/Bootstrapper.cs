namespace DDDSW7.Demo
{
    using System;
    using DDDSW7.Demo.Infrastructure;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Responses.Negotiation;
    using Nancy.Siren;
    using Nancy.TinyIoc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Model;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<JsonSerializer, CustomJsonSerializer>();

            container.Register<ISirenDocumentWriter<SiteDetail>, HomeWriter>();
            container.Register<ISirenDocumentWriter<Order>, OrderWriter>();
            container.Register<ISirenDocumentWriter<OrderItemViewModel>, OrderItemViewModelWriter>();


            container.RegisterMultiple<ILinkGenerator>(new[] { typeof(HomeLinkGenerator), typeof(OrderItemLinkGenerator), typeof(OrderLinkGenerator) });
        }
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                var processors = new[]
                {
                    typeof(JsonProcessor),
                    typeof(SirenResponseProcessor)
                };

                return NancyInternalConfiguration.WithOverrides(x => x.ResponseProcessors = processors);
            }
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });
        }
    }
}