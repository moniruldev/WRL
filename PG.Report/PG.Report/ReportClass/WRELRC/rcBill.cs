using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.Report.ReportClass.WRELRC
{
     [Serializable]
    public class rcBill
    {
         public DateTime? BOOKING_DATE { get; set; }
         public string CN_NUMBER { get; set; }
         public string DEPT { get; set; }
         public string BOOKING { get; set; }

         public string DESTINATION { get; set; }
         public string ITEM_NAME { get; set; }
         public string UOM_NAME { get; set; }
         
        public decimal SERVICE_CHARGE_AMT_DEFAULT { get; set; }
        public decimal WEIGHT { get; set; }
        public Int32 QUANTITY { get; set; }
        public Int32 RATE { get; set; }
        public Int32 TAKA { get; set; }
        public string CLIENT_NAME { get; set; }
         
             
    }
}
