using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace operateHttp
{
    /// <summary>
    /// 用于处理Http请求
    /// </summary>
    public static class HttpHelper
    {
        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            string content = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = reader.ReadToEnd();
            }
            return content;

        }

        public static string Post(string url, string postData)
        {
            Stream outStream = null;
            Stream inStream = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            Encoding encoding = Encoding.UTF8;

            byte[] bytes = encoding.GetBytes(postData);

            try
            {
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                outStream = request.GetRequestStream();
                outStream.Write(bytes, 0, bytes.Length);
                outStream.Close();
                response = request.GetResponse() as HttpWebResponse;
                inStream = response.GetResponseStream();
                reader = new StreamReader(inStream, encoding);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception ex)
            {
                throw new DxnBusinessException(ex.Message);
            }
        }
    } 
}