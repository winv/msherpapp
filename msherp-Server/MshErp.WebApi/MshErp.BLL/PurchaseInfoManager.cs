﻿using Eson.BLL;
using Eson.BLL.Basic;
using Eson.BLL.Purchase;
using Eson.Objects;
using Eson.Objects.Basic;
using Eson.Objects.Purchase;
using Eson.Utils;
using MshErp.BLL.Interface;
using MshErp.Infrastructure;
using MshErp.Model.DTO;
using Newtonsoft.Json;
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

        public PurchasePoMasterResponseDTO InsertPoMaster(PurchasePoMasterRquestDTO request)
        {
            var poinfo = Helper.MapNoProperty<POInfo, PoMasterBody>(request.ReqBodyDTO);
            var itemlist = Helper.MapNoProperty<POItemInfo, PoItemBody>(request.ReqBodyDTO.poItems);
            Hashtable ht = new Hashtable(10);
            foreach (var item in itemlist)
            {
                item.Quantity = item.OrderQty;
                ht.Add(item.ProductSysNo, item);
            }
            poinfo.itemHash = ht;
            poinfo.TotalAmt = poinfo.GetTotalAmt();
            poinfo.CreateTime = DateTime.Now;
            //poinfo.CreateUserSysNo = usersysno;
            poinfo.Status = (int)AppEnum.POStatus.Origin;
            poinfo.IsApportion = (int)AppEnum.YNStatus.No;
            poinfo.ManagerAuditStatus = (int)AppEnum.POManagerAuditStatus.Origin;
            poinfo.PayDate = DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            poinfo.ExpectInTime = Util.TrimDateNull(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 10:00:00");
            poinfo.POType = 0;//普通
            poinfo.CurrencySysNo = 1;//人民币
            poinfo.IsApportion = 1;//是
            poinfo.ShipTypeSysNo = 1;//送货上门
            poinfo.PayTypeSysNo = 1;//货到付款
            poinfo.ExchangeRate = 1m;//汇率 默认为1 人民币
            //poinfo.POInvoiceType = 1;//默认不带票
            poinfo.ReferencePOSysNo = AppConst.IntNull;
            poinfo.MinusPOType = AppConst.IntNull;
            poinfo.Note = string.Format("[小程序生成]{0}", poinfo.Note);
            string msg = PurchaseManager.GetInstance().VerifyPoAndItem(poinfo);
            if (string.IsNullOrEmpty(msg))
            {
                PurchaseManager.GetInstance().CreatePO(poinfo);
                //删除采购篮
                foreach (POItemInfo item in poinfo.itemHash.Values)
                {
                    PurchaseManager.GetInstance().DeletePOBasketAfterPO(item.ProductSysNo, poinfo.StockSysNo);
                }
            }
            request.ReqBodyDTO.SysNo = poinfo.SysNo;

            //var resbody = Helper.MapNoProperty<PoMasterBody, POInfo>(poinfo);
            var result = new PurchasePoMasterResponseDTO
            {
                RetrunMsg = msg,
                ResMasterBody = new List<PoMasterBody> { request.ReqBodyDTO }
            };
            return result;
        }

        public List<PoMasterBody> QueryPoList(PurchasePoMasterRquestDTO request)
        {
            var dto = request.ReqBody;
            Hashtable ht = new Hashtable(10);
            if (!string.IsNullOrEmpty(dto.SysNo))
                ht.Add("SysNo", dto.SysNo);
            if (!string.IsNullOrEmpty(dto.ProductSysNo))
                ht.Add("ProductSysNo", dto.ProductSysNo);
            if (!string.IsNullOrEmpty(dto.CreateUserSysNo))
                ht.Add("CreateUserSysNo", dto.CreateUserSysNo);
            if (!string.IsNullOrEmpty(dto.CreateTimeFrom))
                ht.Add("DateFromCreate", dto.CreateTimeFrom);
            if (!string.IsNullOrEmpty(dto.CreateTimeTo))
                ht.Add("DateToCreate", dto.CreateTimeTo);
            if (!string.IsNullOrEmpty(dto.VendorSysNo))
                ht.Add("VendorSysNo", dto.VendorSysNo);
            if (!string.IsNullOrEmpty(dto.Status))
                ht.Add("Status", dto.Status);
            ht.Add("ShowNum", 2);
            #region 未实现查询逻辑
            //if (txtExpectInTime.Text.Trim() != "")
            //    ht.Add("ExpectInTime", txtExpectInTime.Text.Trim());
            //if (ucVendorStatus.StatusSysNo != AppConst.IntNull)
            //    ht.Add("VendorStatus", ucVendorStatus.StatusSysNo);
            //if (ucStock.StockSysNo != AppConst.IntNull)
            //    ht.Add("StockSysNo", ucStock.StockSysNo);
            //if (txtDateFromInStock.Text.Trim() != "")
            //    ht.Add("DateFromInStock", txtDateFromInStock.Text.Trim());
            //if (txtDateToInStock.Text.Trim() != "")
            //    ht.Add("DateToInStock", txtDateToInStock.Text.Trim());

            //if (ucCurrency.CurrencySysNo != AppConst.IntNull)
            //    ht.Add("CurrencySysNo", ucCurrency.CurrencySysNo);
            //if (ucYNStatus.YNSysNo != AppConst.IntNull)
            //    ht.Add("IsApportion", ucYNStatus.YNSysNo);
            #endregion

            var result = PurchaseManager.GetInstance().GetPODsApi(ht);
            var list = ExtensiveHelper.Map<DataTable, PoMasterBody>(result.Tables[0]);
            return list;
        }

        public PoMasterBody QueryPoMaster(PurchasePoMasterRquestDTO request)
        {
            var requestdto = request.ReqBody;
            Hashtable ht = new Hashtable(10);
            if (!string.IsNullOrEmpty(requestdto.CreateUserSysNo))
                ht.Add("CreateUserSysNo", requestdto.CreateUserSysNo);
            if (!string.IsNullOrEmpty(requestdto.VendorSysNo))
                ht.Add("VendorSysNo", requestdto.VendorSysNo);
            var result = PurchaseManager.GetInstance().LoadPO(int.Parse(request.ReqBody.SysNo));
            var poinfo = Helper.NoPropertyToMap<PoMasterBody, POInfo>(result);
            var vinfo = VendorManager.GetInstance().Load(poinfo.VendorSysNo);
            poinfo.V_VendorName = vinfo.VendorName;
            var dsitem = PurchaseManager.GetInstance().GetPoItemWithPoSysNo(request.ReqBody.SysNo);
            var itemlist=ExtensiveHelper.Map<DataTable, PoItemBody>(dsitem.Tables[0]);
            
            poinfo.poItems = itemlist;
            return poinfo;
        }

        public PurchasePoMasterResponseDTO InsertPoItem(PurchasePoMasterRquestDTO request)
        {
            var poinfo = Helper.MapNoProperty<POInfo, PoMasterBody>(request.ReqBodyDTO);
            var itemlist = Helper.MapNoProperty<POItemInfo, PoItemBody>(request.ReqBodyDTO.poItems);
            Hashtable ht = new Hashtable(10);
            foreach (var item in itemlist)
            {
                item.POSysNo = poinfo.SysNo;
                ht.Add(item.ProductSysNo, item);
            }
            string msg = PurchaseManager.GetInstance().VerifyPoAndItem(poinfo,ht);
            if (string.IsNullOrEmpty(msg))
            {
                //删除采购篮
                foreach (POItemInfo item in itemlist)
                {
                    PurchaseManager.GetInstance().InsertPOItem(poinfo, item);
                    PurchaseManager.GetInstance().DeletePOBasketAfterPO(item.ProductSysNo, poinfo.StockSysNo);
                }
            }
            request.ReqBodyDTO.SysNo = poinfo.SysNo;

            var result = new PurchasePoMasterResponseDTO
            {
                RetrunMsg = msg,
                ResMasterBody = new List<PoMasterBody> { request.ReqBodyDTO }
            };
            return result;
        }

        public void DeletePoItem(PurchasePoMasterRquestDTO request)
        {
            var poinfo = Helper.MapNoProperty<POInfo, PoMasterBody>(request.ReqBodyDTO);
            PurchaseManager.GetInstance().DeletePOItem(poinfo, int.Parse(request.ReqBody.ProductSysNo));
        }

        public PurchasePoMasterResponseDTO UpdatePoitem(PurchasePoMasterRquestDTO request)
        {
            var poinfo = Helper.MapNoProperty<POInfo, PoMasterBody>(request.ReqBodyDTO);
            var itemlist = Helper.MapNoProperty<POItemInfo, PoItemBody>(request.ReqBodyDTO.poItems);
            Hashtable ht = new Hashtable(10);
            foreach (var item in itemlist)
            {
                item.POSysNo = poinfo.SysNo;
                ht.Add(item.ProductSysNo, item);
            }
            poinfo.itemHash = ht;
            string msg = PurchaseManager.GetInstance().VerifyUpdatePoItem(poinfo, itemlist);
            
            request.ReqBodyDTO.SysNo = poinfo.SysNo;

            var result = new PurchasePoMasterResponseDTO
            {
                RetrunMsg = msg,
                ResMasterBody = new List<PoMasterBody> { request.ReqBodyDTO }
            };
            return result;
        }

        public string POApportionCost(PurchasePoMasterRquestDTO request)
        {
            //采购单摊销 只支持单个采购单摊销 批量摊销需要另写逻辑
            List<POApportionInfo> dellist = new List<POApportionInfo>();
            List<POInfo> delPoInfoList = new List<POInfo>();
            List<POApportionInfo> applist = new List<POApportionInfo>();
            List<POInfo> allPoList = new List<POInfo> { PurchaseManager.GetInstance().LoadPO(request.ReqBodyDTO.SysNo) };
            foreach (var item in request.ReqBodyDTO.appList)
            {
                var appitemlist = Helper.MapNoProperty<POApportionItemInfo, POApportionItemInfoBody>(item.itemList);
                var appinfo = Helper.MapNoProperty<POApportionInfo, POApportionInfoBody>(item);
                appinfo.POApportionItemInfoList = appitemlist;
                appinfo.POInfoList = allPoList;
                applist.Add(appinfo);
            }
            PurchaseManagerNew.GetInstance().ExportPOApportionNewApi(dellist,delPoInfoList,allPoList, applist, int.Parse(request.ReqBody.CreateUserSysNo));
            return "";
        }

        public void AbandonPo(PurchasePoMasterRquestDTO request)
        {
            PurchaseManager.GetInstance().Abandon(request.ReqBodyDTO.SysNo, int.Parse(request.UserSysNo));
            LogManager.GetInstance().Write(new LogInfo(request.ReqBodyDTO.SysNo, (int)AppEnum.LogType.Purchase_Abandon, new SessionInfo { User = new UserInfo { SysNo = int.Parse(request.UserSysNo) }, IpAddress = "小程序发起" }));
        }

        public void VerifyPo(PurchasePoMasterRquestDTO request)
        {
            var poinfo = Helper.MapNoProperty<POInfo, PoMasterBody>(request.ReqBodyDTO);
            var itemlist = Helper.MapNoProperty<POItemInfo, PoItemBody>(request.ReqBodyDTO.poItems);
            Hashtable ht = new Hashtable(10);
            foreach (var item in itemlist)
            {
                item.POSysNo = poinfo.SysNo;
                ht.Add(item.ProductSysNo, item);
            }
            poinfo.itemHash = ht;
            List<int> al= JsonConvert.DeserializeObject<List<int>>(request.ReqBody.PrivilegeList);
            PurchaseManager.GetInstance().VerifyPo(poinfo, al, int.Parse(request.UserSysNo));
        }

        public void CancelVerifyPo(PurchasePoMasterRquestDTO request)
        {
            PurchaseManagerNew.GetInstance().CancelVerifyNew(request.ReqBodyDTO.SysNo, int.Parse(request.UserSysNo), null);
            LogManager.GetInstance().Write(new LogInfo(request.ReqBodyDTO.SysNo, (int)AppEnum.LogType.Purchase_CancelVerify, new SessionInfo { User = new UserInfo { SysNo = int.Parse(request.UserSysNo) }, IpAddress = "小程序发起" }));
        }

        public void CancelAbandonPo(PurchasePoMasterRquestDTO request)
        {
            PurchaseManager.GetInstance().CancelAbandon(request.ReqBodyDTO.SysNo, int.Parse(request.UserSysNo));
            LogManager.GetInstance().Write(new LogInfo(request.ReqBodyDTO.SysNo, (int)AppEnum.LogType.Purchase_CancelAbandon, new SessionInfo { User = new UserInfo { SysNo = int.Parse(request.UserSysNo) }, IpAddress = "小程序发起" }));
        }
        
        public dynamic GetVerifyContent(PurchasePoMasterRquestDTO request)
        {
            var poinfo = Helper.MapNoProperty<POInfo, PoMasterBody>(request.ReqBodyDTO);
            var itemlist = Helper.MapNoProperty<POItemInfo, PoItemBody>(request.ReqBodyDTO.poItems);
            Hashtable ht = new Hashtable(10);
            foreach (var item in itemlist)
            {
                item.POSysNo = poinfo.SysNo;
                ht.Add(item.ProductSysNo, item);
            }
            poinfo.itemHash = ht;
            var PMLAuditContent = PurchaseManager.GetInstance().GetPMLAuditNote(poinfo);
            var PMDAuditContent = PurchaseManager.GetInstance().GetPMDAuditNoteWithNew(poinfo);
            return new
            {
                PMLAuditContent,
                PMDAuditContent
            };
        }
    }
}