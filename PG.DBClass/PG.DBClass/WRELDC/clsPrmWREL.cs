using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    public class clsPrmWREL
    {
        public int TRANS_ID = 0;
        public string TRANS_NO = string.Empty;
        public DateTime? FromDate = null;
        public DateTime? ToDate = null;
        public string Status = string.Empty;
        public int CN_ID = 0;
        private int m_CARGO_ID = 0;
        private string m_CARGO_NUMBER = string.Empty;
        private DateTime? m_CARGO_DATE = null;
        private string m_DIST_NAME = string.Empty;
        private decimal m_WEIGHT_IN_KG = 0;
        private string m_CN_NUMBER = string.Empty;
        private string m_CONSIGNEE_NAME = string.Empty;
        private string m_CONSIGNEE_ADDRESS = string.Empty;
        private string m_CONSIGNEE_MOBILE_NO = string.Empty;
        private string m_REMARKS = string.Empty;
        private int m_CLIENT_ID = 0;
        private string m_CLIENT_NAME = string.Empty;
        private string m_DEPARTMENT = string.Empty;
        private string m_ITEM_NAME = string.Empty;
        private decimal m_SERVICE_AMOUNT = 0;
        private string m_USER_TYPE = string.Empty;

        public int CARGO_ID
        {
            get { return this.m_CARGO_ID; }
            set { this.m_CARGO_ID = value; }
        }

        public string CARGO_NUMBER
        {
            get { return this.m_CARGO_NUMBER; }
            set { this.m_CARGO_NUMBER = value; }
        }

        public DateTime? CARGO_DATE
        {
            get { return this.m_CARGO_DATE; }
            set { this.m_CARGO_DATE = value; }
        }

        public string DIST_NAME
        {
            get { return this.m_DIST_NAME; }
            set { this.m_DIST_NAME = value; }
        }

        public decimal WEIGHT_IN_KG
        {
            get { return this.m_WEIGHT_IN_KG; }
            set { this.m_WEIGHT_IN_KG = value; }
        }

        public string CN_NUMBER
        {
            get { return this.m_CN_NUMBER; }
            set { this.m_CN_NUMBER = value; }
        }

        public string CONSIGNEE_NAME
        {
            get { return this.m_CONSIGNEE_NAME; }
            set { this.m_CONSIGNEE_NAME = value; }
        }

        public string CONSIGNEE_ADDRESS
        {
            get { return this.m_CONSIGNEE_ADDRESS; }
            set { this.m_CONSIGNEE_ADDRESS = value; }
        }

        public string CONSIGNEE_MOBILE_NO
        {
            get { return this.m_CONSIGNEE_MOBILE_NO; }
            set { this.m_CONSIGNEE_MOBILE_NO = value; }
        }

        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set { this.m_REMARKS = value; }
        }
        public int CLIENT_ID
        {
            get { return this.m_CLIENT_ID; }
            set { this.m_CLIENT_ID = value; }
        }
        public string CLIENT_NAME
        {
            get { return this.m_CLIENT_NAME; }
            set { this.m_CLIENT_NAME = value; }
        }
        public string DEPARTMENT
        {
            get { return this.m_DEPARTMENT; }
            set { this.m_DEPARTMENT = value; }
        }
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set { this.m_ITEM_NAME = value; }
        }
        public decimal SERVICE_AMOUNT
        {
            get { return this.m_SERVICE_AMOUNT; }
            set { this.m_SERVICE_AMOUNT = value; }
        }

        public string USER_TYPE
        {
            get { return this.m_USER_TYPE; }
            set { this.m_USER_TYPE = value; }
        }
    }
}
