namespace wafabank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.jobs", "photo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.jobs", "photo", c => c.String());
        }
    }
}
