using System;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Extractors
{
    public class CombinedFieldExtractor : IFieldExtractor
    {
        private readonly IFieldExtractor[] inner;

        public CombinedFieldExtractor(params IFieldExtractor[] inner)
        {
            this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
            if (inner.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(inner));
            }
        }

        public bool CanHandle(InvoiceField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            return inner.Any(item => item.CanHandle(field));
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

            var handler = inner.FirstOrDefault(item => item.CanHandle(field));
            if (handler == null)
            {
                yield break;
            }

            var result = handler.Extract(field, document);
            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}
