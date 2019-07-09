using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Extractors
{
    public interface IFieldExtractor
    {
        bool CanHandle(InvoiceField field);

        IEnumerable<FieldResult> Extract(InvoiceField field, Document document);
    }
}