namespace Jello.Nodes
{
    public class LogicalAndExpression : Node<LogicalAndExpression>
    {
        public BooleanExpression LHS { get; set; }
        public string Operator { get; set; }
        public LogicalAndExpression RHS { get; set; }

        protected override LogicalAndExpression ParseNode()
        {
            LHS = ExpectNode<BooleanExpression>();
            if (AcceptToken("&&"))
            {
                Operator = "&&";
                RHS = ExpectNode<LogicalAndExpression>();
            }
            return this;
        }
    }
}