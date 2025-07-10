using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.AccountingDC.GeneralLedgerDC
{
    [DBTable(Name = "tblInstrumentMode")]
    public partial class dcInstrumentMode : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_InstrumentModeID = 0;
        private string m_InstrumentModeCode = string.Empty;
        private string m_InstrumentModeName = string.Empty;

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


        [DBColumn(Name = "InstrumentModeID", Storage = "m_InstrumentModeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int InstrumentModeID
        {
            get { return this.m_InstrumentModeID; }
            set
            {
                this.m_InstrumentModeID = value;
                this.NotifyPropertyChanged("InstrumentModeID");
            }
        }

        [DBColumn(Name = "InstrumentModeCode", Storage = "m_InstrumentModeCode", DbType = "NVarChar(20) NULL")]
        public string InstrumentModeCode
        {
            get { return this.m_InstrumentModeCode; }
            set
            {
                this.m_InstrumentModeCode = value;
                this.NotifyPropertyChanged("InstrumentModeCode");
            }
        }

        [DBColumn(Name = "InstrumentModeName", Storage = "m_InstrumentModeName", DbType = "NVarChar(50) NULL")]
        public string InstrumentModeName
        {
            get { return this.m_InstrumentModeName; }
            set
            {
                this.m_InstrumentModeName = value;
                this.NotifyPropertyChanged("InstrumentModeName");
            }
        }

        #endregion //properties
    }
}
