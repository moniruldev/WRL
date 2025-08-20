using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CN_CREATION_MST")]
    public partial class dcCN_CREATION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CN_ID = 0;
        private string m_CN_NUMBER = string.Empty;
        private int m_CLIENT_ID = 0;
        private int m_AGR_DETAIL_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_SERVICE_AMOUNT = 0;
        private int m_ROUTE_ID = 0;
        private string m_CONSIGNEE_NAME = string.Empty;
        private string m_CONSIGNEE_ADDRESS = string.Empty;
        private string m_CONSIGNEE_MOBILE_NO = string.Empty;
        private decimal m_DESTINATION_DIST_ID = 0;
        private decimal m_DESTINATION_TOWN_ID = 0;
        private string m_SMS_AT_START_SENT = string.Empty;
        private string m_SMS_AT_DELIVERY_SENT = string.Empty;
        private string m_OTP_AT_DELIVERED = string.Empty;
        private string m_IS_BILL_GENERATED = string.Empty;
        private string m_BILL_NO = string.Empty;
        private DateTime? m_BILL_GENERATE_DATE = null;
        private string m_BILL_GENERATED_BY = string.Empty;
        private string m_INVOICE_NO = string.Empty;
        private string m_IS_REFUND = string.Empty;
        private decimal m_REFUND_CAUSE_ID = 0;
        private DateTime? m_REFUND_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;
        private string m_OTP_SUCCESSFUL = string.Empty;
        private decimal m_RETURN_SERVICE_AMOUNT = 0;
        private string m_IS_DELIVERED = string.Empty;
        private string m_POD = string.Empty;
        private DateTime? m_DELIVERY_DATE = null;
        private decimal m_SLA_DAYS = 0;
        private DateTime? m_PICKUP_DATE = null;
        private string m_PICKUP_BY = string.Empty;
        private DateTime? m_BOOKING_DATE = null;
        private string m_CN_CLIENT_CODE = string.Empty;
        private string m_PRODUCT_TYPE = string.Empty;
        private string m_UPS = string.Empty;
        private string m_STATUS = string.Empty;
        private string m_NARRATION = string.Empty;
        private string m_ITEM_NAME = string.Empty;
        private int m_CLIENT_DEPT_ID = 0;
        private int m_HUB_ID = 0;
        private string m_DESTINATION = string.Empty;
        private string m_REF_TYPE = string.Empty;
        private int m_DISTANCE_TYPE_ID = 0;
        private string m_UPDATE_BY_REQUEST = string.Empty;
        private DateTime? m_UPDATE_DATE_REQUEST = null;
        

        #endregion  //private members

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            _UpdateChangedList(info);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion //public events

        #region properties


        [DBColumn(Name = "CN_ID", Storage = "m_CN_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CN_ID
        {
            get { return this.m_CN_ID; }
            set
            {
                this.m_CN_ID = value;
                this.NotifyPropertyChanged("CN_ID");
            }
        }

        [DBColumn(Name = "CN_NUMBER", Storage = "m_CN_NUMBER", DbType = "107")]
        public string CN_NUMBER
        {
            get { return this.m_CN_NUMBER; }
            set
            {
                this.m_CN_NUMBER = value;
                this.NotifyPropertyChanged("CN_NUMBER");
            }
        }

        [DBColumn(Name = "CLIENT_ID", Storage = "m_CLIENT_ID", DbType = "107")]
        public int CLIENT_ID
        {
            get { return this.m_CLIENT_ID; }
            set
            {
                this.m_CLIENT_ID = value;
                this.NotifyPropertyChanged("CLIENT_ID");
            }
        }

        [DBColumn(Name = "AGR_DETAIL_ID", Storage = "m_AGR_DETAIL_ID", DbType = "107")]
        public int AGR_DETAIL_ID
        {
            get { return this.m_AGR_DETAIL_ID; }
            set
            {
                this.m_AGR_DETAIL_ID = value;
                this.NotifyPropertyChanged("AGR_DETAIL_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "SERVICE_AMOUNT", Storage = "m_SERVICE_AMOUNT", DbType = "107")]
        public decimal SERVICE_AMOUNT
        {
            get { return this.m_SERVICE_AMOUNT; }
            set
            {
                this.m_SERVICE_AMOUNT = value;
                this.NotifyPropertyChanged("SERVICE_AMOUNT");
            }
        }

        [DBColumn(Name = "ROUTE_ID", Storage = "m_ROUTE_ID", DbType = "107")]
        public int ROUTE_ID
        {
            get { return this.m_ROUTE_ID; }
            set
            {
                this.m_ROUTE_ID = value;
                this.NotifyPropertyChanged("ROUTE_ID");
            }
        }

        [DBColumn(Name = "CONSIGNEE_NAME", Storage = "m_CONSIGNEE_NAME", DbType = "126")]
        public string CONSIGNEE_NAME
        {
            get { return this.m_CONSIGNEE_NAME; }
            set
            {
                this.m_CONSIGNEE_NAME = value;
                this.NotifyPropertyChanged("CONSIGNEE_NAME");
            }
        }

     

        [DBColumn(Name = "CONSIGNEE_ADDRESS", Storage = "m_CONSIGNEE_ADDRESS", DbType = "126")]
        public string CONSIGNEE_ADDRESS
        {
            get { return this.m_CONSIGNEE_ADDRESS; }
            set
            {
                this.m_CONSIGNEE_ADDRESS = value;
                this.NotifyPropertyChanged("CONSIGNEE_ADDRESS");
            }
        }

        [DBColumn(Name = "CONSIGNEE_MOBILE_NO", Storage = "m_CONSIGNEE_MOBILE_NO", DbType = "126")]
        public string CONSIGNEE_MOBILE_NO
        {
            get { return this.m_CONSIGNEE_MOBILE_NO; }
            set
            {
                this.m_CONSIGNEE_MOBILE_NO = value;
                this.NotifyPropertyChanged("CONSIGNEE_MOBILE_NO");
            }
        }

        [DBColumn(Name = "DESTINATION_DIST_ID", Storage = "m_DESTINATION_DIST_ID", DbType = "107")]
        public decimal DESTINATION_DIST_ID
        {
            get { return this.m_DESTINATION_DIST_ID; }
            set
            {
                this.m_DESTINATION_DIST_ID = value;
                this.NotifyPropertyChanged("DESTINATION_DIST_ID");
            }
        }

        [DBColumn(Name = "DESTINATION_TOWN_ID", Storage = "m_DESTINATION_TOWN_ID", DbType = "107")]
        public decimal DESTINATION_TOWN_ID
        {
            get { return this.m_DESTINATION_TOWN_ID; }
            set
            {
                this.m_DESTINATION_TOWN_ID = value;
                this.NotifyPropertyChanged("DESTINATION_TOWN_ID");
            }
        }

        [DBColumn(Name = "SMS_AT_START_SENT", Storage = "m_SMS_AT_START_SENT", DbType = "126")]
        public string SMS_AT_START_SENT
        {
            get { return this.m_SMS_AT_START_SENT; }
            set
            {
                this.m_SMS_AT_START_SENT = value;
                this.NotifyPropertyChanged("SMS_AT_START_SENT");
            }
        }

        [DBColumn(Name = "SMS_AT_DELIVERY_SENT", Storage = "m_SMS_AT_DELIVERY_SENT", DbType = "126")]
        public string SMS_AT_DELIVERY_SENT
        {
            get { return this.m_SMS_AT_DELIVERY_SENT; }
            set
            {
                this.m_SMS_AT_DELIVERY_SENT = value;
                this.NotifyPropertyChanged("SMS_AT_DELIVERY_SENT");
            }
        }

        [DBColumn(Name = "OTP_AT_DELIVERED", Storage = "m_OTP_AT_DELIVERED", DbType = "126")]
        public string OTP_AT_DELIVERED
        {
            get { return this.m_OTP_AT_DELIVERED; }
            set
            {
                this.m_OTP_AT_DELIVERED = value;
                this.NotifyPropertyChanged("OTP_AT_DELIVERED");
            }
        }

        [DBColumn(Name = "IS_BILL_GENERATED", Storage = "m_IS_BILL_GENERATED", DbType = "126")]
        public string IS_BILL_GENERATED
        {
            get { return this.m_IS_BILL_GENERATED; }
            set
            {
                this.m_IS_BILL_GENERATED = value;
                this.NotifyPropertyChanged("IS_BILL_GENERATED");
            }
        }

        [DBColumn(Name = "BILL_NO", Storage = "m_BILL_NO", DbType = "126")]
        public string BILL_NO
        {
            get { return this.m_BILL_NO; }
            set
            {
                this.m_BILL_NO = value;
                this.NotifyPropertyChanged("BILL_NO");
            }
        }

        [DBColumn(Name = "BILL_GENERATE_DATE", Storage = "m_BILL_GENERATE_DATE", DbType = "106")]
        public DateTime? BILL_GENERATE_DATE
        {
            get { return this.m_BILL_GENERATE_DATE; }
            set
            {
                this.m_BILL_GENERATE_DATE = value;
                this.NotifyPropertyChanged("BILL_GENERATE_DATE");
            }
        }

        [DBColumn(Name = "BILL_GENERATED_BY", Storage = "m_BILL_GENERATED_BY", DbType = "126")]
        public string BILL_GENERATED_BY
        {
            get { return this.m_BILL_GENERATED_BY; }
            set
            {
                this.m_BILL_GENERATED_BY = value;
                this.NotifyPropertyChanged("BILL_GENERATED_BY");
            }
        }

        [DBColumn(Name = "INVOICE_NO", Storage = "m_INVOICE_NO", DbType = "126")]
        public string INVOICE_NO
        {
            get { return this.m_INVOICE_NO; }
            set
            {
                this.m_INVOICE_NO = value;
                this.NotifyPropertyChanged("INVOICE_NO");
            }
        }

        [DBColumn(Name = "IS_REFUND", Storage = "m_IS_REFUND", DbType = "126")]
        public string IS_REFUND
        {
            get { return this.m_IS_REFUND; }
            set
            {
                this.m_IS_REFUND = value;
                this.NotifyPropertyChanged("IS_REFUND");
            }
        }

        [DBColumn(Name = "REFUND_CAUSE_ID", Storage = "m_REFUND_CAUSE_ID", DbType = "107")]
        public decimal REFUND_CAUSE_ID
        {
            get { return this.m_REFUND_CAUSE_ID; }
            set
            {
                this.m_REFUND_CAUSE_ID = value;
                this.NotifyPropertyChanged("REFUND_CAUSE_ID");
            }
        }

        [DBColumn(Name = "REFUND_DATE", Storage = "m_REFUND_DATE", DbType = "106")]
        public DateTime? REFUND_DATE
        {
            get { return this.m_REFUND_DATE; }
            set
            {
                this.m_REFUND_DATE = value;
                this.NotifyPropertyChanged("REFUND_DATE");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        [DBColumn(Name = "OTP_SUCCESSFUL", Storage = "m_OTP_SUCCESSFUL", DbType = "126")]
        public string OTP_SUCCESSFUL
        {
            get { return this.m_OTP_SUCCESSFUL; }
            set
            {
                this.m_OTP_SUCCESSFUL = value;
                this.NotifyPropertyChanged("OTP_SUCCESSFUL");
            }
        }

        [DBColumn(Name = "RETURN_SERVICE_AMOUNT", Storage = "m_RETURN_SERVICE_AMOUNT", DbType = "107")]
        public decimal RETURN_SERVICE_AMOUNT
        {
            get { return this.m_RETURN_SERVICE_AMOUNT; }
            set
            {
                this.m_RETURN_SERVICE_AMOUNT = value;
                this.NotifyPropertyChanged("RETURN_SERVICE_AMOUNT");
            }
        }

        [DBColumn(Name = "IS_DELIVERED", Storage = "m_IS_DELIVERED", DbType = "126")]
        public string IS_DELIVERED
        {
            get { return this.m_IS_DELIVERED; }
            set
            {
                this.m_IS_DELIVERED = value;
                this.NotifyPropertyChanged("IS_DELIVERED");
            }
        }

        [DBColumn(Name = "POD", Storage = "m_POD", DbType = "102")]
        public string POD
        {
            get { return this.m_POD; }
            set
            {
                this.m_POD = value;
                this.NotifyPropertyChanged("POD");
            }
        }

        [DBColumn(Name = "DELIVERY_DATE", Storage = "m_DELIVERY_DATE", DbType = "106")]
        public DateTime? DELIVERY_DATE
        {
            get { return this.m_DELIVERY_DATE; }
            set
            {
                this.m_DELIVERY_DATE = value;
                this.NotifyPropertyChanged("DELIVERY_DATE");
            }
        }

        [DBColumn(Name = "SLA_DAYS", Storage = "m_SLA_DAYS", DbType = "107")]
        public decimal SLA_DAYS
        {
            get { return this.m_SLA_DAYS; }
            set
            {
                this.m_SLA_DAYS = value;
                this.NotifyPropertyChanged("SLA_DAYS");
            }
        }

        [DBColumn(Name = "PICKUP_DATE", Storage = "m_PICKUP_DATE", DbType = "106")]
        public DateTime? PICKUP_DATE
        {
            get { return this.m_PICKUP_DATE; }
            set
            {
                this.m_PICKUP_DATE = value;
                this.NotifyPropertyChanged("PICKUP_DATE");
            }
        }

        [DBColumn(Name = "PICKUP_BY", Storage = "m_PICKUP_BY", DbType = "102")]
        public string PICKUP_BY
        {
            get { return this.m_PICKUP_BY; }
            set
            {
                this.m_PICKUP_BY = value;
                this.NotifyPropertyChanged("PICKUP_BY");
            }
        }

        [DBColumn(Name = "BOOKING_DATE", Storage = "m_BOOKING_DATE", DbType = "106")]
        public DateTime? BOOKING_DATE
        {
            get { return this.m_BOOKING_DATE; }
            set
            {
                this.m_BOOKING_DATE = value;
                this.NotifyPropertyChanged("BOOKING_DATE");
            }
        }

        [DBColumn(Name = "CN_CLIENT_CODE", Storage = "m_CN_CLIENT_CODE", DbType = "102")]
        public string CN_CLIENT_CODE
        {
            get { return this.m_CN_CLIENT_CODE; }
            set
            {
                this.m_CN_CLIENT_CODE = value;
                this.NotifyPropertyChanged("CN_CLIENT_CODE");
            }
        }

        [DBColumn(Name = "PRODUCT_TYPE", Storage = "m_PRODUCT_TYPE", DbType = "102")]
        public string PRODUCT_TYPE
        {
            get { return this.m_PRODUCT_TYPE; }
            set
            {
                this.m_PRODUCT_TYPE = value;
                this.NotifyPropertyChanged("PRODUCT_TYPE");
            }
        }

        [DBColumn(Name = "UPS", Storage = "m_UPS", DbType = "102")]
        public string UPS
        {
            get { return this.m_UPS; }
            set
            {
                this.m_UPS = value;
                this.NotifyPropertyChanged("UPS");
            }
        }
        [DBColumn(Name = "STATUS", Storage = "m_STATUS", DbType = "102")]
        public string STATUS
        {
            get { return this.m_STATUS; }
            set
            {
                this.m_STATUS = value;
                this.NotifyPropertyChanged("STATUS");
            }
        }
        [DBColumn(Name = "NARRATION", Storage = "m_NARRATION", DbType = "102")]
        public string NARRATION
        {
            get { return this.m_NARRATION; }
            set
            {
                this.m_NARRATION = value;
                this.NotifyPropertyChanged("NARRATION");
            }
        }

        [DBColumn(Name = "ITEM_NAME", Storage = "m_ITEM_NAME", DbType = "102")]
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
                this.NotifyPropertyChanged("ITEM_NAME");
            }
        }

       
         [DBColumn(Name = "CLIENT_DEPT_ID", Storage = "m_CLIENT_DEPT_ID", DbType = "107")]
         public int CLIENT_DEPT_ID
         {
             get { return this.m_CLIENT_DEPT_ID; }
             set
             {
                 this.m_CLIENT_DEPT_ID = value;
                 this.NotifyPropertyChanged("CLIENT_DEPT_ID");
             }
         }

        [DBColumn(Name = "HUB_ID", Storage = "m_HUB_ID", DbType = "107")]
         public int HUB_ID
         {
             get { return this.m_HUB_ID; }
             set
             {
                 this.m_HUB_ID = value;
                 this.NotifyPropertyChanged("HUB_ID");
             }
         }

       
        [DBColumn(Name = "DESTINATION", Storage = "m_DESTINATION", DbType = "102")]
        public string DESTINATION
        {
            get { return this.m_DESTINATION; }
            set
            {
                this.m_DESTINATION = value;
                this.NotifyPropertyChanged("DESTINATION");
            }
        }
        [DBColumn(Name = "REF_TYPE", Storage = "m_REF_TYPE", DbType = "102")]
        public string REF_TYPE
        {
            get { return this.m_REF_TYPE; }
            set
            {
                this.m_REF_TYPE = value;
                this.NotifyPropertyChanged("REF_TYPE");
            }
        }
        [DBColumn(Name = "DISTANCE_TYPE_ID", Storage = "m_DISTANCE_TYPE_ID", DbType = "107")]
        public int DISTANCE_TYPE_ID
         {
             get { return this.m_DISTANCE_TYPE_ID; }
             set
             {
                 this.m_DISTANCE_TYPE_ID = value;
                 this.NotifyPropertyChanged("DISTANCE_TYPE_ID");
             }
         }

        [DBColumn(Name = "UPDATE_BY_REQUEST", Storage = "m_UPDATE_BY_REQUEST", DbType = "126")]
        public string UPDATE_BY_REQUEST
        {
            get { return this.m_UPDATE_BY_REQUEST; }
            set
            {
                this.m_UPDATE_BY_REQUEST = value;
                this.NotifyPropertyChanged("UPDATE_BY_REQUEST");
            }
        }

        [DBColumn(Name = "UPDATE_DATE_REQUEST", Storage = "m_UPDATE_DATE_REQUEST", DbType = "106")]
        public DateTime? UPDATE_DATE_REQUEST
        {
            get { return this.m_UPDATE_DATE_REQUEST; }
            set
            {
                this.m_UPDATE_DATE_REQUEST = value;
                this.NotifyPropertyChanged("UPDATE_DATE_REQUEST");
            }
        }

        
        #endregion //properties
    }

     public partial class dcCN_CREATION_MST
     {
         private string m_DESTINATION_DIST_NAME = "";
         private string m_DESTINATION_TOWN_NAME = "";
         private string m_CLIENT_NAME = "";
         private string m_AGREEMENT_DESCRIPTION = "";
         private int m_STEP_NUMBER = 0;
       
         private string m_ROUTE_NAME = "";
         private string m_HUB_NAME = "";

         public string DESTINATION_DIST_NAME
         {
             get { return this.m_DESTINATION_DIST_NAME; }
             set { this.m_DESTINATION_DIST_NAME = value; }
         }

         public string DESTINATION_TOWN_NAME
         {
             get { return this.m_DESTINATION_TOWN_NAME; }
             set { this.m_DESTINATION_TOWN_NAME = value; }
         }

         public string CLIENT_NAME
         {
             get { return this.m_CLIENT_NAME; }
             set { this.m_CLIENT_NAME = value; }
         }

         public string AGREEMENT_DESCRIPTION
         {
             get { return this.m_AGREEMENT_DESCRIPTION; }
             set { this.m_AGREEMENT_DESCRIPTION = value; }
         }

        

         public string ROUTE_NAME
         {
             get { return this.m_ROUTE_NAME; }
             set { this.m_ROUTE_NAME = value; }
         }

         public string HUB_NAME
         {
             get { return this.m_HUB_NAME; }
             set { this.m_HUB_NAME = value; }
         }
         public int STEP_NUMBER
         {
             get { return this.m_STEP_NUMBER; }
             set { this.m_STEP_NUMBER = value; }
         }

         public int SLNO { get; set; }
         public string DEPT_NAME { get; set; }
         public string DISTANCE_TYPE_NAME { get; set; }
         
     }
}
