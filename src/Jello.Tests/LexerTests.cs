using System;
using NUnit.Framework;

namespace Jello.Tests
{
    [TestFixture]
    public class LexerTests
    {
        private static void AssertTokenIs(string type, Token token)
        {
            AssertTokenIs(type, null, token);
        }
        private static void AssertTokenIs(string type, object value, Token token)
        {
            Assert.AreEqual(type, token.Type);
            if (value != null) Assert.AreEqual(value, token.Value);
        }

        [Test]
        public void ShouldReturnEOFAtEnd()
        {
            var lexer = new Lexer("");
            AssertTokenIs("EOF", lexer.Next());
        }

        [Test]
        public void ShouldIgnoreWhitespace()
        {
            var lexer = new Lexer("    ");
            AssertTokenIs("EOF", lexer.Next());
        }

        [Test]
        public void ShouldSplitOperators()
        {
            var lexer = new Lexer("+/=");
            AssertTokenIs("+", lexer.Next());
            AssertTokenIs("/", lexer.Next());
            AssertTokenIs("=", lexer.Next());
            AssertTokenIs("EOF", lexer.Next());
        }

        [Test]
        public void ShouldSplitOperators_WithWhitespace()
        {
            var lexer = new Lexer("+/=");
            AssertTokenIs("+", lexer.Next());
            AssertTokenIs("/", lexer.Next());
            AssertTokenIs("=", lexer.Next());
            AssertTokenIs("EOF", lexer.Next());
        }

        [Test]
        public void ShouldDistinguishTwoDigitOperatorsStartingWithSameChar()
        {
            var lexer = new Lexer("= ==");
            AssertTokenIs("=", lexer.Next());
            AssertTokenIs("==", lexer.Next());
            AssertTokenIs("EOF", lexer.Next());
        }

        [Test]
        public void ShouldDetectUnbalancedBrackets_MissingOpening()
        {
            var lexer = new Lexer("( ) )");
            Assert.AreEqual(1, lexer.Errors.Count);
            Assert.AreEqual("Imbalanced brackets - no matching opening bracket found", lexer.Errors[0].Message);
        }

        [Test]
        public void ShouldDetectUnbalancedBrackets_MissingClosing()
        {
            var lexer = new Lexer("( ( )");
            Assert.AreEqual(1, lexer.Errors.Count);
            Assert.AreEqual("Imbalanced brackets - no closing opening bracket found", lexer.Errors[0].Message);
        }

        [Test]  
        public void ShouldTokenizeString()
        {
            var lexer = new Lexer("\"this is a string\"");
            AssertTokenIs("string", "this is a string", lexer.Next());
        }

        [Test]  
        public void ShouldTokenizeStringIgnoringEscapedQuotes()
        {
            var lexer = new Lexer("\"the cow said \\\"moo\\\"\"");
            AssertTokenIs("string", "the cow said \"moo\"", lexer.Next());
        }

        [Test]
        public void ShouldReportErrorForUnclosedString()
        {
            var lexer = new Lexer("\"the cow said \\\"moo\\\"");
            AssertTokenIs("string", "the cow said \"moo\"", lexer.Next());
            Assert.AreEqual(lexer.Errors.Count, 1);
            Assert.AreEqual("String not closed - Unexpected end of file", lexer.Errors[0].Message);
        }

        [Test]
        public void ShouldReturnKeywordsAndIdentifiers()
        {
            var lexer = new Lexer("var ident = \"beer\"");
            AssertTokenIs("keyword", "var", lexer.Next());
            AssertTokenIs("identifier", "ident", lexer.Next());
            AssertTokenIs("=", lexer.Next());
            AssertTokenIs("string", "beer", lexer.Next());
        }

        [Test]
        public void ShouldRecordPositionOfTokens()
        {
            var lexer = new Lexer("one\ntwo\nthree");
            Assert.AreEqual(1, lexer.Next().LineNo);
            Assert.AreEqual(2, lexer.Next().LineNo);
            Assert.AreEqual(3, lexer.Next().LineNo);
        }

        [Test]
        public void ShouldRecognizeBooleans()
        {
            var lexer = new Lexer("TRUE false");
            AssertTokenIs("bool", true, lexer.Next());
            AssertTokenIs("bool", false, lexer.Next());
        }

        [Test]
        public void ShouldRecognizeNumbers()
        {
            var lexer = new Lexer("1 2.3 4.56789");
            AssertTokenIs("number", 1, lexer.Next());
            AssertTokenIs("number", 2.3, lexer.Next());
            AssertTokenIs("number", 4.56789, lexer.Next());
        }

        [Test]
        public void ShouldRecognizeDates()
        {
            var lexer = new Lexer("'1989-09-29'");
            AssertTokenIs("date", "1989-09-29", lexer.Next());
        }
    }
}
