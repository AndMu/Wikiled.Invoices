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
    public class AmountFieldAggregatorTests
    {
        private ILogger<AmountFieldAggregator> mockLogger;

        private AmountFieldAggregator instance;

        private InvoiceTemplate template;

        [SetUp]
        public void SetUp()
        {
            template = new InvoiceTemplate();
            mockLogger = new NullLogger<AmountFieldAggregator>();
            instance = CreateAmountFieldExtractor();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new AmountFieldAggregator(null));
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
            var result = instance.Aggregate(template, field, new[] {new FieldResult("1", "2"), new FieldResult("1", "3")}).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("2", result[0].Value);
        }

        [Test]
        public void AggregateWithSeparator()
        {
            var field = new InvoiceField { Key = "amount" };
            template.Options = new InvoiceTemplateOptions();
            template.Options.DecimalSeparator = ":";
            var result = instance.Aggregate(template, field, new[] { new FieldResult("1", "2:3")}).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("2.3", result[0].Value);
        }

        [Test]
        public void AggregateError()
        {
            var field = new InvoiceField { Key = "amount" };
            var result = instance.Aggregate(template, field, new[] { new FieldResult("1", "x"), new FieldResult("1", "a") }).ToArray();
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void AggregateSum()
        {
            var field = new InvoiceField { Key = "sum_amount" };
            var result = instance.Aggregate(template, field, new[] { new FieldResult("1", "2"), new FieldResult("1", "3") }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("5", result[0].Value);
        }

        [Test]
        public void AggregateArguments()
        {
            Assert.Throws<ArgumentNullException>(() => instance.Aggregate(null, new InvoiceField(), new[] { new FieldResult("1", "2") }).ToArray());
            Assert.Throws<ArgumentNullException>(() => instance.Aggregate(template, null, new[] { new FieldResult("1", "2") }).ToArray());
            Assert.Throws<ArgumentNullException>(() => instance.Aggregate(template, new InvoiceField(), null).ToArray());
        }


        private AmountFieldAggregator CreateAmountFieldExtractor()
        {
            return new AmountFieldAggregator(mockLogger);
        }
    }
}
