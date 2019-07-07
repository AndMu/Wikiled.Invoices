using System;
using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public class FieldExtractor : IFieldExtractorFactory
    {
        private readonly IFieldExtractor[] extractors;

        public FieldExtractor()
        {
            extractors = new IFieldExtractor[]
                         {
                             new StaticFieldExtractor()
                         };
        }

        public IEnumerable<FieldResult> Construct(InvoiceTemplate template, Document document)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            foreach (InvoiceField field in template.Fields)
            {
                foreach (IFieldExtractor extractor in extractors)
                {
                    var result = extractor.Extract(field, document);
                    foreach (var item in result)
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
