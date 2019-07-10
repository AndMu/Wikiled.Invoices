using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Logic.Preparation;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Extractor
{
    public class TemplateExtractor : ITemplateExtractor
    {
        private ILogger<TemplateExtractor> logger;

        private readonly InvoiceTemplate template;

        private readonly IPreparationStepsFactory preparation;

        private readonly IFieldProcessor processor;

        public TemplateExtractor(ILogger<TemplateExtractor> logger, InvoiceTemplate template, IPreparationStepsFactory preparation, IFieldProcessor processor)
        {
            this.template = template ?? throw new ArgumentNullException(nameof(template));
            this.preparation = preparation ?? throw new ArgumentNullException(nameof(preparation));
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            var fields = processor.Construct(template, document).ToArray();
            var lookup = fields.ToLookup(item => item.Key, item => item.Value, StringComparer.OrdinalIgnoreCase);
            foreach (var required in template.RequiredFields)
            {
                if (!lookup.Contains(required))
                {
                    logger.LogWarning("Required field {0} not found", required);
                    return ExtractionResult.None;
                }
            }

            var result = new ExtractionResult(document, template, fields);
            return result;
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
