namespace DDDSW7.Demo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            this.ContractResolver = new CamelCasePropertyNamesContractResolver();
            this.NullValueHandling = NullValueHandling.Ignore;
            this.TypeNameHandling = TypeNameHandling.None;
        }
    }
}