using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    public class dcPurchaseAgainstIndentReport
    {
        public string DEPARTMENT_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }

        public string ITEM_NAME { get; set; }

        public int INDT_QTY { get; set; }
        public string INDT_NO { get; set; }
        public DateTime? INDT_DATE { get; set; }

        public string DEPT_ID { get; set; }
        public string PURCHASE_NO { get; set; }
        public DateTime? PURCHASE_DATE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        
        public int PURCHASE_QTY { get; set; }

        public int SUM_OF_INDENT { get; set; }

        public int PURCHASE_ID { get; set; }
        public int SUP_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string UOM_CODE { get; set; }
        public string SUP_NAME { get; set; }
        public string SUP_ADDRESS { get; set; }
        public string DELIVERY_LOCATION { get; set; }
        public string REMARKS { get; set; }
        public string PURCHASE_TYPE { get; set; }
        public decimal UNIT_RATE { get; set; }
        public decimal TOTAL_COST { get; set; }
        
       
        public string CONTACT_NO { get; set; }
        public string PURCHASE_NAME { get; set; }
        public string APPROVED_STATUS_PUR_H { get; set; }
        public DateTime? APPROVED_DATE_PUR_H { get; set; }
        public string PURCHASE_HEAD_NAME { get; set; }
        public string APPROVED_STATUS_FINANCE_H { get; set; }
       
        public DateTime? APPROVED_DATE_FINANCE_H { get; set; }
        public string FINANCE_HEAD_NAME { get; set; }
        public string APPROVED_STATUS_BUSINESS_H { get; set; }
        public DateTime? APPROVED_DATE_BUSINESS_H { get; set; }
        public string BUSINESS_HEAD_NAME { get; set; }
        public string APPROVED_STATUS_DIRECTOR { get; set; }
        public DateTime? APPROVED_DATE_DIRECTOR { get; set; }
        public string DIRECTOR_NAME { get; set; }
        public string DIRECTOR_DESIGNATION { get; set; }
        public string PURCHASE_DESIGNATION { get; set; }
        public string PURCHASE_HEAD_DESIGNATION { get; set; }
        public string FINANCE_HEAD_DESIGNATION { get; set; }
        public string BUSINESS_HEAD_DESIGNATION { get; set; }
        public string DESCRIPTION { get; set; }
        public int SL_NO { get; set; }

        public byte[] SIGN_PHOTO { get; set; }
        public byte[] SIGN_PHOTO_PUR_H { get; set; }
        public byte[] SIGN_PHOTO_FIN_H { get; set; }
        public byte[] SIGN_PHOTO_DIR { get; set; }
        public decimal SP_DISCOUNT { get; set; }
        public decimal PRICE_EX_VAT_AIT { get; set; }
        public decimal VAT_AIT { get; set; }
        public decimal INC_VAT_AIT { get; set; }
        public string PO_TYPE { get; set; }
        public string APPROVED_STATUS_FINANCE_DIR { get; set; }
        public DateTime? APPROVED_DATE_FINANCE_DIR { get; set; }
        public string FINANCE_DIR_NAME { get; set; }
        public string FINANCE_DIR_DESIGNATION { get; set; }
        public byte[] SIGN_PHOTO_FINANCE_DIR { get; set; }
        public string VAT_AIT_LABEL { get; set; }
        
    }
}
