using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PG.Core;
using PG.Core.Utility;
using PG.Core.DBBase;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.Report.ReportEnums;
using PG.Report.ReportClass.AccountingRC;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Report.ReportRBL.AccountingRBL
{
    public class JournalRBL
    {
        public static List<rcJournal> GetJournal(clsPrmLedger prmLedger)
        {
            return GetJournal(prmLedger, null);
        }
        public static List<rcJournal> GetJournal(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournal> cRptList = new List<rcJournal>();

            rcJournal cRpt = new rcJournal();

            dcJournal jrnl = JournalBL.GetJournalByID(prmLedger.CompanyID, prmLedger.JournalID, prmLedger.LocationID, dc);
            //dcJournal jrnl = JournalBL.GetJournalByLocationID(prmLedger.CompanyID, prmLedger.JournalID, prmLedger.LocationID, dc);

            cRpt.JournalID = jrnl.JournalID;
            cRpt.CompanyID = jrnl.CompanyID;

            cRpt.JournalTypeID = jrnl.JournalTypeID;
            cRpt.JournalTypeCode = jrnl.JournalTypeCode;
            cRpt.JournalTypeName = jrnl.JournalTypeName;


            cRpt.JournalNo = jrnl.JournalNo;
            cRpt.JournalDate = jrnl.JournalDate;

            cRpt.AccYearID = jrnl.AccYearID;

            cRpt.JournalTypeID = jrnl.JournalTypeID;
            cRpt.JournalTypeCode = jrnl.JournalTypeCode;
            
            cRpt.JournalTypeName = jrnl.JournalTypeName;
            cRpt.JournalDesc = jrnl.JournalDesc;

            cRpt.JournalAmt = jrnl.JournalAmt;

            cRpt.IsPosted = jrnl.IsPosted;
            cRpt.IsDeleted = jrnl.IsDeleted;

            cRpt.EditUserName = jrnl.EditUserName;
            cRpt.LocationName = jrnl.LocationName;
            cRpt.LocationCode = jrnl.LocationCode;

            List<dcJournalDet> jrnlDetList = JournalDetBL.GetJournalDetList(prmLedger.CompanyID, jrnl.JournalID, dc);

            List<rcJournalTrans> jrnlTransList = new List<rcJournalTrans>();


            int SLNo = 0;

            foreach (dcJournalDet jrnlDet in jrnlDetList)
            {
                rcJournalTrans jrnlTrans = new rcJournalTrans();


                jrnlTrans.JournalDetID = jrnlDet.JournalDetID;
                jrnlTrans.JournalID = jrnlDet.JournalID;
                jrnlTrans.JournalDetSLNo = jrnlDet.JournalDetSLNo;

                jrnlTrans.GLAccountID = jrnlDet.GLAccountID;
                jrnlTrans.GLAccountCode = jrnlDet.GLAccountCode;
                jrnlTrans.GLAccountName = jrnlDet.GLAccountName;

                jrnlTrans.JournalDetDesc = jrnlDet.JournalDetDesc;

                jrnlTrans.DebitAmt = jrnlDet.DebitAmt;
                jrnlTrans.CreditAmt = jrnlDet.CreditAmt;

                jrnlTrans.DrCr = jrnlDet.DrCr;


                jrnlTrans.EditUserName = jrnl.EditUserName;

                SLNo++;
                jrnlTrans.JournalDetSLNo = SLNo;

                jrnlTransList.Add(jrnlTrans);

            }

            cRpt.JournalTrans = jrnlTransList;
            cRptList.Add(cRpt);

            return cRptList;
        }


        public static List<rcJournal> GetJournalSingle(clsPrmLedger prmLedger)
        {
            return GetJournalSingle(prmLedger, null);
        }
        public static List<rcJournal> GetJournalSingle(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournal> cRptList = new List<rcJournal>();

            rcJournal cRpt = new rcJournal();

            dcJournal jrnl = JournalBL.GetJournalByID(prmLedger.CompanyID, prmLedger.JournalID,prmLedger.LocationID, dc);
            //dcJournal jrnl = JournalBL.GetJournalByLocationID(prmLedger.CompanyID, prmLedger.JournalID, prmLedger.LocationID, dc);
            cRpt.JournalID = jrnl.JournalID;
            cRpt.CompanyID = jrnl.CompanyID;

            cRpt.JournalTypeID = jrnl.JournalTypeID;
            cRpt.JournalTypeCode = jrnl.JournalTypeCode;
            cRpt.JournalTypeName = jrnl.JournalTypeName;


            cRpt.JournalNo = jrnl.JournalNo;
            cRpt.JournalDate = jrnl.JournalDate;

            cRpt.AccYearID = jrnl.AccYearID;

            cRpt.JournalTypeID = jrnl.JournalTypeID;
            cRpt.JournalTypeCode = jrnl.JournalTypeCode;

            cRpt.JournalTypeName = jrnl.JournalTypeName;
            cRpt.JournalDesc = jrnl.JournalDesc;

            cRpt.JournalAmt = jrnl.JournalAmt;

            cRpt.IsPosted = jrnl.IsPosted;
            cRpt.IsDeleted = jrnl.IsDeleted;

            cRpt.EditUserName = jrnl.EditUserName;
            cRpt.LocationName = jrnl.LocationName;
            cRpt.LocationCode = jrnl.LocationCode;

            List<dcJournalDet> jrnlDetList = JournalDetBL.GetJournalDetList(prmLedger.CompanyID, jrnl.JournalID, dc).OrderBy(c => c.JournalDetSLNo).ToList();


            List<dcJournalDetIns> jrnlDetInsList = null;
            if (prmLedger.IncludeInstrument)
            {
                jrnlDetInsList = JournalDetInsBL.GetJournalDetInsList(prmLedger.CompanyID, prmLedger.JournalID, dc);
            }

            //Cost Center
            List<dcJournalDetRef> jrnlDetRefList = null;
            if (prmLedger.IncludeCostCenter)
            {
                jrnlDetRefList = JournalDetRefBL.GetJournalDetRefList(prmLedger.CompanyID, prmLedger.JournalID, (int)AccRefTypeEnum.CostCenter,  dc);
            }

            List<rcJournalTrans> jrnlTransList = new List<rcJournalTrans>();

            decimal totDebitAmt = 0;
            decimal totCreditAmt = 0;


            dcJournalDet jrnlDetDebit = null;
            dcJournalDet jrnlDetCredit = null;

            foreach (dcJournalDet jrnlDet in jrnlDetList)
            {
                totDebitAmt += jrnlDet.DebitAmt;
                totCreditAmt += jrnlDet.CreditAmt;

                if (jrnlDet.DrCr == (int)DebitCreditEnum.Debit)
                {
                    if (jrnlDetDebit == null)
                    {
                        jrnlDetDebit = jrnlDet;
                    }
                }
                else
                {
                    if (jrnlDetCredit == null)
                    {
                        jrnlDetCredit = jrnlDet;
                    }
                }
            }

            ///debit
            rcJournalTrans jrnlTransDb = new rcJournalTrans();
            jrnlTransDb.JournalDetID = jrnlDetDebit.JournalDetID;
            jrnlTransDb.JournalID = jrnlDetDebit.JournalID;
            
            jrnlTransDb.GLAccountID = jrnlDetDebit.GLAccountID;
            jrnlTransDb.GLAccountCode = jrnlDetDebit.GLAccountCode;
            jrnlTransDb.GLAccountName = jrnlDetDebit.GLAccountName;
            jrnlTransDb.GLGroupCode = jrnlDetDebit.GLGroupCode;
            jrnlTransDb.GLGroupName = jrnlDetDebit.GLGroupName;
            jrnlTransDb.GLGroupNameShort = jrnlDetDebit.GLGroupNameShort;

            jrnlTransDb.JournalNo = jrnlDetDebit.JournalNo;
            jrnlTransDb.JournalDate = jrnlDetDebit.JournalDate;
            jrnlTransDb.JournalDetDesc = jrnlDetDebit.JournalDetDesc;

            jrnlTransDb.DebitAmt = totDebitAmt;
            jrnlTransDb.CreditAmt = 0;

            //jrnlTransDb.DebitAmtInWord = NumberInWord.GetInWord(totDebitAmt.ToString());
            jrnlTransDb.DebitAmtInWord = NumberInWord.GetInWord(totDebitAmt.ToString("#0")) + " Taka Only.";


            jrnlTransDb.DrCr = jrnlDetDebit.DrCr;

            jrnlTransDb.JournalDetDesc = jrnlDetDebit.JournalDetDesc;

            jrnlTransDb.JournalDetSLNo = 1;

            jrnlTransDb.GLAccountIDContra = jrnlDetCredit.GLAccountID;
            jrnlTransDb.GLAccountCodeContra = jrnlDetCredit.GLAccountCode;
            jrnlTransDb.GLAccountNameContra = jrnlDetCredit.GLAccountName;

            jrnlTransDb.GLGroupIDContra = jrnlDetCredit.GLGroupID;
            jrnlTransDb.GLGroupCodeContra = jrnlDetCredit.GLGroupCode;
            jrnlTransDb.GLGroupNameContra = jrnlDetCredit.GLGroupName;

            jrnlTransDb.GLGroupNameShortContra = jrnlDetCredit.GLGroupNameShort;


            jrnlTransDb.EditUserName = jrnl.EditUserName;

            jrnlTransDb.JournalDetInsText = CreateInsText(jrnlTransDb.JournalDetID, jrnlDetInsList);
            jrnlTransDb.JournalDetCostCenterText = CreateCostCenterText(jrnlTransDb.JournalDetID, jrnlDetRefList);

            ////////////////////


            ///Credit
            rcJournalTrans jrnlTransCr = new rcJournalTrans();
            jrnlTransCr.JournalDetID = jrnlDetCredit.JournalDetID;
            jrnlTransCr.JournalID = jrnlDetCredit.JournalID;

            jrnlTransCr.GLAccountID = jrnlDetCredit.GLAccountID;
            jrnlTransCr.GLAccountCode = jrnlDetCredit.GLAccountCode;
            jrnlTransCr.GLAccountName = jrnlDetCredit.GLAccountName;
            jrnlTransCr.GLGroupCode = jrnlDetCredit.GLGroupCode;
            jrnlTransCr.GLGroupName = jrnlDetCredit.GLGroupName;
            jrnlTransCr.GLGroupNameShort = jrnlDetCredit.GLGroupNameShort;

            jrnlTransCr.JournalNo = jrnlDetCredit.JournalNo;
            jrnlTransCr.JournalDate = jrnlDetCredit.JournalDate;
            jrnlTransCr.JournalDetDesc = jrnlDetCredit.JournalDetDesc;

            jrnlTransCr.DebitAmt = 0;
            jrnlTransCr.CreditAmt = totCreditAmt;
            jrnlTransCr.CreditAmtInWord = NumberInWord.GetInWord(totCreditAmt.ToString("#0")) + " Taka Only." ;

            jrnlTransCr.DrCr = jrnlDetCredit.DrCr;
            jrnlTransCr.JournalDetDesc = jrnlDetCredit.JournalDetDesc;

            jrnlTransCr.JournalDetSLNo = 1;

            jrnlTransCr.GLAccountIDContra = jrnlDetDebit.GLAccountID;
            jrnlTransCr.GLAccountCodeContra = jrnlDetDebit.GLAccountCode;
            jrnlTransCr.GLAccountNameContra = jrnlDetDebit.GLAccountName;

            jrnlTransCr.GLGroupIDContra = jrnlDetDebit.GLGroupID;
            jrnlTransCr.GLGroupCodeContra = jrnlDetDebit.GLGroupCode;
            jrnlTransCr.GLGroupNameContra = jrnlDetDebit.GLGroupName;

            jrnlTransCr.GLGroupNameShortContra = jrnlDetDebit.GLGroupNameShort;

            jrnlTransCr.EditUserName = jrnl.EditUserName;

            jrnlTransCr.JournalDetInsText = CreateInsText(jrnlTransCr.JournalDetID, jrnlDetInsList);

            jrnlTransCr.JournalDetCostCenterText = CreateCostCenterText(jrnlTransCr.JournalDetID, jrnlDetRefList);



            jrnlTransCr.JournalDetInsTextContra = jrnlTransDb.JournalDetInsText;
            jrnlTransDb.JournalDetInsTextContra = jrnlTransCr.JournalDetInsText;



            jrnlTransList.Add(jrnlTransDb);
            jrnlTransList.Add(jrnlTransCr);



             ////////////////////


            cRpt.JournalTrans = jrnlTransList;
            cRptList.Add(cRpt);

            return cRptList;
        }


        public static string CreateInsText(int jrnlDetID, List<dcJournalDetIns> jrnlDetInsList)
        {
            StringBuilder sb = new StringBuilder();

            if (jrnlDetID > 0 && jrnlDetInsList != null)
            {
                List<dcJournalDetIns> insList = jrnlDetInsList.Where(c => c.JournalDetID == jrnlDetID).ToList();

                foreach (dcJournalDetIns detIns in insList)
                {
                    if (sb.Length > 0)
                    {
                        sb.AppendLine();
                    }
                    sb.Append("No: " + detIns.InstrumentNo);
                    sb.Append(", Dt: " + detIns.InstrumentDate.Value.ToString("dd-MMM-yy"));
                    sb.Append(", Bank:  " + detIns.BankName + ", " + detIns.BranchName);
                    sb.Append(",");
                }


            }

            return sb.ToString();
        }


        public static string CreateCostCenterText(int jrnlDetID, List<dcJournalDetRef> jrnlDetRefList)
        {
            StringBuilder sb = new StringBuilder();

            if (jrnlDetID > 0 && jrnlDetRefList != null)
            {
                List<dcJournalDetRef> refList = jrnlDetRefList.Where(c => c.JournalDetID == jrnlDetID).ToList();

                foreach (dcJournalDetRef detRef in refList)
                {
                    if (sb.Length > 0)
                    {
                        sb.AppendLine();
                    }
                    sb.Append(detRef.AccRefCode );
                    sb.Append(" , " + detRef.AccRefCategoryCode );
                    sb.Append(",");
                }


            }

            return sb.ToString();
        }



        public static List<rcJournalTrans> GetJournalTrans(clsPrmLedger prmLedger)
        {
            return GetJournalTrans(prmLedger, null);
        }
        public static List<rcJournalTrans> GetJournalTrans(clsPrmLedger prmLedger, DBContext dc)
        {
            //
            List<rcJournalTrans> cObjList = new List<rcJournalTrans>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDet.* ");

            sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
            sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
            sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
            sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

            sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
            sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
            sb.Append(" , tblGLAccount.BalanceType ");
            sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent ");
            sb.Append(" , tblGLAccount.GLGroupID, tblGLGroup.GLGroupIDParent ,TBLLOCATION.LOCATIONNAME ");

            sb.Append(" FROM  tblJournalDet ");
            sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
            sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");
            //sb.Append(" LEFT JOIN tblLocationGLAccount ON TBLJOURNAL.LOCATIONID=tblLocationGLAccount.LOCATIONID ");
            sb.Append("WHERE (1=1) ");

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }


            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                //cmd.Parameters.AddWithValue("@journalTypeID", prmLedger.JournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }

            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            sb.Append(" ORDER BY tblJournal.JournalDate ");
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;



            cObjList = DBQuery.ExecuteDBQuery<rcJournalTrans>(dbq, dc);

            return cObjList;
        }


        public static List<rcJournalTrans> GetJournalTransControl(clsPrmLedger prmLedger, DBContext dc)
        {
            //
            List<rcJournalTrans> cObjList = new List<rcJournalTrans>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDet.* ");

            sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
            sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
            sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
            sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

            sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
            sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
            sb.Append(" , tblGLAccount.BalanceType ");
            sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent ");
            sb.Append(" , tblGLAccount.GLGroupID, tblGLGroup.GLGroupIDParent,TBLLOCATION.LOCATIONNAME ");

            sb.Append(" FROM  tblJournalDet ");
            sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
            sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");

            sb.Append("WHERE (1=1) ");

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }

            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                //cmd.Parameters.AddWithValue("@journalTypeID", prmLedger.JournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }

            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            sb.Append(" ORDER BY tblJournal.JournalDate ");
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;


            cObjList = DBQuery.ExecuteDBQuery<rcJournalTrans>(dbq, dc);

            return cObjList;
        }

   

        public static List<rcJournalTrans> GetJournalBook(clsPrmLedger prmLedger)
        {
            return GetJournalBook(prmLedger, null);
        }
        public static List<rcJournalTrans> GetJournalBook(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournalTrans> cRptList = new List<rcJournalTrans>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDet.* ");

            sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
            sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
            sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
            sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

            sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
            sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
            sb.Append(" , tblGLAccount.BalanceType,TBLLOCATION.LOCATIONNAME ");


            sb.Append(" FROM  tblJournalDet ");
            sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
            sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");

            sb.Append("WHERE (1=1) ");

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }


            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                //cmd.Parameters.AddWithValue("@journalTypeID", prmLedger.JournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }

            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            sb.Append(" ORDER BY tblJournal.JournalDate ");
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            cRptList = DBQuery.ExecuteDBQuery<rcJournalTrans>(dbq, dc);


            return cRptList;
        }


        public static List<rcJournalTrans> GetJournalBookFull(clsPrmLedger prmLedger)
        {
            return GetJournalBookFull(prmLedger, null);
        }
        public static List<rcJournalTrans> GetJournalBookFull(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournalTrans> cRptList = new List<rcJournalTrans>();


           // List<rcJournalTrans> cTransList = GetJournalTrans(prmLedger, dc);

            //List<dcJournalDetIns> cInsList = GetJournalInsDet(prmLedger, dc);

            List<dcJournalDet> cDetList = new List<dcJournalDet>();
            List<dcJournalDet> cDetListControl = new List<dcJournalDet>();
            List<dcJournalDet> cDetListSub = new List<dcJournalDet>();

  

            if (prmLedger.ControlAccountSummary)
            {
                cDetList = JournalDetBL.GetJournalDetByDate_Normal(prmLedger, dc);
                cDetListControl = JournalDetBL.GetJournalDetByDate_ControlSum(prmLedger, dc);

                if (prmLedger.IncludeSubAccountForControl)
                {
                    cDetListSub = JournalDetBL.GetJournalDetByDate_Sub(prmLedger, dc);
                    
                }
            }
            else
            {
                cDetList = JournalDetBL.GetJournalDetByDate(prmLedger, dc);
            }


            List<dcJournalDetIns> cInsList = new List<dcJournalDetIns>();

            if (prmLedger.IncludeInstrument)
            {
                cInsList = JournalDetBL.GetJournalInsDet(prmLedger, dc);
            }
            List<dcJournalDetRef> cRefList = JournalDetBL.GetJournalRefDet(prmLedger, dc);



            foreach (dcJournalDet cDet in cDetList)
            {
                
                AddTranItem(cDet, cInsList, cRefList, cDetListSub, prmLedger, cRptList);
            }



            int detSLNo = 0;
            int detGroupID = 0;
            foreach (dcJournalDet cDet in cDetListControl)
            {
                detSLNo = cDetList.Where(c => c.JournalID == cDet.JournalID).Max(c => c.JournalDetSLNo);
                cDet.JournalDetSLNo = detSLNo + 1;

                detGroupID--;
                cDet.JournalDetID = detGroupID;

                AddTranItem(cDet, cInsList, cRefList, cDetListSub, prmLedger, cRptList);
            }

            return cRptList;
        }

        public static List<rcJournalTrans> GetCashJournalBookFull(clsPrmLedger prmLedger)
        {
            return GetCashJournalBookFull(prmLedger, null);
        }
        public static List<rcJournalTrans> GetCashJournalBookFull(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournalTrans> cRptList = new List<rcJournalTrans>();


            // List<rcJournalTrans> cTransList = GetJournalTrans(prmLedger, dc);

            //List<dcJournalDetIns> cInsList = GetJournalInsDet(prmLedger, dc);

            List<dcJournalDet> cDetList = new List<dcJournalDet>();
            List<dcJournalDet> cDetListControl = new List<dcJournalDet>();
            List<dcJournalDet> cDetListSub = new List<dcJournalDet>();



            if (prmLedger.ControlAccountSummary)
            {
                cDetList = JournalDetBL.GetCashJournalDetByDate_Normal(prmLedger, dc);
                cDetListControl = JournalDetBL.GetCashJournalDetByDate_ControlSum(prmLedger, dc);

                if (prmLedger.IncludeSubAccountForControl)
                {
                    cDetListSub = JournalDetBL.GetCashJournalDetByDate_Sub(prmLedger, dc);

                }
            }
            else
            {
                cDetList = JournalDetBL.GetCashJournalDetByDate(prmLedger, dc);
            }


            List<dcJournalDetIns> cInsList = new List<dcJournalDetIns>();

            if (prmLedger.IncludeInstrument)
            {
                cInsList = JournalDetBL.GetCashJournalInsDet(prmLedger, dc);
            }
            List<dcJournalDetRef> cRefList = JournalDetBL.GetCashJournalRefDet(prmLedger, dc);



            foreach (dcJournalDet cDet in cDetList)
            {

                AddTranItem(cDet, cInsList, cRefList, cDetListSub, prmLedger, cRptList);
            }



            int detSLNo = 0;
            int detGroupID = 0;
            foreach (dcJournalDet cDet in cDetListControl)
            {
                detSLNo = cDetList.Where(c => c.JournalID == cDet.JournalID).Max(c => c.JournalDetSLNo);
                cDet.JournalDetSLNo = detSLNo + 1;

                detGroupID--;
                cDet.JournalDetID = detGroupID;

                AddTranItem(cDet, cInsList, cRefList, cDetListSub, prmLedger, cRptList);
            }

            return cRptList;
        }

        public static void AddTranItem(dcJournalDet jrnlDet,  List<dcJournalDetIns> cInsList, List<dcJournalDetRef> cRefList, List<dcJournalDet> cDetListSub
                                       , clsPrmLedger prmLedger, List<rcJournalTrans> cRptList)
        {
            bool tranAdd = true;

            rcJournalTrans cTran = new rcJournalTrans();
            Helper.CopyPropertyValueByName(jrnlDet, cTran);
            cTran.DetGroupID = jrnlDet.JournalDetID;

            //
            if (jrnlDet.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount)
            {
                dcJournalDetRef cTranCode = cRefList.FirstOrDefault(c => c.JournalDetID == cTran.JournalDetID
                                        && c.AccRefTypeID == (int)AccRefTypeEnum.TranCode);
                if (cTranCode != null)
                {
                    cTran.AccRefCodeTran = cTranCode.AccRefCode;
                }
            }

            if (prmLedger.ControlAccountSummary && prmLedger.IncludeSubAccountForControl)
            {
                List<dcJournalDet> cSubListDet = cDetListSub.Where(c => c.JournalID == cTran.JournalID
                                                                    && c.GLAccountIDParent == cTran.GLAccountID ).ToList();

                foreach (dcJournalDet cSub in cSubListDet)
                {
                    rcJournalTrans nTran = new rcJournalTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);

                    nTran.ReportTranType = GLReportItemTranTypeEnum.SubAcc;
                    nTran.DetTypeName = "Sub Ledger";
                    nTran.DetDesc = cSub.GLAccountCode + " " + cSub.GLAccountName;
                    nTran.DetAmt = cSub.DebitAmt + cSub.CreditAmt;
                    nTran.DetAmtDrCr = cSub.DebitAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = cSub.DebitAmt - cSub.CreditAmt >= 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                    }

                    cRptList.Add(nTran);

                    tranAdd = false;
                }
            }

            if (jrnlDet.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            {
                List<dcJournalDetRef> cRefListTranCode  = cRefList.Where(c => c.JournalID == cTran.JournalID
                                                                                && c.GLAccountIDParent == cTran.GLAccountID
                                                                                && c.AccRefTypeID == (int)AccRefTypeEnum.TranCode).ToList();

                foreach (dcJournalDetRef cTranCode in cRefListTranCode)
                {
                    rcJournalTrans nTran = new rcJournalTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.ReportTranType = GLReportItemTranTypeEnum.CostCenter;
                    nTran.DetTypeName = "Tran Code";
                    nTran.DetDesc = cTranCode.AccRefCode + " " + cTranCode.AccRefCategoryCode;
                    nTran.DetAmt = cTranCode.DebitAmt + cTranCode.CreditAmt;
                    nTran.DetAmtDrCr = cTranCode.DebitAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = cTranCode.DebitAmt - cTranCode.CreditAmt >= 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                    }

                    cRptList.Add(nTran);

                    tranAdd = false;
                }

            }

            if (prmLedger.IncludeInstrument)
            {
                
                //List<dcJournalDetIns> cInsListDet = cInsList.Where(c => c.JournalDetID == cTran.JournalDetID).ToList();
                List<dcJournalDetIns> cInsListDet = new List<dcJournalDetIns>();

                if (jrnlDet.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount )
                {
                    //cInsListDet = cInsList.Where(c => c.JournalID == cTran.JournalID && c.GLAccountID).ToList();
                }
                else
                {
                    cInsListDet = cInsList.Where(c => c.JournalDetID == cTran.JournalDetID).ToList();
                }
                
                foreach (dcJournalDetIns cIns in cInsListDet)
                {
                    rcJournalTrans nTran = new rcJournalTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.ReportTranType = GLReportItemTranTypeEnum.Instrument;
                    nTran.DetTypeName = "Insturment";
                    nTran.DetDesc = cIns.InstrumentTypeName + " " + cIns.InstrumentNo + " " + cIns.InstrumentDate.Value.ToString("dd-MMM-yyyy");
                    nTran.DetAmt = cIns.InsTranAmt;

                    //nTran.DetAmtDrCrText = cIns.InstrumentModeID == (int)InstrumentModeEnum.Issue ? "Is" : "Rc";

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                    }

                    cRptList.Add(nTran);

                    tranAdd = false;
                }
            } //IncludeInstrument


            if (prmLedger.IncludeCostCenter)
            {
                //List<dcJournalDetRef> cRefListCostcenter = cRefList.Where(c => c.JournalDetID == cTran.JournalDetID
                //                                                && c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter).ToList();

                List<dcJournalDetRef> cRefListCostcenter = new List<dcJournalDetRef>();

                if (jrnlDet.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    //cInsListDet = cInsList.Where(c => c.JournalID == cTran.JournalID && c.GLAccountID).ToList();

                    cRefListCostcenter = cRefListCostcenter = cRefList.Where(c => c.JournalID == cTran.JournalID
                                                                && c.GLAccountIDParent == cTran.GLAccountID
                                                                && c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter).ToList();
                }
                else
                {
                    cRefListCostcenter = cRefListCostcenter = cRefList.Where(c => c.JournalDetID == cTran.JournalDetID
                                                                && c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter).ToList();
                }



                foreach (dcJournalDetRef cCostcenter in cRefListCostcenter)
                {
                    rcJournalTrans nTran = new rcJournalTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.ReportTranType = GLReportItemTranTypeEnum.CostCenter;
                    nTran.DetTypeName = "Cost Center";
                    nTran.DetDesc = cCostcenter.AccRefCode + " " + cCostcenter.AccRefCategoryCode;
                    nTran.DetAmt = cCostcenter.DebitAmt + cCostcenter.CreditAmt;
                    nTran.DetAmtDrCr = cCostcenter.DebitAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = cCostcenter.DebitAmt - cCostcenter.CreditAmt >= 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                    }

                    cRptList.Add(nTran);

                    tranAdd = false;
                }
            }

            if (prmLedger.IncludeReference)
            {
                List<dcJournalDetRef> cRefListRef = cRefList.Where(c => c.JournalDetID == cTran.JournalDetID
                                                                && c.AccRefTypeID == (int)AccRefTypeEnum.Reference).ToList();
                foreach (dcJournalDetRef cRef in cRefListRef)
                {
                    rcJournalTrans nTran = new rcJournalTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.ReportTranType = GLReportItemTranTypeEnum.Reference;
                    nTran.DetTypeName = "Reference";
                    nTran.DetDesc = cRef.AccRefCode + " " + cRef.AccRefCategoryCode;
                    nTran.DetAmt = cRef.DebitAmt + cRef.CreditAmt;

                    nTran.DetAmtDrCr = cRef.DebitAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = cRef.DebitAmt - cRef.CreditAmt >= 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                    }
                    cRptList.Add(nTran);
                    tranAdd = false;
                }
            }

            if (tranAdd)
            {
                cTran.ReportTranType = GLReportItemTranTypeEnum.Tran;
                cTran.DetCount = 0;
                cRptList.Add(cTran);
            }

        }

        public static List<rcJournal> GetJournalList(clsPrmLedger prmLedger)
        {
            return GetJournalList(prmLedger, null);
        }
        public static List<rcJournal> GetJournalList(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournal> cRptList = new List<rcJournal>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder(JournalBL.GetJournalListString());

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }


            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }


            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            //if (prmLedger.LocationID > 0)
            //{
            //    sb.Append(" AND tblJournal.LocationID=@LocationID ");
            //    cmdInfo.DBParametersInfo.Add("@LocationID", prmLedger.LocationID);
            //}
            if (prmLedger.IsLocation)
            {
                string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
            }
            //cmdInfo.DBParametersInfo.Add("@LocationID", prmLedger.LocationID);


            sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            cRptList = DBQuery.ExecuteDBQuery<rcJournal>(dbq, dc);


            return cRptList;
        }

        public static List<rcJournal> GetCashJournalList(clsPrmLedger prmLedger)
        {
            return GetCashJournalList(prmLedger, null);
        }
        public static List<rcJournal> GetCashJournalList(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournal> cRptList = new List<rcJournal>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            //StringBuilder sb = new StringBuilder(JournalBL.GetJournalListString());
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblJournal.* ");
            sb.Append(",tblJournalType.JournalTypeCode , tblJournalType.JournalTypeName ");
            sb.Append(",tblAccYear.AccYearName ");
       
            sb.Append(" FROM tblJournal ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
            sb.Append(" INNER JOIN tblAccYear ON tblJournal.AccYearID = tblAccYear.AccYearID ");
            sb.Append(" INNER JOIN tblJournalDet On tblJournal.JournalID=tblJournalDet.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID=tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID=tblGLGroup.GLGroupID ");
           

            sb.Append(" WHERE (1=1) ");

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }


            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                //cmd.Parameters.AddWithValue("@journalTypeID", prmLedger.JournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }


            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }
            sb.Append(" AND (tblGLGroup.IsCash= @isCash  OR tblGLGroup.IsBank= @isBank) ");
            cmdInfo.DBParametersInfo.Add("@isCash", prmLedger.IsCash);
            cmdInfo.DBParametersInfo.Add("@isBank", prmLedger.IsBank);

            sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            cRptList = DBQuery.ExecuteDBQuery<rcJournal>(dbq, dc);


            return cRptList;
        }

        public static List<rcJournalTrans> GetCashJournalBook(clsPrmLedger prmLedger)
        {
            return GetCashJournalBook(prmLedger, null);
        }
        public static List<rcJournalTrans> GetCashJournalBook(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournalTrans> cRptList = new List<rcJournalTrans>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDet.* ");

            sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
            sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
            sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
            sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

            sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
            sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
            sb.Append(" , tblGLAccount.BalanceType ");


            sb.Append(" FROM  tblJournalDet ");
            sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID=tblGLGroup.GLGroupID ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

            sb.Append("WHERE (1=1) ");

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }


            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                //cmd.Parameters.AddWithValue("@journalTypeID", prmLedger.JournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }

            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            sb.Append(" AND (tblGLGroup.IsCash= @isCash  OR tblGLGroup.IsBank= @isBank) ");
            cmdInfo.DBParametersInfo.Add("@isCash", prmLedger.IsCash);
            cmdInfo.DBParametersInfo.Add("@isBank", prmLedger.IsBank);

            sb.Append(" ORDER BY tblJournal.JournalDate ");
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            cRptList = DBQuery.ExecuteDBQuery<rcJournalTrans>(dbq, dc);


            return cRptList;
        }
    }
}
