namespace DDDSW7.Demo.Model
{
    using System;
    public class OrderItemViewModel : OrderItem
    {
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public Guid OrderNumber { get; set; }
    }
}