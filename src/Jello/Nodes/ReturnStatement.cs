using Jello.DataSources;

namespace Jello.Nodes
{
    public class ReturnStatement : Node<ReturnStatement>
    {
        public INode LHS { get; set; }

        protected override ReturnStatement ParseNode()
        {
            if (ExpectKeyword("return"))
            {
                LHS = ExpectNode<Expression>();
                return this;
            }
            return NoMatches("Expected return");
        }

        public override INode GetSingleChild()
        {
            return null;
        }

        public override object GetValue(IDataSource dataSource)
        {
            return LHS.GetValue(dataSource);
        }
    }
}