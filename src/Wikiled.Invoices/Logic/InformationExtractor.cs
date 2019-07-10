using System;
using System.Linq;
using Wikiled.Invoices.Logic.Extractor;
using Wikiled.Invoices.Yaml;

namespace Wikiled.Invoices.Logic
{
    public class InformationExtractor
    {
        private readonly ITemplateExtractor[] templates;

        public InformationExtractor(IYamlLoader templatesSource, ITemplateExtractorFactory factory)
        {
            if (templatesSource == null)
            {
                throw new ArgumentNullException(nameof(templatesSource));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            templates = templatesSource.Load().Select(factory.Create).ToArray();
        }

        public ExtractionResult Extract(string invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice));
            }

            foreach (var template in templates)
            {
                var result = template.Extract(new Document(invoice));
                if (result.IsSuccessful)
                {
                    return result;
                }
            }

            return ExtractionResult.None;
        }
    }
}
