namespace Jello.Nodes
{
    public class LogicalOrExpression : Node<LogicalOrExpression>
    {
        public LogicalOrExpression LogicalOr { get; set; }
        public string Operator { get; set; }
        public LogicalAndExpression LogicalAnd { get; set; }

        protected override LogicalOrExpression ParseNode()
        {
            LogicalAndExpression andExpr;
            if (AcceptNode(out andExpr))
            {
                LogicalAnd = andExpr;
                return this;
            }

            LogicalOrExpression expr;
            if (ExpectNode(out expr))
            {
                LogicalOr = expr;

                if (AcceptToken("||"))
                {
                    Operator = "||";
                    if (ExpectNode(out andExpr))
                    {
                        LogicalAnd = andExpr;
                    }
                    else
                    {
                        return NoMatches("logical_and");
                    }
                }

                return this;
            }
            return this;
        }
    }
}