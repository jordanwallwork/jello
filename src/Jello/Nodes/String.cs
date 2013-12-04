namespace Jello.Nodes
{
    public class String : Node<String>
    {
        public string Value { get; set; }

        protected override String ParseNode()
        {
            Token token;
            if (Expect("string", out token))
            {
                Value = (string)token.Value;
            }
            return this;
        }

    }
}