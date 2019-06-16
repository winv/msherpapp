using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.Model.DTO
{
    public class AjaxResponseInfo
    {
        public AjaxResponseInfo()
        {
            Status = true;
        }

        public AjaxResponseInfo(string msg)
            : this()
        {
            Status = false;
            Message = msg;
        }

        /// <summary>
        /// 成功表示True 失败False
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public dynamic Data { get; set; }
    }
}
