namespace Jello.Nodes
{
    public class LogicalAndExpression : Node<LogicalAndExpression>
    {
        public BooleanExpression BooleanExpression { get; set; }
        public string Operator { get; set; }
        public LogicalAndExpression LogicalAnd { get; set; }

        protected override LogicalAndExpression ParseNode()
        {
            BooleanExpression boolExpr;
            if (AcceptNode(out boolExpr))
            {
                BooleanExpression = boolExpr;
                return this;
            }

            LogicalAndExpression expr;
            if (ExpectNode(out expr))
            {
                LogicalAnd = expr;

                if (AcceptToken("&&"))
                {
                    Operator = "&&";
                    if (ExpectNode(out boolExpr))
                    {
                        BooleanExpression = boolExpr;
                    }
                    else
                    {
                        return NoMatches("equality_expression");
                    }
                }

                return this;
            }
            return this;
        }
    }
}