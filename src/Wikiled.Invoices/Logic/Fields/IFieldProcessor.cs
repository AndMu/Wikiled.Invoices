using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public interface IFieldProcessor
    {
        IEnumerable<FieldResult> Construct(InvoiceTemplate template, Document document);
    }
}