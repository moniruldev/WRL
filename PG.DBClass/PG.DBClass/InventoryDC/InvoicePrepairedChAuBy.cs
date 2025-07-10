using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
   public class InvoicePrepairedChAuBy
    {
       public string USER_NAME { get; set; }
       public string JOB_DESIGNATION { get; set; }
        public string CHECK_BY { get; set; }
        public string PREPARED_BY { get; set; }
        public string AUTHO_BY { get; set; }
        public string EMP_ID { get; set; }
        public string CHECKED_BY { get; set; }
    }
}
