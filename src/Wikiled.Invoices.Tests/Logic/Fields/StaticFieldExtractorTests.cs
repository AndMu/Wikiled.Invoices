using NUnit.Framework;
using System.Linq;
using Wikiled.Invoices.Logic;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Logic.Fields
{
    [TestFixture]
    public class StaticFieldExtractorTests
    {
        private StaticFieldExtractor instance;

        private Document document;

        private InvoiceField staticField;

        private InvoiceField otherField;

        [SetUp]
        public void SetUp()
        {
            document = new Document("Test document");
            staticField = new InvoiceField();
            staticField.Key = "static_invoice";
            staticField.Values = new[] {"Origin"};
            otherField = new InvoiceField();
            otherField.Key = "invoice";
            otherField.Values = new[] { "Origin" };
            instance = CreateStaticFieldExtractor();
        }

        [Test]
        public void Extract()
        {
            var result = instance.Extract(staticField, document).ToArray();
            Assert.AreEqual(1, result);
            Assert.AreEqual("invoice", result[0].Key);
            Assert.AreEqual("Origin", result[0].Value);
        }

        [Test]
        public void ExtractOther()
        {
            var result = instance.Extract(otherField, document).ToArray();
            Assert.AreEqual(0, result);
        }

        private StaticFieldExtractor CreateStaticFieldExtractor()
        {
            return new StaticFieldExtractor();
        }
    }
}
