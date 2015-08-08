using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public class RequestCommon
    {

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip) || ip == "unknown") { ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; }
            if (string.IsNullOrEmpty(ip) || ip == "unknown") { ip = HttpContext.Current.Request.UserHostAddress; }
            if (ip.Contains(",")) { ip = ip.Split(',')[0]; }
            return ip;
        }
    }
}
