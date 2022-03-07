using System;
using System.Collections.Generic;

namespace Core2022.Framework
{
    public static class ObjectExtensions
    {


        public static string AppendDic(this string value, Dictionary<string,string> dic)
        {
            if (dic == null)
            {
                return value;
            }
            value += Environment.NewLine + "    [Tags]" + Environment.NewLine;
            foreach (var item in dic)
            {
                value += $"          {{ Key: { item.Key }, Value: { item.Value } }} { Environment.NewLine }";
            }
            return value;
        }

    }
}
