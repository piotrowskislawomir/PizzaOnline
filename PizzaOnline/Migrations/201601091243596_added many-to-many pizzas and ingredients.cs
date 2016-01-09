namespace PizzaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmanytomanypizzasandingredients : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredients", "Pizza_Id", "dbo.Pizzas");
            DropIndex("dbo.Ingredients", new[] { "Pizza_Id" });
            CreateTable(
                "dbo.PizzaIngredients",
                c => new
                    {
                        Pizza_Id = c.Int(nullable: false),
                        Ingredient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pizza_Id, t.Ingredient_Id })
                .ForeignKey("dbo.Pizzas", t => t.Pizza_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_Id, cascadeDelete: true)
                .Index(t => t.Pizza_Id)
                .Index(t => t.Ingredient_Id);
            
            DropColumn("dbo.Ingredients", "Pizza_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "Pizza_Id", c => c.Int());
            DropForeignKey("dbo.PizzaIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.PizzaIngredients", "Pizza_Id", "dbo.Pizzas");
            DropIndex("dbo.PizzaIngredients", new[] { "Ingredient_Id" });
            DropIndex("dbo.PizzaIngredients", new[] { "Pizza_Id" });
            DropTable("dbo.PizzaIngredients");
            CreateIndex("dbo.Ingredients", "Pizza_Id");
            AddForeignKey("dbo.Ingredients", "Pizza_Id", "dbo.Pizzas", "Id");
        }
    }
}
