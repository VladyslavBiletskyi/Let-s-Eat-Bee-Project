using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Let_s_Eat_Bee_Project.Startup))]
namespace Let_s_Eat_Bee_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
