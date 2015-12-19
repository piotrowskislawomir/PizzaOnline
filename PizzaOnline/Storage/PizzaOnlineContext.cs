using System.Data.Entity;
using PizzaOnline.Model;

namespace PizzaOnline.Storage
{
    public class PizzaOnlineContext : DbContext
    {
        // Your context has been configured to use a 'PizzaOnlineDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PizzaOnline.PizzaOnlineDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PizzaOnlineDb' 
        // connection string in the application configuration file.
        public PizzaOnlineContext()
            : base("name=PizzaOnlineConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Ingredient> Ingredients { get; set; }
    }
}