using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public sealed class Globals
    {
        /// <summary>
        /// Admin后台SessionKey
        /// </summary>
        public static string SESSIONKEY_ADMIN = "Admin_UserInfo";
        /// <summary>
        /// 企业后台SessionKey
        /// </summary>
        public static string SESSIONKEY_ENTERPRISE = "Enterprise_UserInfo";
        /// <summary>
        /// 代理商后台SessionKey
        /// </summary>
        public static string SESSIONKEY_AGENTS = "Agents_UserInfo";
        /// <summary>
        /// 用户后台SessionKey
        /// </summary>
        public static string SESSIONKEY_USER = SESSIONKEY_AGENTS = SESSIONKEY_ENTERPRISE = SESSIONKEY_ADMIN = "UserInfo";

        /// <summary>
        /// 所有后台共享Session
        /// </summary>
        public static bool IsPublicSession
        {
            get { return false; }
        }

        /// <summary>
        /// 站内订单支付密钥
        /// </summary>
        public const string PAY_SECURITY_CODE = "MATICSOFT_SENDING";
        public const string PAY_SECURITY_KEY = "MATICSOFT_SECURITY_CODE";

        /// <summary>
        /// 余额支付接口ID
        /// </summary>
        public const int PAY_BALANCE_PAYMENTMODEID = -2;

        /// <summary>
        /// 积分比例
        /// </summary>
        public static decimal POINT_RATIO = 1;

        private Globals()
        {
        }

        public static string AppendQuerystring(string url, string querystring)
        {
            return AppendQuerystring(url, querystring, false).Trim();
        }

        public static string AppendQuerystring(string url, string querystring, bool urlEncoded)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            string str = "?";
            if (url.IndexOf('?') > -1)
            {
                if (!urlEncoded)
                {
                    str = "&";
                }
                else
                {
                    str = "&amp;";
                }
            }
            return (url + str + querystring);
        }

        public static string FullPath(string local)
        {
            if (string.IsNullOrEmpty(local))
            {
                return local;
            }
            if (local.ToLower(CultureInfo.InvariantCulture).StartsWith("http://"))
            {
                return local;
            }
            if (HttpContext.Current == null)
            {
                return local;
            }
            return FullPath(HostPath(HttpContext.Current.Request.Url), local);
        }

        public static string FullPath(string hostPath, string local)
        {
            return (hostPath + local);
        }

        public static string HostPath(Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }
            string str = (uri.Port == 80) ? string.Empty : (":" + uri.Port.ToString(CultureInfo.InvariantCulture));
            return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[] { uri.Scheme, uri.Host, str });
        }

        public static string HtmlDecode(object target)
        {
            if (StringPlus.IsNullOrEmpty(target))
            {
                return "";
            }
            return HttpUtility.HtmlDecode(target.ToString().Trim());
        }

        public static string HtmlEncode(object target)
        {
            if (StringPlus.IsNullOrEmpty(target))
            {
                return "";
            }
            return HttpUtility.HtmlEncode(target.ToString().Trim());
        }

        public static void RedirectToSSL(HttpContext context)
        {
            if ((context != null) && !context.Request.IsSecureConnection)
            {
                Uri url = context.Request.Url;
                context.Response.Redirect("https://" + url.ToString().Substring(7));
            }
        }

        public static bool SafeBool(string text, bool defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        public static DateTime SafeDateTime(string text, DateTime defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        public static decimal SafeDecimal(string text, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static short SafeShort(string text, short defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static int SafeInt(string text, int defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static long SafeLong(string text, long defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static string SafeString(object target, string defaultValue)
        {
            if (null != target && "" != target.ToString())
            {
                return target.ToString();
            }
            return defaultValue;
        }

        /// <summary>
        /// 将枚举数值or枚举名称 安全转换为枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">数值or名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <remarks>转换区分大小写</remarks>
        /// <returns></returns>
        public static T SafeEnum<T>(string value, T defaultValue) where T : struct
        {
            return Globals.SafeEnum<T>(value, defaultValue, false);
        }

        /// <summary>
        /// 将枚举数值or枚举名称 安全转换为枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">数值or名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="ignoreCase">是否忽略大小写 true 不区分大小写 | false 区分大小写</param>
        /// <returns></returns>
        public static T SafeEnum<T>(string value, T defaultValue, bool ignoreCase) where T : struct
        {
            T result;
            if (Enum.TryParse<T>(value, ignoreCase, out result))
            {
                if (Enum.IsDefined(typeof(T), result))
                {
                    defaultValue = result;
                }
            }
            return defaultValue;
        }

        public static string StripAllTags(string strToStrip)
        {
            strToStrip = Regex.Replace(strToStrip, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", "\n\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, @"<br(?:\s*)/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, "\"", "''", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = StripHtmlXmlTags(strToStrip);
            return strToStrip;
        }

        public static string StripForPreview(string content)
        {
            content = Regex.Replace(content, "<br>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br />", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<p>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = content.Replace("'", "&#39;");
            return StripHtmlXmlTags(content);
        }

        public static string HtmlEncodeForSpaceWrap(string content)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            return HttpUtility.HtmlEncode(content).Replace(" ", "&nbsp;").Replace("\n", "<br />");
        }

        public static string HtmlDecodeForSpaceWrap(string content)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            return HttpUtility.HtmlDecode(content).Replace("<br />", "\n").Replace("&nbsp;", " ");
        }

        public static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string StripScriptTags(string content)
        {
            content = Regex.Replace(content, "<script((.|\n)*?)</script>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "'javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return Regex.Replace(content, "\"javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        public static string ToDelimitedString(ICollection collection, string delimiter)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            if (collection is Hashtable)
            {
                foreach (object obj2 in ((Hashtable)collection).Keys)
                {
                    builder.Append(obj2.ToString() + delimiter);
                }
            }
            if (collection is ArrayList)
            {
                foreach (object obj3 in (ArrayList)collection)
                {
                    builder.Append(obj3.ToString() + delimiter);
                }
            }
            if (collection is string[])
            {
                foreach (string str in (string[])collection)
                {
                    builder.Append(str + delimiter);
                }
            }
            if (collection is MailAddressCollection)
            {
                foreach (MailAddress address in (MailAddressCollection)collection)
                {
                    builder.Append(address.Address + delimiter);
                }
            }
            return builder.ToString().TrimEnd(new char[] { Convert.ToChar(delimiter, CultureInfo.InvariantCulture) });
        }

        public static string UnHtmlEncode(string formattedPost)
        {
            RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase;
            formattedPost = Regex.Replace(formattedPost, "&quot;", "\"", options);
            formattedPost = Regex.Replace(formattedPost, "&lt;", "<", options);
            formattedPost = Regex.Replace(formattedPost, "&gt;", ">", options);
            return formattedPost;
        }

        public static string UrlDecode(string urlToDecode)
        {
            if (string.IsNullOrEmpty(urlToDecode))
            {
                return urlToDecode;
            }
            return HttpUtility.UrlDecode(urlToDecode, Encoding.UTF8);
        }

        public static string UrlEncode(string urlToEncode)
        {
            if (string.IsNullOrEmpty(urlToEncode))
            {
                return urlToEncode;
            }
            return HttpUtility.UrlEncode(urlToEncode, Encoding.UTF8);
        }

        public static string ApplicationPath
        {
            get
            {
                string applicationPath = "/";
                if (HttpContext.Current != null)
                {
                    applicationPath = HttpContext.Current.Request.ApplicationPath;
                }
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                return applicationPath.ToLower(CultureInfo.InvariantCulture);
            }
        }
        //public static void AddLog(string type, string operating, string remark,int? uid=null)
        //{
        //    Users_OperatorLog log = new Users_OperatorLog();
        //    log.addip = getIPAddress();
        //    log.addtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
        //    log.type = type;
        //    log.Remark = remark;
        //    log.user_id = uid;
        //    log.operating = operating;
        //    HuRongClub.DAL.Users_OperatorLog dal = new DAL.Users_OperatorLog();
        //    dal.Add(log);
        //}
        #region
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string getIPAddress()
        {
            string result = String.Empty;

            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            // 如果使用代理，获取真实IP 
            if (result != null && result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                result = null;
            else if (result != null)
            {
                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。 
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];    //找到不是内网的地址 
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式 
                    return result;
                else
                    result = null;    //代理中的内容 非IP，取IP 
            }
            if (null == result || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.UserHostAddress;

            return result;
        }
        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        private static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
        #endregion

        /// <summary>
        /// 获取此实例的主机部分
        /// </summary>
        /// <remarks>此属性值不包括端口号</remarks>
        public static string DomainName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Request.Url.Host;
            }
        }
        /// <summary>
        /// 获取服务器的域名系统 (DNS) 主机名或 IP 地址和端口号
        /// </summary>
        public static string DomainFullName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Request.Url.Authority;
            }
        }

        public static string GenRandomCodeFor6()
        {
            //生成6位随机数验证码
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            return ran.Next(000000, 999999).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// DataTable分页
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">页索引,注意：从1开始</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns>分好页的DataTable数据</returns>
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) { return dt; }
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            { return newdt; }

            if (rowend > dt.Rows.Count)
            { rowend = dt.Rows.Count; }
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }

        /// <summary>
        /// 返回分页的页数
        /// </summary>
        /// <param name="count">总条数</param>
        /// <param name="pageye">每页显示多少条</param>
        /// <returns>如果 结尾为0：则返回1</returns>
        public static int PageCount(int count, int pageye)
        {
            int page = 0;
            int sesepage = pageye;
            if (count % sesepage == 0) { page = count / sesepage; }
            else { page = (count / sesepage) + 1; }
            if (page == 0) { page += 1; }
            return page;
        }
    }
}
