using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Linq;
using Wikiled.Invoices.Logic.Fields;
using Wikiled.Invoices.Logic.Fields.Aggregators;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Logic.Fields.Aggregators
{
    [TestFixture]
    public class AmountFieldExtractorTests
    {
        private ILogger<AmountFieldExtractor> mockLogger;

        private AmountFieldExtractor instance;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new NullLogger<AmountFieldExtractor>();
            instance = CreateAmountFieldExtractor();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new AmountFieldExtractor(null));
        }

        [TestCase("SUM_AMOUNT", true)]
        [TestCase("amount", true)]
        [TestCase("date", false)]
        public void CanHandle(string field, bool expected)
        {
            var result = instance.CanHandle(new InvoiceField { Key = field });
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Aggregate()
        {
            var field = new InvoiceField {Key = "amount"};
            var result = instance.Aggregate(field, new[] {new FieldResult("1", "2"), new FieldResult("1", "3")}).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("2", result[0].Value);
        }

        [Test]
        public void AggregateError()
        {
            var field = new InvoiceField { Key = "amount" };
            var result = instance.Aggregate(field, new[] { new FieldResult("1", "x"), new FieldResult("1", "a") }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("0", result[0].Value);
        }

        [Test]
        public void AggregateSum()
        {
            var field = new InvoiceField { Key = "sum_amount" };
            var result = instance.Aggregate(field, new[] { new FieldResult("1", "2"), new FieldResult("1", "3") }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("5", result[0].Value);
        }

        private AmountFieldExtractor CreateAmountFieldExtractor()
        {
            return new AmountFieldExtractor(mockLogger);
        }
    }
}
