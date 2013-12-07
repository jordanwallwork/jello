namespace Jello.Nodes
{
    public class MultiplicativeExpression : BinaryTreeNode<MultiplicativeExpression>
    {
        protected override MultiplicativeExpression ParseNode()
        {
            LHS = ExpectNode<PrimaryExpression>();

            object op;
            if (AcceptToken("*", out op) || AcceptToken("/", out op))
            {
                Operator = op.ToString();
                RHS = ExpectNode<MultiplicativeExpression>();
            }
            return this;
        }

        public override object GetValue()
        {
            if (Operator == "*") return Evaluate((l, r) => (decimal?)l * (decimal?)r);
            if (Operator == "/") return Evaluate((l, r) => (decimal?)l / (decimal?)r);
            return LHS.GetValue();
        }
    }
}