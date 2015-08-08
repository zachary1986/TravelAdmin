using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Net;
using System.IO;

namespace Common
{
    public class SmsSender
    {
        //private readonly HuRongClub.DAL.SA_Config_System dal = new HuRongClub.DAL.SA_Config_System();


        public static void Send(string smsbody, string phone)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string configPath = HttpContext.Current.Request.MapPath("/Config/SmsSetting.config");
                doc.Load(configPath);

                smsbody += doc.DocumentElement.SelectSingleNode("signname").InnerText;
                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = "";
                byte[] data = encoding.GetBytes(postData);
                var url = doc.DocumentElement.SelectSingleNode("posturl").InnerText;
                url += "&userid=" + doc.DocumentElement.SelectSingleNode("userid").InnerText;
                url += "&account=" + doc.DocumentElement.SelectSingleNode("account").InnerText;
                url += "&password=" + doc.DocumentElement.SelectSingleNode("password").InnerText;
                url += "&mobile=" + phone + "";
                url += "&content=" + smsbody + "";
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";

                myRequest.ContentType = "application/json; charset=UTF-8";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string rcontent = reader.ReadToEnd();
                reader.Close();

                XmlDocument docreturn = new XmlDocument();
                docreturn.LoadXml(rcontent);
                string returnstatus = docreturn.SelectSingleNode("returnsms/returnstatus").InnerText;

                if (returnstatus == "Success" || returnstatus.ToLower() == "success")
                {
                    //Globals.AddLog("短信发送", "获取验证码", "发送成功,接收者:" + phone + "");
                }
                else
                {
                    //Globals.AddLog("短信发送", "获取验证码", "发送失败" + docreturn.SelectSingleNode("returnsms/message").InnerText + ",接收者:" + phone + "");
                }
            }
            catch (Exception ex)
            {
                //Globals.AddLog("短信发送", "获取验证码", "发送失败" + ex.Message + ",接收者:" + phone + "");
            }
        }
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="Template">模板</param>
        /// <param name="args">内容</param>
        public static void SendValidCode(string phone, string Template, string[] args)
        {
            try
            {
                //判断单个手机发送次数
                object obj = DataCache.GetCache("cachePhone" + phone);
                int phoneCount = 0;
                if (obj != null)
                {
                    phoneCount = Convert.ToInt32(obj);
                }
                else
                {
                    DataCache.SetCache("cachePhone" + phone, phoneCount, DateTime.UtcNow.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                //判断同一个IP当天发送次数
                object obj1 = DataCache.GetCache("cacheIp" + Globals.getIPAddress());
                int IpCount = 0;
                if (obj1 != null)
                {
                    IpCount = Convert.ToInt32(obj1);
                }
                else
                {
                    DataCache.SetCache("cacheIp" + Globals.getIPAddress(), IpCount, DateTime.UtcNow.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                if (IpCount < 100 && phoneCount < 10)
                {
                    DataCache.SetCache("cachePhone" + phone, (phoneCount + 1));
                    DataCache.SetCache("cacheIp" + Globals.getIPAddress(), (IpCount + 1));

                    XmlDocument doc = new XmlDocument();
                    string configPath = HttpContext.Current.Request.MapPath("/Config/SmsSetting1.config");
                    doc.Load(configPath);
                    string smsbody = "";

                    if (Template == "{0}是您的验证码，将在{1}分钟后失效。如非本人操作请忽略或致电4001-000-381。")
                    {
                        Random ran = new Random();
                        int RandKey = ran.Next(10000, 99999);
                        smsbody = string.Format(Template,RandKey.ToString() ,"5");
                        HttpContext.Current.Session["phonetime"] = DateTime.Now;
                        HttpContext.Current.Session["phoneyzm"] = RandKey;
                    }
                    else if (Template=="验证码:{0},不要告诉别人哦。您正在使用尾号{1}银行卡进行提现如非本人操作请立即致电客服4001-000-381")
                    {
                        Random ran = new Random();
                        int RandKey = ran.Next(10000, 99999);
                        smsbody = string.Format(Template,RandKey.ToString() ,args[0]);
                        HttpContext.Current.Session["phonetime"] = DateTime.Now;
                        HttpContext.Current.Session["phoneyzm"] = RandKey;
                    }
                    else
                    {
                        smsbody = string.Format(Template, args);
                    }


                    Encoding enc = Encoding.GetEncoding("GBK");
                    string spcode = doc.DocumentElement.SelectSingleNode("SpCode").InnerText;
                    string LoginName = doc.DocumentElement.SelectSingleNode("LoginName").InnerText;
                    string Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;


                    string param = HttpUtility.UrlEncode("SpCode", enc) + "=" + HttpUtility.UrlEncode(spcode, enc)
                        + "&" + HttpUtility.UrlEncode("LoginName", enc) + "=" + HttpUtility.UrlEncode(LoginName, enc)
                        + "&" + HttpUtility.UrlEncode("Password", enc) + "=" + HttpUtility.UrlEncode(Password, enc)
                        + "&" + HttpUtility.UrlEncode("MessageContent", enc) + "=" + HttpUtility.UrlEncode(smsbody, enc)
                        + "&" + HttpUtility.UrlEncode("UserNumber", enc) + "=" + HttpUtility.UrlEncode(phone, enc)
                        + "&" + HttpUtility.UrlEncode("SerialNumber", enc) + "=" + HttpUtility.UrlEncode(DateTime.Now.ToString("yyyyMMddhhmmssffffff"), enc)
                        + "&ScheduleTime=&ExtendAccessNum=&f=1";


                    var url = doc.DocumentElement.SelectSingleNode("posturl").InnerText;
                    byte[] data = Encoding.ASCII.GetBytes(param);

                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url + "?" + param);
                    myRequest.Method = "POST";

                    myRequest.ContentType = "application/json; charset=gbk";
                    myRequest.ContentLength = data.Length;
                    Stream newStream = myRequest.GetRequestStream();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();

                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
                    string rcontent = reader.ReadToEnd();
                    reader.Close();

                    string docreturn = rcontent;
                    string reStatus = docreturn.Split('&')[0].Remove(0, 7);
                    string reDescription = docreturn.Split('&')[1].Remove(0, 12);

                    if (reStatus == "0")
                    {
                        //Globals.AddLog("短信发送", "获取验证码", "发送成功,接收者:" + phone + " 短信内容：" + smsbody);
                    }
                    else
                    {
                        //Globals.AddLog("短信发送", "获取验证码", "发送失败" + reDescription + ",接收者:" + phone + " 短信内容：" + smsbody);
                    }
                }
                else
                {
                    if (IpCount >= 100)
                    {
                    //    Globals.AddLog("短信发送", "获取验证码", "发送失败" + Globals.getIPAddress() + "已发送验证码一百次" + ",接收者:" + phone + "");
                    }
                    if (phoneCount >= 10)
                    {
                        //Globals.AddLog("短信发送", "获取验证码", "发送失败" + phone + "已发送验证码十次" + ",接收者:" + phone + "");
                    }
                }
            }
            catch (Exception ex)
            {
                //Globals.AddLog("短信发送", "获取验证码", "发送失败" + ex.Message + ",接收者:" + phone + "");
            }
        }

        public static void SendInviteCode(string phone, string content)
        {
            try
            {
                //判断单个手机发送次数
                object obj = DataCache.GetCache("cachePhone" + phone);
                int phoneCount = 0;
                if (obj != null)
                {
                    phoneCount = Convert.ToInt32(obj);
                }
                else
                {
                    DataCache.SetCache("cachePhone" + phone, phoneCount, DateTime.UtcNow.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                //判断同一个IP当天发送次数
                object obj1 = DataCache.GetCache("cacheIp" + Globals.getIPAddress());
                int IpCount = 0;
                if (obj1 != null)
                {
                    IpCount = Convert.ToInt32(obj1);
                }
                else
                {
                    DataCache.SetCache("cacheIp" + Globals.getIPAddress(), IpCount, DateTime.UtcNow.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                if (IpCount < 100 && phoneCount < 10)
                {
                    DataCache.SetCache("cachePhone" + phone, (phoneCount + 1));
                    DataCache.SetCache("cacheIp" + Globals.getIPAddress(), (IpCount + 1));

                    XmlDocument doc = new XmlDocument();
                    string configPath = HttpContext.Current.Request.MapPath("/Config/SmsSetting1.config");
                    doc.Load(configPath);

                    //string smsbody = "您正在互融CLUB进行注册，您的验证码为" + randkey.ToString() + "如非本人操作请忽略";
                    string smsbody = content;

                    Encoding enc = Encoding.GetEncoding("GBK");
                    string spcode = doc.DocumentElement.SelectSingleNode("SpCode").InnerText;
                    string LoginName = doc.DocumentElement.SelectSingleNode("LoginName").InnerText;
                    string Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;


                    string param = HttpUtility.UrlEncode("SpCode", enc) + "=" + HttpUtility.UrlEncode(spcode, enc)
                        + "&" + HttpUtility.UrlEncode("LoginName", enc) + "=" + HttpUtility.UrlEncode(LoginName, enc)
                        + "&" + HttpUtility.UrlEncode("Password", enc) + "=" + HttpUtility.UrlEncode(Password, enc)
                        + "&" + HttpUtility.UrlEncode("MessageContent", enc) + "=" + HttpUtility.UrlEncode(smsbody, enc)
                        + "&" + HttpUtility.UrlEncode("UserNumber", enc) + "=" + HttpUtility.UrlEncode(phone, enc)
                        + "&" + HttpUtility.UrlEncode("SerialNumber", enc) + "=" + HttpUtility.UrlEncode(DateTime.Now.ToString("yyyyMMddhhmmssffffff"), enc)
                        + "&ScheduleTime=&ExtendAccessNum=&f=1";


                    var url = doc.DocumentElement.SelectSingleNode("posturl").InnerText;
                    byte[] data = Encoding.ASCII.GetBytes(param);

                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url + "?" + param);
                    myRequest.Method = "POST";

                    myRequest.ContentType = "application/json; charset=gbk";
                    myRequest.ContentLength = data.Length;
                    Stream newStream = myRequest.GetRequestStream();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();

                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
                    string rcontent = reader.ReadToEnd();
                    reader.Close();

                    string docreturn = rcontent;
                    string reStatus = docreturn.Split('&')[0].Remove(0, 7);
                    string reDescription = docreturn.Split('&')[1].Remove(0, 12);

                    if (reStatus == "0")
                    {
                        //Globals.AddLog("短信发送", "获取验证码", "发送成功,接收者:" + phone + "");
                    }
                    else
                    {
                        //Globals.AddLog("短信发送", "获取验证码", "发送失败" + reDescription + ",接收者:" + phone + "");
                    }
                }
                else
                {
                    if (IpCount >= 100)
                    {
                        //Globals.AddLog("短信发送", "获取验证码", "发送失败" + Globals.getIPAddress() + "已发送验证码一百次" + ",接收者:" + phone + "");
                    }
                    if (phoneCount >= 10)
                    {
                        //Globals.AddLog("短信发送", "获取验证码", "发送失败" + phone + "已发送验证码十次" + ",接收者:" + phone + "");
                    }
                }
            }
            catch (Exception ex)
            {
                //Globals.AddLog("短信发送", "获取验证码", "发送失败" + ex.Message + ",接收者:" + phone + "");
            }
        }
    }
}
