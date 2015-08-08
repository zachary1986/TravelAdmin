using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    /// <summary>
    /// Cookies封装操作类
    /// </summary>
    public class Cookies
    {
        #region 获取Cookie
        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string getCookie(string key, string value = "Value")
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            try
            {
                if (cookie != null)
                {
                    return HttpUtility.UrlDecode(cookie.Values[value]);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 设置Cookie
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="time">过期时间(分钟)</param>
        /// <returns></returns>
        public static bool setCookie(string key, string value, double time)
        {
            try
            {
                string s = HttpUtility.UrlEncode(value);
                HttpCookie cookie = new HttpCookie(key)
                {
                    Expires = DateTime.Now.AddMinutes(time)
                };
                cookie.Values.Add("Value", HttpContext.Current.Server.UrlEncode(s));
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 更新Cookie
        /// <summary>
        /// 更新Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="time">过期时间</param>
        /// <returns></returns>
        public static bool updateCookies(string key, string value, double time)
        {
            bool flag;
            try
            {
                HttpContext.Current.Response.Cookies[key]["Value"] = value;
                flag = setCookie(key, value, time);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }
        public static void SetCookie(string cookiename, string cookievalue)
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string GetCookie(string cookiename)
        {
            //string cvalue = TransForm.ToString(HttpContext.Current.Response.Cookies[cookiename].Value);
            //if (cvalue == "")
            //{
            //    cvalue = TransForm.ToString(HttpContext.Current.Request.Cookies[cookiename].Value);
            //}
            try
            {
                return HttpContext.Current.Request.Cookies[cookiename].Value.ToString();
            }
            catch
            {
                return "";
            }
            //return cvalue;
        }
        #endregion
    }
}
