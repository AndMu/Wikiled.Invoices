using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Aggregators
{
    public class DateFieldExtractor : IFieldAggregator
    {
        private readonly ILogger<DateFieldExtractor> logger;

        public DateFieldExtractor(ILogger<DateFieldExtractor> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool CanHandle(InvoiceField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            return field.Key.StartsWith("date", StringComparison.OrdinalIgnoreCase) || field.Key.EndsWith("date", StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<FieldResult> Aggregate(InvoiceTemplate template, InvoiceField field, IEnumerable<FieldResult> results)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            var allItems = results.ToArray();
            if (allItems.Length > 0)
            {
                foreach (var item in allItems)
                {
                    if (!GetValue(template, item, out var date))
                    {
                        continue;
                    }

                    yield return new FieldResult(field.Key, date.ToString(CultureInfo.CurrentCulture));
                    break;
                }
            }
        }

        private bool GetValue(InvoiceTemplate template, FieldResult result, out DateTime parsed)
        {
            parsed = DateTime.MinValue;
            if (template.Options.DateFormats == null)
            {
                if (!DateTime.TryParse(result.Value, out parsed))
                {
                    return true;
                }
            }
            else
            {
                foreach (var format in template.Options.DateFormats)
                {
                    if (!DateTime.TryParseExact(result.Value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
                    {
                        return true;
                    }
                }
            }

            logger.LogWarning("Failed parsing {0}", result.Value);
            return false;
        }
    }
}
