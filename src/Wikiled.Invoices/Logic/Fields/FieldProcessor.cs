using System;
using System.Collections.Generic;
using Wikiled.Invoices.Logic.Fields.Aggregators;
using Wikiled.Invoices.Logic.Fields.Extractors;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public class FieldProcessor : IFieldProcessor
    {
        private readonly IFieldExtractor extractor;

        private readonly IFieldAggregator[] aggregators;

        public FieldProcessor(IFieldExtractor extractor, IFieldAggregator[] aggregators)
        {
            this.extractor = extractor ?? throw new ArgumentNullException(nameof(extractor));
            this.aggregators = aggregators ?? throw new ArgumentNullException(nameof(aggregators));
        }

        public IEnumerable<FieldResult> Construct(InvoiceTemplate template, Document document)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            foreach (InvoiceField field in template.Fields)
            {
                var result = extractor.Extract(field, document);
                foreach (var aggregator in aggregators)
                {
                    if (!aggregator.CanHandle(field))
                    {
                        continue;
                    }

                    IEnumerable<FieldResult> aggregated = aggregator.Aggregate(template, field, result);
                    foreach (FieldResult item in aggregated)
                    {
                        yield return item;
                    }

                    break;
                }
                
            }
        }
    }
}
