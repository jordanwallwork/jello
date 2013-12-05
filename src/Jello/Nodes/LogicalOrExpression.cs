namespace Jello.Nodes
{
    public class LogicalOrExpression : Node<LogicalOrExpression>
    {
        public LogicalAndExpression LogicalAnd { get; set; }
        public string Operator { get; set; }
        public LogicalOrExpression LogicalOr { get; set; }

        protected override LogicalOrExpression ParseNode()
        {
            LogicalAndExpression andExpr;
            if (ExpectNode(out andExpr))
            {
                LogicalAnd = andExpr;

                if (AcceptToken("||"))
                {
                    Operator = "||";
                    LogicalOrExpression orExpr;
                    if (ExpectNode(out orExpr))
                    {
                        LogicalOr = orExpr;
                    }
                }

            }
            return this;
        }
    }
}