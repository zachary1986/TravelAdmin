using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// 时间格式转换处理类
    /// </summary>
    public class TimeParser
    {
        #region 把秒转换成分钟
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }
        #endregion

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 时分秒转换成秒
        /// <summary>
        /// 时分秒转换成秒
        /// </summary>
        public static int TimeToSecond(int hour, int minute, int second)
        {
            TimeSpan ts = new TimeSpan(hour, minute, second);
            return (int)ts.TotalSeconds;
        }
        #endregion

        #region 秒转换为时分秒
        /// <summary>
        /// 秒转换为时分秒
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string SecondToDateTime(int seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, seconds);
            string totalTime = string.Format("{0:00}:{1:00}:{2:00}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            return totalTime;// (int)ts.TotalHours + ":" + ts.Minutes + ":" + ts.Seconds;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region 时间格式化
        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <param name="isFormat"></param>
        /// <returns></returns>
        public static string DateTimeFormat(object obj, string format, bool isFormat)
        {
            string str = string.Empty;
            if (null != obj && PageValidate.IsDateTime(obj.ToString()))
            {
                if (isFormat)
                {
                    str = Convert.ToDateTime(obj).ToString(format);
                }
                else
                {
                    str = obj.ToString();
                }
            }
            return str;
        }
        #endregion

        
        public static DateTime ParseDate(string str,DateTime defValue)
        {
            DateTime tmpDate;

            if (!DateTime.TryParse(str, out tmpDate))
            {
                return defValue;
            }
            else
            {
                return tmpDate;
            }
        
        }

    }
}
