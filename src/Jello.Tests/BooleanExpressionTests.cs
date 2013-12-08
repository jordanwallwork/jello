using System;
using Jello.Nodes;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class BooleanExpressionTests
    {
        [Test]
        public void NumericEquality()
        {
            var boolean = new Jello().Parse<BooleanExpression>("5 == 5");
            Assert.AreEqual(5, boolean.LHS.GetValue());
            Assert.AreEqual("==", boolean.Operator);
            Assert.AreEqual(5, boolean.RHS.GetValue());

            Assert.IsTrue((bool)boolean.GetValue());
        }

        [Test]
        public void NumericInequality()
        {
            var boolean = new Jello().Parse<BooleanExpression>("1 != 2");
            Assert.AreEqual(1, boolean.LHS.GetValue());
            Assert.AreEqual("!=", boolean.Operator);
            Assert.AreEqual(2, boolean.RHS.GetValue());

            Assert.IsTrue((bool)boolean.GetValue());
        }

        [Test]
        public void BooleanInequality()
        {
            var boolean = new Jello().Parse<BooleanExpression>("true != false");
            Assert.IsTrue((bool)boolean.LHS.GetValue());
            Assert.AreEqual("!=", boolean.Operator);
            Assert.IsFalse((bool)boolean.RHS.GetValue());

            Assert.IsTrue((bool)boolean.GetValue());
        }

        [Test]
        public void NumbersLessThan()
        {
            var boolean = new Jello().Parse<BooleanExpression>("10 < 12");
            Assert.AreEqual(10, boolean.LHS.GetValue());
            Assert.AreEqual("<", boolean.Operator);
            Assert.AreEqual(12, boolean.RHS.GetValue());
            Assert.IsTrue((bool)boolean.GetValue());
        }

        [Test]
        [Ignore]
        public void DatesLessThan()
        {
            var boolean = new Jello().Parse<BooleanExpression>("'01/01/2010' < '01/01/2012'");
            Assert.AreEqual(new DateTime(2010, 1, 1), boolean.LHS.GetValue());
            Assert.AreEqual("<", boolean.Operator);
            Assert.AreEqual(new DateTime(2012, 1, 1), boolean.RHS.GetValue());
            Assert.IsTrue((bool)boolean.GetValue());
        }
    }
}