using System;

namespace Jello.Utils
{
    public static class ObjectExtensions
    {
        public static bool? AsBool(this object obj)
        {
            var bCast = (bool?)obj;
            if (bCast.HasValue) return bCast;

            bool bParse;
            return bool.TryParse(obj.ToString(), out bParse) ? bParse : (bool?)null;
        }

        public static DateTime? AsDate(this object obj)
        {
            var dCast = (DateTime?)obj;
            if (dCast.HasValue) return dCast;

            DateTime dParse;
            return DateTime.TryParse(obj.ToString(), out dParse) ? dParse : (DateTime?)null;
        }

        public static decimal? AsNumber(this object obj)
        {
            var dCast = (decimal?)obj;
            if (dCast.HasValue) return dCast;

            decimal dParse;
            return decimal.TryParse(obj.ToString(), out dParse) ? dParse : (decimal?)null;
        }

        public static string AsString(this object obj)
        {
            return obj.ToString();
        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
        }
    }
}