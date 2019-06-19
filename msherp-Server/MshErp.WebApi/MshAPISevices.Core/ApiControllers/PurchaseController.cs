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
            var respbody = IPurchaseInfoManager.GetPoBasketInfo(request);
            var body = new List<PurchaseBasketResponseBody>();
            body.Add(respbody);

            return new AjaxResponseInfo
            {
                Status = true,
                Data = new PurchaseBasketResponseDTO { ReqBody = body }
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