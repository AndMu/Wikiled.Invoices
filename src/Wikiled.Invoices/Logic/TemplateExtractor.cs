using System;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic
{
    public class TemplateExtractor
    {
        private InvoiceTemplate template;

        public TemplateExtractor(InvoiceTemplate template)
        {
            this.template = template ?? throw new ArgumentNullException(nameof(template));
        }

        public ExtractionResult Extract(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            throw new NotImplementedException();
        }
    }
}
