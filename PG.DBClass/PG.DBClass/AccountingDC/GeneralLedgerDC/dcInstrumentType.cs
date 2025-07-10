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
    [DBTable(Name = "tblInstrumentType")]
    public partial class dcInstrumentType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_InstrumentTypeID = 0;
        private string m_InstrumentTypeCode = string.Empty;
        private string m_InstrumentTypeName = string.Empty;
        private int m_InstrumentTypeSLNo = 0;
        private bool m_IsActive = false;
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


        [DBColumn(Name = "InstrumentTypeID", Storage = "m_InstrumentTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int InstrumentTypeID
        {
            get { return this.m_InstrumentTypeID; }
            set
            {
                this.m_InstrumentTypeID = value;
                this.NotifyPropertyChanged("InstrumentTypeID");
            }
        }

        [DBColumn(Name = "InstrumentTypeCode", Storage = "m_InstrumentTypeCode", DbType = "NVarChar(50) NULL")]
        public string InstrumentTypeCode
        {
            get { return this.m_InstrumentTypeCode; }
            set
            {
                this.m_InstrumentTypeCode = value;
                this.NotifyPropertyChanged("InstrumentTypeCode");
            }
        }

        [DBColumn(Name = "InstrumentTypeName", Storage = "m_InstrumentTypeName", DbType = "NVarChar(100) NULL")]
        public string InstrumentTypeName
        {
            get { return this.m_InstrumentTypeName; }
            set
            {
                this.m_InstrumentTypeName = value;
                this.NotifyPropertyChanged("InstrumentTypeName");
            }
        }

        [DBColumn(Name = "InstrumentTypeSLNo", Storage = "m_InstrumentTypeSLNo", DbType = "Int NULL")]
        public int InstrumentTypeSLNo
        {
            get { return this.m_InstrumentTypeSLNo; }
            set
            {
                this.m_InstrumentTypeSLNo = value;
                this.NotifyPropertyChanged("InstrumentTypeSLNo");
            }
        }

        [DBColumn(Name = "IsActive", Storage = "m_IsActive", DbType = "Bit NULL")]
        public bool IsActive
        {
            get { return this.m_IsActive; }
            set
            {
                this.m_IsActive = value;
                this.NotifyPropertyChanged("IsActive");
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
