namespace DDDSW7.Demo.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using DDDSW7.Demo.Model;
    using Nancy.Siren;

    public class OrderItemLinkGenerator : ILinkGenerator
    {
        private ISirenDocumentWriter<OrderItemViewModel> writer;

        public OrderItemLinkGenerator(ISirenDocumentWriter<OrderItemViewModel> writer)
        {
            this.writer = writer;
        }
        public bool CanHandle(Type model)
        {
            var expectedType = typeof(IEnumerable<OrderItemViewModel>);

            if (expectedType.IsAssignableFrom(model))
            {
                return true;
            }

            return false;
        }

        public Siren Handle(object model, Uri uri)
        {
            var modeldata = model as IEnumerable<OrderItemViewModel>;
            var sirenDocWithItems = writer.Write(modeldata, uri);
            return sirenDocWithItems;
        }
    }
}