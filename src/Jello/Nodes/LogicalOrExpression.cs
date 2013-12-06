namespace Jello.Nodes
{
    public class LogicalOrExpression : Node<LogicalOrExpression>
    {
        public LogicalAndExpression LHS { get; set; }
        public string Operator { get; set; }
        public LogicalOrExpression RHS { get; set; }

        protected override LogicalOrExpression ParseNode()
        {
            LogicalAndExpression andExpr;
            if (ExpectNode(out andExpr))
            {
                LHS = andExpr;

                if (AcceptToken("||"))
                {
                    Operator = "||";
                    LogicalOrExpression orExpr;
                    if (ExpectNode(out orExpr))
                    {
                        RHS = orExpr;
                    }
                }

            }
            return this;
        }
    }
}