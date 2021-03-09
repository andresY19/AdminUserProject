namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatusstring3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent_Empresa", "string_status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agent_Empresa", "string_status");
        }
    }
}
