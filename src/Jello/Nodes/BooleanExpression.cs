namespace Jello.Nodes
{
    public class BooleanExpression : Node<BooleanExpression>
    {
        public AdditiveExpression LHS { get; set; }
        public string BooleanOperator { get; set; }
        public BooleanExpression RHS { get; set; }

        protected override BooleanExpression ParseNode()
        {
            LHS = ExpectNode<AdditiveExpression>();

            object op;
            if (AcceptToken("==", out op) || AcceptToken("!=", out op) ||
                AcceptToken("<", out op) || AcceptToken("<=", out op) ||
                AcceptToken(">", out op) || AcceptToken(">=", out op))
            {
                BooleanOperator = op.ToString();
                RHS = ExpectNode<BooleanExpression>();
            }
            return this;
        }
    }
}