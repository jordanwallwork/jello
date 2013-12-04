namespace Jello
{
    public static class CharExtensions
    {
        public static bool CanHaveEqualsAppended(this char ch)
        {
            return ch == '=' || ch == '<' || ch == '>';
        }

        public static bool IsBasicOperator(this char ch)
        {
            return ch == '+' || ch == '-' || ch == '*' || ch == '/';
        }

        public static bool IsPunctuation(this char ch)
        {
            return ch == ',' || ch == ';';
        }

        public static bool IsEscapable(this char ch)
        {
            return ch == '\"' || ch == '\\' || ch == 't' || ch == 'n';
        }

        public static bool IsValidIdentifierCharacter(this char ch)
        {
            return char.IsLetterOrDigit(ch) || ch == '.' || ch == '_';
        }
    }
}
