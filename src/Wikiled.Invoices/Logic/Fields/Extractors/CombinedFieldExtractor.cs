using System;
using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Extractors
{
    public class CombinedFieldExtractor : IFieldExtractor
    {
        public bool CanHandle(InvoiceField field)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldResult> Extract(InvoiceField field, Document document)
        {
            throw new NotImplementedException();
        }
    }
}
