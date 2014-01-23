using Jello.DataSources;
using Jello.Utils;

namespace Jello.Nodes
{
    public class BooleanExpression : BinaryTreeNode<BooleanExpression>
    {
        protected override BooleanExpression ParseNode()
        {
            INode node;
            if (ExpectNode<AdditiveExpression>(out node))
            {
                LHS = node;
                object op;
                if (AcceptToken(out op, "==", "!=", "<", "<=", ">", ">="))
                {
                    Operator = op.ToString();
                    RHS = ExpectNode<BooleanExpression>();
                }
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            if (Operator == "==") return Evaluate((l, r) => l.Equals(r), dataSource);
            if (Operator == "!=") return Evaluate((l, r) => !l.Equals(r), dataSource);

            if (Operator == "<") return Evaluate((l, r) => (decimal?)l < (decimal?)r, dataSource);
            if (Operator == "<=") return Evaluate((l, r) => (decimal?)l <= (decimal?)r, dataSource);
            if (Operator == ">") return Evaluate((l, r) => (decimal?)l > (decimal?)r, dataSource);
            if (Operator == ">=") return Evaluate((l, r) => (decimal?)l >= (decimal?)r, dataSource);

            return LHS.GetValue(dataSource);
        }

        public override ValueType Type(IDataSource dataSource)
        {
            return RHS == null ? LHS.Type(dataSource) : ValueType.Bool;
        }
    }
}