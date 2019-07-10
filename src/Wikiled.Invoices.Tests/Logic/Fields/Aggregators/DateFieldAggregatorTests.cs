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
    public class DateFieldAggregatorTests
    {
        private ILogger<DateFieldAggregator> mockLogger;

        private DateFieldAggregator instance;

        private InvoiceTemplate template;

        [SetUp]
        public void SetUp()
        {
            template = new InvoiceTemplate();
            mockLogger = new NullLogger<DateFieldAggregator>();
            instance = CreateInstance();
        }

       [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new DateFieldAggregator(null));
        }

        [TestCase("date", true)]
        [TestCase("amount_date", true)]
        [TestCase("xxx", false)]
        public void CanHandle(string field, bool expected)
        {
            var result = instance.CanHandle(new InvoiceField { Key = field });
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Aggregate()
        {
            var field = new InvoiceField { Key = "date" };
            template.Options = new InvoiceTemplateOptions();
            template.Options.DateFormats = new[] { "yyyy-MM-dd" };
            var result = instance.Aggregate(template, field, new[] { new FieldResult("1", "2015-10-01"), new FieldResult("1", "2015-12-01") }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(new DateTime(2015, 10, 01).ToShortDateString(), result[0].Value);
        }

        [Test]
        public void AggregateError()
        {
            var field = new InvoiceField { Key = "date" };
            var result = instance.Aggregate(template, field, new[] { new FieldResult("1", "x"), new FieldResult("1", "a") }).ToArray();
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void AggregateArguments()
        {
            Assert.Throws<ArgumentNullException>(() => instance.Aggregate(null, new InvoiceField(), new[] { new FieldResult("1", "2")}).ToArray());
            Assert.Throws<ArgumentNullException>(() => instance.Aggregate(template, null, new[] { new FieldResult("1", "2") }).ToArray());
            Assert.Throws<ArgumentNullException>(() => instance.Aggregate(template, new InvoiceField(), null).ToArray());
        }

        private DateFieldAggregator CreateInstance()
        {
            return new DateFieldAggregator(mockLogger);
        }
    }
}
