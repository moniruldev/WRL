using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.HMSDC
{
    [DBTable(Name = "HMRESERVATION_MST")]
    public partial class dcHMRESERVATION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RESERVATION_ID = 0;
        private int m_GUEST_ID = 0;
        private DateTime? m_CHECK_IN = null;
        private DateTime? m_CHECK_OUT = null;
        private DateTime? m_CREATE_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private string m_STATUS = string.Empty;
        private string m_NOTE = string.Empty;
        private string m_CONFIRMED_BY = string.Empty;
        private DateTime? m_CONFIRMED_DATE = null;
        private string m_CONFIRMED_NOTE = string.Empty;
        private string m_CONFIRMED_FEEDBACK = string.Empty;
        private string m_CONFIRMED_FEEDBACK_MEDIA = string.Empty;
        private string m_CANCEL_BY = string.Empty;
        private DateTime? m_CANCEL_DATE = null;
        private string m_CANCEL_NOTE = string.Empty;
        private string m_CANCEL_FEEDBACK = string.Empty;
        private string m_CANCEL_FEEDBACK_MEDIA = string.Empty;
        private string m_IS_CHECKED_IN = string.Empty;
        private int m_CHECK_IN_OUT_ID = 0;
        private string m_VISIT_FOR = string.Empty;

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


        [DBColumn(Name = "RESERVATION_ID", Storage = "m_RESERVATION_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RESERVATION_ID
        {
            get { return this.m_RESERVATION_ID; }
            set
            {
                this.m_RESERVATION_ID = value;
                this.NotifyPropertyChanged("RESERVATION_ID");
            }
        }

        [DBColumn(Name = "GUEST_ID", Storage = "m_GUEST_ID", DbType = "107")]
        public int GUEST_ID
        {
            get { return this.m_GUEST_ID; }
            set
            {
                this.m_GUEST_ID = value;
                this.NotifyPropertyChanged("GUEST_ID");
            }
        }

        [DBColumn(Name = "CHECK_IN", Storage = "m_CHECK_IN", DbType = "106")]
        public DateTime? CHECK_IN
        {
            get { return this.m_CHECK_IN; }
            set
            {
                this.m_CHECK_IN = value;
                this.NotifyPropertyChanged("CHECK_IN");
            }
        }

        [DBColumn(Name = "CHECK_OUT", Storage = "m_CHECK_OUT", DbType = "106")]
        public DateTime? CHECK_OUT
        {
            get { return this.m_CHECK_OUT; }
            set
            {
                this.m_CHECK_OUT = value;
                this.NotifyPropertyChanged("CHECK_OUT");
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

        [DBColumn(Name = "NOTE", Storage = "m_NOTE", DbType = "126")]
        public string NOTE
        {
            get { return this.m_NOTE; }
            set
            {
                this.m_NOTE = value;
                this.NotifyPropertyChanged("NOTE");
            }
        }

        [DBColumn(Name = "CONFIRMED_BY", Storage = "m_CONFIRMED_BY", DbType = "126")]
        public string CONFIRMED_BY
        {
            get { return this.m_CONFIRMED_BY; }
            set
            {
                this.m_CONFIRMED_BY = value;
                this.NotifyPropertyChanged("CONFIRMED_BY");
            }
        }

        [DBColumn(Name = "CONFIRMED_DATE", Storage = "m_CONFIRMED_DATE", DbType = "106")]
        public DateTime? CONFIRMED_DATE
        {
            get { return this.m_CONFIRMED_DATE; }
            set
            {
                this.m_CONFIRMED_DATE = value;
                this.NotifyPropertyChanged("CONFIRMED_DATE");
            }
        }

        [DBColumn(Name = "CONFIRMED_NOTE", Storage = "m_CONFIRMED_NOTE", DbType = "126")]
        public string CONFIRMED_NOTE
        {
            get { return this.m_CONFIRMED_NOTE; }
            set
            {
                this.m_CONFIRMED_NOTE = value;
                this.NotifyPropertyChanged("CONFIRMED_NOTE");
            }
        }

        [DBColumn(Name = "CONFIRMED_FEEDBACK", Storage = "m_CONFIRMED_FEEDBACK", DbType = "126")]
        public string CONFIRMED_FEEDBACK
        {
            get { return this.m_CONFIRMED_FEEDBACK; }
            set
            {
                this.m_CONFIRMED_FEEDBACK = value;
                this.NotifyPropertyChanged("CONFIRMED_FEEDBACK");
            }
        }

        [DBColumn(Name = "CONFIRMED_FEEDBACK_MEDIA", Storage = "m_CONFIRMED_FEEDBACK_MEDIA", DbType = "126")]
        public string CONFIRMED_FEEDBACK_MEDIA
        {
            get { return this.m_CONFIRMED_FEEDBACK_MEDIA; }
            set
            {
                this.m_CONFIRMED_FEEDBACK_MEDIA = value;
                this.NotifyPropertyChanged("CONFIRMED_FEEDBACK_MEDIA");
            }
        }

        [DBColumn(Name = "CANCEL_BY", Storage = "m_CANCEL_BY", DbType = "126")]
        public string CANCEL_BY
        {
            get { return this.m_CANCEL_BY; }
            set
            {
                this.m_CANCEL_BY = value;
                this.NotifyPropertyChanged("CANCEL_BY");
            }
        }

        [DBColumn(Name = "CANCEL_DATE", Storage = "m_CANCEL_DATE", DbType = "106")]
        public DateTime? CANCEL_DATE
        {
            get { return this.m_CANCEL_DATE; }
            set
            {
                this.m_CANCEL_DATE = value;
                this.NotifyPropertyChanged("CANCEL_DATE");
            }
        }

        [DBColumn(Name = "CANCEL_NOTE", Storage = "m_CANCEL_NOTE", DbType = "126")]
        public string CANCEL_NOTE
        {
            get { return this.m_CANCEL_NOTE; }
            set
            {
                this.m_CANCEL_NOTE = value;
                this.NotifyPropertyChanged("CANCEL_NOTE");
            }
        }

        [DBColumn(Name = "CANCEL_FEEDBACK", Storage = "m_CANCEL_FEEDBACK", DbType = "126")]
        public string CANCEL_FEEDBACK
        {
            get { return this.m_CANCEL_FEEDBACK; }
            set
            {
                this.m_CANCEL_FEEDBACK = value;
                this.NotifyPropertyChanged("CANCEL_FEEDBACK");
            }
        }

        [DBColumn(Name = "CANCEL_FEEDBACK_MEDIA", Storage = "m_CANCEL_FEEDBACK_MEDIA", DbType = "126")]
        public string CANCEL_FEEDBACK_MEDIA
        {
            get { return this.m_CANCEL_FEEDBACK_MEDIA; }
            set
            {
                this.m_CANCEL_FEEDBACK_MEDIA = value;
                this.NotifyPropertyChanged("CANCEL_FEEDBACK_MEDIA");
            }
        }

        [DBColumn(Name = "IS_CHECKED_IN", Storage = "m_IS_CHECKED_IN", DbType = "126")]
        public string IS_CHECKED_IN
        {
            get { return this.m_IS_CHECKED_IN; }
            set
            {
                this.m_IS_CHECKED_IN = value;
                this.NotifyPropertyChanged("IS_CHECKED_IN");
            }
        }

        [DBColumn(Name = "CHECK_IN_OUT_ID", Storage = "m_CHECK_IN_OUT_ID", DbType = "107")]
        public int CHECK_IN_OUT_ID
        {
            get { return this.m_CHECK_IN_OUT_ID; }
            set
            {
                this.m_CHECK_IN_OUT_ID = value;
                this.NotifyPropertyChanged("CHECK_IN_OUT_ID");
            }
        }

        [DBColumn(Name = "VISIT_FOR", Storage = "m_VISIT_FOR", DbType = "126")]
        public string VISIT_FOR
        {
            get { return this.m_VISIT_FOR; }
            set
            {
                this.m_VISIT_FOR = value;
                this.NotifyPropertyChanged("VISIT_FOR");
            }
        }

        #endregion //properties
    }
}
