using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "TEMP_CN_INFO")]
    public partial class dcTEMP_CN_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_SLNO = 0;
        private DateTime? m_PICKUP_DATE = null;
        private string m_CN_CLIENT_CODE = string.Empty;
        private string m_CN_NAME = string.Empty;
        private string m_CN_MOBILE_NO = string.Empty;
        private string m_ADDRESS = string.Empty;
        private string m_ITEM_NAME = string.Empty;
        private string m_PRODUCT_TYPE = string.Empty;
        private string m_UPS = string.Empty;
        private string m_DESTINATION = string.Empty;
        private decimal m_SLA_BREEZE = 0;
        private string m_STATUS = string.Empty;
        private string m_NARRATION = string.Empty;
        private DateTime? m_CN_DATE = null;
        private string m_REF_TYPE = string.Empty;
        private string m_REF_MOBILE_NO = string.Empty;
        private string m_REF_CHALLAN_NO = string.Empty;
        private string m_REF_ACCOUNT_NO = string.Empty;
        private string m_DISTANCE_TYPE_NAME = string.Empty;
        private string m_CLIENT_DEPTORBRANCH = string.Empty;
        private decimal m_WEIGHT = 0;
        private decimal m_QTY = 0;
        private decimal m_RATE = 0;
        private decimal m_TAKA = 0;
        private decimal m_SERVICE_CHARGE = 0;
        private decimal m_TOTAL_AMT = 0;   
            
        //dd
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


        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
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

        [DBColumn(Name = "CN_CLIENT_CODE", Storage = "m_CN_CLIENT_CODE", DbType = "126")]
        public string CN_CLIENT_CODE
        {
            get { return this.m_CN_CLIENT_CODE; }
            set
            {
                this.m_CN_CLIENT_CODE = value;
                this.NotifyPropertyChanged("CN_CLIENT_CODE");
            }
        }

        [DBColumn(Name = "CN_NAME", Storage = "m_CN_NAME", DbType = "126")]
        public string CN_NAME
        {
            get { return this.m_CN_NAME; }
            set
            {
                this.m_CN_NAME = value;
                this.NotifyPropertyChanged("CN_NAME");
            }
        }

        [DBColumn(Name = "CN_MOBILE_NO", Storage = "m_CN_MOBILE_NO", DbType = "126")]
        public string CN_MOBILE_NO
        {
            get { return this.m_CN_MOBILE_NO; }
            set
            {
                this.m_CN_MOBILE_NO = value;
                this.NotifyPropertyChanged("CN_MOBILE_NO");
            }
        }

        [DBColumn(Name = "ADDRESS", Storage = "m_ADDRESS", DbType = "126")]
        public string ADDRESS
        {
            get { return this.m_ADDRESS; }
            set
            {
                this.m_ADDRESS = value;
                this.NotifyPropertyChanged("ADDRESS");
            }
        }

        [DBColumn(Name = "ITEM_NAME", Storage = "m_ITEM_NAME", DbType = "126")]
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
                this.NotifyPropertyChanged("ITEM_NAME");
            }
        }

        [DBColumn(Name = "PRODUCT_TYPE", Storage = "m_PRODUCT_TYPE", DbType = "126")]
        public string PRODUCT_TYPE
        {
            get { return this.m_PRODUCT_TYPE; }
            set
            {
                this.m_PRODUCT_TYPE = value;
                this.NotifyPropertyChanged("PRODUCT_TYPE");
            }
        }

        [DBColumn(Name = "UPS", Storage = "m_UPS", DbType = "126")]
        public string UPS
        {
            get { return this.m_UPS; }
            set
            {
                this.m_UPS = value;
                this.NotifyPropertyChanged("UPS");
            }
        }

        [DBColumn(Name = "DESTINATION", Storage = "m_DESTINATION", DbType = "126")]
        public string DESTINATION
        {
            get { return this.m_DESTINATION; }
            set
            {
                this.m_DESTINATION = value;
                this.NotifyPropertyChanged("DESTINATION");
            }
        }

        [DBColumn(Name = "SLA_BREEZE", Storage = "m_SLA_BREEZE", DbType = "107")]
        public decimal SLA_BREEZE
        {
            get { return this.m_SLA_BREEZE; }
            set
            {
                this.m_SLA_BREEZE = value;
                this.NotifyPropertyChanged("SLA_BREEZE");
            }
        }

        [DBColumn(Name = "STATUS", Storage = "m_STATUS", DbType = "126")]
        public string STATUS
        {
            get { return this.m_STATUS; }
            set
            {
                this.m_STATUS = value;
                this.NotifyPropertyChanged("STATUS");
            }
        }

        [DBColumn(Name = "NARRATION", Storage = "m_NARRATION", DbType = "126")]
        public string NARRATION
        {
            get { return this.m_NARRATION; }
            set
            {
                this.m_NARRATION = value;
                this.NotifyPropertyChanged("NARRATION");
            }
        }

        [DBColumn(Name = "CN_DATE", Storage = "m_CN_DATE", DbType = "106")]
        public DateTime? CN_DATE
        {
            get { return this.m_CN_DATE; }
            set
            {
                this.m_CN_DATE = value;
                this.NotifyPropertyChanged("CN_DATE");
            }
        }

        [DBColumn(Name = "REF_TYPE", Storage = "m_REF_TYPE", DbType = "126")]
        public string REF_TYPE
        {
            get { return this.m_REF_TYPE; }
            set
            {
                this.m_REF_TYPE = value;
                this.NotifyPropertyChanged("REF_TYPE");
            }
        }

        [DBColumn(Name = "REF_MOBILE_NO", Storage = "m_REF_MOBILE_NO", DbType = "107")]
        public string REF_MOBILE_NO
        {
            get { return this.m_REF_MOBILE_NO; }
            set
            {
                this.m_REF_MOBILE_NO = value;
                this.NotifyPropertyChanged("REF_MOBILE_NO");
            }
        }

        [DBColumn(Name = "REF_CHALLAN_NO", Storage = "m_REF_CHALLAN_NO", DbType = "107")]
        public string REF_CHALLAN_NO
        {
            get { return this.m_REF_CHALLAN_NO; }
            set
            {
                this.m_REF_CHALLAN_NO = value;
                this.NotifyPropertyChanged("REF_CHALLAN_NO");
            }
        }

        [DBColumn(Name = "REF_ACCOUNT_NO", Storage = "m_REF_ACCOUNT_NO", DbType = "107")]
        public string REF_ACCOUNT_NO
        {
            get { return this.m_REF_ACCOUNT_NO; }
            set
            {
                this.m_REF_ACCOUNT_NO = value;
                this.NotifyPropertyChanged("REF_ACCOUNT_NO");
            }
        }

         [DBColumn(Name = "DISTANCE_TYPE_NAME", Storage = "m_DISTANCE_TYPE_NAME", DbType = "107")]
        public string DISTANCE_TYPE_NAME
        {
            get { return this.m_DISTANCE_TYPE_NAME; }
            set
            {
                this.m_DISTANCE_TYPE_NAME = value;
                this.NotifyPropertyChanged("DISTANCE_TYPE_NAME");
            }
        }
        [DBColumn(Name = "CLIENT_DEPTORBRANCH", Storage = "m_CLIENT_DEPTORBRANCH", DbType = "107")]
         public string CLIENT_DEPTORBRANCH
        {
            get { return this.m_CLIENT_DEPTORBRANCH; }
            set
            {
                this.m_CLIENT_DEPTORBRANCH = value;
                this.NotifyPropertyChanged("CLIENT_DEPTORBRANCH");
            }
        }
         [DBColumn(Name = "WEIGHT", Storage = "m_WEIGHT", DbType = "107")]
        public decimal WEIGHT
        {
            get { return this.m_WEIGHT; }
            set
            {
                this.m_WEIGHT = value;
                this.NotifyPropertyChanged("WEIGHT");
            }
        }

          [DBColumn(Name = "QTY", Storage = "m_QTY", DbType = "107")]
         public decimal QTY
        {
            get { return this.m_QTY; }
            set
            {
                this.m_QTY = value;
                this.NotifyPropertyChanged("QTY");
            }
        }
          [DBColumn(Name = "RATE", Storage = "m_RATE", DbType = "107")]
          public decimal RATE
        {
            get { return this.m_RATE; }
            set
            {
                this.m_RATE = value;
                this.NotifyPropertyChanged("RATE");
            }
        }
         [DBColumn(Name = "TAKA", Storage = "m_TAKA", DbType = "107")]
          public decimal TAKA
        {
            get { return this.m_TAKA; }
            set
            {
                this.m_TAKA = value;
                this.NotifyPropertyChanged("TAKA");
            }
        }
         [DBColumn(Name = "SERVICE_CHARGE", Storage = "m_SERVICE_CHARGE", DbType = "107")]
          public decimal SERVICE_CHARGE
        {
            get { return this.m_SERVICE_CHARGE; }
            set
            {
                this.m_SERVICE_CHARGE = value;
                this.NotifyPropertyChanged("SERVICE_CHARGE");
            }
        }
         [DBColumn(Name = "TOTAL_AMT", Storage = "m_TOTAL_AMT", DbType = "107")]
         public decimal TOTAL_AMT
        {
            get { return this.m_TOTAL_AMT; }
            set
            {
                this.m_TOTAL_AMT = value;
                this.NotifyPropertyChanged("TOTAL_AMT");
            }
        }
        
            
            


        #endregion //properties
    }
}
