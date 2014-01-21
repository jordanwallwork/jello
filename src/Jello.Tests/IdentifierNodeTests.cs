using System.Collections.Generic;
using Jello.DataSources;
using Jello.Nodes;
using Jello.Tests.DataSources;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class IdentifierNodeTests : TestBase
    {
        [Test]
        public void ShouldAcceptDataSource()
        {
            var dataSource = new TestDataSource(new Dictionary<string, object>
            {
                {"Num", 123},
                {"String", "abc"},
                {"Bool", true}
            });
            Assert.AreEqual(123, Parse<Identifier>("Num").GetValue(dataSource));
            Assert.AreEqual("abc", Parse<Identifier>("String").GetValue(dataSource));
            Assert.IsTrue((bool)Parse<Identifier>("Bool").GetValue(dataSource));
        }
    }
}