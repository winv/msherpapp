using Eson.Objects.Basic;
using Eson.Objects.Purchase;
using Eson.Objects.Sale;
using Eson.Objects.Stock;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MshErp.Model.DTO
{
    public class UserSessionDTO : BaseDTO
    {
        public UserBasic User { get; set; }
        public string IpAddress { get; set; }
        public ArrayList PrivilegeAL { get; set; }
        public ArrayList CopyUserAL { get; set; }
        public ArrayList CopyPrivilegeAL { get; set; }
        public LendInfo sLend { get; set; }
        public AdjustInfo sAdjust { get; set; }
        public ShiftInfo sShift { get; set; }
        public TransferInfo sTransfer { get; set; }
        public POInfo sPO { get; set; }
        public SOInfo sSO { get; set; }
        public CustomerInfo sCustomer { get; set; }
        public RMAInfo sRMA { get; set; }
        public Hashtable InvoiceItemHt { get; set; }
    }
}