namespace DDDSW7.Demo
{
    using LiteDB;
    using Microsoft.AspNetCore.Hosting;
    using Model;
    public class Program
    {
        static void Main(string[] args)
        {
            //Setup some data.
            using(var db = new LiteRepository(@"Orders.db"))
            {
                var items = db.Fetch<OrderItem>();
                if(items.Count == 0)
                {
                    db.Insert(new OrderItem{ProductCode = "QW123", ProductName = "Desk", Quantity = 2});                        
                    db.Insert(new OrderItem{ProductCode = "CXP433", ProductName = "Table", Quantity = 1});
                    db.Insert(new OrderItem{ProductCode = "PL645", ProductName = "Keyboard", Quantity = 1});
                    db.Insert(new OrderItem{ProductCode = "XK847", ProductName = "Chair", Quantity = 1});
                    db.Insert(new OrderItem{ProductCode = "ZZ555", ProductName = "Stool", Quantity = 5});
                }
            }


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://+:8080")
                .Build();
            
            host.Run();
        }
    }
}
