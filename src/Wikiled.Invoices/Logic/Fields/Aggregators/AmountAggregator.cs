using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Aggregators
{
    public class AmountFieldAggregator : IFieldAggregator
    {
        private readonly ILogger<AmountFieldAggregator> logger;

        public AmountFieldAggregator(ILogger<AmountFieldAggregator> logger)
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

        public IEnumerable<FieldResult> Aggregate(InvoiceTemplate template, InvoiceField field, IEnumerable<FieldResult> results)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (field?.Key == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            double value = 0;
            var allItems = results.ToArray();
            bool found = false;
            if (allItems.Length > 0)
            {
                if (field.Key.StartsWith("sum_amount", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var item in allItems)
                    {
                        if (GetValue(template, item, out var calculated))
                        {
                            value += calculated;
                            found = true;
                        }
                    }
                }
                else
                {
                    if (GetValue(template, allItems[0], out value))
                    {
                        found = true;
                    }
                }
            }

            if (found)
            {
                yield return new FieldResult(field.Key, value.ToString(CultureInfo.CurrentCulture));
            }
        }

        private bool GetValue(InvoiceTemplate template, FieldResult result, out double parsed)
        {
            var separator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var text = result.Value;
            if (!string.IsNullOrEmpty(template?.Options.DecimalSeparator) &&
                template.Options.DecimalSeparator != separator)
            {
                text = text.Replace(template.Options.DecimalSeparator, separator);
            }

            if (double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out parsed))
            {
                return true;
            }

            logger.LogWarning("Failed parsing {0}", result.Value);
            return false;
        }
    }
}
