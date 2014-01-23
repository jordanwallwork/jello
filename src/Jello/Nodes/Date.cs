using System;
using Jello.DataSources;
using Jello.Errors;

namespace Jello.Nodes
{
    public class Date : TerminalNode<Date>
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

        public override object GetValue(IDataSource dataSource)
        {
            return Value;
        }

        public override ValueType Type(IDataSource dataSource) { return ValueType.Date; }
    }
}