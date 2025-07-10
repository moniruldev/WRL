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
    [DBTable(Name = "tblAccSettings")]
    public partial class dcAccSettings : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AccSettingsID = 0;
        private int m_CompanyID = 0;
        private int m_AccYearID = 0;
        private bool m_IsInventoryPeriodic = true;
        private bool m_ApplyGroupIsVisible = false;
        private bool m_GLGroupShowByShortName = false;
        private bool m_AllowChangeGLGroupCLass = false;
        private bool m_AllowGrpAccUnderPLGrp = false;
        private bool m_AllowJournalUnpost = false;
        private bool m_AutoGLAccountCode = false;
        private bool m_PrefixGLAccountCode = false;
        private string m_GLAccountCodePrefixSep = string.Empty;
        private int m_GLAccountCodeLength = 0;
        private bool m_IsPadGLAccountCode = false;
        private string m_GLAccountCodePadChar = string.Empty;
        private int m_DefJournalReportFormat = 0;
        private int m_DefJouralReportOpenType = 0;
        private int m_DefJournalReportViewMode = 0;
        private int m_DefJournalReportExportType = 0;
        private bool m_IsIERsClientPrint = false;
        private int m_LanguageID_01 = 0;
        private int m_LanguageID_02 = 0;
        private bool m_DisableReportPrint = false;
        private bool m_DisableReportExport = false;
        private bool m_DisablePDFPrint = false;
        private bool m_DisablePDFSaveAs = false;

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


        [DBColumn(Name = "AccSettingsID", Storage = "m_AccSettingsID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int AccSettingsID
        {
            get { return this.m_AccSettingsID; }
            set
            {
                this.m_AccSettingsID = value;
                this.NotifyPropertyChanged("AccSettingsID");
            }
        }

        [DBColumn(Name = "CompanyID", Storage = "m_CompanyID", DbType = "Int NULL")]
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set
            {
                this.m_CompanyID = value;
                this.NotifyPropertyChanged("CompanyID");
            }
        }

        [DBColumn(Name = "AccYearID", Storage = "m_AccYearID", DbType = "Int NULL")]
        public int AccYearID
        {
            get { return this.m_AccYearID; }
            set
            {
                this.m_AccYearID = value;
                this.NotifyPropertyChanged("AccYearID");
            }
        }


        [DBColumn(Name = "IsInventoryPeriodic", Storage = "m_IsInventoryPeriodic", DbType = "Bit NULL")]
        public bool IsInventoryPeriodic
        {
            get { return this.m_IsInventoryPeriodic; }
            set
            {
                this.m_IsInventoryPeriodic = value;
                this.NotifyPropertyChanged("IsInventoryPeriodic");
            }
        }


        [DBColumn(Name = "ApplyGroupIsVisible", Storage = "m_ApplyGroupIsVisible", DbType = "Bit NULL")]
        public bool ApplyGroupIsVisible
        {
            get { return this.m_ApplyGroupIsVisible; }
            set
            {
                this.m_ApplyGroupIsVisible = value;
                this.NotifyPropertyChanged("ApplyGroupIsVisible");
            }
        }

        [DBColumn(Name = "GLGroupShowByShortName", Storage = "m_GLGroupShowByShortName", DbType = "Bit NULL")]
        public bool GLGroupShowByShortName
        {
            get { return this.m_GLGroupShowByShortName; }
            set
            {
                this.m_GLGroupShowByShortName = value;
                this.NotifyPropertyChanged("GLGroupShowByShortName");
            }
        }


        [DBColumn(Name = "AllowChangeGLGroupCLass", Storage = "m_AllowChangeGLGroupCLass", DbType = "Bit NULL")]
        public bool AllowChangeGLGroupCLass
        {
            get { return this.m_AllowChangeGLGroupCLass; }
            set
            {
                this.m_AllowChangeGLGroupCLass = value;
                this.NotifyPropertyChanged("AllowChangeGLGroupCLass");
            }
        }

        [DBColumn(Name = "AllowGrpAccUnderPLGrp", Storage = "m_AllowGrpAccUnderPLGrp", DbType = "Bit NULL")]
        public bool AllowGrpAccUnderPLGrp
        {
            get { return this.m_AllowGrpAccUnderPLGrp; }
            set
            {
                this.m_AllowGrpAccUnderPLGrp = value;
                this.NotifyPropertyChanged("AllowGrpAccUnderPLGrp");
            }
        }

        [DBColumn(Name = "AllowJournalUnpost", Storage = "m_AllowJournalUnpost", DbType = "Bit NULL")]
        public bool AllowJournalUnpost
        {
            get { return this.m_AllowJournalUnpost; }
            set
            {
                this.m_AllowJournalUnpost = value;
                this.NotifyPropertyChanged("AllowJournalUnpost");
            }
        }

        [DBColumn(Name = "AutoGLAccountCode", Storage = "m_AutoGLAccountCode", DbType = "Bit NULL")]
        public bool AutoGLAccountCode
        {
            get { return this.m_AutoGLAccountCode; }
            set
            {
                this.m_AutoGLAccountCode = value;
                this.NotifyPropertyChanged("AutoGLAccountCode");
            }
        }

        [DBColumn(Name = "PrefixGLAccountCode", Storage = "m_PrefixGLAccountCode", DbType = "Bit NULL")]
        public bool PrefixGLAccountCode
        {
            get { return this.m_PrefixGLAccountCode; }
            set
            {
                this.m_PrefixGLAccountCode = value;
                this.NotifyPropertyChanged("PrefixGLAccountCode");
            }
        }

        [DBColumn(Name = "GLAccountCodePrefixSep", Storage = "m_GLAccountCodePrefixSep", DbType = "NVarChar(50) NULL")]
        public string GLAccountCodePrefixSep
        {
            get { return this.m_GLAccountCodePrefixSep; }
            set
            {
                this.m_GLAccountCodePrefixSep = value;
                this.NotifyPropertyChanged("GLAccountCodePrefixSep");
            }
        }

        [DBColumn(Name = "GLAccountCodeLength", Storage = "m_GLAccountCodeLength", DbType = "Int NULL")]
        public int GLAccountCodeLength
        {
            get { return this.m_GLAccountCodeLength; }
            set
            {
                this.m_GLAccountCodeLength = value;
                this.NotifyPropertyChanged("GLAccountCodeLength");
            }
        }

        [DBColumn(Name = "IsPadGLAccountCode", Storage = "m_IsPadGLAccountCode", DbType = "Bit NULL")]
        public bool IsPadGLAccountCode
        {
            get { return this.m_IsPadGLAccountCode; }
            set
            {
                this.m_IsPadGLAccountCode = value;
                this.NotifyPropertyChanged("IsPadGLAccountCode");
            }
        }

        [DBColumn(Name = "GLAccountCodePadChar", Storage = "m_GLAccountCodePadChar", DbType = "NVarChar(50) NULL")]
        public string GLAccountCodePadChar
        {
            get { return this.m_GLAccountCodePadChar; }
            set
            {
                this.m_GLAccountCodePadChar = value;
                this.NotifyPropertyChanged("GLAccountCodePadChar");
            }
        }

        [DBColumn(Name = "DefJournalReportFormat", Storage = "m_DefJournalReportFormat", DbType = "Int NULL")]
        public int DefJournalReportFormat
        {
            get { return this.m_DefJournalReportFormat; }
            set
            {
                this.m_DefJournalReportFormat = value;
                this.NotifyPropertyChanged("DefJournalReportFormat");
            }
        }

        [DBColumn(Name = "DefJouralReportOpenType", Storage = "m_DefJouralReportOpenType", DbType = "Int NULL")]
        public int DefJouralReportOpenType
        {
            get { return this.m_DefJouralReportOpenType; }
            set
            {
                this.m_DefJouralReportOpenType = value;
                this.NotifyPropertyChanged("DefJouralReportOpenType");
            }
        }

        [DBColumn(Name = "DefJournalReportViewMode", Storage = "m_DefJournalReportViewMode", DbType = "Int NULL")]
        public int DefJournalReportViewMode
        {
            get { return this.m_DefJournalReportViewMode; }
            set
            {
                this.m_DefJournalReportViewMode = value;
                this.NotifyPropertyChanged("DefJournalReportViewMode");
            }
        }

        [DBColumn(Name = "DefJournalReportExportType", Storage = "m_DefJournalReportExportType", DbType = "Int NULL")]
        public int DefJournalReportExportType
        {
            get { return this.m_DefJournalReportExportType; }
            set
            {
                this.m_DefJournalReportExportType = value;
                this.NotifyPropertyChanged("DefJournalReportExportType");
            }
        }

        [DBColumn(Name = "IsIERsClientPrint", Storage = "m_IsIERsClientPrint", DbType = "Bit NULL")]
        public bool IsIERsClientPrint
        {
            get { return this.m_IsIERsClientPrint; }
            set
            {
                this.m_IsIERsClientPrint = value;
                this.NotifyPropertyChanged("IsIERsClientPrint");
            }
        }

        [DBColumn(Name = "LanguageID_01", Storage = "m_LanguageID_01", DbType = "Int NULL")]
        public int LanguageID_01
        {
            get { return this.m_LanguageID_01; }
            set
            {
                this.m_LanguageID_01 = value;
                this.NotifyPropertyChanged("LanguageID_01");
            }
        }

        [DBColumn(Name = "LanguageID_02", Storage = "m_LanguageID_02", DbType = "Int NULL")]
        public int LanguageID_02
        {
            get { return this.m_LanguageID_02; }
            set
            {
                this.m_LanguageID_02 = value;
                this.NotifyPropertyChanged("LanguageID_02");
            }
        }

        [DBColumn(Name = "DisableReportPrint", Storage = "m_DisableReportPrint", DbType = "Bit NULL")]
        public bool DisableReportPrint
        {
            get { return this.m_DisableReportPrint; }
            set
            {
                this.m_DisableReportPrint = value;
                this.NotifyPropertyChanged("DisableReportPrint");
            }
        }

        [DBColumn(Name = "DisableReportExport", Storage = "m_DisableReportExport", DbType = "Bit NULL")]
        public bool DisableReportExport
        {
            get { return this.m_DisableReportExport; }
            set
            {
                this.m_DisableReportExport = value;
                this.NotifyPropertyChanged("DisableReportExport");
            }
        }

        [DBColumn(Name = "DisablePDFPrint", Storage = "m_DisablePDFPrint", DbType = "Bit NULL")]
        public bool DisablePDFPrint
        {
            get { return this.m_DisablePDFPrint; }
            set
            {
                this.m_DisablePDFPrint = value;
                this.NotifyPropertyChanged("DisablePDFPrint");
            }
        }

        [DBColumn(Name = "DisablePDFSaveAs", Storage = "m_DisablePDFSaveAs", DbType = "Bit NULL")]
        public bool DisablePDFSaveAs
        {
            get { return this.m_DisablePDFSaveAs; }
            set
            {
                this.m_DisablePDFSaveAs = value;
                this.NotifyPropertyChanged("DisablePDFSaveAs");
            }
        }

        #endregion //properties
    }
}
