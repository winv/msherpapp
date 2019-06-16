using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using MshErp.Infrastructure;
using MshErp.Model.DTO;

namespace MshErp.APIServices.Core.Filters
{
    public class HandleCustomExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            var ex = filterContext.Exception;
            if (ex is LoginFaildException)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new AjaxResponseInfo(ex.Message));
            }
            else if (ex is BaseCustomException)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.NotImplemented, new AjaxResponseInfo(ex.Message));
            }
            else
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.ServiceUnavailable, new AjaxResponseInfo(ex.Message));

            Logger.Error($"请求地址:{filterContext.Request.RequestUri}  发生错误:{ex}");
            base.OnException(filterContext);
        }
    }
}