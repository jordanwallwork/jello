namespace Jello.Nodes
{
    public class Expression : Node<Expression>
    {
        public ConditionalExpression LHS { get; set; }

        protected override Expression ParseNode()
        {
            LHS = ExpectNode<ConditionalExpression>();
            return this;
        }
    }
}