﻿namespace CalcCal.Domain.Shared;

public static class StringFormatter
{
    public static string CapitalizeFirstLetter(this string value)
    {
        return string.IsNullOrEmpty(value) 
            ? value 
            : string.Concat(char.ToUpper(value[0]), value.ToLower().Skip(1));
    }
}