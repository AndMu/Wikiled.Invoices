using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
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
            Assert.Throws<ArgumentNullException>(() => new YamlLoader(null));
        }

        [Test]
        public void Load()
        {
            var result = instance.Load().ToArray();
            Assert.AreEqual(107, result.Length);
        }

        private YamlLoader CreateInstance()
        {
            return new YamlLoader(NullLogger<YamlLoader>.Instance);
        }
    }
}
