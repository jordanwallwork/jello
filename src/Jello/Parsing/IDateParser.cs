using System;

namespace Jello
{
    public interface IDateParser
    {
        bool TryParse(string dateString, out DateTime date);
    }
}