using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class UserManageController : BaseApiController
    {
        public IUserInfoManager _userInfoManage { get; set; }
        [Route("api/UserManage/BindWeiXinOpenId")]
        public AjaxResponseInfo BindWeiXinOpenId([FromBody] UserRequestDTO requestDTO)
        {

            var result = _userInfoManage.BindUserWxOpenid(requestDTO);
            return new AjaxResponseInfo
            {
                Status = result,
                Message = "成功"
            };
        }
    }
}
