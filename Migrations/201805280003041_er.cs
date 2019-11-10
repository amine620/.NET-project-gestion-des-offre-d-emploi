namespace wafabank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class er : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Applies", newName: "Demandes");
            RenameTable(name: "dbo.jobs", newName: "offres");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.offres", newName: "jobs");
            RenameTable(name: "dbo.Demandes", newName: "Applies");
        }
    }
}
