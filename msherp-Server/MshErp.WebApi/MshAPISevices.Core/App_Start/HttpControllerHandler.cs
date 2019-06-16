using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace MshErp.APIServices.Core.App_Start
{
    public class SessionRouteHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionRouteHandler(RouteData routeData) : base(routeData)
        {

        }
    }

}