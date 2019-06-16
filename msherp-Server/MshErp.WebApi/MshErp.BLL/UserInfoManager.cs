using Eson.BizModel.SysInfo;
using Eson.BLL.SysInfo;
using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using RobotMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.BLL
{
    public class UserInfoManager : IUserInfoManager
    {
        public UserResponseBody CheckUser(UserRequestDTO request)
        {
            var search = request.ReqBody.RobotMap<UserRequestBody, BizSysUserSearchInfo>();
            var result = (new SysUserBo()).GetPageList(search).FirstOrDefault();
            return result.RobotMap<BizSysUserInfo, UserResponseBody>();
        }

        public bool BindUserWxOpenid(string username)
        {
            throw new NotImplementedException();
        }
        
        public bool GetUserWithOpenid(string wxopenid)
        {
            throw new NotImplementedException();
        }
    }
}
