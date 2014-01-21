using System.Runtime;
using Jello.Nodes;
using Jello.Tests.DataSources;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class AdditiveExpressionNodeTests : TestBase
    {
        [Test]
        public void ShouldResolveExpressionWithNoOperatorOrRHS()
        {
            var additiveExpr = Parse<AdditiveExpression>("1");
            Assert.AreEqual(1, additiveExpr.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Multiply()
        {
            var additiveExpr = Parse<MultiplicativeExpression>("2 * 4");
            Assert.AreEqual(2, additiveExpr.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("*", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.GetValue(new TestDataSource()));

            Assert.AreEqual(8, additiveExpr.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Divide()
        {
            var additiveExpr = Parse<MultiplicativeExpression>("2 / 4");
            Assert.AreEqual(2, additiveExpr.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("/", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Plus()
        {
            var additiveExpr = Parse<AdditiveExpression>("2 + 4");
            Assert.AreEqual(2, additiveExpr.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("+", additiveExpr.Operator);
            Assert.AreEqual(4, additiveExpr.RHS.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldResolveBasicAdditiveExpression_Minus()
        {
            var additiveExpr = Parse<AdditiveExpression>("6 - 1");
            Assert.AreEqual(6, additiveExpr.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("-", additiveExpr.Operator);
            Assert.AreEqual(1, additiveExpr.RHS.GetValue(new TestDataSource()));
        }

        [Test]
        public void TwoAdditiveExpressions_NoBrackets()
        {
            var additiveExpr = Parse<MultiplicativeExpression>("12 / 2 * 3");

            Assert.AreEqual(12, additiveExpr.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("/", additiveExpr.Operator);

            var me = additiveExpr.RHS as MultiplicativeExpression;
            Assert.AreEqual(2, me.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("*", me.Operator);
            Assert.AreEqual(3, me.RHS.GetValue(new TestDataSource()));

            Assert.AreEqual(6, me.GetValue(new TestDataSource()));
            Assert.AreEqual(2, additiveExpr.GetValue(new TestDataSource()));
        }

        [Test]
        public void TwoAdditiveExpressions_WithBracketsToReverseAssociativity()
        {
            var additiveExpr = Parse<MultiplicativeExpression>("(1 / 2) * 3");
            Assert.AreEqual(1.5, additiveExpr.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus()
        {
            var additiveExpr = Parse<AdditiveExpression>("2 * 2 + 4");
            Assert.AreEqual(8, additiveExpr.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldAssociateMultiplyBeforePlus_Reversed()
        {
            var additiveExpr = Parse<AdditiveExpression>("4 + 2 * 2");
            Assert.AreEqual(8, additiveExpr.GetValue(new TestDataSource()));
        }

        [Test]
        public void BracketsShouldOverrideAssociativity()
        {
            var additiveExpr = Parse<MultiplicativeExpression>("(4 + 2) * 2");
            Assert.AreEqual("*", additiveExpr.Operator);
        }
    }
}