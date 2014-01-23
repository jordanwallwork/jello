using Jello.DataSources;

namespace Jello.Nodes
{
    public class Bool : TerminalNode<Bool>
    {
        public bool Value { get; set; }

        protected override Bool ParseNode()
        {
            object boolVal;
            if (ExpectToken("bool", out boolVal))
            {
                Value = (bool)boolVal;
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            return Value;
        }

        public override ValueType Type(IDataSource dataSource) { return ValueType.Bool; }
    }

}