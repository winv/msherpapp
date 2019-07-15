using MshErp.Infrastructure;
using MshErp.Model.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class BaiAIController : ApiController
    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static string TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "B1nuSVlhXgP9A5GyeRtV6Yo6";
        // 百度云中开通对应服务应用的 Secret Key
        private static string clientSecret = "RQpS0GcrkY14WYmqyrCFH7ZIhila7gbg";

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }
        [AllowAnonymous]
        [HttpPost]
        public AjaxResponseInfo GetImageInfo([FromBody]BaiduAIRquestDTO request)
        {
            try
            {
                var client = new Baidu.Aip.ImageClassify.ImageClassify(ConfigHelper.BaiduApiKey, ConfigHelper.BaiduSecretKey)
                {
                    Timeout = 60000  // 修改超时时间
                };
                byte[] image = Convert.FromBase64String(request.ReqBody.Imgbase64);

                //// 调用通用物体识别，可能会抛出网络等异常，请使用try/catch捕获
                //var result = client.AdvancedGeneral(image);
                //Console.WriteLine(result);
                //// 如果有可选参数
                //var options = new Dictionary<string, object>{
                //        {"baike_num", 5}
                //    };
                //// 带参数调用通用物体识别
                var result = client.AdvancedGeneral(image);
                var result2 = client.Ingredient(image);
                var res = new { result, result2 };
                return new AjaxResponseInfo
                {
                    Status = true,
                    Data = res,
                };
            }
            catch (Exception ex)
            {
                return new AjaxResponseInfo
                {
                    Data = ex,
                    Status = false,
                };
            }
        }
    }
}
