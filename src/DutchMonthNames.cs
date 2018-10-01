using System;

namespace PipView
{
    static class DutchMonthNames
    {
        internal static string[] MonthNames = { "januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december" };

        public static string GetMonthName(int monthNumber)
        {
            return MonthNames[monthNumber];
        }

        public static int GetMonthNumber(string monthName)
        {
            return Array.IndexOf(MonthNames, monthName);
        }
    }
}
