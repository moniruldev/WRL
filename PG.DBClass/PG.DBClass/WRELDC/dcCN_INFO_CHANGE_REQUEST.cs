using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CN_INFO_CHANGE_REQUEST")]
    public partial class dcCN_INFO_CHANGE_REQUEST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REQUEST_ID = 0;
        private int m_CN_ID = 0;
        private int m_CLIENT_ID = 0;
        private string m_CURRENT_CONSIGNEE_NAME = string.Empty;
        private string m_NEW_CONSIGNEE_NAME = string.Empty;
        private string m_CURRENT_CONSIGNEE_ADDRESS = string.Empty;
        private string m_NEW_CONSIGNEE_ADDRESS = string.Empty;
        private string m_CURRENT_MOBILE_NO = string.Empty;
        private string m_NEW_MOBILE_NO = string.Empty;
        private string m_CURRENT_DESTINATION = string.Empty;
        private string m_NEW_DESTINATION = string.Empty;
        private string m_REASON = string.Empty;
        private DateTime? m_REQUEST_DATE = null;
        private string m_REQUEST_BY = string.Empty;
        private string m_APPROVED_STATUS = string.Empty;
        private string m_APPROVED_BY = string.Empty;
        private DateTime? m_APPROVED_DATE = null;

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


        [DBColumn(Name = "REQUEST_ID", Storage = "m_REQUEST_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REQUEST_ID
        {
            get { return this.m_REQUEST_ID; }
            set
            {
                this.m_REQUEST_ID = value;
                this.NotifyPropertyChanged("REQUEST_ID");
            }
        }

        [DBColumn(Name = "CN_ID", Storage = "m_CN_ID", DbType = "107")]
        public int CN_ID
        {
            get { return this.m_CN_ID; }
            set
            {
                this.m_CN_ID = value;
                this.NotifyPropertyChanged("CN_ID");
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

        [DBColumn(Name = "CURRENT_CONSIGNEE_NAME", Storage = "m_CURRENT_CONSIGNEE_NAME", DbType = "126")]
        public string CURRENT_CONSIGNEE_NAME
        {
            get { return this.m_CURRENT_CONSIGNEE_NAME; }
            set
            {
                this.m_CURRENT_CONSIGNEE_NAME = value;
                this.NotifyPropertyChanged("CURRENT_CONSIGNEE_NAME");
            }
        }

        [DBColumn(Name = "NEW_CONSIGNEE_NAME", Storage = "m_NEW_CONSIGNEE_NAME", DbType = "126")]
        public string NEW_CONSIGNEE_NAME
        {
            get { return this.m_NEW_CONSIGNEE_NAME; }
            set
            {
                this.m_NEW_CONSIGNEE_NAME = value;
                this.NotifyPropertyChanged("NEW_CONSIGNEE_NAME");
            }
        }

        [DBColumn(Name = "CURRENT_CONSIGNEE_ADDRESS", Storage = "m_CURRENT_CONSIGNEE_ADDRESS", DbType = "126")]
        public string CURRENT_CONSIGNEE_ADDRESS
        {
            get { return this.m_CURRENT_CONSIGNEE_ADDRESS; }
            set
            {
                this.m_CURRENT_CONSIGNEE_ADDRESS = value;
                this.NotifyPropertyChanged("CURRENT_CONSIGNEE_ADDRESS");
            }
        }

        [DBColumn(Name = "NEW_CONSIGNEE_ADDRESS", Storage = "m_NEW_CONSIGNEE_ADDRESS", DbType = "126")]
        public string NEW_CONSIGNEE_ADDRESS
        {
            get { return this.m_NEW_CONSIGNEE_ADDRESS; }
            set
            {
                this.m_NEW_CONSIGNEE_ADDRESS = value;
                this.NotifyPropertyChanged("NEW_CONSIGNEE_ADDRESS");
            }
        }

        [DBColumn(Name = "CURRENT_MOBILE_NO", Storage = "m_CURRENT_MOBILE_NO", DbType = "126")]
        public string CURRENT_MOBILE_NO
        {
            get { return this.m_CURRENT_MOBILE_NO; }
            set
            {
                this.m_CURRENT_MOBILE_NO = value;
                this.NotifyPropertyChanged("CURRENT_MOBILE_NO");
            }
        }

        [DBColumn(Name = "NEW_MOBILE_NO", Storage = "m_NEW_MOBILE_NO", DbType = "126")]
        public string NEW_MOBILE_NO
        {
            get { return this.m_NEW_MOBILE_NO; }
            set
            {
                this.m_NEW_MOBILE_NO = value;
                this.NotifyPropertyChanged("NEW_MOBILE_NO");
            }
        }

        [DBColumn(Name = "CURRENT_DESTINATION", Storage = "m_CURRENT_DESTINATION", DbType = "126")]
        public string CURRENT_DESTINATION
        {
            get { return this.m_CURRENT_DESTINATION; }
            set
            {
                this.m_CURRENT_DESTINATION = value;
                this.NotifyPropertyChanged("CURRENT_DESTINATION");
            }
        }

        [DBColumn(Name = "NEW_DESTINATION", Storage = "m_NEW_DESTINATION", DbType = "126")]
        public string NEW_DESTINATION
        {
            get { return this.m_NEW_DESTINATION; }
            set
            {
                this.m_NEW_DESTINATION = value;
                this.NotifyPropertyChanged("NEW_DESTINATION");
            }
        }

        [DBColumn(Name = "REASON", Storage = "m_REASON", DbType = "126")]
        public string REASON
        {
            get { return this.m_REASON; }
            set
            {
                this.m_REASON = value;
                this.NotifyPropertyChanged("REASON");
            }
        }

        [DBColumn(Name = "REQUEST_DATE", Storage = "m_REQUEST_DATE", DbType = "106")]
        public DateTime? REQUEST_DATE
        {
            get { return this.m_REQUEST_DATE; }
            set
            {
                this.m_REQUEST_DATE = value;
                this.NotifyPropertyChanged("REQUEST_DATE");
            }
        }

        [DBColumn(Name = "REQUEST_BY", Storage = "m_REQUEST_BY", DbType = "126")]
        public string REQUEST_BY
        {
            get { return this.m_REQUEST_BY; }
            set
            {
                this.m_REQUEST_BY = value;
                this.NotifyPropertyChanged("REQUEST_BY");
            }
        }

        [DBColumn(Name = "APPROVED_STATUS", Storage = "m_APPROVED_STATUS", DbType = "126")]
        public string APPROVED_STATUS
        {
            get { return this.m_APPROVED_STATUS; }
            set
            {
                this.m_APPROVED_STATUS = value;
                this.NotifyPropertyChanged("APPROVED_STATUS");
            }
        }

        [DBColumn(Name = "APPROVED_BY", Storage = "m_APPROVED_BY", DbType = "126")]
        public string APPROVED_BY
        {
            get { return this.m_APPROVED_BY; }
            set
            {
                this.m_APPROVED_BY = value;
                this.NotifyPropertyChanged("APPROVED_BY");
            }
        }

        [DBColumn(Name = "APPROVED_DATE", Storage = "m_APPROVED_DATE", DbType = "106")]
        public DateTime? APPROVED_DATE
        {
            get { return this.m_APPROVED_DATE; }
            set
            {
                this.m_APPROVED_DATE = value;
                this.NotifyPropertyChanged("APPROVED_DATE");
            }
        }

        #endregion //properties
    }

    public partial class dcCN_INFO_CHANGE_REQUEST
    {
        public string CN_NUMBER { get; set; }
        public string CLIENT_NAME { get; set; }
        public string REQUEST_BY_NAME { get; set; }
        
    }
}
