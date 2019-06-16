using Eson.BizModel.SysInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.Model.DTO
{
    public class UserRequestDTO : BaseDTO
    {
        public UserRequestBody ReqBody { get; set; }
    }
    public class UserResponseDTO : BaseDTO
    {
        public UserResponseDTO ReqBody { get; set; }
    }
    public class UserRequestBody
    {
        /// <summary>
        /// 用户信息表SysNo（支持逗号批量搜索）
        /// </summary>
        public string SysNo { get; set; }

        /// <summary>
        /// 工号（全匹配搜索）
        /// </summary>
        public string UserIDFull { get; set; }

        /// <summary>
        /// 用户姓名（全匹配搜索）
        /// </summary>
        public string UserNameFull { get; set; }

        /// <summary>
        /// 工号（支持逗号批量搜索、模糊查询）
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 工号（支持逗号批量搜索、模糊查询）
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 状态（取枚举EnumCls.ValidEnum，支持逗号分隔批量搜索）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 状态（取枚举EnumCls.YesNoEnum）
        /// </summary>
        public string IsStoreUser { get; set; }

        /// <summary>
        /// Pwd
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 手机号（支持逗号批量搜索、模糊查询）
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户部门Sysno（支持逗号批量搜索）
        /// </summary>
        public string DepartmentSysNo { get; set; }

        /// <summary>
        /// 用户所在门店SysNo（支持逗号批量搜索）
        /// </summary>
        public string Location { get; set; }
    }
    public class UserResponseBody
    {
        #region Model
        private int _sysNo;
        private string _userID;
        private string _userName;
        private string _pwd;
        private string _email;
        private string _phone;
        private string _note;
        private int _status;
        private int _bossSysNo;
        private int _departmentSysNo;
        private int _branchSysNo;
        private int _wageFormSysNo;
        private int _location;
        private string _extPhone;
        private int _toBranchSysNo;
        private long _qQ;
        private DateTime _rowCreateDate;
        private DateTime _rowModifyDate;
        private DateTime _dimissionDate;

        /// <summary>
        /// SysNo
        /// </summary>
        public int SysNo
        {
            set { _sysNo = value; }
            get { return _sysNo; }
        }
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID
        {
            set { _userID = value; }
            get { return _userID; }
        }
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
        }
        /// <summary>
        /// Pwd
        /// </summary>
        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// Note
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// Status
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// BossSysNo
        /// </summary>
        public int BossSysNo
        {
            set { _bossSysNo = value; }
            get { return _bossSysNo; }
        }
        /// <summary>
        /// DepartmentSysNo
        /// </summary>
        public int DepartmentSysNo
        {
            set { _departmentSysNo = value; }
            get { return _departmentSysNo; }
        }
        /// <summary>
        /// BranchSysNo
        /// </summary>
        public int BranchSysNo
        {
            set { _branchSysNo = value; }
            get { return _branchSysNo; }
        }
        /// <summary>
        /// WageFormSysNo
        /// </summary>
        public int WageFormSysNo
        {
            set { _wageFormSysNo = value; }
            get { return _wageFormSysNo; }
        }
        /// <summary>
        /// 用户所在工作门店
        /// </summary>
        public int Location
        {
            set { _location = value; }
            get { return _location; }
        }
        /// <summary>
        /// ExtPhone
        /// </summary>
        public string ExtPhone
        {
            set { _extPhone = value; }
            get { return _extPhone; }
        }
        /// <summary>
        /// ToBranchSysNo
        /// </summary>
        public int ToBranchSysNo
        {
            set { _toBranchSysNo = value; }
            get { return _toBranchSysNo; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public long QQ
        {
            set { _qQ = value; }
            get { return _qQ; }
        }
        /// <summary>
        /// rowCreateDate
        /// </summary>
        public DateTime rowCreateDate
        {
            set { _rowCreateDate = value; }
            get { return _rowCreateDate; }
        }
        /// <summary>
        /// rowModifyDate
        /// </summary>
        public DateTime rowModifyDate
        {
            set { _rowModifyDate = value; }
            get { return _rowModifyDate; }
        }
        /// <summary>
        /// DimissionDate
        /// </summary>
        public DateTime DimissionDate
        {
            set { _dimissionDate = value; }
            get { return _dimissionDate; }
        }

        /// <summary>
        /// 是否门店用户(默认否)
        /// </summary>
        public bool IsStoreUser { get; set; }

        /// <summary>
        /// 是否拥有所有门店登录权限
        /// </summary>
        public bool IsHaveAllStoreRight { get; set; }

        /// <summary>
        /// 配送员可领单数
        /// </summary>
        public int DMSLimitQty { get; set; }

        /// <summary>
        /// 钉钉Id
        /// </summary>
        public string DingtalkId { get; set; }
        #endregion Model

        #region 虚拟字段

        /// <summary>
        /// 虚拟字段：门店名称
        /// </summary>
        public string V_StockName { get; set; }

        /// <summary>
        /// 虚拟字段：用户角色泛型
        /// </summary>
        public List<BizSysRoleInfo> V_RoleList { get; set; }
        #endregion
    }
}
