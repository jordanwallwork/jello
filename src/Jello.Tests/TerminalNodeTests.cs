using System;
using Jello.Nodes;
using Jello.Tests.DataSources;
using NUnit.Framework;
using String = Jello.Nodes.String;

namespace Jello.Tests
{
    [TestFixture]
    public class TerminalNodeTests : TestBase
    {
        [Test]
        public void ShouldResolveBool()
        {
            var b = Parse<Bool>("true");
            Assert.IsTrue(b.Value);
        }

        [Test]
        public void ShouldResolveString()
        {
            var s = Parse<String>("\"this is a string\"");
            Assert.AreEqual("this is a string", s.Value);
        }

        [Test]
        public void ShouldResolveInt()
        {
            var i = Parse<Number>("1.23");
            Assert.AreEqual(1.23, i.Value);
        }

        [Test]
        public void ShouldResolveBoolTerm()
        {
            var b = Parse<Term>("true");
            Assert.IsTrue((bool)b.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldResolveStringTerm()
        {
            var b = Parse<Term>("\"string\"");
            Assert.AreEqual("string", b.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldResolveDateTerm()
        {
            var d = Parse<Term>("'1989-09-29'");
            Assert.AreEqual(new DateTime(1989, 09, 29), d.GetValue(new TestDataSource()));
        }

        [Test]
        public void ShouldShowUsefulErrorInformation()
        {
            var expr = Parse<Term>("");
            var error = expr.Errors[0];
            Assert.AreEqual(1, error.LineNo);
            Assert.AreEqual(1, error.Col);
            Assert.AreEqual("Expected bool, string, number or date", error.Message);
        }
    }
}