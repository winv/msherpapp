using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.BLL.Interface
{
    public interface IProductInfoManager:IDependency
    {
        List<ProductResponseBody> GetProductList(ProductRequstBody request);

        ProductBasicResponseBody GetProductDetail(ProductRequstBody request);
    }
}
