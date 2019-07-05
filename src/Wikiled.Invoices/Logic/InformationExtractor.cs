using System;
using System.Linq;
using Wikiled.Invoices.Yaml;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic
{
    public class InformationExtractor
    {
        private InvoiceTemplate[] templates;

        public InformationExtractor(IYamlLoader templatesSource)
        {
            templates = templatesSource.Load().ToArray();
        }

        public ExtractionResult Extract(string invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice));
            }

            throw new NotImplementedException();
        }
    }
}
