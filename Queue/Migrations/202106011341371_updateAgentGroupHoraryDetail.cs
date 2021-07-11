namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAgentGroupHoraryDetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Agent_GroupHoraryDetail", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Agent_GroupHoraryDetail", "Type", c => c.Int(nullable: false));
        }
    }
}
