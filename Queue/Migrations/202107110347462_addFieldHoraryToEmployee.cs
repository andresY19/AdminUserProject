namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldHoraryToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent_Employee", "Id_GroupHorary", c => c.Guid());
            CreateIndex("dbo.Agent_Employee", "Id_GroupHorary");
            AddForeignKey("dbo.Agent_Employee", "Id_GroupHorary", "dbo.Agent_Horary", "Id_GroupHorary");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agent_Employee", "Id_GroupHorary", "dbo.Agent_Horary");
            DropIndex("dbo.Agent_Employee", new[] { "Id_GroupHorary" });
            DropColumn("dbo.Agent_Employee", "Id_GroupHorary");
        }
    }
}
