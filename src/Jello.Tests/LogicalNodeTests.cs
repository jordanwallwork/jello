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
            Assert.IsTrue(logicalAnd.LHS.LHS.LHS.LHS.Term.Bool.Value);
            Assert.AreEqual("&&", logicalAnd.Operator);
            Assert.IsFalse(logicalAnd.RHS.LHS.LHS.LHS.LHS.Term.Bool.Value);
        }

        [Test]
        public void ShouldResolveSimpleOrExpression()
        {
            var logicalOr = new Jello().Parse<LogicalOrExpression>("true || false");
            Assert.IsTrue(logicalOr.LHS.LHS.LHS.LHS.LHS.Term.Bool.Value);
            Assert.AreEqual("||", logicalOr.Operator);
            Assert.IsFalse(logicalOr.RHS.LHS.LHS.LHS.LHS.LHS.Term.Bool.Value);
        }
    }
}