using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public interface IFieldExtractor
    {
        IEnumerable<FieldResult> Extract(InvoiceField field, Document document);
    }
}