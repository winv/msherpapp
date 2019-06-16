using MshErp.Infrastructure;
using MshErp.Model.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Http;
using Formatting = Newtonsoft.Json.Formatting;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class WeiXinController : BaseApiController
    {
        [AllowAnonymous]
        [Route("api/WeiXin/GetOpenId")]
        [HttpGet]
        public AjaxResponseInfo GetOpenId(string json_code)
        {
            try
            {
                string serviceAddress = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code";
                serviceAddress = string.Format(serviceAddress, ConfigHelper.WeiXinAppid, ConfigHelper.WeiXinSecret, json_code);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                request.Method = "GET";
                request.ContentType = "text/html;charset=utf-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return new AjaxResponseInfo
                {
                    Data = JObject.Parse(retString),
                    Status = true,
                };
            }
            catch(Exception ex)
            {
                throw new LoginFaildException(ex);
            }

        }
    }
}