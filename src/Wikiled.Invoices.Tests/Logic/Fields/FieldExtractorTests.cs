using System.Linq;
using NUnit.Framework;
using Wikiled.Invoices.Logic;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Logic.Fields
{
    [TestFixture]
    public class FieldExtractorTests
    {
        private FieldExtractor instance;

        [SetUp]
        public void SetUp()
        {
            instance = CreateFieldExtractor();
        }

        [Test]
        public void Extract()
        {
            var result = instance.Extract(new InvoiceField {Key = "Price", Values = new[] {@"price:\s(\d+)", @"^price:\s(\d+)"}}, new Document("price: 20 and price: 30"))
                                 .ToArray();
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("20", result[0].Value);
            Assert.AreEqual("30", result[1].Value);
            Assert.AreEqual("20", result[2].Value);
        }

        [Test]
        public void ExtractBasic()
        {
            var result = instance.Extract(new InvoiceField { Key = "Price", Values = new[] { @"price:\s\d+" } }, new Document("price: 20 and price: 30"))
                                 .ToArray();
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("price: 20", result[0].Value);
            Assert.AreEqual("price: 30", result[1].Value);
        }

        private FieldExtractor CreateFieldExtractor()
        {
            return new FieldExtractor();
        }
    }
}
