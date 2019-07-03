using System.Collections.Generic;

namespace Wikiled.Invoices.Yaml
{
    public interface IYamlLoader
    {
        IEnumerable<YamlDefinition> Load();
    }
}