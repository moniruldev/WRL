using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "AGREEMENT_MST")]
    public partial class dcAGREEMENT_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AGR_ID = 0;
        private int m_CLIENT_ID = 0;
        private int m_DEPT_ID = 0;
        private DateTime? m_AGREEMENT_DATE = null;
        private DateTime? m_AGREEMENT_START_DATE = null;
        private DateTime? m_AGREEMENT_END_DATE = null;
        private string m_DESCRIPTION = string.Empty;
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


        [DBColumn(Name = "AGR_ID", Storage = "m_AGR_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int AGR_ID
        {
            get { return this.m_AGR_ID; }
            set
            {
                this.m_AGR_ID = value;
                this.NotifyPropertyChanged("AGR_ID");
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

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "AGREEMENT_DATE", Storage = "m_AGREEMENT_DATE", DbType = "106")]
        public DateTime? AGREEMENT_DATE
        {
            get { return this.m_AGREEMENT_DATE; }
            set
            {
                this.m_AGREEMENT_DATE = value;
                this.NotifyPropertyChanged("AGREEMENT_DATE");
            }
        }

        [DBColumn(Name = "AGREEMENT_START_DATE", Storage = "m_AGREEMENT_START_DATE", DbType = "106")]
        public DateTime? AGREEMENT_START_DATE
        {
            get { return this.m_AGREEMENT_START_DATE; }
            set
            {
                this.m_AGREEMENT_START_DATE = value;
                this.NotifyPropertyChanged("AGREEMENT_START_DATE");
            }
        }

        [DBColumn(Name = "AGREEMENT_END_DATE", Storage = "m_AGREEMENT_END_DATE", DbType = "106")]
        public DateTime? AGREEMENT_END_DATE
        {
            get { return this.m_AGREEMENT_END_DATE; }
            set
            {
                this.m_AGREEMENT_END_DATE = value;
                this.NotifyPropertyChanged("AGREEMENT_END_DATE");
            }
        }

        [DBColumn(Name = "DESCRIPTION", Storage = "m_DESCRIPTION", DbType = "126")]
        public string DESCRIPTION
        {
            get { return this.m_DESCRIPTION; }
            set
            {
                this.m_DESCRIPTION = value;
                this.NotifyPropertyChanged("DESCRIPTION");
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
