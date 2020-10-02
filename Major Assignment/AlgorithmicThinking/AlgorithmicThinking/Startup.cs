using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlgorithmicThinking.Startup))]
namespace AlgorithmicThinking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
