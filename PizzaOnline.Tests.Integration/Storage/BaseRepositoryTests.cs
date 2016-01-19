using System;
using System.Data.Entity;
using NUnit.Framework;
using PizzaOnline.Storage;

namespace PizzaOnline.Tests.Integration.Storage
{
    public class BaseRepositoryTests
    {
        protected static Func<DbContext> DbContextFactory;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            DbContextFactory = () => new PizzaOnlineContext("IntegrationTestsConnectionString");
            Database.SetInitializer(new DropCreateDatabaseAlways<PizzaOnlineContext>());
            DbContextFactory().Database.Initialize(true);
        }
    }
}
