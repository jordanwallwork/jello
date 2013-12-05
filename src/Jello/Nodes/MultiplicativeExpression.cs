namespace Jello.Nodes
{
    public class MultiplicativeExpression : Node<MultiplicativeExpression>
    {
        public PrimaryExpression LHS { get; set; }  
        public string Operator { get; set; }
        public MultiplicativeExpression RHS { get; set; }

        protected override MultiplicativeExpression ParseNode()
        {
            PrimaryExpression lhs;
            if (ExpectNode(out lhs))
            {
                LHS = lhs;
                object op;
                if (AcceptToken("*", out op) || AcceptToken("/", out op))
                {
                    Operator = op.ToString();

                    MultiplicativeExpression rhs;
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