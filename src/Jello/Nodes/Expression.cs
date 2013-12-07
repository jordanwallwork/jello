namespace Jello.Nodes
{
    public class Expression : Node<Expression>
    {
        public INode LHS { get; set; }

        protected override Expression ParseNode()
        {
            LHS = ExpectNode<ConditionalExpression>();
            return this;
        }

        public override INode GetSingleChild()
        {
            return LHS;
        }
    }
}