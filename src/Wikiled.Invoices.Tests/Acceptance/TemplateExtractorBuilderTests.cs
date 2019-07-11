using NUnit.Framework;
using System.IO;
using Wikiled.Invoices.Logic;

namespace Wikiled.Invoices.Tests.Acceptance
{
    [TestFixture]
    public class TemplateExtractorBuilderTests
    {
        private InformationExtractor instance;

        [SetUp]
        public void SetUp()
        {
            instance = CreateTemplateExtractorBuilder();
        }

        [Test]
        public void Construct()
        {
            var text = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "oyo.txt"));
            var result = instance.Extract(text);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(5, result.Fields.Length);
        }

        private InformationExtractor CreateTemplateExtractorBuilder()
        {
            return new TemplateExtractorBuilder().Construct();
        }
    }
}
