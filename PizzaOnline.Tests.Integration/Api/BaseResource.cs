using System.Configuration;
using RestSharp;

namespace PizzaOnline.Tests.Integration.Api
{
    public abstract class BaseResource
    {
        protected RestClient Client;

        protected BaseResource()
        {
            var apiUrl = ConfigurationManager.AppSettings["TestedApiUrl"];
            Client = new RestClient(apiUrl);  
        }
    }
}
