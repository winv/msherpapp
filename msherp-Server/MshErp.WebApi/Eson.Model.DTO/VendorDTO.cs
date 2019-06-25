using Eson.Utils;
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
        /// <summary>
        /// 单据主键（支持逗号批量检索）
        /// </summary>
        public string SysNo { get; set; }

        /// <summary>
        /// 行创建时间（From）
        /// </summary>
        public string rowCreateDateFrom { get; set; }

        /// <summary>
        /// 行创建时间（To）
        /// </summary>
        public string rowCreateDateTo { get; set; }

        /// <summary>
        /// 行修改时间（From）
        /// </summary>
        public string rowModifyDateFrom { get; set; }

        /// <summary>
        /// 行修改时间（To）
        /// </summary>
        public string rowModifyDateTo { get; set; }
        public string VendorName { get; set; }
        public string Status { get; set; }
        public string VendorID { get; set; }
        public string V_VendorNameLetter { get; set; }
        public string V_VendorAbbLetter { get; set; }
    }
    public class VendorResponseBody
    {
        #region 属性
        /// <summary>
        /// SysNo
        /// </summary>		
        public int SysNo { get; set; }

        /// <summary>
        /// VendorID
        /// </summary>		
        public string VendorID { get; set; }

        /// <summary>
        /// VendorName
        /// </summary>		
        public string VendorName { get; set; }

        /// <summary>
        /// EnglishName
        /// </summary>		
        public string EnglishName { get; set; }

        /// <summary>
        /// BriefName
        /// </summary>		
        public string BriefName { get; set; }

        /// <summary>
        /// VendorType
        /// </summary>		
        public int VendorType { get; set; }

        /// <summary>
        /// District
        /// </summary>		
        public string District { get; set; }

        /// <summary>
        /// Address
        /// </summary>		
        public string Address { get; set; }

        /// <summary>
        /// Zip
        /// </summary>		
        public string Zip { get; set; }

        /// <summary>
        /// Contact
        /// </summary>		
        public string Contact { get; set; }

        /// <summary>
        /// Phone
        /// </summary>		
        public string Phone { get; set; }

        /// <summary>
        /// Fax
        /// </summary>		
        public string Fax { get; set; }

        /// <summary>
        /// Email
        /// </summary>		
        public string Email { get; set; }

        /// <summary>
        /// Site
        /// </summary>		
        public string Site { get; set; }

        /// <summary>
        /// Bank
        /// </summary>		
        public string Bank { get; set; }

        /// <summary>
        /// Account
        /// </summary>		
        public string Account { get; set; }

        /// <summary>
        /// TaxNo
        /// </summary>		
        public string TaxNo { get; set; }

        /// <summary>
        /// Comment
        /// </summary>		
        public string Comment { get; set; }

        /// <summary>
        /// Note
        /// </summary>		
        public string Note { get; set; }

        /// <summary>
        /// WarrantyAreaSysNo
        /// </summary>		
        public int WarrantyAreaSysNo { get; set; }

        /// <summary>
        /// WarrantyAddress
        /// </summary>		
        public string WarrantyAddress { get; set; }

        /// <summary>
        /// WarrantyZip
        /// </summary>		
        public string WarrantyZip { get; set; }

        /// <summary>
        /// WarrantyContact
        /// </summary>		
        public string WarrantyContact { get; set; }

        /// <summary>
        /// WarrantyPhone
        /// </summary>		
        public string WarrantyPhone { get; set; }

        /// <summary>
        /// WarrantyFax
        /// </summary>		
        public string WarrantyFax { get; set; }

        /// <summary>
        /// WarrantyEmail
        /// </summary>		
        public string WarrantyEmail { get; set; }

        /// <summary>
        /// WarrantySite
        /// </summary>		
        public string WarrantySite { get; set; }

        /// <summary>
        /// WarrantySelfSend
        /// </summary>		
        public int WarrantySelfSend { get; set; }

        /// <summary>
        /// Status
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// RMAPosition
        /// </summary>		
        public string RMAPosition { get; set; }

        /// <summary>
        /// 帐期类型
        /// </summary>		
        public int APType { get; set; }

        /// <summary>
        /// PoMobileTel
        /// </summary>		
        public string PoMobileTel { get; set; }

        /// <summary>
        /// PoMSN
        /// </summary>		
        public string PoMSN { get; set; }

        /// <summary>
        /// PoQQ
        /// </summary>		
        public string PoQQ { get; set; }

        /// <summary>
        /// WarrantyMSN
        /// </summary>		
        public string WarrantyMSN { get; set; }

        /// <summary>
        /// WarrantyQQ
        /// </summary>		
        public string WarrantyQQ { get; set; }

        /// <summary>
        /// RCProductPolicy
        /// </summary>		
        public string RCProductPolicy { get; set; }

        /// <summary>
        /// FinanceContact
        /// </summary>		
        public string FinanceContact { get; set; }

        /// <summary>
        /// FinanceFax
        /// </summary>		
        public string FinanceFax { get; set; }

        /// <summary>
        /// FinanceTel
        /// </summary>		
        public string FinanceTel { get; set; }

        /// <summary>
        /// FinanceMSN
        /// </summary>		
        public string FinanceMSN { get; set; }

        /// <summary>
        /// FinanceQQ
        /// </summary>		
        public string FinanceQQ { get; set; }

        /// <summary>
        /// FinanceEmail
        /// </summary>		
        public string FinanceEmail { get; set; }

        /// <summary>
        /// FinanceAddress
        /// </summary>		
        public string FinanceAddress { get; set; }

        /// <summary>
        /// OtherAccount
        /// </summary>		
        public string OtherAccount { get; set; }

        /// <summary>
        /// ContractDateStart
        /// </summary>		
        public DateTime ContractDateStart { get; set; }

        /// <summary>
        /// ContractDateEnd
        /// </summary>		
        public DateTime ContractDateEnd { get; set; }

        /// <summary>
        /// SendGoodsCycle
        /// </summary>		
        public decimal SendGoodsCycle { get; set; }

        /// <summary>
        /// AccountUpdateUserSysNo
        /// </summary>		
        public int AccountUpdateUserSysNo { get; set; }

        /// <summary>
        /// AccountUpdateTime
        /// </summary>		
        public DateTime AccountUpdateTime { get; set; }

        /// <summary>
        /// BankSubbranch
        /// </summary>		
        public string BankSubbranch { get; set; }

        /// <summary>
        /// AccountName
        /// </summary>		
        public string AccountName { get; set; }

        /// <summary>
        /// BankSecond
        /// </summary>		
        public string BankSecond { get; set; }

        /// <summary>
        /// BankSubbranchSecond
        /// </summary>		
        public string BankSubbranchSecond { get; set; }

        /// <summary>
        /// CycleLess7
        /// </summary>		
        public string CycleLess7 { get; set; }

        /// <summary>
        /// CycleLess7Policy
        /// </summary>		
        public string CycleLess7Policy { get; set; }

        /// <summary>
        /// Cycle7To15
        /// </summary>		
        public string Cycle7To15 { get; set; }

        /// <summary>
        /// Cycle7To15Policy
        /// </summary>		
        public string Cycle7To15Policy { get; set; }

        /// <summary>
        /// CycleMore15
        /// </summary>		
        public string CycleMore15 { get; set; }

        /// <summary>
        /// CycleMore15Policy
        /// </summary>		
        public string CycleMore15Policy { get; set; }

        /// <summary>
        /// IsHasBusinessLicence
        /// </summary>		
        public int IsHasBusinessLicence { get; set; }

        /// <summary>
        /// IsHasOpenAccountLicence
        /// </summary>		
        public int IsHasOpenAccountLicence { get; set; }

        /// <summary>
        /// IsHasTaxRegistration
        /// </summary>		
        public int IsHasTaxRegistration { get; set; }

        /// <summary>
        /// IsHasPersonIDCard
        /// </summary>		
        public int IsHasPersonIDCard { get; set; }

        /// <summary>
        /// IsHasGeneralTaxpayer
        /// </summary>		
        public int IsHasGeneralTaxpayer { get; set; }

        /// <summary>
        /// GeneralTaxpayerNote
        /// </summary>		
        public string GeneralTaxpayerNote { get; set; }

        /// <summary>
        /// IsHasOrganizationCode
        /// </summary>		
        public int IsHasOrganizationCode { get; set; }

        /// <summary>
        /// Vendor2Type
        /// </summary>		
        public int Vendor2Type { get; set; }

        /// <summary>
        /// VendorTitle
        /// </summary>		
        public string VendorTitle { get; set; }

        /// <summary>
        /// IsLockVendor
        /// </summary>		
        public int IsLockVendor { get; set; }

        /// <summary>
        /// LockUserSysNo
        /// </summary>		
        public int LockUserSysNo { get; set; }

        /// <summary>
        /// LockTime
        /// </summary>		
        public DateTime LockTime { get; set; }

        /// <summary>
        /// LockNote
        /// </summary>		
        public string LockNote { get; set; }

        /// <summary>
        /// rowCreateDate
        /// </summary>		
        public DateTime rowCreateDate { get; set; }

        /// <summary>
        /// rowModifyDate
        /// </summary>		
        public DateTime rowModifyDate { get; set; }

        /// <summary>
        /// ZeroRateLicence
        /// </summary>		
        public int ZeroRateLicence { get; set; }

        /// <summary>
        /// SelfSaleLicence
        /// </summary>		
        public int SelfSaleLicence { get; set; }

        /// <summary>
        /// FcpLicence
        /// </summary>		
        public int FcpLicence { get; set; }

        /// <summary>
        /// InspectingLicence
        /// </summary>		
        public int InspectingLicence { get; set; }

        /// <summary>
        /// PQRLicence
        /// </summary>		
        public int PQRLicence { get; set; }


        #endregion

        #region 虚拟字段
        /// <summary>
        /// 首字母
        /// </summary>
        public string V_VendorFirstLetter
        {
            get
            {
                return SpellCodeTookit.GetSpellCode(this.VendorName).Substring(0, 1);
            }
        }

        public string V_VendorLetter { get; set; }

        public string V_VendorAbbLetter
        {
            get
            {
                return SpellCodeTookit.GetSpellCode(this.VendorName);
            }
        }
        #endregion
    }
}
