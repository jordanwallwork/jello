using System;

namespace Jello.Nodes
{
    public class Bool : Node<Bool>
    {
        public bool Value { get; set; }

        protected override Bool ParseNode()
        {
            Token token;
            if (Expect("bool", out token))
            {
                Value = (bool)token.Value;
            }
            return this;
        }
    }
}