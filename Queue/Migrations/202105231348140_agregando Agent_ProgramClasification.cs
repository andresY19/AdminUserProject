namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandoAgent_ProgramClasification : DbMigration
    {
        public override void Up()
        {
           
            
            CreateTable(
                "dbo.Agent_ProgramClasification",
                c => new
                    {
                        idprogramclasification = c.Guid(nullable: false),
                        name = c.String(nullable: false),
                        title = c.String(nullable: false),
                        clasification = c.Int(nullable: false),
                        Agent_Empresa_IdCompany = c.Guid(),
                    })
                .PrimaryKey(t => t.idprogramclasification)
                .ForeignKey("dbo.Agent_Empresa", t => t.Agent_Empresa_IdCompany)
                .Index(t => t.Agent_Empresa_IdCompany);
            
          
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agent_ProgramClasification", "Agent_Empresa_IdCompany", "dbo.Agent_Empresa");
            DropIndex("dbo.Agent_ProgramClasification", new[] { "Agent_Empresa_IdCompany" });
            DropTable("dbo.Agent_UserCompanies");
            DropTable("dbo.Agent_ProgramClasification");
            DropTable("dbo.Agent_Job");
            DropTable("dbo.Agent_Employee");
        }
    }
}
