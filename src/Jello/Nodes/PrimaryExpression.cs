using System.Runtime.InteropServices;

namespace Jello.Nodes
{
    public class PrimaryExpression : Node<PrimaryExpression>
    {
        public Term Term { get; set; }
        public Expression Expr { get; set; }

        protected override PrimaryExpression ParseNode()
        {
            Term term;
            if (AcceptNode(out term))
            {
                Term = term;
                return this;
            }

            if (AcceptToken("("))
            {
                Expression primaryExpression;
                if (ExpectNode(out primaryExpression)) Expr = primaryExpression;
                if (ExpectToken(")")) return this;
            }

            return NoMatches("'('", "expression");
        }
    }
}