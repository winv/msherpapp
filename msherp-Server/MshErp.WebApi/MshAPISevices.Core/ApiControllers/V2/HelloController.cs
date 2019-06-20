using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MshErp.APIServices.Core.ApiControllers.V2
{
    public class HelloController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public string Hello()
        {
            return "Hello World";
        }
    }
}
