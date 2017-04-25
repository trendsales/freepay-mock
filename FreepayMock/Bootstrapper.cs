using System.ComponentModel;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Nancy.ViewEngines;

namespace FreepayMock
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            conventions.StaticContentsConventions.Add(StaticResourceConventionBuilder.AddDirectory("/Content", GetType().Assembly, "Trendsales.Explore.Content"));
            conventions.StaticContentsConventions.Add(StaticResourceConventionBuilder.AddDirectory("/Scripts", GetType().Assembly, "Trendsales.Explore.Scripts"));

            var moduleNames = Modules.Select(x => x.ModuleType.Name.Replace("Module", string.Empty));

            foreach (var name in moduleNames)
            {
                conventions.StaticContentsConventions.Add(StaticResourceConventionBuilder.AddDirectory("/features/" + name.ToLower(), GetType().Assembly, "FreepayMock.Features." + name));
            }
        }


        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            var assembly = GetType().Assembly;
            ResourceViewLocationProvider.RootNamespaces.Add(assembly, "FreepayMock.Features");
        }
        
        protected override NancyInternalConfiguration InternalConfiguration
            => NancyInternalConfiguration.WithOverrides(ConfigurationBuilder);

        private void ConfigurationBuilder(NancyInternalConfiguration nancyInternalConfiguration)
        {
            nancyInternalConfiguration.ViewLocationProvider = typeof(ResourceViewLocationProvider);
        }
    }
}