using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Yaml
{
    public interface IYamlLoader
    {
        IEnumerable<InvoiceTemplate> Load();
    }
}