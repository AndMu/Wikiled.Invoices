namespace Wikiled.Invoices.Logic.Extractor
{
    public interface ITemplateExtractor
    {
        ExtractionResult Extract(Document document);
    }
}