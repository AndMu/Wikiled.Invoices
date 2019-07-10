using NUnit.Framework;
using Wikiled.Invoices.Helpers;

namespace Wikiled.Invoices.Tests.Helpers
{
    [TestFixture]
    public class PythonDateStyleConverterTests
    {
        [TestCase("%B%d,%Y", "MMMMdd,yyyy")]
        [TestCase("%B %d, %Y", "MMMM dd, yyyy")]
        [TestCase("%Y-%m-%d", "yyyy-MM-dd")]
        public void Construct(string input, string expected)
        {
            var result = PythonDateStyleConverter.Convert(input);
            Assert.AreEqual(expected, result);
        }
    }
}
