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
                Expr = ExpectNode<Expression>();
                ExpectToken(")");
                return this;
            }

            return NoMatches("'('", "expression");
        }
    }
}