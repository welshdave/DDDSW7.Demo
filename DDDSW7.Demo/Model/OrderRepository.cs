namespace DDDSW7.Demo.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LiteDB;

    public class OrderRepository : IOrderRepository
    {
        string connectionString = @"Orders.db";
        
        public Order CreateNewOrder(List<OrderItem> model)
        {
            using(var db = new LiteRepository(connectionString))
            {
                var newOrder = new Order
                {
                    OrderNumber = Guid.NewGuid(),
                    Status = "Pending",
                    Items = model
                };
                db.Insert<Order>(newOrder);
                return newOrder;
            }
        }
        
        public bool AddItemsToOrder(Guid id, List<OrderItem> model)
        {
            using(var db = new LiteRepository(connectionString))
            {
                var order = db.SingleOrDefault<Order>(x => x.OrderNumber == id);
                order.Items.AddRange(model);
                return db.Update<Order>(order);
            }
        }

        public bool Delete(Guid id)
        {
            using(var db = new LiteRepository(connectionString))
            {
                var result = db.Delete<Order>(x => x.OrderNumber == id);
                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using(var db = new LiteRepository(connectionString))
            {
                return db.Fetch<Order>();
            }
        }

        public Order GetById(Guid id)
        {
            using(var db = new LiteRepository(connectionString))
            {
                return db.SingleOrDefault<Order>(x => x.OrderNumber == id);
            }
        }

        public IEnumerable<OrderItemViewModel> GetItemsForOrder(Guid id)
        {
            var order = GetById(id);
            return order.Items.Select(item => new OrderItemViewModel
            {
                ProductCode = item.ProductCode,
                Quantity = item.Quantity,
                ProductName = $"Product {item.ProductCode}",
                ProductUrl = "https://www.google.co.uk/search?q=" + item.ProductCode,
                OrderNumber = id
            });
        }

        public bool ChangeStatus(Guid id, string status, string info)
        {
            using(var db = new LiteRepository(connectionString))
            {
                var order =  db.SingleOrDefault<Order>(x => x.OrderNumber == id);
                order.Status = status;
                order.Information += info;
                return db.Update<Order>(order);
            }
        }
    }
}