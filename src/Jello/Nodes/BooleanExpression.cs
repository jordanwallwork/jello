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

        public override object GetValue()
        {
            if (Operator == "==") return Evaluate((l,r) => l.Equals(r));
            if (Operator == "!=") return Evaluate((l,r) => !l.Equals(r));

            if (Operator == "<") return Evaluate((l, r) => (decimal?)l < (decimal?)r);
            if (Operator == "<=") return Evaluate((l, r) => (decimal?)l <= (decimal?)r);
            if (Operator == ">") return Evaluate((l, r) => (decimal?)l > (decimal?)r);
            if (Operator == ">=") return Evaluate((l, r) => (decimal?)l >= (decimal?)r);

            return LHS.GetValue();
        }
    }
}