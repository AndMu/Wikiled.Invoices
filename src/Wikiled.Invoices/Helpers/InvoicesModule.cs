using Autofac;
using Wikiled.Common.Utilities.Modules;
using Wikiled.Invoices.Logic;
using Wikiled.Invoices.Logic.Extractor;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Logic.Fields.Aggregators;
using Wikiled.Invoices.Logic.Fields.Extractors;
using Wikiled.Invoices.Logic.Preparation;
using Wikiled.Invoices.Yaml;

namespace Wikiled.Invoices.Helpers
{
    public class InvoicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<LoggingModule>();
            
            builder.RegisterType<InformationExtractor>();
            builder.RegisterType<YamlLoader>().As<IYamlLoader>();
            builder.RegisterType<TemplateExtractorFactory>().As<ITemplateExtractorFactory>();
            builder.RegisterType<PreparationStepsFactory>().As<IPreparationStepsFactory>();
            builder.RegisterType<FieldProcessor>().As<IFieldProcessor>();

            builder.RegisterType<AmountFieldAggregator>().As<IFieldAggregator>();
            builder.RegisterType<DateFieldAggregator>().As<IFieldAggregator>();
            builder.Register(context => new CombinedFieldExtractor(new StaticFieldExtractor(), new FieldExtractor())).As<IFieldExtractor>();

            base.Load(builder);
        }
    }
}
