namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatusstring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent_Empresa", "string_status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Agent_Empresa", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Agent_Empresa", "Direccion", c => c.String(nullable: false));
            AlterColumn("dbo.Agent_Empresa", "Telefono", c => c.String(nullable: false));
            AlterColumn("dbo.Agent_Empresa", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Agent_Empresa", "Rut", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Agent_Empresa", "Rut", c => c.String());
            AlterColumn("dbo.Agent_Empresa", "Email", c => c.String());
            AlterColumn("dbo.Agent_Empresa", "Telefono", c => c.String());
            AlterColumn("dbo.Agent_Empresa", "Direccion", c => c.String());
            AlterColumn("dbo.Agent_Empresa", "Nombre", c => c.String());
            DropColumn("dbo.Agent_Empresa", "string_status");
        }
    }
}
