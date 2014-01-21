using System;
using System.Globalization;

namespace Jello
{
    public class StandardDateParser : IDateParser
    {
        private readonly IFormatProvider _formatProvider;
        private readonly DateTimeStyles _dateTimeStyles;
        private readonly bool _specifiedFormattingAndStyles;

        public StandardDateParser()
        {
            _specifiedFormattingAndStyles = false;
        }

        public StandardDateParser(IFormatProvider formatProvider, DateTimeStyles dateTimeStyles)
        {
            _formatProvider = formatProvider;
            _dateTimeStyles = dateTimeStyles;
            _specifiedFormattingAndStyles = true;
        }

        public bool TryParse(string dateString, out DateTime date)
        {
            return _specifiedFormattingAndStyles
                            ? DateTime.TryParse(dateString, _formatProvider, _dateTimeStyles, out date)
                            : DateTime.TryParse(dateString, out date);
        }
    }
}