using System.Web.Http;
using NUnit.Framework;
using PizzaOnline.Api;

namespace PizzaOnline.Tests.Api
{
    public class BaseControllerTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AutoMapperConfig.Configure();
        }
    }
}
