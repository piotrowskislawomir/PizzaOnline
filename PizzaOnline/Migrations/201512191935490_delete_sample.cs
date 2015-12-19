namespace PizzaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_sample : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MyEntities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MyEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
