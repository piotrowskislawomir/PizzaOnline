namespace PizzaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtoppingstopizza : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredients", "Pizza_Id", c => c.Int());
            CreateIndex("dbo.Ingredients", "Pizza_Id");
            AddForeignKey("dbo.Ingredients", "Pizza_Id", "dbo.Pizzas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Pizza_Id", "dbo.Pizzas");
            DropIndex("dbo.Ingredients", new[] { "Pizza_Id" });
            DropColumn("dbo.Ingredients", "Pizza_Id");
        }
    }
}
