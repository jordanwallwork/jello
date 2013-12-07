using System.Runtime;
using Jello.Nodes;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class AdditiveExpressionNodeTests
    {
        [Test]
        public void ShouldResolveExpressionWithNoOperatorOrRHS()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("1");
            Assert.AreEqual(1, additiveExpr.GetValue());
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Multiply()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("2 * 4");
            Assert.AreEqual(2, additiveExpr.LHS.GetValue());
            Assert.AreEqual("*", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.GetValue());

            Assert.AreEqual(8, additiveExpr.GetValue());
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Divide()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("2 / 4");
            Assert.AreEqual(2, additiveExpr.LHS.GetValue());
            Assert.AreEqual("/", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.GetValue());
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Plus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("2 + 4");
            Assert.AreEqual(2, additiveExpr.LHS.GetValue());
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.GetValue());
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Minus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("6 - 1");
            Assert.AreEqual(6, additiveExpr.LHS.GetValue());
            Assert.AreEqual("-", additiveExpr.Operator);
            Assert.AreEqual(1, additiveExpr.RHS.GetValue());
        }

        [Test]
        public void TwoAdditiveExpressions_NoBrackets()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("12 / 2 * 3");

            Assert.AreEqual(12, additiveExpr.LHS.GetValue());
            Assert.AreEqual("/", additiveExpr.Operator);

            var me = additiveExpr.RHS as MultiplicativeExpression;
            Assert.AreEqual(2, me.LHS.GetValue());
            Assert.AreEqual("*", me.Operator);
            Assert.AreEqual(3, me.RHS.GetValue());

            Assert.AreEqual(6, me.GetValue());
            Assert.AreEqual(2, additiveExpr.GetValue());
        }

        [Test]
        public void TwoAdditiveExpressions_WithBracketsToReverseAssociativity()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("(1 / 2) * 3");
            Assert.AreEqual(1.5, additiveExpr.GetValue());
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("2 * 2 + 4");
            Assert.AreEqual(8, additiveExpr.GetValue());
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus_Reversed()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("4 + 2 * 2");
            Assert.AreEqual(8, additiveExpr.GetValue());
        }

        [Test]
        public void BracketsShouldOverrideAssociativity()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("(4 + 2) * 2");
            Assert.AreEqual("*", additiveExpr.Operator);
        }
    }
}