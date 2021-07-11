namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changevaluedata : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Agent_GroupHoraryDetail", "Day", c => c.Int(nullable: false));
            AlterColumn("dbo.Agent_GroupHoraryDetail", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Agent_GroupHoraryDetail", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Agent_GroupHoraryDetail", "Day", c => c.String(nullable: false));
        }
    }
}
