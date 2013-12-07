namespace Jello.Nodes
{
    public class BooleanExpression : BinaryTreeNode<BooleanExpression>
    {
        public string BooleanOperator { get; set; }

        protected override BooleanExpression ParseNode()
        {
            LHS = ExpectNode<AdditiveExpression>();

            object op;
            if (AcceptToken("==", out op) || AcceptToken("!=", out op) ||
                AcceptToken("<", out op) || AcceptToken("<=", out op) ||
                AcceptToken(">", out op) || AcceptToken(">=", out op))
            {
                BooleanOperator = op.ToString();
                RHS = ExpectNode<BooleanExpression>();
            }
            return this;
        }

        public override object GetValue()
        {
            if (Operator == "==") return Evaluate((l,r) => l == r);
            if (Operator == "!=") return Evaluate((l,r) => l != r);

            if (Operator == "<") return Evaluate((l, r) => (decimal?)l < (decimal?)r);
            if (Operator == "<=") return Evaluate((l, r) => (decimal?)l <= (decimal?)r);
            if (Operator == ">") return Evaluate((l, r) => (decimal?)l > (decimal?)r);
            if (Operator == ">=") return Evaluate((l, r) => (decimal?)l >= (decimal?)r);

            return LHS.GetValue();
        }
    }
}