using System.Collections.Generic;
using Jello.DataSources;
using Jello.Nodes;
using Jello.Tests.DataSources;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class IdentifierNodeTests
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
            Assert.AreEqual(123, new Jello().Parse<Identifier>("Num").GetValue(dataSource));
            Assert.AreEqual("abc", new Jello().Parse<Identifier>("String").GetValue(dataSource));
            Assert.IsTrue((bool)new Jello().Parse<Identifier>("Bool").GetValue(dataSource));
        }
    }
}