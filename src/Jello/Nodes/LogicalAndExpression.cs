namespace Jello.Nodes
{
    public class LogicalAndExpression : BinaryTreeNode<LogicalAndExpression>
    {
        protected override LogicalAndExpression ParseNode()
        {
            LHS = ExpectNode<BooleanExpression>();
            if (AcceptToken("&&"))
            {
                RHS = ExpectNode<LogicalAndExpression>();
            }
            return this;
        }

        public override object GetValue()
        {
            return RHS == null ? LHS.GetValue() : Evaluate((l, r) => (bool?)l == true && (bool?)r == true);
        }
    }
}