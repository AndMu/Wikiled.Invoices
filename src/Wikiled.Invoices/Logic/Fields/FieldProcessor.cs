using System;
using System.Collections.Generic;
using System.Linq;
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

            if (!string.IsNullOrEmpty(template.Issuer))
            {
                yield return new FieldResult("issuer", template.Issuer);
            }
            else if (template.Keywords != null &&
                     template.Keywords.Length > 0)
            {
                yield return new FieldResult("issuer", template.Keywords[0]);
            }

            foreach (InvoiceField field in template.Fields)
            {
                foreach (var result in ProcessField(template, document, field))
                {
                    yield return result;
                }
            }
        }

        private IEnumerable<FieldResult> ProcessField(InvoiceTemplate template, Document document, InvoiceField field)
        {
            var result = extractor.Extract(field, document).ToArray();
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

                yield break;
            }

            foreach (var fieldResult in result)
            {
                yield return fieldResult;
            }
        }
    }
}
