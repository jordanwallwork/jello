namespace Jello.Nodes
{
    public class IfStatement : Node<IfStatement>
    {
        public INode Condition { get; set; }
        public INode Statements { get; set; }
        public INode ElseStatements { get; set; }

        protected override IfStatement ParseNode()
        {
            if (ExpectKeyword("if"))
            {
                Condition = ExpectNode<ConditionalExpression>();
                ExpectKeyword("then");
                Statements = ExpectNode<StatementList>();
                if (AcceptKeyword("else"))
                {
                    ElseStatements = ExpectNode<StatementList>();
                }
            }
            return this;
        }

        public override INode GetSingleChild()
        {
            return null;
        }
    }
}