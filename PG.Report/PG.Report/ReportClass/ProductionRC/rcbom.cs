using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.ProductionRC
{
     [Serializable]
    public class rcbom
    {
        public string BOM_ITEM_NAME { get; set; }
        public string BOM_ITEM_CODE { get; set; }
         
        public int BOM_DTL_ID { get; set; }
        public int BOM_MST_ID { get; set; }
        public int ITEM_ID { get; set; }
        public int BOM_ID { get; set; }
         
        public decimal ITEM_QTY { get; set; }
        public int ITEM_UNIT_ID { get; set; }
        public string IS_PRIME { get; set; }
        public int PACKAGE_ID { get; set; }

        public string REMARKS { get; set; }
        public int SLNO { get; set; }

        public string ITEM_CODE { get; set; }
        public int ITEM_BOM_ID { get; set; }
        public string ITEM_NAME { get; set; }

        public string UOM_NAME { get; set; }
        public string ITEM_GROUP_DESC { get; set; }
        
        public string BOM_ITEM_DESC { get; set; }
        //public string BOM_ITEM_NAME { get; set; }

        public DateTime? CREATE_DATE { get; set; }
        public string CREATE_BY { get; set; }

        public string BOM_NO { get; set; }

        public decimal ITEM_WEIGHT { get; set; }

         public string ISACTIVE { get; set; }

         public string BOM_VER { get; set; }
             

         
    }
}
