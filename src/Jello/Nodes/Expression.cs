namespace Jello.Nodes
{
    public class Expression : Node<Expression>
    {
        public ConditionalExpression Conditional { get; set; }

        protected override Expression ParseNode()
        {
            ConditionalExpression expr;
            if (ExpectNode(out expr))
            {
                Conditional = expr;
                return this;
            }
            return this;
        }
    }
}