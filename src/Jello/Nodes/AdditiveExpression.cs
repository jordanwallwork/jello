using System.Collections.Generic;

namespace Jello.Nodes
{
    public class AdditiveExpression : Node<AdditiveExpression>
    {
        public MultiplicativeExpression LHS { get; set; }  
        public string Operator { get; set; }
        public AdditiveExpression RHS { get; set; }

        protected override AdditiveExpression ParseNode()
        {
            LHS = ExpectNode<MultiplicativeExpression>();
            object op;
            if (AcceptToken("+", out op) || AcceptToken("-", out op))
            {
                Operator = op.ToString();
                RHS = ExpectNode<AdditiveExpression>();
            }
            return this;
        }
    }
}