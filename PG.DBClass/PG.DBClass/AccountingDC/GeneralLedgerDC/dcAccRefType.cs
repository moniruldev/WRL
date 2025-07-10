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
    [DBTable(Name = "tblAccRefType")]
    public partial class dcAccRefType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AccRefTypeID = 0;
        private string m_AccRefTypeCode = string.Empty;
        private string m_AccRefTypeName = string.Empty;

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

        [DBColumn(Name = "AccRefTypeID", Storage = "m_AccRefTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int AccRefTypeID
        {
            get { return this.m_AccRefTypeID; }
            set
            {
                this.m_AccRefTypeID = value;
                this.NotifyPropertyChanged("AccRefTypeID");
            }
        }

        [DBColumn(Name = "AccRefTypeCode", Storage = "m_AccRefTypeCode", DbType = "NVarChar(50) NULL")]
        public string AccRefTypeCode
        {
            get { return this.m_AccRefTypeCode; }
            set
            {
                this.m_AccRefTypeCode = value;
                this.NotifyPropertyChanged("AccRefTypeCode");
            }
        }

        [DBColumn(Name = "AccRefTypeName", Storage = "m_AccRefTypeName", DbType = "NVarChar(100) NULL")]
        public string AccRefTypeName
        {
            get { return this.m_AccRefTypeName; }
            set
            {
                this.m_AccRefTypeName = value;
                this.NotifyPropertyChanged("AccRefTypeName");
            }
        }

        #endregion //properties
    }
}
