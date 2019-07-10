using Microsoft.Extensions.Logging;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Logic.Preparation;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Extractor
{
    public class TemplateExtractorFactory : ITemplateExtractorFactory
    {
        private readonly IPreparationStepsFactory preparation;

        private readonly IFieldProcessor processor;

        private readonly ILoggerFactory factory;

        public TemplateExtractorFactory(ILoggerFactory factory, IPreparationStepsFactory preparation, IFieldProcessor processor)
        {
            this.preparation = preparation;
            this.processor = processor;
            this.factory = factory;
        }

        public ITemplateExtractor Create(InvoiceTemplate template)
        {
            return new TemplateExtractor(factory.CreateLogger<TemplateExtractor>(), template, preparation, processor);
        }
    }
}
