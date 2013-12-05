namespace Jello.Nodes
{
    public class AdditiveExpression : Node<AdditiveExpression>
    {
        public MultiplicativeExpression LHS { get; set; }  
        public string Operator { get; set; }
        public AdditiveExpression RHS { get; set; }

        protected override AdditiveExpression ParseNode()
        {
            MultiplicativeExpression lhs;
            if (ExpectNode(out lhs))
            {
                LHS = lhs;
                object op;
                if (AcceptToken("+", out op) || AcceptToken("-", out op))
                {
                    Operator = op.ToString();

                    AdditiveExpression rhs;
                    if (ExpectNode(out rhs))
                    {
                        RHS = rhs;
                        return this;
                    }
                }
            }
            return this;
        }
    }
}