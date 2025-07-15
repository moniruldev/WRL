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
        private string m_SHIPPER_ADDRESS = string.Empty;
        private string m_SHIPPER_MOBILE_NO = string.Empty;
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

        [DBColumn(Name = "SHIPPER_ADDRESS", Storage = "m_SHIPPER_ADDRESS", DbType = "126")]
        public string SHIPPER_ADDRESS
        {
            get { return this.m_SHIPPER_ADDRESS; }
            set
            {
                this.m_SHIPPER_ADDRESS = value;
                this.NotifyPropertyChanged("SHIPPER_ADDRESS");
            }
        }

        [DBColumn(Name = "SHIPPER_MOBILE_NO", Storage = "m_SHIPPER_MOBILE_NO", DbType = "126")]
        public string SHIPPER_MOBILE_NO
        {
            get { return this.m_SHIPPER_MOBILE_NO; }
            set
            {
                this.m_SHIPPER_MOBILE_NO = value;
                this.NotifyPropertyChanged("SHIPPER_MOBILE_NO");
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

        #endregion //properties
    }
}
