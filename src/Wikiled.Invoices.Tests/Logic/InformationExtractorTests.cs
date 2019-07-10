using System;
using Moq;
using NUnit.Framework;
using Wikiled.Invoices.Logic;
using Wikiled.Invoices.Logic.Extractor;
using Wikiled.Invoices.Yaml;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Logic
{
    [TestFixture]
    public class InformationExtractorTests
    {
        private Mock<IYamlLoader> mockYamlLoader;

        private InformationExtractor instance;

        private InvoiceTemplate[] templates;

        private Mock<ITemplateExtractorFactory> factory;

        [SetUp]
        public void SetUp()
        {
            factory = new Mock<ITemplateExtractorFactory>();
            templates = new[] { new InvoiceTemplate(), new InvoiceTemplate() };
            mockYamlLoader = new Mock<IYamlLoader>();
            mockYamlLoader.Setup(item => item.Load()).Returns(templates);
            instance = CreateInstance();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new InformationExtractor(null, factory.Object));
            Assert.Throws<ArgumentNullException>(() => new InformationExtractor(mockYamlLoader.Object, null));
        }

        private InformationExtractor CreateInstance()
        {
            return new InformationExtractor(mockYamlLoader.Object, factory.Object);
        }
    }
}
