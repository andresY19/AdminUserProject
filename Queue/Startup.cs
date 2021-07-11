using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using MongoDB.Driver;
using Owin;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Queue.Startup))]
namespace Queue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
        
        public void ConfigurationServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                //return new MongoClient("mongodb+srv://tracker:3hQCcdxBkzHDkB2J@cluster0.4xbwb.mongodb.net/MonitorTracker?retryWrites=true&w=majority");
                return new MongoClient("mongodb://localhost:27017");
            });
            //var database = client.GetDatabase("test");
        }

    }
}
