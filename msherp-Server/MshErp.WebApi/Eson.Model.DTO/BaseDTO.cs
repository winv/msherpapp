using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.Model.DTO
{
    public class BaseDTO
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public int TimeSpan { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string WeiXinOpenID { get; set; }
        public string RetrunMsg { get; set; }
        public string UserSysNo { get; set; }
    }
}
