using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Helper
/// </summary>
public static class Helper
{
    //    public Helper()
    //    {
    //        //
    //        // TODO: Add constructor logic here
    //        //
    //    }

    public static string GetLast(this string source, int tail_length)
    {
        if (tail_length >= source.Length)
            return source;
        return source.Substring(source.Length - tail_length);
    }
}