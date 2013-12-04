namespace Jello.Nodes
{
    public class Term : Node<Term>
    {
        public Bool Bool { get; set; }
        public String String { get; set; }

        protected override Term ParseNode()
        {
            Bool boolNode;
            if (ExpectNode(out boolNode))
            {
                Bool = boolNode;
                return this;
            }

            String stringNode;
            if (ExpectNode(out stringNode))
            {
                String = stringNode;
                return this;
            }

            return this;
        }
    }
}