using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public interface IFieldExtractorFactory
    {
        IEnumerable<FieldResult> Construct(InvoiceTemplate template, Document document);
    }
}