using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class JournalDetBL
    {
        public static string GetJournalDetList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalDet.* ");
            sb.Append(" FROM tblJournalDet ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        //public static DataLoadOptions JournalDetLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}

        public static string GetJournalDetListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblJournalDet.* ");
            sb.Append(", tblJournal.JournalNo, tblJournal.JournalDate, tblJournal.IsPosted ");
            sb.Append(", tblJournal.JournalRefNo, tblJournal.JournalDesc, tblJournal.AccYearID ");
            sb.Append(", tblJournal.JournalTypeID, tblJournal.JournalAdjustTypeID, tblJournal.JournalIDAdjust ");
            sb.Append(", tblJournal.JournalUpdateNo, tblJournal.EditUserName ");
            sb.Append(", tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
            sb.Append(", tblGLAccount.GLGroupID ");
            sb.Append(", tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.BalanceType ");
            sb.Append(", tblGLGroup.GLGroupCode, tblGLGroup.GLGroupName, tblGLGroup.GLGroupNameShort, tblGLGroup.IsInstrument, tblGLGroup.IsCash ");
            sb.Append(", tblGLGroup.GLGroupClassID, tblGLGroupClass.GLGroupClassName,TBLLOCATION.LOCATIONNAME ");
            sb.Append(" FROM tblJournalDet ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

            sb.Append(" LEFT OUTER JOIN tblGLGroupClass ON tblGLGroupClass.GLGroupClassID = tblGLGroup.GLGroupClassID ");
            sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }


        public static List<dcJournalDet> GetJournalDetList(int pCompanyID, int pJournalID)
        {
            return GetJournalDetList(pCompanyID, pJournalID, null);
        }
        public static List<dcJournalDet> GetJournalDetList(int pCompanyID, int pJournalID, DBContext dc)
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(GetJournalDetListString());

            sb.Append(" AND tblJournal.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


            if (pJournalID > 0)
            {
                sb.Append(" AND tblJournalDet.JournalID=@journalID ");
                //cmd.Parameters.AddWithValue("@journalID", pJournalID);
                cmdInfo.DBParametersInfo.Add("@journalID", pJournalID);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            return GetJournalDetList(dbq, dc);
        }

        //TODO Change Monir
        public static List<dcJournalDet> GetJournalDetLocationList(int pCompanyID, int pJournalID,int pLocationID)
        {
            return GetJournalDetLocationList(pCompanyID, pJournalID,pLocationID, null);
        }
        public static List<dcJournalDet> GetJournalDetLocationList(int pCompanyID, int pJournalID,int pLocationID, DBContext dc)
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(GetJournalDetListString());

            sb.Append(" AND tblJournal.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


            if (pJournalID > 0)
            {
                sb.Append(" AND tblJournalDet.JournalID=@journalID ");
                //cmd.Parameters.AddWithValue("@journalID", pJournalID);
                cmdInfo.DBParametersInfo.Add("@journalID", pJournalID);
            }

            if (pLocationID > 0)
            {
                sb.Append(" AND tblJournal.LocationID=@LocationID ");
                //cmd.Parameters.AddWithValue("@journalID", pJournalID);
                cmdInfo.DBParametersInfo.Add("@LocationID", pLocationID);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            return GetJournalDetList(dbq, dc);
        }
        //END
        public static List<dcJournalDet> GetJournalDetList(DBQuery dbq)
        {
            return GetJournalDetList(dbq, null);
        }

        public static List<dcJournalDet> GetJournalDetList(DBQuery dbq, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcJournalDet>(dbq, dc);
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "PeriodStartDate";
                //    }
                //    cObjList = DBQuery.ExecuteDBQuery<dcJournalDet>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static dcJournalDet GetJournalDetListByID(int pJournalDetID)
        {
            return GetJournalDetListByID(pJournalDetID, null);
        }
        public static dcJournalDet GetJournalDetListByID(int pJournalDetID, DBContext dc)
        {
            dcJournalDet cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalDetList_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcJournalDet>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcJournalDet>()
                //                  where c.JournalDetID == pJournalDetID
                //                  select c).ToList();
                //    if (result.Count() > 0)
                //    {
                //        cObj = result.First();
                //    }
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }


        public static void SetTranTypeToDetList(List<dcJournalDet> pJrnlDetList, List<dcJournalDetRef> pJrnlDetRefList)
        {
            if (pJrnlDetList == null) return;
            if (pJrnlDetRefList == null) return;

            foreach (dcJournalDet det in pJrnlDetList)
            {
                dcJournalDetRef detRefTT = pJrnlDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranCode
                                                              && c.JournalDetID == det.JournalDetID
                                                              && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                if (detRefTT != null)
                {
                    det.TranTypeID = detRefTT.AccRefID;
                    det.TranTypeCode = detRefTT.AccRefCode;
                    det.TranTypeName = detRefTT.AccRefName;
                }
            }
        }


        public static void FillRefListToDetList(List<dcJournalDet> pJrnlDetList, List<dcJournalDetRef> pJrnlDetRefList, bool pUseDetIDLink)
        {
            foreach (dcJournalDet jDet in pJrnlDetList)
            {
                if (pUseDetIDLink)
                {
                    jDet.JournalDetRefList = pJrnlDetRefList.Where(c => c.JournalDetID_Link == jDet.JournalDetID_Link).ToList();
                }
                else
                {
                    jDet.JournalDetRefList = pJrnlDetRefList.Where(c => c.JournalDetID == jDet.JournalDetID).ToList();
                }
            }
        }

        public static void FillInsListToDetList(List<dcJournalDet> pJrnlDetList, List<dcJournalDetIns> pJrnlDetInsList, bool pUseDetIDLink)
        {
            foreach (dcJournalDet jDet in pJrnlDetList)
            {
                if (pUseDetIDLink)
                {
                    jDet.JournalDetInsList = pJrnlDetInsList.Where(c => c.JournalDetID_Link == jDet.JournalDetID_Link).ToList();
                }
                else
                {
                    jDet.JournalDetInsList = pJrnlDetInsList.Where(c => c.JournalDetID == jDet.JournalDetID).ToList();
                }
            }
        }


        public static void FillRefInsListToDetList(List<dcJournalDet> pJrnlDetList, List<dcJournalDetRef> pJrnlDetRefList, List<dcJournalDetIns> pJrnlDetInsList, bool pUseDetIDLink)
        {
            foreach (dcJournalDet jDet in pJrnlDetList)
            {
                if (pUseDetIDLink)
                {
                    jDet.JournalDetInsList = pJrnlDetInsList.Where(c => c.JournalDetID_Link == jDet.JournalDetID_Link).ToList();
                    jDet.JournalDetInsList = pJrnlDetInsList.Where(c => c.JournalDetID_Link == jDet.JournalDetID_Link).ToList();
                }
                else
                {
                    jDet.JournalDetInsList = pJrnlDetInsList.Where(c => c.JournalDetID == jDet.JournalDetID).ToList();
                    jDet.JournalDetInsList = pJrnlDetInsList.Where(c => c.JournalDetID == jDet.JournalDetID).ToList();
                }
            }
        }



        public static void SetDetListRefText(List<dcJournalDet> pJrnlDetList, bool pUseDetIDLink)
        {
            foreach (dcJournalDet det in pJrnlDetList)
            {

                #region TranCode
                dcJournalDetRef detRefTT = null;
                if (pUseDetIDLink)
                {
                    detRefTT = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranCode
                                                                                && c.JournalDetID_Link == det.JournalDetID_Link
                                                                                && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                }
                else
                {
                    detRefTT = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranCode
                                                                   && c.JournalDetID == det.JournalDetID
                                                                   && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                }

                if (detRefTT != null)
                {
                    det.TranTypeID = detRefTT.AccRefID;
                    det.TranTypeCode = detRefTT.AccRefCode;
                    det.TranTypeName = detRefTT.AccRefName;
                }
                #endregion

                //////////////////////

                #region costcenter
                int ccCount = 0;
                if (pUseDetIDLink)
                {
                    ccCount = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                                                                              && c.JournalDetID_Link == det.JournalDetID_Link
                                                                              && c._RecordState != RecordStateEnum.Deleted).Count();

                }
                else
                {
                    ccCount = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                                                                      && c.JournalDetID == det.JournalDetID
                                                                      && c._RecordState != RecordStateEnum.Deleted).Count();
                }
                
                string cText = ccCount.ToString() + " entry(s)";
                if (ccCount == 1)
                {
                    dcJournalDetRef detRefCC = null;
                    if (pUseDetIDLink)
                    {
                        detRefCC = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                                                                                 && c.JournalDetID_Link == det.JournalDetID_Link
                                                                                 && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                    }
                    else
                    {
                        detRefCC = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                                                           && c.JournalDetID == det.JournalDetID
                                                           && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
 
                    }
                    cText = detRefCC.AccRefCode;
                }
                det.CostCenterText = ccCount > 0 ? cText : string.Empty;
                #endregion


                #region reference
                int rfCount = 0;
                if (pUseDetIDLink)
                {
                    rfCount = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference
                                                                              && c.JournalDetID_Link == det.JournalDetID_Link
                                                                              && c._RecordState != RecordStateEnum.Deleted).Count();

                }
                else
                {
                    rfCount = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference
                                                                      && c.JournalDetID == det.JournalDetID
                                                                      && c._RecordState != RecordStateEnum.Deleted).Count();
                }

                string rText = rfCount.ToString() + " entry(s)";
                if (rfCount == 1)
                {
                    dcJournalDetRef detRefCC = null;
                    if (pUseDetIDLink)
                    {
                        detRefCC = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference
                                                                                 && c.JournalDetID_Link == det.JournalDetID_Link
                                                                                 && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                    }
                    else
                    {
                        detRefCC = det.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference
                                                           && c.JournalDetID == det.JournalDetID
                                                           && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();

                    }
                    rText = detRefCC.AccRefCode;
                }
                det.ReferenceText = rfCount > 0 ? rText : string.Empty;
                #endregion


            }
        }

        public static void SetDetListInsText(List<dcJournalDet> pJrnlDetList, bool pUseDetIDLink)
        {
            foreach (dcJournalDet det in pJrnlDetList)
            {
                ///instrument
                int insCount = 0;

                if (pUseDetIDLink)
                {
                    insCount = det.JournalDetInsList.Where(c => c.JournalDetID_Link == det.JournalDetID_Link
                                                                  && c._RecordState != RecordStateEnum.Deleted).Count();
                }
                else
                {
                    insCount = det.JournalDetInsList.Where(c => c.JournalDetID == det.JournalDetID
                                                                  && c._RecordState != RecordStateEnum.Deleted).Count();

                }

                det.InstrumentText = insCount > 0 ? insCount.ToString() + " entry(s)" : string.Empty;

            }
        }



        public static int Insert(dcJournalDet cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcJournalDet cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;

            cObj.TranAmt = cObj.DebitAmt - cObj.CreditAmt;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcJournalDet>(cObj, true);
                if (id > 0) { cObj.JournalDetID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcJournalDet cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcJournalDet cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            cObj.TranAmt = cObj.DebitAmt - cObj.CreditAmt;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcJournalDet>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pJournalDetID)
        {
            return Delete(pJournalDetID, null);
        }
        public static bool Delete(int pJournalDetID, DBContext dc)
        {
            dcJournalDet cObj = new dcJournalDet();
            cObj.JournalDetID = pJournalDetID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcJournalDet>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool IsDebitCreditBalanced(List<dcJournalDet> detList)
        {
            bool bStatus = false;
            if (detList == null)
            {
                return false;
            }
            decimal debitSum = detList.Where(c => c._RecordState != RecordStateEnum.Deleted).Sum(s => s.DebitAmt);
            decimal creditSum = detList.Where(c => c._RecordState != RecordStateEnum.Deleted).Sum(s => s.CreditAmt);

            if (debitSum == creditSum)
            {
                bStatus = true;
            }
            return bStatus;
        }


        public static int Save(dcJournalDet cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }
        public static int Save(dcJournalDet cObj, bool isAdd, DBContext dc)
        {
            int newID = 0;
            bool isDCInit = false;
            bool isTransInit = false;

            bool bStatus = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                using (DataContext dataContext = dc.NewDataContext())
                {
                    //cnt = dc.DoDelete<DBClass.PayRoll.dcSalaryDef>(cObj);

                    if (isAdd)
                    {
                        newID = Insert(cObj, dc);
                    }
                    else
                    {
                        if (Update(cObj, dc))
                        {
                            newID = cObj.JournalDetID;
                        }
                    }
                    //save details
                    if (newID > 0)
                    {
                        if (cObj.JournalDetRefList != null)
                        {
                            foreach (dcJournalDetRef detRef in cObj.JournalDetRefList)
                            {
                                detRef.JournalDetID = newID;
                            }
                            bStatus = JournalDetRefBL.SaveList(cObj.JournalDetRefList, dc);

                        }

                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                    }
                }
            }
            //catch
            {
                //dc.RollbackTransaction(isTransInit);
                //throw;
            }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return newID;
        }

        public static bool SaveList(List<dcJournalDet> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcJournalDet> detList, DBContext dc)
        {

            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcJournalDet oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case  RecordStateEnum.Added:
                        int newID = Insert(oDet, dc);
                        if (oDet.JournalDetRefList != null)
                        {
                            foreach (dcJournalDetRef detRef in oDet.JournalDetRefList)
                            {
                                detRef.JournalDetID = newID;
                            }
                            JournalDetRefBL.SaveList(oDet.JournalDetRefList,dc);
                        }
                        if (oDet.JournalDetInsList != null)
                        {
                            foreach (dcJournalDetIns detIns in oDet.JournalDetInsList)
                            {
                                detIns.JournalDetID = newID;
                            }
                            JournalDetInsBL.SaveList(oDet.JournalDetInsList,dc);
                        }


                        break;
                    case RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        if (oDet.JournalDetRefList != null)
                        {
                            foreach (dcJournalDetRef detRef in oDet.JournalDetRefList)
                            {
                                detRef.JournalDetID = oDet.JournalDetID;
                            }
                            JournalDetRefBL.SaveList(oDet.JournalDetRefList, dc);
                        }
                        

                        if (oDet.JournalDetInsList != null)
                        {
                            foreach (dcJournalDetIns detIns in oDet.JournalDetInsList)
                            {
                                detIns.JournalDetID = oDet.JournalDetID;
                            }
                            JournalDetInsBL.SaveList(oDet.JournalDetInsList,dc);
                        }
                        break;
                    case RecordStateEnum.Deleted:
                        if (oDet.JournalDetRefList != null)
                        {
                            foreach (dcJournalDetRef detRef in oDet.JournalDetRefList)
                            {
                                JournalDetRefBL.Delete(detRef.JournalDetRefID, dc);
                            }
                        }
                        

                        if (oDet.JournalDetInsList != null)
                        {
                            foreach (dcJournalDetIns detIns in oDet.JournalDetInsList)
                            {
                                JournalDetInsBL.Delete(detIns.JournalDetInsID,dc);
                            }
                        }


                        bool d = Delete(oDet.JournalDetID, dc);
                        break;
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }

        public static void UpdateSLNo(List<dcJournalDet> pListDetails)
        {
            UpdateSLNo(pListDetails, false);
        }

        public static void UpdateSLNo(List<dcJournalDet> pListDetails, bool pIsByDebitCredit)
        {
            int slNoDr = 0;
            int slNoCr = 0;

            int slNo = 0;
            foreach (dcJournalDet oDet in pListDetails)
            {
                if (oDet._RecordState != RecordStateEnum.Deleted)
                {
                    if (pIsByDebitCredit)
                    {
                        if (oDet.DebitCredit == DebitCreditEnum.Debit)
                        {
                            slNoDr++;
                            oDet.JournalDetSLNo = slNoDr;
                        }

                        if (oDet.DebitCredit == DebitCreditEnum.Credit)
                        {
                            slNoCr++;
                            oDet.JournalDetSLNo = slNoCr;
                        }
                    }
                    else
                    {
                        slNo++;
                        oDet.JournalDetSLNo = slNo;
                    }
                }
            }
        }

        public static Boolean IsJournalDetRowValid(dcJournalDet cObj)
        {
            bool isValid = false;

            if (cObj.GLAccountID > 0)
            {
                isValid = true;
            }
            return isValid;
        }


        public static List<dcJournalDet> ValidateJournalDetList(List<dcJournalDet> pJournalDetList)
        {
            List<int> delIndex = new List<int>();
            List<dcJournalDet> newList = new List<dcJournalDet>();
            foreach (dcJournalDet cObj in pJournalDetList)
            {
                int detID = cObj.JournalDetID;
                if (cObj._RecordState == RecordStateEnum.Deleted)
                {
                    if (detID > 0)
                    {
                        newList.Add(cObj);
                    }
                }
                else
                {
                    bool isRowValid = IsJournalDetRowValid(cObj);
                    if (isRowValid)
                    {
                        if (detID > 0)
                        {
                            //edited
                            cObj._RecordState = RecordStateEnum.Edited;
                            //dRow["RState"] = "edited";
                        }
                        else
                        {
                            //added
                            cObj._RecordState = RecordStateEnum.Added;
                            //dRow["RState"] = "added";
                        }

                        //dtUpdate.ImportRow(dRow);
                        newList.Add(cObj);
                    }
                    else
                    {
                        if (detID > 0)
                        {
                            cObj._RecordState = RecordStateEnum.Deleted;
                            newList.Add(cObj);
                            //dRow["RState"] = "deleted";
                            //dtUpdate.ImportRow(dRow);
                        }
                        else
                        {
                            //now use less
                            delIndex.Add(pJournalDetList.IndexOf(cObj));
                            //delIndex.Add(dtToValid.Rows.IndexOf(dRow));
                        }
                    }
                }

            }

            return newList;
        }



        public static List<dcJournalDet> GetJournalDetByDate(clsPrmLedger prmLedger)
        {
            return GetJournalDetByDate(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
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
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent,TBLLOCATION.LOCATIONNAME ");
               

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

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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

                if (prmLedger.IsLocation)
                {
                    string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                    sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
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


                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_Normal(clsPrmLedger prmLedger)
        {
            return GetJournalDetByDate_Normal(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_Normal(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
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
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent,TBLLOCATION.LOCATIONNAME ");


                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
                sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");
                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                if (prmLedger.JournalID > 0)
                {
                    sb.Append(" AND tblJournal.JournalID=@JournalID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@JournalID", prmLedger.JournalID);
                }


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.NormalAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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

                if(prmLedger.IsLocation)
                { 
                string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
                }

                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_Control(clsPrmLedger prmLedger)
        {
            return GetJournalDetByDate_Control(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_Control(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
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
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");


                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
                    }
                }

                switch (prmLedger.IncludePostType)
                {
                    case IncludePostEnum.Posted:
                        sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", true);
                        break;
                    case IncludePostEnum.Unposted:
                        sb.Append(" AND (tblJournal.IsPosted =@isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", false);
                        break;
                    case IncludePostEnum.All:
                        break;
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

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_ControlSum(clsPrmLedger prmLedger)
        {
            return GetJournalDetByDate_ControlSum(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_ControlSum(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //SqlCommand cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();
                
                sb.Append(" SELECT tblJournal.JournalID, tblJournal.JournalNo, tblJournal.JournalDate, tblJournal.JournalDesc, tblJournal.IsPosted ");
                sb.Append(" ,tblJournal.CompanyID, tblJournal.AccYearID ");
                sb.Append(" ,tblJournal.JournalAdjustTypeID ");
                sb.Append(" ,tblJournal.JournalTypeID , tblJournalType.JournalTypeCode,tblJournalType.JournalTypeName ");
                sb.Append(" ,JournalDetSum.GLAccountIDParent AS  GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" ,tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");   
                sb.Append(" ,JournalDetSum.DebitAmt, JournalDetSum.CreditAmt, JournalDetSum.DebitAmt - JournalDetSum.CreditAmt AS TranAmt ");
                sb.Append(" ,JournalDetSum.TranCount ");
                sb.Append(" FROM tblJournal ");
                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" , SUM(tblJournalDet.CreditAmt) AS CreditAmt, SUM(tblJournalDet.DebitAmt) AS DebitAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");
                sb.Append(" WHERE (1=1) "); 
                //WHERE     (Accounting.tblGLAccount.GLAccountIDParent = 2)
               
                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);


                //sb.Append(" AND tblGLAccount.GLAccountIDParent > 0 ");
                ////if (prmLedger.GLAccountID > 0)
                //{
                    
                //    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                //}


                //filter by account parent
                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
                    }
                }

                switch (prmLedger.IncludePostType)
                {
                    case IncludePostEnum.Posted:
                        sb.Append(" AND (tblJournal.IsPosted =@isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", true);
                        break;
                    case IncludePostEnum.Unposted:
                        sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", false);
                        break;
                    case IncludePostEnum.All:
                        break;
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

                if(prmLedger.IsLocation)
                {
                    string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                    sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
                }
               


                sb.Append(" GROUP BY tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" ) JournalDetSum ON tblJournal.JournalID = JournalDetSum.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON JournalDetSum.GLAccountIDParent = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
                
                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_Sub(clsPrmLedger prmLedger)
        {
            return GetJournalDetByDate_Sub(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_Sub(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDet.* ");

                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");


                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
                sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");
                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int) GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                        }
                    }
                }

                switch (prmLedger.IncludePostType)
                {
                    case IncludePostEnum.Posted:
                        sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", true);
                        break;
                    case IncludePostEnum.Unposted:
                        sb.Append(" AND (tblJournal.IsPosted =@isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", false);
                        break;
                    case IncludePostEnum.All:
                        break;
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

                if (prmLedger.IsLocation)
                {
                    string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                    sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
                }

                 sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                 DBQuery dbq = new DBQuery();
                 dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                 cmdInfo.CommandText = sb.ToString();
                 cmdInfo.CommandType = CommandType.Text;
                 dbq.DBCommandInfo = cmdInfo;

                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_SubByControl(clsPrmLedger prmLedger)
        {
            return GetJournalDetByDate_SubByControl(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_SubByControl(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDet.* ");

                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");


                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
                    }
                }

                switch (prmLedger.IncludePostType)
                {
                    case IncludePostEnum.Posted:
                        sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", true);
                        break;
                    case IncludePostEnum.Unposted:
                        sb.Append(" AND (tblJournal.IsPosted =@isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", false);
                        break;
                    case IncludePostEnum.All:
                        break;
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

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcJournalDet> GetJournalDetListContra_NormalForNormal(clsPrmLedger prmLedger)
        {
            return GetJournalDetListContra_NormalForNormal(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetListContra_NormalForNormal(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDet.* ");

                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append(" , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");

                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN ( " );
                sb.Append(" SELECT DISTINCT tblJournalDet.JournalID FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");


                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.NormalAccount);


                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        }
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

                sb.Append(" ) JR ON tblJournalDet.JournalID = JR.JournalID ");

                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID2 ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID2", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID2", (int)GLAccountTypeEnum.NormalAccount);

                sb.Append(" AND tblJournalDet.GLAccountID<>@glAccountID2 ");
                //cmd.Parameters.AddWithValue("@glAccountID2", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID2", prmLedger.GLAccountID);


                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetListContra_NormalForControl(clsPrmLedger prmLedger)
        {
            return GetJournalDetListContra_NormalForControl(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetListContra_NormalForControl(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //SqlCommand cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDet.* ");

                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append(" , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");

                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN ( ");
                sb.Append(" SELECT DISTINCT tblJournalDet.JournalID FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");


                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                //sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.NormalAccount);


                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        }
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

                sb.Append(" ) AS JR ON tblJournalDet.JournalID = JR.JournalID ");

                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID2 ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID2", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID2", (int)GLAccountTypeEnum.NormalAccount);

                sb.Append(" AND tblJournalDet.GLAccountID<>@glAccountID2 ");
                //cmd.Parameters.AddWithValue("@glAccountID2", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID2", prmLedger.GLAccountID);

                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetListContra_NormalForSub(clsPrmLedger prmLedger)
        {
            return GetJournalDetListContra_NormalForSub(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetListContra_NormalForSub(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDet.* ");

                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append(" , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");

                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN ( ");
                sb.Append(" SELECT DISTINCT tblJournalDet.JournalID FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");


                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);


                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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

                sb.Append(" ) AS JR ON tblJournalDet.JournalID = JR.JournalID ");

                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID2 ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID2", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID2", (int)GLAccountTypeEnum.NormalAccount);

                sb.Append(" AND tblJournalDet.GLAccountID<>@glAccountID2 ");
                //cmd.Parameters.AddWithValue("@glAccountID2", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID2", prmLedger.GLAccountID);

                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetListContra_SubForControl(clsPrmLedger prmLedger)
        {
            return GetJournalDetListContra_SubForControl(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetListContra_SubForControl(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDet.* ");

                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append(" , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");

                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN ( ");
                sb.Append(" SELECT tblJournalDet.JournalID FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");


                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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

                if (prmLedger.JournalAdjustTypeID == 0)
                {
                    //do nothing
                }
                else
                {
                    if (prmLedger.JournalAdjustTypeID > 0)
                    {
                        sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                        cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                        //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    }
                    else
                    {

                    }
                }

                sb.Append(" ) AS JR ON tblJournalDet.JournalID = JR.JournalID ");

                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.ControlAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.ControlAccount);

                sb.Append(" AND tblJournalDet.GLAccountID<>@glAccountID ");
                //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);

                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetListContra_ControlSumForNormal(clsPrmLedger prmLedger)
        {
            return GetJournalDetListContra_ControlSumForNormal(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetListContra_ControlSumForNormal(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT tblJournal.JournalID, tblJournal.JournalNo, tblJournal.JournalDate, tblJournal.JournalDesc, tblJournal.IsPosted ");
                sb.Append(" ,tblJournal.CompanyID, tblJournal.AccYearID ");
                sb.Append(" ,tblJournal.JournalAdjustTypeID ");
                sb.Append(" ,tblJournal.JournalTypeID , tblJournalType.JournalTypeCode,tblJournalType.JournalTypeName ");
                sb.Append(" ,JournalDetSum.GLAccountIDParent AS  GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" ,tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" ,JournalDetSum.DebitAmt, JournalDetSum.CreditAmt, JournalDetSum.DebitAmt - JournalDetSum.CreditAmt AS TranAmt ");
                sb.Append(" ,JournalDetSum.TranCount ");
                sb.Append(" FROM tblJournal ");
                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" , SUM(tblJournalDet.CreditAmt) AS CreditAmt, SUM(tblJournalDet.DebitAmt) AS DebitAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN ( SELECT DISTINCT tblJournalDet.JournalID ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
                
                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblJournalDet.GLAccountID = @glAccountID");
                //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        }
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
                sb.Append(" ) JR ON tblJournalDet.JournalID = JR.JournalID ");


                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);

                sb.Append(" GROUP BY tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" ) JournalDetSum ON tblJournal.JournalID = JournalDetSum.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON JournalDetSum.GLAccountIDParent = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID2 ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID2", (int)GLAccountTypeEnum.ControlAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID2", (int)GLAccountTypeEnum.ControlAccount);

                sb.Append(" AND tblGLAccount.GLAccountID<>@glAccountID2 ");
                //cmd.Parameters.AddWithValue("@glAccountID2", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID2", prmLedger.GLAccountID);


                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetListContra_ControlSumForControl(clsPrmLedger prmLedger)
        {
            return GetJournalDetListContra_ControlSumForControl(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetListContra_ControlSumForControl(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT tblJournal.JournalID, tblJournal.JournalNo, tblJournal.JournalDate, tblJournal.JournalDesc, tblJournal.IsPosted ");
                sb.Append(" ,tblJournal.CompanyID, tblJournal.AccYearID ");
                sb.Append(" ,tblJournal.JournalAdjustTypeID ");
                sb.Append(" ,tblJournal.JournalTypeID , tblJournalType.JournalTypeCode,tblJournalType.JournalTypeName ");
                sb.Append(" ,JournalDetSum.GLAccountIDParent AS  GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" ,tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" ,JournalDetSum.DebitAmt, JournalDetSum.CreditAmt, JournalDetSum.DebitAmt - JournalDetSum.CreditAmt AS TranAmt ");
                sb.Append(" ,JournalDetSum.TranCount ");
                sb.Append(" FROM tblJournal ");
                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" , SUM(tblJournalDet.CreditAmt) AS CreditAmt, SUM(tblJournalDet.DebitAmt) AS DebitAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN ( SELECT DISTINCT tblJournalDet.JournalID ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
                }

                sb.Append(" AND tblGLAccount.GLAccountIDParent = @glAccountID");
                //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        }
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
                sb.Append(" ) As JR ON tblJournalDet.JournalID = JR.JournalID ");

                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);

                sb.Append(" GROUP BY tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" ) As JournalDetSum ON tblJournal.JournalID = JournalDetSum.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON JournalDetSum.GLAccountIDParent = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID2 ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID2", (int)GLAccountTypeEnum.ControlAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID2", (int)GLAccountTypeEnum.ControlAccount);

                sb.Append(" AND tblGLAccount.GLAccountIDParent<>@glAccountID2 ");
                //cmd.Parameters.AddWithValue("@glAccountID2", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID2", prmLedger.GLAccountID);

                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetListContra_ControlSumForSub(clsPrmLedger prmLedger)
        {
            return GetJournalDetListContra_ControlSumForSub(prmLedger, null);
        }
        public static List<dcJournalDet> GetJournalDetListContra_ControlSumForSub(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT tblJournal.JournalID, tblJournal.JournalNo, tblJournal.JournalDate, tblJournal.JournalDesc, tblJournal.IsPosted ");
                sb.Append(" ,tblJournal.CompanyID, tblJournal.AccYearID ");
                sb.Append(" ,tblJournal.JournalAdjustTypeID ");
                sb.Append(" ,tblJournal.JournalTypeID , tblJournalType.JournalTypeCode,tblJournalType.JournalTypeName ");
                sb.Append(" ,JournalDetSum.GLAccountIDParent AS  GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" ,tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" ,JournalDetSum.DebitAmt, JournalDetSum.CreditAmt, JournalDetSum.DebitAmt - JournalDetSum.CreditAmt AS TranAmt ");
                sb.Append(" ,JournalDetSum.TranCount ");
                sb.Append(" FROM tblJournal ");
                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" , SUM(tblJournalDet.CreditAmt) AS CreditAmt, SUM(tblJournalDet.DebitAmt) AS DebitAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN ( SELECT DISTINCT tblJournalDet.JournalID ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);

                sb.Append(" AND tblJournalDet.GLAccountID = @glAccountID");
                //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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
                sb.Append(" ) JR ON tblJournalDet.JournalID = JR.JournalID ");

                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID2 ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID2", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID2", (int)GLAccountTypeEnum.SubAccount);

                sb.Append(" AND tblGLAccount.GLAccountID<>@glAccountID2 ");
                //cmd.Parameters.AddWithValue("@glAccountID2", prmLedger.GLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountID2", prmLedger.GLAccountID);


                sb.Append(" GROUP BY tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" ) JournalDetSum ON tblJournal.JournalID = JournalDetSum.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON JournalDetSum.GLAccountIDParent = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID3 ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID3", (int)GLAccountTypeEnum.ControlAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID3", (int)GLAccountTypeEnum.ControlAccount);


                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcJournalDet> GetJournalDetByDate_Cash(clsPrmLedger prmLedger, CashTranOption cashTranOpt)
        {
            return GetJournalDetByDate_Cash(prmLedger, cashTranOpt, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_Cash(clsPrmLedger prmLedger, CashTranOption cashTranOpt, DBContext dc)
        {
            List<dcJournalDet> cObjList = GetJournalDetByDate_CashNormal(prmLedger, cashTranOpt, dc);
            cObjList.AddRange(GetJournalDetByDate_CashControl(prmLedger, cashTranOpt, dc)); 
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_CashNormal(clsPrmLedger prmLedger,CashTranOption cashTranOpt)
        {
            return GetJournalDetByDate_CashNormal(prmLedger, cashTranOpt, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_CashNormal(clsPrmLedger prmLedger, CashTranOption cashTranOpt, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //SqlCommand cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT tblJournalDet.GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
                sb.Append(" FROM tblJournalDet ");

                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID ");
                sb.Append(" FROM tblJournalDet ");

                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");
                //WHERE     (Accounting.tblGLAccount.GLAccountIDParent = 2)

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);


                switch (cashTranOpt)
                {
                    case CashTranOption.CashTran:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCash ) ");
                        cmdInfo.DBParametersInfo.Add("@isCash", false);
                        break;
                    case CashTranOption.CashTranContra:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCash) ");
                        cmdInfo.DBParametersInfo.Add("@isCash", true);
                        break;
                }


                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                ////filter by account parent
                //if (prmLedger.GLAccountID > 0)
                //{
                //    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                //    cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                //}


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);

                ////filter by account parent
                //if (prmLedger.g > 0)
                //{
                    
                //}


                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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

                if (prmLedger.JournalAdjustTypeID == 0)
                {
                    //do nothing
                }
                else
                {
                    if (prmLedger.JournalAdjustTypeID > 0)
                    {
                        sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                        cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                        //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    }
                    else
                    {

                    }
                }

                //TODO Change as
                sb.Append(" GROUP BY tblJournalDet.JournalID ");
                sb.Append(" )  JournalCash ON tblJournalDet.JournalID = JournalCash.JournalID ");
               
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

                sb.Append(" WHERE   (1 = 1) ");

                switch (cashTranOpt)
                {
                    case CashTranOption.CashTran:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCashTrn ) ");
                        cmdInfo.DBParametersInfo.Add("@isCashTrn", true);
                        break;
                    case CashTranOption.CashTranContra:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCashTrn) ");
                        cmdInfo.DBParametersInfo.Add("@isCashTrn", false);
                        break;
                }

                sb.Append(" GROUP BY  tblJournalDet.GLAccountID ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_CashSub(clsPrmLedger prmLedger, CashTranOption cashTranOpt)
        {
            return GetJournalDetByDate_CashSub(prmLedger, cashTranOpt, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_CashSub(clsPrmLedger prmLedger, CashTranOption cashTranOpt, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT tblJournalDet.GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
                sb.Append(" FROM tblJournalDet ");

                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID ");
                sb.Append(" FROM tblJournalDet ");

                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");
                //WHERE     (Accounting.tblGLAccount.GLAccountIDParent = 2)

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);


                switch (cashTranOpt)
                {
                    case CashTranOption.CashTran:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCash ) ");
                        cmdInfo.DBParametersInfo.Add("@isCash", false);
                        break;
                    case CashTranOption.CashTranContra:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCash) ");
                        cmdInfo.DBParametersInfo.Add("@isCash", true);
                        break;
                }


                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                ////filter by account parent
                //if (prmLedger.GLAccountID > 0)
                //{
                //    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                //    cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                //}


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);

                ////filter by account parent
                //if (prmLedger.g > 0)
                //{

                //}


                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        }
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


                sb.Append(" GROUP BY tblJournalDet.JournalID ");
                sb.Append(" ) JournalCash ON tblJournalDet.JournalID = JournalCash.JournalID ");

                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

                sb.Append(" WHERE   (1 = 1) ");

                switch (cashTranOpt)
                {
                    case CashTranOption.CashTran:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCashTrn ) ");
                        cmdInfo.DBParametersInfo.Add("@isCashTrn", true);
                        break;
                    case CashTranOption.CashTranContra:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCashTrn) ");
                        cmdInfo.DBParametersInfo.Add("@isCashTrn", false);
                        break;
                }

                sb.Append(" GROUP BY  tblJournalDet.GLAccountID ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetByDate_CashControl(clsPrmLedger prmLedger, CashTranOption cashTranOpt)
        {
            return GetJournalDetByDate_CashControl(prmLedger, cashTranOpt, null);
        }
        public static List<dcJournalDet> GetJournalDetByDate_CashControl(clsPrmLedger prmLedger, CashTranOption cashTranOpt, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT tblGLAccount.GLAccountIDParent AS GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
                sb.Append(" FROM tblJournalDet ");

                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID ");
                sb.Append(" FROM tblJournalDet ");

                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");
                //WHERE     (Accounting.tblGLAccount.GLAccountIDParent = 2)

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);


                switch (cashTranOpt)
                {
                    case CashTranOption.CashTran:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCash ) ");
                        cmdInfo.DBParametersInfo.Add("@isCash", false);
                        break;
                    case CashTranOption.CashTranContra:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCash) ");
                        cmdInfo.DBParametersInfo.Add("@isCash", true);
                        break;
                }


                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                ////filter by account parent
                //if (prmLedger.GLAccountID > 0)
                //{
                //    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                //    cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                //}


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);

                ////filter by account parent
                //if (prmLedger.g > 0)
                //{

                //}


                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        }
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


                sb.Append(" GROUP BY tblJournalDet.JournalID ");
                sb.Append(" )  JournalCash ON tblJournalDet.JournalID = JournalCash.JournalID ");

                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

                sb.Append(" WHERE   (1 = 1) ");

                switch (cashTranOpt)
                {
                    case CashTranOption.CashTran:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCashTrn ) ");
                        cmdInfo.DBParametersInfo.Add("@isCashTrn", true);
                        break;
                    case CashTranOption.CashTranContra:
                        sb.Append(" AND (tblGLGroup.IsCash= @isCashTrn) ");
                        cmdInfo.DBParametersInfo.Add("@isCashTrn", false);
                        break;
                }

                sb.Append(" GROUP BY tblGLAccount.GLAccountIDParent ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetSumByDate(clsPrmLedger prmLedger)
        {
            return GetJournalDetSumByDate(prmLedger, null, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate(clsPrmLedger prmLedger, DBContext dc)
        {
            return GetJournalDetSumByDate(prmLedger, null, dc);
        }

        //public static List<dcJournalDet> GetJournalDetSumByDate(clsPrmLedger prmLedger, List<dcGLAccount> pGLAccList)
        //{
        //    return GetJournalDetSumByDate(prmLedger, pGLAccList, null);
        //}
        //public static List<dcJournalDet> GetJournalDetSumByDate(clsPrmLedger prmLedger, List<dcGLAccount> pGLAccList, DBContext dc)
        //{
        //    List<dcJournalDet> cObjList = new List<dcJournalDet>();

        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        //        SqlCommand cmd = new SqlCommand();
        //        StringBuilder sb = new StringBuilder();

        //        sb.Append("SELECT tblJournalDet.GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
        //        sb.Append(" , SUM(tblJournalDet.TranAmt) AS TranAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount  ");

        //        sb.Append(" FROM tblJournalDet INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
        //        sb.Append(" WHERE (1=1) ");

        //        sb.Append(" AND tblJournal.CompanyID=@companyID ");
        //        cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                
        //        if (prmLedger.AccYearID > 0)
        //        {
        //            sb.Append(" AND tblJournal.AccYearID=@accYearID ");
        //            cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
        //        }

        //        if (prmLedger.GLAccountID > 0)
        //        {
        //            sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
        //            cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
        //        }

        //        if (pGLAccList != null)
        //        {
        //            string strAccList = string.Empty;
        //            string comma = "";

        //            foreach (dcGLAccount acc in pGLAccList)
        //            {
        //                strAccList += comma + acc.GLAccountID;
        //                comma = ",";
        //            }

        //            if (strAccList != string.Empty)
        //            {
        //                sb.Append(string.Format(" AND tblJournalDet.GLAccountID IN ({0}) ", strAccList));

        //                //sb.Append(" AND tblJournalDet.GLAccountID IN (@accidlist) ");
        //                //cmd.Parameters.AddWithValue("@accidlist", strAccList);
        //            }
        //        }


        //        if (prmLedger.FromDate.HasValue)
        //        {
        //            if (prmLedger.IsBeforeDate)
        //            {
        //                sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
        //                cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
        //            }
        //            else
        //            {
        //                if (prmLedger.ToDate.HasValue)
        //                {
        //                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
        //                    cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
        //                    cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
        //                }
        //                else
        //                {
        //                    sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
        //                    cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
        //                }
        //            }
        //        }
        //        switch (prmLedger.IncludePostType)
        //        {
        //            case IncludePostEnum.Posted:
        //                sb.Append(" AND (tblJournal.IsPosted = 1) ");
        //                break;
        //            case IncludePostEnum.Unposted:
        //                sb.Append(" AND (tblJournal.IsPosted = 0) ");
        //                break;
        //            case IncludePostEnum.All:
        //                break;
        //        }


        //        if (prmLedger.JournalAdjustTypeID == 0)
        //        {
        //            //do nothing
        //        }
        //        else
        //        {
        //            if (prmLedger.JournalAdjustTypeID > 0)
        //            {
        //                sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
        //                cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
        //            }
        //            else
        //            {

        //            }
        //        }



        //        sb.Append(" GROUP BY tblJournalDet.GLAccountID ");
        //        // sb.Append(" ORDER BY tblAccJournal.AccJournalDate ");

        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = sb.ToString();

        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
        //        dbq.DBCommand = cmd;

        //        cObjList = GetJournalDetList(dbq, dc);
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObjList;
        //}

        public static List<dcJournalDet> GetJournalDetSumByDate(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetJournalDetSumByDate(prmLedger, pGLGroupList, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();
            switch (prmLedger.GLAccountTypeFilter)
            {
                case GLAccountTypeFilterEnum.NoFilter:
                case GLAccountTypeFilterEnum.AllAccount:
                    cObjList = GetJournalDetSumByDate_ALL(prmLedger, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.NormalAccount:
                    cObjList = GetJournalDetSumByDate_Normal(prmLedger, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.ControlAccount:
                    cObjList = GetJournalDetSumByDate_Control(prmLedger, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.SubAccount:
                    cObjList = GetJournalDetSumByDate_Sub(prmLedger, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.NormalControlAccount:
                    cObjList = GetJournalDetSumByDate_NormalControl(prmLedger, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.NormalSubAccount:
                    cObjList = GetJournalDetSumByDate_Normal(prmLedger, pGLGroupList, dc);
                    cObjList.AddRange(GetJournalDetSumByDate_Sub(prmLedger, pGLGroupList, dc));
                    break;
                case GLAccountTypeFilterEnum.ControlSubAccount:
                    cObjList = GetJournalDetSumByDate_Control(prmLedger, pGLGroupList, dc);
                    cObjList.AddRange(GetJournalDetSumByDate_Sub(prmLedger, pGLGroupList, dc));
                    break;
                case GLAccountTypeFilterEnum.SubAccountByControl:
                    cObjList = GetJournalDetSumByDate_SubByControl(prmLedger, pGLGroupList, dc);
                    break;
            }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetSumByDate_Normal(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetJournalDetSumByDate_Normal(prmLedger, pGLGroupList, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate_Normal(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT tblJournalDet.GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
                sb.Append(" , SUM(tblJournalDet.TranAmt) AS TranAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount  ");

                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN  tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);


                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                }

                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }


                if (prmLedger.JournalID > 0)
                {
                    sb.Append(" AND tblJournal.JournalID=@journalID ");
                    //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                    cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        }
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

                if(prmLedger.IsLocation)
                {
                    string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                    sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
                }
               


                sb.Append(" GROUP BY tblJournalDet.GLAccountID ");
                // sb.Append(" ORDER BY tblAccJournal.AccJournalDate ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetSumByDate_Sub(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetJournalDetSumByDate_Sub(prmLedger, pGLGroupList, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate_Sub(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT tblJournalDet.GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
                sb.Append(" , SUM(tblJournalDet.TranAmt) AS TranAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount  ");

                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN  tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);


                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }

                if (prmLedger.JournalID > 0)
                {
                    sb.Append(" AND tblJournal.JournalID=@journalID ");
                    //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                    cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
                }


                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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



                sb.Append(" GROUP BY tblJournalDet.GLAccountID ");
                // sb.Append(" ORDER BY tblAccJournal.AccJournalDate ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetSumByDate_SubByControl(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetJournalDetSumByDate_SubByControl(prmLedger, pGLGroupList, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate_SubByControl(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT tblJournalDet.GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
                sb.Append(" , SUM(tblJournalDet.TranAmt) AS TranAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount  ");

                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN  tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" WHERE (1=1) ");

                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);


                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }

                if (prmLedger.JournalID > 0)
                {
                    sb.Append(" AND tblJournal.JournalID=@journalID ");
                    //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                    cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
                }


                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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


                if (prmLedger.JournalAdjustTypeID == 0)
                {
                    //do nothing
                }
                else
                {
                    if (prmLedger.JournalAdjustTypeID > 0)
                    {
                        sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                        cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                        //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    }
                    else
                    {

                    }
                }



                sb.Append(" GROUP BY tblJournalDet.GLAccountID ");
                // sb.Append(" ORDER BY tblAccJournal.AccJournalDate ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetSumByDate_Control(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetJournalDetSumByDate_Control(prmLedger, pGLGroupList, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate_Control(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                StringBuilder sbSelect = new StringBuilder();
                StringBuilder sbFrom = new StringBuilder();
                StringBuilder sbWhere = new StringBuilder();
                StringBuilder sbGroupBy = new StringBuilder();
                StringBuilder sbOrderBy = new StringBuilder();

                sbSelect.Append("SELECT  tblGLAccount.GLAccountIDParent AS GLAccountID, SUM(tblJournalDet.DebitAmt) AS DebitAmt, SUM(tblJournalDet.CreditAmt) AS CreditAmt ");
                sbSelect.Append(" , SUM(tblJournalDet.DebitAmt) - SUM(tblJournalDet.CreditAmt) AS TranAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount  ");

                sbFrom.Append(" FROM tblJournalDet ");
                sbFrom.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sbFrom.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");

                sbWhere.Append(" WHERE (1=1) ");

                sbWhere.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                sbWhere.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);

                //if (prmLedger.AccYearID > 0)
                //{
                //    sbWhere.Append(" AND tblJournal.AccYearID=@accYearID ");
                //    cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                //}

                sbWhere.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sbWhere.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }


                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sbWhere.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }

                if (prmLedger.JournalID > 0)
                {
                    sbWhere.Append(" AND tblJournal.JournalID=@journalID ");
                    //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                    cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sbWhere.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
                    {
                        if (prmLedger.ToDate.HasValue)
                        {
                            sbWhere.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                        }
                        else
                        {
                            sbWhere.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
                    }
                }

                switch (prmLedger.IncludePostType)
                {
                    case IncludePostEnum.Posted:
                        sbWhere.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", true);
                        break;
                    case IncludePostEnum.Unposted:
                        sbWhere.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", false);
                        break;
                    case IncludePostEnum.All:
                        break;
                }

                if (prmLedger.JournalAdjustTypeID == 0)
                {
                    //do nothing
                }
                else
                {
                    if (prmLedger.JournalAdjustTypeID > 0)
                    {
                        sbWhere.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                        cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    }
                    else
                    {

                    }
                }

                sbGroupBy.Append(" GROUP BY tblGLAccount.GLAccountIDParent ");
                                
                if (prmLedger.GroupByJournal)
                {
                    sbSelect.Append(", tblJournalDet.JournalID ");

                    sbGroupBy.Append(", tblJournalDet.JournalID ");
                }

                //if (prmLedger.GroupByTranType)
                //{
                //    sbSelect.Append(", tblJournalDet.GLTranTypeID ");

                //    sbGroupBy.Append(", tblJournalDet.GLTranTypeID ");
                //}

                
                // sb.Append(" ORDER BY tblAccJournal.AccJournalDate ");

                sb.Append(sbSelect.ToString());
                sb.Append(sbFrom.ToString());
                sb.Append(sbWhere.ToString());
                sb.Append(sbGroupBy.ToString());
                sb.Append(sbOrderBy.ToString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();
                //DBQuery dbq = new DBQuery();

                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetSumByDate_NormalControl(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetJournalDetSumByDate_NormalControl(prmLedger, pGLGroupList, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate_NormalControl(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcJournalDet> cObjList = GetJournalDetSumByDate_Normal(prmLedger, pGLGroupList, dc);
            cObjList.AddRange(GetJournalDetSumByDate_Control(prmLedger, pGLGroupList, dc));
            return cObjList;
        }

        public static List<dcJournalDet> GetJournalDetSumByDate_ALL(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetJournalDetSumByDate_ALL(prmLedger, pGLGroupList, null);
        }
        public static List<dcJournalDet> GetJournalDetSumByDate_ALL(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcJournalDet> cObjList = GetJournalDetSumByDate_Normal(prmLedger, pGLGroupList, dc);
            cObjList.AddRange(GetJournalDetSumByDate_Control(prmLedger, pGLGroupList, dc));
            cObjList.AddRange(GetJournalDetSumByDate_Sub(prmLedger, pGLGroupList, dc));
            return cObjList;
        }


        public static List<dcJournalDetIns> GetJournalInsDet(clsPrmLedger prmLedger)
        {
            return GetJournalInsDet(prmLedger, null);
        }

        public static List<dcJournalDetIns> GetJournalInsDet(clsPrmLedger prmLedger, DBContext dc)
        {
            //
            List<dcJournalDetIns> cObjList = new List<dcJournalDetIns>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDetIns.* ");
            sb.Append(", tblJournalDet.JournalID, tblJournalDet.GLAccountID ");

            sb.Append(", tblInstrument.InstrumentNo, tblInstrument.InstrumentDate, tblInstrument.InstrumentAmt ");
            sb.Append(", tblInstrument.InstrumentModeID ");
            sb.Append(", tblInstrument.InstrumentTypeID, tblInstrumentType.InstrumentTypeCode, tblInstrumentType.InstrumentTypeName ");
            sb.Append(", tblInstrument.IssueName, tblInstrument.BankName, tblInstrument.BranchName  ");

            sb.Append(" FROM tblJournalDetIns ");
            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetIns.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblInstrument ON tblJournalDetIns.InstrumentID = tblInstrument.InstrumentID ");
            sb.Append(" INNER JOIN tblInstrumentType ON tblInstrument.InstrumentTypeID = tblInstrumentType.InstrumentTypeID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            //sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");


            sb.Append(" WHERE 1=1 ");

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.JournalID > 0)
            {
                sb.Append(" AND tblJournal.JournalID=@journalID ");
                cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
            }


            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
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

            if (prmLedger.IsLocation)
            {
                string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
            }
            sb.Append(" ORDER BY tblJournal.JournalNo, tblJournalDetIns.JournalDetInsSLNo ");

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;



            cObjList = DBQuery.ExecuteDBQuery<dcJournalDetIns>(dbq, dc);

            return cObjList;
        }

        public static List<dcJournalDetRef> GetJournalRefDet(clsPrmLedger prmLedger)
        {
            return GetJournalRefDet(prmLedger, null);
        }
        public static List<dcJournalDetRef> GetJournalRefDet(clsPrmLedger prmLedger, DBContext dc)
        {
            //
            List<dcJournalDetRef> cObjList = new List<dcJournalDetRef>();


            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDetRef.* ");
            sb.Append(", tblJournalDet.JournalID, tblJournalDet.GLAccountID ");
            sb.Append(", tblAccRef.AccRefCode, tblAccRef.AccRefName, tblAccRef.AccRefCategoryID ");
            sb.Append(", tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName, tblAccRefCategory.AccRefTypeID ");
            sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
            sb.Append(", tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent ");
            sb.Append(", tblGLAccount.GLGroupID ");

            sb.Append(" FROM tblJournalDetRef ");
            
            
            
            
            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetRef.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblAccRef ON tblJournalDetRef.AccRefID = tblAccRef.AccRefID ");
            sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            //sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");






            sb.Append(" WHERE 1=1 ");


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

            if (prmLedger.JournalID > 0)
            {
                sb.Append(" AND tblJournal.JournalID=@journalID ");
                //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
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
            if (prmLedger.IsLocation)
            {
                string sList = String.Join(",", prmLedger.LocationIDList.ToArray());
                sb.Append(string.Format(" AND tblJournal.LocationID IN ({0})", sList));
            }

            sb.Append(" AND ( ");
            sb.Append(" tblAccRefCategory.AccRefTypeID = " + (int)AccRefTypeEnum.TranCode);
            if (prmLedger.IncludeCostCenter)
            {
                sb.Append(" OR tblAccRefCategory.AccRefTypeID = " + (int)AccRefTypeEnum.CostCenter);
            }
            if (prmLedger.IncludeReference)
            {
                sb.Append(" OR tblAccRefCategory.AccRefTypeID = " + +(int)AccRefTypeEnum.Reference);
            }
            sb.Append(")");


            sb.Append(" ORDER BY tblJournal.JournalNo, tblJournalDetRef.JournalDetRefSLNo ");
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


            cObjList = DBQuery.ExecuteDBQuery<dcJournalDetRef>(dbq, dc);

            return cObjList;
        }

        public static List<dcJournalDetRef> GetJournalRefDetSum(clsPrmLedger prmLedger)
        {
            return GetJournalRefDetSum(prmLedger, null);
        }
        public static List<dcJournalDetRef> GetJournalRefDetSum(clsPrmLedger prmLedger, DBContext dc)
        {
            //
            List<dcJournalDetRef> cObjList = new List<dcJournalDetRef>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDetRef.* ");
            sb.Append(", tblJournalDet.JournalID, tblJournalDet.GLAccountID ");
            sb.Append(", tblAccRef.AccRefCode, tblAccRef.AccRefName, tblAccRef.AccRefCategoryID ");
            sb.Append(", tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName, tblAccRefCategory.AccRefTypeID ");
            sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
            sb.Append(", tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent ");
            sb.Append(", tblGLAccount.GLGroupID ");

            sb.Append(" FROM tblJournalDetRef ");




            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetRef.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblAccRef ON tblJournalDetRef.AccRefID = tblAccRef.AccRefID ");
            sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");






            sb.Append(" WHERE 1=1 ");


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

            if (prmLedger.JournalID > 0)
            {
                sb.Append(" AND tblJournal.JournalID=@journalID ");
                //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
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
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            sb.Append(" AND ( ");
            sb.Append(" tblAccRefCategory.AccRefTypeID = " + (int)AccRefTypeEnum.TranCode);
            if (prmLedger.IncludeCostCenter)
            {
                sb.Append(" OR tblAccRefCategory.AccRefTypeID = " + (int)AccRefTypeEnum.CostCenter);
            }
            if (prmLedger.IncludeReference)
            {
                sb.Append(" OR tblAccRefCategory.AccRefTypeID = " + +(int)AccRefTypeEnum.Reference);
            }
            sb.Append(")");


            sb.Append(" ORDER BY tblJournal.JournalNo, tblJournalDetRef.JournalDetRefSLNo ");
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


            cObjList = DBQuery.ExecuteDBQuery<dcJournalDetRef>(dbq, dc);

            return cObjList;
        }

        public static List<dcJournalDet> GetCashJournalDetByDate_Normal(clsPrmLedger prmLedger)
        {
            return GetCashJournalDetByDate_Normal(prmLedger, null);
        }
        public static List<dcJournalDet> GetCashJournalDetByDate_Normal(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
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
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");


                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID=tblGLGroup.GLGroupID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                if (prmLedger.JournalID > 0)
                {
                    sb.Append(" AND tblJournal.JournalID=@JournalID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@JournalID", prmLedger.JournalID);
                }


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int)GLAccountTypeEnum.NormalAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.NormalAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetCashJournalDetByDate_ControlSum(clsPrmLedger prmLedger)
        {
            return GetCashJournalDetByDate_ControlSum(prmLedger, null);
        }
        public static List<dcJournalDet> GetCashJournalDetByDate_ControlSum(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //SqlCommand cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT tblJournal.JournalID, tblJournal.JournalNo, tblJournal.JournalDate, tblJournal.JournalDesc, tblJournal.IsPosted ");
                sb.Append(" ,tblJournal.CompanyID, tblJournal.AccYearID ");
                sb.Append(" ,tblJournal.JournalAdjustTypeID ");
                sb.Append(" ,tblJournal.JournalTypeID , tblJournalType.JournalTypeCode,tblJournalType.JournalTypeName ");
                sb.Append(" ,JournalDetSum.GLAccountIDParent AS  GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" ,tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" ,JournalDetSum.DebitAmt, JournalDetSum.CreditAmt, JournalDetSum.DebitAmt - JournalDetSum.CreditAmt AS TranAmt ");
                sb.Append(" ,JournalDetSum.TranCount ");
                sb.Append(" FROM tblJournal ");
                sb.Append(" INNER JOIN ( SELECT tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" , SUM(tblJournalDet.CreditAmt) AS CreditAmt, SUM(tblJournalDet.DebitAmt) AS DebitAmt, COUNT(tblJournalDet.JournalDetID) AS TranCount ");
                sb.Append(" FROM tblJournalDet ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID=tblGLGroup.GLGroupID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" WHERE (1=1) ");
                //WHERE     (Accounting.tblGLAccount.GLAccountIDParent = 2)

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //cmd.Parameters.AddWithValue("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", (int)GLAccountTypeEnum.SubAccount);


                //sb.Append(" AND tblGLAccount.GLAccountIDParent > 0 ");
                ////if (prmLedger.GLAccountID > 0)
                //{

                //    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                //}


                //filter by account parent
                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
                    }
                }

                switch (prmLedger.IncludePostType)
                {
                    case IncludePostEnum.Posted:
                        sb.Append(" AND (tblJournal.IsPosted =@isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", true);
                        break;
                    case IncludePostEnum.Unposted:
                        sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", false);
                        break;
                    case IncludePostEnum.All:
                        break;
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


                sb.Append(" GROUP BY tblJournalDet.JournalID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" ) JournalDetSum ON tblJournal.JournalID = JournalDetSum.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON JournalDetSum.GLAccountIDParent = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetCashJournalDetByDate_Sub(clsPrmLedger prmLedger)
        {
            return GetCashJournalDetByDate_Sub(prmLedger, null);
        }
        public static List<dcJournalDet> GetCashJournalDetByDate_Sub(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDet.* ");

                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");


                sb.Append(" FROM  tblJournalDet ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID=tblGLGroup.GLGroupID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
                sb.Append(" WHERE (1 = 1) ");

                sb.Append(" AND (tblJournal.CompanyID=@companyID) ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }


                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAcountTypeID ");
                //cmd.Parameters.AddWithValue("@glAcountTypeID", (int) GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAcountTypeID", (int)GLAccountTypeEnum.SubAccount);

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                        }
                    }
                }

                switch (prmLedger.IncludePostType)
                {
                    case IncludePostEnum.Posted:
                        sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", true);
                        break;
                    case IncludePostEnum.Unposted:
                        sb.Append(" AND (tblJournal.IsPosted =@isPosted) ");
                        cmdInfo.DBParametersInfo.Add("@isPosted", false);
                        break;
                    case IncludePostEnum.All:
                        break;
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

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDet> GetCashJournalDetByDate(clsPrmLedger prmLedger)
        {
            return GetCashJournalDetByDate(prmLedger, null);
        }
        public static List<dcJournalDet> GetCashJournalDetByDate(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDet> cObjList = new List<dcJournalDet>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
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
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID ");
                sb.Append(" , tblGLAccount.GLGroupID, tblGLAccount.GLAccountIDParent ");


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

                if (prmLedger.GLAccountID > 0)
                {
                    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }

                if (prmLedger.FromDate.HasValue)
                {
                    if (prmLedger.IsBeforeDate)
                    {
                        sb.Append(" AND (tblJournal.JournalDate < @fromDate) ");
                        //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    }
                    else
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
                            sb.Append(" AND (tblJournal.JournalDate >= @fromDate) ");
                            //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                            cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                        }
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


                cObjList = GetJournalDetList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcJournalDetIns> GetCashJournalInsDet(clsPrmLedger prmLedger)
        {
            return GetCashJournalInsDet(prmLedger, null);
        }

        public static List<dcJournalDetIns> GetCashJournalInsDet(clsPrmLedger prmLedger, DBContext dc)
        {
            //
            List<dcJournalDetIns> cObjList = new List<dcJournalDetIns>();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDetIns.* ");
            sb.Append(", tblJournalDet.JournalID, tblJournalDet.GLAccountID ");

            sb.Append(", tblInstrument.InstrumentNo, tblInstrument.InstrumentDate, tblInstrument.InstrumentAmt ");
            sb.Append(", tblInstrument.InstrumentModeID ");
            sb.Append(", tblInstrument.InstrumentTypeID, tblInstrumentType.InstrumentTypeCode, tblInstrumentType.InstrumentTypeName ");
            sb.Append(", tblInstrument.IssueName, tblInstrument.BankName, tblInstrument.BranchName  ");

            sb.Append(" FROM tblJournalDetIns ");
            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetIns.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblInstrument ON tblJournalDetIns.InstrumentID = tblInstrument.InstrumentID ");
            sb.Append(" INNER JOIN tblInstrumentType ON tblInstrument.InstrumentTypeID = tblInstrumentType.InstrumentTypeID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");//new add by M
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID=tblGLGroup.GLGroupID ");



            sb.Append(" WHERE 1=1 ");

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

            if (prmLedger.JournalID > 0)
            {
                sb.Append(" AND tblJournal.JournalID=@journalID ");
                //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
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

            sb.Append(" ORDER BY tblJournal.JournalNo, tblJournalDetIns.JournalDetInsSLNo ");

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;



            cObjList = DBQuery.ExecuteDBQuery<dcJournalDetIns>(dbq, dc);

            return cObjList;
        }

        public static List<dcJournalDetRef> GetCashJournalRefDet(clsPrmLedger prmLedger)
        {
            return GetCashJournalRefDet(prmLedger, null);
        }
        public static List<dcJournalDetRef> GetCashJournalRefDet(clsPrmLedger prmLedger, DBContext dc)
        {
            //
            List<dcJournalDetRef> cObjList = new List<dcJournalDetRef>();


            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDetRef.* ");
            sb.Append(", tblJournalDet.JournalID, tblJournalDet.GLAccountID ");
            sb.Append(", tblAccRef.AccRefCode, tblAccRef.AccRefName, tblAccRef.AccRefCategoryID ");
            sb.Append(", tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName, tblAccRefCategory.AccRefTypeID ");
            sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
            sb.Append(", tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent ");
            sb.Append(", tblGLAccount.GLGroupID ");

            sb.Append(" FROM tblJournalDetRef ");




            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetRef.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblAccRef ON tblJournalDetRef.AccRefID = tblAccRef.AccRefID ");
            sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID=tblGLGroup.GLGroupID ");






            sb.Append(" WHERE 1=1 ");


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

            if (prmLedger.JournalID > 0)
            {
                sb.Append(" AND tblJournal.JournalID=@journalID ");
                //cmd.Parameters.AddWithValue("@journalID", prmLedger.JournalID);
                cmdInfo.DBParametersInfo.Add("@journalID", prmLedger.JournalID);
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

            sb.Append(" AND ( ");
            sb.Append(" tblAccRefCategory.AccRefTypeID = " + (int)AccRefTypeEnum.TranCode);
            if (prmLedger.IncludeCostCenter)
            {
                sb.Append(" OR tblAccRefCategory.AccRefTypeID = " + (int)AccRefTypeEnum.CostCenter);
            }
            if (prmLedger.IncludeReference)
            {
                sb.Append(" OR tblAccRefCategory.AccRefTypeID = " + +(int)AccRefTypeEnum.Reference);
            }
            sb.Append(")");

            sb.Append(" AND (tblGLGroup.IsCash= @isCash  OR tblGLGroup.IsBank= @isBank) ");
            cmdInfo.DBParametersInfo.Add("@isCash", prmLedger.IsCash);
            cmdInfo.DBParametersInfo.Add("@isBank", prmLedger.IsBank);


            sb.Append(" ORDER BY tblJournal.JournalNo, tblJournalDetRef.JournalDetRefSLNo ");
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


            cObjList = DBQuery.ExecuteDBQuery<dcJournalDetRef>(dbq, dc);

            return cObjList;
        }
    }
}
