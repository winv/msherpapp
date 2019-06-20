using MshErp.Model.DTO;
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
    }
}