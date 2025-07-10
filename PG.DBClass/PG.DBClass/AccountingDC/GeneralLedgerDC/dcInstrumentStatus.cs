using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.AccountingDC
{
    [DBTable(Name = "tblInstrumentStatus")]
    public partial class dcInstrumentStatus : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_InstrumentStatusID = 0;
        private int m_InstrumentStatusSLNo = 0;
        private string m_InstrumentStatusCode = string.Empty;
        private string m_InstrumentStatusName = string.Empty;

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


        [DBColumn(Name = "InstrumentStatusID", Storage = "m_InstrumentStatusID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int InstrumentStatusID
        {
            get { return this.m_InstrumentStatusID; }
            set
            {
                this.m_InstrumentStatusID = value;
                this.NotifyPropertyChanged("InstrumentStatusID");
            }
        }

        [DBColumn(Name = "InstrumentStatusSLNo", Storage = "m_InstrumentStatusSLNo", DbType = "Int NULL")]
        public int InstrumentStatusSLNo
        {
            get { return this.m_InstrumentStatusSLNo; }
            set
            {
                this.m_InstrumentStatusSLNo = value;
                this.NotifyPropertyChanged("InstrumentStatusSLNo");
            }
        }

        [DBColumn(Name = "InstrumentStatusCode", Storage = "m_InstrumentStatusCode", DbType = "NVarChar(50) NULL")]
        public string InstrumentStatusCode
        {
            get { return this.m_InstrumentStatusCode; }
            set
            {
                this.m_InstrumentStatusCode = value;
                this.NotifyPropertyChanged("InstrumentStatusCode");
            }
        }

        [DBColumn(Name = "InstrumentStatusName", Storage = "m_InstrumentStatusName", DbType = "NVarChar(100) NULL")]
        public string InstrumentStatusName
        {
            get { return this.m_InstrumentStatusName; }
            set
            {
                this.m_InstrumentStatusName = value;
                this.NotifyPropertyChanged("InstrumentStatusName");
            }
        }

        #endregion //properties
    }
}
