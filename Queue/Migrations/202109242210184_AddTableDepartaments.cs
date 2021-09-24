namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDepartaments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent_CompanyDepartment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdCompany = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agent_Empresa", t => t.IdCompany, cascadeDelete: true)
                .Index(t => t.IdCompany);
            
            AddColumn("dbo.Agent_Employee", "DepartmentId", c => c.Guid(nullable: false));
            AddColumn("dbo.Agent_Employee", "Agent_CompanyDepartment_Id", c => c.Guid());
            CreateIndex("dbo.Agent_Employee", "Agent_CompanyDepartment_Id");
            AddForeignKey("dbo.Agent_Employee", "Agent_CompanyDepartment_Id", "dbo.Agent_CompanyDepartment", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agent_Employee", "Agent_CompanyDepartment_Id", "dbo.Agent_CompanyDepartment");
            DropForeignKey("dbo.Agent_CompanyDepartment", "IdCompany", "dbo.Agent_Empresa");
            DropIndex("dbo.Agent_CompanyDepartment", new[] { "IdCompany" });
            DropIndex("dbo.Agent_Employee", new[] { "Agent_CompanyDepartment_Id" });
            DropColumn("dbo.Agent_Employee", "Agent_CompanyDepartment_Id");
            DropColumn("dbo.Agent_Employee", "DepartmentId");
            DropTable("dbo.Agent_CompanyDepartment");
        }
    }
}
