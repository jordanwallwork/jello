using System.Collections.Generic;
using Jello.DataSources;

namespace Jello.Nodes
{
    public class AdditiveExpression : BinaryTreeNode<AdditiveExpression>
    {
        protected override AdditiveExpression ParseNode()
        {
            INode node;
            if (ExpectNode<MultiplicativeExpression>(out node))
            {
                LHS = node;
                object op;
                if (AcceptToken(out op, "+", "-"))
                {
                    Operator = op.ToString();
                    RHS = ExpectNode<AdditiveExpression>();
                }
            }
            return this;
        }

        public override INode GetSingleChild()
        {
            if (RHS == null) return LHS;
            return null;
        }

        public override object GetValue(IDataSource dataSource)
        {
            if (Operator == "+") return Evaluate((l, r) => (decimal?)l + (decimal?)r, dataSource);
            if (Operator == "-") return Evaluate((l, r) => (decimal?)l - (decimal?)r, dataSource);
            return LHS.GetValue(dataSource);
        }
    }
}