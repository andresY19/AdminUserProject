namespace Queue.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Queue.DAL;
    using Queue.Models;
    using System.Data.Entity.Migrations;

    public class QueueDBInitial : DbMigrationsConfiguration<Queue.DAL.QueueContext>
    {
        public QueueDBInitial()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Queue.DAL.QueueContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //  to avoid creating duplicate seed data.
            //  to avoid creating duplicate seed data.
            //  to avoid creating duplicate seed data.

            //Role
            //// ############################### A PARTIR DE AQUI ###################################
            //// #################################################################################### 
            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new QueueContext()));
            //string[] RolesNames = { "Admin","User", "Tablet", "Viewer" };

            //foreach (var role in RolesNames)
            //{
            //    if (!RoleManager.RoleExists(role))
            //    {
            //        context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = role } );
            //    }
            //}

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new QueueContext()));
      
            //var user = new ApplicationUser
            //{
            //    EmailConfirmed = true,
            //    UserName = "admin@queue.com",
            //    Email = "admin@queue.com",
            //    FirstName = "Admin",
            //    LastName = "Admin",
            //    IsLoged = false
            //};
            //var _users = manager.Create(user, "Micr0s0ft1.");
            //context.SaveChanges();
            ////Assign Role to User

            //manager.AddToRole(user.Id.ToString(), "Admin");
            //context.SaveChanges();
        }

    }
}
