﻿using MshErp.APIServices.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;

namespace MshErp.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public override void Init()
        {
            this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            base.Init();
        }
        protected void Application_Start()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            MshAPIServiceManage.WebHostStart();
        }

        protected void Application_End()
        {
            MshAPIServiceManage.WebHostStop();
        }
    }
}
