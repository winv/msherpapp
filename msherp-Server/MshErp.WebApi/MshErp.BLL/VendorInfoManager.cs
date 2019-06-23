using Eson.BLL.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.BLL.Interface
{
    public class VendorInfoManager : IVendorInfoManager
    {
        public void QueryVendorList()
        {
            VendorManager.GetInstance().GetVendorDs(null);
        }
    }
}
