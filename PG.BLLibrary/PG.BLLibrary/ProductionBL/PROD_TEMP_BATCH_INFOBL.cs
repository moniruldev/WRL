using PG.Core.DBBase;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.ProductionBL
{
    public class PROD_TEMP_BATCH_INFOBL
    {
        public static DataLoadOptions PROD_TEMP_BATCH_INFOLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPROD_TEMP_BATCH_INFO>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetBatchInfoSql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM PROD_TEMP_BATCH_INFO WHERE 1=1 ");
            return sb.ToString();
        }
        public static List<dcPROD_TEMP_BATCH_INFO> GetPROD_TEMP_BATCH_INFOList()
        {
            return GetPROD_TEMP_BATCH_INFOList(null, null);
        }
        public static List<dcPROD_TEMP_BATCH_INFO> GetPROD_TEMP_BATCH_INFOList(DBContext dc)
        {
            return GetPROD_TEMP_BATCH_INFOList(null, dc);
        }
        public static List<dcPROD_TEMP_BATCH_INFO> GetPROD_TEMP_BATCH_INFOList(DBQuery dbq, DBContext dc)
        {
            List<dcPROD_TEMP_BATCH_INFO> cObjList = new List<dcPROD_TEMP_BATCH_INFO>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPROD_TEMP_BATCH_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPROD_TEMP_BATCH_INFO GetPROD_TEMP_BATCH_INFOByID(int pPROD_TEMP_BATCH_INFOID)
        {
            return GetPROD_TEMP_BATCH_INFOByID(pPROD_TEMP_BATCH_INFOID, null);
        }
        public static dcPROD_TEMP_BATCH_INFO GetPROD_TEMP_BATCH_INFOByID(int pPROD_TEMP_BATCH_INFOID, DBContext dc)
        {
            dcPROD_TEMP_BATCH_INFO cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPROD_TEMP_BATCH_INFO>()
                                  where c.ITEM_ID == pPROD_TEMP_BATCH_INFOID
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

        public static int Insert(dcPROD_TEMP_BATCH_INFO cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPROD_TEMP_BATCH_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPROD_TEMP_BATCH_INFO>(cObj, false);
                if (id > 0) { cObj.ITEM_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPROD_TEMP_BATCH_INFO cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPROD_TEMP_BATCH_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPROD_TEMP_BATCH_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPROD_TEMP_BATCH_INFOID)
        {
            return Delete(pPROD_TEMP_BATCH_INFOID, null);
        }
        public static bool Delete(int pPROD_TEMP_BATCH_INFOID, DBContext dc)
        {
            dcPROD_TEMP_BATCH_INFO cObj = new dcPROD_TEMP_BATCH_INFO();
            cObj.ITEM_ID = pPROD_TEMP_BATCH_INFOID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPROD_TEMP_BATCH_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static void DeleteByItem(int deptId,int ItemId,int machineId,int stlmId,DBContext dc)
        {
            bool isDCInit = false;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " DELETE FROM PROD_TEMP_BATCH_INFO WHERE 1=1 AND DEPT_ID=@deptId AND ITEM_ID=@ItemId AND MACHINE_ID=@machineId AND STLM_ID=@stlmId ";
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);
                cmdInfo.DBParametersInfo.Add("@ItemId", ItemId);
                cmdInfo.DBParametersInfo.Add("@machineId", machineId);
                cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        }

        public static void DeleteByItem(int deptId, int ItemId, int machineId, int stlmId,int FGItemId, DBContext dc)
        {
            bool isDCInit = false;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " DELETE FROM PROD_TEMP_BATCH_INFO WHERE 1=1 AND DEPT_ID=@deptId AND ITEM_ID=@ItemId AND MACHINE_ID=@machineId AND STLM_ID=@stlmId AND FG_ITEM_ID=@FGItemId ";
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);
                cmdInfo.DBParametersInfo.Add("@ItemId", ItemId);
                cmdInfo.DBParametersInfo.Add("@machineId", machineId);
                cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);
                cmdInfo.DBParametersInfo.Add("@FGItemId", FGItemId);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        }

        public static void DeleteByStorageLocation(int deptId,int stlmId, DBContext dc)
        {
            bool isDCInit = false;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " DELETE FROM PROD_TEMP_BATCH_INFO WHERE 1=1 AND DEPT_ID=@deptId AND STLM_ID=@stlmId ";
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);
                cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        }

        public static bool DeleteByFGItemID(int pDeptId,int pStlmId, int pFGItemId, int pMachineId)
        {
            return DeleteByFGItemID(pDeptId, pStlmId,pFGItemId, pMachineId, null);
        }
        public static bool DeleteByFGItemID(int pDeptId,int pStlmId, int pFGItemId, int pMachineId, DBContext dc)
        {
            dcPROD_BATT_BREAKING_TEMP_BATCH cObj = new dcPROD_BATT_BREAKING_TEMP_BATCH();
            cObj.DEPT_ID = pDeptId;
            cObj.STLM_ID = pStlmId;
            cObj.FG_ITEM_ID = pFGItemId;
            cObj.MACHINE_ID = pMachineId;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPROD_BATT_BREAKING_TEMP_BATCH>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPROD_TEMP_BATCH_INFO cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPROD_TEMP_BATCH_INFO cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPROD_TEMP_BATCH_INFO cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPROD_TEMP_BATCH_INFO cObj, DBContext dc)
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
                                newID = cObj.ITEM_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ITEM_ID, dc))
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

        public static bool SaveList(List<dcPROD_TEMP_BATCH_INFO> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPROD_TEMP_BATCH_INFO> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPROD_TEMP_BATCH_INFO oDet in detList)
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
                        bool d = Delete(oDet.ITEM_ID, dc);
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

        public static List<dcPROD_TEMP_BATCH_INFO> GetTempBatchInfo(int pMachineId, int pItem_id, DBContext dc)
        {
            List<dcPROD_TEMP_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());

                sb.Append(" AND MACHINE_ID=@pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pMachineId);

                sb.Append(" AND ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPROD_TEMP_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPROD_TEMP_BATCH_INFO> GetTempBatchInfo(int pMachineId, int pItem_id,int pFgItemId, DBContext dc)
        {
            List<dcPROD_TEMP_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());

                sb.Append(" AND MACHINE_ID=@pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pMachineId);

                sb.Append(" AND ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                sb.Append(" AND FG_ITEM_ID=@pFgItemId ");
                cmdInfo.DBParametersInfo.Add("@pFgItemId", pFgItemId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPROD_TEMP_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPROD_TEMP_BATCH_INFO> GetTempBatchDeptItemInfo(int deptId, int stlmId, int pItem_id, DBContext dc)
        {
            List<dcPROD_TEMP_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());

                sb.Append(" AND DEPT_ID=@deptId ");
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);

                sb.Append(" AND STLM_ID=@stlmId ");
                cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);

                sb.Append(" AND ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPROD_TEMP_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPROD_TEMP_BATCH_INFO> GetFinalBatchDeptItemInfo(int deptId, int stlmId, int pItem_id,int prodId, DBContext dc)
        {
            List<dcPROD_TEMP_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT * FROM PRODUCTION_BATCH_INFO WHERE 1=1 ");

                sb.Append(" AND DEPT_ID=@deptId ");
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);

                sb.Append(" AND STLM_ID=@stlmId ");
                cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);

                sb.Append(" AND ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                sb.Append(" AND PROD_ID=@prodId ");
                cmdInfo.DBParametersInfo.Add("@prodId", prodId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPROD_TEMP_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

      
    }
}
