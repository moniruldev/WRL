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

        

        #endregion //properties
    }
}
