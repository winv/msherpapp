using Eson.Objects;
using Eson.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.Model.DTO
{
    public class PurchasePoMasterRquestDTO : BaseDTO
    {
        public PoMasterRequestBody ReqBody { get; set; }
        public PoMasterBody ReqBodyDTO { get; set; }
    }

    public class PurchasePoMasterResponseDTO : BaseDTO
    {
        public List<PoItemBody> ResBody { get; set; }
        public List<PoMasterBody> ResMasterBody { get; set; }
    }

    public class PoMasterRequestBody
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

        public string CreateUserSysNo { get; set; }

        public string VendorSysNo { get; set; }

        public string Status { get; set; }
        public string ProductSysNo { get; set; }

        public string CreateTimeFrom { get; set; }
        public string CreateTimeTo { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public string PrivilegeList { get; set; }
    }

    public class PoMasterBody
    {
        #region 属性
        /// <summary>
        /// SysNo
        /// </summary>		
        public int SysNo { get; set; }

        /// <summary>
        /// POID
        /// </summary>		
        public string POID { get; set; }

        /// <summary>
        /// VendorSysNo
        /// </summary>		
        public int VendorSysNo { get; set; }

        /// <summary>
        /// StockSysNo
        /// </summary>		
        public int StockSysNo { get; set; }

        /// <summary>
        /// ShipTypeSysNo
        /// </summary>		
        public int ShipTypeSysNo { get; set; }

        /// <summary>
        /// PayTypeSysNo
        /// </summary>		
        public int PayTypeSysNo { get; set; }

        /// <summary>
        /// CurrencySysNo
        /// </summary>		
        public int CurrencySysNo { get; set; }

        /// <summary>
        /// ExchangeRate
        /// </summary>		
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// TotalAmt
        /// </summary>		
        public decimal TotalAmt { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreateUserSysNo
        /// </summary>		
        public int CreateUserSysNo { get; set; }

        /// <summary>
        /// AuditTime
        /// </summary>		
        public DateTime AuditTime { get; set; }

        /// <summary>
        /// AuditUserSysNo
        /// </summary>		
        public int AuditUserSysNo { get; set; }

        /// <summary>
        /// ReceiveTime
        /// </summary>		
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// ReceiveUserSysNo
        /// </summary>		
        public int ReceiveUserSysNo { get; set; }

        /// <summary>
        /// InTime
        /// </summary>		
        public DateTime InTime { get; set; }

        /// <summary>
        /// InUserSysNo
        /// </summary>		
        public int InUserSysNo { get; set; }

        /// <summary>
        /// IsApportion
        /// </summary>		
        public int IsApportion { get; set; }

        /// <summary>
        /// ApportionTime
        /// </summary>		
        public DateTime ApportionTime { get; set; }

        /// <summary>
        /// ApportionUserSysNo
        /// </summary>		
        public int ApportionUserSysNo { get; set; }

        /// <summary>
        /// PayDate
        /// </summary>		
        public DateTime PayDate { get; set; }

        /// <summary>
        /// Memo
        /// </summary>		
        public string Memo { get; set; }

        /// <summary>
        /// Note
        /// </summary>		
        public string Note { get; set; }

        /// <summary>
        /// Status
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// POType
        /// </summary>		
        public int POType { get; set; }

        /// <summary>
        /// POInvoiceType
        /// </summary>		
        public int POInvoiceType { get; set; }

        /// <summary>
        /// ManagerAuditMemo
        /// </summary>		
        public string ManagerAuditMemo { get; set; }

        /// <summary>
        /// ManagerAuditStatus
        /// </summary>		
        public int ManagerAuditStatus { get; set; }

        /// <summary>
        /// POInvoiceDunDesc
        /// </summary>		
        public string POInvoiceDunDesc { get; set; }

        /// <summary>
        /// InvoiceExpectReceiveDate
        /// </summary>		
        public DateTime InvoiceExpectReceiveDate { get; set; }

        /// <summary>
        /// ShelveUserSysNo
        /// </summary>		
        public int ShelveUserSysNo { get; set; }

        /// <summary>
        /// ShelveTime
        /// </summary>		
        public DateTime ShelveTime { get; set; }

        /// <summary>
        /// MinusPOType
        /// </summary>		
        public int MinusPOType { get; set; }

        /// <summary>
        /// ReferencePOSysNo
        /// </summary>		
        public int ReferencePOSysNo { get; set; }

        /// <summary>
        /// IsNeedApportion
        /// </summary>		
        public int IsNeedApportion { get; set; }

        /// <summary>
        /// AuditNote
        /// </summary>		
        public string AuditNote { get; set; }

        /// <summary>
        /// CheckQtyUserSysNo
        /// </summary>		
        public int CheckQtyUserSysNo { get; set; }

        /// <summary>
        /// CheckQtyTime
        /// </summary>		
        public DateTime CheckQtyTime { get; set; }

        /// <summary>
        /// ExpectInTime
        /// </summary>		
        public DateTime ExpectInTime { get; set; }

        /// <summary>
        /// LabledUserSysNo
        /// </summary>		
        public int LabledUserSysNo { get; set; }

        /// <summary>
        /// LabledTime
        /// </summary>		
        public DateTime LabledTime { get; set; }

        /// <summary>
        /// PayDateType
        /// </summary>		
        public int PayDateType { get; set; }

        /// <summary>
        /// IsCanOvertimeReceive
        /// </summary>		
        public int IsCanOvertimeReceive { get; set; }

        /// <summary>
        /// PrintTime
        /// </summary>		
        public DateTime PrintTime { get; set; }

        /// <summary>
        /// PrintUserSysNo
        /// </summary>		
        public int PrintUserSysNo { get; set; }

        /// <summary>
        /// PorterUserSysNo
        /// </summary>		
        public int PorterUserSysNo { get; set; }

        /// <summary>
        /// PortTime
        /// </summary>		
        public DateTime PortTime { get; set; }

        /// <summary>
        /// POInvoiceTen
        /// </summary>		
        public decimal POInvoiceTen { get; set; }

        /// <summary>
        /// DeliveryTime
        /// </summary>		
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// POShelfLifeVerifyStatus
        /// </summary>		
        public int POShelfLifeVerifyStatus { get; set; }

        /// <summary>
        /// rowCreateDate
        /// </summary>		
        public DateTime rowCreateDate { get; set; }

        /// <summary>
        /// rowModifyDate
        /// </summary>		
        public DateTime rowModifyDate { get; set; }

        /// <summary>
        /// ReceiveAddress
        /// </summary>		
        public string ReceiveAddress { get; set; }

        /// <summary>
        /// ReturnAddress
        /// </summary>		
        public string ReturnAddress { get; set; }

        /// <summary>
        /// PoPlanSysNo
        /// </summary>		
        public int PoPlanSysNo { get; set; }


        #endregion

        #region 虚拟字段
        public List<PoItemBody> poItems { get; set; }
        public string V_VendorName { get; set; }
        public string V_StockName { get; set; }
        public string V_CreateName { get; set; }

        public string V_Status
        {
            get
            {
                return DataHelper.GetDescription(typeof(AppEnum.POStatus), this.Status);
            }
        }

        public List<POApportionInfoBody> appList { get; set; }
        #endregion
    }

    public class PoItemBody
    {
        #region 属性
        /// <summary>
        /// POSysNo
        /// </summary>		
        public int POSysNo { get; set; }

        /// <summary>
        /// ProductSysNo
        /// </summary>		
        public int ProductSysNo { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>		
        public decimal Quantity { get; set; }

        /// <summary>
        /// Weight
        /// </summary>		
        public int Weight { get; set; }

        /// <summary>
        /// OrderPrice
        /// </summary>		
        public decimal OrderPrice { get; set; }

        /// <summary>
        /// ApportionAddOn
        /// </summary>		
        public decimal ApportionAddOn { get; set; }

        /// <summary>
        /// UnitCost
        /// </summary>		
        public decimal UnitCost { get; set; }

        /// <summary>
        /// C1Price
        /// </summary>		
        public decimal C1Price { get; set; }

        /// <summary>
        /// C2Price
        /// </summary>		
        public decimal C2Price { get; set; }

        /// <summary>
        /// C3Price
        /// </summary>		
        public decimal C3Price { get; set; }

        /// <summary>
        /// OrderQty
        /// </summary>		
        public decimal OrderQty { get; set; }

        /// <summary>
        /// C3URL
        /// </summary>		
        public string C3URL { get; set; }

        /// <summary>
        /// ShelveQuantity
        /// </summary>		
        public int ShelveQuantity { get; set; }

        /// <summary>
        /// ShelveUserSysNo
        /// </summary>		
        public int ShelveUserSysNo { get; set; }

        /// <summary>
        /// ShelveTime
        /// </summary>		
        public DateTime ShelveTime { get; set; }

        /// <summary>
        /// StockNo
        /// </summary>		
        public string StockNo { get; set; }

        /// <summary>
        /// UnitCostInvoice
        /// </summary>		
        public decimal UnitCostInvoice { get; set; }

        /// <summary>
        /// ManufactureDate
        /// </summary>		
        public DateTime ManufactureDate { get; set; }

        /// <summary>
        /// ValidityDateTo
        /// </summary>		
        public DateTime ValidityDateTo { get; set; }

        /// <summary>
        /// rowCreateDate
        /// </summary>		
        public DateTime rowCreateDate { get; set; }

        /// <summary>
        /// rowModifyDate
        /// </summary>		
        public DateTime rowModifyDate { get; set; }

        /// <summary>
        /// UnitEIMSAmt
        /// </summary>		
        public decimal UnitEIMSAmt { get; set; }

        /// <summary>
        /// IsPromotion
        /// </summary>		
        public int IsPromotion { get; set; }

        /// <summary>
        /// SysNo
        /// </summary>		
        public int SysNo { get; set; }

        /// <summary>
        /// RecieveOrderNum
        /// </summary>		
        public int RecieveOrderNum { get; set; }

        /// <summary>
        /// RefuseQty
        /// </summary>		
        public decimal RefuseQty { get; set; }


        #endregion

        #region 虚拟字段
        public decimal V_AccountQty {get;set;}
        public decimal V_AvailableQty { get; set; }
        public decimal V_DemandQty { get; set; }
        public decimal V_DaySaleQty { get; set; }
        public string ProductName { get; set; }
        public string V_VendorName { get; set; }
        #endregion
    }

    public class POApportionInfoBody
    {
        #region 属性
        /// <summary>
        /// SysNo
        /// </summary>		
        public int SysNo { get; set; }

        /// <summary>
        /// POSysNo
        /// </summary>		
        public int POSysNo { get; set; }

        /// <summary>
        /// ApportionSubjectSysNo
        /// </summary>		
        public int ApportionSubjectSysNo { get; set; }

        /// <summary>
        /// ApportionType
        /// </summary>		
        public int ApportionType { get; set; }

        /// <summary>
        /// ExpenseAmt
        /// </summary>		
        public decimal ExpenseAmt { get; set; }

        /// <summary>
        /// rowCreateDate
        /// </summary>		
        public DateTime rowCreateDate { get; set; }

        /// <summary>
        /// rowModifyDate
        /// </summary>		
        public DateTime rowModifyDate { get; set; }

        /// <summary>
        /// Status
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// CreateUserSysNo
        /// </summary>		
        public int CreateUserSysNo { get; set; }

        /// <summary>
        /// UpdateUserSysNo
        /// </summary>		
        public int UpdateUserSysNo { get; set; }

        public List<POApportionItemInfoBody> itemList { get; set; }
        public List<PoMasterBody> POInfoList { get; set; }
        #endregion

        #region 虚拟字段


        #endregion
    }

    public class POApportionItemInfoBody
    {
        #region 属性
        /// <summary>
        /// SysNo
        /// </summary>		
        public int SysNo { get; set; }

        /// <summary>
        /// ApportionSysNo
        /// </summary>		
        public int ApportionSysNo { get; set; }

        /// <summary>
        /// ProductSysNo
        /// </summary>		
        public int ProductSysNo { get; set; }

        /// <summary>
        /// rowCreateDate
        /// </summary>		
        public DateTime rowCreateDate { get; set; }

        /// <summary>
        /// rowModifyDate
        /// </summary>		
        public DateTime rowModifyDate { get; set; }

        /// <summary>
        /// POSysNo
        /// </summary>		
        public int POSysNo { get; set; }

        /// <summary>
        /// ApportionAddOn
        /// </summary>		
        public decimal ApportionAddOn { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>		
        public decimal Quantity { get; set; }

        /// <summary>
        /// ApportionAmt
        /// </summary>		
        public decimal ApportionAmt { get; set; }

        /// <summary>
        /// Status
        /// </summary>		
        public int Status { get; set; }


        #endregion

        #region 虚拟字段


        #endregion
    }
}