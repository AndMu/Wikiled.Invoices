using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System.Linq;
using Wikiled.Invoices.Logic.Preparation;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Tests.Logic.Preparation
{
    [TestFixture]
    public class PreparationStepsFactoryTests
    {
        private PreparationStepsFactory instance;

        private InvoiceTemplate invoice;

        [SetUp]
        public void SetUp()
        {
            invoice = new InvoiceTemplate();
            instance = CreateFactory();
        }

        [Test]
        public void ConstructLowercase()
        {
            invoice.Options.IsLowercase = true;
            var result = instance.Construct(invoice).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.IsAssignableFrom<LowerCaseStep>(result[0]);
        }

        [Test]
        public void ConstructEmpty()
        {
            var result = instance.Construct(invoice).ToArray();
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void ConstructAll()
        {
            invoice.Options.IsLowercase = true;
            invoice.Options.RemoveWhitespaces = true;
            invoice.Options.RemoveAccents = true;
            //invoice.Options.Replace = new[] {new KeyValuePair<string, string>("One", "Two")};
            var result = instance.Construct(invoice).ToArray();
            Assert.AreEqual(4, result.Length);
        }

        private PreparationStepsFactory CreateFactory()
        {
            return new PreparationStepsFactory(new NullLogger<PreparationStepsFactory>());
        }
    }
}
