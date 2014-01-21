using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Jello.Nodes;

namespace Jello
{
    public class Jello
    {
        public Settings Settings { get; set; }

        public Jello(Settings settings = null)
        {
            Settings = settings ?? new Settings();
        }

        public ParseResult Parse(string input)
        {
            var lexer = new Lexer(input);
            if (lexer.Errors.Any()) return new ParseResult(lexer.Errors);
            var node = new PrimaryExpression();
            node.Parse(this, lexer);
            if (node.Errors.Any()) return new ParseResult(node.Errors);
            return new ParseResult(node);
        }
    }
}