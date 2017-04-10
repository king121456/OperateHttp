using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace operateHttp
{
    /// <summary>
    /// api 的摘要说明
    /// </summary>
    public class api : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            string content = HttpHelper.Get(url);
            context.Response.Write(content);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}