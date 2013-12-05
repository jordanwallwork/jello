namespace Jello.Nodes
{
    public class Number : Node<Number>
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
    }
}