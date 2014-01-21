namespace Jello.Errors
{
    public class LexError : IError
    {
        public Token Token { get; set; }
        public string Message { get; set; }

        public string DisplayMessage() { return Message; }
    }
}