using Jello.DataSources;

namespace Jello.Nodes
{
    public class MultiplicativeExpression : BinaryTreeNode<MultiplicativeExpression>
    {
        protected override MultiplicativeExpression ParseNode()
        {
            INode node;
            if (ExpectNode<PrimaryExpression>(out node))
            {
                LHS = node;
                object op;
                if (AcceptToken(out op, "*", "/"))
                {
                    Operator = op.ToString();
                    RHS = ExpectNode<MultiplicativeExpression>();
                }
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            if (Operator == "*") return Evaluate((l, r) => (decimal?)l * (decimal?)r, dataSource);
            if (Operator == "/") return Evaluate((l, r) => (decimal?)l / (decimal?)r, dataSource);
            return LHS.GetValue(dataSource);
        }
    }
}