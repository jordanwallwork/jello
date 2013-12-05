using System.Runtime;
using Jello.Nodes;
using NUnit.Framework;

namespace Jello.Tests
{
    public class AdditiveExpressionNodeTests
    {
        [Test]
        public void ShouldResolveExpressionWithNoOperatorOrRHS()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("true");
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.IsNull(additiveExpr.RHS);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Multiply()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("true * false");
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.AreEqual("*", additiveExpr.Operator);
            Assert.IsNotNull(additiveExpr.RHS);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Divide()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("true / false");
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.AreEqual("/", additiveExpr.Operator);
            Assert.IsNotNull(additiveExpr.RHS);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Plus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("true + false");
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.IsNotNull(additiveExpr.RHS);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Minus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("true - false");
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.AreEqual("-", additiveExpr.Operator);
            Assert.IsNotNull(additiveExpr.RHS);
        }

        [Test]
        public void TwoAdditiveExpressions_NoBrackets()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("true / false * true");
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.AreEqual("/", additiveExpr.Operator);
            Assert.IsNotNull(additiveExpr.RHS);

            Assert.IsNotNull(additiveExpr.RHS.LHS);
            Assert.AreEqual("*", additiveExpr.RHS.Operator);
            Assert.IsNotNull(additiveExpr.RHS.RHS);
        }

        [Test]
        public void TwoAdditiveExpressions_WithBracketsToReverseAssociativity()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("(true / false) * true");
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.AreEqual("*", additiveExpr.Operator);
            Assert.IsNotNull(additiveExpr.RHS);
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("true * true + true");
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.AreEqual("*", additiveExpr.LHS.Operator);
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus_Reversed()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("true + true * true");
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.AreEqual("*", additiveExpr.RHS.LHS.Operator);
        }

        [Test]
        public void BracketsShouldOverrideAssociativity()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("(true + true) * true");
            Assert.AreEqual("*", additiveExpr.Operator);
        }
    }
}