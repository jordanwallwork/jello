namespace Jello.Errors
{
    public class ParseError : IError
    {
        public string Message { get; set; }
        public int LineNo { get; set; }
        public int Col { get; set; }

        public ParseError(string message, int lineNo, int col)
        {
            Message = message;
            LineNo = lineNo;
            Col = col;
        }

        public string DisplayMessage()
        {
            return string.Format("{0} (Line: {1}, Col: {2})", Message, LineNo, Col);
        }
    }
}