namespace Jello.Nodes
{
    public class BooleanExpression : Node<BooleanExpression>
    {
        public AdditiveExpression ArithmeticExpression { get; set; }
        public string BooleanOperator { get; set; }
        public BooleanExpression Boolean { get; set; }

        protected override BooleanExpression ParseNode()
        {
            AdditiveExpression arithExpr;
            if (ExpectNode(out arithExpr))
            {
                ArithmeticExpression = arithExpr;

                object op;
                if (AcceptToken("==", out op) || AcceptToken("!=", out op) ||
                    AcceptToken("<", out op) || AcceptToken("<=", out op) ||
                    AcceptToken(">", out op) || AcceptToken(">=", out op))
                {
                    BooleanOperator = op.ToString();
                    BooleanExpression boolExpr;
                    if (ExpectNode(out boolExpr))
                    {
                        Boolean = boolExpr;
                    }
                }
            }
            return this;
        }
    }
}