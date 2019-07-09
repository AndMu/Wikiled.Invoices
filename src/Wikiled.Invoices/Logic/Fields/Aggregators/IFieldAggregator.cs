using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Aggregators
{
    public interface IFieldAggregator
    {
        IEnumerable<FieldResult> Aggregate(InvoiceField field, IEnumerable<FieldResult> results);
    }
}
