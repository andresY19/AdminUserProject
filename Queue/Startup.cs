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
                return new MongoClient("mongodb+srv://tracker:URRiIxri8IpX4Eq7@tracker.xdg7k.mongodb.net/MonitorTracker?retryWrites=true&w=majority");
            });
        }

    }
}
