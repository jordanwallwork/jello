namespace Jello.Nodes
{
    public class LogicalAndExpression : Node<LogicalAndExpression>
    {
        public BooleanExpression LHS { get; set; }
        public string Operator { get; set; }
        public LogicalAndExpression RHS { get; set; }

        protected override LogicalAndExpression ParseNode()
        {
            BooleanExpression boolExpr;
            if (ExpectNode(out boolExpr))
            {
                LHS = boolExpr;

                if (AcceptToken("&&"))
                {
                    Operator = "&&";
                    LogicalAndExpression andExpr;
                    if (ExpectNode(out andExpr))
                    {
                        RHS = andExpr;
                    }
                }
            }
            return this;
        }
    }
}