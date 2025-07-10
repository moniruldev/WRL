using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.SecurityDC
{
    [DBTable(Name = "DATATRANSFER_TABLE_LOG")]
    public partial class dcDATATRANSFER_TABLE_LOG : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ID = 0;
        private string m_TABLENAME = string.Empty;
        private DateTime? m_TRANSFERDATETIME = null;
        private string m_IS_SUCCESS = string.Empty;
        private DateTime? m_FROM_DATE = null;
        private DateTime? m_TO_DATE = null;

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


        [DBColumn(Name = "ID", Storage = "m_ID" ,IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true,DbType = "Int NOT NULL IDENTITY" )]
        public int ID
        {
            get { return this.m_ID; }
            set
            {
                this.m_ID = value;
                this.NotifyPropertyChanged("ID");
            }
        }

        [DBColumn(Name = "TABLENAME", Storage = "m_TABLENAME", DbType = "126")]
        public string TABLENAME
        {
            get { return this.m_TABLENAME; }
            set
            {
                this.m_TABLENAME = value;
                this.NotifyPropertyChanged("TABLENAME");
            }
        }

        [DBColumn(Name = "TRANSFERDATETIME", Storage = "m_TRANSFERDATETIME", DbType = "106")]
        public DateTime? TRANSFERDATETIME
        {
            get { return this.m_TRANSFERDATETIME; }
            set
            {
                this.m_TRANSFERDATETIME = value;
                this.NotifyPropertyChanged("TRANSFERDATETIME");
            }
        }

        [DBColumn(Name = "IS_SUCCESS", Storage = "m_IS_SUCCESS", DbType = "126")]
        public string IS_SUCCESS
        {
            get { return this.m_IS_SUCCESS; }
            set
            {
                this.m_IS_SUCCESS = value;
                this.NotifyPropertyChanged("IS_SUCCESS");
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

        #endregion //properties
    }
}
