namespace Jello
{
    public class ParseError
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
    }
}