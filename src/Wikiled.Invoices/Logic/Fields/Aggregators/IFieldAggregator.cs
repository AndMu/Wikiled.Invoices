using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields.Aggregators
{
    public interface IFieldAggregator : IFieldSupport
    {
        IEnumerable<FieldResult> Aggregate(InvoiceTemplate template,
                                           InvoiceField field,
                                           IEnumerable<FieldResult> results);
    }
}
