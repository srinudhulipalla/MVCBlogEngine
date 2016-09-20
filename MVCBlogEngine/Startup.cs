using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCBlogEngine.Startup))]
namespace MVCBlogEngine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
