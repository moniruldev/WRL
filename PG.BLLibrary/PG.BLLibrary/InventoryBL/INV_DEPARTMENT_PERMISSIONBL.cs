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
    public static class INV_DEPARTMENT_PERMISSIONBL
    {
        public static string Department_Permission_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" deptInfo.DEPARTMENT_ID,deptInfo.DEPARTMENT_NAME,deptInfo.DEPARTMENT_CODE ");
            sb.Append(" ,deptPermission.PERMISSION_ID ");
           //sb.Append(" ,(CASE WHEN (deptInfo.DEPARTMENT_ID = deptPermission.TO_DEPARTMENT_ID AND deptPermission.FROM_DEPARTMENT_ID=@deptId1 ) THEN  deptPermission.PERMISSION_ID ELSE null END)as PERMISSION_ID  ");
           sb.Append(" ,deptPermission.FROM_DEPARTMENT_ID,deptPermission.TO_DEPARTMENT_ID ");
            sb.Append(" ,(CASE WHEN (deptInfo.DEPARTMENT_ID = deptPermission.TO_DEPARTMENT_ID AND deptPermission.FROM_DEPARTMENT_ID=@deptId1 ) THEN 1 ELSE 0 END)as IS_ASSIGNED ");
            sb.Append(" from  ");
            sb.Append(" DEPARTMENT_INFO deptInfo LEFT JOIN INV_DEPARTMENT_PERMISSION deptPermission ");
            sb.Append(" on deptInfo.DEPARTMENT_ID=deptPermission.TO_DEPARTMENT_ID AND  deptPermission.FROM_DEPARTMENT_ID=@deptId");
            //  sb.Append(" on deptInfo.DEPARTMENT_ID=deptPermission.TO_DEPARTMENT_ID AND deptPermission.FROM_DEPARTMENT_ID=@deptId ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static string Department_Only_Permission_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" deptInfo.DEPARTMENT_ID,deptInfo.DEPARTMENT_NAME,deptInfo.DEPARTMENT_CODE ");
            sb.Append(" ,deptPermission.PERMISSION_ID ");
            //sb.Append(" ,(CASE WHEN (deptInfo.DEPARTMENT_ID = deptPermission.TO_DEPARTMENT_ID AND deptPermission.FROM_DEPARTMENT_ID=@deptId1 ) THEN  deptPermission.PERMISSION_ID ELSE null END)as PERMISSION_ID  ");
            sb.Append(" ,deptPermission.FROM_DEPARTMENT_ID,deptPermission.TO_DEPARTMENT_ID ");
            sb.Append(" , 1 IS_ASSIGNED ");
            sb.Append(" from  ");
            sb.Append(" DEPARTMENT_INFO deptInfo LEFT JOIN INV_DEPARTMENT_PERMISSION deptPermission ");
            sb.Append(" on deptInfo.DEPARTMENT_ID=deptPermission.TO_DEPARTMENT_ID");
            //  AND  deptPermission.FROM_DEPARTMENT_ID=@deptId
            //  sb.Append(" on deptInfo.DEPARTMENT_ID=deptPermission.TO_DEPARTMENT_ID AND deptPermission.FROM_DEPARTMENT_ID=@deptId ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static List<dcINV_DEPARTMENT_PERMISSION> Department_Permission_List()
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(Department_Permission_SQLString());
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            return Department_Permission_List(dbq, null);
        }


     


        public static List<dcINV_DEPARTMENT_PERMISSION> Department_Permission_List(DBContext dc)
        {
            return Department_Permission_List(null, dc);
        }

        public static List<dcINV_DEPARTMENT_PERMISSION> Department_Permission_List(DBQuery dbq, DBContext dc)
        {
            List<dcINV_DEPARTMENT_PERMISSION> cObjList = new List<dcINV_DEPARTMENT_PERMISSION>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                    }

                    cObjList = DBQuery.ExecuteDBQuery<dcINV_DEPARTMENT_PERMISSION>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcINV_DEPARTMENT_PERMISSION Get_Company_Info_By_Id(int id)
        {
            return Get_Company_Info_By_Id(id, null);
        }
        public static dcINV_DEPARTMENT_PERMISSION Get_Company_Info_By_Id(int id, DBContext dc)
        {
            dcINV_DEPARTMENT_PERMISSION cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcINV_DEPARTMENT_PERMISSION>()
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

        public static int Insert(dcINV_DEPARTMENT_PERMISSION cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcINV_DEPARTMENT_PERMISSION cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcINV_DEPARTMENT_PERMISSION>(cObj, true);
                if (id > 0) { cObj.PERMISSION_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcINV_DEPARTMENT_PERMISSION cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcINV_DEPARTMENT_PERMISSION cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcINV_DEPARTMENT_PERMISSION key = new dcINV_DEPARTMENT_PERMISSION();
            key.PERMISSION_ID = cObj.PERMISSION_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcINV_DEPARTMENT_PERMISSION>(cObj, key);
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
            dcINV_DEPARTMENT_PERMISSION cObj = new dcINV_DEPARTMENT_PERMISSION();
            cObj.PERMISSION_ID = id;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcINV_DEPARTMENT_PERMISSION>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcINV_DEPARTMENT_PERMISSION cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcINV_DEPARTMENT_PERMISSION cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcINV_DEPARTMENT_PERMISSION cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcINV_DEPARTMENT_PERMISSION cObj, DBContext dc)
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

        public static bool SaveList(List<dcINV_DEPARTMENT_PERMISSION> detList)
        {
            return SaveList(detList, null);
        }


      


        public static bool SaveList(List<dcINV_DEPARTMENT_PERMISSION> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcINV_DEPARTMENT_PERMISSION oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case RecordStateEnum.Added:
                        INV_DEPARTMENT_PERMISSIONBL.Insert(oDet, dc);
                        break;
                    case RecordStateEnum.Edited:
                        INV_DEPARTMENT_PERMISSIONBL.Update(oDet, dc);
                        break;
                    case RecordStateEnum.Deleted:
                        INV_DEPARTMENT_PERMISSIONBL.Delete(oDet.PERMISSION_ID, dc);
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


        public static List<dcINV_DEPARTMENT_PERMISSION> User_Dept_List(int deptId)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(Department_Only_Permission_SQLString());
            sb.Append(" AND deptPermission.FROM_DEPARTMENT_ID= @P_FROM_DEPARTMENT_ID");
            cmdInfo.DBParametersInfo.Add("@P_FROM_DEPARTMENT_ID", deptId);
            //cmdInfo.DBParametersInfo.Add("@deptId", deptId);
           
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            return Department_Permission_List(dbq, null);
        }


        public static List<dcDEPARTMENT_INFO> UserDepartment_List(int deptId, out bool hasItem)
        {
            hasItem = false;
            List<dcDEPARTMENT_INFO> deptList = new List<dcDEPARTMENT_INFO>();
            if (deptId > 0)
            {
                List<dcINV_DEPARTMENT_PERMISSION> userDept = User_Dept_List(deptId);
                deptList = userDept.Select(x => new dcDEPARTMENT_INFO()
                {

                    DEPARTMENT_ID = x.DEPARTMENT_ID,
                    DEPARTMENT_NAME = x.DEPARTMENT_NAME,
                    DEPARTMENT_CODE = x.DEPARTMENT_CODE
                }).ToList();

            }

            if (deptList.Count() > 1)
            {
                hasItem = true;
            }

            return deptList;
        }

    }
}
