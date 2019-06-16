using Microsoft.Owin.Hosting;
using MshErp.APIServices.Core.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MshErp.APIServices.Core
{
    public class MshAPIServiceManage
    {
        private static IDisposable Service;

        public static bool SelfHostStart(string url)
        {
            try
            {
                //Logger.Info($"管理端后台站点地址为:{url}");
                Service = WebApp.Start<Startup>(url);
                return true;
            }
            catch (Exception ex)
            {
                MshErp.Infrastructure.Logger.Error($"启动WebApi服务失败:{ex}");
                return false;
            }
        }

        public static void SelfHostStop()
        {
            Service.Dispose();
        }

        public static bool WebHostStart()
        {
            try
            {
                GlobalConfiguration.Configure(config => MshAPIConfig.Register(config));
                GlobalConfiguration.Configure(config => AutofacConfig.Register(config, true));
                //GlobalConfiguration.Configure(config => SwaggerConfig.Register());
                //MapperConfig.Initial();
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
                MshErp.Infrastructure.Logger.Info($"服务启动失败:{ex}");
                return false;
            }
        }

        public static void WebHostStop()
        {
        }
    }
}
