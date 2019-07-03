using NUnit.Framework;
using System.Linq;
using Wikiled.Invoices.Yaml;

namespace Wikiled.Invoices.Tests.Yaml
{
    [TestFixture]
    public class YamlLoaderTests
    {
        private YamlLoader instance;

        [SetUp]
        public void SetUp()
        {
            instance = CreateInstance();
        }

        [Test]
        public void Construct()
        {
            var result = instance.Load().ToArray();
            Assert.AreEqual(1, result);
        }

        private YamlLoader CreateInstance()
        {
            return new YamlLoader();
        }
    }
}
