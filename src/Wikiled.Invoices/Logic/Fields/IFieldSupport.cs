using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public interface IFieldSupport
    {
        bool CanHandle(InvoiceField field);
    }
}
