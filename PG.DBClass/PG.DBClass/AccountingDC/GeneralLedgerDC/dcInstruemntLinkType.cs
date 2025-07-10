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
    [DBTable(Name = "tblInstrumentLinkType")]
    public partial class dcInstrumentLinkType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_InstrumentLinkTypeID = 0;
        private string m_InstrumentLinkTypeCode = string.Empty;
        private string m_InstrumentLinkTypeName = string.Empty;
        private int m_InstrumentLinkTypeSLNo = 0;
        private bool m_IsSystem = false;
        private bool m_IsVisible = false;

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


        [DBColumn(Name = "InstrumentLinkTypeID", Storage = "m_InstrumentLinkTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int InstrumentLinkTypeID
        {
            get { return this.m_InstrumentLinkTypeID; }
            set
            {
                this.m_InstrumentLinkTypeID = value;
                this.NotifyPropertyChanged("InstrumentLinkTypeID");
            }
        }

        [DBColumn(Name = "InstrumentLinkTypeCode", Storage = "m_InstrumentLinkTypeCode", DbType = "NVarChar(20) NULL")]
        public string InstrumentLinkTypeCode
        {
            get { return this.m_InstrumentLinkTypeCode; }
            set
            {
                this.m_InstrumentLinkTypeCode = value;
                this.NotifyPropertyChanged("InstrumentLinkTypeCode");
            }
        }

        [DBColumn(Name = "InstrumentLinkTypeName", Storage = "m_InstrumentLinkTypeName", DbType = "NVarChar(50) NULL")]
        public string InstrumentLinkTypeName
        {
            get { return this.m_InstrumentLinkTypeName; }
            set
            {
                this.m_InstrumentLinkTypeName = value;
                this.NotifyPropertyChanged("InstrumentLinkTypeName");
            }
        }

        [DBColumn(Name = "InstrumentLinkTypeSLNo", Storage = "m_InstrumentLinkTypeSLNo", DbType = "Int NULL")]
        public int InstrumentLinkTypeSLNo
        {
            get { return this.m_InstrumentLinkTypeSLNo; }
            set
            {
                this.m_InstrumentLinkTypeSLNo = value;
                this.NotifyPropertyChanged("InstrumentLinkTypeSLNo");
            }
        }

        [DBColumn(Name = "IsSystem", Storage = "m_IsSystem", DbType = "Bit NULL")]
        public bool IsSystem
        {
            get { return this.m_IsSystem; }
            set
            {
                this.m_IsSystem = value;
                this.NotifyPropertyChanged("IsSystem");
            }
        }

        [DBColumn(Name = "IsVisible", Storage = "m_IsVisible", DbType = "Bit NULL")]
        public bool IsVisible
        {
            get { return this.m_IsVisible; }
            set
            {
                this.m_IsVisible = value;
                this.NotifyPropertyChanged("IsVisible");
            }
        }

        #endregion //properties
    }
}
