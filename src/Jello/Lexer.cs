using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jello
{
    public class Lexer
    {
        private int _currLine = 1;
        private int _currTok { get; set; }
        private readonly List<Token> _tokens = new List<Token>();
        private readonly Stack<Token> _brackets = new Stack<Token>();

        public List<LexError> Errors = new List<LexError>();
        public int Pos { get { return _currTok; } }

        public Lexer(string input)
        {
            Lex(new StringReader(input));
        }

        public void ResetPos(int pos)
        {
            _currTok = pos;
        }

        public void Lex(TextReader input)
        {
            using (input)
            {
                while (input.Peek() != -1)
                {
                    var ch = (char) input.Read();
                    if (ch == '\n')
                    {
                        _currLine++;
                        continue;
                    }
                    if (char.IsWhiteSpace(ch)) continue;
                    var tok = GetToken(input, ch);
                    if (tok != null) _tokens.Add(tok);
                }
                EOF();
            }
        }

        private Token GetToken(TextReader input, char ch)
        {
            if (ch.IsPunctuation() || ch.IsBasicOperator()) return CreateToken(ch);
            if (ch.CanHaveEqualsAppended()) return TryGetOperatorWithAppendedEquals(input, ch);

            if (ch == '(' || ch == '[') return OpenBrackets(ch);
            if (ch == ')' || ch == ']') return CloseBrackets(ch);

            if (ch == '\"') return StringToken(input);

            return GetKeywordOrIdentifier(input, ch);
        }

        private Token CreateToken(string type, object value = null)
        {
            return new Token(type, value) { LineNo = _currLine };
        }

        private Token CreateToken(char type, object value = null)
        {
            return CreateToken(type.ToString(), value);
        }

        private Token TryGetOperatorWithAppendedEquals(TextReader input, char ch)
        {
            var next = (char)input.Peek();
            if (next != '=') return CreateToken(ch);
            input.Read();
            return CreateToken(ch + "=");
        }

        private Token StringToken(TextReader input)
        {
            var token = CreateToken("string");
            var stringBuilder = new StringBuilder();
            while (input.Peek() != -1)
            {
                var _ch = (char)input.Read();
                if (_ch == '\"')
                {
                    token.Value = stringBuilder.ToString();
                    return token;
                }
                if (_ch == '\\')
                {
                    var following = input.Read();
                    if (following == -1)
                    {
                        Errors.Add(new LexError { Token = token, Message = "String not closed - Unexpected end of file" });
                        token.Value = stringBuilder.ToString();
                        return token;
                    }
                    stringBuilder.Append(((char)following).IsEscapable() ? (char)following : _ch);
                }
                else stringBuilder.Append(_ch);
            }
            Errors.Add(new LexError { Token = token, Message = "String not closed - Unexpected end of file" });
            token.Value = stringBuilder.ToString();
            return token;
        }

        private Token GetKeywordOrIdentifier(TextReader input, char ch)
        {
            var value = GetValue(input, ch);

            if (IsKeyword(value)) return CreateToken("keyword", value);

            bool boolVal;
            if (IsBoolean(value, out boolVal)) return CreateToken("bool", boolVal);

            decimal decVal;
            if (IsNumeric(value, out decVal)) return CreateToken("number", decVal);

            return CreateToken("identifier", value);
        }

        private string GetValue(TextReader input, char ch)
        {
            var stringBuilder = new StringBuilder(ch.ToString());
            while (((char)input.Peek()).IsValidIdentifierCharacter())
            {
                stringBuilder.Append((char)input.Read());
            }
            return stringBuilder.ToString();
        }

        private bool IsBoolean(string value, out bool boolVal)
        {
            if (string.Equals(value, "true", StringComparison.InvariantCultureIgnoreCase))
            {
                boolVal = true;
                return true;
            }
            if (string.Equals(value, "false", StringComparison.InvariantCultureIgnoreCase))
            {
                boolVal = false;
                return true;
            }
            boolVal = false;
            return false;
        }

        private static bool IsNumeric(string value, out decimal decVal)
        {
            return decimal.TryParse(value, out decVal);
        }

        private static bool IsKeyword(string value)
        {
            return value == "var" || value == "if" || value == "then"
                || value == "else" || value == "return";
        }

        private Token OpenBrackets(char ch)
        {
            var open = CreateToken(ch);
            _brackets.Push(open);
            return open;
        }

        private Token CloseBrackets(char ch)
        {
            var close = CreateToken(ch);
            if (_brackets.Any())
            {
                var open = _brackets.Peek();
                if (BracketsMatch(open, close))
                {
                    _brackets.Pop();
                    return close;
                }
            }
            
            Errors.Add(new LexError { Token = close, Message = "Imbalanced brackets - no matching opening bracket found" });
            return close;
        }

        private static bool BracketsMatch(Token open, Token close)
        {
            return (open.Type == "(" && close.Type == ")") || (open.Type == "[" && close.Type == "]");
        }

        private void EOF()
        {
            foreach (var unclosed in _brackets)
            {
                Errors.Add(new LexError { Token = unclosed, Message = "Imbalanced brackets - no closing opening bracket found" });
            }
            _tokens.Add(CreateToken("EOF"));
        }

        public Token Next()
        {
            return _tokens[_currTok++];
        }
    }
}