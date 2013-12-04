using System.Runtime.InteropServices;

namespace Jello.Nodes
{
    public class Expression : Node<Expression>
    {
        public Term Term { get; set; }
        public Expression Expr { get; set; }

        protected override Expression ParseNode()
        {
            Term term;
            if (AcceptNode(out term))
            {
                Term = term;
                return this;
            }

            if (AcceptToken("("))
            {
                Expression expression;
                if (ExpectNode(out expression)) Expr = expression;
                if (ExpectToken(")")) return this;
            }

            Errors.Add(new ParseError("Expected '(' or expression", Lexer.LineNo, Lexer.Col));
            return this;
        }
    }
}