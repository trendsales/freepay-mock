using Nancy.Owin;
using Owin;

namespace FreepayMock
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new NancyOptions();
            options.Bootstrapper = new Bootstrapper();
            app.UseNancy();
        }
    }
}