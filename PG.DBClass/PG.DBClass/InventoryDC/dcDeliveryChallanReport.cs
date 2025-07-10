using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcDeliveryChallanReport
    {
        public string DC_NO { get; set; }
        public DateTime? DC_DATE { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_ADDRESS { get; set; }
        public string CREATE_BY { get; set; }
        public string CUST_PHONE { get; set; }
        public string TRANSPORT_DETAILS { get; set; }
        public string GP_NO { get; set; }

        public string SALES_INVOICE_NO { get; set; }
        public string DC_REMARKS { get; set; }
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string ITEM_TYPE_CODE { get; set; }
        public string DC_NO_MST { get; set; }
        public string RTN_NO { get; set; }
        public string SUP_NAME { get; set; }
        public string SUP_ADDRESS { get; set; }
        public string SUP_PHONE { get; set; }
        public string USERNAME { get; set; }
        public string DESIGNATION { get; set; }
        public string EMPID { get; set; }
        public decimal RTN_QNTY { get; set; }
        public DateTime? RTN_DATE { get; set; }
         
             
             
             
         
         
    }
}
