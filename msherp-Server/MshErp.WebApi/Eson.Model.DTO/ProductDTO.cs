using Eson.BizModel;
using Eson.BizModel.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MshErp.Model.DTO
{
    public class ProductRequstDTO : BaseDTO
    {
        public ProductRequstBody ReqBody { get; set; }
    }
    public class ProductResponseDTO : BaseDTO
    {
        public List<ProductResponseBody> ReqBody { get; set; }

    }
    public class ProductBasicResponseDTO : BaseDTO
    {
        public ProductBasicResponseBody ReqBody { get; set; }

    }

    public class ProductRequstBody
    {
        public string SearchKey { get; set; }
        /// <summary>
        /// 商品号
        /// </summary>
        public string ProductSysNo { get; set; }


        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 门店号
        /// </summary>
        public string StockSysno { get; set; }

        /// <summary>
        /// 大类
        /// </summary>
        public string C1SysNo { get; set; }

        /// <summary>
        /// 中类
        /// </summary>
        public string C2SysNo { get; set; }

        /// <summary>
        /// 小类
        /// </summary>
        public string C3SysNo { get; set; }

        /// <summary>
        /// 商品管理SysNo
        /// </summary>
        public string GroupSysNo { get; set; }

        /// <summary>
        /// 商品管理人名称
        /// </summary>
        public string CurrUserGroupName { get; set; }

        /// <summary>
        /// 是否要货
        /// </summary>
        public string IsDemandProduct { get; set; }

        /// <summary>
        /// 商品状态(支持逗号批量检索)
        /// </summary>
        public string[] searchStatus { get; set; }

        public string SaleStatus { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public string InventoryStatus { get; set; }

        /// <summary>
        /// 子商品状态
        /// </summary>
        public string ChildProductSubstationStatus { get; set; }

        public string serachType { get; set; }

        public string serachValue { get; set; }

        public string[] csysno { get; set; }

        /// <summary>
        /// 门店周转天数
        /// </summary>
        public string WeekDay { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string ManufacturerSysNo { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public string ProductTypeSysNo { get; set; }

        public string MasterProductSysNo { get; set; }

        public bool IsQueryMainProduct { get; set; }
    }

    public class ProductResponseBody
    {
        /// <summary>
        /// 商品号
        /// </summary>
        public int ProductSysNo { get; set; }

        /// <summary>
        /// 主商品号
        /// </summary>
        public int MasterProductSysNo { get; set; }

        /// <summary>
        /// 价格系数
        /// </summary>
        public decimal MasterProductQtyRate { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public int StockSysNo { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        public string BriefName { get; set; }

        /// <summary>
        /// 标尾
        /// </summary>
        public string PackSpecification { get; set; }

        /// <summary>
        /// 公司库存
        /// </summary>
        public decimal CompanyStock { get; set; }

        /// <summary>
        /// 总仓库存
        /// </summary>
        public decimal WareHouseStock { get; set; }

        /// <summary>
        /// 所有门店数量
        /// </summary>
        public decimal AllStoreNum { get; set; }

        /// <summary>
        /// 财务数量
        /// </summary>
        public decimal Accountry { get; set; }

        /// <summary>
        /// 可用数量
        /// </summary>
        public decimal AvailableQty { get; set; }

        /// <summary>
        /// 共享数量
        /// </summary>
        public decimal ShareQty { get; set; }

        /// <summary>
        /// 虚拟数量
        /// </summary>
        public decimal VirtualQty { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 供应商省略
        /// </summary>
        public string SupplierLike { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime StorageTime { get; set; }

        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime InTime { get; set; }

        /// <summary>
        /// 商品管理人
        /// </summary>
        public string ProductLeader { get; set; }

        /// <summary>
        /// 单位成本
        /// </summary>
        public decimal Unicost { get; set; }

        /// <summary>
        /// 商品状态
        /// </summary>
        public int ProductStatus { get; set; }

        /// <summary>
        /// 商品状态
        /// </summary>
        public string ProductStatusName { get; set; }

        public string V_ProductStatusName { get; set; }

        public decimal d0 { get; set; }

        public decimal d1 { get; set; }

        public decimal d2 { get; set; }

        public decimal d3 { get; set; }

        public decimal d4 { get; set; }

        public decimal d5 { get; set; }

        public decimal d6 { get; set; }

        public decimal d7 { get; set; }

        public decimal w1 { get; set; }

        /// <summary>
        /// W1损耗
        /// </summary>
        public decimal SunW1 { get; set; }

        /// <summary>
        /// App售价
        /// </summary>
        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// 门店售价
        /// </summary>
        public decimal StockCurrentPrice { get; set; }

        /// <summary>
        /// 价格异常门店数
        /// </summary>
        public int AbnormalStcokNum { get; set; }

        /// <summary>
        /// 角标
        /// </summary>
        public string JiaoBiao { get; set; }

        /// <summary>
        /// 采购成本
        /// </summary>
        public decimal BuyUniCost { get; set; }

        /// <summary>
        /// 毛利率
        /// </summary>
        public string MaoLiLv { get; set; }

        /// <summary>
        /// 是否要货
        /// </summary>
        public string IsDemandProductName
        {
            get
            {
                string name = "";
                switch (IsDemandProduct)
                {
                    case 0:
                        name = "否";
                        break;
                    case 1:
                        name = "是";
                        break;
                }
                return name;
            }
        }

        /// <summary>
        /// 毛利
        /// </summary>
        public decimal GrossProfit { get; set; }

        /// <summary>
        /// 损耗率
        /// </summary>
        public string SunHaoLv { get; set; }

        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime PurchaseTime { get; set; }

        /// <summary>
        /// 缺货门店个数
        /// </summary>
        public int OutStockNum { get; set; }

        public string firstlevel { get; set; }

        public string secondlevel { get; set; }

        public string thirdlevel { get; set; }

        /// <summary>
        /// 箱规
        /// </summary>
        public decimal BoxGauge { get; set; }

        /// <summary>
        /// 订购价格
        /// </summary>
        public string OrderPrice { get; set; }

        /// <summary>
        /// 订购数量
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 箱规
        /// </summary>
        public decimal CartonMeasurement { get; set; }
        /// <summary>
        /// m1
        /// </summary>
        public decimal m1 { get; set; }

        public decimal pam1 { get; set; }

        /// <summary>
        /// 是否要货
        /// </summary>
        public int IsDemandProduct { get; set; }

        /// <summary>
        /// w1损耗
        /// </summary>
        public string paw1 { get; set; }

        /// <summary>
        /// 库存金额
        /// </summary>
        public decimal InventoryAmt { get; set; }

        /// <summary>
        /// 缺货
        /// </summary>
        public string QueHuo { get; set; }

        public string MeiTuanPrice { get; set; }
        public string ElemePrice { get; set; }
        public string JDPrice { get; set; }

        /// <summary>
        /// 异常价格颜色
        /// </summary>
        public string priceColor { get; set; }

        /// <summary>
        /// 是否能购买
        /// </summary>
        public int IsCanPurchase { get; set; }

        /// <summary>
        /// 供应商SysNo
        /// </summary>
        public int lastvendorsysno { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int V_sortValue { get; set; }
        /// <summary>
        /// 缩进字符
        /// </summary>
        public string V_spaceValue { get; set; }
        /// <summary>
        ///  是否展开
        /// </summary>
        public bool V_isExpand { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public int DenominatedUnit { get; set; }

        /// <summary>
        /// monitorqty
        /// </summary>
        public int monitorqty { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string DenominatedUnitName { get; set; }

        /// <summary>
        /// 单位名称(主商品)
        /// </summary>
        public string MasterDenominatedUnitName { get; set; }

        /// <summary>
        /// Iview CellClass
        /// </summary>
        public XObject cellClassName { get; set; }

        /// <summary>
        /// 该商品状态不是所有门店一致悬浮提示
        /// </summary>
        public string ProductStatusDiffer { get; set; }

        public string color { get; set; }

        /// <summary>
        /// APP基准价
        /// </summary>
        public decimal AppBasicPrice { get; set; }
        /// <summary>
        /// 门店基准价
        /// </summary>
        public decimal StockBasicPrice { get; set; }
        /// <summary>
        /// 门店会员价
        /// </summary>
        public decimal StockMemberPrice { get; set; }
    }

    public class ProductBasicResponseBody
    {
        public string SearchKey { get; set; }
        public decimal CartonMeasurement { get; set; }
        public decimal IncrementQty { get; set; }
        public decimal SaleMiniQty { get; set; }
        public string BriefDesc { get; set; }
        public decimal DMSWeight { get; set; }
        public string SaleUnit { get; set; }
        public int DenominatedUnit { get; set; }
        public int IsHelpOrder { get; set; }
        public string InstallService { get; set; }
        public DateTime QQPromotionTimeTo { get; set; }
        public int DiscountValidDate { get; set; }
        public DateTime QQPromotionTimeFrom { get; set; }
        public int IsSynQQPromotionWord { get; set; }
        public int IsRestrictedTransportType { get; set; }
        public int SaleModeSysNo { get; set; }
        public DateTime InfoConfirmTime { get; set; }
        public int InfoConfirmUserSysNo { get; set; }
        public int InfoConfirmStatus { get; set; }
        public DateTime rowcreatedate { get; set; }
        public DateTime rowModifydate { get; set; }
        public int ProductTypeMasterID { get; set; }
        public string ProductName2 { get; set; }
        public int ProductSaleType { get; set; }
        public string QQPromotionWord { get; set; }
        public int LoseValidDate { get; set; }
        public int IsSharedInventoryWithMaster { get; set; }
        public decimal MasterProductQtyRate { get; set; }
        public bool V_IsRealChildProduct { get; }
        public string V_DenominatedUnit { get; }
        public int V_C1SysNo { get; set; }
        public string V_C1Name { get; set; }
        public string V_C2Name { get; set; }
        public int V_C2SysNo { get; set; }
        public string V_C3Name { get; set; }
        public decimal V_MasterProductShoppingQty { get; set; }
        public decimal V_ShoppingNum { get; set; }
        public BizProductSubstationInfo V_ModelSubstation { get; set; }
        public InventoryStockInfo V_ModelProductInventory { get; set; }
        public BizProductPriceInfo V_ModelProductPrice { get; set; }
        public string StopOrderTime { get; set; }
        public string PurchasePeriodWeek { get; set; }
        public int PurchasePeriod { get; set; }
        public decimal height { get; set; }
        public decimal width { get; set; }
        public decimal lengths { get; set; }
        public string BriefNameSpellCode { get; set; }
        public string PackSpecification { get; set; }
        public int ShipWay { get; set; }
        public decimal DivisionUnit { get; set; }
        public decimal TurnOverDays { get; set; }
        public int PickScale { get; set; }
        public string PromotionDesc { get; set; }
        public DateTime SaleTime { get; set; }
        public string ProductTag { get; set; }
        public int AvgRemarkScore { get; set; }
        public int IsValuable { get; set; }
        public string AssessmentLink { get; set; }
        public string AssessmentTitle { get; set; }
        public int AvgDailyClick { get; set; }
        public int IsLarge { get; set; }
        public string BarCode { get; set; }
        public string Note { get; set; }
        public string Attention { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUserSysNo { get; set; }
        public int APMUserSysNo { get; set; }
        public int PPMUserSysNo { get; set; }
        public int MultiPicNum { get; set; }
        public string ProductLink { get; set; }
        public int ManufacturerSysNo { get; set; }
        public int C3SysNo { get; set; }
        public int Weight { get; set; }
        public string PackageList { get; set; }
        public string Warranty { get; set; }
        public string Performance { get; set; }
        public string BriefName { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string ProductMode { get; set; }
        public string ProductID { get; set; }
        public int SysNo { get; set; }
        public int OrderNum { get; set; }
        public int V_RealMasterProductSysNo { get; }
        public int ProductColor { get; set; }
        public int Product2ndType { get; set; }
        public int SPMUserSysNo { get; set; }
        public int HasGift { get; set; }
        public int IsCanReturnToVendor { get; set; }
        public string ProductNamePreview { get; set; }
        public int NewShow { get; set; }
        public int SaleModel { get; set; }
        public int ReviewCountInfoPic { get; set; }
        public decimal AvgInfoScore { get; set; }
        public decimal AvgPicScore { get; set; }
        public int ReviewCount { get; set; }
        public int ReviewStart { get; set; }
        public int IsAllCarPublic { get; set; }
        public string TaoBaoKeyWordsUrl { get; set; }
        public string NewEggKeyWords { get; set; }
        public string JDKeyWords { get; set; }
        public int IsWeigh { get; set; }
        public decimal MallMelonCutRate { get; set; }
        public int IsSaleOnly { get; set; }
        public int PriceVelocityType { get; set; }
        public decimal NeweggGPRate { get; set; }
        public decimal JDGPRate { get; set; }
        public string NeweggProductID { get; set; }
        public int NeweggSysNo { get; set; }
        public int JDSysNo { get; set; }
        public int MasterProductSysNo { get; set; }
        public int ProductSize { get; set; }
    }
}
