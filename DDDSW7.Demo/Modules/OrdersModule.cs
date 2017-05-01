namespace DDDSW7.Demo.Modules
{
    using Nancy;
    using Nancy.ModelBinding;
    using DDDSW7.Demo.Model;
    using DDDSW7.Demo.Extensions;
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
                var createdOrder = orderRepository.CreateNewOrder();
                return Response.AsCreatedResource(createdOrder.OrderNumber);
            });
        }
    }
}