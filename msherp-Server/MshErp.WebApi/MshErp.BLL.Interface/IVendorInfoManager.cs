using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.BLL.Interface
{
    public interface IVendorInfoManager:IDependency
    {
        List<VendorResponseBody> QueryVendorList(VendorRquestDTO request);

        string VerifyVendor(int VendorSysNo);
    }
}
