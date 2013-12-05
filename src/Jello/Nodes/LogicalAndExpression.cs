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
            if (ExpectNode(out boolExpr))
            {
                BooleanExpression = boolExpr;

                if (AcceptToken("&&"))
                {
                    Operator = "&&";
                    LogicalAndExpression andExpr;
                    if (ExpectNode(out andExpr))
                    {
                        LogicalAnd = andExpr;
                    }
                }
            }
            return this;
        }
    }
}