using Eson.BLL.Basic;
using Eson.Utils;
using MshErp.Model.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.BLL.Interface
{
    public class VendorInfoManager : IVendorInfoManager
    {
        public List<VendorResponseBody> QueryVendorList(VendorRquestDTO request)
        {
            var requestbody = request.ReqBody;
            Hashtable ht = new Hashtable(10);
            if (!string.IsNullOrEmpty(requestbody.SysNo))
            {
                ht.Add("VendorSysNo", requestbody.SysNo);
            }
            if (!string.IsNullOrEmpty(requestbody.Status))
            {
                ht.Add("Status", requestbody.Status);
            }
            if (!string.IsNullOrEmpty(requestbody.VendorName))
            {
                ht.Add("VendorName", requestbody.VendorName);
            }
            var ds=VendorManager.GetInstance().GetVendorDs(ht);
            var list = ExtensiveHelper.Map<DataTable, VendorResponseBody>(ds.Tables[0]).OrderBy(x=>x.V_VendorFirstLetter).ToList();
            
            return list;
        }
    }
}
