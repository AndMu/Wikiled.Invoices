using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wikiled.Invoices.Yaml.Data
{
    public class InvoiceTemplate
    {
        public static InvoiceTemplate None { get; } = new InvoiceTemplate();

        public string Issuer { get; set; }

        public string[] Keywords { get; set; }

        [JsonConverter(typeof(InvoiceFieldsConverter))]
        public InvoiceField[] Fields { get; set; }

        public JArray Tables { get; set; }

        public InvoiceTemplateOptions Options { get; set; } = new InvoiceTemplateOptions();

        public InvoiceTemplateLines Lines { get; set; }

        public override string ToString()
        {
            return $"Invoice <{Issuer}>";
        }

        public bool Validate()
        {
            return Keywords != null && Keywords.Length > 0;
        }
    }
}
