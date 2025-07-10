using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
    public class DEPARTMENT_INFOBL
    {
        public static string GetDepartment_Info_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select DEPARTMENT_INFO.* FROM DEPARTMENT_INFO where 1=1 AND DEPARTMENT_INFO.IS_ACTIVE='Y' ");
            return sb.ToString();
        }


        public static string GetDepartmentByUserId_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT TBLUSERDEPARTMENT.*,DEPARTMENT_INFO.DEPARTMENT_NAME,DEPARTMENT_INFO.DEPARTMENT_CODE,DEPARTMENT_INFO.DEPARTMENT_ID ");
            sb.Append(" FROM TBLUSERDEPARTMENT ");
            sb.Append(" inner join DEPARTMENT_INFO on TBLUSERDEPARTMENT.DEPTID=DEPARTMENT_INFO.DEPARTMENT_ID ");
            sb.Append(" WHERE (1=1) ");
            return sb.ToString();
        }
        public static List<dcDEPARTMENT_INFO> Department_Info_List()
        {
            return Department_Info_List(null, null, null);
        }
        public static List<dcDEPARTMENT_INFO> Department_Info_List(DBContext dc)
        {
            return Department_Info_List(null, dc, null);
        }
        public static List<dcDEPARTMENT_INFO> Department_Info_List(string isStore)
        {
            return Department_Info_List(null, null, isStore);
        }
        public static List<dcDEPARTMENT_INFO> Department_Info_List(DBQuery dbq, DBContext dc, string isStore)
        {
            List<dcDEPARTMENT_INFO> cObjList = new List<dcDEPARTMENT_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetDepartment_Info_SQLString());
                    if (!String.IsNullOrEmpty(isStore))
                    {
                        sb.Append("and IS_STORE=@IsStore");
                        cmdInfo.DBParametersInfo.Add("@IsStore", isStore);
                    }

                    sb.Append(" order by DEPARTMENT_NAME asc ");

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcDEPARTMENT_INFO Department_Info_ByDeptId(DBContext dc, int deptId)
        {
            dcDEPARTMENT_INFO cObj = new dcDEPARTMENT_INFO();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetDepartment_Info_SQLString());
                    if (deptId > 0)
                    {
                        sb.Append("and DEPARTMENT_ID=@deptId");
                        cmdInfo.DBParametersInfo.Add("@deptId", deptId);
                    }

                    sb.Append(" order by DEPARTMENT_NAME asc ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObj = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc).FirstOrDefault();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcDEPARTMENT_INFO Department_Info_ByAltDeptName(DBContext dc, string deptName)
        {
            dcDEPARTMENT_INFO cObj = new dcDEPARTMENT_INFO();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Select DEPARTMENT_INFO.* FROM DEPARTMENT_INFO where 1=1 ");

                    if (deptName != string.Empty)
                    {
                        sb.Append("and ALTERNATIVE_DEPT_NAME=@deptName");
                        cmdInfo.DBParametersInfo.Add("@deptName", deptName);
                    }

                    sb.Append(" order by DEPARTMENT_NAME asc ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObj = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc).FirstOrDefault();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static List<dcDEPARTMENT_INFO> Department_Info_List(DBQuery dbq, DBContext dc)
        {
            List<dcDEPARTMENT_INFO> cObjList = new List<dcDEPARTMENT_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetDepartment_Info_SQLString());
                    //if (!String.IsNullOrEmpty(isStore))
                    //{
                    //    sb.Append("and IS_STORE=@IsStore");
                    //    cmdInfo.DBParametersInfo.Add("@IsStore", isStore);
                    //}

                    //sb.Append(" order by DEPARTMENT_NAME asc ");

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcDEPARTMENT_INFO> Department_List(DBQuery dbq, DBContext dc)
        {
            List<dcDEPARTMENT_INFO> cObjList = new List<dcDEPARTMENT_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    //StringBuilder sb = new StringBuilder(GetDepartment_Info_SQLString());
                    //if (!String.IsNullOrEmpty(isStore))
                    //{
                    //    sb.Append("and IS_STORE=@IsStore");
                    //    cmdInfo.DBParametersInfo.Add("@IsStore", isStore);
                    //}

                    //sb.Append(" order by DEPARTMENT_NAME asc ");

                    //dbq = new DBQuery();
                    //dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    ////cmdInfo.CommandText = sb.ToString();
                    ////cmdInfo.CommandType = CommandType.Text;
                    //dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcDEPARTMENT_INFO> UserDepartment_List(int userId, out bool hasItem)
        {
            hasItem = false;
            List<dcDEPARTMENT_INFO> deptList = new List<dcDEPARTMENT_INFO>();
            if (userId > 0)
            {
                List<dcTBLUSERDEPARTMENT> userDept = TBLUSERDEPARTMENTBL.GetDepartmentByUserId(Convert.ToInt32(userId));
                deptList = userDept.Select(x => new dcDEPARTMENT_INFO()
                {

                    DEPARTMENT_ID = x.DEPARTMENT_ID,
                    DEPARTMENT_NAME = x.DEPARTMENT_NAME,
                    DEPARTMENT_CODE = x.DEPARTMENT_CODE,
                    IS_STORE = x.IS_STORE
                }).ToList();

            }
            else
            {

                deptList = Department_Info_List();
            }
            if (deptList.Count() > 1)
            {
                hasItem = true;
            }

            return deptList;
        }




        public static dcDEPARTMENT_INFO Get_Company_Info_By_Id(int id)
        {
            return Get_Company_Info_By_Id(id, null);
        }
        public static dcDEPARTMENT_INFO Get_Company_Info_By_Id(int id, DBContext dc)
        {
            dcDEPARTMENT_INFO cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcDEPARTMENT_INFO>()
                                  //where c.ELECTROLYTE_GRAVITYID == pELECTROLYTE_GRAVITYID
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

        public static int Insert(dcDEPARTMENT_INFO cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcDEPARTMENT_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcDEPARTMENT_INFO>(cObj, false);
                //if (id > 0) { cObj.ELECTROLYTE_GRAVITYID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcDEPARTMENT_INFO cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcDEPARTMENT_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcDEPARTMENT_INFO key = new dcDEPARTMENT_INFO();
            key.COMPANY_ID = cObj.COMPANY_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcDEPARTMENT_INFO>(cObj, key);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int id)
        {
            return Delete(id, null);
        }
        public static bool Delete(int id, DBContext dc)
        {
            dcDEPARTMENT_INFO cObj = new dcDEPARTMENT_INFO();
            //cObj.ELECTROLYTE_GRAVITYID = pELECTROLYTE_GRAVITYID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcDEPARTMENT_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcDEPARTMENT_INFO cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcDEPARTMENT_INFO cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcDEPARTMENT_INFO cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcDEPARTMENT_INFO cObj, DBContext dc)
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

        public static bool SaveList(List<dcDEPARTMENT_INFO> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcDEPARTMENT_INFO> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcDEPARTMENT_INFO oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }


        public static List<dcDEPARTMENT_INFO> GetStorageLocationList(int pDeptId, DBContext dc)
        {
            List<dcDEPARTMENT_INFO> cObjList = new List<dcDEPARTMENT_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT STLM.STLM_ID,STLM.NAME,STLM.CODE,STLM.DEPT_ID DEPARTMENT_ID,STLM.SHORT_CODE,STLM.IS_BATCH_USABLE FROM STORAGE_LOCATION_MST STLM ");
                    sb.Append(" WHERE 1=1 ");
                    if (pDeptId > 0)
                    {
                        sb.Append("AND STLM.DEPT_ID=@pDeptId");
                        cmdInfo.DBParametersInfo.Add("@pDeptId", pDeptId);
                    }

                    sb.Append(" ORDER BY STLM.NAME ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcDEPARTMENT_INFO> GetStorageLocationListByStock(int pDeptId, int pItemId, DBContext dc)
        {
            List<dcDEPARTMENT_INFO> cObjList = new List<dcDEPARTMENT_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT DISTINCT STK.ITEM_ID,  STK.STLM_ID,STLM.NAME  ");
                    sb.Append(" FROM ITEM_STOCK_DETAILS STK ");
                    sb.Append(" INNER JOIN STORAGE_LOCATION_MST STLM ON STK.STLM_ID=STLM.STLM_ID ");
                    sb.Append(" WHERE 1=1 ");
                    sb.Append(" AND (SELECT GET_DEPT_REJECT_CLOSING_QTY(STK.ITEM_ID,STK.DEPARTMENT_ID,STK.STLM_ID) FROM DUAL) > 0 ");

                    if (pDeptId > 0)
                    {
                        sb.Append(" AND STK.DEPARTMENT_ID=@pDeptId ");
                        cmdInfo.DBParametersInfo.Add("@pDeptId", pDeptId);
                    }

                    if (pDeptId > 0)
                    {
                        sb.Append(" AND STK.ITEM_ID=@pItemId ");
                        cmdInfo.DBParametersInfo.Add("@pItemId", pItemId);
                    }

                    sb.Append(" ORDER BY STLM.NAME ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcDEPARTMENT_INFO GetStorageLocationById(int pStlmId, DBContext dc)
        {
            dcDEPARTMENT_INFO cObj = new dcDEPARTMENT_INFO();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT * FROM STORAGE_LOCATION_MST ");
                    sb.Append(" WHERE 1=1 ");

                    if (pStlmId > 0)
                    {
                        sb.Append("AND STLM_ID=@pStlmId");
                        cmdInfo.DBParametersInfo.Add("@pStlmId", pStlmId);
                    }

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObj = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc).FirstOrDefault();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }
    }
}
