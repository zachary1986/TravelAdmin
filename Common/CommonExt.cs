
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Common
{
    public static class CommonExt
    {
        #region string is null or empty
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        #endregion

        #region json convert
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static T ToObject<T>(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        #endregion

        #region get current request site url
        public static string SiteUrl(this HttpRequestBase request)
        {
            var uri = request.Url;
            var revalue = uri.Scheme + Uri.SchemeDelimiter + uri.Host + (uri.Port == 80 ? "" : ":" + uri.Port);
            return revalue;
        }
        public static string SiteUrl(this HttpRequest request)
        {
            var uri = request.Url;
            var revalue = uri.Scheme + Uri.SchemeDelimiter + uri.Host + (uri.Port == 80 ? "" : ":" + uri.Port);
            return revalue;
        }
        #endregion

        public static string Description(this System.Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            if (fieldInfo == null)
            {
                return "";
            }
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            else
            {
                description = value.ToString();//如果没有设置Description返回英文
            }
            return description;
        }

       
    }
}
