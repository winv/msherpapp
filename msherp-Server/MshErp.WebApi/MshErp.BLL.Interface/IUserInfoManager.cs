using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.BLL.Interface
{
    public interface IUserInfoManager : IDependency
    {
        UserResponseBody CheckUser(UserRequestDTO request);

        bool BindUserWxOpenid(string username);

        bool GetUserWithOpenid(string wxopenid);
    }
}