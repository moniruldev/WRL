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
    [DBTable(Name = "tblGLClass")]
    public partial class dcGLClass : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GLClassID = 0;
        private string m_GLClassName = string.Empty;
        private string m_GLClassCode = string.Empty;
        private int m_GLClassType = 0;
        private string m_GLClassDesc = string.Empty;
        private int m_GLClassSLNo = 0;
        private int m_BalanceType = 0;
        private bool m_IsNumbering = false;
        private int m_StartNumber = 0;
        private int m_EndNumber = 0;
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


        [DBColumn(Name = "GLClassID", Storage = "m_GLClassID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int GLClassID
        {
            get { return this.m_GLClassID; }
            set
            {
                this.m_GLClassID = value;
                this.NotifyPropertyChanged("GLClassID");
            }
        }

        [DBColumn(Name = "GLClassName", Storage = "m_GLClassName", DbType = "NVarChar(100) NULL")]
        public string GLClassName
        {
            get { return this.m_GLClassName; }
            set
            {
                this.m_GLClassName = value;
                this.NotifyPropertyChanged("GLClassName");
            }
        }

        [DBColumn(Name = "GLClassCode", Storage = "m_GLClassCode", DbType = "NVarChar(100) NULL")]
        public string GLClassCode
        {
            get { return this.m_GLClassCode; }
            set
            {
                this.m_GLClassCode = value;
                this.NotifyPropertyChanged("GLClassCode");
            }
        }

        [DBColumn(Name = "GLClassType", Storage = "m_GLClassType", DbType = "Int NULL")]
        public int GLClassType
        {
            get { return this.m_GLClassType; }
            set
            {
                this.m_GLClassType = value;
                this.NotifyPropertyChanged("GLClassType");
            }
        }

        [DBColumn(Name = "GLClassDesc", Storage = "m_GLClassDesc", DbType = "NVarChar(100) NULL")]
        public string GLClassDesc
        {
            get { return this.m_GLClassDesc; }
            set
            {
                this.m_GLClassDesc = value;
                this.NotifyPropertyChanged("GLClassDesc");
            }
        }

        [DBColumn(Name = "GLClassSLNo", Storage = "m_GLClassSLNo", DbType = "Int NULL")]
        public int GLClassSLNo
        {
            get { return this.m_GLClassSLNo; }
            set
            {
                this.m_GLClassSLNo = value;
                this.NotifyPropertyChanged("GLClassSLNo");
            }
        }

        [DBColumn(Name = "BalanceType", Storage = "m_BalanceType", DbType = "Int NULL")]
        public int BalanceType
        {
            get { return this.m_BalanceType; }
            set
            {
                this.m_BalanceType = value;
                this.NotifyPropertyChanged("BalanceType");
            }
        }

        [DBColumn(Name = "IsNumbering", Storage = "m_IsNumbering", DbType = "Bit NULL")]
        public bool IsNumbering
        {
            get { return this.m_IsNumbering; }
            set
            {
                this.m_IsNumbering = value;
                this.NotifyPropertyChanged("IsNumbering");
            }
        }

        [DBColumn(Name = "StartNumber", Storage = "m_StartNumber", DbType = "Int NULL")]
        public int StartNumber
        {
            get { return this.m_StartNumber; }
            set
            {
                this.m_StartNumber = value;
                this.NotifyPropertyChanged("StartNumber");
            }
        }

        [DBColumn(Name = "EndNumber", Storage = "m_EndNumber", DbType = "Int NULL")]
        public int EndNumber
        {
            get { return this.m_EndNumber; }
            set
            {
                this.m_EndNumber = value;
                this.NotifyPropertyChanged("EndNumber");
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
