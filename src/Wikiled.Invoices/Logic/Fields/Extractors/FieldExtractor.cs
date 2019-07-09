using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Extractors
{
    public class FieldExtractor : IFieldExtractor
    {
        public bool CanHandle(InvoiceField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            return true;
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

            foreach (var value in field.Values)
            {
                var match = Regex.Matches(document.Text, value);
                foreach (Match matchGroup in match)
                {
                    yield return new FieldResult(field.Key, matchGroup.Groups[matchGroup.Groups.Count - 1].Value);
                }
            }
        }
    }
}
