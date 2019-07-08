using System;
using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public class FieldProcessor : IFieldProcessor
    {
        private readonly IFieldExtractor extractors;

        public FieldProcessor(IFieldExtractor extractors)
        {
            this.extractors = extractors;
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
