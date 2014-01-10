namespace Jello.Nodes
{
    public class AssignmentStatement : Node<AssignmentStatement>
    {
        public bool IsNew { get; set; }
        public string Identifier { get; set; }
        public INode Expression { get; set; }

        protected override AssignmentStatement ParseNode()
        {
            if (AcceptKeyword("var"))
            {
                IsNew = true;
            }
            object identifier;
            if (ExpectToken("identifier", out identifier))
            {
                Identifier = identifier.ToString();
                if (ExpectToken("="))
                {
                    Expression = ExpectNode<PrimaryExpression>();
                }
            }
            return this;
        }

        public override INode GetSingleChild()
        {
            return null;
        }

        public override object GetValue()
        {
            return Expression.GetValue();
        }
    }
}