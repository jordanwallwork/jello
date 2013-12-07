namespace Jello.Nodes
{
    public class LogicalOrExpression : BinaryTreeNode<LogicalOrExpression>
    {
        protected override LogicalOrExpression ParseNode()
        {
            LHS = ExpectNode<LogicalAndExpression>();
            if (AcceptToken("||"))
            {
                Operator = "||";
                RHS = ExpectNode<LogicalOrExpression>();
            }
            return this;
        }

        public override object GetValue()
        {
            return RHS == null ? LHS.GetValue() : Evaluate((l, r) => (bool?)l == true || (bool?)r == true);
        }
    }
}