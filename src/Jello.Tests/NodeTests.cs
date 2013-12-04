using System.Runtime;
using System.Security.Cryptography;
using Jello.Nodes;
using NUnit.Framework;

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
        public void ShouldShowUsefulErrorInformation()
        {
            var expr = new Jello().Parse<Term>("");
            var error = expr.Errors[0];
            Assert.AreEqual(1, error.LineNo);
            Assert.AreEqual(1, error.Col);
            Assert.AreEqual("Expected bool or string", error.Message);
        }
    }
}