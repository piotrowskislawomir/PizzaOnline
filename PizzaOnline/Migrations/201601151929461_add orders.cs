namespace PizzaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Address = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrdersIngredients",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.IngredientId })
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.OrdersPizzas",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        PizzaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.PizzaId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.PizzaId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.PizzaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersPizzas", "PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.OrdersPizzas", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrdersIngredients", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrdersIngredients", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.OrdersPizzas", new[] { "PizzaId" });
            DropIndex("dbo.OrdersPizzas", new[] { "OrderId" });
            DropIndex("dbo.OrdersIngredients", new[] { "IngredientId" });
            DropIndex("dbo.OrdersIngredients", new[] { "OrderId" });
            DropTable("dbo.OrdersPizzas");
            DropTable("dbo.OrdersIngredients");
            DropTable("dbo.Orders");
        }
    }
}
