using System.Collections.Generic;

namespace Jello.Nodes
{
    public class AdditiveExpression : BinaryTreeNode<AdditiveExpression>
    {
        protected override AdditiveExpression ParseNode()
        {
            LHS = ExpectNode<MultiplicativeExpression>();
            object op;
            if (AcceptToken("+", out op) || AcceptToken("-", out op))
            {
                Operator = op.ToString();
                RHS = ExpectNode<AdditiveExpression>();
            }
            return this;
        }

        public override INode GetSingleChild()
        {
            if (RHS == null) return LHS;
            return null;
        }

        public override object GetValue()
        {
            if (Operator == "+") return Evaluate((l, r) => (decimal?) l + (decimal?) r);
            if (Operator == "-") return Evaluate((l, r) => (decimal?) l - (decimal?) r);
            return LHS.GetValue();
        }
    }
}