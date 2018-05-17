using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Service
{
    public static class X12Helper
    {
        public static string SplitX12ForHtml(this string value)
        {
            if (value.Length > 106)
            {
                char segmentSeperator = Convert.ToChar(value.Substring(105, 1));
                return value.Replace(segmentSeperator.ToString(), segmentSeperator.ToString() + "<br />");
            }
            return value;
        }
    }
}
