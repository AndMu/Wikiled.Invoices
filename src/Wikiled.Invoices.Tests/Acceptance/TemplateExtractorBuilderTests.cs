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
        public void ConstructOyo()
        {
            var text = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "oyo.txt"));
            var result = instance.Extract(text);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(5, result.Fields.Length);
        }

        [Test]
        public void ConstructAmazon()
        {
            var text = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "AmazonWebServices.txt"));
            var result = instance.Extract(text);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(5, result.Fields.Length);
        }

        [Test]
        public void ConstructFiber()
        {
            var text = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "free_fiber.txt"));
            var result = instance.Extract(text);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(5, result.Fields.Length);
        }

        [Test]
        public void ConstructQualityHosting()
        {
            var text = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "QualityHosting.txt"));
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
