namespace Jello.Nodes
{
    public class String : Node<String>
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
    }
}