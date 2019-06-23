using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.Model.DTO
{
    public class VendorRquestDTO : BaseDTO
    {
        public VendorRequestBody ReqBody { get; set; }
        public VendorResponseBody ReqBodyDTO { get; set; }
    }

    public class VendorResponseDTO : BaseDTO
    {
        public List<VendorResponseBody> ResBody { get; set; }
    }

    public class VendorRequestBody
    {
    }
    public class VendorResponseBody
    {
    }
}
