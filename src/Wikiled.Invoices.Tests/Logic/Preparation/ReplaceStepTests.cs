using System;
using NUnit.Framework;
using Wikiled.Invoices.Logic.Preparation;

namespace Wikiled.Invoices.Tests.Logic.Preparation
{
    [TestFixture]
    public class ReplaceStepTests
    {
        private ReplaceStep instance;

        [SetUp]
        public void SetUp()
        {
            instance = CreateReplaceStep();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new ReplaceStep(null, "Test"));
            Assert.Throws<ArgumentNullException>(() => new ReplaceStep("Test", null));
        }

        [TestCase("One One", "Two Two")]
        [TestCase("I am the One", "I am the Two")]
        public void PrepareExpectedBehavior(string source, string expected)
        {
            var result = instance.Prepare(source);
            Assert.AreEqual(expected, result);
        }

        private ReplaceStep CreateReplaceStep()
        {
            return new ReplaceStep("One", "Two");
        }
    }
}
