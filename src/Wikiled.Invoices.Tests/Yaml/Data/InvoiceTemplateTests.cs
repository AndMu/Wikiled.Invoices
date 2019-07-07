using NUnit.Framework;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Yaml.Data
{
    [TestFixture]
    public class InvoiceTemplateTests
    {
        private InvoiceTemplate instance;

        [SetUp]
        public void SetUp()
        {
            instance = CreateInstance();
        }

        [Test]
        public void Default()
        {
            Assert.AreEqual("GBP", instance.Options.Currency);
        }

        [Test]
        public void EmptyInvalid()
        {
            var result = instance.Validate();
            Assert.IsFalse(result);
        }

        [Test]
        public void Valid()
        {
            instance.Keywords = new[] {"Test"};
            var result = instance.Validate();
            Assert.IsTrue(result);
        }

        private InvoiceTemplate CreateInstance()
        {
            return new InvoiceTemplate();
        }
    }
}
