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
    [DBTable(Name = "tblGLAccountType")]
    public partial class dcGLAccountType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GLAccountTypeID = 0;
        private string m_GLAccountTypeName = string.Empty;

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


        [DBColumn(Name = "GLAccountTypeID", Storage = "m_GLAccountTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int GLAccountTypeID
        {
            get { return this.m_GLAccountTypeID; }
            set
            {
                this.m_GLAccountTypeID = value;
                this.NotifyPropertyChanged("GLAccountTypeID");
            }
        }

        [DBColumn(Name = "GLAccountTypeName", Storage = "m_GLAccountTypeName", DbType = "NVarChar(50) NULL")]
        public string GLAccountTypeName
        {
            get { return this.m_GLAccountTypeName; }
            set
            {
                this.m_GLAccountTypeName = value;
                this.NotifyPropertyChanged("GLAccountTypeName");
            }
        }

        #endregion //properties
    }
}
