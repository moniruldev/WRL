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
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class JournalDetInsBL
    {
        public static string GetJournalDetInsByID_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalDetIns.* ");
            sb.Append(" FROM tblJournalDetIns ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions JournalDetInsLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.dcJournalDetIns>(obj => obj.relatedclassname);
        //    return dlo;
        //}

        public static string GetJournalDetInsListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalDetIns.* ");
            sb.Append(", tblJournalDet.JournalID, tblJournal.JournalNo, tblJournal.JournalDate, tblJournal.IsPosted ");
            sb.Append(", tblJournal.JournalTypeID, tblJournalType.JournalTypeName ");
            sb.Append(", tblJournalDet.GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName  ");
            sb.Append(", tblGLAccount.GLAccountTypeID ");
            sb.Append(", tblGLAccount.GLGroupID, tblGLGroup.GLGroupCode, tblGLGroup.GLGroupName,  tblGLGroup.GLGroupNameShort ");
            sb.Append(", tblJournalDet.JournalDetDesc ");

            sb.Append(", tblInstrument.InstrumentNo, tblInstrument.InstrumentDate, tblInstrument.InstrumentAmt ");
            sb.Append(", tblInstrument.InstrumentModeID, tblInstrument.InstrumentTypeID ");
            sb.Append(", tblInstrument.IssueName, tblInstrument.BankName, tblInstrument.BranchName  ");
            
            sb.Append(" FROM tblJournalDetIns ");
            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetIns.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblInstrument ON tblJournalDetIns.InstrumentID = tblInstrument.InstrumentID ");

            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
            


            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }


        public static List<dcJournalDetIns> GetJournalDetInsList(int pCompanyID, int pJournalID)
        {
            return GetJournalDetInsList(pCompanyID, pJournalID, null);
        }
        public static List<dcJournalDetIns> GetJournalDetInsList(int pCompanyID, int pJournalID, DBContext dc)
        {
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetJournalDetInsListString());

            sb.Append(" AND tblJournal.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            if (pJournalID > 0)
            {
                sb.Append(" AND tblJournalDet.JournalID=@pJournalID ");
                //cmd.Parameters.AddWithValue("@pInstrumentID", pJournalID);
                cmdInfo.DBParametersInfo.Add("@pJournalID", pJournalID);
            }

            //if (pJournalDetRefTypeID > 0)
            //{
            //    sb.Append(" AND tblJournalDetRef.JournalDetRefTypeID=@journalDetRefTypeID ");
            //    cmd.Parameters.AddWithValue("@journalDetRefTypeID", pJournalDetRefTypeID);
            //}

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
            return GetJournalDetInsList(dbq, dc);
        }



        public static List<dcJournalDetIns> GetJournalDetInsListByInstrumentID(int pCompanyID, int pInstrumentID)
        {
            return GetJournalDetInsListByInstrumentID(pCompanyID, pInstrumentID, null);
        }
        public static List<dcJournalDetIns> GetJournalDetInsListByInstrumentID(int pCompanyID, int pInstrumentID, DBContext dc)
        {
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetJournalDetInsListString());

            sb.Append(" AND tblJournal.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            if (pInstrumentID > 0)
            {
                sb.Append(" AND tblJournalDetIns.InstrumentID=@instrumentID ");
                //cmd.Parameters.AddWithValue("@instrumentID", pInstrumentID);
                cmdInfo.DBParametersInfo.Add("@instrumentID", pInstrumentID);
            }

            //if (pJournalDetRefTypeID > 0)
            //{
            //    sb.Append(" AND tblJournalDetRef.JournalDetRefTypeID=@journalDetRefTypeID ");
            //    cmd.Parameters.AddWithValue("@journalDetRefTypeID", pJournalDetRefTypeID);
            //}

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
            return GetJournalDetInsList(dbq, dc);
        }


        public static List<dcJournalDetIns> GetJournalDetInsList(DBQuery dbq, DBContext dc)
        {
            List<dcJournalDetIns> cObjList = new List<dcJournalDetIns>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcJournalDetIns>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcJournalDetIns GetJournalDetInsByID(int pJournalDetInsID)
        {
            return GetJournalDetInsByID(pJournalDetInsID, null);
        }
        public static dcJournalDetIns GetJournalDetInsByID(int pJournalDetInsID, DBContext dc)
        {
            dcJournalDetIns cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalDetInsByID_SQLString());
                sb.Append(" AND tblJournalDetIns.JournalDetInsID=@journalDetInsID ");
                cmdInfo.DBParametersInfo.Add("@journalDetInsID", pJournalDetInsID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObj = DBQuery.ExecuteDBQuery<dcJournalDetIns>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcJournalDetIns>()
                //                  where c.JournalDetInsID == pJournalDetInsID
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

        public static int Insert(dcJournalDetIns cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcJournalDetIns cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcJournalDetIns>(cObj, true);
                if (id > 0) { cObj.JournalDetInsID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcJournalDetIns cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcJournalDetIns cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcJournalDetIns>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pJournalDetInsID)
        {
            return Delete(pJournalDetInsID, null);
        }
        public static bool Delete(int pJournalDetInsID, DBContext dc)
        {
            dcJournalDetIns cObj = new dcJournalDetIns();
            cObj.JournalDetInsID = pJournalDetInsID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcJournalDetIns>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static void UpdateSLNo(List<dcJournalDetIns> pListDetails)
        {
            if (pListDetails == null)
            {
                return;
            }

            int slNo = 0;
            var grpDetLink = pListDetails.GroupBy(c => c.JournalDetID_Link);

            foreach (var grp in grpDetLink)
            {
                slNo = 0;
                foreach (dcJournalDetIns oDet in grp)
                {
                    if (oDet._RecordState != RecordStateEnum.Deleted)
                    {
                        slNo++;
                        oDet.JournalDetInsSLNo = slNo;
                    }
                }
            }
        }


        public static int Save(dcJournalDetIns cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcJournalDetIns cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcJournalDetIns cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcJournalDetIns cObj, DBContext dc)
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
                                newID = cObj.JournalDetInsID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.JournalDetInsID, dc))
                            {
                                newID = cObj.JournalDetInsID;
                            }
                            break;
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;
                        InstrumentBL.UpdateInstrumentAmt(cObj.CompanyID, cObj.InstrumentID, dc);
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

        public static bool SaveList(List<dcJournalDetIns> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcJournalDetIns> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcJournalDetIns oDet in detList)
            {
                int a = Save(oDet, dc);


                //switch (oDet._RecordState)
                //{
                //    case PG.Core.DBClass.RecordStateEnum.Added:
                //        int a = Insert(oDet, dc);
                //        break;
                //    case PG.Core.DBClass.RecordStateEnum.Edited:
                //        bool e = Update(oDet, dc);
                //        break;
                //    case PG.Core.DBClass.RecordStateEnum.Deleted:
                //        bool d = Delete(oDet.JournalDetInsID, dc);
                //        break;
                //    default:
                //        break;
                //}
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }


        public static dcJournalDetIns GetSumByInstrumentID(int pCompanyID, int pInstrumentID)
        {
            return GetSumByInstrumentID(pCompanyID, pInstrumentID, null);
        }

        public static dcJournalDetIns GetSumByInstrumentID(int pCompanyID, int pInstrumentID, DBContext dc)
        {
            dcJournalDetIns cObj = null;

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblJournalDetIns.InstrumentID, SUM(tblJournalDetIns.InsTranAmt) AS InsTranAmt ");
            sb.Append(" FROM  tblJournalDetIns ");
            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetIns.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            
            sb.Append(" WHERE (1=1) ");


            sb.Append(" AND tblJournal.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


            sb.Append(" AND tblJournalDetIns.InstrumentID=@instrumentID ");
            //cmd.Parameters.AddWithValue("@instrumentID", pInstrumentID);
            cmdInfo.DBParametersInfo.Add("@instrumentID", pInstrumentID);

            sb.Append (" GROUP BY InstrumentID ");

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

            cObj = GetJournalDetInsList(dbq, dc).FirstOrDefault();

            return cObj;
        }

        public static List<dcJournalDetIns> GetSumByJournalID(int pCompanyID, int pJournalID)
        {
            return GetSumByJournalID(pCompanyID, pJournalID, null);
        }

        public static List<dcJournalDetIns> GetSumByJournalID(int pCompanyID, int pJournalID, DBContext dc)
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblJournalDetIns.InstrumentID, SUM(tblJournalDetIns.InsTranAmt) AS InsTranAmt ");
            sb.Append(" FROM  tblJournalDetIns ");
            sb.Append(" INNER JOIN tblJournalDet ON tblJournalDetIns.JournalDetID = tblJournalDet.JournalDetID ");
            sb.Append(" INNER JOIN tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            
            sb.Append(" WHERE (1=1) ");

            sb.Append(" AND tblJournal.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            sb.Append(" AND tblJournalDet.JournalID=@journalID ");
            //cmd.Parameters.AddWithValue("@journalID", pJournalID);
            cmdInfo.DBParametersInfo.Add("@journalID", pJournalID);

            sb.Append(" GROUP BY tblJournalDetIns.InstrumentID ");

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

            return GetJournalDetInsList(dbq, dc);
        }


    }
}
