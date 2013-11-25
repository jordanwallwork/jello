namespace Jello
{
    public class Token
    {
        public string Type { get; set; }
        public object Value { get; set; }
        public int LineNo { get; set; }

        public Token(string type, object value = null)
        {
            Type = type;
            Value = value;
        }
    }
}