using Queue.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Queue.DAL
{
    using global::Queue.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;


    public class QueueContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        public QueueContext() : base("name=QueueContext")
        {
            Database.SetInitializer<QueueContext>(null);
        }


        public static QueueContext Create()
        {
            return new QueueContext();
        }

        public DbSet<RoleViewModel> RoleViewModels { get; set; }        
        public DbSet<Agent_Empresa> Agent_Empresa { get; set; }
        public DbSet<Agent_GenericError> Agent_GenericError { get; set; }
        public DbSet<Agent_Configuration> Agent_Configuration { get; set; }        
    }
}