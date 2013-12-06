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
            Assert.IsNotNull(additiveExpr.LHS);
            Assert.IsNull(additiveExpr.RHS);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Multiply()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("2 * 4");
            Assert.AreEqual(2, additiveExpr.LHS.Term.Number.Value);
            Assert.AreEqual("*", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.LHS.Term.Number.Value);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Divide()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("2 / 4");
            Assert.AreEqual(2, additiveExpr.LHS.Term.Number.Value);
            Assert.AreEqual("/", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.LHS.Term.Number.Value);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Plus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("2 + 4");
            Assert.AreEqual(2, additiveExpr.LHS.LHS.Term.Number.Value);
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.LHS.LHS.Term.Number.Value);
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Minus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("6 - 1");
            Assert.AreEqual(6, additiveExpr.LHS.LHS.Term.Number.Value);
            Assert.AreEqual("-", additiveExpr.Operator);
            Assert.AreEqual(1, additiveExpr.RHS.LHS.LHS.Term.Number.Value);
        }

        [Test]
        public void TwoAdditiveExpressions_NoBrackets()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("1 / 2 * 3");

            Assert.AreEqual(1, additiveExpr.LHS.Term.Number.Value);
            Assert.AreEqual("/", additiveExpr.Operator);

            Assert.AreEqual(2, additiveExpr.RHS.LHS.Term.Number.Value);
            Assert.AreEqual("*", additiveExpr.RHS.Operator);
            Assert.AreEqual(3, additiveExpr.RHS.RHS.LHS.Term.Number.Value);
        }

        [Test]
        public void TwoAdditiveExpressions_WithBracketsToReverseAssociativity()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("(1 / 2) * 3");

            var bracketedExpr = additiveExpr.LHS.Expr.LHS.LHS.LHS.LHS.LHS;
            Assert.AreEqual(1, bracketedExpr.LHS.LHS.Term.Number.Value);
            Assert.AreEqual("/", bracketedExpr.LHS.Operator);
            Assert.AreEqual(2, bracketedExpr.LHS.RHS.LHS.Term.Number.Value);

            Assert.AreEqual("*", additiveExpr.Operator);
            Assert.AreEqual(3, additiveExpr.RHS.LHS.Term.Number.Value);
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("2 * 2 + 4");
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.AreEqual("*", additiveExpr.LHS.Operator);
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus_Reversed()
        {
            var additiveExpr = new Jello().Parse<AdditiveExpression>("4 + 2 * 2");
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.AreEqual("*", additiveExpr.RHS.LHS.Operator);
        }

        [Test]
        public void BracketsShouldOverrideAssociativity()
        {
            var additiveExpr = new Jello().Parse<MultiplicativeExpression>("(4 + 2) * 2");
            Assert.AreEqual("*", additiveExpr.Operator);
        }
    }
}