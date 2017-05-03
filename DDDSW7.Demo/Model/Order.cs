namespace DDDSW7.Demo.Model
{
    using System;
    using System.Collections.Generic;
    using LiteDB;

    public class Order
    {
        [BsonId]
        public Guid OrderNumber { get; set; }
        public List<OrderItem> Items { get; set;}
        public string Status { get; set; }
        public string Information { get; set; }
    }
}