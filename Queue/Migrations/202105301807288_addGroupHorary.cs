namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGroupHorary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent_Horary",
                c => new
                    {
                        Id_GroupHorary = c.Guid(nullable: false),
                        NameGroup = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_GroupHorary);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Agent_Horary");
        }
    }
}
