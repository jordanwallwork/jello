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

            var num = Parse<Identifier>("Num");
            Assert.AreEqual(123, num.GetValue(dataSource));
            Assert.AreEqual(ValueType.Number, num.Type(dataSource));

            var str = Parse<Identifier>("String");
            Assert.AreEqual("abc", str.GetValue(dataSource));
            Assert.AreEqual(ValueType.String, str.Type(dataSource));

            var boole = Parse<Identifier>("Bool");
            Assert.IsTrue((bool)boole.GetValue(dataSource));
            Assert.AreEqual(ValueType.Bool, boole.Type(dataSource));
        }

        [Test]
        public void StringEquality()
        {
            var dataSource = new TestDataSource(new Dictionary<string, object>
            {
                {"String", "abc"}
            });
            var strEq = Parse<Expression>("String == \"abc\"");
            Assert.IsTrue((bool)strEq.GetValue(dataSource));
        }
    }
}