using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
   public class dcGatePassPrepairedChAuBy
    {
        public string DC_NO { get; set; }
        public decimal CREATE_BY { get; set; }
        public string USERNAME { get; set; }
        public string DESIGNATION { get; set; }
        public string EMPID { get; set; }

    }
}
