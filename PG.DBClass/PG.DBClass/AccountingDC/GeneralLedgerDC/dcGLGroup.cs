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
    [DBTable(Name = "tblGLGroup")]
    public partial class dcGLGroup : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GLGroupID = 0;
        private int m_CompanyID = 0;
        private int m_GLClassID = 0;
        private int m_GLGroupClassID = 0;
        private int m_GLGroupLevel = 0;
        private int m_GLGroupIDParent = 0;
        private int m_GLGroupSLNo = 0;
        private string m_GLGroupCode = string.Empty;
        private string m_GLGroupNameShort = string.Empty;
        private string m_GLGroupName = string.Empty;
        private string m_GLGroupNameB = string.Empty;
        private string m_GLGroupNameAlias = string.Empty;
        private string m_GLGroupNamePrint = string.Empty;
        private string m_GLGroupNameSys = string.Empty;
        private string m_GLGroupCodeOS = string.Empty;
        private int m_BalanceType = 0;
        private bool m_ShowAsLedger = false;
        private bool m_ShowAlways = false;
        private bool m_IsCash = false;
        private bool m_IsBank = false;
        private bool m_IsInstrument = false;
        private bool m_IsInventory = false;
        private bool m_IsGrossProfit = false;
        private int m_CashFlowGroupID = 0;
        private int m_LastLedgerNo = 0;
        private bool m_IsDeleted = false;
        private bool m_IsSystem = false;
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


        [DBColumn(Name = "GLGroupID", Storage = "m_GLGroupID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity=true)]
        public int GLGroupID
        {
            get { return this.m_GLGroupID; }
            set
            {
                this.m_GLGroupID = value;
                this.NotifyPropertyChanged("GLGroupID");
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

        [DBColumn(Name = "GLGroupClassID", Storage = "m_GLGroupClassID", DbType = "Int NULL")]
        public int GLGroupClassID
        {
            get { return this.m_GLGroupClassID; }
            set
            {
                this.m_GLGroupClassID = value;
                this.NotifyPropertyChanged("GLGroupClassID");
            }
        }

        [DBColumn(Name = "GLGroupLevel", Storage = "m_GLGroupLevel", DbType = "Int NULL")]
        public int GLGroupLevel
        {
            get { return this.m_GLGroupLevel; }
            set
            {
                this.m_GLGroupLevel = value;
                this.NotifyPropertyChanged("GLGroupLevel");
            }
        }

        [DBColumn(Name = "GLGroupIDParent", Storage = "m_GLGroupIDParent", DbType = "Int NULL")]
        public int GLGroupIDParent
        {
            get { return this.m_GLGroupIDParent; }
            set
            {
                this.m_GLGroupIDParent = value;
                this.NotifyPropertyChanged("GLGroupIDParent");
            }
        }

        [DBColumn(Name = "GLGroupSLNo", Storage = "m_GLGroupSLNo", DbType = "Int NULL")]
        public int GLGroupSLNo
        {
            get { return this.m_GLGroupSLNo; }
            set
            {
                this.m_GLGroupSLNo = value;
                this.NotifyPropertyChanged("GLGroupSLNo");
            }
        }

        [DBColumn(Name = "GLGroupCode", Storage = "m_GLGroupCode", DbType = "NVarChar(20) NULL")]
        public string GLGroupCode
        {
            get { return this.m_GLGroupCode; }
            set
            {
                this.m_GLGroupCode = value;
                this.NotifyPropertyChanged("GLGroupCode");
            }
        }

        [DBColumn(Name = "GLGroupNameShort", Storage = "m_GLGroupNameShort", DbType = "NVarChar(50) NULL")]
        public string GLGroupNameShort
        {
            get { return this.m_GLGroupNameShort; }
            set
            {
                this.m_GLGroupNameShort = value;
                this.NotifyPropertyChanged("GLGroupNameShort");
            }
        }

        [DBColumn(Name = "GLGroupName", Storage = "m_GLGroupName", DbType = "NVarChar(100) NULL")]
        public string GLGroupName
        {
            get { return this.m_GLGroupName; }
            set
            {
                this.m_GLGroupName = value;
                this.NotifyPropertyChanged("GLGroupName");
            }
        }

        [DBColumn(Name = "GLGroupNameB", Storage = "m_GLGroupNameB", DbType = "NVarChar(100) NULL")]
        public string GLGroupNameB
        {
            get { return this.m_GLGroupNameB; }
            set
            {
                this.m_GLGroupNameB = value;
                this.NotifyPropertyChanged("GLGroupNameB");
            }
        }

        [DBColumn(Name = "GLGroupNameAlias", Storage = "m_GLGroupNameAlias", DbType = "NVarChar(100) NULL")]
        public string GLGroupNameAlias
        {
            get { return this.m_GLGroupNameAlias; }
            set
            {
                this.m_GLGroupNameAlias = value;
                this.NotifyPropertyChanged("GLGroupNameAlias");
            }
        }

        [DBColumn(Name = "GLGroupNamePrint", Storage = "m_GLGroupNamePrint", DbType = "NVarChar(100) NULL")]
        public string GLGroupNamePrint
        {
            get { return this.m_GLGroupNamePrint; }
            set
            {
                this.m_GLGroupNamePrint = value;
                this.NotifyPropertyChanged("GLGroupNamePrint");
            }
        }

        [DBColumn(Name = "GLGroupNameSys", Storage = "m_GLGroupNameSys", DbType = "NVarChar(100) NULL")]
        public string GLGroupNameSys
        {
            get { return this.m_GLGroupNameSys; }
            set
            {
                this.m_GLGroupNameSys = value;
                this.NotifyPropertyChanged("GLGroupNameSys");
            }
        }

        [DBColumn(Name = "GLGroupCodeOS", Storage = "m_GLGroupCodeOS", DbType = "NVarChar(50) NULL")]
        public string GLGroupCodeOS
        {
            get { return this.m_GLGroupCodeOS; }
            set
            {
                this.m_GLGroupCodeOS = value;
                this.NotifyPropertyChanged("GLGroupCodeOS");
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

        [DBColumn(Name = "ShowAsLedger", Storage = "m_ShowAsLedger", DbType = "Bit NULL")]
        public bool ShowAsLedger
        {
            get { return this.m_ShowAsLedger; }
            set
            {
                this.m_ShowAsLedger = value;
                this.NotifyPropertyChanged("ShowAsLedger");
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

        [DBColumn(Name = "LastLedgerNo", Storage = "m_LastLedgerNo", DbType = "Int NULL")]
        public int LastLedgerNo
        {
            get { return this.m_LastLedgerNo; }
            set
            {
                this.m_LastLedgerNo = value;
                this.NotifyPropertyChanged("LastLedgerNo");
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

        #endregion //properties
    }
    
    public partial class dcGLGroup
    {

        #region custom properties

        private int m_GLGroupLevelCurrent = 0;
        public int GLGroupLevelCurrent
        {

            get { return m_GLGroupLevelCurrent; }
            set { this.m_GLGroupLevelCurrent = value; }
        }

        private bool m_HasParent = false;
        public bool HasParent
        {

            get { return m_HasParent; }
            set { this.m_HasParent = value; }
        }

        public string GLGroupNameShortName
        {
            get {

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



        private string m_GLGroupNameIndent = string.Empty;
        public string GLGroupNameIndent
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupNameIndent; }
            set { this.m_GLGroupNameIndent = value; }
        }

        private string m_GLClassName = string.Empty;
        public string GLClassName
        {
            get { return m_GLClassName; }
            set { this.m_GLClassName = value; }
        }

        private string m_GLClassCode = string.Empty;
        public string GLClassCode
        {
            get { return m_GLClassCode; }
            set { this.m_GLClassCode = value; }
        }


        private string m_GLGroupClassName = string.Empty;
        public string GLGroupClassName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupClassName; }
            set { this.m_GLGroupClassName = value; }
        }

        public string GLGroupClassNameCode
        {
            get
            {
                return m_GLClassName + " - " + m_GLClassCode;
            }
        }



        private string m_GLGroupNameParent = string.Empty;
        public string GLGroupNameParent
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupNameParent; }
            set { this.m_GLGroupNameParent = value; }
        }

        private string m_GLGroupCodeParent = string.Empty;
        public string GLGroupCodeParent
        {
            get { return m_GLGroupCodeParent; }
            set { this.m_GLGroupCodeParent = value; }
        }


        private string m_GLGroupNameShortParent = string.Empty;
        public string GLGroupNameShortParent
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupNameShortParent; }
            set { this.m_GLGroupNameShortParent = value; }
        }



        public string GLGroupNameCode
        {
            get
            {
                return m_GLGroupName + " - " + m_GLGroupCode ;
            }
        }

        public string GLGroupNameCodeShortName
        {
            get
            {
                return m_GLGroupName + " (" +  m_GLGroupNameShort  +  ") - " + m_GLGroupCode;
            }
        }

        public string GLGroupNameParentEffective
        {
            get {
                return m_GLGroupIDParent == 0 ? GLClassName : m_GLGroupNameParent;
            }
        }

        public string GLGroupNameCodeParent
        {
            get
            {
                return m_GLGroupNameParent + " - " + m_GLGroupCodeParent;
            }
        }

        public string GLGroupNameCodeShortNameParent
        {
            get
            {
                return m_GLGroupNameParent + " (" + m_GLGroupNameShortParent + ") - " + m_GLGroupCodeParent;
            }
        }

        public string GLGroupNameCodeParentEffective
        {
            get
            {
                return m_GLGroupIDParent == 0 ? GLGroupClassNameCode : GLGroupNameCodeParent;
            }
        }

        public string GLGroupNameCodeShortNameParentEffective
        {
            get
            {
                return m_GLGroupIDParent == 0 ? GLGroupClassNameCode : GLGroupNameCodeShortNameParent;
            }
        }

        public string GLGroupParentKey
        {
            get
            {
                string pKey = string.Empty;
                if (m_GLGroupIDParent == 0)
                {
                    pKey = "gclid" + m_GLClassID.ToString();
                }
                else
                {
                    pKey = "grpid" + m_GLGroupIDParent.ToString();
                }
                return pKey;
            }
        }


        private string m_GLGroupNameHierarchy = string.Empty;
        public string GLGroupNameHierarchy
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupNameHierarchy; }
            set { this.m_GLGroupNameHierarchy = value; }
        }

        private string m_GLGroupNameParentHierarchy = string.Empty;
        public string GLGroupNameParentHierarchy
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupNameParentHierarchy; }
            set { this.m_GLGroupNameParentHierarchy = value; }
        }


        private int m_ChildGroupCount = 0;
        public int ChildGroupCount
        {
            get { return m_ChildGroupCount; }
            set { m_ChildGroupCount = value; }
        }

        private int m_ChildAccountCount = 0;
        public int ChildAccountCount
        {
            get { return m_ChildAccountCount; }
            set { m_ChildAccountCount = value; }
        }

        #endregion

        #region Calcluation fields
        private decimal m_AccYearID = 0;
        public decimal AccYearID
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

        private decimal m_OpenAmt = 0;
        public decimal OpenAmt
        {
            get { return m_OpenAmt; }
            set { m_OpenAmt = value; }
        }


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

        private decimal m_OpenBalnceAmt = 0;
        public decimal OpenBalnceAmt
        {
            get { return m_OpenBalnceAmt; }
            set { m_OpenBalnceAmt = value; }
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
