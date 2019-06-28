using Eson.BLL.Basic;
using Eson.Objects;
using Eson.Objects.Basic;
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

        public string VerifyVendor(int VendorSysNo)
        {
            VendorInfo vInfo = VendorManager.GetInstance().Load(VendorSysNo);
            if (vInfo.Status != (int)AppEnum.BiStatus.Valid)
            {
                string msg= "无效状态的供应商，不能创建采购单！ ";
                return msg;
            }
            if (vInfo == null || vInfo.APType == AppConst.IntNull)
            {
                string msg = "请完成更新供应商的帐期类型！ ";
                return msg;
            }
            if (vInfo.CateManuHash.Count == 0)
            {
                string msg = "请完成供应商的供应品类！";
                return msg;
            }
            return "";
        }
    }
}
