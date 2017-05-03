namespace DDDSW7.Demo.Infrastructure
{
    using Nancy.Siren;
    using DDDSW7.Demo.Model;
    using System;
    using System.Collections.Generic;

    public class HomeWriter : ISirenDocumentWriter<SiteDetail>
    {
        public Siren Write(IEnumerable<SiteDetail> data, Uri uri)
        {
            throw new NotImplementedException();
        }

        public Siren Write(SiteDetail data, Uri uri)
        {
            var sirenDoc = new Siren
            {
                @class = new [] { "siteDetail" },
                properties = data
            };

            sirenDoc.links = new List<Link>(){
                new Link { rel = new [] { "self" }, href = uri.ToString () },
                new Link { rel = new [] { "orders", "collection" }, href = uri + "orders"}
            };
            return sirenDoc;
        }
    }
}
