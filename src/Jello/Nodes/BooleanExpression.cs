namespace Jello.Nodes
{
    public class BooleanExpression : Node<BooleanExpression>
    {
        public AdditiveExpression LHS { get; set; }
        public string BooleanOperator { get; set; }
        public BooleanExpression RHS { get; set; }

        protected override BooleanExpression ParseNode()
        {
            AdditiveExpression arithExpr;
            if (ExpectNode(out arithExpr))
            {
                LHS = arithExpr;

                object op;
                if (AcceptToken("==", out op) || AcceptToken("!=", out op) ||
                    AcceptToken("<", out op) || AcceptToken("<=", out op) ||
                    AcceptToken(">", out op) || AcceptToken(">=", out op))
                {
                    BooleanOperator = op.ToString();
                    BooleanExpression boolExpr;
                    if (ExpectNode(out boolExpr))
                    {
                        RHS = boolExpr;
                    }
                }
            }
            return this;
        }
    }
}