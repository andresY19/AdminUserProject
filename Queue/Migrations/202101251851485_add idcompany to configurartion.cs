namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidcompanytoconfigurartion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Agent_Configuration", "Agent_Empresa_IdCompany", "dbo.Agent_Empresa");
            DropIndex("dbo.Agent_Configuration", new[] { "Agent_Empresa_IdCompany" });
            RenameColumn(table: "dbo.Agent_Configuration", name: "Agent_Empresa_IdCompany", newName: "IdCompany");
            AlterColumn("dbo.Agent_Configuration", "IdCompany", c => c.Guid(nullable: false));
            AlterColumn("dbo.Agent_Empresa", "Key", c => c.String(nullable: false));
            CreateIndex("dbo.Agent_Configuration", "IdCompany");
            AddForeignKey("dbo.Agent_Configuration", "IdCompany", "dbo.Agent_Empresa", "IdCompany", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agent_Configuration", "IdCompany", "dbo.Agent_Empresa");
            DropIndex("dbo.Agent_Configuration", new[] { "IdCompany" });
            AlterColumn("dbo.Agent_Empresa", "Key", c => c.String());
            AlterColumn("dbo.Agent_Configuration", "IdCompany", c => c.Guid());
            RenameColumn(table: "dbo.Agent_Configuration", name: "IdCompany", newName: "Agent_Empresa_IdCompany");
            CreateIndex("dbo.Agent_Configuration", "Agent_Empresa_IdCompany");
            AddForeignKey("dbo.Agent_Configuration", "Agent_Empresa_IdCompany", "dbo.Agent_Empresa", "IdCompany");
        }
    }
}
