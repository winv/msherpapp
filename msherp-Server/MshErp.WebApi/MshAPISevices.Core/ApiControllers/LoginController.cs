using MshErp.Model.DTO;
using MshErp.BLL.Interface;
using MshErp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Eson.Objects;
using Eson.Objects.Basic;

namespace MshErp.APIServices.Core.ApiControllers
{
    public class LoginController : BaseApiController
    {
        public IUserInfoManager _userManager { get; set; }

        [AllowAnonymous]
        [Route("api/login/check")]
        public AjaxResponseInfo Check([FromBody] UserRequestDTO userDto)
        {
            StringBuilder sb = new StringBuilder();
            bool issuccess = true;
            var Token = "";
            var sessionid = "";
            try
            {
                var oUser = _userManager.CheckUser(userDto);
                if (oUser == null)
                {
                    issuccess = false;
                    sb.AppendFormat("用户名：{0} 不存在！", userDto.ReqBody.UserName);
                }
                else
                {
                    if (!oUser.Pwd.Equals(userDto.ReqBody.Pwd))
                    {
                        issuccess = false;
                        sb.Append("登录失败，密码错误！");
                    }
                    else
                    {
                        Token = JwtTokenHelper.Generate(new Dictionary<string, dynamic>
                        {
                            { "token", userDto }
                        });
                        if (oUser.Status == (int)AppEnum.BiStatus.Valid
                           && oUser.DepartmentSysNo != (int)AppEnum.DepartmentID.Mall
                           && oUser.DepartmentSysNo != (int)AppEnum.DepartmentID.Vendor)
                        {
                            SessionInfo oSession = new SessionInfo();
                            oSession.User = MapUserInfo(oUser);
                            //oSession.IpAddress = Request.UserHostAddress;
                            //oSession.PrivilegeAL = SysManager.GetInstance().GetPrivilegeALByUser(oUser.SysNo);
                            //oSession.CopyUserAL = SysManager.GetInstance().GetUserFigureCopyList(oUser.SysNo, true);
                            //oSession.CopyPrivilegeAL = SysManager.GetInstance().GetPrivilegeALByUserCopy(oUser.SysNo);
                            System.Web.HttpContext.Current.Session["SessionInfo"] = oSession;
                            sessionid = System.Web.HttpContext.Current.Session.SessionID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                issuccess = false;
                sb.AppendFormat(ex.ToString());
            }
            var res = new
            {
                Token,
                sessionid,
                msg = sb.ToString()
            };

            return new AjaxResponseInfo()
            {
                Status = issuccess,
                Data = res
            };
        }

        [AllowAnonymous]
        public AjaxResponseInfo CheckWithWxOpenId([FromBody] UserRequestDTO userDto)
        {
            StringBuilder sb = new StringBuilder();
            bool issuccess = true;
            var Token = "";
            try
            {
                var isbind = _userManager.GetUserWithOpenid(userDto.WeiXinOpenID);
                if (!isbind)
                {
                    issuccess = false;
                    sb.AppendFormat("您尚未绑定微信登录,请先登录erp，绑定微信后再登录");
                }
                else
                {
                    Token = JwtTokenHelper.Generate(new Dictionary<string, dynamic>
                    {
                        { "token", userDto }
                    });
                }
            }
            catch (Exception ex)
            {
                issuccess = false;
                sb.AppendFormat(ex.ToString());
            }
            var res = new
            {
                Token,
                msg = sb.ToString()
            };

            return new AjaxResponseInfo()
            {
                Status = issuccess,
                Data = res
            };
        }

        [AllowAnonymous]
        [Route("api/login/ShowTest")]
        public string ShowTest([FromBody] string value)
        {
            object o = System.Web.HttpContext.Current.Session["SessionInfo"];
            return "hello jwt";
        }

        private UserInfo MapUserInfo(UserResponseBody oUser)
        {
            UserInfo oParam = new UserInfo
            {
                SysNo = oUser.SysNo,
                UserID = oUser.UserID,
                UserName = oUser.UserName,
                Pwd = oUser.Pwd,
                Email = oUser.Email,
                Phone = oUser.Phone,
                Note = oUser.Note,
                Status = oUser.Status,
                DepartmentSysNo = oUser.DepartmentSysNo,
                BranchSysNo = oUser.BranchSysNo,
                WageFormSysNo = oUser.WageFormSysNo,
                Location = oUser.Location,
                BossSysNo = oUser.BossSysNo,
                ExtPhone = oUser.ExtPhone
            };
            return oParam;
        }
    }
}
