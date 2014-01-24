using System;
using Jello.Nodes;
using Jello.Tests.DataSources;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class BooleanExpressionTests : TestBase
    {
        [Test]
        public void NumericEquality()
        {
            var boolean = Parse<BooleanExpression>("5 == 5");
            Assert.AreEqual(5, boolean.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("==", boolean.Operator);
            Assert.AreEqual(5, boolean.RHS.GetValue(new TestDataSource()));

            Assert.IsTrue((bool)boolean.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, boolean.Type(new TestDataSource()));
        }

        [Test]
        public void NumericEquality_NodeTypeNotSpecified()
        {
            var boolean = Parse<Expression>("5 == 5");
            Assert.IsTrue((bool)boolean.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, boolean.Type(new TestDataSource()));
        }

        [Test]
        public void StringEquality()
        {
            var boolean = Parse<Expression>("\"A\" == \"A\"");
            Assert.IsTrue((bool)boolean.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, boolean.Type(new TestDataSource()));
        }

        [Test]
        public void NumericInequality()
        {
            var boolean = Parse<BooleanExpression>("1 != 2");
            Assert.AreEqual(1, boolean.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("!=", boolean.Operator);
            Assert.AreEqual(2, boolean.RHS.GetValue(new TestDataSource()));

            Assert.IsTrue((bool)boolean.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, boolean.Type(new TestDataSource()));
        }

        [Test]
        public void BooleanInequality()
        {
            var boolean = Parse<BooleanExpression>("true != false");
            Assert.IsTrue((bool)boolean.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("!=", boolean.Operator);
            Assert.IsFalse((bool)boolean.RHS.GetValue(new TestDataSource()));

            Assert.IsTrue((bool)boolean.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, boolean.Type(new TestDataSource()));
        }

        [Test]
        public void NumbersLessThan()
        {
            var boolean = Parse<BooleanExpression>("10 < 12");
            Assert.AreEqual(10, boolean.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("<", boolean.Operator);
            Assert.AreEqual(12, boolean.RHS.GetValue(new TestDataSource()));
            Assert.IsTrue((bool)boolean.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, boolean.Type(new TestDataSource()));
        }

        [Test]
        [Ignore]
        public void DatesLessThan()
        {
            var boolean = Parse<BooleanExpression>("'01/01/2010' < '01/01/2012'");
            Assert.AreEqual(new DateTime(2010, 1, 1), boolean.LHS.GetValue(new TestDataSource()));
            Assert.AreEqual("<", boolean.Operator);
            Assert.AreEqual(new DateTime(2012, 1, 1), boolean.RHS.GetValue(new TestDataSource()));
            Assert.IsTrue((bool)boolean.GetValue(new TestDataSource()));
            Assert.AreEqual(ValueType.Bool, boolean.Type(new TestDataSource()));
        }
    }
}