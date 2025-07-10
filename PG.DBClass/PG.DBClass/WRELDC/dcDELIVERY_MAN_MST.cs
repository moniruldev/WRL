using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "DELIVERY_MAN_MST")]
    public partial class dcDELIVERY_MAN_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_DELIVERY_MAN_ID = 0;
        private string m_DELIVERY_MAN_NAME = string.Empty;
        private string m_FATHER_NAME = string.Empty;
        private string m_MOTHER_NAME = string.Empty;
        private string m_ADDRESS = string.Empty;
        private string m_MOBILE_NO = string.Empty;
        private string m_IS_UNDER_AGENT = string.Empty;
        private decimal m_AGENT_ID = 0;
        private string m_IS_ACTIVE = string.Empty;
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


        [DBColumn(Name = "DELIVERY_MAN_ID", Storage = "m_DELIVERY_MAN_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int DELIVERY_MAN_ID
        {
            get { return this.m_DELIVERY_MAN_ID; }
            set
            {
                this.m_DELIVERY_MAN_ID = value;
                this.NotifyPropertyChanged("DELIVERY_MAN_ID");
            }
        }

        [DBColumn(Name = "DELIVERY_MAN_NAME", Storage = "m_DELIVERY_MAN_NAME", DbType = "126")]
        public string DELIVERY_MAN_NAME
        {
            get { return this.m_DELIVERY_MAN_NAME; }
            set
            {
                this.m_DELIVERY_MAN_NAME = value;
                this.NotifyPropertyChanged("DELIVERY_MAN_NAME");
            }
        }

        [DBColumn(Name = "FATHER_NAME", Storage = "m_FATHER_NAME", DbType = "126")]
        public string FATHER_NAME
        {
            get { return this.m_FATHER_NAME; }
            set
            {
                this.m_FATHER_NAME = value;
                this.NotifyPropertyChanged("FATHER_NAME");
            }
        }

        [DBColumn(Name = "MOTHER_NAME", Storage = "m_MOTHER_NAME", DbType = "126")]
        public string MOTHER_NAME
        {
            get { return this.m_MOTHER_NAME; }
            set
            {
                this.m_MOTHER_NAME = value;
                this.NotifyPropertyChanged("MOTHER_NAME");
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

        [DBColumn(Name = "IS_UNDER_AGENT", Storage = "m_IS_UNDER_AGENT", DbType = "126")]
        public string IS_UNDER_AGENT
        {
            get { return this.m_IS_UNDER_AGENT; }
            set
            {
                this.m_IS_UNDER_AGENT = value;
                this.NotifyPropertyChanged("IS_UNDER_AGENT");
            }
        }

        [DBColumn(Name = "AGENT_ID", Storage = "m_AGENT_ID", DbType = "107")]
        public decimal AGENT_ID
        {
            get { return this.m_AGENT_ID; }
            set
            {
                this.m_AGENT_ID = value;
                this.NotifyPropertyChanged("AGENT_ID");
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
