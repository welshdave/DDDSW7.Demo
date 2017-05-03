namespace DDDSW7.Demo
{
    using LiteDB;
    using Microsoft.AspNetCore.Hosting;
    using Model;
    public class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://+:8080")
                .Build();
            
            host.Run();
        }
    }
}
