using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.SystemDC
{
    [Serializable]
    [DBTable(Name = "tblAppInfo")]
    public class dcAppInfo : DBBaseClass , INotifyPropertyChanged
    {
        #region private members

        private int m_AppID = 0;
        private bool m_IsAppDate = false;
        private DateTime? m_AppDate = null;
        private DateTime? m_CAppDateSys = null;
        private string m_CAppDateUserID = string.Empty;
        private DateTime? m_AppStDtApp = null;
        private DateTime? m_AppStDtSys = null;
        private DateTime? m_ValidTrnDate = null;
        private DateTime? m_YearStartDate = null;
        private DateTime? m_YearEndDate = null;
        private byte m_FiscalMonth = 0;
        private bool m_BackDateEntry = false;
        private bool m_CheckReg = false;
        private DateTime? m_RegDate = null;
        private int m_RegDays = 0;
        private bool m_IsValid = false;
        private bool m_IsPassEncrypted = false;
        private bool m_IsAppRole = false;
        private string m_AppRoleName = string.Empty;
        private string m_AppRolePass = string.Empty;
        private bool m_MenuLinkLeadingDot = false;

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


        [DBColumn(Name = "AppID", Storage = "m_AppID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int AppID
        {
            get { return this.m_AppID; }
            set
            {
                this.m_AppID = value;
                this.NotifyPropertyChanged("AppID");
            }
        }

        [DBColumn(Name = "IsAppDate", Storage = "m_IsAppDate", DbType = "Bit NOT NULL")]
        public bool IsAppDate
        {
            get { return this.m_IsAppDate; }
            set
            {
                this.m_IsAppDate = value;
                this.NotifyPropertyChanged("IsAppDate");
            }
        }

        [DBColumn(Name = "AppDate", Storage = "m_AppDate", DbType = "SmallDateTime NULL")]
        public DateTime? AppDate
        {
            get { return this.m_AppDate; }
            set
            {
                this.m_AppDate = value;
                this.NotifyPropertyChanged("AppDate");
            }
        }

        [DBColumn(Name = "CAppDateSys", Storage = "m_CAppDateSys", DbType = "SmallDateTime NULL")]
        public DateTime? CAppDateSys
        {
            get { return this.m_CAppDateSys; }
            set
            {
                this.m_CAppDateSys = value;
                this.NotifyPropertyChanged("CAppDateSys");
            }
        }

        [DBColumn(Name = "CAppDateUserID", Storage = "m_CAppDateUserID", DbType = "NVarChar(20) NULL")]
        public string CAppDateUserID
        {
            get { return this.m_CAppDateUserID; }
            set
            {
                this.m_CAppDateUserID = value;
                this.NotifyPropertyChanged("CAppDateUserID");
            }
        }

        [DBColumn(Name = "AppStDtApp", Storage = "m_AppStDtApp", DbType = "SmallDateTime NULL")]
        public DateTime? AppStDtApp
        {
            get { return this.m_AppStDtApp; }
            set
            {
                this.m_AppStDtApp = value;
                this.NotifyPropertyChanged("AppStDtApp");
            }
        }

        [DBColumn(Name = "AppStDtSys", Storage = "m_AppStDtSys", DbType = "SmallDateTime NULL")]
        public DateTime? AppStDtSys
        {
            get { return this.m_AppStDtSys; }
            set
            {
                this.m_AppStDtSys = value;
                this.NotifyPropertyChanged("AppStDtSys");
            }
        }

        [DBColumn(Name = "ValidTrnDate", Storage = "m_ValidTrnDate", DbType = "SmallDateTime NULL")]
        public DateTime? ValidTrnDate
        {
            get { return this.m_ValidTrnDate; }
            set
            {
                this.m_ValidTrnDate = value;
                this.NotifyPropertyChanged("ValidTrnDate");
            }
        }

        [DBColumn(Name = "YearStartDate", Storage = "m_YearStartDate", DbType = "SmallDateTime NULL")]
        public DateTime? YearStartDate
        {
            get { return this.m_YearStartDate; }
            set
            {
                this.m_YearStartDate = value;
                this.NotifyPropertyChanged("YearStartDate");
            }
        }

        [DBColumn(Name = "YearEndDate", Storage = "m_YearEndDate", DbType = "SmallDateTime NULL")]
        public DateTime? YearEndDate
        {
            get { return this.m_YearEndDate; }
            set
            {
                this.m_YearEndDate = value;
                this.NotifyPropertyChanged("YearEndDate");
            }
        }

        [DBColumn(Name = "FiscalMonth", Storage = "m_FiscalMonth", DbType = "TinyInt NULL")]
        public byte FiscalMonth
        {
            get { return this.m_FiscalMonth; }
            set
            {
                this.m_FiscalMonth = value;
                this.NotifyPropertyChanged("FiscalMonth");
            }
        }

        [DBColumn(Name = "BackDateEntry", Storage = "m_BackDateEntry", DbType = "Bit NOT NULL")]
        public bool BackDateEntry
        {
            get { return this.m_BackDateEntry; }
            set
            {
                this.m_BackDateEntry = value;
                this.NotifyPropertyChanged("BackDateEntry");
            }
        }

        [DBColumn(Name = "CheckReg", Storage = "m_CheckReg", DbType = "Bit NOT NULL")]
        public bool CheckReg
        {
            get { return this.m_CheckReg; }
            set
            {
                this.m_CheckReg = value;
                this.NotifyPropertyChanged("CheckReg");
            }
        }

        [DBColumn(Name = "RegDate", Storage = "m_RegDate", DbType = "SmallDateTime NULL")]
        public DateTime? RegDate
        {
            get { return this.m_RegDate; }
            set
            {
                this.m_RegDate = value;
                this.NotifyPropertyChanged("RegDate");
            }
        }

        [DBColumn(Name = "RegDays", Storage = "m_RegDays", DbType = "Int NULL")]
        public int RegDays
        {
            get { return this.m_RegDays; }
            set
            {
                this.m_RegDays = value;
                this.NotifyPropertyChanged("RegDays");
            }
        }

        [DBColumn(Name = "IsValid", Storage = "m_IsValid", DbType = "Bit NOT NULL")]
        public bool IsValid
        {
            get { return this.m_IsValid; }
            set
            {
                this.m_IsValid = value;
                this.NotifyPropertyChanged("IsValid");
            }
        }

        [DBColumn(Name = "IsPassEncrypted", Storage = "m_IsPassEncrypted", DbType = "Bit NULL")]
        public bool IsPassEncrypted
        {
            get { return this.m_IsPassEncrypted; }
            set
            {
                this.m_IsPassEncrypted = value;
                this.NotifyPropertyChanged("IsPassEncrypted");
            }
        }

        [DBColumn(Name = "IsAppRole", Storage = "m_IsAppRole", DbType = "Bit NULL")]
        public bool IsAppRole
        {
            get { return this.m_IsAppRole; }
            set
            {
                this.m_IsAppRole = value;
                this.NotifyPropertyChanged("IsAppRole");
            }
        }

        [DBColumn(Name = "AppRoleName", Storage = "m_AppRoleName", DbType = "VarChar(50) NULL")]
        public string AppRoleName
        {
            get { return this.m_AppRoleName; }
            set
            {
                this.m_AppRoleName = value;
                this.NotifyPropertyChanged("AppRoleName");
            }
        }

        [DBColumn(Name = "AppRolePass", Storage = "m_AppRolePass", DbType = "VarChar(120) NULL")]
        public string AppRolePass
        {
            get { return this.m_AppRolePass; }
            set
            {
                this.m_AppRolePass = value;
                this.NotifyPropertyChanged("AppRolePass");
            }
        }


        [DBColumn(Name = "MenuLinkLeadingDot", Storage = "m_MenuLinkLeadingDot", DbType = "Bit NULL")]
        public bool MenuLinkLeadingDot
        {
            get { return this.m_MenuLinkLeadingDot; }
            set
            {
                this.m_MenuLinkLeadingDot = value;
                this.NotifyPropertyChanged("MenuLinkLeadingDot");
            }
        }

        #endregion //properties
    }
}
