using Jello.DataSources;

namespace Jello.Nodes
{
    public class LogicalOrExpression : BinaryTreeNode<LogicalOrExpression>
    {
        protected override LogicalOrExpression ParseNode()
        {
            INode expr;
            if (ExpectNode<LogicalAndExpression>(out expr))
            {
                LHS = expr;
                if (AcceptToken("||"))
                {
                    RHS = ExpectNode<LogicalOrExpression>();
                }
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            return RHS == null ? LHS.GetValue(dataSource) : Evaluate((l, r) => (bool?)l == true || (bool?)r == true, dataSource);
        }

        public override ValueType Type(IDataSource dataSource)
        {
            return RHS == null ? LHS.Type(dataSource) : ValueType.Bool;
        }
    }
}