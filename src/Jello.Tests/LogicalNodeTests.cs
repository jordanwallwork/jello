using Jello.Nodes;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class LogicalNodeTests
    {
        [Test]
        public void ShouldResolveSimpleAndExpression()
        {
            var logicalAnd = new Jello().Parse<LogicalAndExpression>("true && false");
            Assert.IsTrue((bool)logicalAnd.LHS.GetValue());
            Assert.IsFalse((bool)logicalAnd.RHS.GetValue());
            Assert.IsFalse((bool)logicalAnd.GetValue());
        }

        [Test]
        public void ShouldResolveSimpleOrExpression()
        {
            var logicalOr = new Jello().Parse<LogicalOrExpression>("true || false");
            Assert.IsTrue((bool)logicalOr.LHS.GetValue());
            Assert.IsFalse((bool)logicalOr.RHS.GetValue());
            Assert.IsTrue((bool)logicalOr.GetValue());
        }
    }
}