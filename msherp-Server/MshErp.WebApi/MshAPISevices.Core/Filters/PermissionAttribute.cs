using MshErp.Infrastructure;
using MshErp.Infrastructure.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static Eson.Objects.AppEnum;

namespace MshErp.APIServices.Core.Filters
{
    public class PermissionAttribute: AuthorizationFilterAttribute
    {
        private Privilege _Privilege;
        public PermissionAttribute(Privilege privilege)
        {
            this._Privilege = privilege;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;
            //获取Header
            var plHeader = headers.FirstOrDefault(a => a.Key.ToLower().Equals(Constants.PrivilegeAL));
            List<int> al = JsonConvert.DeserializeObject<List<int>>(plHeader.Value.FirstOrDefault());
            if (al.Contains((int)Privilege.Administrator))
                return;
            if (al.Contains((int)this._Privilege))
                return;
            throw new Exception("权限不足！");
        }
    }
}