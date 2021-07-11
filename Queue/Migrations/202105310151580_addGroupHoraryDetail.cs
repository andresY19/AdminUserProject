namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGroupHoraryDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent_GroupHoraryDetail",
                c => new
                    {
                        Id_GroupHoraryDetail = c.Guid(nullable: false),
                        Day = c.String(nullable: false),
                        HourFrom = c.DateTime(nullable: false),
                        HourUntil = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Id_GroupHorary = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id_GroupHoraryDetail)
                .ForeignKey("dbo.Agent_Horary", t => t.Id_GroupHorary, cascadeDelete: true)
                .Index(t => t.Id_GroupHorary);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agent_GroupHoraryDetail", "Id_GroupHorary", "dbo.Agent_Horary");
            DropIndex("dbo.Agent_GroupHoraryDetail", new[] { "Id_GroupHorary" });
            DropTable("dbo.Agent_GroupHoraryDetail");
        }
    }
}
