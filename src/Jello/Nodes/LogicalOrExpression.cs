namespace Jello.Nodes
{
    public class LogicalOrExpression : Node<LogicalOrExpression>
    {
        public LogicalAndExpression LHS { get; set; }
        public string Operator { get; set; }
        public LogicalOrExpression RHS { get; set; }

        protected override LogicalOrExpression ParseNode()
        {
            LHS = ExpectNode<LogicalAndExpression>();
            if (AcceptToken("||"))
            {
                Operator = "||";
                RHS = ExpectNode<LogicalOrExpression>();
            }
            return this;
        }
    }
}