namespace Jello.Nodes
{
    public class ConditionalExpression : Node<ConditionalExpression>
    {
        public LogicalOrExpression LogicalOr { get; set; }

        protected override ConditionalExpression ParseNode()
        {
            LogicalOrExpression expr;
            if (ExpectNode(out expr))
            {
                LogicalOr = expr;
                return this;
            }
            return this;
        }
    }
}