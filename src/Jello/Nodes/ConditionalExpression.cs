namespace Jello.Nodes
{
    public class ConditionalExpression : Node<ConditionalExpression>
    {
        public LogicalOrExpression LHS { get; set; }

        protected override ConditionalExpression ParseNode()
        {
            LogicalOrExpression expr;
            if (ExpectNode(out expr))
            {
                LHS = expr;
                return this;
            }
            return this;
        }
    }
}