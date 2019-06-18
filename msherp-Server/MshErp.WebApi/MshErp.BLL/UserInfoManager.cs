using Eson.BizModel.SysInfo;
using Eson.BLL.SysInfo;
using Eson.Utils;
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
            search.Status = ((int)EnumCls.ValidEnum.Valid).ToString();
            search.wxopenid = request.WeiXinOpenID;
            search.UserIDFull = request.ReqBody.UserID;
            search.UserID = "";
            var result = (new SysUserBo()).GetPageList(search).FirstOrDefault();
            return result.RobotMap<BizSysUserInfo, UserResponseBody>();
        }

        public bool BindUserWxOpenid(UserRequestDTO requestDTO)
        {
            if (!string.IsNullOrEmpty(requestDTO.ReqBody.SysNo))
            {
                return new SysUserBo().UpdateUserWeiXinOpenID(int.Parse(requestDTO.ReqBody.SysNo), requestDTO.WeiXinOpenID);
            }
            return false;
        }
        
        public bool GetUserWithOpenid(string wxopenid)
        {
            throw new NotImplementedException();
        }
    }
}
