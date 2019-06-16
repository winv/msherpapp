using Microsoft.Owin;
using Microsoft.Owin.Cors;
using MshErp.APIServices.Core.App_Start;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(MshErp.APIServices.Core.Startup))]
namespace MshErp.APIServices.Core
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            MshErp.Infrastructure.Logger.Info("启动配置！");
            HttpConfiguration config = new HttpConfiguration();

            //跨域配置
            app.UseCors(CorsOptions.AllowAll);

            MshAPIConfig.Register(config);
            AutofacConfig.Register(config,true);
            //SwaggerConfig.Register();

            app.UseWebApi(config);

            //MapperConfig.Initial();
        }
    }
}
