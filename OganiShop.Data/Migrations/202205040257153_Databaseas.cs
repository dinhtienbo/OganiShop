namespace OganiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Databaseas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderStatus");
        }
    }
}
