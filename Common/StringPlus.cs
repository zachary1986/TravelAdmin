using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
namespace Common
{
    /// <summary>
    /// String字符串扩展类，增加了很多扩展功能
    /// </summary>
    public class StringPlus
    {

        public static string GetMyVcode()
        {
            return "122";
        }


        #region 获取字符串分割后的字符集合
        /// <summary>
        /// 获取字符串分割后的字符集合
        /// </summary>
        /// <param name="str"></param>
        /// <param name="speater"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }
        #endregion

        #region 获取用逗号分割后的字符数组
        /// <summary>
        /// 获取用逗号分割后字符数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] GetStrArray(string str)
        {
            return str.Split(new Char[] { ',' });
        }
        #endregion

        #region 获取集合中的字符串
        /// <summary>
        /// 获取集合中的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 得到数组列表以逗号分隔的字符串
        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayStr(List<int> list)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i].ToString());
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 得到数组列表以逗号分隔的字符串
        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayValueStr(Dictionary<int, int> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, int> kvp in list)
            {
                sb.Append(kvp.Value + ",");
            }
            if (list.Count > 0)
            {
                return DelLastComma(sb.ToString());
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 删除最后一个字符之后的字符

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion

        #region 转全角的函数(SBC case)
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion

        #region 转半角的函数(SBC case)
        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region  得到子字符串集合

        /// <summary>
        /// 得到子字符串集合
        /// </summary>
        /// <param name="o_str"></param>
        /// <param name="sepeater"></param>
        /// <returns></returns>
        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            string[] ss = o_str.Split(sepeater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                {
                    list.Add(s);
                }
            }
            return list;
        }
        #endregion

        #region 将字符串样式转换为纯字符串
        public static string GetCleanStyle(string StrList, string SplitString)
        {
            string RetrunValue = "";
            //如果为空，返回空值
            if (StrList == null)
            {
                RetrunValue = "";
            }
            else
            {
                //返回去掉分隔符
                string NewString = "";
                NewString = StrList.Replace(SplitString, "");
                RetrunValue = NewString;
            }
            return RetrunValue;
        }
        #endregion

        #region 将字符串转换为新样式
        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string ReturnValue = "";
            //如果输入空值，返回空，并给出错误提示
            if (StrList == null)
            {
                ReturnValue = "";
                Error = "请输入需要划分格式的字符串";
            }
            else
            {
                //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
                int strListLength = StrList.Length;
                int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
                if (strListLength != NewStyleLength)
                {
                    ReturnValue = "";
                    Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                }
                else
                {
                    //检查新样式中分隔符的位置
                    string Lengstr = "";
                    for (int i = 0; i < NewStyle.Length; i++)
                    {
                        if (NewStyle.Substring(i, 1) == SplitString)
                        {
                            Lengstr = Lengstr + "," + i;
                        }
                    }
                    if (Lengstr != "")
                    {
                        Lengstr = Lengstr.Substring(1);
                    }
                    //将分隔符放在新样式中的位置
                    string[] str = Lengstr.Split(',');
                    foreach (string bb in str)
                    {
                        StrList = StrList.Insert(int.Parse(bb), SplitString);
                    }
                    //给出最后的结果
                    ReturnValue = StrList;
                    //因为是正常的输出，没有错误
                    Error = "";
                }
            }
            return ReturnValue;
        }
        #endregion

        #region 分割字符串
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitstr"></param>
        /// <returns></returns>
        public static string[] SplitMulti(string str, string splitstr)
        {
            string[] strArray = null;
            if ((str != null) && (str != ""))
            {
                strArray = new Regex(splitstr).Split(str);
            }
            return strArray;
        }
        #endregion

        #region 替换SQL字符
        /// <summary>
        /// 替换SQL字符
        /// </summary>
        /// <param name="String"></param>
        /// <param name="IsDel"></param>
        /// <returns></returns>
        public static string SqlSafeString(string String, bool IsDel)
        {
            if (IsDel)
            {
                String = String.Replace("'", "");
                String = String.Replace("\"", "");
                return String;
            }
            String = String.Replace("'", "&#39;");
            String = String.Replace("\"", "&#34;");
            return String;
        }
        #endregion

        #region 从 srcString 的开头剔除掉 trimString
        /// <summary>
        /// 从 srcString 的开头剔除掉 trimString
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static string TrimStart(String srcString, String trimString)
        {
            if (srcString == null) return null;
            if (trimString == null) return srcString;
            if (IsNullOrEmpty(srcString)) return String.Empty;
            if (srcString.StartsWith(trimString) == false) return srcString;
            return srcString.Substring(trimString.Length);
        }
        #endregion

        #region 从 srcString 的末尾剔除掉 trimString
        /// <summary>
        /// 从 srcString 的末尾剔除掉 trimString
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static string TrimEnd(String srcString, String trimString)
        {
            if (IsNullOrEmpty(trimString)) return srcString;
            if (srcString.EndsWith(trimString) == false) return srcString;
            if (srcString.Equals(trimString)) return "";
            return srcString.Substring(0, srcString.Length - trimString.Length);
        }
        #endregion

        #region 截取字符串
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="target">内容</param>
        /// <param name="sign">替换符号</param>
        /// <param name="subLength">截取长度</param>
        /// <param name="isShow">是否显示替换符号</param>
        /// <returns></returns>
        [Obsolete]
        public static string SubString(object target, string sign, int subLength, bool isShow)
        {
            string str = string.Empty;
            if (!IsNullOrEmpty(target))
            {
                str = target.ToString();
                if (str.Length > subLength)
                {
                    str = str.Substring(0, subLength);
                    if (isShow)
                    {
                        str = str + sign;
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="target">内容</param>
        /// <param name="subLength">截取长度</param>
        /// <param name="sign">替换符号</param>
        /// <returns></returns>
        public static string SubString(object target, int subLength, string sign = null)
        {
            string str = string.Empty;
            if (!IsNullOrEmpty(target))
            {
                str = target.ToString();
                if (str.Length > subLength)
                {
                    if (!string.IsNullOrWhiteSpace(sign))
                    {
                        //截取时, 包含占位符号长度, 以保证页面不变形
                        str = str.Substring(0, subLength - sign.Length / 2);
                        str = str + sign;
                    }
                    else
                    {
                        str = str.Substring(0, subLength);
                    }
                }
            }
            return str;
        }
        #endregion

        #region 检查字符串是否是 null 或者空白字符,不同于.net自带的string.IsNullOrEmpty，多个空格在这里也返回true。
        /// <summary>
        /// 检查字符串是否是 null 或者空白字符,不同于.net自带的string.IsNullOrEmpty，多个空格在这里也返回true。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object target)
        {
            if (null != target && "" != target.ToString())
            {
                return target.ToString().Trim().Length == 0;
            }
            return true;
        }
        #endregion

        #region 分割字符串-返回Size对象
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">原字符</param>
        /// <param name="splitstr">分隔符</param>
        /// <param name="defWidth">默认宽度</param>
        /// <param name="defHeight">默认高度</param>
        /// <returns>Size对象</returns>
        public static Size SplitToSize(string str, char splitstr, int defWidth, int defHeight)
        {
            int width = defWidth;
            int height = defHeight;
            if (!string.IsNullOrWhiteSpace(str))
            {
                string[] tmpNormalSize = str.Split(new char[] { splitstr },
                                                                     StringSplitOptions.RemoveEmptyEntries);
                if (tmpNormalSize.Length == 2)
                {
                    width = Globals.SafeInt(tmpNormalSize[0], defWidth);
                    height = Globals.SafeInt(tmpNormalSize[1], defHeight);
                }
            }
            return new Size(width, height);
        }

        #endregion
    }
}
