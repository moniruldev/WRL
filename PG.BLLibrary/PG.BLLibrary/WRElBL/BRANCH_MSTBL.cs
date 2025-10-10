using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.WRElBL
{
    public class BRANCH_MSTBL
    {
        public static DataLoadOptions BRANCH_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcBRANCH_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetBranchMstSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT B.*,emp.emp_name BRANCH_HEAD_NAME  ");
            sb.Append(" FROM branch_mst B ");
            sb.Append(" LEFT JOIN EMPLOYEE_MST EMP ON B.BRANCH_HEAD=emp.emp_id ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static List<dcBRANCH_MST> GetBRANCH_MSTList()
        {
            return GetBRANCH_MSTList(null, null);
        }
        public static List<dcBRANCH_MST> GetBRANCH_MSTList(DBContext dc)
        {
            return GetBRANCH_MSTList(null, dc);
        }
        public static List<dcBRANCH_MST> GetBRANCH_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcBRANCH_MST> cObjList = new List<dcBRANCH_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBRANCH_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcBRANCH_MST GetBRANCH_MSTByID(int pBRANCH_ID)
        {
            return GetBRANCH_MSTByID(pBRANCH_ID, null);
        }
        public static dcBRANCH_MST GetBRANCH_MSTByID(int pBRANCH_ID, DBContext dc)
        {
            dcBRANCH_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcBRANCH_MST>()
                                  where c.BRANCH_ID == pBRANCH_ID
                                  select c).ToList();
                    if (result.Count() > 0)
                    {
                        cObj = result.First();
                    }
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcBRANCH_MST GetBranchMstInfoById(int pBRANCH_ID)
        {
            return GetBranchMstInfoById(pBRANCH_ID, null).FirstOrDefault();
        }
        public static List<dcBRANCH_MST> GetBranchMstInfoById(int pBRANCH_ID, DBContext dc)
        {
            List<dcBRANCH_MST> cObjList = new List<dcBRANCH_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBranchMstSQLString());
                if (pBRANCH_ID > 0)
                {
                    sb.Append(" AND b.BRANCH_ID= @pBRANCH_ID ");
                    cmdInfo.DBParametersInfo.Add("@pBRANCH_ID", pBRANCH_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcBRANCH_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcBRANCH_MST> GetBranchListInfo(clsPrmWREL prm, DBContext dc)
        {
            List<dcBRANCH_MST> cObjList = new List<dcBRANCH_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBranchMstSQLString());




                if (prm.IsActive != "0")
                {
                    sb.Append(" AND b.IS_ACTIVE= @IS_ACTIVE ");
                    cmdInfo.DBParametersInfo.Add("@IS_ACTIVE", prm.IsActive);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcBRANCH_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static bool IsBranchNameExists(string pBranchName)
        {
            return IsBranchNameExists(pBranchName, null);
        }
        public static bool IsBranchNameExists(string pBranchName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBranchMstSQLString());

                sb.Append(" AND UPPER(b.BRANCH_NAME)=UPPER(@branchName) ");
                cmdInfo.DBParametersInfo.Add("@branchName", pBranchName);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetBRANCH_MSTList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsBranchNameExists(string pBranchName, int pBranchId)
        {
            return IsBranchNameExists(pBranchName, pBranchId, null);
        }
        public static bool IsBranchNameExists(string pBranchName, int pBranchId, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBranchMstSQLString());

                sb.Append(" AND UPPER(b.BRANCH_NAME)=UPPER(@branchName) ");
                cmdInfo.DBParametersInfo.Add("@branchName", pBranchName);


                sb.Append(" AND b.BRANCH_ID <> @branchId ");
                cmdInfo.DBParametersInfo.Add("@branchId", pBranchId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                isData = GetBRANCH_MSTList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static int Insert(dcBRANCH_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcBRANCH_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcBRANCH_MST>(cObj, true);
                if (id > 0) { cObj.BRANCH_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcBRANCH_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcBRANCH_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcBRANCH_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pBRANCH_ID)
        {
            return Delete(pBRANCH_ID, null);
        }
        public static bool Delete(int pBRANCH_ID, DBContext dc)
        {
            dcBRANCH_MST cObj = new dcBRANCH_MST();
            cObj.BRANCH_ID = pBRANCH_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcBRANCH_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcBRANCH_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcBRANCH_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcBRANCH_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcBRANCH_MST cObj, DBContext dc)
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
                                newID = cObj.BRANCH_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.BRANCH_ID, dc))
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

        public static bool SaveList(List<dcBRANCH_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcBRANCH_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcBRANCH_MST oDet in detList)
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
                        bool d = Delete(oDet.BRANCH_ID, dc);
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
