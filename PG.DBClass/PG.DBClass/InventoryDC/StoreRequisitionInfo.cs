using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class StoreRequisitionInfo
    {
        public string BRANCH_NAME { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string REQ_NO { get; set; }
        public DateTime? REQ_DATE { get; set; }
        public string ISSUE_NO { get; set; }
        public DateTime? ISSUE_DATE { get; set; }
        public string RCV_NO { get; set; }
        public DateTime? RCV_DATE { get; set; }
        public decimal? REQ_QNTY { get; set; }
        public string ITEM_DESC { get; set; }
        public decimal? ISSUE_QNTY { get; set; }
        public decimal? RCV_QNTY { get; set; }
        public DateTime? APRV_DATE { get; set; }
        public decimal? REQ_APRV_QNTY { get; set; }
        public DateTime? CNCL_DATE { get; set; }
        
       
    }
}
