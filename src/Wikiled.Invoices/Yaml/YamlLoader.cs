using System;
using System.Collections.Generic;

namespace Wikiled.Invoices.Yaml
{
    public class YamlLoader : IYamlLoader
    {
        public IEnumerable<YamlDefinition> Load()
        {
            GetType().Assembly.GetManifestResourceNames();
            throw new NotImplementedException();
        }
    }
}
