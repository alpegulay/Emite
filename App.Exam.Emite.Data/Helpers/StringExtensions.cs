using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Helpers
{
    public static class StringExtensions
    {
        public static string CamelCase(this string s)
        {
            var x = s.Replace("_", "");
            if (x.Length == 0) return "Null";
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToLower(x[0]) + x.Substring(1);
        }

        public static bool IgnoreCaseEquals(this string s, string a)
        {
            return string.Equals(s, a, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IgnoreCaseContains(this string s, string a)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            return s.Contains(a, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
