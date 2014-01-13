using Jello.DataSources;

namespace Jello.Nodes
{
    public class LogicalAndExpression : BinaryTreeNode<LogicalAndExpression>
    {
        protected override LogicalAndExpression ParseNode()
        {
            INode expr;
            if (ExpectNode<BooleanExpression>(out expr))
            {
                LHS = expr;
                if (AcceptToken("&&"))
                {
                    RHS = ExpectNode<LogicalAndExpression>();
                }
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            return RHS == null ? LHS.GetValue(dataSource) : Evaluate((l, r) => (bool?)l == true && (bool?)r == true, dataSource);
        }
    }
}