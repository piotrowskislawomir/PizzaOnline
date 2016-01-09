namespace PizzaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pizzaingredientmanytomanyaddedjoinedtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PizzaIngredients", "Pizza_Id", "dbo.Pizzas");
            DropForeignKey("dbo.PizzaIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropIndex("dbo.PizzaIngredients", new[] { "Pizza_Id" });
            DropIndex("dbo.PizzaIngredients", new[] { "Ingredient_Id" });
            CreateTable(
                "dbo.PizzasIngredients",
                c => new
                    {
                        PizzaId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PizzaId, t.IngredientId })
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.PizzaId, cascadeDelete: true)
                .Index(t => t.PizzaId)
                .Index(t => t.IngredientId);
            
            DropTable("dbo.PizzaIngredients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PizzaIngredients",
                c => new
                    {
                        Pizza_Id = c.Int(nullable: false),
                        Ingredient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pizza_Id, t.Ingredient_Id });
            
            DropForeignKey("dbo.PizzasIngredients", "PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.PizzasIngredients", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.PizzasIngredients", new[] { "IngredientId" });
            DropIndex("dbo.PizzasIngredients", new[] { "PizzaId" });
            DropTable("dbo.PizzasIngredients");
            CreateIndex("dbo.PizzaIngredients", "Ingredient_Id");
            CreateIndex("dbo.PizzaIngredients", "Pizza_Id");
            AddForeignKey("dbo.PizzaIngredients", "Ingredient_Id", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PizzaIngredients", "Pizza_Id", "dbo.Pizzas", "Id", cascadeDelete: true);
        }
    }
}
