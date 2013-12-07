using Jello.Nodes;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class PrimaryExpressionNodeTests
    {
        [Test]
        public void ShouldResolveSingleTerminalAsExpression()
        {
            var expr = new Jello().Parse<PrimaryExpression>("true");
            Assert.IsNotNull(expr.Inner);
            Assert.IsTrue((bool)expr.Inner.GetValue());
        }

        [Test]
        public void ShouldResolveSingleTerminalInParensAsExpression()
        {
            var expr = new Jello().Parse<PrimaryExpression>("(true)");
            Assert.IsNotNull(expr.Inner);
            Assert.IsTrue((bool)expr.Inner.GetValue());
        }

        [Test]
        public void ShouldShowUsefulErrorInformation()
        {
            var expr = new Jello().Parse<PrimaryExpression>("()");
            var error = expr.Errors[0];
            Assert.AreEqual(1, error.LineNo);
            Assert.AreEqual(2, error.Col);
            Assert.AreEqual("Expected '(' or expression", error.Message);
        }
    }
}