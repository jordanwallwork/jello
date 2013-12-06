namespace Jello.Nodes
{
    public class ConditionalExpression : Node<ConditionalExpression>
    {
        public LogicalOrExpression LHS { get; set; }

        protected override ConditionalExpression ParseNode()
        {
            LHS = ExpectNode<LogicalOrExpression>();
            return this;
        }
    }
}