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
    public class PRODUCTION_BATCH_INFOBL
    {
        public static DataLoadOptions PRODUCTION_BATCH_INFOLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPRODUCTION_BATCH_INFO>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetTempBatchInfoSql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM PROD_TEMP_BATCH_INFO WHERE 1=1 ");
            return sb.ToString();
        }

        public static string GetBatchInfoSql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT B.PROD_BATCH_ID,B.PROD_BATCH_NO,B.PROD_ID,B.PROD_NO,B.DEPT_ID,B.FG_ITEM_ID,IM.ITEM_GROUP_ID ");
            sb.Append(" ,B.ITEM_ID,B.MACHINE_ID,B.STLM_ID,B.UOM_ID CLOSING_UOM_ID,U.UOM_CODE CLOSING_UOM_NAME,B.USED_QTY,B.CLOSING_QTY SYSTEM_OPENING_STOCK ");
            sb.Append(" FROM PRODUCTION_BATCH_INFO B ");
            sb.Append(" LEFT JOIN UOM_INFO U ON B.UOM_ID=U.UOM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER IM ON B.ITEM_ID=IM.ITEM_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }

        public static string GetBatchIdSql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT B.PROD_BATCH_ID,B.PROD_ID,B.PROD_BATCH_NO,B.ITEM_ID BATCH_ITEM_ID,B.MACHINE_ID,B.FG_ITEM_ID,DTL.ITEM_ID ");
            sb.Append(" FROM PRODUCTION_BATCH_INFO B ");
            sb.Append(" LEFT JOIN PRODUCTION_DTL DTL ON B.FG_ITEM_ID=DTL.ITEM_ID AND B.PROD_ID=DTL.PROD_MST_ID AND B.MACHINE_ID=DTL.MACHINE_ID ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND DTL.ITEM_ID IS NULL ");
            return sb.ToString();
        }

        public static string GetBatchDeptInfoSql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT B.PROD_BATCH_ID,B.PROD_BATCH_NO,B.PROD_ID,B.PROD_NO,B.DEPT_ID ");
            sb.Append(" ,B.ITEM_ID,B.MACHINE_ID,B.STLM_ID,B.UOM_ID CLOSING_UOM_ID,U.UOM_CODE CLOSING_UOM_NAME,B.USED_QTY,B.CLOSING_QTY SYSTEM_OPENING_STOCK ");
            sb.Append(" FROM PRODUCTION_BATCH_INFO B ");
            sb.Append(" INNER JOIN PRODUCTION_MST PMST ON B.PROD_ID=PMST.PROD_ID ");  //new added 30-mar-23
            sb.Append(" LEFT JOIN UOM_INFO U ON B.UOM_ID=U.UOM_ID ");
            sb.Append(" WHERE 1=1  ");
            sb.Append(" AND PMST.AUTH_STATUS='N' "); //new added 30-mar-23
            return sb.ToString();
        }
        public static List<dcPRODUCTION_BATCH_INFO> GetPRODUCTION_BATCH_INFOList()
        {
            return GetPRODUCTION_BATCH_INFOList(null, null);
        }
        public static List<dcPRODUCTION_BATCH_INFO> GetPRODUCTION_BATCH_INFOList(DBContext dc)
        {
            return GetPRODUCTION_BATCH_INFOList(null, dc);
        }
        public static List<dcPRODUCTION_BATCH_INFO> GetPRODUCTION_BATCH_INFOList(DBQuery dbq, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = new List<dcPRODUCTION_BATCH_INFO>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPRODUCTION_BATCH_INFO GetPRODUCTION_BATCH_INFOByID(int pPRODUCTION_BATCH_INFOID)
        {
            return GetPRODUCTION_BATCH_INFOByID(pPRODUCTION_BATCH_INFOID, null);
        }
        public static dcPRODUCTION_BATCH_INFO GetPRODUCTION_BATCH_INFOByID(int pPRODUCTION_BATCH_INFOID, DBContext dc)
        {
            dcPRODUCTION_BATCH_INFO cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPRODUCTION_BATCH_INFO>()
                                  where c.PROD_BATCH_ID == pPRODUCTION_BATCH_INFOID
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

        public static int Insert(dcPRODUCTION_BATCH_INFO cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPRODUCTION_BATCH_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPRODUCTION_BATCH_INFO>(cObj, true);
                if (id > 0) { cObj.PROD_BATCH_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPRODUCTION_BATCH_INFO cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPRODUCTION_BATCH_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPRODUCTION_BATCH_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPRODUCTION_BATCH_INFOID)
        {
            return Delete(pPRODUCTION_BATCH_INFOID, null);
        }
        public static bool Delete(int pPRODUCTION_BATCH_INFOID, DBContext dc)
        {
            dcPRODUCTION_BATCH_INFO cObj = new dcPRODUCTION_BATCH_INFO();
            cObj.PROD_BATCH_ID = pPRODUCTION_BATCH_INFOID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_BATCH_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool DeleteByPROD_MST_ID(int pPROD_MST_ID)
        {
            return DeleteByPROD_MST_ID(pPROD_MST_ID, null);
        }
        public static bool DeleteByPROD_MST_ID(int pPROD_MST_ID, DBContext dc)
        {
            dcPRODUCTION_BATCH_INFO cObj = new dcPRODUCTION_BATCH_INFO();
            cObj.PROD_ID = pPROD_MST_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_BATCH_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool DeleteByItemID(int pPROD_MST_ID, int pItemId, int pFGItemId,int pMachineId)
        {
            return DeleteByItemID(pPROD_MST_ID, pItemId, pFGItemId, pMachineId, null);
        }
        public static bool DeleteByItemID(int pPROD_MST_ID, int pItemId,int pFGItemId,int pMachineId, DBContext dc)
        {
            dcPRODUCTION_BATCH_INFO cObj = new dcPRODUCTION_BATCH_INFO();
            cObj.PROD_ID = pPROD_MST_ID;
            cObj.ITEM_ID = pItemId;
            cObj.FG_ITEM_ID = pFGItemId;
            cObj.MACHINE_ID = pMachineId;
            //cObj.PROD_BATCH_NO = pbatchno;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_BATCH_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool DeleteByFGItemID(int pPROD_MST_ID, int pFGItemId, int pMachineId)
        {
            return DeleteByFGItemID(pPROD_MST_ID, pFGItemId, pMachineId, null);
        }
        public static bool DeleteByFGItemID(int pPROD_MST_ID, int pFGItemId, int pMachineId, DBContext dc)
        {
            dcPRODUCTION_BATCH_INFO cObj = new dcPRODUCTION_BATCH_INFO();
            cObj.PROD_ID = pPROD_MST_ID;
            cObj.FG_ITEM_ID = pFGItemId;
            cObj.MACHINE_ID = pMachineId;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_BATCH_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static void DeleteByProdItem(int prodId,int deptId, int ItemId, int machineId, int stlmId, int FGItemId, DBContext dc)
        {
            bool isDCInit = false;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " DELETE FROM PRODUCTION_BATCH_INFO WHERE 1=1 AND PROD_ID=@prodId AND DEPT_ID=@deptId AND ITEM_ID=@ItemId AND MACHINE_ID=@machineId AND STLM_ID=@stlmId AND FG_ITEM_ID=@FGItemId ";
                cmdInfo.DBParametersInfo.Add("@prodId", prodId);
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


        public static int Save(dcPRODUCTION_BATCH_INFO cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPRODUCTION_BATCH_INFO cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPRODUCTION_BATCH_INFO cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPRODUCTION_BATCH_INFO cObj, DBContext dc)
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
                                newID = cObj.PROD_BATCH_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.PROD_BATCH_ID, dc))
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

        public static bool SaveList(List<dcPRODUCTION_BATCH_INFO> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPRODUCTION_BATCH_INFO> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPRODUCTION_BATCH_INFO oDet in detList)
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
                        bool d = Delete(oDet.PROD_BATCH_ID, dc);
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

        public static List<dcPRODUCTION_BATCH_INFO> GetTempBatchDeptItemInfo(int deptId, int stlmId, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetTempBatchInfoSql_String());

                sb.Append(" AND DEPT_ID=@deptId ");
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);
                if(stlmId > 0)
                {
                    sb.Append(" AND STLM_ID=@stlmId ");
                    cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_BATCH_INFO> GetBatchInfoByProdId(int ProdId, int pMachineId, int pItem_id, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());

                sb.Append(" AND B.PROD_ID=@ProdId ");
                cmdInfo.DBParametersInfo.Add("@ProdId", ProdId);

                sb.Append(" AND B.MACHINE_ID=@pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pMachineId);

                sb.Append(" AND B.ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_BATCH_INFO> GetBatchInfoByProdId(int ProdId, int pMachineId, int pItem_id,int pFinishedItemId, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());

                sb.Append(" AND B.PROD_ID=@ProdId ");
                cmdInfo.DBParametersInfo.Add("@ProdId", ProdId);

                sb.Append(" AND B.MACHINE_ID=@pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pMachineId);

                sb.Append(" AND B.ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                sb.Append(" AND B.FG_ITEM_ID=@pFinishedItemId ");
                cmdInfo.DBParametersInfo.Add("@pFinishedItemId", pFinishedItemId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_BATCH_INFO> GetBatchInfoByProdIdforFinishedBatch(int ProdId, int pMachineId, int pFinishedItemId, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());

                sb.Append(" AND B.PROD_ID=@ProdId ");
                cmdInfo.DBParametersInfo.Add("@ProdId", ProdId);

                sb.Append(" AND B.MACHINE_ID=@pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pMachineId);

                //sb.Append(" AND ITEM_ID=@pItem_id ");
                //cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                sb.Append(" AND B.FG_ITEM_ID=@pFinishedItemId ");
                cmdInfo.DBParametersInfo.Add("@pFinishedItemId", pFinishedItemId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_BATCH_INFO> GetBatchIdListByProdId(int ProdId,DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchIdSql_String());

                sb.Append(" AND B.PROD_ID=@ProdId ");
                cmdInfo.DBParametersInfo.Add("@ProdId", ProdId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcPRODUCTION_BATCH_INFO> GetBatchDeptItemInfo(int deptId, int stlmId, int pItem_id, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchDeptInfoSql_String());

                sb.Append(" AND B.DEPT_ID=@deptId ");
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);

                sb.Append(" AND B.STLM_ID=@stlmId ");
                cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);

                sb.Append(" AND B.ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_BATCH_INFO> GetBatchInfoByItemId(int deptId, int stlmId, int pItem_id, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());

                sb.Append(" AND B.DEPT_ID=@deptId ");
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);

                sb.Append(" AND B.STLM_ID=@stlmId ");
                cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);

                sb.Append(" AND B.ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_BATCH_INFO> GetBatchInfoByProdId(int prodId,int pItem_id, DBContext dc)
        {
            List<dcPRODUCTION_BATCH_INFO> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatchInfoSql_String());


                sb.Append(" AND B.PROD_ID=@prodId ");
                cmdInfo.DBParametersInfo.Add("@prodId", prodId);

                sb.Append(" AND B.ITEM_ID=@pItem_id ");
                cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);

                //sb.Append(" AND MACHINE_ID=@pMachineId ");
                //cmdInfo.DBParametersInfo.Add("@pMachineId", pMachineId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_BATCH_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
