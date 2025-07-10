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

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class JournalTypeBL
    {
        //public static DataLoadOptions JournalTypeLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.dcJournalType>(obj => obj.relatedclassname);
        //    return dlo;
        //}

        public static string GetJournalTypeListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblJournalType.* ");
            sb.Append(", tblJournalTypeClass.JournalTypeClassCode, tblJournalTypeClass.JournalTypeClassName ");
            sb.Append(", tblJournalTypeClass.GLGroupClassInclude, tblJournalTypeClass.GLGroupClassExclude ");
            sb.Append(" FROM tblJournalType ");
            sb.Append(" INNER JOIN tblJournalTypeClass ON tblJournalType.JournalTypeClassID = tblJournalTypeClass.JournalTypeClassID "); 
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }


        public static List<dcJournalType> GetJournalTypeList(int pCompanyID)
        {
            return GetJournalTypeList(pCompanyID, null);
        }
        public static List<dcJournalType> GetJournalTypeList(int pCompanyID, DBContext dc)
        {
            List<dcJournalType> cObjList = new List<dcJournalType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalTypeListString());

                sb.Append(" AND tblJournalType.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                sb.Append(" ORDER BY tblJournalType.JournalTypeSLNo ");

                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = GetJournalTypeList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcJournalType> GetJournalTypeList(DBQuery dbq, DBContext dc)
        {
            List<dcJournalType> cObjList = new List<dcJournalType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "YearStartDate Desc";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcJournalType>(dbq, dc);

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "YearStartDate Desc";
                //    }
                //    cObjList = DBQuery.ExecuteDBQuery<dcJournalType>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcJournalType GetJournalTypeByID(int pCompanyID, int pJournalTypeID)
        {
            return GetJournalTypeByID( pCompanyID,pJournalTypeID, null);
        }
        public static dcJournalType GetJournalTypeByID(int pCompanyID, int pJournalTypeID, DBContext dc)
        {
            dcJournalType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalTypeListString());

                sb.Append(" AND tblJournalType.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@pCompanyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (pJournalTypeID > 0)
                {
                    sb.Append(" AND tblJournalType.JournalTypeID=@journalTypeID ");
                    //cmd.Parameters.AddWithValue("@journalTypeID", pJournalTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalTypeID", pJournalTypeID);

                }

                sb.Append(" ORDER BY tblJournalType.JournalTypeSLNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObj = GetJournalTypeList(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static int Insert(dcJournalType cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcJournalType cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcJournalType>(cObj, true);
                if (id > 0) { cObj.JournalTypeID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcJournalType cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcJournalType cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcJournalType>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pJournalTypeID)
        {
            return Delete(pJournalTypeID, null);
        }
        public static bool Delete(int pJournalTypeID, DBContext dc)
        {
            dcJournalType cObj = new dcJournalType();
            cObj.JournalTypeID = pJournalTypeID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcJournalType>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcJournalType cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcJournalType cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcJournalType cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcJournalType cObj, DBContext dc)
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
                                newID = cObj.JournalTypeID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.JournalTypeID, dc))
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

        public static bool SaveList(List<dcJournalType> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcJournalType> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcJournalType oDet in detList)
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
                        bool d = Delete(oDet.JournalTypeID, dc);
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

  
   


    }
}
