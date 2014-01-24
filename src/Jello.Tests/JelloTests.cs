using Jello.Tests.DataSources;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class JelloTests : TestBase
    {
        [Test]
        public void ShouldGetBoolResult()
        {
            var jello = new Jello().Parse("(1 == 2) && (3 == 4)");
            var resp = jello.ExecuteBool(new TestDataSource());
            Assert.IsNotNull(resp);
            Assert.IsFalse(resp == true);
        }
    }
}