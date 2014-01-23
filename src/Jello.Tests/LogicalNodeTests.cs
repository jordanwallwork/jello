using Jello.Nodes;
using Jello.Tests.DataSources;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class LogicalNodeTests : TestBase
    {
        [Test]
        public void ShouldResolveSimpleAndExpression()
        {
            var logicalAnd = Parse<LogicalAndExpression>("true && false");
            Assert.IsTrue((bool)logicalAnd.LHS.GetValue(new TestDataSource()));
            Assert.IsFalse((bool)logicalAnd.RHS.GetValue(new TestDataSource()));
            Assert.IsFalse((bool)logicalAnd.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, logicalAnd.Type(new TestDataSource()));
        }

        [Test]
        public void ShouldResolveSimpleOrExpression()
        {
            var logicalOr = Parse<LogicalOrExpression>("true || false");
            Assert.IsTrue((bool)logicalOr.LHS.GetValue(new TestDataSource()));
            Assert.IsFalse((bool)logicalOr.RHS.GetValue(new TestDataSource()));
            Assert.IsTrue((bool)logicalOr.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, logicalOr.Type(new TestDataSource()));
        }
    }
}