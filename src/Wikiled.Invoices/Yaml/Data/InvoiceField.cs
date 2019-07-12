namespace Wikiled.Invoices.Yaml.Data
{
    public class InvoiceField
    {
        public string Key { get; set; }

        public string[] Values { get; set; }

        public override string ToString()
        {
            return $"Field: {Key}";
        }
    }
}
