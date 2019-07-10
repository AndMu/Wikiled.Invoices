using Autofac;
using Wikiled.Invoices.Helpers;

namespace Wikiled.Invoices.Logic
{
    public class TemplateExtractorBuilder
    {
        public InformationExtractor Construct()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<InvoicesModule>();
            var container = builder.Build();
            return container.Resolve<InformationExtractor>();
        }
    }
}
