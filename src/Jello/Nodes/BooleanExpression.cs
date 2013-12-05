namespace Jello.Nodes
{
    public class BooleanExpression : Node<BooleanExpression>
    {
        public BooleanExpression Boolean { get; set; }
        public string BooleanOperator { get; set; }
        public AdditiveExpression ArithmeticExpression { get; set; }

        protected override BooleanExpression ParseNode()
        {
            AdditiveExpression arithExpr;
            if (AcceptNode(out arithExpr))
            {
                ArithmeticExpression = arithExpr;
                return this;
            }

            BooleanExpression relExpr;
            if (ExpectNode(out relExpr))
            {
                object op;
                if (AcceptToken("==", out op) || AcceptToken("!=", out op))
                {
                    if (ExpectNode(out arithExpr))
                    {
                        ArithmeticExpression = arithExpr;
                    }
                }
                if (AcceptToken("<", out op) || AcceptToken("<=", out op) ||
                    AcceptToken(">", out op) || AcceptToken(">=", out op))
                {
                    if (ExpectNode(out arithExpr))
                    {
                        ArithmeticExpression = arithExpr;
                    }
                }
            }
            return this;
        }
    }
}