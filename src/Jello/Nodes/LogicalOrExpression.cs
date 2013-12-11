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

        public override object GetValue()
        {
            return RHS == null ? LHS.GetValue() : Evaluate((l, r) => (bool?)l == true || (bool?)r == true);
        }
    }
}