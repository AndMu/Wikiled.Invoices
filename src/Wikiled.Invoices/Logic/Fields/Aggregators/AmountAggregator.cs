using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Aggregators
{
    public class AmountFieldExtractor : IFieldAggregator
    {
        private ILogger<AmountFieldExtractor> logger;

        public AmountFieldExtractor(ILogger<AmountFieldExtractor> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool CanHandle(InvoiceField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            return field.Key.StartsWith("sum_amount", StringComparison.OrdinalIgnoreCase) || field.Key.StartsWith("amount", StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<FieldResult> Aggregate(InvoiceField field, IEnumerable<FieldResult> results)
        {
            double value = 0;
            var allItems = results.ToArray();
            if (allItems.Length > 0)
            {
                if (field.Key.StartsWith("sum_amount", StringComparison.OrdinalIgnoreCase))
                {
                    value += allItems.Sum(GetValue);
                }
                else
                {
                    value = GetValue(allItems[0]);
                }
            }

            yield return new FieldResult(field.Key, value.ToString(CultureInfo.CurrentCulture));
        }

        private double GetValue(FieldResult result)
        {
            if (double.TryParse(result.Value, NumberStyles.Any, CultureInfo.CurrentCulture, out var parsed))
            {
                return parsed;
            }

            logger.LogWarning("Failed parsing {0}", result.Value);
            return 0;

        }
    }
}
