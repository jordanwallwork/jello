using System.Linq;
using Jello.Nodes;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class StatementNodeTests
    {
        [Test]
        public void ShouldResolveReturnStatement()
        {
            var retStmt = new Jello().Parse<ReturnStatement>("return 1");
            Assert.IsTrue(!retStmt.Errors.Any());
            Assert.AreEqual(1, retStmt.LHS.GetValue());
        }

        [Test]
        public void ShouldResolveReturnStatementAsStatement()
        {
            var stmt = new Jello().Parse<Statement>("return true");
            Assert.IsTrue((bool) stmt.LHS.GetValue());
        }

        [Test]
        public void ShouldResolveIfStatement()
        {
            var stmt = new Jello().Parse<IfStatement>("if false then return true");
            Assert.IsFalse((bool)stmt.Condition.GetValue());
            Assert.IsAssignableFrom<ReturnStatement>(stmt.Statements);
        }

        [Test]
        public void ShouldResolveAssignmentStatus()
        {
            var stmt = new Jello().Parse<Statement>("var t = 1");
            Assert.IsAssignableFrom<AssignmentStatement>(stmt.GetSingleChild());
            var assStmt = stmt.GetSingleChild() as AssignmentStatement;
            Assert.IsTrue(assStmt.IsNew);
            Assert.AreEqual("t", assStmt.Identifier);
            Assert.AreEqual(1, assStmt.Expression.GetValue());
        }
    }
}