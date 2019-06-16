using MshErp.Model.DTO;
using MshErp.BLL.Interface;
using MshErp.Infrastructure;
using System;
using System.Web.Http;
using System.Collections.Generic;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class ProductController : BaseApiController
    {
        public IProductInfoManager _productInfoManager { get; set; }

        [Route("api/Product/QueryProductList")]
        [HttpPost]
        public AjaxResponseInfo QueryProductList([FromBody] ProductRequstDTO request)
        {
            try
            {
                if (request.ReqBody == null)
                {
                    throw new BaseCustomException("请求的参数不能为空,请至少指定一个查询条件");
                }
                var list = _productInfoManager.GetProductList(request.ReqBody);
                var result = new ProductResponseDTO
                {
                    ReqBody = list
                };
                return new AjaxResponseInfo
                {
                    Data = result,
                    Status = true,
                };
            }
            catch (Exception ex)
            {
                throw new BaseCustomException(ex);
            }
        }

        [Route("api/Product/QueryProductDetail")]
        [HttpPost]
        public AjaxResponseInfo QueryProductDetail([FromBody] ProductRequstDTO request)
        {
            try
            {
                if (request.ReqBody == null)
                {
                    throw new BaseCustomException("请求的参数不能为空,请至少指定一个查询条件");
                }
                var resultInfo = _productInfoManager.GetProductDetail(request.ReqBody);
                
                var result = new ProductBasicResponseDTO
                {
                    ReqBody = resultInfo
                };
                return new AjaxResponseInfo
                {
                    Data = result,
                    Status = true,
                };
            }
            catch (Exception ex)
            {
                throw new BaseCustomException(ex);
            }
        }
    }
}