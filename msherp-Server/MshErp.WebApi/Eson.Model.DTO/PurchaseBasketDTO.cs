using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.Model.DTO
{
    public class PurchaseBasketRequstDTO : BaseDTO
    {
        public PurchaseBasketRequstBody ReqBody { get; set; }
        public PurchaseBasketResponseBody ReqBodyModel { get; set; }
    }
    public class PurchaseBasketResponseDTO : BaseDTO
    {
        public List<PurchaseBasketResponseBody> ReqBody { get; set; }
    }
    public class PurchaseBasketRequstBody
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

        public string ProductSysNo { get; set; }

        public string StockSysNo { get; set; }
    }
    public class PurchaseBasketResponseBody
    {
        #region 属性
        /// <summary>
        /// CreateUserSysNo
        /// </summary>		
        public int CreateUserSysNo { get; set; }

        /// <summary>
        /// ProductSysNo
        /// </summary>		
        public int ProductSysNo { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>		
        public decimal Quantity { get; set; }

        /// <summary>
        /// OrderPrice
        /// </summary>		
        public decimal OrderPrice { get; set; }

        /// <summary>
        /// rowCreateDate
        /// </summary>		
        public DateTime rowCreateDate { get; set; }

        /// <summary>
        /// rowModifyDate
        /// </summary>		
        public DateTime rowModifyDate { get; set; }

        /// <summary>
        /// SysNo
        /// </summary>		
        public int SysNo { get; set; }

        /// <summary>
        /// VendorSysNo
        /// </summary>		
        public int VendorSysNo { get; set; }

        /// <summary>
        /// StockSysNo
        /// </summary>		
        public int StockSysNo { get; set; }


        #endregion

        #region 虚拟字段
        public string V_ProductName { get; set; }
        public string V_ProductID { get; set; }
        public string V_UserName { get; set; }
        public string V_VendorName { get; set; }
        public string V_StockName { get; set; }
        public int V_LastVendorSysNo { get; set; }
        #endregion
    }
}