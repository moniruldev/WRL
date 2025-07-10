using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "SHIFT_MST")]
    public partial class dcSHIFT_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_SHIFT_ID = string.Empty;
        private string m_SHIFT_NAME = string.Empty;
        private string m_START_TIME = string.Empty;
        private string m_END_TIME = string.Empty;
        private int m_SHIFT_MSTID = 0;

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

        [DBColumn(Name = "SHIFT_MSTID", Storage = "m_SHIFT_MSTID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int SHIFT_MSTID
        {
            get { return this.m_SHIFT_MSTID; }
            set
            {
                this.m_SHIFT_MSTID = value;
                this.NotifyPropertyChanged("SHIFT_MSTID");
            }
        }

        [DBColumn(Name = "SHIFT_ID", Storage = "m_SHIFT_ID", DbType = "126")]
        public string SHIFT_ID
        {
            get { return this.m_SHIFT_ID; }
            set
            {
                this.m_SHIFT_ID = value;
                this.NotifyPropertyChanged("SHIFT_ID");
            }
        }

        [DBColumn(Name = "SHIFT_NAME", Storage = "m_SHIFT_NAME", DbType = "126")]
        public string SHIFT_NAME
        {
            get { return this.m_SHIFT_NAME; }
            set
            {
                this.m_SHIFT_NAME = value;
                this.NotifyPropertyChanged("SHIFT_NAME");
            }
        }

        [DBColumn(Name = "START_TIME", Storage = "m_START_TIME", DbType = "126")]
        public string START_TIME
        {
            get { return this.m_START_TIME; }
            set
            {
                this.m_START_TIME = value;
                this.NotifyPropertyChanged("START_TIME");
            }
        }

        [DBColumn(Name = "END_TIME", Storage = "m_END_TIME", DbType = "126")]
        public string END_TIME
        {
            get { return this.m_END_TIME; }
            set
            {
                this.m_END_TIME = value;
                this.NotifyPropertyChanged("END_TIME");
            }
        }

        #endregion //properties
    }
}
