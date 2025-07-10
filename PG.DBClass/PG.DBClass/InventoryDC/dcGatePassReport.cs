using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcGatePassReport
    {
        public string GP_NO { get; set; }
        public DateTime? GP_DATE { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_ADDRESS { get; set; }
        public string ItemDetail { get; set; }
        public string CREATE_BY { get; set; }
        public string DC_NO { get; set; }
        public string CUST_PHONE { get; set; }
        public string TRANSPORT_DETAILS { get; set; }
        public string DC_REMARKS { get; set; }
        
    }
}
