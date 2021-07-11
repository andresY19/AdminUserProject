namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editAgent_ProgramClasification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Agent_ProgramClasification", "title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Agent_ProgramClasification", "title", c => c.String(nullable: false));
        }
    }
}
