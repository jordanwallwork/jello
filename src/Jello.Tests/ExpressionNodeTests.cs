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

        [Test]
        public void ShouldShowUsefulErrorInformation()
        {
            var expr = new Jello().Parse<Expression>("()");
            var error = expr.Errors[0];
            Assert.AreEqual(1, error.LineNo);
            Assert.AreEqual(2, error.Col);
            Assert.AreEqual("Expected '(' or expression", error.Message);
        }
    }
}