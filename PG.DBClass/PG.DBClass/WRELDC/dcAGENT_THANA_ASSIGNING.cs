using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "AGENT_THANA_ASSIGNING")]
    public partial class dcAGENT_THANA_ASSIGNING : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AGENT_THANA_ID = 0;
        private int m_AGENT_ID = 0;
        private int m_TOWN_ID = 0;
        private DateTime? m_CONTRACT_DATE_FROM = null;
        private DateTime? m_FROM_DATE = null;
        private DateTime? m_TO_DATE = null;
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


        [DBColumn(Name = "AGENT_THANA_ID", Storage = "m_AGENT_THANA_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int AGENT_THANA_ID
        {
            get { return this.m_AGENT_THANA_ID; }
            set
            {
                this.m_AGENT_THANA_ID = value;
                this.NotifyPropertyChanged("AGENT_THANA_ID");
            }
        }

        [DBColumn(Name = "AGENT_ID", Storage = "m_AGENT_ID", DbType = "107")]
        public int AGENT_ID
        {
            get { return this.m_AGENT_ID; }
            set
            {
                this.m_AGENT_ID = value;
                this.NotifyPropertyChanged("AGENT_ID");
            }
        }

        [DBColumn(Name = "TOWN_ID", Storage = "m_TOWN_ID", DbType = "107")]
        public int TOWN_ID
        {
            get { return this.m_TOWN_ID; }
            set
            {
                this.m_TOWN_ID = value;
                this.NotifyPropertyChanged("TOWN_ID");
            }
        }

        [DBColumn(Name = "CONTRACT_DATE_FROM", Storage = "m_CONTRACT_DATE_FROM", DbType = "106")]
        public DateTime? CONTRACT_DATE_FROM
        {
            get { return this.m_CONTRACT_DATE_FROM; }
            set
            {
                this.m_CONTRACT_DATE_FROM = value;
                this.NotifyPropertyChanged("CONTRACT_DATE_FROM");
            }
        }

        [DBColumn(Name = "FROM_DATE", Storage = "m_FROM_DATE", DbType = "106")]
        public DateTime? FROM_DATE
        {
            get { return this.m_FROM_DATE; }
            set
            {
                this.m_FROM_DATE = value;
                this.NotifyPropertyChanged("FROM_DATE");
            }
        }

        [DBColumn(Name = "TO_DATE", Storage = "m_TO_DATE", DbType = "106")]
        public DateTime? TO_DATE
        {
            get { return this.m_TO_DATE; }
            set
            {
                this.m_TO_DATE = value;
                this.NotifyPropertyChanged("TO_DATE");
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
