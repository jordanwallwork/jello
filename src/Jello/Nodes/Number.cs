using System;
using Jello.Errors;

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
    public class Date : Node<Date>
    {
        public DateTime Value { get; set; }

        protected override Date ParseNode()
        {
            object dateVal;
            if (ExpectToken("date", out dateVal))
            {
                DateTime date;
                if (Jello.Settings.DateParser.TryParse(dateVal.ToString(), out date))
                {
                    Value = date;
                }
                else
                {
                    Errors.Add(new ParseError("Invalid date: " + dateVal, Lexer.LineNo, Lexer.Col));
                }
            }
            return this;
        }
    }
}