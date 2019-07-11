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
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using Eson.BLL.Basic;

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
            UserSessionDTO oSession = new UserSessionDTO();
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

                            GetSessionInfo(oUser, oSession);
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
                oSession,
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
            UserSessionDTO oSession = new UserSessionDTO();
            try
            {
                var oUser = _userManager.GetUserWithOpenid(userDto.WeiXinOpenID);
                if (oUser==null)
                {
                    issuccess = false;
                    sb.AppendFormat("您尚未绑定微信登录,请先登录erp,绑定微信后再使用自动登录");
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

                        GetSessionInfo(oUser, oSession);
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
                oSession,
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
            CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            var sessionId = "";
            if (cookie != null)
            {
                sessionId = cookie["session-id"].Value;
            }
            return "hello jwt";
        }

        private UserBasic MapUserInfo(UserResponseBody oUser)
        {
            UserBasic oParam = new UserBasic
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

        private UserSessionDTO GetSessionInfo(UserResponseBody oUser, UserSessionDTO oSession)
        {
            oSession.User = MapUserInfo(oUser);
            string resultIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(resultIP))
            {
                resultIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(resultIP))
            {
                resultIP = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(resultIP))
            {
                resultIP = "0.0.0.0";
            }
            oSession.IpAddress = resultIP;
            oSession.PrivilegeAL = SysManager.GetInstance().GetPrivilegeALByUser(oUser.SysNo);
            oSession.CopyUserAL = SysManager.GetInstance().GetUserFigureCopyList(oUser.SysNo, true);
            oSession.CopyPrivilegeAL = SysManager.GetInstance().GetPrivilegeALByUserCopy(oUser.SysNo);
            return oSession;
        }
    }
}
