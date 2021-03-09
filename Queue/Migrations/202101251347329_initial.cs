namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent_Configuration",
                c => new
                    {
                        Id_Configuration = c.Guid(nullable: false),
                        InactivityTime = c.Int(nullable: false),
                        InactivityPeriod = c.Int(nullable: false),
                        CaptureFrecuency = c.Int(nullable: false),
                        Agent_Empresa_IdCompany = c.Guid(),
                    })
                .PrimaryKey(t => t.Id_Configuration)
                .ForeignKey("dbo.Agent_Empresa", t => t.Agent_Empresa_IdCompany)
                .Index(t => t.Agent_Empresa_IdCompany);
            
            CreateTable(
                "dbo.Agent_Empresa",
                c => new
                    {
                        IdCompany = c.Guid(nullable: false),
                        Nombre = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Rut = c.String(),
                        Key = c.String(),
                        Id_EmpresaBPM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdCompany);
            
            CreateTable(
                "dbo.Agent_GenericError",
                c => new
                    {
                        codigo_id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.codigo_id);
           
        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.Agent_Configuration", "Agent_Empresa_IdCompany", "dbo.Agent_Empresa");
            
            DropIndex("dbo.Agent_Configuration", new[] { "Agent_Empresa_IdCompany" });
            
            DropTable("dbo.Agent_GenericError");
            DropTable("dbo.Agent_Empresa");
            DropTable("dbo.Agent_Configuration");
        }
    }
}
