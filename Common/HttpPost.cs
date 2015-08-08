using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Common
{
    public class HttpPost
    {
        public static string PostUrl(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            byte[] data = Encoding.UTF8.GetBytes(postDataStr);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = reader.ReadToEnd();
            reader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}
