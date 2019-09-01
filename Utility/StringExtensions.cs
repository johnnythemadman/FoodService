using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s) => s == null || s.Length == 0;

    }
}
