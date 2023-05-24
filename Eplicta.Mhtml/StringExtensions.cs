﻿using System;
using System.Collections.Generic;

namespace Eplicta.Mhtml;

public static class StringExtensions
{
    public static IEnumerable<string> SplitByLength(this string str, int maxLength)
    {
        for (var index = 0; index < str.Length; index += maxLength)
        {
            yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
        }
    }
}