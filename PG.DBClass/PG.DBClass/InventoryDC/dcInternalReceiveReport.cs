using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcInternalReceiveReport
    {
        private decimal m_Issue_Qty_Percent = 0;
        public string ISSUE_RECEIVE_NO { get; set; }
        public DateTime? ISSUE_RECEIVE_DATE { get; set; }
        public DateTime? ISSUE_RECEIVE_TIME { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string FromDept { get; set; }
        public string TODept { get; set; }
        public string ITEM_NAME { get; set; }
        public string REQ_NO { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public string ITEM_GROUP_CODE { get; set; }
        public string ITEM_CLASS_NAME { get; set; }
        public string ITEM_TYPE_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public Int64 REQ_ID { get; set; }

        public DateTime? REQ_DATE { get; set; }
        public decimal REQ_QNTY { get; set; }
        public DateTime? APPROVED_DATE { get; set; }
        public decimal REQ_APRV_QNTY { get; set; }
        public DateTime? CANCEL_DATE { get; set; }
        public string REQ_ISSUE_NO { get; set; }
        public Int64 REQ_ISSUE_ID { get; set; }
        public DateTime? REQ_ISSUE_DATE { get; set; }
        public DateTime? REQ_ISSUE_TIME { get; set; }
        public decimal ISSUE_QNTY { get; set; }
        public decimal RCV_QNTY { get; set; }
        public decimal REPORT_PERCENT { get; set; }

        public decimal ISSUE_QNTY_Percent { get; set; }
        public decimal RCV_QNTY_Percent { get; set; }

        public decimal MS_PRC { get; set; }
        public string TYPE_QTY { get; set; }
        public string IGR_STATUS { get; set; }
        public string FROM_DEPT { get; set; }
        public string TO_DEPT { get; set; }
        public DateTime? REQ_TIME { get; set; }
        public string ITC_NO { get; set; }
        public string IRR_NO { get; set; }
        public string IRR_STATUS { get; set; }
        public Int64 ISSUE_RECEIVE_ID { get; set; }
        public string FROM_DEPARTMENT_NAME { get; set; }
        public int FROM_DEPARTMENT_ID { get; set; }
        public string ITEM_SNS_NAME { get; set; }
        public string IS_CLOSE { get; set; }

        public string USER_NAME { get; set; }
        public string REQ_ISSUE_NOTE { get; set; }
         public string  DESIGNATION {get; set;}
         public string  REQ_ISSUE_REMARKS { get; set; }
         public string USER_ID { get; set; }
    }
}
