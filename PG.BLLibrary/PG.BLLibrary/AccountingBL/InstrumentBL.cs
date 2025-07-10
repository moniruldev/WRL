using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using System.Data.Common;

namespace PG.BLLibrary.AccountingBL
{
    public class InstrumentBL
    {
        //public static DataLoadOptions InstrumentLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.dcInstrument>(obj => obj.relatedclassname);
        //    return dlo;
        //}
        public static string GetInstrument_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblInstrument.* ");
            sb.Append(" FROM tblInstrument ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static string GetInsturmentListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblInstrument.* ");
            sb.Append(", tblInstrumentMode.InstrumentModeName "); 
            sb.Append(", tblInstrumentType.InstrumentTypeCode, tblInstrumentType.InstrumentTypeName ");
            sb.Append(", tblInstrumentStatus.InstrumentStatusCode, tblInstrumentStatus.InstrumentStatusName ");

            sb.Append(" FROM tblInstrument ");
            sb.Append(" INNER JOIN tblInstrumentMode ON tblInstrument.InstrumentModeID = tblInstrumentMode.InstrumentModeID "); 
            sb.Append(" INNER JOIN tblInstrumentType ON tblInstrument.InstrumentTypeID = tblInstrumentType.InstrumentTypeID ");
            sb.Append(" INNER JOIN tblInstrumentStatus ON tblInstrument.InstrumentStatusID = tblInstrumentStatus.InstrumentStatusID ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }


        public static List<dcInstrument> GetInstrumentList()
        {
            return GetInstrumentList(null, null);
        }
        public static List<dcInstrument> GetInstrumentList(int pCompanyID, int pInstrumentModeID, int pInstrumentTypeID, int pInstrumentStatusID, DBContext dc)
        {
            //DbCommand dCmd;
            //DbConnection dCon;
            //DbParameter dParam = new SqlParameter();
           
            

            //SqlCommand cmd = new SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetInsturmentListString());

            sb.Append(" AND tblInstrument.CompanyID = @companyID");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID",pCompanyID);

            if (pInstrumentTypeID > 0)
            {
                sb.Append(" AND tblInstrument.InstrumentTypeID = @instrumentTypeID");
                //cmd.Parameters.AddWithValue("@instrumentTypeID", pInstrumentTypeID);
                cmdInfo.DBParametersInfo.Add("@instrumentTypeID", pInstrumentTypeID);
            }

            if (pInstrumentModeID > 0)
            {
                sb.Append(" AND tblInstrument.InstrumentModeID = @instrumentModeID");
                //cmd.Parameters.AddWithValue("@instrumentModeID", pInstrumentModeID);
                cmdInfo.DBParametersInfo.Add("@instrumentModeID", pInstrumentModeID);
            }

            if (pInstrumentStatusID > 0)
            {
                sb.Append(" AND tblInstrument.InstrumentStatusID = @instrumentStatusID");
                //cmd.Parameters.AddWithValue("@instrumentStatusID", pInstrumentStatusID);
                cmdInfo.DBParametersInfo.Add("@instrumentStatusID", pInstrumentStatusID);
            }


            sb.Append(" ORDER BY tblInstrument.InstrumentDate, tblInstrument.InstrumentNo ");

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

            return GetInstrumentList(dbq, dc);
        }
        public static List<dcInstrument> GetInstrumentList(DBQuery dbq, DBContext dc)
        {
            List<dcInstrument> cObjList = new List<dcInstrument>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "YearStartDate Desc";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcInstrument>(dbq, dc);
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "YearStartDate Desc";
                //    }
                //    cObjList = DBQuery.ExecuteDBQuery<dcInstrument>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcInstrument GetInstrumentByID(int pCompanyID, int pInstrumentID)
        {
            return GetInstrumentByID(pCompanyID, pInstrumentID, null);
        }
        public static dcInstrument GetInstrumentByID(int pCompanyID, int pInstrumentID, DBContext dc)
        {
            dcInstrument cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInstrument_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInstrument>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcInstrument>()
                //                  where c.CompanyID == pCompanyID && c.InstrumentID == pInstrumentID
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

        public static int Insert(dcInstrument cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcInstrument cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcInstrument>(cObj, true);
                if (id > 0) { cObj.InstrumentID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcInstrument cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcInstrument cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcInstrument>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pInstrumentID)
        {
            return Delete(pInstrumentID, null);
        }
        public static bool Delete(int pInstrumentID, DBContext dc)
        {
            dcInstrument cObj = new dcInstrument();
            cObj.InstrumentID = pInstrumentID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcInstrument>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcInstrument cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcInstrument cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcInstrument cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcInstrument cObj, DBContext dc)
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
                                newID = cObj.InstrumentID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.InstrumentID, dc))
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
                        UpdateInstrumentAmt(cObj.CompanyID, newID, dc);
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

        public static bool SaveList(List<dcInstrument> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcInstrument> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcInstrument oDet in detList)
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
                        bool d = Delete(oDet.InstrumentID, dc);
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

        public static bool IsInstrumentNoExists(int pComapnyID, string pInsNo, int pInsModeID, int pInsTypeID)
        {
            return IsInstrumentNoExists(pComapnyID, pInsNo, pInsModeID, pInsTypeID, null);
        }
        public static bool IsInstrumentNoExists(int pComapnyID, string pInsNo, int pInsModeID, int pInsTypeID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInstrument_SQLString());
                sb.Append(" AND tblInstrument.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pComapnyID);

                sb.Append(" AND  tblInstrument.InstrumentNo=@insNo");
                cmdInfo.DBParametersInfo.Add("@insNo", pInsNo);

                sb.Append(" AND tblInstrument.InstrumentModeID=@instrumentModeID");
                cmdInfo.DBParametersInfo.Add("@instrumentModeID", pInsModeID);

                sb.Append(" AND tblInstrument.InstrumentTypeID=@insTypeID");
                cmdInfo.DBParametersInfo.Add("@insTypeID", pInsTypeID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcInstrument>()
                //                 where cObj.InstrumentNo == pInsNo && cObj.InstrumentModeID == pInsModeID
                //                        && cObj.InstrumentTypeID == pInsTypeID && cObj.CompanyID == pComapnyID
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static bool IsInstrumentNoExists(int pComapnyID, string pInsNo, int pInsModeID, int pInsTypeID, int pInstrumentID)
        {
            return IsInstrumentNoExists(pComapnyID, pInsNo, pInsModeID, pInsTypeID, pInstrumentID, null);
        }
        public static bool IsInstrumentNoExists(int pComapnyID, string pInsNo, int pInsModeID, int pInsTypeID, int pInstrumentID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInstrument_SQLString());
                sb.Append(" AND tblInstrument.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pComapnyID);

                sb.Append(" AND  tblInstrument.InstrumentNo=@insNo");
                cmdInfo.DBParametersInfo.Add("@insNo", pInsNo);

                sb.Append(" AND tblInstrument.InstrumentModeID=@instrumentModeID");
                cmdInfo.DBParametersInfo.Add("@instrumentModeID", pInsModeID);

                sb.Append(" AND tblInstrument.InstrumentTypeID=@insTypeID");
                cmdInfo.DBParametersInfo.Add("@insTypeID", pInsTypeID);

                sb.Append(" AND tblInstrument.InstrumentID!=@instrumentID");
                cmdInfo.DBParametersInfo.Add("@instrumentID", pInstrumentID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcInstrument>()
                //                 where cObj.InstrumentID != pInstrumentID
                //                        && cObj.InstrumentNo == pInsNo && cObj.InstrumentModeID == pInsModeID
                //                        && cObj.InstrumentTypeID == pInsTypeID && cObj.CompanyID == pComapnyID
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static bool UpdateInstrumentAmt(int pCompanyID, int pInstrumentID)
        {
            return UpdateInstrumentAmt(pCompanyID, pInstrumentID, null);
        }

        public static bool UpdateInstrumentAmt(int pCompanyID, int pInstrumentID, DBContext dc)
        {
            bool bStatus = false;
            dcInstrument ins = GetInstrumentByID(pCompanyID, pInstrumentID, dc);
            if (ins != null)
            {
                decimal totTranAmt = 0;
                decimal totRemainAmt = 0;
                dcJournalDetIns detIns = JournalDetInsBL.GetSumByInstrumentID(pCompanyID, pInstrumentID, dc);
                if (detIns != null)
                {
                    totTranAmt = detIns.InsTranAmt;
                }
                dcInstrument insUpd = new dcInstrument();
                insUpd.InstrumentID = ins.InstrumentID;

                if (ins.InstrumentAmt > 0)
                {
                    totRemainAmt = ins.InstrumentAmt - totTranAmt;
                    totRemainAmt = totRemainAmt < 0 ? 0 : totRemainAmt;
                }
                insUpd.InstrumentAmtTran = totTranAmt;
                insUpd.InstrumentAmtRemain = totRemainAmt;
                bStatus = Update(insUpd, dc);
            }

            return bStatus;
        }

    }
}
