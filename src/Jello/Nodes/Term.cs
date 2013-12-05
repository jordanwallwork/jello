namespace Jello.Nodes
{
    public class Term : Node<Term>
    {
        public Bool Bool { get; set; }
        public String String { get; set; }
        public Number Number { get; set; }

        protected override Term ParseNode()
        {
            Bool boolNode;
            if (AcceptNode(out boolNode))
            {
                Bool = boolNode;
                return this;
            }

            String stringNode;
            if (AcceptNode(out stringNode))
            {
                String = stringNode;
                return this;
            }

            Number numberNode;
            if (AcceptNode(out numberNode))
            {
                Number = numberNode;
                return this;
            }

            return NoMatches("bool", "string");
        }
    }
}