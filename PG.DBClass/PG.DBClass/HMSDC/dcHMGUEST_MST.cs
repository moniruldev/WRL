using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.HMSDC
{
    [DBTable(Name = "HMGUEST_MST")]
    public partial class dcHMGUEST_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GUEST_ID = 0;
        private string m_GUEST_NAME = string.Empty;
        private string m_ADDRESS = string.Empty;
        private string m_EMAIL = string.Empty;
        private int m_COUNTRY_ID = 0;
        private string m_PASSPORT_NO = string.Empty;
        private DateTime? m_ISSUE_DATE = null;
        private string m_GENDER = string.Empty;
        private DateTime? m_DATE_OF_BIRTH = null;
        private string m_PABX_NO = string.Empty;
        private string m_MOBILE_NO = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;

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


        [DBColumn(Name = "GUEST_ID", Storage = "m_GUEST_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int GUEST_ID
        {
            get { return this.m_GUEST_ID; }
            set
            {
                this.m_GUEST_ID = value;
                this.NotifyPropertyChanged("GUEST_ID");
            }
        }

        [DBColumn(Name = "GUEST_NAME", Storage = "m_GUEST_NAME", DbType = "126")]
        public string GUEST_NAME
        {
            get { return this.m_GUEST_NAME; }
            set
            {
                this.m_GUEST_NAME = value;
                this.NotifyPropertyChanged("GUEST_NAME");
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

        [DBColumn(Name = "EMAIL", Storage = "m_EMAIL", DbType = "126")]
        public string EMAIL
        {
            get { return this.m_EMAIL; }
            set
            {
                this.m_EMAIL = value;
                this.NotifyPropertyChanged("EMAIL");
            }
        }

        [DBColumn(Name = "COUNTRY_ID", Storage = "m_COUNTRY_ID", DbType = "107")]
        public int COUNTRY_ID
        {
            get { return this.m_COUNTRY_ID; }
            set
            {
                this.m_COUNTRY_ID = value;
                this.NotifyPropertyChanged("COUNTRY_ID");
            }
        }

        [DBColumn(Name = "PASSPORT_NO", Storage = "m_PASSPORT_NO", DbType = "126")]
        public string PASSPORT_NO
        {
            get { return this.m_PASSPORT_NO; }
            set
            {
                this.m_PASSPORT_NO = value;
                this.NotifyPropertyChanged("PASSPORT_NO");
            }
        }

        [DBColumn(Name = "ISSUE_DATE", Storage = "m_ISSUE_DATE", DbType = "106")]
        public DateTime? ISSUE_DATE
        {
            get { return this.m_ISSUE_DATE; }
            set
            {
                this.m_ISSUE_DATE = value;
                this.NotifyPropertyChanged("ISSUE_DATE");
            }
        }

        [DBColumn(Name = "GENDER", Storage = "m_GENDER", DbType = "126")]
        public string GENDER
        {
            get { return this.m_GENDER; }
            set
            {
                this.m_GENDER = value;
                this.NotifyPropertyChanged("GENDER");
            }
        }

        [DBColumn(Name = "DATE_OF_BIRTH", Storage = "m_DATE_OF_BIRTH", DbType = "106")]
        public DateTime? DATE_OF_BIRTH
        {
            get { return this.m_DATE_OF_BIRTH; }
            set
            {
                this.m_DATE_OF_BIRTH = value;
                this.NotifyPropertyChanged("DATE_OF_BIRTH");
            }
        }

        [DBColumn(Name = "PABX_NO", Storage = "m_PABX_NO", DbType = "126")]
        public string PABX_NO
        {
            get { return this.m_PABX_NO; }
            set
            {
                this.m_PABX_NO = value;
                this.NotifyPropertyChanged("PABX_NO");
            }
        }

        [DBColumn(Name = "MOBILE_NO", Storage = "m_MOBILE_NO", DbType = "126")]
        public string MOBILE_NO
        {
            get { return this.m_MOBILE_NO; }
            set
            {
                this.m_MOBILE_NO = value;
                this.NotifyPropertyChanged("MOBILE_NO");
            }
        }

        [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public string IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
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

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        #endregion //properties
    }

     public partial class dcHMGUEST_MST
     {
       public  dcHMRESERVATION_MST objReservationMst = new dcHMRESERVATION_MST();
       public List<dcHMRESERVATION_DTL> ReservationDtlList = new List<dcHMRESERVATION_DTL>();
       public DateTime CHECK_IN { get; set; }
       public DateTime CHECK_OUT { get; set; }
       public string NOTE { get; set; }
       public int RESERVATION_ID { get; set; }
       public string COUNTRY_NAME { get; set; }
     }
}
