﻿using MshErp.Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.BLL.Interface
{
    public interface IPurchaseInfoManager : IDependency
    {
        PurchaseBasketResponseBody GetPoBasketInfo(PurchaseBasketRequstDTO request);

        List<PurchaseBasketResponseBody> GetPoBasketList(PurchaseBasketRequstDTO request);

        int GetPoBasketCount(PurchaseBasketRequstDTO request);

        PurchaseBasketResponseBody InsertPoBasketInfo(PurchaseBasketRequstDTO request);

        bool DeletePoBasketInfo(PurchaseBasketRequstDTO request);

        List<PoItemBody> QueryPoItemListWithBaskt(PurchaseBasketRequstDTO request);

        PurchasePoMasterResponseDTO InsertPoMaster(PurchasePoMasterRquestDTO request);

        PurchasePoMasterResponseDTO InsertPoItem(PurchasePoMasterRquestDTO request);

        List<PoMasterBody> QueryPoList(PurchasePoMasterRquestDTO request);

        PoMasterBody QueryPoMaster(PurchasePoMasterRquestDTO request);

        void DeletePoItem(PurchasePoMasterRquestDTO request);

        PurchasePoMasterResponseDTO UpdatePoitem(PurchasePoMasterRquestDTO request);

        string POApportionCost(PurchasePoMasterRquestDTO request);

        void AbandonPo(PurchasePoMasterRquestDTO request);
        void CancelAbandonPo(PurchasePoMasterRquestDTO request);
        void VerifyPo(PurchasePoMasterRquestDTO request);
        void CancelVerifyPo(PurchasePoMasterRquestDTO request);
        dynamic GetVerifyContent(PurchasePoMasterRquestDTO request);
    }
}