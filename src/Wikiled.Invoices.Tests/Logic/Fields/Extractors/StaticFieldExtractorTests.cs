using System.Linq;
using NUnit.Framework;
using Wikiled.Invoices.Logic;
using Wikiled.Invoices.Logic.Fields.Extractors;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Logic.Fields.Extractors
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

        [TestCase("static_value", true)]
        [TestCase("value", false)]
        public void CanHandle(string name, bool expected)
        {
            staticField.Key = name;
            var result = instance.CanHandle(staticField);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Extract()
        {
            var result = instance.Extract(staticField, document).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("invoice", result[0].Key);
            Assert.AreEqual("Origin", result[0].Value);
        }

        [Test]
        public void ExtractOther()
        {
            var result = instance.Extract(otherField, document).ToArray();
            Assert.AreEqual(0, result.Length);
        }

        private StaticFieldExtractor CreateStaticFieldExtractor()
        {
            return new StaticFieldExtractor();
        }
    }
}
