using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Wikiled.Invoices.Logic;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Logic.Fields.Extractors;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Logic.Fields.Extractors
{
    [TestFixture]
    public class CombinedFieldExtractorTests
    {
        private CombinedFieldExtractor instance;

        private Mock<IFieldExtractor> first;

        private Mock<IFieldExtractor> second;

        private Document document;

        private InvoiceField field;

        [SetUp]
        public void SetUp()
        {
            first = new Mock<IFieldExtractor>();
            second = new Mock<IFieldExtractor>();
            document = new Document("Test");
            field = new InvoiceField();
            instance = CreateCombinedFieldExtractor();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new CombinedFieldExtractor(null));
            Assert.Throws<ArgumentException>(() => new CombinedFieldExtractor());
        }

        [Test]
        public void Arguments()
        {
            Assert.Throws<ArgumentNullException>(() => instance.CanHandle(null));
            Assert.Throws<ArgumentNullException>(() => instance.Extract(null, document).ToArray());
            Assert.Throws<ArgumentNullException>(() => instance.Extract(field, null).ToArray());
        }

        [Test]
        public void CanHandle()
        {
            var result = instance.CanHandle(field);
            Assert.IsFalse(result);

            second.Setup(item => item.CanHandle(field)).Returns(true);
            result = instance.CanHandle(field);
            Assert.IsTrue(result);
        }

        [Test]
        public void ExtractNone()
        {
            var result = instance.Extract(field, document).ToArray();
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void Extract()
        {
            first.Setup(item => item.CanHandle(field)).Returns(true);
            first.Setup(item => item.Extract(field, document)).Returns(new[] {new FieldResult("1", "2")});
            var result = instance.Extract(field, document).ToArray();
            Assert.AreEqual(1, result.Length);
        }

        private CombinedFieldExtractor CreateCombinedFieldExtractor()
        {
            return new CombinedFieldExtractor(first.Object, second.Object);
        }
    }
}
