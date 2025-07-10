using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.Report.ReportClass.InventoryRC
{
    [Serializable]
    public class rcLcCosting
    {
        public string COSTING_NO { get; set; }
        public string B_E_NO { get; set; }
        public string BILL_NO { get; set; }
        public string LC_NO { get; set; }
        public string SUP_NAME { get; set; }
        public string ITEM_NAME { get; set; }
        public Int64 MONTH_YEAR { get; set; }
        public string MONTHS { get; set; }
        public decimal ITEM_QTY { get; set; }
        public decimal UNIT_RATE { get; set; }
        public decimal LC_RATE { get; set; }
        public string UOM_NAME { get; set; }
        public string CONVERTED_UOM_NAME { get; set; }
        public decimal INVOICE_VALUED_USD { get; set; }
        public decimal CONVERSION_RATE { get; set; }
        public decimal INVOICE_VALUE_BDT { get; set; }
        public decimal TOTAL_COST_WO_VAT { get; set; }
        public decimal LANDED_COST { get; set; }
        public decimal FACTOR { get; set; }
        public string COSTING_ITEM_CAT_NAME { get; set; }   
        public decimal ASSESSABLE_VALUE { get; set; }
        public decimal GLOBAL_TAXES { get; set; }
        public decimal CUSTOMS_DUTY { get; set; }
        public decimal RD_IMPORT { get; set; }
        public decimal SD_IMPORT { get; set; }
        public decimal INPUT_VAT_RECEIVEABLE { get; set; }
        public decimal AIT_IMPORT { get; set; }
        public decimal AT_IMPORT { get; set; }
        public decimal MARINE_INSURANCE { get; set; }
        public decimal SEA_FREIGHT { get; set; }
        public decimal TOTAL_CLEARING_CHARGE { get; set; }
        public decimal CARRIGE_INWARD_IMPORT { get; set; }
        public decimal TOTAL { get; set; }
         
        
    }
}
