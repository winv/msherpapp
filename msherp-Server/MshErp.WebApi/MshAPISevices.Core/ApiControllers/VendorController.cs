using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class VendorController : BaseApiController
    {
        public IVendorInfoManager IVendorInfoManager { get; set; }
        public AjaxResponseInfo QueryVendorList([FromBody] VendorRquestDTO request)
        {
            var list = IVendorInfoManager.QueryVendorList(request);
            return new AjaxResponseInfo
            {
                Status = true,
                Data = new VendorResponseDTO() { ResBody = list }
            };
        }

        public AjaxResponseInfo GetVendorVerifyMsg([FromUri] int VendorSysNo)
        {
            var msg = IVendorInfoManager.VerifyVendor(VendorSysNo);
            return new AjaxResponseInfo
            {
                Status = true,
                Data = msg
            };
        }
    }
}
