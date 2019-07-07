using System;
using Wikiled.Invoices.Logic.Preparation;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic
{
    public class TemplateExtractor
    {
        private readonly InvoiceTemplate template;

        private readonly IPreparationStepsFactory preparation;

        public TemplateExtractor(InvoiceTemplate template, IPreparationStepsFactory preparation)
        {
            this.template = template ?? throw new ArgumentNullException(nameof(template));
            this.preparation = preparation ?? throw new ArgumentNullException(nameof(preparation));
            if (!template.Validate())
            {
                throw new ArgumentOutOfRangeException(nameof(template));
            }
        }

        public ExtractionResult Extract(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var text = ProcessPreparation(document.Text);

            if (!CanProcess(text))
            {
                return ExtractionResult.None;
            }

            //var result = new ExtractionResult(template, null);
            throw new NotImplementedException();
        }

        private bool CanProcess(string text)
        {
            foreach (var keyword in template.Keywords)
            {
                if (!text.Contains(keyword))
                {
                    return false;
                }
            }

            return true;
        }

        private string ProcessPreparation(string text)
        {
            foreach (var preparationStep in preparation.Construct(template))
            {
                text = preparationStep.Prepare(text);
            }

            return text;
        }
    }
}
