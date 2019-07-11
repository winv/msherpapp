using MshErp.APIServices.Core.Filters;
using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;
using static Eson.Objects.AppEnum;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class PurchaseController : BaseApiController
    {
        public IPurchaseInfoManager IPurchaseInfoManager { get; set; }

        #region 查询操作 无需权限验证
        [HttpPost]
        public AjaxResponseInfo QueryPoBaksetDetail([FromBody] PurchaseBasketRequstDTO request)
        {
            var respbody = IPurchaseInfoManager.GetPoBasketInfo(request);
            var body = new List<PurchaseBasketResponseBody>
            {
                respbody
            };

            return new AjaxResponseInfo
            {
                Status = true,
                Data = new PurchaseBasketResponseDTO { ReqBody = body }
            };
        }
        [HttpPost]
        public AjaxResponseInfo QueryPoBaksetList([FromBody] PurchaseBasketRequstDTO request)
        {
            var respbody = IPurchaseInfoManager.GetPoBasketList(request);
            var dto = new PurchaseBasketResponseDTO
            {
                ReqBody = respbody
            };
            return new AjaxResponseInfo
            {
                Status = true,
                Data = dto
            };
        }
        public AjaxResponseInfo QueryPoBaksetCount([FromBody] PurchaseBasketRequstDTO request)
        {
            var count = IPurchaseInfoManager.GetPoBasketCount(request);

            return new AjaxResponseInfo
            {
                Status = true,
                Data = count
            };
        }
        public AjaxResponseInfo QueryPoItemListWithBaskt([FromBody] PurchaseBasketRequstDTO request)
        {
            var result = IPurchaseInfoManager.QueryPoItemListWithBaskt(request);
            return new AjaxResponseInfo
            {
                Status = true,
                Data = new PurchasePoMasterResponseDTO { ResBody = result }
            };
        }
        [HttpPost]
        public AjaxResponseInfo QueryPoList([FromBody] PurchasePoMasterRquestDTO request)
        {
            var result = IPurchaseInfoManager.QueryPoList(request);
            var dto = new PurchasePoMasterResponseDTO
            {
                ResMasterBody = result
            };
            return new AjaxResponseInfo
            {
                Status = true,
                Data = dto
            };
        }
        public AjaxResponseInfo QueryPoMaster([FromBody] PurchasePoMasterRquestDTO request)
        {
            var result = IPurchaseInfoManager.QueryPoMaster(request);
            var dto = new PurchasePoMasterResponseDTO
            {
                ResMasterBody = new List<PoMasterBody> { result }
            };
            return new AjaxResponseInfo
            {
                Status = true,
                Data = dto
            };
        }
        #endregion

        #region 基础增删改操作 验证权限 Privilege.POFillIn 201
        [Permission(Privilege.POFillIn)]
        public AjaxResponseInfo InsertPoBasket([FromBody] PurchaseBasketRequstDTO request)
        {
            var respbody = IPurchaseInfoManager.InsertPoBasketInfo(request);
            var body = new List<PurchaseBasketResponseBody>
            {
                respbody
            };
            return new AjaxResponseInfo
            {
                Status = true,
                Data = new PurchaseBasketResponseDTO { ReqBody = body }
            };
        }

        [Permission(Privilege.POFillIn)]
        [HttpDelete]
        public AjaxResponseInfo DeletePoBasketInfo([FromBody] PurchaseBasketRequstDTO request)
        {
            var result = IPurchaseInfoManager.DeletePoBasketInfo(request);
            return new AjaxResponseInfo
            {
                Status = result,
                Data = new PurchaseBasketResponseDTO { ReqBody = null }
            };
        }

        [HttpPut]
        [Permission(Privilege.POFillIn)]
        public AjaxResponseInfo InsertPoMaster([FromBody] PurchasePoMasterRquestDTO request)
        {
            var result = IPurchaseInfoManager.InsertPoMaster(request);

            return new AjaxResponseInfo
            {
                Status = true,
                Data = result
            };
        }

        [HttpPut]
        [Permission(Privilege.POFillIn)]
        public AjaxResponseInfo InsertPoItem([FromBody] PurchasePoMasterRquestDTO request)
        {
            var result = IPurchaseInfoManager.InsertPoItem(request);
            return new AjaxResponseInfo
            {
                Status = true,
                Data = result
            };
        }

        [HttpDelete]
        [Permission(Privilege.POFillIn)]
        public AjaxResponseInfo DeletePoItem([FromBody] PurchasePoMasterRquestDTO request)
        {
            IPurchaseInfoManager.DeletePoItem(request);
            return new AjaxResponseInfo
            {
                Status = true,
                Data = null
            };
        }

        [HttpPost]
        [Permission(Privilege.POFillIn)]
        public AjaxResponseInfo UpdatePoitem([FromBody] PurchasePoMasterRquestDTO request)
        {
            var result = IPurchaseInfoManager.UpdatePoitem(request);
            return new AjaxResponseInfo
            {
                Status = true,
                Data = result
            };
        }
        #endregion

        #region 审核 收货操作 验证权限
        #endregion
    }
}