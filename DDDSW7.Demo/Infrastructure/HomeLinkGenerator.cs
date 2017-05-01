namespace DDDSW7.Demo.Infrastructure
{
    using System;
    using DDDSW7.Demo.Model;
    using Nancy.Siren;

    public class HomeLinkGenerator : ILinkGenerator
    {
        private ISirenDocumentWriter<SiteDetail> writer;

        public HomeLinkGenerator(ISirenDocumentWriter<SiteDetail> writer)
        {
            this.writer = writer;
        }
        public bool CanHandle(Type model)
        {
            if (model == typeof(SiteDetail))
            {
                return true;
            }
            return false;
        }

        public Siren Handle(object model, Uri uri)
        {
            var data = model as SiteDetail;
            var sirenDoc = writer.Write(data, uri);
            return sirenDoc;
        }
    }
}