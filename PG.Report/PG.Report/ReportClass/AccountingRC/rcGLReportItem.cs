using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcGLReportItem
    {
        
        //Class,Group,Account,header, Sum/Total,BlankSpace
        private int m_ItemType = 0;
        public int ItemType
        {
            get { return this.m_ItemType; }
            set { this.m_ItemType = value; }
        }

        ////0=item,1=Sum/Total, 2=BlankSpace, 
        //private int m_ItemShowType = 0;
        //public int ItemShowType
        //{
        //    get { return this.m_ItemShowType; }
        //    set { this.m_ItemShowType = value; }
        //}


        private int m_ItemID = 0;
        public int ItemID
        {
            get { return this.m_ItemID; }
            set { this.m_ItemID = value; }
        }

        private string m_ItemCode = string.Empty;
        public string ItemCode
        {
            get { return this.m_ItemCode; }
            set { this.m_ItemCode = value; }
        }


        private string m_ItemName = string.Empty;
        public string ItemName
        {
            get { return this.m_ItemName; }
            set { this.m_ItemName = value; }
        }

        private string m_ItemNameShort = string.Empty;
        public string ItemNameShort
        {
            get { return this.m_ItemNameShort; }
            set { this.m_ItemNameShort = value; }
        }


        private string m_ItemNameIndent = string.Empty;
        public string ItemNameIndent
        {
            get { return this.m_ItemNameIndent; }
            set { this.m_ItemNameIndent = value; }
        }

        private string m_ItemNameDispaly = string.Empty;
        public string ItemNameDispaly
        {
            get { return this.m_ItemNameDispaly; }
            set { this.m_ItemNameDispaly = value; }
        }



        private int m_AccGLClassID = 0;
        public int AccGLClassID
        {
            get { return this.m_AccGLClassID; }
            set { this.m_AccGLClassID = value; }
        }

        private int m_ItemIDParent = 0;
        public int ItemIDParent
        {
            get { return this.m_ItemIDParent; }
            set { this.m_ItemIDParent = value; }
        }

        private string m_ItemNameParent = string.Empty;
        public string ItemNameParent
        {
            get { return this.m_ItemNameParent; }
            set { this.m_ItemNameParent = value; }
        }

        private string m_ItemNameParentEffective = string.Empty;
        public string ItemNameParentEffective
        {
            get { return this.m_ItemNameParentEffective; }
            set { this.m_ItemNameParentEffective = value; }
        }


        private string m_ItemNameFull = string.Empty;
        public string ItemNameFull
        {
            get { return this.m_ItemNameFull; }
            set { this.m_ItemNameFull = value; }
        }

        private int m_ItemSLNo = 0;
        public int ItemSLNo
        {
            get { return this.m_ItemSLNo; }
            set { this.m_ItemSLNo = value; }
        }


        private int m_ItemLevel = 0;
        public int ItemLevel
        {
            get { return this.m_ItemLevel; }
            set { this.m_ItemLevel = value; }
        }


        private int m_ChildItemCount = 0;
        public int ChildItemCount
        {
            get { return this.m_ChildItemCount; }
            set { this.m_ChildItemCount = value; }
        }



        private bool m_IsBoldName = false;
        public bool IsBoldName
        {
            get { return this.m_IsBoldName; }
            set { this.m_IsBoldName = value; }
        }

        private bool m_IsItalicName = false;
        public bool IsItalicName
        {
            get { return this.m_IsItalicName; }
            set { this.m_IsItalicName = value; }
        }

        private bool m_IsUnderlinedName = false;
        public bool IsUnderlinedName
        {
            get { return this.m_IsUnderlinedName; }
            set { this.m_IsUnderlinedName = value; }
        }

   
        private bool m_IsBoldAmt = false;
        public bool IsBoldAmt
        {
            get { return this.m_IsBoldAmt; }
            set { this.m_IsBoldAmt = value; }
        }

        private bool m_IsItalicAmt = false;
        public bool IsItalicAmt
        {
            get { return this.m_IsItalicAmt; }
            set { this.m_IsItalicAmt = value; }
        }

        private bool m_IsUnderlinedAmt = false;
        public bool IsUnderlinedAmt
        {
            get { return this.m_IsUnderlinedAmt; }
            set { this.m_IsUnderlinedAmt = value; }
        }

        private string m_BorderTopAmt = "None";
        public string BorderTopAmt
        {
            get { return this.m_BorderTopAmt; }
            set { this.m_BorderTopAmt = value; }
        }

        private string m_BorderBottomAmt = "None";
        public string BorderBottomAmt
        {
            get { return this.m_BorderBottomAmt; }
            set { this.m_BorderBottomAmt = value; }
        }

        private string m_BorderWidthTopAmt = "1pt";
        public string BorderWidthTopAmt
        {
            get { return this.m_BorderWidthTopAmt; }
            set { this.m_BorderWidthTopAmt = value; }
        }

        private string m_BorderWidthBottomAmt = "1pt";
        public string BorderWidthBottomAmt
        {
            get { return this.m_BorderWidthBottomAmt; }
            set { this.m_BorderWidthBottomAmt = value; }
        }


        private bool m_IsBoldAmtSub1 = false;
        public bool IsBoldAmtSub1
        {
            get { return this.m_IsBoldAmtSub1; }
            set { this.m_IsBoldAmtSub1 = value; }
        }

        private bool m_IsItalicAmtSub1 = false;
        public bool IsItalicAmtSub1
        {
            get { return this.m_IsItalicAmtSub1; }
            set { this.m_IsItalicAmtSub1 = value; }
        }


        private string m_BorderTopAmtSub1 = "None";
        public string BorderTopAmtSub1
        {
            get { return this.m_BorderTopAmtSub1; }
            set { this.m_BorderTopAmtSub1 = value; }
        }

        private string m_BorderBottomAmtSub1 = "None";
        public string BorderBottomAmtSub1
        {
            get { return this.m_BorderBottomAmtSub1; }
            set { this.m_BorderBottomAmtSub1 = value; }
        }

        private string m_BorderWidthTopAmtSub1 = "1pt";
        public string BorderWidthTopAmtSub1
        {
            get { return this.m_BorderWidthTopAmtSub1; }
            set { this.m_BorderWidthTopAmtSub1 = value; }
        }

        private string m_BorderWidthBottomAmtSub1 = "1pt";
        public string BorderWidthBottomAmtSub1
        {
            get { return this.m_BorderWidthBottomAmtSub1; }
            set { this.m_BorderWidthBottomAmtSub1 = value; }
        }

        private string m_NumberFormat = string.Empty;
        public string NumberFormat
        {
            get { return this.m_NumberFormat; }
            set { this.m_NumberFormat = value; }
        }

        private bool m_ItemIsCash = false;
        public bool ItemIsCash
        {
            get { return this.m_ItemIsCash; }
            set { this.m_ItemIsCash = value; }
        }

        private bool m_ItemIsBank = false;
        public bool ItemIsBank
        {
            get { return this.m_ItemIsBank; }
            set { this.m_ItemIsBank = value; }
        }

        private string m_GroupledgerShowType ="";
        public string GroupledgerShowType
        {
            get { return this.m_GroupledgerShowType; }
            set { this.m_GroupledgerShowType = value; }
        }

        #region OpeningYear

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
            get { return this.m_OpenAmtYear; }
            set { this.m_OpenAmtYear = value; }
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



        private decimal m_OpenBalanceAmtYear = 0;
        public decimal OpenBalanceAmtYear 
        {
            get { return this.m_OpenBalanceAmtYear; }
            set { this.m_OpenBalanceAmtYear = value; }
        }

        private decimal m_OpenBalanceDisplayYear = 0;
        public decimal OpenBalanceDisplayYear 
        {
            get { return this.m_OpenBalanceDisplayYear; }
            set { this.m_OpenBalanceDisplayYear = value; }
        }


        private string m_OpenBalanceTextYear = string.Empty;
        public string OpenBalanceTextYear 
        {
            get { return this.m_OpenBalanceTextYear; }
            set { this.m_OpenBalanceTextYear = value; }
        }

        private int m_OpenDrCrYear = 0;
        public int OpenDrCrYear 
        {
            get { return this.m_OpenDrCrYear; }
            set { this.m_OpenDrCrYear = value; }
        }

        private string m_OpenDrCrTextYear = string.Empty;
        public string OpenDrCrTextYear 
        {
            get { return this.m_OpenDrCrTextYear; }
            set { this.m_OpenDrCrTextYear = value; }
        }


        #endregion

        #region OpeningDateRange

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
            get { return this.m_OpenAmtDateRange; }
            set { this.m_OpenAmtDateRange = value; }
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
            get { return this.m_OpenBalanceAmtDateRange; }
            set { this.m_OpenBalanceAmtDateRange = value; }
        }

        private decimal m_OpenBalanceDisplayDateRange = 0;
        public decimal OpenBalanceDisplayDateRange
        {
            get { return this.m_OpenBalanceDisplayDateRange; }
            set { this.m_OpenBalanceDisplayDateRange = value; }
        }


        private string m_OpenBalanceTextDateRange = string.Empty;
        public string OpenBalanceTextDateRange
        {
            get { return this.m_OpenBalanceTextDateRange; }
            set { this.m_OpenBalanceTextDateRange = value; }
        }

        private int m_OpenBalanceDrCrDateRange = 0;
        public int OpenBalanceDrCrDateRange
        {
            get { return this.m_OpenBalanceDrCrDateRange; }
            set { this.m_OpenBalanceDrCrDateRange = value; }
        }

        private string m_OpenBalanceDrCrTextDateRange = string.Empty;
        public string OpenBalanceDrCrTextDateRange
        {
            get { return this.m_OpenBalanceDrCrTextDateRange; }
            set { this.m_OpenBalanceDrCrTextDateRange = value; }
        }


        #endregion
       
        #region Opening

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
            get { return this.m_OpenAmt; }
            set { this.m_OpenAmt = value; }
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
            get { return this.m_OpenBalanceAmt; }
            set { this.m_OpenBalanceAmt = value; }
        }

        private int m_OpenBalanceDrCr = 0;
        public int OpenBalanceDrCr
        {
            get { return this.m_OpenBalanceDrCr; }
            set { this.m_OpenBalanceDrCr = value; }
        }

        private string m_OpenBalanceDrCrText = string.Empty;
        public string OpenBalanceDrCrText
        {
            get { return this.m_OpenBalanceDrCrText; }
            set { this.m_OpenBalanceDrCrText = value; }
        }

        private string m_OpenBalanceText = string.Empty;
        public string OpenBalanceText
        {
            get { return this.m_OpenBalanceText; }
            set { this.m_OpenBalanceText = value; }
        }
        
        private decimal m_OpenBalanceDisplay = 0;
        public decimal OpenBalanceDisplay
        {
            get { return this.m_OpenBalanceDisplay; }
            set { this.m_OpenBalanceDisplay = value; }
        }



        private decimal m_OpenBalancePercent = 0;
        public decimal OpenBalancePercent
        {
            get { return this.m_OpenBalancePercent; }
            set { this.m_OpenBalancePercent = value; }
        }



        #endregion

        #region Transaction
        private decimal m_TranDebitAmt = 0;
        public decimal TranDebitAmt
        {
            get { return this.m_TranDebitAmt; }
            set { this.m_TranDebitAmt = value; }
        }

        private string m_TranDebitAmtText = string.Empty;
        public string TranDebitAmtText
        {
            get { return this.m_TranDebitAmtText; }
            set { this.m_TranDebitAmtText = value; }
        }

        private int m_TranDebitCount = 0;
        public int TranDebitCount
        {
            get { return this.m_TranDebitCount; }
            set { this.m_TranDebitCount = value; }
        }

        private decimal m_TranCreditAmt = 0;
        public decimal TranCreditAmt
        {
            get { return this.m_TranCreditAmt; }
            set { this.m_TranCreditAmt = value; }
        }

        private string m_TranCreditAmtText = string.Empty;
        public string TranCreditAmtText
        {
            get { return this.m_TranCreditAmtText; }
            set { this.m_TranCreditAmtText = value; }
        }
        private int m_TranCreditCount = 0;
        public int TranCreditCount
        {
            get { return this.m_TranCreditCount; }
            set { this.m_TranCreditCount = value; }
        }


        private decimal m_TranAmt = 0;
        public decimal TranAmt
        {
            get { return this.m_TranAmt; }
            set { this.m_TranAmt = value; }
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
            get { return this.m_TranBalanceAmt; }
            set { this.m_TranBalanceAmt = value; }
        }

        private int m_TranBalanceDrCr = 0;
        public int TranBalanceDrCr
        {
            get { return this.m_TranBalanceDrCr; }
            set { this.m_TranBalanceDrCr = value; }
        }

        private string m_TranBalanceDrCrText = string.Empty;
        public string TranBalanceDrCrText
        {
            get { return this.m_TranBalanceDrCrText; }
            set { this.m_TranBalanceDrCrText = value; }
        }

        private decimal m_TranBalanceDisplay = 0;
        public decimal TranBalanceDisplay
        {
            get { return this.m_TranBalanceDisplay; }
            set { this.m_TranBalanceDisplay = value; }
        }


        private decimal m_TranBalancePercent = 0;
        public decimal TranBalancePercent
        {
            get { return this.m_TranBalancePercent; }
            set { this.m_TranBalancePercent = value; }
        }




        #endregion

        #region Closing Sub1
        private decimal m_CloseDebitAmtSub1 = 0;
        public decimal CloseDebitAmtSub1
        {
            get { return this.m_CloseDebitAmtSub1; }
            set { this.m_CloseDebitAmtSub1 = value; }
        }

        private decimal m_CloseCreditAmtSub1 = 0;
        public decimal CloseCreditAmtSub1
        {
            get { return this.m_CloseCreditAmtSub1; }
            set { this.m_CloseCreditAmtSub1 = value; }
        }


        private decimal m_CloseAmtSub1 = 0;
        public decimal CloseAmtSub1
        {
            get { return this.m_CloseAmtSub1; }
            set { this.m_CloseAmtSub1 = value; }
        }

        private decimal m_CloseBalanceAmtSub1 = 0;
        public decimal CloseBalanceAmtSub1
        {
            get { return this.m_CloseBalanceAmtSub1; }
            set { this.m_CloseBalanceAmtSub1 = value; }
        }

        private decimal m_CloseBalanceDisplaySub1 = 0;
        public decimal CloseBalanceDisplaySub1
        {
            get { return this.m_CloseBalanceDisplaySub1; }
            set { this.m_CloseBalanceDisplaySub1 = value; }
        }
       


        private string m_CloseBalanceTextSub1 = string.Empty;
        public string CloseBalanceTextSub1
        {
            get { return this.m_CloseBalanceTextSub1; }
            set { this.m_CloseBalanceTextSub1 = value; }
        }


        private int m_CloseBalanceDrCrSub1 = 0;
        public int CloseBalanceDrCrSub1
        {
            get { return this.m_CloseBalanceDrCrSub1; }
            set { this.m_CloseBalanceDrCrSub1 = value; }
        }

        private string m_CloseBalanceDrCrTextSub1 = string.Empty;
        public string CloseBalanceDrCrTextSub1
        {
            get { return this.m_CloseBalanceDrCrTextSub1; }
            set { this.m_CloseBalanceDrCrTextSub1 = value; }
        }

        private decimal m_CloseBalancePercentSub1 = 0;
        public decimal CloseBalancePercentSub1
        {
            get { return this.m_CloseBalancePercentSub1; }
            set { this.m_CloseBalancePercentSub1 = value; }
        }


        #endregion

        #region Closing
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

        private decimal m_CloseBalanceDisplay = 0;
        public decimal CloseBalanceDisplay
        {
            get { return this.m_CloseBalanceDisplay; }
            set { this.m_CloseBalanceDisplay = value; }
        }

        private string m_CloseBalanceText = string.Empty;
        public string CloseBalanceText
        {
            get { return this.m_CloseBalanceText; }
            set { this.m_CloseBalanceText = value; }
        }


        private int m_CloseBalanceDrCr = 0;
        public int CloseBalanceDrCr
        {
            get { return this.m_CloseBalanceDrCr; }
            set { this.m_CloseBalanceDrCr = value; }
        }

        private string m_CloseBalanceDrCrText = string.Empty;
        public string CloseBalanceDrCrText
        {
            get { return this.m_CloseBalanceDrCrText; }
            set { this.m_CloseBalanceDrCrText = value; }
        }

        private decimal m_CloseBalancePercent = 0;
        public decimal CloseBalancePercent
        {
            get { return this.m_CloseBalancePercent; }
            set { this.m_CloseBalancePercent = value; }
        }



        #endregion

        #region Closing Prev. Year
        private decimal m_CloseAmtPrevYear = 0;
        public decimal CloseAmtPrevYear
        {
            get { return this.m_CloseAmtPrevYear; }
            set { this.m_CloseAmtPrevYear = value; }
        }

        private decimal m_BalanceAmtPrevYear = 0;
        public decimal BalanceAmtPrevYear
        {
            get { return this.m_BalanceAmtPrevYear; }
            set { this.m_BalanceAmtPrevYear = value; }
        }

        private decimal m_BalanceAmtPrevYearDisplay = 0;
        public decimal BalanceAmtPrevYearDisplay
        {
            get { return this.m_BalanceAmtPrevYearDisplay; }
            set { this.m_BalanceAmtPrevYearDisplay = value; }
        }

        private string m_BalanceAmtPrevYearText = string.Empty;
        public string BalanceAmtPrevYearText
        {
            get { return this.m_BalanceAmtPrevYearText; }
            set { this.m_BalanceAmtPrevYearText = value; }
        }

        private int m_BalancePrevYearDrCr = 0;
        public int BalancePrevYearDrCr
        {
            get { return this.m_BalancePrevYearDrCr; }
            set { this.m_BalancePrevYearDrCr = value; }
        }

        private string m_BalancePrevYearDrCrText = string.Empty;
        public string BalancePrevYearDrCrText
        {
            get { return this.m_BalancePrevYearDrCrText; }
            set { this.m_BalancePrevYearDrCrText = value; }
        }


        #endregion


        #region Change
        private decimal m_DebitAmtChange = 0;
        public decimal DebitAmtChange
        {
            get { return this.m_DebitAmtChange; }
            set { this.m_DebitAmtChange = value; }
        }

        private decimal m_CreditAmtChange = 0;
        public decimal CreditAmtChange
        {
            get { return this.m_CreditAmtChange; }
            set { this.m_CreditAmtChange = value; }
        }

        private decimal m_ChangeAmt = 0;
        public decimal ChangeAmt
        {
            get { return this.m_ChangeAmt; }
            set { this.m_ChangeAmt = value; }
        }


        private string m_ChangeAmtText = string.Empty;
        public string ChangeAmtText
        {
            get { return this.m_ChangeAmtText; }
            set { this.m_ChangeAmtText = value; }
        }

        private int m_ChangeDrCr = 0;
        public int ChangeDrCr
        {
            get { return this.m_ChangeDrCr; }
            set { this.m_ChangeDrCr = value; }
        }

        private string m_ChangeDrCrText = string.Empty;
        public string ChangeDrCrText
        {
            get { return this.m_ChangeDrCrText; }
            set { this.m_ChangeDrCrText = value; }
        }

        private decimal m_ChangePercent = 0;
        public decimal ChangePercent
        {
            get { return this.m_ChangePercent; }
            set { this.m_ChangePercent = value; }
        }

        #endregion


        #region Item Amt
        private decimal m_ItemDebitAmt = 0;
        public decimal ItemDebitAmt
        {
            get { return this.m_ItemDebitAmt; }
            set { this.m_ItemDebitAmt = value; }
        }

        private decimal m_ItemCreditAmt = 0;
        public decimal ItemCreditAmt
        {
            get { return this.m_ItemCreditAmt; }
            set { this.m_ItemCreditAmt = value; }
        }

        private decimal m_ItemAmt = 0;
        public decimal ItemAmt
        {
            get { return this.m_ItemAmt; }
            set { this.m_ItemAmt = value; }
        }

        private decimal m_ItemDebitBalanceAmt = 0;
        public decimal ItemDebitBalanceAmt
        {
            get { return this.m_ItemDebitBalanceAmt; }
            set { this.m_ItemDebitBalanceAmt = value; }
        }

        private decimal m_ItemCreditBalanceAmt = 0;
        public decimal ItemCreditBalanceAmt
        {
            get { return this.m_ItemCreditBalanceAmt; }
            set { this.m_ItemCreditBalanceAmt = value; }
        }


        private decimal m_ItemBalanceAmt = 0;
        public decimal ItemBalanceAmt
        {
            get { return this.m_ItemBalanceAmt; }
            set { this.m_ItemBalanceAmt = value; }
        }

        private string m_ItemBalanceText = string.Empty;
        public string ItemBalanceText
        {
            get { return this.m_ItemBalanceText; }
            set { this.m_ItemBalanceText = value; }
        }

        private int m_ItemBalanceDrCr = 0;
        public int ItemBalanceDrCr
        {
            get { return this.m_ItemBalanceDrCr; }
            set { this.m_ItemBalanceDrCr = value; }
        }

        private string m_ItemBalanceDrCrText = string.Empty;
        public string ItemBalanceDrCrText
        {
            get { return this.m_ItemBalanceDrCrText; }
            set { this.m_ItemBalanceDrCrText = value; }
        }


        private decimal m_ItemAmtDisplay = 0;
        public decimal ItemAmtDisplay
        {
            get { return this.m_ItemAmtDisplay; }
            set { this.m_ItemAmtDisplay = value; }
        }

        private string m_ItemAmtDisplayText = string.Empty;
        public string ItemAmtDisplayText
        {
            get { return this.m_ItemAmtDisplayText; }
            set { this.m_ItemAmtDisplayText = value; }
        }


        private decimal m_ItemAmtPercent = 0;
        public decimal ItemAmtPercent
        {
            get { return this.m_ItemAmtPercent; }
            set { this.m_ItemAmtPercent = value; }
        }



        #endregion

        #region Item Amt Sub1
        private decimal m_ItemDebitAmtSub1 = 0;
        public decimal ItemDebitAmtSub1
        {
            get { return this.m_ItemDebitAmtSub1; }
            set { this.m_ItemDebitAmtSub1 = value; }
        }

        private decimal m_ItemCreditAmtSub1 = 0;
        public decimal ItemCreditAmtSub1
        {
            get { return this.m_ItemCreditAmtSub1; }
            set { this.m_ItemCreditAmtSub1 = value; }
        }

        private decimal m_ItemAmtSub1 = 0;
        public decimal ItemAmtSub1
        {
            get { return this.m_ItemAmtSub1; }
            set { this.m_ItemAmtSub1 = value; }
        }

        private decimal m_ItemDebitBalanceAmtSub1 = 0;
        public decimal ItemDebitBalanceAmtSub1
        {
            get { return this.m_ItemDebitBalanceAmtSub1; }
            set { this.m_ItemDebitBalanceAmtSub1 = value; }
        }

        private decimal m_ItemCreditBalanceAmtSub1 = 0;
        public decimal ItemCreditBalanceAmtSub1
        {
            get { return this.m_ItemCreditBalanceAmtSub1; }
            set { this.m_ItemCreditBalanceAmtSub1 = value; }
        }



        private decimal m_ItemBalanceAmtSub1 = 0;
        public decimal ItemBalanceAmtSub1
        {
            get { return this.m_ItemBalanceAmtSub1; }
            set { this.m_ItemBalanceAmtSub1 = value; }
        }

        private string m_ItemBalanceTextSub1 = string.Empty;
        public string ItemBalanceTextSub1
        {
            get { return this.m_ItemBalanceTextSub1; }
            set { this.m_ItemBalanceTextSub1 = value; }
        }


        private int m_ItemBalanceDrCrSub1 = 0;
        public int ItemBalanceDrCrSub1
        {
            get { return this.m_ItemBalanceDrCrSub1; }
            set { this.m_ItemBalanceDrCrSub1 = value; }
        }

        private string m_ItemBalanceDrCrTextSub1 = string.Empty;
        public string ItemBalanceDrCrTextSub1
        {
            get { return this.m_ItemBalanceDrCrTextSub1; }
            set { this.m_ItemBalanceDrCrTextSub1 = value; }
        }


        private decimal m_ItemAmtDisplaySub1 = 0;
        public decimal ItemAmtDisplaySub1
        {
            get { return this.m_ItemAmtDisplaySub1; }
            set { this.m_ItemAmtDisplaySub1 = value; }
        }

        private string m_ItemAmtDisplayTextSub1 = string.Empty;
        public string ItemAmtDisplayTextSub1
        {
            get { return this.m_ItemAmtDisplayTextSub1; }
            set { this.m_ItemAmtDisplayTextSub1 = value; }
        }

        private decimal m_ItemBalancePercentSub1 = 0;
        public decimal ItemBalancePercentSub1
        {
            get { return this.m_ItemBalancePercentSub1; }
            set { this.m_ItemBalancePercentSub1 = value; }
        }



        #endregion

        #region Item Amt Sub2
        private decimal m_ItemDebitAmtSub2 = 0;
        public decimal ItemDebitAmtSub2
        {
            get { return this.m_ItemDebitAmtSub2; }
            set { this.m_ItemDebitAmtSub2 = value; }
        }

        private decimal m_ItemCreditAmtSub2 = 0;
        public decimal ItemCreditAmtSub2
        {
            get { return this.m_ItemCreditAmtSub2; }
            set { this.m_ItemCreditAmtSub2 = value; }
        }

        private decimal m_ItemAmtSub2 = 0;
        public decimal ItemAmtSub2
        {
            get { return this.m_ItemAmtSub2; }
            set { this.m_ItemAmtSub2 = value; }
        }

        private decimal m_ItemDebitBalanceAmtSub2 = 0;
        public decimal ItemDebitBalanceAmtSub2
        {
            get { return this.m_ItemDebitBalanceAmtSub2; }
            set { this.m_ItemDebitBalanceAmtSub2 = value; }
        }

        private decimal m_ItemCreditBalanceAmtSub2 = 0;
        public decimal ItemCreditBalanceAmtSub2
        {
            get { return this.m_ItemCreditBalanceAmtSub2; }
            set { this.m_ItemCreditBalanceAmtSub2 = value; }
        }


        private decimal m_ItemBalanceAmtSub2 = 0;
        public decimal ItemBalanceAmtSub2
        {
            get { return this.m_ItemBalanceAmtSub2; }
            set { this.m_ItemBalanceAmtSub2 = value; }
        }

        private string m_ItemBalanceTextSub2 = string.Empty;
        public string ItemBalanceTextSub2
        {
            get { return this.m_ItemBalanceTextSub2; }
            set { this.m_ItemBalanceTextSub2 = value; }
        }


        private int m_ItemBalanceDrCrSub2 = 0;
        public int ItemBalanceDrCrSub2
        {
            get { return this.m_ItemBalanceDrCrSub2; }
            set { this.m_ItemBalanceDrCrSub2 = value; }
        }

        private string m_ItemBalanceDrCrTextSub2 = string.Empty;
        public string ItemBalanceDrCrTextSub2
        {
            get { return this.m_ItemBalanceDrCrTextSub2; }
            set { this.m_ItemBalanceDrCrTextSub2 = value; }
        }


        private decimal m_ItemAmtDisplaySub2 = 0;
        public decimal ItemAmtDisplaySub2
        {
            get { return this.m_ItemAmtDisplaySub2; }
            set { this.m_ItemAmtDisplaySub2 = value; }
        }

        private string m_ItemAmtDisplayTextSub2 = string.Empty;
        public string ItemAmtDisplayTextSub2
        {
            get { return this.m_ItemAmtDisplayTextSub2; }
            set { this.m_ItemAmtDisplayTextSub2 = value; }
        }

        private decimal m_ItemBalancePercentSub2 = 0;
        public decimal ItemBalancePercentSub2
        {
            get { return this.m_ItemBalancePercentSub2; }
            set { this.m_ItemBalancePercentSub2 = value; }
        }


        #endregion



        public List<rcGLReportItem> GetData()
        {
            return new List<rcGLReportItem>();
        }

        //TODO Change Monir
        private string m_LocationName = string.Empty;
        public string LocationName
        {
            get { return this.m_LocationName; }
            set { this.m_LocationName = value; }
        }
    }
}
