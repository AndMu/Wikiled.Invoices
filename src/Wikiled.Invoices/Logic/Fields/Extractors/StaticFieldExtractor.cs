using System;
using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Extractors
{
    public class StaticFieldExtractor : IFieldExtractor
    {
        public bool CanHandle(InvoiceField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            return field.Key.StartsWith("static_", StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<FieldResult> Extract(InvoiceField field, Document document)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (!CanHandle(field))
            {
                yield break;
            }

            var key = field.Key.Substring(7);
            foreach (var value in field.Values)
            {
                yield return new FieldResult(key, value);
            }
        }
    }
}
