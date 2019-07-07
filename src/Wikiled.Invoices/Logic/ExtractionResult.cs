using System;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic
{
    public class ExtractionResult
    {
        public ExtractionResult(Document document, InvoiceTemplate template, FieldResult[] fields)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
            Document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public static ExtractionResult None { get; } = new ExtractionResult(new Document("EMPTY"), InvoiceTemplate.None, new FieldResult[] { });

        public string Issuer => Template.Issuer;

        public string Currency => Template.Options.Currency;

        public Document Document { get; }

        public InvoiceTemplate Template { get; }

        public FieldResult[] Fields { get; }
    }
}
