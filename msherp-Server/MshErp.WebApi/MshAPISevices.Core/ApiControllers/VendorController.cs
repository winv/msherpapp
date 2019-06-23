using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class VendorController : BaseApiController
    {
        public IVendorInfoManager IVendorInfoManager { get; set; }
        public AjaxResponseInfo QueryVendorList()
        {
            //IVendorInfoManager
            return new AjaxResponseInfo
            {
                Status = false,
                Data = null
            };
        }
    }
}
