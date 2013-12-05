using System;
using Jello.Nodes;
using NUnit.Framework;
using String = Jello.Nodes.String;

namespace Jello.Tests
{
    [TestFixture]
    public class TerminalNodeTests
    {
        [Test]
        public void ShouldResolveBool()
        {
            var b = new Jello().Parse<Bool>("true");
            Assert.IsTrue(b.Value);
        }

        [Test]
        public void ShouldResolveString()
        {
            var s = new Jello().Parse<String>("\"this is a string\"");
            Assert.AreEqual("this is a string", s.Value);
        }

        [Test]
        public void ShouldResolveInt()
        {
            var i = new Jello().Parse<Number>("1.23");
            Assert.AreEqual(1.23, i.Value);
        }

        [Test]
        public void ShouldResolveBoolTerm()
        {
            var b = new Jello().Parse<Term>("true");
            Assert.IsNotNull(b.Bool);
            Assert.IsTrue(b.Bool.Value);
            Assert.IsNull(b.String);
        }

        [Test]
        public void ShouldResolveStringTerm()
        {
            var b = new Jello().Parse<Term>("\"string\"");
            Assert.IsNotNull(b.String);
            Assert.AreEqual("string", b.String.Value);
            Assert.IsNull(b.Bool);
        }

        [Test]
        public void ShouldResolveDateTerm()
        {
            var d = new Jello().Parse<Term>("'1989-09-29'");
            Assert.IsNotNull(d.Date);
            Assert.AreEqual(new DateTime(1989, 09, 29), d.Date.Value);
        }

        [Test]
        public void ShouldShowUsefulErrorInformation()
        {
            var expr = new Jello().Parse<Term>("");
            var error = expr.Errors[0];
            Assert.AreEqual(1, error.LineNo);
            Assert.AreEqual(1, error.Col);
            Assert.AreEqual("Expected bool, string, number or date", error.Message);
        }
    }
}