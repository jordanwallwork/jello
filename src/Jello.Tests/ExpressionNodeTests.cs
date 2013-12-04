using System.Runtime;
using Jello.Nodes;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class ExpressionNodeTests
    {
        [Test]
        public void ShouldResolveSingleTerminalAsExpression()
        {
            var expr = new Jello().Parse<Expression>("true");
            Assert.IsNotNull(expr.Term);
            Assert.IsTrue(expr.Term.Bool.Value);
        }

        [Test]
        public void ShouldResolveSingleTerminalInParensAsExpression()
        {
            var expr = new Jello().Parse<Expression>("(true)");
            Assert.IsNotNull(expr.Expr);
            Assert.IsTrue(expr.Expr.Term.Bool.Value);
        }
    }
}