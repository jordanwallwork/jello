using Jello.DataSources;

namespace Jello.Nodes
{
    public class Term : Node<Term>
    {
        public INode Node { get; set; }

        protected override Term ParseNode()
        {
            INode node;
            if (AcceptNode<Identifier>(out node) || AcceptNode<Bool>(out node) || AcceptNode<String>(out node) ||
                AcceptNode<Number>(out node) || AcceptNode<Date>(out node))
            {
                Node = node;
                return this;
            }
            return NoMatches("identifier", "bool", "string", "number", "date");
        }

        public override INode GetSingleChild()
        {
            return Node;
        }

        public override object GetValue(IDataSource dataSource)
        {
            return Node.GetValue(dataSource);
        }
    }
}