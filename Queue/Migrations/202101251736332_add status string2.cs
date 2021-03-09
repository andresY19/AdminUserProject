namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatusstring2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Agent_Empresa", "string_status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agent_Empresa", "string_status", c => c.Boolean(nullable: false));
        }
    }
}
