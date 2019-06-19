using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class PurchaseController : BaseApiController
    {
        public IPurchaseInfoManager IPurchaseInfoManager { get; set; }

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
    }
}