namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finconfigurationtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Agent_Configuration", "IdCompany", "dbo.Agent_Empresa");
            DropIndex("dbo.Agent_Configuration", new[] { "IdCompany" });
            AddColumn("dbo.Agent_Configuration", "UploadFrecuency", c => c.Int(nullable: false));
            AlterColumn("dbo.Agent_Configuration", "IdCompany", c => c.Guid());
            CreateIndex("dbo.Agent_Configuration", "IdCompany");
            AddForeignKey("dbo.Agent_Configuration", "IdCompany", "dbo.Agent_Empresa", "IdCompany");
            DropColumn("dbo.Agent_Configuration", "InactivityTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agent_Configuration", "InactivityTime", c => c.Int(nullable: false));
            DropForeignKey("dbo.Agent_Configuration", "IdCompany", "dbo.Agent_Empresa");
            DropIndex("dbo.Agent_Configuration", new[] { "IdCompany" });
            AlterColumn("dbo.Agent_Configuration", "IdCompany", c => c.Guid(nullable: false));
            DropColumn("dbo.Agent_Configuration", "UploadFrecuency");
            CreateIndex("dbo.Agent_Configuration", "IdCompany");
            AddForeignKey("dbo.Agent_Configuration", "IdCompany", "dbo.Agent_Empresa", "IdCompany", cascadeDelete: true);
        }
    }
}
