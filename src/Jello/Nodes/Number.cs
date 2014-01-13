using Jello.DataSources;

namespace Jello.Nodes
{
    public class Number : BinaryTreeNode<Number>
    {
        public decimal Value { get; set; }

        protected override Number ParseNode()
        {
            object decVal;
            if (ExpectToken("number", out decVal))
            {
                Value = (decimal) decVal;
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            return Value;
        }
    }
}