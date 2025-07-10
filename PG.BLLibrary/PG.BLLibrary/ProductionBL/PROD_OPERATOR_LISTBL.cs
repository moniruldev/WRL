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
    public class PROD_OPERATOR_LISTBL
    {
        public static DataLoadOptions PROD_OPERATOR_LISTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPROD_OPERATOR_LIST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetTempOperatorInfoSql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM PROD_OPERATOR_TEMP WHERE 1=1 ");
            return sb.ToString();
        }

        public static string GetOperatorInfoSql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT OP.*,MCN.MACHINE_NAME  ");
            sb.Append(" FROM PROD_OPERATOR_LIST OP ");
            sb.Append(" INNER JOIN MACHINE_MST_ASM MCN ON OP.MACHINE_ID=MCN.MACHINE_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }
        public static List<dcPROD_OPERATOR_LIST> GetPROD_OPERATOR_LISTList()
        {
            return GetPROD_OPERATOR_LISTList(null, null);
        }
        public static List<dcPROD_OPERATOR_LIST> GetPROD_OPERATOR_LISTList(DBContext dc)
        {
            return GetPROD_OPERATOR_LISTList(null, dc);
        }
        public static List<dcPROD_OPERATOR_LIST> GetPROD_OPERATOR_LISTList(DBQuery dbq, DBContext dc)
        {
            List<dcPROD_OPERATOR_LIST> cObjList = new List<dcPROD_OPERATOR_LIST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPROD_OPERATOR_LIST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPROD_OPERATOR_LIST GetPROD_OPERATOR_LISTByID(int pPROD_OPERATOR_LISTID)
        {
            return GetPROD_OPERATOR_LISTByID(pPROD_OPERATOR_LISTID, null);
        }
        public static dcPROD_OPERATOR_LIST GetPROD_OPERATOR_LISTByID(int pPROD_OPERATOR_LISTID, DBContext dc)
        {
            dcPROD_OPERATOR_LIST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPROD_OPERATOR_LIST>()
                                  where c.PROD_OP_ID == pPROD_OPERATOR_LISTID
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

        public static List<dcPROD_OPERATOR_LIST> GetTempOperatorInfo(int deptId, int stlmId, DBContext dc)
        {
            List<dcPROD_OPERATOR_LIST> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetTempOperatorInfoSql_String());

                sb.Append(" AND DEPT_ID=@deptId ");
                cmdInfo.DBParametersInfo.Add("@deptId", deptId);

                if (stlmId > 0)
                {
                    sb.Append(" AND STLM_ID=@stlmId ");
                    cmdInfo.DBParametersInfo.Add("@stlmId", stlmId);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPROD_OPERATOR_LIST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static void DeleteByProdId(int prodId, DBContext dc)
        {
            bool isDCInit = false;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " DELETE FROM PROD_OPERATOR_LIST WHERE 1=1 AND PROD_ID=@prodId ";
                cmdInfo.DBParametersInfo.Add("@prodId", prodId);

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

        public static List<dcPROD_OPERATOR_LIST> GetOperatorInfoByProdId(int ProdId, int pMachineId, int pItem_id, DBContext dc)
        {
            List<dcPROD_OPERATOR_LIST> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetOperatorInfoSql_String());

                sb.Append(" AND op.PROD_ID=@ProdId ");
                cmdInfo.DBParametersInfo.Add("@ProdId", ProdId);

                sb.Append(" AND op.REF_MACHINE_ID=@pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pMachineId);

                //sb.Append(" AND ITEM_ID=@pItem_id ");
                //cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);
                sb.Append(" ORDER BY MCN.MACHINE_ID ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPROD_OPERATOR_LIST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static int Insert(dcPROD_OPERATOR_LIST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPROD_OPERATOR_LIST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPROD_OPERATOR_LIST>(cObj, true);
                if (id > 0) { cObj.PROD_OP_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPROD_OPERATOR_LIST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPROD_OPERATOR_LIST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPROD_OPERATOR_LIST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPROD_OPERATOR_LISTID)
        {
            return Delete(pPROD_OPERATOR_LISTID, null);
        }
        public static bool Delete(int pPROD_OPERATOR_LISTID, DBContext dc)
        {
            dcPROD_OPERATOR_LIST cObj = new dcPROD_OPERATOR_LIST();
            cObj.PROD_OP_ID = pPROD_OPERATOR_LISTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPROD_OPERATOR_LIST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPROD_OPERATOR_LIST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPROD_OPERATOR_LIST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPROD_OPERATOR_LIST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPROD_OPERATOR_LIST cObj, DBContext dc)
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
                                newID = cObj.PROD_OP_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.PROD_OP_ID, dc))
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

        public static bool SaveList(List<dcPROD_OPERATOR_LIST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPROD_OPERATOR_LIST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPROD_OPERATOR_LIST oDet in detList)
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
                        bool d = Delete(oDet.PROD_OP_ID, dc);
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

        public static bool DeleteAssemblyMacineOperatorListbyProd_ID(int pPROD_MST_ID)
        {
            return DeleteAssemblyMacineOperatorListbyProd_ID(pPROD_MST_ID, null);
        }
        public static bool DeleteAssemblyMacineOperatorListbyProd_ID(int pPROD_MST_ID, DBContext dc)
        {
            dcPROD_OPERATOR_LIST cObj = new dcPROD_OPERATOR_LIST();
            cObj.PROD_ID = pPROD_MST_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPROD_OPERATOR_LIST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static string GetProductionAssemblyMOperatorSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT OP.*,MCN.MACHINE_NAME,M.MACHINE_NAME REF_MACHINE_NAME ,SM.FULL_NAME,im.ITEM_NAME  ");
            sb.Append(" FROM PROD_OPERATOR_LIST OP  ");
            sb.Append(" INNER JOIN MACHINE_MST_ASM MCN ON OP.MACHINE_ID=MCN.MACHINE_ID ");
            sb.Append(" INNER JOIN MACHINE_MST M ON OP.REF_MACHINE_ID=M.MACHINE_ID ");
            sb.Append(" INNER JOIN SUPPERVISOR_MST SM ON OP.EMP_ID=SM.EMP_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER im ON OP.ITEM_ID=im.ITEM_ID ");
             
           sb.Append(" WHERE 1=1  ");

           
            return sb.ToString();
        }




        public static List<dcPROD_OPERATOR_LIST> GetProductionAssemblyMOperatorByProdID(int pProd_id, DBContext dc)
        {
            List<dcPROD_OPERATOR_LIST> cObjList = new List<dcPROD_OPERATOR_LIST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionAssemblyMOperatorSQLString());

                    // StringBuilder sb = new StringBuilder(GetProductionClosingDtlsWithDeptIDSQLString());
                    if (pProd_id > 0)
                    {
                        sb.Append("  AND OP.PROD_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    //sb.Append(" ORDER BY  cl.CLOSING_SI, finishItem.ITEM_NAME,itm.ITEM_NAME ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPROD_OPERATOR_LIST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



    }
}
