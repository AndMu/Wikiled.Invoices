using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wikiled.Invoices.Yaml.Data
{
    public class InvoiceTemplateOptions
    {
        [JsonProperty("remove_whitespace")]
        public bool RemoveWhitespaces { get; set; }

        [JsonProperty("remove_accents")]
        public bool RemoveAccents { get; set; }

        [JsonProperty("lowercase")]
        public bool Lowercase { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "GBP";

        [JsonProperty("date_formats")]
        public string[] DateFormats { get; set; }

        [JsonProperty("languages")]
        public string[] Languages { get; set; }

        [JsonProperty("replace")]
        public KeyValuePair<string, string>[] Replace { get; set; }

        [JsonProperty("decimal_separator")]
        public string DecimalSeparator { get; set; } = ".";
    }
}
