namespace Jello.Nodes
{
    public class MultiplicativeExpression : Node<MultiplicativeExpression>
    {
        public PrimaryExpression LHS { get; set; }  
        public string Operator { get; set; }
        public MultiplicativeExpression RHS { get; set; }

        protected override MultiplicativeExpression ParseNode()
        {
            LHS = ExpectNode<PrimaryExpression>();

            object op;
            if (AcceptToken("*", out op) || AcceptToken("/", out op))
            {
                Operator = op.ToString();
                RHS = ExpectNode<MultiplicativeExpression>();
            }
            return this;
        }
    }
}