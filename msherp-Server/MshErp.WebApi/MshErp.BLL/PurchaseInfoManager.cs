using Eson.BLL.Purchase;
using Eson.Objects.Purchase;
using MshErp.BLL.Interface;
using MshErp.Model.DTO;
using RobotMapper;
using System;
using System.Collections.Generic;
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
    }
}