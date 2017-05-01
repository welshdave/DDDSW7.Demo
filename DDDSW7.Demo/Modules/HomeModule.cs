namespace DDDSW7.Demo.Modules
{
    using Nancy;
    using DDDSW7.Demo.Model;
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => new SiteDetail
            {
                Name = "DDDSW7 HATEOAS Demo",
                Description = "A Simple Siren Demo"
            });
        }
    }
}