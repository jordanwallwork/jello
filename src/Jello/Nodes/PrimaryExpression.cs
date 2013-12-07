using System.Runtime.InteropServices;

namespace Jello.Nodes
{
    public class PrimaryExpression : Node<PrimaryExpression>
    {
        public INode Inner { get; set; }

        protected override PrimaryExpression ParseNode()
        {
            INode term;
            if (AcceptNode<Term>(out term))
            {
                Inner = term;
                return this;
            }

            if (AcceptToken("("))
            {
                Inner = ExpectNode<Expression>();
                ExpectToken(")");
                return this;
            }

            return NoMatches("'('", "expression");
        }

        public override INode GetSingleChild()
        {
            return Inner;
        }
    }

}