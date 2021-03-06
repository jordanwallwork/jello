﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jello.Errors;
using Jello.Utils;

namespace Jello
{
    public class Lexer
    {
        private int _currTok { get; set; }
        private readonly List<Token> _tokens = new List<Token>();
        private readonly Stack<Token> _brackets = new Stack<Token>();

        public List<LexError> Errors = new List<LexError>();
        public int Pos { get { return _currTok; } }
        public int LineNo  { get; private set; }
        public int Col  { get; private set; }

        public Lexer(string input)
        {
            LineNo = 1;
            Col = 1;
            Lex(new StringReader(input));
        }

        public void ResetPos(int pos)
        {
            _currTok = pos;
            LineNo = _tokens[pos].LineNo;
            Col = _tokens[pos].Col;
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
                        LineNo++;
                        Col = 1;
                        continue;
                    }
                    if (!char.IsWhiteSpace(ch))
                    {
                        var tok = GetToken(input, ch);
                        if (tok != null) _tokens.Add(tok);
                    }
                    Col++;
                }
                EOF();
            }
        }

        private Token GetToken(TextReader input, char ch)
        {
            if (ch.IsPunctuation()) return CreateToken(ch);
            Token op;
            if (TryGetOperator(input, ch, out op))
            {
                return op;
            }

            if (ch == '(' || ch == '[') return OpenBrackets(ch);
            if (ch == ')' || ch == ']') return CloseBrackets(ch);

            if (ch == '\"') return StringToken(input);
            if (ch == '\'') return DateToken(input);

            return GetKeywordOrIdentifier(input, ch);
        }

        private Token CreateToken(string type, object value = null)
        {
            return new Token(type, value) { LineNo = LineNo, Col = Col };
        }

        private Token CreateToken(char type, object value = null)
        {
            return CreateToken(type.ToString(), value);
        }

        private bool TryGetOperator(TextReader input, char ch, out Token token)
        {
            var next = (char)input.Peek();
            if (IsOperator(ch + next.ToString()))
            {
                token = CreateToken(ch.ToString() + (char)input.Read());
                return true;
            }
            if (IsOperator(ch.ToString()))
            {
                token = CreateToken(ch);
                return true;
            }
            token = null;
            return false;
        }

        private static string[] _operators = { "+", "-", "*", "/", "=", "==", "!=", "<", "<=", ">", ">=", "&&", "||" };
        private bool IsOperator(string str)
        {
            return _operators.Contains(str);
        }

        private Token DateToken(TextReader input)
        {
            var token = CreateToken("date");
            var stringBuilder = new StringBuilder();
            while (input.Peek() != -1)
            {
                var _ch = (char)input.Read();
                if (_ch == '\'')
                {
                    token.Value = stringBuilder.ToString();
                    return token;
                }
                stringBuilder.Append(_ch);
            }
            Errors.Add(new LexError { Token = token, Message = "String not closed - Unexpected end of file" });
            token.Value = stringBuilder.ToString();
            return token;
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