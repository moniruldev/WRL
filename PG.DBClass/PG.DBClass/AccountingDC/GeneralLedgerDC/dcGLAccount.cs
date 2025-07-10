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
    [DBTable(Name = "tblGLAccount")]
    public partial class dcGLAccount : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GLAccountID = 0;
        private int m_GLGroupID = 0;
        private int m_GLAccountTypeID = 0;
        private int m_GLAccountIDParent = 0;
        private int m_CompanyID = 0;
        private string m_GLAccountCode = string.Empty;
        private int m_GLAccountNo = 0;
        private string m_GLAccountName = string.Empty;
        private string m_GLAccountNameB = string.Empty;
        private string m_GLAccountNamePrint = string.Empty;
        private string m_GLAccountNameAlias = string.Empty;
        private string m_GLAccountNameSys = string.Empty;
        private string m_GLAccountDesc = string.Empty;
        private int m_GLAccountSLNo = 0;
        private int m_BalanceType = 0;
        private int m_CashFlowGroupID = 0;
        private DateTime? m_CreateDate = null;
        private bool m_IsClosed = false;
        private DateTime? m_ClosedDate = null;
        private int m_LastSubLedgerNo = 0;
        private bool m_IsDeleted = false;
        private bool m_IsSystem = false;
        private bool m_IsActive = false;
        private bool m_IsVisible = false;
        private string m_InstutitueACNo = string.Empty;
        private string m_InstituteName = string.Empty;
        private string m_InstituteBranchName = string.Empty;
        private string m_InstituteAddress = string.Empty;

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


        [DBColumn(Name = "GLAccountID", Storage = "m_GLAccountID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true,IsIdentity=true)]
        public int GLAccountID
        {
            get { return this.m_GLAccountID; }
            set
            {
                this.m_GLAccountID = value;
                this.NotifyPropertyChanged("GLAccountID");
            }
        }

        [DBColumn(Name = "GLGroupID", Storage = "m_GLGroupID", DbType = "Int NULL")]
        public int GLGroupID
        {
            get { return this.m_GLGroupID; }
            set
            {
                this.m_GLGroupID = value;
                this.NotifyPropertyChanged("GLGroupID");
            }
        }

        [DBColumn(Name = "GLAccountTypeID", Storage = "m_GLAccountTypeID", DbType = "Int NULL")]
        public int GLAccountTypeID
        {
            get { return this.m_GLAccountTypeID; }
            set
            {
                this.m_GLAccountTypeID = value;
                this.NotifyPropertyChanged("GLAccountTypeID");
            }
        }

        [DBColumn(Name = "GLAccountIDParent", Storage = "m_GLAccountIDParent", DbType = "Int NULL")]
        public int GLAccountIDParent
        {
            get { return this.m_GLAccountIDParent; }
            set
            {
                this.m_GLAccountIDParent = value;
                this.NotifyPropertyChanged("GLAccountIDParent");
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

        [DBColumn(Name = "GLAccountCode", Storage = "m_GLAccountCode", DbType = "NVarChar(20) NULL")]
        public string GLAccountCode
        {
            get { return this.m_GLAccountCode; }
            set
            {
                this.m_GLAccountCode = value;
                this.NotifyPropertyChanged("GLAccountCode");
            }
        }

        [DBColumn(Name = "GLAccountNo", Storage = "m_GLAccountNo", DbType = "Int NULL")]
        public int GLAccountNo
        {
            get { return this.m_GLAccountNo; }
            set
            {
                this.m_GLAccountNo = value;
                this.NotifyPropertyChanged("GLAccountNo");
            }
        }

        [DBColumn(Name = "GLAccountName", Storage = "m_GLAccountName", DbType = "NVarChar(100) NULL")]
        public string GLAccountName
        {
            get { return this.m_GLAccountName; }
            set
            {
                this.m_GLAccountName = value;
                this.NotifyPropertyChanged("GLAccountName");
            }
        }

        [DBColumn(Name = "GLAccountNameB", Storage = "m_GLAccountNameB", DbType = "NVarChar(100) NULL")]
        public string GLAccountNameB
        {
            get { return this.m_GLAccountNameB; }
            set
            {
                this.m_GLAccountNameB = value;
                this.NotifyPropertyChanged("GLAccountNameB");
            }
        }

        [DBColumn(Name = "GLAccountNamePrint", Storage = "m_GLAccountNamePrint", DbType = "NVarChar(100) NULL")]
        public string GLAccountNamePrint
        {
            get { return this.m_GLAccountNamePrint; }
            set
            {
                this.m_GLAccountNamePrint = value;
                this.NotifyPropertyChanged("GLAccountNamePrint");
            }
        }

        [DBColumn(Name = "GLAccountNameAlias", Storage = "m_GLAccountNameAlias", DbType = "NVarChar(100) NULL")]
        public string GLAccountNameAlias
        {
            get { return this.m_GLAccountNameAlias; }
            set
            {
                this.m_GLAccountNameAlias = value;
                this.NotifyPropertyChanged("GLAccountNameAlias");
            }
        }

        [DBColumn(Name = "GLAccountNameSys", Storage = "m_GLAccountNameSys", DbType = "NVarChar(100) NULL")]
        public string GLAccountNameSys
        {
            get { return this.m_GLAccountNameSys; }
            set
            {
                this.m_GLAccountNameSys = value;
                this.NotifyPropertyChanged("GLAccountNameSys");
            }
        }

        [DBColumn(Name = "GLAccountDesc", Storage = "m_GLAccountDesc", DbType = "NVarChar(200) NULL")]
        public string GLAccountDesc
        {
            get { return this.m_GLAccountDesc; }
            set
            {
                this.m_GLAccountDesc = value;
                this.NotifyPropertyChanged("GLAccountDesc");
            }
        }

        [DBColumn(Name = "GLAccountSLNo", Storage = "m_GLAccountSLNo", DbType = "Int NULL")]
        public int GLAccountSLNo
        {
            get { return this.m_GLAccountSLNo; }
            set
            {
                this.m_GLAccountSLNo = value;
                this.NotifyPropertyChanged("GLAccountSLNo");
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

        [DBColumn(Name = "CreateDate", Storage = "m_CreateDate", DbType = "DateTime NULL")]
        public DateTime? CreateDate
        {
            get { return this.m_CreateDate; }
            set
            {
                this.m_CreateDate = value;
                this.NotifyPropertyChanged("CreateDate");
            }
        }

        [DBColumn(Name = "IsClosed", Storage = "m_IsClosed", DbType = "Bit NULL")]
        public bool IsClosed
        {
            get { return this.m_IsClosed; }
            set
            {
                this.m_IsClosed = value;
                this.NotifyPropertyChanged("IsClosed");
            }
        }

        [DBColumn(Name = "ClosedDate", Storage = "m_ClosedDate", DbType = "DateTime NULL")]
        public DateTime? ClosedDate
        {
            get { return this.m_ClosedDate; }
            set
            {
                this.m_ClosedDate = value;
                this.NotifyPropertyChanged("ClosedDate");
            }
        }

        [DBColumn(Name = "LastSubLedgerNo", Storage = "m_LastSubLedgerNo", DbType = "Int NULL")]
        public int LastSubLedgerNo
        {
            get { return this.m_LastSubLedgerNo; }
            set
            {
                this.m_LastSubLedgerNo = value;
                this.NotifyPropertyChanged("LastSubLedgerNo");
            }
        }

        [DBColumn(Name = "IsDeleted", Storage = "m_IsDeleted", DbType = "Bit NULL")]
        public bool IsDeleted
        {
            get { return this.m_IsDeleted; }
            set
            {
                this.m_IsDeleted = value;
                this.NotifyPropertyChanged("IsDeleted");
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

        [DBColumn(Name = "InstutitueACNo", Storage = "m_InstutitueACNo", DbType = "NVarChar(50) NULL")]
        public string InstutitueACNo
        {
            get { return this.m_InstutitueACNo; }
            set
            {
                this.m_InstutitueACNo = value;
                this.NotifyPropertyChanged("InstutitueACNo");
            }
        }

        [DBColumn(Name = "InstituteName", Storage = "m_InstituteName", DbType = "NVarChar(100) NULL")]
        public string InstituteName
        {
            get { return this.m_InstituteName; }
            set
            {
                this.m_InstituteName = value;
                this.NotifyPropertyChanged("InstituteName");
            }
        }

        [DBColumn(Name = "InstituteBranchName", Storage = "m_InstituteBranchName", DbType = "NVarChar(100) NULL")]
        public string InstituteBranchName
        {
            get { return this.m_InstituteBranchName; }
            set
            {
                this.m_InstituteBranchName = value;
                this.NotifyPropertyChanged("InstituteBranchName");
            }
        }

        [DBColumn(Name = "InstituteAddress", Storage = "m_InstituteAddress", DbType = "NVarChar(200) NULL")]
        public string InstituteAddress
        {
            get { return this.m_InstituteAddress; }
            set
            {
                this.m_InstituteAddress = value;
                this.NotifyPropertyChanged("InstituteAddress");
            }
        }

        #endregion //properties
    }
    
    public partial class dcGLAccount
    {
        #region cusotm properties

        private int m_GLClassID = 0;
        public int GLClassID
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLClassID; }
            set { this.m_GLClassID = value; }
        }

        private string m_GLGroupCode = string.Empty;
        public string GLGroupCode
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupCode; }
            set { this.m_GLGroupCode = value; }
        }

        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupName; }
            set { this.m_GLGroupName = value; }
        }

        private string m_GLGroupNameShort = string.Empty;
        public string GLGroupNameShort
        {
            get { return m_GLGroupNameShort; }
            set { this.m_GLGroupNameShort = value; }
        }

        public string GLGroupNameShortName
        {
            get
            {
                string s = string.Empty;
                if (m_GLGroupNameShort != string.Empty)
                {
                    s = m_GLGroupNameShort + ", " + m_GLGroupName;
                }
                else
                {
                    s = m_GLGroupName;
                }
                return s;
            }
        }


        public string GLGroupKey
        {
            get
            {
                string pKey = string.Empty;
                if (m_GLGroupID == 0)
                {
                    pKey = "gclid" + m_GLClassID.ToString();
                }
                else
                {
                    pKey = "grpid" + m_GLGroupID.ToString();
                }
                return pKey;
            }
        }

        private string m_GLAccountTypeName = string.Empty;
        public string GLAccountTypeName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountTypeName; }
            set { this.m_GLAccountTypeName = value; }
        }

        private int m_GLGroupClassID = 0;
        public int GLGroupClassID
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupClassID; }
            set { this.m_GLGroupClassID = value; }
        }


        private string m_GLGroupClassName = string.Empty;
        public string GLGroupClassName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupClassName; }
            set { this.m_GLGroupClassName = value; }
        }


        private string m_GLClassName = string.Empty;
        public string GLClassName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLClassName; }
            set { this.m_GLClassName = value; }
        }



        private bool m_IsInstrument = false;
        public bool IsInstrument
        {
            get { return m_IsInstrument; }
            set { this.m_IsInstrument = value; }
        }

        private bool m_IsCash = false;
        public bool IsCash
        {
            get { return m_IsCash; }
            set { this.m_IsCash = value; }
        } 

        private string m_GLAccountCodeParent = string.Empty;
        public string GLAccountCodeParent
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountCodeParent; }
            set { this.m_GLAccountCodeParent = value; }
        }


        private string m_GLAccountNameParent = string.Empty;
        public string GLAccountNameParent
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountNameParent; }
            set { this.m_GLAccountNameParent = value; }
        }




        private string m_GLAccountSubTypeName = string.Empty;
        public string GLAccountSubTypeName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountSubTypeName; }
            set { this.m_GLAccountSubTypeName = value; }
        }


        private int m_GLAccountLevel = 0;
        public int GLAccountLevel
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountLevel; }
            set { this.m_GLAccountLevel = value; }
        }

        private int m_ChildAccountCount = 0;
        public int ChildAccountCount
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_ChildAccountCount; }
            set { this.m_ChildAccountCount = value; }
        }



        private string m_GLAccountNameIndent = string.Empty;
        public string GLAccountNameIndent
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountNameIndent; }
            set { this.m_GLAccountNameIndent = value; }
        }



        public string GLAccountCodeName
        {
            get
            {
                string codeName = m_GLAccountCode + ", " + m_GLAccountName;
                return codeName;
            }
        }

        public string GLAccountCodeNameGroup
        {
            get
            {
                string codeName = m_GLAccountCode + ", " + m_GLAccountName + ", " + m_GLGroupName;
                return codeName;
            }
        }

        public string GLAccountCodeNameGroupClass
        {
            get
            {
                string codeName = m_GLAccountCode + ", " + m_GLAccountName + ", " + m_GLGroupName + " (" + m_GLClassName + ")";
                return codeName;
            }
        }




        private dcGLAccountHistory m_GLAccountHistory = null;
        public dcGLAccountHistory GLAccountHistory
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountHistory; }
            set { this.m_GLAccountHistory = value; }
        }



        #endregion

        
        #region Calcluation fields
        private int m_AccYearID = 0;
        public int AccYearID
        {
            get { return m_AccYearID; }
            set { m_AccYearID = value; }
        }


        #region openingYear
        private decimal m_OpenDebitAmtYear = 0;
        public decimal OpenDebitAmtYear
        {
            get { return this.m_OpenDebitAmtYear; }
            set { this.m_OpenDebitAmtYear = value; }
        }

        private decimal m_OpenCreditAmtYear = 0;
        public decimal OpenCreditAmtYear
        {
            get { return this.m_OpenCreditAmtYear; }
            set { this.m_OpenCreditAmtYear = value; }
        }


        private decimal m_OpenAmtYear = 0;
        public decimal OpenAmtYear
        {
            get { return m_OpenAmtYear; }
            set { m_OpenAmtYear = value; }
        }

        private decimal m_OpenDebitBalanceAmtYear = 0;
        public decimal OpenDebitBalanceAmtYear
        {
            get { return m_OpenDebitBalanceAmtYear; }
            set { m_OpenDebitBalanceAmtYear = value; }
        }

        private decimal m_OpenCreditBalanceAmtYear = 0;
        public decimal OpenCreditBalanceAmtYear
        {
            get { return m_OpenCreditBalanceAmtYear; }
            set { m_OpenCreditBalanceAmtYear = value; }
        }



        private decimal m_OpenBalnceAmtYear = 0;
        public decimal OpenBalnceAmtYear
        {
            get { return m_OpenBalnceAmtYear; }
            set { m_OpenBalnceAmtYear = value; }
        }

        private int m_DrCrOpenYear = 0;
        public int DrCrOpenYear
        {
            get { return this.m_DrCrOpenYear; }
            set { this.m_DrCrOpenYear = value; }
        }

        private string m_DrCrOpenTextYear = string.Empty;
        public string DrCrOpenTextYear
        {
            get { return this.m_DrCrOpenTextYear; }
            set { this.m_DrCrOpenTextYear = value; }
        }

        #endregion

        #region opening daterange
        private decimal m_OpenDebitAmtDateRange = 0;
        public decimal OpenDebitAmtDateRange
        {
            get { return this.m_OpenDebitAmtDateRange; }
            set { this.m_OpenDebitAmtDateRange = value; }
        }

        private decimal m_OpenCreditAmtDateRange = 0;
        public decimal OpenCreditAmtDateRange
        {
            get { return this.m_OpenCreditAmtDateRange; }
            set { this.m_OpenCreditAmtDateRange = value; }
        }


        private decimal m_OpenAmtDateRange = 0;
        public decimal OpenAmtDateRange
        {
            get { return m_OpenAmtDateRange; }
            set { m_OpenAmtDateRange = value; }
        }


        private decimal m_OpenDebitBalanceAmtDateRange = 0;
        public decimal OpenDebitBalanceAmtDateRange
        {
            get { return m_OpenDebitBalanceAmtDateRange; }
            set { m_OpenDebitBalanceAmtDateRange = value; }
        }

        private decimal m_OpenCreditBalanceAmtDateRange = 0;
        public decimal OpenCreditBalanceAmtDateRange
        {
            get { return m_OpenCreditBalanceAmtDateRange; }
            set { m_OpenCreditBalanceAmtDateRange = value; }
        }



        private decimal m_OpenBalanceAmtDateRange = 0;
        public decimal OpenBalanceAmtDateRange
        {
            get { return m_OpenBalanceAmtDateRange; }
            set { m_OpenBalanceAmtDateRange = value; }
        }

        private int m_DrCrOpenDateRange = 0;
        public int DrCrOpenDateRange
        {
            get { return this.m_DrCrOpenDateRange; }
            set { this.m_DrCrOpenDateRange = value; }
        }

        private string m_DrCrOpenTextDateRange = string.Empty;
        public string DrCrOpenTextDateRange
        {
            get { return this.m_DrCrOpenTextDateRange; }
            set { this.m_DrCrOpenTextDateRange = value; }
        }

        #endregion

        #region opening
        private decimal m_OpenDebitAmt = 0;
        public decimal OpenDebitAmt
        {
            get { return this.m_OpenDebitAmt; }
            set { this.m_OpenDebitAmt = value; }
        }

        private decimal m_OpenCreditAmt = 0;
        public decimal OpenCreditAmt
        {
            get { return this.m_OpenCreditAmt; }
            set { this.m_OpenCreditAmt = value; }
        }


        private decimal m_OpenAmt = 0;
        public decimal OpenAmt
        {
            get { return m_OpenAmt; }
            set { m_OpenAmt = value; }
        }


        private decimal m_OpenDebitBalanceAmt = 0;
        public decimal OpenDebitBalanceAmt
        {
            get { return m_OpenDebitBalanceAmt; }
            set { m_OpenDebitBalanceAmt = value; }
        }

        private decimal m_OpenCreditBalanceAmt = 0;
        public decimal OpenCreditBalanceAmt
        {
            get { return m_OpenCreditBalanceAmt; }
            set { m_OpenCreditBalanceAmt = value; }
        }


        private decimal m_OpenBalanceAmt = 0;
        public decimal OpenBalanceAmt
        {
            get { return m_OpenBalanceAmt; }
            set { m_OpenBalanceAmt = value; }
        }

        private int m_DrCrOpen = 0;
        public int DrCrOpen
        {
            get { return this.m_DrCrOpen; }
            set { this.m_DrCrOpen = value; }
        }

        private string m_DrCrOpenText = string.Empty;
        public string DrCrOpenText
        {
            get { return this.m_DrCrOpenText; }
            set { this.m_DrCrOpenText = value; }
        }

        #endregion

        #region trans
        private decimal m_DebitAmt = 0;
        public decimal DebitAmt
        {
            get { return m_DebitAmt; }
            set { m_DebitAmt = value; }
        }

        private decimal m_CreditAmt = 0;
        public decimal CreditAmt
        {
            get { return m_CreditAmt; }
            set { m_CreditAmt = value; }
        }

        private decimal m_TranAmt = 0;
        public decimal TranAmt
        {
            get { return m_TranAmt; }
            set { m_TranAmt = value; }
        }

        private decimal m_TranDebitBalanceAmt = 0;
        public decimal TranDebitBalanceAmt
        {
            get { return m_TranDebitBalanceAmt; }
            set { m_TranDebitBalanceAmt = value; }
        }

        private decimal m_TranCreditBalanceAmt = 0;
        public decimal TranCreditBalanceAmt
        {
            get { return m_TranCreditBalanceAmt; }
            set { m_TranCreditBalanceAmt = value; }
        }


        private decimal m_TranBalanceAmt = 0;
        public decimal TranBalanceAmt
        {
            get { return m_TranBalanceAmt; }
            set { m_TranBalanceAmt = value; }
        }
        private int m_DrCrTranBalance = 0;
        public int DrCrTranBalance
        {
            get { return this.m_DrCrTranBalance; }
            set { this.m_DrCrTranBalance = value; }
        }

        private string m_DrCrTranBalanceText = string.Empty;
        public string DrCrTranBalanceText
        {
            get { return this.m_DrCrTranBalanceText; }
            set { this.m_DrCrTranBalanceText = value; }
        }


        #endregion

        #region closing

        private decimal m_CloseDebitAmt = 0;
        public decimal CloseDebitAmt
        {
            get { return this.m_CloseDebitAmt; }
            set { this.m_CloseDebitAmt = value; }
        }

        private decimal m_CloseCreditAmt = 0;
        public decimal CloseCreditAmt
        {
            get { return this.m_CloseCreditAmt; }
            set { this.m_CloseCreditAmt = value; }
        }


        private decimal m_CloseAmt = 0;
        public decimal CloseAmt
        {
            get { return this.m_CloseAmt; }
            set { this.m_CloseAmt = value; }
        }


        private decimal m_CloseDebitBalanceAmt = 0;
        public decimal CloseDebitBalanceAmt
        {
            get { return m_CloseDebitBalanceAmt; }
            set { m_CloseDebitBalanceAmt = value; }
        }

        private decimal m_CloseCreditBalanceAmt = 0;
        public decimal CloseCreditBalanceAmt
        {
            get { return m_CloseCreditBalanceAmt; }
            set { m_CloseCreditBalanceAmt = value; }
        }

        private decimal m_CloseBalanceAmt = 0;
        public decimal CloseBalanceAmt
        {
            get { return this.m_CloseBalanceAmt; }
            set { this.m_CloseBalanceAmt = value; }
        }

        private int m_DrCrCloseBalance = 0;
        public int DrCrCloseBalance
        {
            get { return this.m_DrCrCloseBalance; }
            set { this.m_DrCrCloseBalance = value; }
        }

        private string m_DrCrCloseBalanceText = string.Empty;
        public string DrCrCloseBalanceText
        {
            get { return this.m_DrCrCloseBalanceText; }
            set { this.m_DrCrCloseBalanceText = value; }
        }
      
        #endregion


        #endregion
    }

}
