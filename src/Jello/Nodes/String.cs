using Jello.DataSources;

namespace Jello.Nodes
{
    public class String : TerminalNode<String>
    {
        public string Value { get; set; }

        protected override String ParseNode()
        {
            object strVal;
            if (ExpectToken("string", out strVal))
            {
                Value = (string)strVal;
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            return Value;
        }

        public override ValueType Type(IDataSource dataSource) { return ValueType.String; }
    }
}