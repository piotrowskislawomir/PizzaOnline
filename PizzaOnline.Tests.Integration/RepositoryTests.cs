using System;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using PizzaOnline.Model;
using PizzaOnline.Storage;

namespace PizzaOnline.Tests.Integration
{
    [TestFixture]
    public class RepositoryTests
    {
        [SetUp]
        public void SetUp()
        {
            _sut = new Repository<Ingredient>(_dbContextFactory);
        }

        private static Func<DbContext> _dbContextFactory;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _dbContextFactory = () => new PizzaOnlineContext("IntegrationTestsConnectionString");
            Database.SetInitializer(new DropCreateDatabaseAlways<PizzaOnlineContext>());
            _dbContextFactory().Database.Initialize(true);
        }
        

        private Repository<Ingredient> _sut;

        [Test]
        public void FindById_Should_Return_Null_When_Object_Og_Given_Id_Was_Not_Found()
        {
            var actual = _sut.FindById(4475438);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void GetAll_Returns_All_Items()
        {
            var model1 = new Ingredient { Id = null, Name = "Ing1", Price = 100.22M};
            var model2 = new Ingredient { Id = null,  Name = "Ing2", Price= 100.11M};

            _sut.Persist(model1);
            _sut.Persist(model2);
            var result = _sut.GetAll();

            Assert.That(result.Count(), Is.EqualTo(2));
            CollectionAssert.AllItemsAreUnique(result);
        }

        [Test]
        public void Persist_Should_Return_Copy_Of_Transient_Object_With_Id_Assigned()
        {
            var someTransientModel = new Ingredient { Id = null, Name = "Ing3", Price = 55M };

            var result = _sut.Persist(someTransientModel);

            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.Not.SameAs(someTransientModel));
            Assert.That(result.Price, Is.EqualTo(someTransientModel.Price));
            Assert.That(result.Name, Is.EqualTo(someTransientModel.Name));
            Assert.That(result.Id.HasValue);
        }

        [Test]
        public void Persisted_Data_Should_Be_Accesible_By_Id_Via_FindById()
        {
            var someTransientModel = new Ingredient { Id = null, Name = "Ing4", Price = 155M };

            var persisted = _sut.Persist(someTransientModel);
            var actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual.Id, Is.EqualTo(persisted.Id));
            Assert.That(actual.Price, Is.EqualTo(persisted.Price));
            Assert.That(actual.Name, Is.EqualTo(persisted.Name));
        }

        [Test]
        public void Persisted_Object_With_Already_Existing_Id_Should_Evict_Previus_Data()
        {
            var someTransientModel = new Ingredient { Id = null, Name = "Ing5", Price = 255M };

            var persisted = _sut.Persist(someTransientModel);
            var anotherWithSameId = new Ingredient { Id = someTransientModel.Id, Name = "Ing6", Price = 355M };
            _sut.Persist(anotherWithSameId);
            var actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual.Id, Is.EqualTo(persisted.Id));
            Assert.That(actual.Price, Is.EqualTo(anotherWithSameId.Price));
            Assert.That(actual.Name, Is.EqualTo(anotherWithSameId.Name));
        }

        [Test]
        public void Remove_Should_Remove_Item_Of_Same_Id_From_Storage()
        {
            var someTransientModel = new Ingredient { Id = null, Name = "Ing7", Price = 555M };

            var persisted = _sut.Persist(someTransientModel);
            var anotherWithSameId = new Ingredient { Id = persisted.Id };
            _sut.Remove(anotherWithSameId);

            var actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Subsequent_Persist_Calls_Objects_Should_Assign_Different_Id()
        {
            var someTransientModel = new Ingredient { Id = null, Name = "Ing8", Price = 655M };
            var anotherTransientModel = (Ingredient)someTransientModel.Clone();

            var result1 = _sut.Persist(someTransientModel);
            var result2 = _sut.Persist(anotherTransientModel);

            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);

            Assert.That(result1, Is.Not.SameAs(someTransientModel));
            Assert.That(result2, Is.Not.SameAs(someTransientModel));
            Assert.That(result1, Is.Not.SameAs(result2));
            Assert.That(result1.Id, Is.Not.Null);
            Assert.That(result2.Id, Is.Not.Null);
            Assert.That(result1.Id, Is.Not.EqualTo(result2.Id));
        }
    }
}
