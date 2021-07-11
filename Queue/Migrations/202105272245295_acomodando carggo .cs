namespace Queue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acomodandocarggo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent_Job", "Lunes", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agent_Job", "Masrtes", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agent_Job", "Miercoles", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agent_Job", "Jueves", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agent_Job", "Viernes", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agent_Job", "Sabado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agent_Job", "Domingo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agent_Job", "HorarioIniciaLunes", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioTerminaLunes", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioIniciaMartes", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioTerminaMartes", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioIniciaMiercoles", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioTerminaMiercoles", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioIniciaJueves", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioTerminaJueves", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioIniciaViernes", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioTerminaViernes", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioIniciaSabado", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioTerminaSabado", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioIniciaDomingo", c => c.DateTime());
            AddColumn("dbo.Agent_Job", "HorarioTerminaDomingo", c => c.DateTime());
            DropColumn("dbo.Agent_Job", "HorarioInicia");
            DropColumn("dbo.Agent_Job", "HorarioTermina");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agent_Job", "HorarioTermina", c => c.String(nullable: false));
            AddColumn("dbo.Agent_Job", "HorarioInicia", c => c.String(nullable: false));
            DropColumn("dbo.Agent_Job", "HorarioTerminaDomingo");
            DropColumn("dbo.Agent_Job", "HorarioIniciaDomingo");
            DropColumn("dbo.Agent_Job", "HorarioTerminaSabado");
            DropColumn("dbo.Agent_Job", "HorarioIniciaSabado");
            DropColumn("dbo.Agent_Job", "HorarioTerminaViernes");
            DropColumn("dbo.Agent_Job", "HorarioIniciaViernes");
            DropColumn("dbo.Agent_Job", "HorarioTerminaJueves");
            DropColumn("dbo.Agent_Job", "HorarioIniciaJueves");
            DropColumn("dbo.Agent_Job", "HorarioTerminaMiercoles");
            DropColumn("dbo.Agent_Job", "HorarioIniciaMiercoles");
            DropColumn("dbo.Agent_Job", "HorarioTerminaMartes");
            DropColumn("dbo.Agent_Job", "HorarioIniciaMartes");
            DropColumn("dbo.Agent_Job", "HorarioTerminaLunes");
            DropColumn("dbo.Agent_Job", "HorarioIniciaLunes");
            DropColumn("dbo.Agent_Job", "Domingo");
            DropColumn("dbo.Agent_Job", "Sabado");
            DropColumn("dbo.Agent_Job", "Viernes");
            DropColumn("dbo.Agent_Job", "Jueves");
            DropColumn("dbo.Agent_Job", "Miercoles");
            DropColumn("dbo.Agent_Job", "Masrtes");
            DropColumn("dbo.Agent_Job", "Lunes");
        }
    }
}
