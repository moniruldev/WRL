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
    [DBTable(Name = "tblGLGroupClass")]
    public partial class dcGLGroupClass : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GLGroupClassID = 0;
        private int m_GLClassID = 0;
        private int m_GLGroupClassLevel = 0;
        private int m_GLGroupClassIDParent = 0;
        private int m_GLGroupClassSLNo = 0;
        private string m_GLGroupClassCode = string.Empty;
        private string m_GLGroupClassNameShort = string.Empty;
        private string m_GLGroupClassName = string.Empty;
        private string m_GLGroupClassNamePredefined = string.Empty;
        private string m_GLGroupClassDesc = string.Empty;
        private int m_BalanceType = 0;
        private bool m_IsCash = false;
        private bool m_IsBank = false;
        private bool m_IsInstrument = false;
        private bool m_IsGrossProfit = false;
        private bool m_IsInventory = false;
        private int m_CashFlowGroupID = 0;
        private bool m_ShowAlways = false;

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


        [DBColumn(Name = "GLGroupClassID", Storage = "m_GLGroupClassID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int GLGroupClassID
        {
            get { return this.m_GLGroupClassID; }
            set
            {
                this.m_GLGroupClassID = value;
                this.NotifyPropertyChanged("GLGroupClassID");
            }
        }

        [DBColumn(Name = "GLClassID", Storage = "m_GLClassID", DbType = "Int NULL")]
        public int GLClassID
        {
            get { return this.m_GLClassID; }
            set
            {
                this.m_GLClassID = value;
                this.NotifyPropertyChanged("GLClassID");
            }
        }

        [DBColumn(Name = "GLGroupClassLevel", Storage = "m_GLGroupClassLevel", DbType = "Int NULL")]
        public int GLGroupClassLevel
        {
            get { return this.m_GLGroupClassLevel; }
            set
            {
                this.m_GLGroupClassLevel = value;
                this.NotifyPropertyChanged("GLGroupClassLevel");
            }
        }

        [DBColumn(Name = "GLGroupClassIDParent", Storage = "m_GLGroupClassIDParent", DbType = "Int NULL")]
        public int GLGroupClassIDParent
        {
            get { return this.m_GLGroupClassIDParent; }
            set
            {
                this.m_GLGroupClassIDParent = value;
                this.NotifyPropertyChanged("GLGroupClassIDParent");
            }
        }

        [DBColumn(Name = "GLGroupClassSLNo", Storage = "m_GLGroupClassSLNo", DbType = "Int NULL")]
        public int GLGroupClassSLNo
        {
            get { return this.m_GLGroupClassSLNo; }
            set
            {
                this.m_GLGroupClassSLNo = value;
                this.NotifyPropertyChanged("GLGroupClassSLNo");
            }
        }

        [DBColumn(Name = "GLGroupClassCode", Storage = "m_GLGroupClassCode", DbType = "NVarChar(50) NULL")]
        public string GLGroupClassCode
        {
            get { return this.m_GLGroupClassCode; }
            set
            {
                this.m_GLGroupClassCode = value;
                this.NotifyPropertyChanged("GLGroupClassCode");
            }
        }

        [DBColumn(Name = "GLGroupClassNameShort", Storage = "m_GLGroupClassNameShort", DbType = "NVarChar(50) NULL")]
        public string GLGroupClassNameShort
        {
            get { return this.m_GLGroupClassNameShort; }
            set
            {
                this.m_GLGroupClassNameShort = value;
                this.NotifyPropertyChanged("GLGroupClassNameShort");
            }
        }

        [DBColumn(Name = "GLGroupClassName", Storage = "m_GLGroupClassName", DbType = "NVarChar(200) NULL")]
        public string GLGroupClassName
        {
            get { return this.m_GLGroupClassName; }
            set
            {
                this.m_GLGroupClassName = value;
                this.NotifyPropertyChanged("GLGroupClassName");
            }
        }

        [DBColumn(Name = "GLGroupClassNamePredefined", Storage = "m_GLGroupClassNamePredefined", DbType = "NVarChar(100) NULL")]
        public string GLGroupClassNamePredefined
        {
            get { return this.m_GLGroupClassNamePredefined; }
            set
            {
                this.m_GLGroupClassNamePredefined = value;
                this.NotifyPropertyChanged("GLGroupClassNamePredefined");
            }
        }

        [DBColumn(Name = "GLGroupClassDesc", Storage = "m_GLGroupClassDesc", DbType = "VarChar(200) NULL")]
        public string GLGroupClassDesc
        {
            get { return this.m_GLGroupClassDesc; }
            set
            {
                this.m_GLGroupClassDesc = value;
                this.NotifyPropertyChanged("GLGroupClassDesc");
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

        [DBColumn(Name = "IsCash", Storage = "m_IsCash", DbType = "Bit NULL")]
        public bool IsCash
        {
            get { return this.m_IsCash; }
            set
            {
                this.m_IsCash = value;
                this.NotifyPropertyChanged("IsCash");
            }
        }


        [DBColumn(Name = "IsBank", Storage = "m_IsBank", DbType = "Bit NULL")]
        public bool IsBank
        {
            get { return this.m_IsBank; }
            set
            {
                this.m_IsBank = value;
                this.NotifyPropertyChanged("IsBank");
            }
        }


        [DBColumn(Name = "IsInstrument", Storage = "m_IsInstrument", DbType = "Bit NULL")]
        public bool IsInstrument
        {
            get { return this.m_IsInstrument; }
            set
            {
                this.m_IsInstrument = value;
                this.NotifyPropertyChanged("IsInstrument");
            }
        }

        [DBColumn(Name = "IsGrossProfit", Storage = "m_IsGrossProfit", DbType = "Bit NULL")]
        public bool IsGrossProfit
        {
            get { return this.m_IsGrossProfit; }
            set
            {
                this.m_IsGrossProfit = value;
                this.NotifyPropertyChanged("IsGrossProfit");
            }
        }

        [DBColumn(Name = "IsInventory", Storage = "m_IsInventory", DbType = "Bit NULL")]
        public bool IsInventory
        {
            get { return this.m_IsInventory; }
            set
            {
                this.m_IsInventory = value;
                this.NotifyPropertyChanged("IsInventory");
            }
        }


        [DBColumn(Name = "CashFlowGroupID", Storage = "m_CashFlowGroupID", DbType = "Int NULL")]
        public int CashFlowGroupID
        {
            get { return this.m_CashFlowGroupID; }
            set
            {
                this.m_CashFlowGroupID = value;
                this.NotifyPropertyChanged("CashFlowGroupID");
            }
        }

        [DBColumn(Name = "ShowAlways", Storage = "m_ShowAlways", DbType = "Bit NULL")]
        public bool ShowAlways
        {
            get { return this.m_ShowAlways; }
            set
            {
                this.m_ShowAlways = value;
                this.NotifyPropertyChanged("ShowAlways");
            }
        }

        #endregion //properties
    }
}
