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
            var settings = new JelloSettings
            {
                DataSources = new List<IDataSource>
                {
                    new TestDataSource(new Dictionary<string, object>
                    {
                        { "Num", 123 },
                        { "String", "abc" },
                        { "Bool", true }
                    })
                }
            };
            Assert.AreEqual(123, new Jello(settings).Parse<Identifier>("Num").GetValue());
            Assert.AreEqual("abc", new Jello(settings).Parse<Identifier>("String").GetValue());
            Assert.IsTrue((bool)new Jello(settings).Parse<Identifier>("Bool").GetValue());
        }
    }
}