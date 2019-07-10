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

        private readonly IFieldAggregator aggregator;

        public FieldProcessor(IFieldExtractor extractor, IFieldAggregator aggregator)
        {
            this.extractor = extractor ?? throw new ArgumentNullException(nameof(extractor));
            this.aggregator = aggregator ?? throw new ArgumentNullException(nameof(aggregator));
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
                result = aggregator.Aggregate(template, field, result);
                foreach (var item in result)
                {
                    yield return item;
                }
            }
        }
    }
}
