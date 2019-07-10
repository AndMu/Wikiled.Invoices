using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Extractor
{
    public interface ITemplateExtractorFactory
    {
        ITemplateExtractor Create(InvoiceTemplate template);
    }
}