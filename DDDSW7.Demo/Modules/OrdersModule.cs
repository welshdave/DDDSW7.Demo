namespace DDDSW7.Demo.Modules
{
    using Nancy;
    using Nancy.ModelBinding;
    using DDDSW7.Demo.Model;
    using DDDSW7.Demo.Extensions;

    using System;
    using System.Collections.Generic;

    public class OrdersModule : NancyModule
    {
        public OrdersModule(IOrderRepository orderRepository)
            : base ("/orders")
        {
            Get("/", _ => 
            {
                var orders = orderRepository.GetAll();
                return orders;
            });

            Post("/", _ =>
            {
                var model = this.Bind<List<OrderItem>>();
                
                var createdOrder = orderRepository.CreateNewOrder(model);
                return Response.AsCreatedResource(createdOrder.OrderNumber);
            });

            Post("/{id:Guid}", parameters =>
            {
                var model = this.Bind<List<OrderItem>>();

                Guid id = parameters.id;
                var result = orderRepository.AddItemsToOrder(id, model);
                return result ? HttpStatusCode.Created : HttpStatusCode.NotFound;
            });

            Get ("/{id:Guid}", parameters =>
            {
                Guid id = parameters.id;

                var order = orderRepository.GetById(id);
                if(order == null)
                {
                    return HttpStatusCode.NotFound;
                }
                return order;
            });

            Get ("/{id:Guid}/items", parameters =>
             {
                 Guid id = parameters.id;

                 var items = orderRepository.GetItemsForOrder(id);

                 return items;
             });

            Delete ("/{id:Guid}", parameters =>
             {
                 Guid id = parameters.id;

                 var result = orderRepository.Delete(id);

                 return result ? HttpStatusCode.NoContent : HttpStatusCode.NotFound;
             });

            Post("/{id:Guid}/ship", parameters =>
            {
                Guid id = parameters.id;
                var model = this.Bind<ShipTo>();

                var order = orderRepository.GetById(id);

                if(order == null || order.Status == "Shipped" || order.Status == "Returned")
                {
                    return HttpStatusCode.NotFound;
                }
                
                orderRepository.ChangeStatus(id,"Shipped", $"Shipped to {model.Name}, {model.Address} on {DateTime.Now}\n");
                
                return HttpStatusCode.OK;
            });

            Post("/{id:Guid}/return", parameters =>
            {
                Guid id = parameters.id;
                var model = this.Bind<Return>();

                var order = orderRepository.GetById(id);

                if(order == null || order.Status != "Shipped")
                {
                    return HttpStatusCode.NotFound;
                }
                
                orderRepository.ChangeStatus(id,"Returned", $"Returned on {DateTime.Now}, reason: {model.Reason}");
                return HttpStatusCode.OK;
            });

        }
    }
}