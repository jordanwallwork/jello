namespace Jello
{
    public class ParseError
    {
        public string Message { get; set; }

        public ParseError(string message)
        {
            Message = message;
        }
    }
}