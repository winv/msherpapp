using Eson.BLL.Purchase;
using Eson.Objects.Purchase;
using Eson.Utils;
using MshErp.BLL.Interface;
using MshErp.Infrastructure;
using MshErp.Model.DTO;
using RobotMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.BLL
{
    public class PurchaseInfoManager : IPurchaseInfoManager
    {
        
        public PurchaseBasketResponseBody GetPoBasketInfo(PurchaseBasketRequstDTO request)
        {
            var result = PurchaseManager.GetInstance().LoadBasket(int.Parse(request.ReqBody.ProductSysNo), int.Parse(request.ReqBody.CreateUserSysNo), int.Parse(request.ReqBody.StockSysNo));
            if (result != null)
            {
                //var respbody = result.RobotMap<POBasketInfo, PurchaseBasketResponseBody>();
                var respbody = new PurchaseBasketResponseBody
                {
                    CreateUserSysNo = result.CreateUserSysNo,
                    ProductSysNo = result.ProductSysNo,
                    Quantity = result.Quantity,
                    OrderPrice = result.OrderPrice,
                    StockSysNo = result.StockSysNo,
                    SysNo = result.SysNo,
                    VendorSysNo = result.VendorSysNo
                };
                return respbody;
            }
            return null;
        }

        public List<PurchaseBasketResponseBody> GetPoBasketList(PurchaseBasketRequstDTO request)
        {
            var result = PurchaseManager.GetInstance().GetPOBasketDsApi(int.Parse(request.ReqBody.CreateUserSysNo), int.Parse(request.ReqBody.StockSysNo));
            //var list = result.RobotMap<DataSet, List<PurchaseBasketResponseBody>>();
            //var list = AutoMapper.Mapper.Map<List<PurchaseBasketResponseBody>>(result);
            var list = ExtensiveHelper.Map<DataTable, PurchaseBasketResponseBody>(result.Tables[0]);
            return list;
        }
        public int GetPoBasketCount(PurchaseBasketRequstDTO request)
        {
            var result = PurchaseManager.GetInstance().GetPOBasketDsApi(int.Parse(request.ReqBody.CreateUserSysNo), int.Parse(request.ReqBody.StockSysNo));
            return result.Tables[0].Rows.Count;
        }

        public PurchaseBasketResponseBody InsertPoBasketInfo(PurchaseBasketRequstDTO request)
        {
            var result = GetPoBasketInfo(request);
            var info = request.ReqBodyModel;
            var bkinfo = new POBasketInfo
            {
                CreateUserSysNo = info.CreateUserSysNo,
                ProductSysNo = info.ProductSysNo,
                Quantity = info.Quantity,
                OrderPrice = info.OrderPrice,
                StockSysNo = info.StockSysNo
            };
            if (result != null)
            {
                bkinfo.SysNo = result.SysNo;
                PurchaseManager.GetInstance().UpdatePOBasket(bkinfo);
            }
            else
            {
                PurchaseManager.GetInstance().InsertPOBasket(bkinfo);
            }
            return result;
        }

        public bool DeletePoBasketInfo(PurchaseBasketRequstDTO request)
        {
            try
            {
                PurchaseManager.GetInstance().DeletePOBasket(int.Parse(request.ReqBody.SysNo), int.Parse(request.ReqBody.CreateUserSysNo));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
            
        }

        public List<PoItemBody> QueryPoItemListWithBaskt(PurchaseBasketRequstDTO request)
        {
            var reqbody = request.ReqBody;
            var ds = PurchaseManager.GetInstance().GetPoItemWithBasket(reqbody.SysNo, reqbody.ProductSysNo);
            var list = ExtensiveHelper.Map<DataTable, PoItemBody>(ds.Tables[0]);
            return list;
        }

        public PoMasterBody InsertPoMaster(PurchasePoMasterRquestDTO request)
        {
            var poinfo = Helper.MapNoProperty<POInfo, PoMasterBody>(request.ReqBodyDTO);
            var itemlist = Helper.MapNoProperty<POItemInfo, PoItemBody>(request.ReqBodyDTO.poItems);
            Hashtable ht = new Hashtable(10);
            foreach (var item in itemlist)
            {
                ht.Add(item.ProductSysNo, item);
            }
            poinfo.itemHash = ht;
            poinfo.TotalAmt = poinfo.GetTotalAmt();
            poinfo.CreateTime = DateTime.Now;

            PurchaseManager.GetInstance().CreatePO(poinfo);
            var resbody = Helper.MapNoProperty<PoMasterBody, POInfo>(poinfo);
            return resbody;
        }
    }
}