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
    public class LedgerRBL
    {
        public static List<rcLedger> GetLedger(clsPrmLedger prmLedger)
        {
            return GetLedger(prmLedger, null);
        }
        public static List<rcLedger> GetLedger(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcLedger> cRptList = new List<rcLedger>();

            rcLedger cRpt = new rcLedger();

            dcAccYear accYear = AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);

            dcGLAccount acc = GLAccountBL.GetGLAccountByID(prmLedger.CompanyID, prmLedger.GLAccountID,dc);
            dcGLGroup group = GLGroupBL.GetGLGroupByID(prmLedger.CompanyID,acc.GLGroupID, dc);

            cRpt.GLAccountID = acc.GLAccountID;
            cRpt.GLAccountCode = acc.GLAccountCode;
            cRpt.GLAccountName = acc.GLAccountName;
            cRpt.GLGroupName = acc.GLGroupName;
            cRpt.GLClassName = acc.GLClassName;

            cRpt.GLAccountNameFull = acc.GLAccountCodeName;
           
            List<dcJournalDet> transListDate = new List<dcJournalDet>();
 

            decimal openDebitAmtYear = 0;
            decimal openCreditAmtYear = 0;
            decimal openDebitAmtDateRange = 0;
            decimal openCreditAmtDateRange = 0;


            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeYear)
            {
                dcGLAccountHistory accHist = null;
                //accHist = GLAccountHistoryBL.GetGLAccountHistoryByID(prmLedger.GLAccountID, prmLedger.AccYearID, dc);
                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    accHist = GLAccountHistoryBL.GetGLAccountHistoryByID_Control(prmLedger.CompanyID,prmLedger.AccYearID,prmLedger.GLAccountID,  dc);
                }
                else
                {
                    accHist = GLAccountHistoryBL.GetGLAccountHistoryByID(prmLedger.CompanyID, prmLedger.AccYearID, prmLedger.GLAccountID, dc);
                }

                if (accHist != null)
                {
                    openDebitAmtYear = accHist.DebitAmtOpen;
                    openCreditAmtYear = accHist.CreditAmtOpen;
                }
            }

            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                    || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                    || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeDateRange)
            {
                List<dcJournalDet> transListBfDate = new List<dcJournalDet>();

                clsPrmLedger prmLdgBfDate = (clsPrmLedger)prmLedger.Clone();
                prmLdgBfDate.IsBeforeDate = true;

                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    //transListDate = JournalDetBL.GetJournalDetByDate_ControlSum(prmLedger, dc);
                    transListBfDate = JournalDetBL.GetJournalDetSumByDate_Control(prmLdgBfDate, null, dc);
                }
                else
                {
                    //transListDate = JournalDetBL.GetJournalDetByDate(prmLedger, dc);
                    if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.NormalAccount)
                    {
                        transListBfDate = JournalDetBL.GetJournalDetSumByDate_Normal(prmLdgBfDate, null, dc);
                    }
                    else
                    {
                        transListBfDate = JournalDetBL.GetJournalDetSumByDate_Sub(prmLdgBfDate, null, dc);
                    }
                }

                if (transListBfDate.Count > 0)
                {
                    dcJournalDet tranBfDate = transListBfDate.FirstOrDefault(c => c.GLAccountID == prmLedger.GLAccountID);

                    if (tranBfDate != null)
                    {
                        openDebitAmtDateRange = tranBfDate.DebitAmt;
                        openCreditAmtDateRange = tranBfDate.CreditAmt;
                    }
                }

            }


            if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            {
                transListDate = JournalDetBL.GetJournalDetByDate_ControlSum(prmLedger, dc);
            }
            else
            {
                transListDate = JournalDetBL.GetJournalDetByDate(prmLedger, dc);
            }





            decimal openBalanceAmtYear = openDebitAmtYear - openCreditAmtYear;
            decimal openBalanceAmtDateRange = openDebitAmtDateRange - openCreditAmtDateRange;

            decimal openDebitAmt = openDebitAmtYear + openDebitAmtDateRange;
            decimal openCreditAmt = openCreditAmtYear + openCreditAmtDateRange;

            decimal openBalanceAmt = openDebitAmt - openCreditAmt;

            int slNo = 0;

            //transactions
            List<rcLedgerTrans> transList = new List<rcLedgerTrans>();

            decimal curBalance = 0;
            decimal totDebitAmt = 0;
            decimal totCreditAmt = 0;

            decimal debitCreditBal = 0;


            int curDrCr = 0;
            string curDrCrString = "Dr";

            switch (prmLedger.IncludeOpBalanceType)
            {
                case InculdeOpBalanceEnum.None:
                    curBalance = openBalanceAmt;
                    break;

                case InculdeOpBalanceEnum.IncludeALL:
                    slNo++;
                    rcLedgerTrans openTranAll = new rcLedgerTrans();
                    openTranAll.GLAccountID = prmLedger.GLAccountID;
                    openTranAll.TranDate = prmLedger.FromDate;
                    openTranAll.TranSLNo = slNo;
                    openTranAll.TranDesc = "Openning Balance";

                    openTranAll.DebitAmt = openDebitAmt;
                    openTranAll.CreditAmt = openCreditAmt;

                    debitCreditBal = openDebitAmt - openCreditAmt;
                    openTranAll.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranAll.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranAll.DebitAmtDisplay = openTranAll.DebitBalanceAmt;
                    openTranAll.CreditAmtDisplay = openTranAll.CreditBalanceAmt;


                    openTranAll.BalanceAmt = Math.Abs(openBalanceAmt);
                    openTranAll.BalanceAmtDisplay = Math.Abs(openTranAll.BalanceAmt);
                    openTranAll.DrCrBalance = openBalanceAmt >= 0 ? 0 : 1;
                    if (openBalanceAmt != 0)
                    {
                        openTranAll.DrCrText = openBalanceAmt > 0 ? "Dr" : "Cr";
                    }

                    openTranAll.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranAll);

                    curBalance = openBalanceAmt;

                    break;
                case InculdeOpBalanceEnum.IncludeYear:
                    slNo++;
                    rcLedgerTrans openTranYr = new rcLedgerTrans();
                    openTranYr.GLAccountID = prmLedger.GLAccountID;
                    openTranYr.TranDate = accYear.YearStartDate;
                    openTranYr.TranSLNo = slNo;
                    openTranYr.TranDesc = "Openning Balance";

                    openTranYr.DebitAmt = openDebitAmtYear;
                    openTranYr.CreditAmt = openCreditAmtYear;

                    debitCreditBal = openDebitAmtYear - openCreditAmtYear;
                    openTranYr.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranYr.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranYr.DebitAmtDisplay = openTranYr.DebitBalanceAmt;
                    openTranYr.CreditAmtDisplay = openTranYr.CreditBalanceAmt;


                    openTranYr.BalanceAmt = Math.Abs(openBalanceAmtYear);
                    openTranYr.BalanceAmtDisplay = openTranYr.BalanceAmt;



                    openTranYr.DrCrBalance = openBalanceAmtYear >= 0 ? 0 : 1;
                    if (openBalanceAmtYear != 0)
                    {
                        openTranYr.DrCrText = openBalanceAmtYear > 0 ? "Dr" : "Cr";
                    }
                    openTranYr.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranYr);

                    curBalance = openBalanceAmtYear;
                    break;
                case InculdeOpBalanceEnum.IncludeDateRange:
                    slNo++;
                    rcLedgerTrans openTranDr = new rcLedgerTrans();
                    openTranDr.GLAccountID = prmLedger.GLAccountID;
                    openTranDr.TranDate = prmLedger.FromDate;
                    openTranDr.TranSLNo = slNo;
                    openTranDr.TranDesc = "Openning Balance - Date Range";
                    openTranDr.DebitAmt = openDebitAmtDateRange;
                    openTranDr.CreditAmt = openCreditAmtDateRange;


                    debitCreditBal = openDebitAmtDateRange - openCreditAmtDateRange;
                    openTranDr.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranDr.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranDr.DebitAmtDisplay = openTranDr.DebitBalanceAmt;
                    openTranDr.CreditAmtDisplay = openTranDr.CreditBalanceAmt;
                    
                    
                    openTranDr.BalanceAmt = Math.Abs(openBalanceAmtDateRange);
                    openTranDr.BalanceAmtDisplay = openTranDr.BalanceAmt;


                    openTranDr.DrCrBalance = openBalanceAmtDateRange >= 0 ? 0 : 1;
                    if (openBalanceAmtDateRange != 0)
                    {
                        openTranDr.DrCrText = openBalanceAmtDateRange > 0 ? "Dr" : "Cr";
                    }
                    openTranDr.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranDr);
                    curBalance = openBalanceAmtDateRange;


                    break;
                case InculdeOpBalanceEnum.IncludeALLIndvidual:
                    slNo++;
                    rcLedgerTrans openTranYr1 = new rcLedgerTrans();
                    openTranYr1.GLAccountID = prmLedger.GLAccountID;
                    openTranYr1.TranDate = accYear.YearStartDate;
                    openTranYr1.TranSLNo = slNo;
                    openTranYr1.TranDesc = "Openning Balance - Year";

                    openTranYr1.DebitAmt = openDebitAmtYear;
                    openTranYr1.CreditAmt = openCreditAmtYear;


                    debitCreditBal = openDebitAmtYear - openCreditAmtYear;
                    openTranYr1.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranYr1.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranYr1.DebitAmtDisplay = openTranYr1.DebitBalanceAmt;
                    openTranYr1.CreditAmtDisplay = openTranYr1.CreditBalanceAmt;
                    


                    openTranYr1.BalanceAmt = Math.Abs(openBalanceAmtYear);

                    openTranYr1.BalanceAmtDisplay = openTranYr1.BalanceAmt;

                    openTranYr1.DrCrBalance = openBalanceAmtYear >= 0 ? 0 : 1;
                    if (openBalanceAmtYear != 0)
                    {
                        openTranYr1.DrCrText = openBalanceAmtYear > 0 ? "Dr" : "Cr";
                    }
                    openTranYr1.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranYr1);

                    curBalance = openBalanceAmtYear;


                    slNo++;
                    rcLedgerTrans openTranDr1 = new rcLedgerTrans();
                    openTranDr1.GLAccountID = prmLedger.GLAccountID;
                    openTranDr1.TranDate = prmLedger.FromDate;
                    openTranDr1.TranSLNo = slNo;
                    openTranDr1.TranDesc = "Openning Balance - Date Range";
                    openTranDr1.DebitAmt = openDebitAmtDateRange;
                    openTranDr1.CreditAmt = openCreditAmtDateRange;

                    debitCreditBal = openDebitAmtDateRange - openCreditAmtDateRange;
                    openTranDr1.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranDr1.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranDr1.DebitAmtDisplay = openTranDr1.DebitBalanceAmt;
                    openTranDr1.CreditAmtDisplay = openTranDr1.CreditBalanceAmt;
                    


                    openTranDr1.BalanceAmt = Math.Abs(openBalanceAmt);
                    openTranDr1.BalanceAmtDisplay = openTranDr1.BalanceAmt;


                    openTranDr1.DrCrBalance = openBalanceAmt >= 0 ? 0 : 1;
                    if (openBalanceAmt != 0)
                    {
                        openTranDr1.DrCrText = openBalanceAmt > 0 ? "Dr" : "Cr";
                    }
                    openTranDr1.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranDr1);
                    curBalance = openBalanceAmt;

                    break;
            }


            ///trans

            foreach (dcJournalDet transDate in transListDate)
            {
                slNo++;
                rcLedgerTrans cLTran = new rcLedgerTrans();
                cLTran.GLAccountID = prmLedger.GLAccountID;
                cLTran.TranSLNo = slNo;
                cLTran.TranDate = transDate.JournalDate;

                cLTran.JournalNo = transDate.JournalNo;
                cLTran.JournalTypeID = transDate.JournalTypeID;
                cLTran.JournalTypeCode = transDate.JournalTypeCode;
                cLTran.JournalTypeName = transDate.JournalTypeName;
                cLTran.IsPosted = transDate.IsPosted;
                cLTran.IsReconciled = transDate.IsReconciled;
                cLTran.JournalAdjsutTypeID = transDate.JournalAdjustTypeID;

                cLTran.TranCodeAccRefCode = transDate.TranTypeCode; 
                //cLTran.TranTypeName = Conversion.DBNullStringToEmpty(dRow["AccGLTranTypeName"]);

                cLTran.TranNote = transDate.JournalDetNote;
                cLTran.TranDesc = transDate.JournalDetDesc;

                cLTran.DebitAmt = transDate.DebitAmt;
                cLTran.CreditAmt = transDate.CreditAmt;

                debitCreditBal = cLTran.DebitAmt - cLTran.CreditAmt;
                cLTran.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                cLTran.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;


                cLTran.DebitAmtDisplay = cLTran.DebitAmt;
                cLTran.CreditAmtDisplay = cLTran.CreditAmt;

                curBalance += cLTran.DebitAmt - cLTran.CreditAmt;
                curDrCr  = curBalance >= 0 ? (int)DebitCreditEnum.Debit : (int)DebitCreditEnum.Credit;
                curDrCrString = curBalance >= 0 ? "Dr" : "Cr";
                curDrCrString = curBalance == 0 ? "" : curDrCrString;


                cLTran.BalanceAmt = Math.Abs(curBalance);
                cLTran.BalanceAmtDisplay = cLTran.BalanceAmt;

                cLTran.DrCrBalance = curDrCr;
                cLTran.DrCrText = curDrCrString;


                cLTran.NumberFormat = prmLedger.NumberFormat;

                transList.Add(cLTran);

                totDebitAmt += cLTran.DebitAmt;
                totCreditAmt += cLTran.CreditAmt;
            }


            cRpt.GLAccountCode = acc.GLAccountCode;
            cRpt.GLAccountID = acc.GLAccountID;
            cRpt.GLAccountName = acc.GLAccountName;
            cRpt.GLGroupName = group.GLGroupName;

            cRpt.AccYearName = accYear.AccYearName;
            
            cRpt.TotDebitAmt = totDebitAmt;
            cRpt.TotCreditAmt = totCreditAmt;

            cRpt.BalanceAmt = Math.Abs(curBalance);
            cRpt.DrCrBalance = curDrCr;
            cRpt.DrCrText = curDrCrString;

            cRpt.LedgerTrans = transList;

            cRpt.DateString = prmLedger.FromDate.Value.ToString("dd-MMM-yyyy") + " To " +  prmLedger.ToDate.Value.ToString("dd-MMM-yyyy");


            cRptList.Add(cRpt);

            return cRptList;
        }

        public static List<rcLedger> GetLedgerFull(clsPrmLedger prmLedger)
        {
            return GetLedgerFull(prmLedger, null);
        }
        public static List<rcLedger> GetLedgerFull(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcLedger> cRptList = new List<rcLedger>();

            rcLedger cRpt = new rcLedger();

            dcAccYear accYear = AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID, dc);

            dcGLAccount acc = GLAccountBL.GetGLAccountByID(prmLedger.CompanyID, prmLedger.GLAccountID, dc);
            dcGLGroup group = GLGroupBL.GetGLGroupByID(prmLedger.CompanyID, acc.GLGroupID, dc);


            cRpt.GLAccountID = acc.GLAccountID;
            cRpt.GLAccountCode = acc.GLAccountCode;
            cRpt.GLAccountName = acc.GLAccountName;
            cRpt.GLGroupName = acc.GLGroupName;
            cRpt.GLClassName = acc.GLClassName;

            cRpt.GLAccountNameFull = acc.GLAccountCodeName;

            //DataTable dtTrans = BLLibrary.AccountingBL.AccJournalDetBL.GetJournalDetByDate(prmLedger, dc);
            List<dcJournalDet> transListDate = new List<dcJournalDet>();
            //List<dcJournalDet> transListBfDate = new List<dcJournalDet>();

            List<dcJournalDet> cDetListSub = new List<dcJournalDet>();
            List<dcJournalDet> cDetListContra = new List<dcJournalDet>();
            List<dcJournalDetIns> cInsList = new List<dcJournalDetIns>();

            decimal openDebitAmtYear = 0;
            decimal openCreditAmtYear = 0;
            decimal openDebitAmtDateRange = 0;
            decimal openCreditAmtDateRange = 0;



            if (prmLedger.IncludeInstrument)
            {
                cInsList = JournalDetBL.GetJournalInsDet(prmLedger, dc);
            }
            List<dcJournalDetRef> cRefList = JournalDetBL.GetJournalRefDet(prmLedger, dc);


            //clsPrmLedger prmLdgBfDate = (clsPrmLedger)prmLedger.Clone();
            //prmLdgBfDate.IsBeforeDate = true;

            //if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            //{
            //    transListDate = JournalDetBL.GetJournalDetByDate_ControlSum(prmLedger, dc);
            //    transListBfDate = JournalDetBL.GetJournalDetSumByDate_Control(prmLdgBfDate, null, dc);

            //    //transListBfDate = JournalDetBL.GetJournalDetByDate_ControlSum(prmLdgBfDate, dc);
                
            //    if (prmLedger.IncludeSubAccountForControl)
            //    {
            //        cDetListSub = JournalDetBL.GetJournalDetByDate_SubByControl(prmLedger, dc);
            //    }
            //}
            //else
            //{
            //    transListDate = JournalDetBL.GetJournalDetByDate(prmLedger, dc);
            //    if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.NormalAccount)
            //    {
            //        transListBfDate = JournalDetBL.GetJournalDetSumByDate_Normal(prmLdgBfDate, null, dc);
            //    }
            //    else
            //    {
            //        transListBfDate = JournalDetBL.GetJournalDetSumByDate_Sub(prmLdgBfDate, null, dc);
            //    }
                
            //    //transListBfDate = JournalDetBL.GetJournalDetByDate(prmLdgBfDate, dc);
            //}

            if (prmLedger.IncludeContraEntry)
            {

                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.NormalAccount)
                {
                    cDetListContra = JournalDetBL.GetJournalDetListContra_NormalForNormal(prmLedger, dc);
                    cDetListContra.AddRange(JournalDetBL.GetJournalDetListContra_ControlSumForNormal(prmLedger, dc));
                }

                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    cDetListContra = JournalDetBL.GetJournalDetListContra_NormalForControl(prmLedger, dc);
                    cDetListContra.AddRange(JournalDetBL.GetJournalDetListContra_ControlSumForControl(prmLedger, dc));
                }

                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.SubAccount)
                {
                    cDetListContra = JournalDetBL.GetJournalDetListContra_NormalForSub(prmLedger, dc);
                    cDetListContra.AddRange(JournalDetBL.GetJournalDetListContra_ControlSumForSub(prmLedger, dc));
                }
            }


            //decimal hisBalAmt = 0;
            //if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL)
            //{
            //    dcGLAccountHistory accHist = null;
            //    //accHist = GLAccountHistoryBL.GetGLAccountHistoryByID(prmLedger.GLAccountID, prmLedger.AccYearID, dc);
            //    if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            //    {
            //        accHist = GLAccountHistoryBL.GetGLAccountHistoryByID_Control(prmLedger.CompanyID, prmLedger.AccYearID, prmLedger.GLAccountID, dc);
            //    }
            //    else
            //    {
            //        accHist = GLAccountHistoryBL.GetGLAccountHistoryByID(prmLedger.CompanyID, prmLedger.AccYearID, prmLedger.GLAccountID, dc);
            //    }

            //    if (accHist != null)
            //    {
            //        hisBalAmt = accHist.DebitAmtOpen - accHist.CreditAmtOpen;
            //    }
            //}

            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeYear)
            {
                dcGLAccountHistory accHist = null;
                //accHist = GLAccountHistoryBL.GetGLAccountHistoryByID(prmLedger.GLAccountID, prmLedger.AccYearID, dc);
                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    accHist = GLAccountHistoryBL.GetGLAccountHistoryByID_Control(prmLedger.CompanyID, prmLedger.AccYearID, prmLedger.GLAccountID, dc);
                }
                else
                {
                    accHist = GLAccountHistoryBL.GetGLAccountHistoryByID(prmLedger.CompanyID, prmLedger.AccYearID, prmLedger.GLAccountID, dc);
                }

                if (accHist != null)
                {
                    openDebitAmtYear = accHist.DebitAmtOpen;
                    openCreditAmtYear = accHist.CreditAmtOpen;
                }
            }


            //decimal prevDrAmt = 0;
            //decimal prevCrAmt = 0;
            //if (transListBfDate.Count > 0)
            //{
            //    dcJournalDet tranBfDate = transListBfDate.FirstOrDefault(c => c.GLAccountID == prmLedger.GLAccountID);

            //    if (tranBfDate != null)
            //    {
            //        prevDrAmt = tranBfDate.DebitAmt;
            //        prevCrAmt = tranBfDate.CreditAmt;
            //    }
            //}

            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                    || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                    || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeDateRange)
            {
                List<dcJournalDet> transListBfDate = new List<dcJournalDet>();

                clsPrmLedger prmLdgBfDate = (clsPrmLedger)prmLedger.Clone();
                prmLdgBfDate.IsBeforeDate = true;

                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    //transListDate = JournalDetBL.GetJournalDetByDate_ControlSum(prmLedger, dc);
                    transListBfDate = JournalDetBL.GetJournalDetSumByDate_Control(prmLdgBfDate, null, dc);
                }
                else
                {
                    //transListDate = JournalDetBL.GetJournalDetByDate(prmLedger, dc);
                    if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.NormalAccount)
                    {
                        transListBfDate = JournalDetBL.GetJournalDetSumByDate_Normal(prmLdgBfDate, null, dc);
                    }
                    else
                    {
                        transListBfDate = JournalDetBL.GetJournalDetSumByDate_Sub(prmLdgBfDate, null, dc);
                    }
                }

                if (transListBfDate.Count > 0)
                {
                    dcJournalDet tranBfDate = transListBfDate.FirstOrDefault(c => c.GLAccountID == prmLedger.GLAccountID);

                    if (tranBfDate != null)
                    {
                        openDebitAmtDateRange = tranBfDate.DebitAmt;
                        openCreditAmtDateRange = tranBfDate.CreditAmt;
                    }
                }

            }


            if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            {
                transListDate = JournalDetBL.GetJournalDetByDate_ControlSum(prmLedger, dc);
            }
            else
            {
                transListDate = JournalDetBL.GetJournalDetByDate(prmLedger, dc);
            }

            //decimal opBalance = hisBalAmt + prevDrAmt - prevCrAmt;
            //int DrCrOpen = opBalance >= 0 ? (int)DebitCreditEnum.Debit : (int)DebitCreditEnum.Credit;
            //string DrCrString = opBalance >= 0 ? "Dr" : "Cr";
            //DrCrString = opBalance == 0 ? "" : DrCrString;

            

            decimal openBalanceAmtYear = openDebitAmtYear - openCreditAmtYear;
            decimal openBalanceAmtDateRange = openDebitAmtDateRange - openCreditAmtDateRange;

            decimal openDebitAmt = openDebitAmtYear + openDebitAmtDateRange;
            decimal openCreditAmt = openCreditAmtYear + openCreditAmtDateRange;

            decimal openBalanceAmt = openDebitAmt - openCreditAmt;


            List<rcLedgerTrans> transList = new List<rcLedgerTrans>();

            int slNo = 0;
            int detGroupID = 0;


            ///transactions
            decimal curBalance = 0;
            decimal totDebitAmt = 0;
            decimal totCreditAmt = 0;

            decimal debitCreditBal = 0;

            int curDrCr = 0;
            string curDrCrString = "Dr";

            //curBalance = opBalance;

            switch (prmLedger.IncludeOpBalanceType)
            {
                case InculdeOpBalanceEnum.None:
                    curBalance = openBalanceAmt;
                    break;

                case InculdeOpBalanceEnum.IncludeALL:
                    slNo++;
                    rcLedgerTrans openTranAll = new rcLedgerTrans();
                    openTranAll.GLAccountID = prmLedger.GLAccountID;
                    openTranAll.TranDate = prmLedger.FromDate;
                    openTranAll.TranSLNo = slNo;
                    openTranAll.TranDesc = "Openning Balance";

                    openTranAll.DebitAmt = openDebitAmt;
                    openTranAll.CreditAmt = openCreditAmt;

                    debitCreditBal = openDebitAmt - openCreditAmt;
                    openTranAll.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranAll.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranAll.DebitAmtDisplay = openTranAll.DebitBalanceAmt;
                    openTranAll.CreditAmtDisplay = openTranAll.CreditBalanceAmt;


                    openTranAll.BalanceAmt = Math.Abs(openBalanceAmt);
                    openTranAll.BalanceAmtDisplay = Math.Abs(openTranAll.BalanceAmt);
                    openTranAll.DrCrBalance = openBalanceAmt >= 0 ? 0 : 1;
                    if (openBalanceAmt != 0)
                    {
                        openTranAll.DrCrText = openBalanceAmt > 0 ? "Dr" : "Cr";
                    }

                    openTranAll.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranAll);

                    curBalance = openBalanceAmt;

                    break;
                case InculdeOpBalanceEnum.IncludeYear:
                    slNo++;
                    rcLedgerTrans openTranYr = new rcLedgerTrans();
                    openTranYr.GLAccountID = prmLedger.GLAccountID;
                    openTranYr.TranDate = accYear.YearStartDate;
                    openTranYr.TranSLNo = slNo;
                    openTranYr.TranDesc = "Openning Balance";

                    openTranYr.DebitAmt = openDebitAmtYear;
                    openTranYr.CreditAmt = openCreditAmtYear;

                    debitCreditBal = openDebitAmtYear - openCreditAmtYear;
                    openTranYr.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranYr.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranYr.DebitAmtDisplay = openTranYr.DebitBalanceAmt;
                    openTranYr.CreditAmtDisplay = openTranYr.CreditBalanceAmt;


                    openTranYr.BalanceAmt = Math.Abs(openBalanceAmtYear);
                    openTranYr.BalanceAmtDisplay = openTranYr.BalanceAmt;



                    openTranYr.DrCrBalance = openBalanceAmtYear >= 0 ? 0 : 1;
                    if (openBalanceAmtYear != 0)
                    {
                        openTranYr.DrCrText = openBalanceAmtYear > 0 ? "Dr" : "Cr";
                    }
                    openTranYr.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranYr);

                    curBalance = openBalanceAmtYear;
                    break;
                case InculdeOpBalanceEnum.IncludeDateRange:
                    slNo++;
                    rcLedgerTrans openTranDr = new rcLedgerTrans();
                    openTranDr.GLAccountID = prmLedger.GLAccountID;
                    openTranDr.TranDate = prmLedger.FromDate;
                    openTranDr.TranSLNo = slNo;
                    openTranDr.TranDesc = "Openning Balance - Date Range";
                    openTranDr.DebitAmt = openDebitAmtDateRange;
                    openTranDr.CreditAmt = openCreditAmtDateRange;


                    debitCreditBal = openDebitAmtDateRange - openCreditAmtDateRange;
                    openTranDr.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranDr.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranDr.DebitAmtDisplay = openTranDr.DebitBalanceAmt;
                    openTranDr.CreditAmtDisplay = openTranDr.CreditBalanceAmt;


                    openTranDr.BalanceAmt = Math.Abs(openBalanceAmtDateRange);
                    openTranDr.BalanceAmtDisplay = openTranDr.BalanceAmt;


                    openTranDr.DrCrBalance = openBalanceAmtDateRange >= 0 ? 0 : 1;
                    if (openBalanceAmtDateRange != 0)
                    {
                        openTranDr.DrCrText = openBalanceAmtDateRange > 0 ? "Dr" : "Cr";
                    }
                    openTranDr.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranDr);
                    curBalance = openBalanceAmtDateRange;


                    break;
                case InculdeOpBalanceEnum.IncludeALLIndvidual:
                    slNo++;
                    rcLedgerTrans openTranYr1 = new rcLedgerTrans();
                    openTranYr1.GLAccountID = prmLedger.GLAccountID;
                    openTranYr1.TranDate = accYear.YearStartDate;
                    openTranYr1.TranSLNo = slNo;
                    openTranYr1.TranDesc = "Openning Balance - Year";

                    openTranYr1.DebitAmt = openDebitAmtYear;
                    openTranYr1.CreditAmt = openCreditAmtYear;


                    debitCreditBal = openDebitAmtYear - openCreditAmtYear;
                    openTranYr1.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranYr1.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranYr1.DebitAmtDisplay = openTranYr1.DebitBalanceAmt;
                    openTranYr1.CreditAmtDisplay = openTranYr1.CreditBalanceAmt;



                    openTranYr1.BalanceAmt = Math.Abs(openBalanceAmtYear);

                    openTranYr1.BalanceAmtDisplay = openTranYr1.BalanceAmt;

                    openTranYr1.DrCrBalance = openBalanceAmtYear >= 0 ? 0 : 1;
                    if (openBalanceAmtYear != 0)
                    {
                        openTranYr1.DrCrText = openBalanceAmtYear > 0 ? "Dr" : "Cr";
                    }
                    openTranYr1.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranYr1);

                    curBalance = openBalanceAmtYear;


                    slNo++;
                    rcLedgerTrans openTranDr1 = new rcLedgerTrans();
                    openTranDr1.GLAccountID = prmLedger.GLAccountID;
                    openTranDr1.TranDate = prmLedger.FromDate;
                    openTranDr1.TranSLNo = slNo;
                    openTranDr1.TranDesc = "Openning Balance - Date Range";
                    openTranDr1.DebitAmt = openDebitAmtDateRange;
                    openTranDr1.CreditAmt = openCreditAmtDateRange;

                    debitCreditBal = openDebitAmtDateRange - openCreditAmtDateRange;
                    openTranDr1.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranDr1.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranDr1.DebitAmtDisplay = openTranDr1.DebitBalanceAmt;
                    openTranDr1.CreditAmtDisplay = openTranDr1.CreditBalanceAmt;



                    openTranDr1.BalanceAmt = Math.Abs(openBalanceAmt);
                    openTranDr1.BalanceAmtDisplay = openTranDr1.BalanceAmt;


                    openTranDr1.DrCrBalance = openBalanceAmt >= 0 ? 0 : 1;
                    if (openBalanceAmt != 0)
                    {
                        openTranDr1.DrCrText = openBalanceAmt > 0 ? "Dr" : "Cr";
                    }
                    openTranDr1.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranDr1);
                    curBalance = openBalanceAmt;

                    break;
            }



            // List<dcAccGLTran> curTranList = accTranList.Where(c => c.TranDate >= fromDate).OrderBy(c => c.TranDate).ToList();

            foreach (dcJournalDet transDate in transListDate)
            {
                slNo++;
                transDate.JournalDetSLNo = slNo;

                if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    detGroupID--;
                    transDate.JournalDetID =  detGroupID;
                }

                rcLedgerTrans cLTran = new rcLedgerTrans();
                Helper.CopyPropertyValueByName(transDate, cLTran);
                cLTran.NumberFormat = prmLedger.NumberFormat;

                cLTran.DetGroupID = transDate.JournalDetID;
                cLTran.TranSLNo = transDate.JournalDetSLNo;
                cLTran.TranDate = transDate.JournalDate;


                cLTran.TranNote = transDate.JournalDetNote;
                cLTran.TranDesc = transDate.JournalDetDesc;

                cLTran.DebitAmt = transDate.DebitAmt;
                cLTran.CreditAmt = transDate.CreditAmt;

                debitCreditBal = cLTran.DebitAmt - cLTran.CreditAmt;
                cLTran.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                cLTran.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                //cLTran.DebitAmtDisplay = cLTran.DebitBalanceAmt;
                //cLTran.CreditAmtDisplay = cLTran.CreditBalanceAmt;

                cLTran.DebitAmtDisplay = cLTran.DebitAmt;
                cLTran.CreditAmtDisplay = cLTran.CreditAmt;


                curBalance += cLTran.DebitAmt - cLTran.CreditAmt;
                curDrCr = curBalance >= 0 ? (int)DebitCreditEnum.Debit : (int)DebitCreditEnum.Credit;
                curDrCrString = curBalance >= 0 ? "Dr" : "Cr";
                curDrCrString = curBalance == 0 ? "" : curDrCrString;

                cLTran.RunningTotalAmt = curBalance;

                cLTran.BalanceAmt = Math.Abs(curBalance);
                cLTran.BalanceAmtDisplay = cLTran.BalanceAmt;

                cLTran.DrCrBalance = curDrCr;
                cLTran.DrCrText = curDrCrString;

                AddTranItem(cLTran, transDate, cInsList, cRefList, cDetListSub, cDetListContra, prmLedger, transList);


                totDebitAmt += transDate.DebitAmt;
                totCreditAmt += transDate.CreditAmt;
            }


            cRpt.GLAccountCode = acc.GLAccountCode;
            cRpt.GLAccountID = acc.GLAccountID;
            cRpt.GLAccountName = acc.GLAccountName;
            cRpt.GLGroupName = group.GLGroupName;

            cRpt.AccYearName = accYear.AccYearName;

            cRpt.TotDebitAmt = totDebitAmt;
            cRpt.TotCreditAmt = totCreditAmt;

            cRpt.BalanceAmt = Math.Abs(curBalance);
            cRpt.DrCrBalance = curDrCr;
            cRpt.DrCrText = curDrCrString;

            cRpt.LedgerTrans = transList;

            cRpt.DateString = prmLedger.FromDate.Value.ToString("dd-MMM-yyyy") + " To " + prmLedger.ToDate.Value.ToString("dd-MMM-yyyy");


            cRptList.Add(cRpt);

            return cRptList;
        }



        public static void AddTranItem(rcLedgerTrans cTran, dcJournalDet jrnlDet, List<dcJournalDetIns> cInsList, List<dcJournalDetRef> cRefList, List<dcJournalDet> cDetListSub
                                       , List<dcJournalDet> cDetListContra, clsPrmLedger prmLedger, List<rcLedgerTrans> cRptList)
        {
            bool tranAdd = true;

  
            //
            if (jrnlDet.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount)
            {
                if (prmLedger.IncludeTranCode)
                {
                    dcJournalDetRef cTranCode = cRefList.FirstOrDefault(c => c.JournalDetID == cTran.JournalDetID
                                            && c.AccRefTypeID == (int)AccRefTypeEnum.TranCode);
                    if (cTranCode != null)
                    {
                        cTran.TranCodeAccRefCode = cTranCode.AccRefCode;
                    }
                }
            }

            if (prmLedger.ControlAccountSummary && prmLedger.IncludeSubAccountForControl)
            {
                List<dcJournalDet> cSubListDet = cDetListSub.Where(c => c.JournalID == cTran.JournalID
                                                                    && c.GLAccountIDParent == cTran.GLAccountID).ToList();

                foreach (dcJournalDet cSub in cSubListDet)
                {
                    rcLedgerTrans nTran = new rcLedgerTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);

                    nTran.ReportTranType = GLReportItemTranTypeEnum.SubAcc;
                    nTran.NumberFormat = prmLedger.NumberFormat;
                    nTran.DetTypeName = "Sub Ledger";
                    nTran.DetDesc = cSub.GLAccountCode + " " + cSub.GLAccountName;
                    nTran.DetAmt = cSub.DebitAmt - cSub.CreditAmt;
                    nTran.DetBalanceAmt = Math.Abs(nTran.DetAmt);
                    nTran.DetAmtDisplay = nTran.DetBalanceAmt;

                    nTran.DetAmtDrCr = nTran.DetAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = nTran.DetAmt > 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                        nTran.DebitBalanceAmt = 0;
                        nTran.CreditBalanceAmt = 0;
                        nTran.DebitAmtDisplay = 0;
                        nTran.CreditAmtDisplay = 0;

                        nTran.BalanceAmt = 0;
                        nTran.BalanceAmtDisplay = 0;

                        //nTran.DrCrText = "";
                    }

                    cRptList.Add(nTran);

                    tranAdd = false;
                }
            }



            if (prmLedger.IncludeContraEntry)
            {
                List<dcJournalDet> cContraListDet = cDetListContra.Where(c => c.JournalID == cTran.JournalID
                                                                   && c.GLAccountID != cTran.GLAccountID).ToList();

                //List<dcJournalDet> cContraListDet = cDetListContra.Where(c => c.JournalID == cTran.JournalID).ToList();
                decimal trnAmt = 0;
                foreach (dcJournalDet cContra in cContraListDet)
                {
                    rcLedgerTrans nTran = new rcLedgerTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.ReportTranType = GLReportItemTranTypeEnum.Contra;
                    nTran.NumberFormat = prmLedger.NumberFormat;
                    nTran.DetTypeName = "Contra";
                    nTran.DetDesc = cContra.GLAccountCode + " " + cContra.GLAccountName;

                    nTran.DetAmt = cContra.DebitAmt - cContra.CreditAmt;
                    nTran.DetBalanceAmt = Math.Abs(nTran.DetAmt);
                    nTran.DetAmtDisplay = nTran.DetBalanceAmt;
                    
                    nTran.DetAmtDrCr = trnAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = nTran.DetAmt > 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                        nTran.DebitBalanceAmt = 0;
                        nTran.CreditBalanceAmt = 0;
                        nTran.DebitAmtDisplay = 0;
                        nTran.CreditAmtDisplay = 0;

                        nTran.BalanceAmt = 0;
                        nTran.BalanceAmtDisplay = 0;
                        
                        //nTran.DrCrText = "";


                    }

                    cRptList.Add(nTran);

                    tranAdd = false;
                }



            }



            if (jrnlDet.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            {
                if (prmLedger.IncludeTranCode)
                {

                    List<dcJournalDetRef> cRefListTranCode = cRefList.Where(c => c.JournalID == cTran.JournalID
                                                                                    && c.GLAccountIDParent == cTran.GLAccountID
                                                                                    && c.AccRefTypeID == (int)AccRefTypeEnum.TranCode).ToList();

                    foreach (dcJournalDetRef cTranCode in cRefListTranCode)
                    {
                        rcLedgerTrans nTran = new rcLedgerTrans();
                        Helper.CopyPropertyValueByName(cTran, nTran);
                        nTran.ReportTranType = GLReportItemTranTypeEnum.CostCenter;
                        nTran.NumberFormat = prmLedger.NumberFormat;
                        nTran.DetTypeName = "Tran Code";
                        nTran.DetDesc = cTranCode.AccRefCode + " " + cTranCode.AccRefCategoryCode;


                        nTran.DetAmt = cTranCode.DebitAmt - cTranCode.CreditAmt;
                        nTran.DetBalanceAmt = Math.Abs(nTran.DetAmt);
                        nTran.DetAmtDisplay = nTran.DetBalanceAmt;


                        nTran.DetAmtDrCr = nTran.DetAmt >= 0 ? 0 : 1;
                        nTran.DetAmtDrCrText = nTran.DetAmt > 0 ? "Dr" : "Cr";

                        nTran.DetCount = 1;

                        if (tranAdd == false)
                        {
                            nTran.DebitAmt = 0;
                            nTran.CreditAmt = 0;
                            nTran.DebitBalanceAmt = 0;
                            nTran.CreditBalanceAmt = 0;
                            nTran.DebitAmtDisplay = 0;
                            nTran.CreditAmtDisplay = 0;

                            nTran.BalanceAmt = 0;
                            nTran.BalanceAmtDisplay = 0;

                            //nTran.DrCrText = "";
                        }

                        cRptList.Add(nTran);

                        tranAdd = false;
                    }
                }
            }





            if (prmLedger.IncludeInstrument)
            {

                //List<dcJournalDetIns> cInsListDet = cInsList.Where(c => c.JournalDetID == cTran.JournalDetID).ToList();
                List<dcJournalDetIns> cInsListDet = new List<dcJournalDetIns>();

                if (jrnlDet.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    //cInsListDet = cInsList.Where(c => c.JournalID == cTran.JournalID && c.GLAccountID).ToList();
                }
                else
                {
                    cInsListDet = cInsList.Where(c => c.JournalDetID == cTran.JournalDetID).ToList();
                }

                foreach (dcJournalDetIns cIns in cInsListDet)
                {
                    rcLedgerTrans nTran = new rcLedgerTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.NumberFormat = prmLedger.NumberFormat;
                    nTran.ReportTranType = GLReportItemTranTypeEnum.Instrument;
                    nTran.DetTypeName = "Insturment";
                    nTran.DetDesc = cIns.InstrumentTypeName + " " + cIns.InstrumentNo + " " + cIns.InstrumentDate.Value.ToString("dd-MMM-yyyy");
                    nTran.DetAmt = cIns.InsTranAmt;
                    nTran.DetBalanceAmt = Math.Abs(nTran.DetAmt);
                    nTran.DetAmtDisplay = nTran.DetBalanceAmt;
                    //nTran.DetAmtDrCrText = cIns.InstrumentModeID == (int)InstrumentModeEnum.Issue ? "Is" : "Rc";

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                        nTran.DebitBalanceAmt = 0;
                        nTran.CreditBalanceAmt = 0;
                        nTran.DebitAmtDisplay = 0;
                        nTran.CreditAmtDisplay = 0;

                        nTran.BalanceAmt = 0;
                        nTran.BalanceAmtDisplay = 0;

                        //nTran.DrCrText = "";
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
                    rcLedgerTrans nTran = new rcLedgerTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.ReportTranType = GLReportItemTranTypeEnum.CostCenter;
                    nTran.NumberFormat = prmLedger.NumberFormat;
                    nTran.DetTypeName = "Cost Center";
                    nTran.DetDesc = cCostcenter.AccRefCode + " " + cCostcenter.AccRefCategoryCode;

                    nTran.DetAmt = cCostcenter.DebitAmt - cCostcenter.CreditAmt;
                    nTran.DetBalanceAmt = Math.Abs(nTran.DetAmt);
                    nTran.DetAmtDisplay = nTran.DetBalanceAmt;

                    nTran.DetAmtDrCr = nTran.DetAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = nTran.DetAmt > 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                        nTran.DebitBalanceAmt = 0;
                        nTran.CreditBalanceAmt = 0;
                        nTran.DebitAmtDisplay = 0;
                        nTran.CreditAmtDisplay = 0;

                        nTran.BalanceAmt = 0;
                        nTran.BalanceAmtDisplay = 0;

                        //nTran.DrCrText = "";
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
                    rcLedgerTrans nTran = new rcLedgerTrans();
                    Helper.CopyPropertyValueByName(cTran, nTran);
                    nTran.ReportTranType = GLReportItemTranTypeEnum.Reference;
                    nTran.NumberFormat = prmLedger.NumberFormat;
                    nTran.DetTypeName = "Reference";
                    nTran.DetDesc = cRef.AccRefCode + " " + cRef.AccRefCategoryCode;

                    nTran.DetAmt = cRef.DebitAmt - cRef.CreditAmt;
                    nTran.DetBalanceAmt = Math.Abs(nTran.DetAmt);
                    nTran.DetAmtDisplay = nTran.DetBalanceAmt;

                    nTran.DetAmtDrCr = nTran.DetAmt >= 0 ? 0 : 1;
                    nTran.DetAmtDrCrText = nTran.DetAmt >= 0 ? "Dr" : "Cr";
                    nTran.DetAmtDrCrText = nTran.DetAmt == 0 ? "" : nTran.DetAmtDrCrText;

                    nTran.DetCount = 1;

                    if (tranAdd == false)
                    {
                        nTran.DebitAmt = 0;
                        nTran.CreditAmt = 0;
                        nTran.DebitBalanceAmt = 0;
                        nTran.CreditBalanceAmt = 0;
                        
                        nTran.DebitAmtDisplay = 0;
                        nTran.CreditAmtDisplay = 0;

                        nTran.BalanceAmt = 0;
                        nTran.BalanceAmtDisplay = 0;

                        //nTran.DrCrText = "";
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

        public static List<rcLedgerSummary> GetLedgerSummary(clsPrmLedger prmLedger)
        {
            return GetLedgerSummary(prmLedger, null);
        }

        public static List<rcLedgerSummary> GetLedgerSummary(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcLedgerSummary> cRptList = new List<rcLedgerSummary>();

            rcLedgerSummary cRpt = new rcLedgerSummary();

            rcGLReportHeader cRptHeader = new rcGLReportHeader();

            dcAccYear year = BLLibrary.AccountingBL.AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);


            cRptHeader.AccYearID = prmLedger.AccYearID;
            cRptHeader.AccYearName = year.AccYearName;

            cRptHeader.FromDate = prmLedger.FromDate;
            cRptHeader.ToDate = prmLedger.ToDate;

            cRptHeader.NumberFormat = prmLedger.NumberFormat;


            ///summary
            List<rcGLReportItem> cRptItemList = new List<rcGLReportItem>();

            List<dcGLGroup> grpListFull = GLGroupBL.GetGLGroupList(prmLedger.CompanyID, dc);

            List<dcGLGroup> grpList = new List<dcGLGroup>();

            prmLedger.GLAccountTypeFilter = GLAccountTypeFilterEnum.NormalControlAccount;
            if (prmLedger.GLAccountID > 0)
            {
                dcGLAccount glAcc = GLAccountBL.GetGLAccountByID(prmLedger.CompanyID, prmLedger.GLAccountID, dc);
                prmLedger.GLGroupID = glAcc.GLGroupID;
                grpList = GLGroupBL.GetGLGroupListByGLGroupID(prmLedger.CompanyID, glAcc.GLGroupID, false, grpListFull, dc);

                if (glAcc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    prmLedger.GLAccountTypeFilter = GLAccountTypeFilterEnum.SubAccountByControl;
                }
                else
                {
                    prmLedger.GLAccountTypeFilter = GLAccountTypeFilterEnum.AllAccount;
                }
                
                prmLedger.GroupLedgerShowType = GroupsLedgerShowEnum.Ledgers;
            }
            else
            {
                if (prmLedger.GLGroupID > 0)
                {
                    grpList = GLGroupBL.GetGLGroupListByGLGroupID(prmLedger.CompanyID, prmLedger.GLGroupID, prmLedger.IncludeGroupChilds, grpListFull, dc);
                }
                else
                {
                    grpList = grpListFull;
                }
            }
                
          
            grpList = GLGroupBL.SetGLGroupListLevelCurrent(grpList);

            List<dcGLAccount> accBalance = GLAccountBL.GetAccountBalance(prmLedger, grpList, dc);
            List<dcGLGroup> grpBalance = GLGroupBL.GetGroupBalance(prmLedger, grpList, accBalance, dc);

            int defLevel = 0;


            List<rcGLReportItem> cRptListItems = new List<rcGLReportItem>();
            List<dcGLGroup> grpAccRootList = grpList.Where(c => c.GLGroupLevelCurrent == 0).ToList();

            GLReportRBL.AddGLReportItems(grpAccRootList, defLevel, prmLedger,
                                                         grpBalance, accBalance, cRptListItems);



            foreach (rcGLReportItem itm in cRptListItems)
            {
                ResetItemIndent(itm, prmLedger);
                ResetItemBalanceDisplay(itm, prmLedger);
                cRptItemList.Add(itm);
            }


            //summ
            decimal totOpenAmtDebit = 0;
            decimal totOpenAmtCredit = 0;

            decimal totOpenAmtDebitBalance = 0;
            decimal totOpenAmtCreditBalance = 0;

            decimal totTranAmtDebit = 0;
            decimal totTranAmtCredit = 0;

            decimal totTranAmtDebitBalance = 0;
            decimal totTranAmtCreditBalance = 0;

            decimal totCloseAmtDebit = 0;
            decimal totCloseAmtCredit = 0;


            decimal totCloseAmtDebitBalance = 0;
            decimal totCloseAmtCreditBalance = 0;


            foreach (dcGLGroup grpRoot in grpAccRootList)
            {
                totOpenAmtDebit += grpRoot.OpenDebitAmt;
                totOpenAmtCredit += grpRoot.OpenCreditAmt;

                totOpenAmtDebitBalance += grpRoot.OpenDebitBalanceAmt;
                totOpenAmtCreditBalance += grpRoot.OpenCreditBalanceAmt;

                totTranAmtDebit += grpRoot.DebitAmt;
                totTranAmtCredit += grpRoot.CreditAmt;

                totTranAmtDebitBalance += grpRoot.TranDebitBalanceAmt;
                totTranAmtCreditBalance += grpRoot.TranCreditBalanceAmt;

                totCloseAmtDebit += grpRoot.CloseDebitAmt;
                totCloseAmtCredit += grpRoot.CloseCreditAmt;

                totCloseAmtDebitBalance += grpRoot.CloseDebitBalanceAmt;
                totCloseAmtCreditBalance += grpRoot.CloseCreditBalanceAmt;


            }

            decimal totOpenAmt = totOpenAmtDebit - totOpenAmtCredit;
            decimal totTranAmt = totTranAmtDebit - totTranAmtCredit;
            decimal totCloseAmt = totCloseAmtDebit - totCloseAmtCredit;


            cRptHeader.OpenDebitAmt = totOpenAmtDebit;
            cRptHeader.OpenCreditAmt = totOpenAmtCredit;
            cRptHeader.OpenAmt = totOpenAmt;
            cRptHeader.OpenDebitBalanceAmt = totOpenAmtDebitBalance;
            cRptHeader.OpenCreditBalanceAmt = totOpenAmtCreditBalance;
            cRptHeader.OpenBalanceAmt = Math.Abs(cRptHeader.OpenAmt);
            cRptHeader.OpenBalanceDrCr = cRptHeader.OpenAmt > 0 ? 0 : 1;

            if (cRptHeader.OpenAmt != 0)
            {
                cRptHeader.OpenBalanceDrCrText = cRptHeader.OpenAmt > 0 ? "Dr" : "Cr";
            }


            cRptHeader.OpenBalanceAmt = Math.Abs(cRptHeader.OpenAmt);

            cRptHeader.TranDebitAmt = totTranAmtDebit;
            cRptHeader.TranCreditAmt = totTranAmtCredit;
            cRptHeader.TranAmt = totTranAmt;
            cRptHeader.TranDebitBalanceAmt = totTranAmtDebitBalance;
            cRptHeader.TranCreditBalanceAmt = totTranAmtCreditBalance;

            cRptHeader.TranBalanceAmt = Math.Abs(cRptHeader.TranAmt);
            cRptHeader.TranBalanceDrCr = cRptHeader.TranAmt > 0 ? 0 : 1;
            if (cRptHeader.TranAmt != 0)
            {
                cRptHeader.TranBalanceDrCrText = cRptHeader.TranAmt > 0 ? "Dr" : "Cr";
            }

            cRptHeader.CloseDebitAmt = totCloseAmtDebit;
            cRptHeader.CloseCreditAmt = totCloseAmtCredit;
            cRptHeader.CloseAmt = totCloseAmt;
            cRptHeader.CloseDebitBalanceAmt = totCloseAmtDebitBalance;
            cRptHeader.CloseCreditBalanceAmt = totCloseAmtCreditBalance;
            cRptHeader.CloseBalanceAmt = Math.Abs(cRptHeader.CloseAmt);
            cRptHeader.CloseBalanceDrCr = cRptHeader.CloseAmt > 0 ? 0 : 1;
            if (cRptHeader.CloseAmt != 0)
            {
                cRptHeader.CloseBalanceDrCrText = cRptHeader.CloseAmt > 0 ? "Dr" : "Cr";
            }


            cRpt.LedgerSummaryHeader.Add(cRptHeader);
            cRpt.LedgerSummaryItems = cRptItemList;

            //CalcSum(grpListCash, cRptHeader);

            cRptList.Add(cRpt);

            return cRptList;

        }

        public static void ResetItemIndent(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {
            if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            {
                if (prmLedger.IncludeGLClass)
                {
                    rptItem.ItemLevel = rptItem.ItemLevel > 1 ? 1 : 0;
                }
                else
                {
                    rptItem.ItemLevel = 0;
                }
            }

            if (prmLedger.IsIndentName)
            {
                if (prmLedger.IsExcelExport)
                {
                    rptItem.ItemNameIndent = string.Concat(ArrayList.Repeat(prmLedger.IndentPaddingCharExcel, rptItem.ItemLevel).ToArray()) + rptItem.ItemName;
                }
                else
                {
                    rptItem.ItemNameIndent = string.Concat(ArrayList.Repeat(prmLedger.IndentPaddingChar, rptItem.ItemLevel).ToArray()) + rptItem.ItemName;
                }
                rptItem.ItemNameDispaly = rptItem.ItemNameIndent;
            }

            else
            {
                rptItem.ItemNameIndent = rptItem.ItemName;
                rptItem.ItemNameDispaly = rptItem.ItemName;
            }
        }


        public static void ResetItemBalanceDisplay(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {


            if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {

                if (rptItem.ItemType != (int)GLReportItemTypeEnum.System
                         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLItem
                         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLClosing)
                {

                    if (rptItem.CloseBalanceDisplay != 0)
                    {
                        if (rptItem.ItemLevel == 0)
                        {
                            rptItem.IsBoldAmt = true;
                        }

                        if (rptItem.ItemLevel == 0)
                        {
                            rptItem.IsBoldAmt = true;
                        }

                        if (rptItem.ItemLevel == 1)
                        {
                            rptItem.IsBoldAmt = false;
                        }

                        if (rptItem.ItemLevel > 1)
                        {
                            //rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
                            //rptItem.CloseBalanceDisplay = 0;

                            if (rptItem.ChildItemCount > 0)
                            {
                                rptItem.IsBoldAmt = false;
                                rptItem.IsUnderlinedAmt = false;
                            }
                            else
                            {
                                rptItem.IsBoldAmt = false;
                                rptItem.IsUnderlinedAmt = false;
                            }

                        }

                        //if (rptItem.ItemLevel > 2)
                        //{
                        //    rptItem.IsBoldAmt = false;
                        //    rptItem.IsUnderlinedAmt = false;
                        //    rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
                        //    rptItem.CloseBalanceDisplay = 0;
                        //}
                    }


                }

            }




            //if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            //{
            //    if (rptItem.CloseBalanceDisplay == 0 | rptItem.ChildItemCount == 0)
            //    {
            //        return;
            //    }


            //    if (rptItem.ItemType != (int)GLReportItemTypeEnum.System
            //         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLItem
            //         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLClosing)
            //    {

            //        if (prmLedger.DisplayBalanceLevel == -1)
            //        {
            //            if (rptItem.ChildItemCount > 0)
            //            {
            //                rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
            //                rptItem.CloseBalanceDisplay = 0;
            //            }
            //        }
            //        else
            //        {
            //            if (prmLedger.DisplayBalanceLevel != rptItem.ItemLevel)
            //            {
            //                rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
            //                rptItem.CloseBalanceDisplay = 0;
            //            }
            //        }

            //    }

            //}

        }


    }
}
