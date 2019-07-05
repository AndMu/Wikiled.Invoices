using Newtonsoft.Json;

namespace Wikiled.Invoices.Yaml.Data
{
    public class InvoiceTemplateLines
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("first_line")]
        public string FirstLine { get; set; }

        [JsonProperty("line")]
        public string Line { get; set; }

        [JsonProperty("last_line")]
        public string LastLine { get; set; }
    }
}
