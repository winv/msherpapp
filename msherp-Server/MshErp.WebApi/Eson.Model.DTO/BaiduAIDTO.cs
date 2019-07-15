using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.Model.DTO
{
    public class BaiduAIRquestDTO:BaseDTO
    {
        public BaiduAIRequestBody ReqBody { get; set; }
    }
    public class BaiduAIResponseDTO : BaseDTO
    {
        public BaiAIResponseBody ReqBody { get; set; }
    }
    
    public class BaiduAIRequestBody
    {
        public string ApiType { get; set; }
        public string Url { get; set; }
        public string Imgbase64 { get; set; }
    }
    public class BaiAIResponseBody
    {
       public dynamic Result { get; set; }
    }
}