using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using PG.Core.Extentions;
using PG.Core.DBBase;
using PG.Core.DBFilters;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.Core.DBValidation;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class JournalDetRefBL
    {

        public static string GetJournalDetRefList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalDetRef.* ");
            sb.Append(" FROM tblJournalDetRef ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions JournalDetRefLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.dcJournalDetRef>(obj => obj.relatedclassname);
        //    return dlo;
        //}

        public static string GetJournalDetRefListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalDetRef.* ");
            sb.Append(", tblJournalDet.JournalID, tblJournalDet.GLAccountID ");
            sb.Append(", tblAccRef.AccRefCode, tblAccRef.AccRefName, tblAccRef.AccRefCategoryID ");
            sb.Append(", tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName, tblAccRefCategory.AccRefTypeID ");
            sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");

            sb.Append(" FROM tblJournalDetRef ");
            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetRef.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblAccRef ON tblJournalDetRef.AccRefID = tblAccRef.AccRefID ");
            sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }


        public static List<dcJournalDetRef> GetJournalDetRefList(int pCompanyID, int pJournalID, int pJournalDetRefTypeID)
        {
            return GetJournalDetRefList(pCompanyID, pJournalID, pJournalDetRefTypeID, null);
        }
        public static List<dcJournalDetRef> GetJournalDetRefList(int pCompanyID, int pJournalID, int pJournalDetRefTypeID, DBContext dc)
        {
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetJournalDetRefListString());


            sb.Append(" AND tblJournal.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            if (pJournalID > 0)
            {
                sb.Append(" AND tblJournalDet.JournalID=@journalID ");
                //cmd.Parameters.AddWithValue("@journalID", pJournalID);
                cmdInfo.DBParametersInfo.Add("@journalID", pJournalID);
            }

            if (pJournalDetRefTypeID > 0)
            {
                sb.Append(" AND tblJournalDetRef.JournalDetRefTypeID=@journalDetRefTypeID ");
                //cmd.Parameters.AddWithValue("@journalDetRefTypeID", pJournalDetRefTypeID);
                cmdInfo.DBParametersInfo.Add("@journalDetRefTypeID", pJournalDetRefTypeID);
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
            return GetJournalDetRefList(dbq, dc);
        }

        public static List<dcJournalDetRef> GetJournalDetRefList(DBQuery dbq, DBContext dc)
        {
            List<dcJournalDetRef> cObjList = new List<dcJournalDetRef>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                        //dbq.OrderBy = "YearStartDate Desc";
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcJournalDetRef>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        
        
        public static dcJournalDetRef GetJournalDetRefByID(int pJournalDetRefID)
        {
            return GetJournalDetRefByID(pJournalDetRefID, null);
        }
        public static dcJournalDetRef GetJournalDetRefByID(int pJournalDetRefID, DBContext dc)
        {
            dcJournalDetRef cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalDetRefList_SQLString());
                sb.Append(" AND tblJournalDetRef.JournalDetRefID=@journalDetRefID ");
                cmdInfo.DBParametersInfo.Add("@journalDetRefID", pJournalDetRefID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcJournalDetRef>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcJournalDetRef>()
                //                  where c.JournalDetRefID == pJournalDetRefID
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

        public static int Insert(dcJournalDetRef cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcJournalDetRef cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcJournalDetRef>(cObj, true);
                if (id > 0) { cObj.JournalDetRefID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcJournalDetRef cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcJournalDetRef cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcJournalDetRef>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pJournalDetRefID)
        {
            return Delete(pJournalDetRefID, null);
        }
        public static bool Delete(int pJournalDetRefID, DBContext dc)
        {
            dcJournalDetRef cObj = new dcJournalDetRef();
            cObj.JournalDetRefID = pJournalDetRefID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcJournalDetRef>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static void UpdateSLNo(List<dcJournalDetRef> pListDetails)
        {
            if (pListDetails == null)
            {
                return;
            }

            int slNo = 0;
            var grpDetLink = pListDetails.GroupBy(c => c.JournalDetID_Link);

            foreach (var grpLink in grpDetLink)
            {
                var grpDetLinkType = pListDetails.Where(c => c.JournalDetID_Link == grpLink.Key).GroupBy(c => c.AccRefTypeID);
                foreach (var grpLinkType in grpDetLinkType)
                {
                    List<dcJournalDetRef> detList = pListDetails.Where(c => c.JournalDetID_Link == grpLink.Key
                                                                       && c.AccRefTypeID == grpLinkType.Key).ToList();
                    slNo = 0;
                    foreach (dcJournalDetRef oDet in detList)
                    {
                        if (oDet._RecordState != RecordStateEnum.Deleted)
                        {
                            slNo++;
                            oDet.JournalDetRefSLNo = slNo;
                        }
                    }
                }
            }
        }


        public static int Save(dcJournalDetRef cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcJournalDetRef cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcJournalDetRef cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcJournalDetRef cObj, DBContext dc)
        {
            int newID = 0;
            bool isDCInit = false;
            bool isTransInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                using (DataContext dataContext = dc.NewDataContext())
                {

                    switch (cObj._RecordState)
                    {
                        case RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.JournalDetRefID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.JournalDetRefID, dc))
                            {
                                newID = 1;
                            }
                            break;
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;

                        ///code list save logic here

                        bStatus = true;
                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                    }
                }
            }
            catch
            {
                dc.RollbackTransaction(isTransInit);
                throw;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return newID;
        }

        public static bool SaveList(List<dcJournalDetRef> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcJournalDetRef> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;

            //if (detList == null)
            //{
            //    return true;
            //}
            
            
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcJournalDetRef oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case RecordStateEnum.Deleted:
                        bool d = Delete(oDet.JournalDetRefID, dc);
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

        public static List<dcJournalDetRef> GetJournalDetRefByDate(clsPrmLedger prmLedger)
        {
            return GetJournalDetRefByDate(prmLedger, null);
        }
        public static List<dcJournalDetRef> GetJournalDetRefByDate(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDetRef> cObjList = new List<dcJournalDetRef>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //SqlCommand cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT tblJournalDetRef.* ");
                sb.Append(" , tblJournalDet.GLAccountID , tblJournalDet.JournalDetDesc ");
                sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
                sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
                sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
                sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

                sb.Append(" , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
                sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.BalanceType, tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent ");
                sb.Append(" , tblGLAccount.GLGroupID  ");
                sb.Append(" , tblGLGroup.GLGroupCode, tblGLGroup.GLGroupName, tblGLGroup.GLGroupNameShort ");
                sb.Append(" , tblAccRef.AccRefCode, tblAccRef.AccRefNameShort, tblAccRef.AccRefName ");
                sb.Append(" , tblAccRef.AccRefCategoryID, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryNameShort, tblAccRefCategory.AccRefCategoryName ");
                sb.Append(" , tblAccRefCategory.AccRefTypeID ");



                sb.Append(" FROM  tblJournalDetRef ");
                sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetRef.JournalDetID = tblJournalDet.JournalDetID ");
                sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
                sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

                sb.Append(" INNER JOIN tblAccRef ON tblJournalDetRef.AccRefID = tblAccRef.AccRefID ");
                sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");

                sb.Append("WHERE (1=1) ");

                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);

                if (prmLedger.AccYearID > 0)
                {
                    sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
                }


                if (prmLedger.AccRefType > 0)
                {
                    sb.Append(" AND tblAccRefCategory.AccRefTypeID=@accRefTypeID ");
                    //cmd.Parameters.AddWithValue("@accRefTypeID", (int)prmLedger.AccRefType);
                    cmdInfo.DBParametersInfo.Add("@accRefTypeID", (int)prmLedger.AccRefType);
                }


                if (prmLedger.AccRefCategoryID > 0)
                {
                    sb.Append(" AND tblAccRef.AccRefCategoryID=@accRefCategoryID ");
                    //cmd.Parameters.AddWithValue("@accRefCategoryID", prmLedger.AccRefCategoryID);
                    cmdInfo.DBParametersInfo.Add("@accRefCategoryID", prmLedger.AccRefCategoryID);
                }

                if (prmLedger.AccRefID > 0)
                {
                    sb.Append(" AND tblJournalDetRef.AccRefID=@AccRefID ");
                    //cmd.Parameters.AddWithValue("@AccRefID", prmLedger.AccRefID);
                    cmdInfo.DBParametersInfo.Add("@AccRefID", prmLedger.AccRefID);
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


                sb.Append(" ORDER BY tblJournal.JournalDate ");
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

                cObjList = GetJournalDetRefList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcJournalDetRef> GetJournalDetRefSumByDate(clsPrmLedger prmLedger)
        {
            return GetJournalDetRefSumByDate(prmLedger, null);
        }
        public static List<dcJournalDetRef> GetJournalDetRefSumByDate(clsPrmLedger prmLedger, DBContext dc)
        {
            List<dcJournalDetRef> cObjList = new List<dcJournalDetRef>();

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                int glAccountTypeID = GLAccountBL.GetGLAccountTypeID(prmLedger.CompanyID, prmLedger.GLAccountID, dc);

                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT tblJournalDetRef.AccRefID, SUM(tblJournalDetRef.DebitAmt) AS DebitAmt, SUM(tblJournalDetRef.CreditAmt) AS CreditAmt ");
                sb.Append(" , SUM(tblJournalDetRef.TranAmt) AS TranAmt, COUNT(tblJournalDetRef.JournalDetRefID) AS TranCount  ");

                sb.Append(" FROM tblJournalDetRef ");
                sb.Append(" INNER JOIN  tblJournalDet ON tblJournalDetRef.JournalDetID = tblJournalDet.JournalDetID ");
                sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
                sb.Append(" INNER JOIN  tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");

                sb.Append(" INNER JOIN  tblAccRef ON tblJournalDetRef.AccRefID = tblAccRef.AccRefID ");
                sb.Append(" INNER JOIN  tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");

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

                //sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);


                if ((int)prmLedger.AccRefType > 0)
                {
                    sb.Append(" AND tblAccRefCategory.AccRefTypeID=@AccRefTypeID ");
                    cmdInfo.DBParametersInfo.Add("@AccRefTypeID", (int)prmLedger.AccRefType);
                }


                if (prmLedger.AccRefCategoryID > 0)
                {
                    sb.Append(" AND tblAccRef.AccRefCategoryID=@accRefCategoryID ");
                    //cmd.Parameters.AddWithValue("@accRefCategoryID", prmLedger.AccRefCategoryID);
                    cmdInfo.DBParametersInfo.Add("@accRefCategoryID", prmLedger.AccRefCategoryID);
                }


                if (prmLedger.AccRefID > 0)
                {
                    sb.Append(" AND tblJournalDetRef.AccRefID=@accRefID ");
                    //cmd.Parameters.AddWithValue("@accRefID", prmLedger.AccRefID);
                    cmdInfo.DBParametersInfo.Add("@accRefID", prmLedger.AccRefID);
                }


                //if (prmLedger.GLAccountID > 0)
                //{
                //    sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                //    cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                //}


                if (prmLedger.GLAccountID > 0)
                {
                    if (glAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                    {
                        sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    }
                    else
                    {
                        sb.Append(" AND tblJournalDet.GLAccountID=@glAccountID ");
                    }
                    //cmd.Parameters.AddWithValue("@glAccountID", prmLedger.GLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", prmLedger.GLAccountID);
                }



                //if (pGLGroupList != null)
                //{
                //    string strGrpList = string.Empty;
                //    string comma = "";

                //    foreach (dcGLGroup grp in pGLGroupList)
                //    {
                //        strGrpList += comma + grp.GLGroupID;
                //        comma = ",";
                //    }

                //    if (strGrpList != string.Empty)
                //    {
                //        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                //    }
                //}


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

                sb.Append(" GROUP BY tblJournalDetRef.AccRefID ");
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

                cObjList = GetJournalDetRefList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



      }
}
