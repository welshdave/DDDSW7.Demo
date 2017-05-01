namespace DDDSW7.Demo.Model
{
    using System;
    using System.Collections.Generic;
    public class Order
    {
        public Guid OrderNumber { get; set; }
        public List<OrderItem> Items { get; set;}
        public string Status { get; set; }
    }
}