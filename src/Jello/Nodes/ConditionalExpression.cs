namespace Jello.Nodes
{
    public class ConditionalExpression : Node<ConditionalExpression>
    {
        public INode LHS { get; set; }

        protected override ConditionalExpression ParseNode()
        {
            LHS = ExpectNode<LogicalOrExpression>();
            return this;
        }

        public override INode GetSingleChild()
        {
            return LHS;
        }
    }
}