
using Eson.BizModel;
using Eson.BLL;
using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using RobotMapper;
using System;
using System.Collections.Generic;

namespace MshErp.BLL
{
    public class ProductInfoManager : IProductInfoManager
    {
        public ProductBasicResponseBody GetProductDetail(ProductRequstBody request)
        {
            var list = new ProductBo().GetModel(new BizProductSearchInfo { SysNo = request.ProductSysNo });
            var responseBody = list.RobotMap<BizProductInfo, ProductBasicResponseBody>();
            return responseBody;
        }

        public List<ProductResponseBody> GetProductList(ProductRequstBody request)
        {
            request.StockSysno = "6";
            request.ProductSysNo = request.SearchKey;
            var searchInfo = request.RobotMap<ProductRequstBody, BizProductQueryUISearchInfo>();
            var list = new StoreProductManageBo().GetProductQuery(searchInfo, true);
            var responseBody = list.RobotMap<BizProductQueryUIInfo, ProductResponseBody>();
            return responseBody;
        }
    }
}

