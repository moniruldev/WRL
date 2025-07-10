using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.InventoryRC
{
     [Serializable]
   public class rcProduction
    {
       public string ITEM_NAME { get; set; }
       public decimal ISSUE_QNTY_Percent { get; set; }
       public DateTime? REQ_ISSUE_DATE { get; set; }

    }
}
