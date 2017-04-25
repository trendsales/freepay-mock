using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace FreepayMock
{
    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "FreepayMock";
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "FreepayMock";
        }

        public bool AutoIncludeModelNamespace => true;
    }
}