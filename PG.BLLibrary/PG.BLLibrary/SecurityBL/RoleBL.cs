using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.Extentions;
using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.DBClass.SecurityDC;

namespace PG.BLLibrary.SecurityBL
{
    /// <summary>
    /// AppMenuBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    public class RoleBL
    {
        public static string GetRole_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblRole.* ");
            sb.Append(" FROM tblRole ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions RoleLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
            return dlo;
        }

        public static List<dcRole> GetRoleList()
        {
            return GetRoleList(0,null);
        }

        public static List<dcRole> GetRoleList(int pAppID)
        {
            return GetRoleList(pAppID, null);
        }

        public static List<dcRole> GetRoleList(int pAppID, DBContext dc)
        {
            DBQuery dbq = new DBQuery();
            if (pAppID > 0)
            {
                dbq.DBFilterList.Add(new DBFilter("AppID", pAppID));
            }
            return GetRoleList(dbq, dc);
        }


        public static List<dcRole> GetRoleList(DBQuery dbq, DBContext dc)
        {
            List<dcRole> cObjList = new List<dcRole>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRole_SQLString());

                //DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRole>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "PeriodStartDate";
                //    }


                //    cObjList = DBQuery.ExecuteDBQuery<dcRole>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


    public static dcRole GetRoleByRoleName(int pAppID, string pRoleName)
        {
            return GetRoleByRoleName(pAppID, pRoleName, null);
        }
        public static dcRole GetRoleByRoleName(int pAppID, string pRoleName, DBContext dc)
        {
            dcRole cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);


                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRole_SQLString());
                sb.Append(" AND tblRole.AppID=@appID AND tblRole.RoleName=@RoleName");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);
                cmdInfo.DBParametersInfo.Add("@RoleName", pRoleName);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcRole>(dbq, dc).FirstOrDefault();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcRole>()
                //                  where c.AppID == pAppID && c.RoleName == pRoleName
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

        public static dcRole GetRoleByRoleID(int pRoleID)
        {
            return GetRoleByRoleID(pRoleID, null);
        }
        public static dcRole GetRoleByRoleID(int pRoleID, DBContext dc)
        {
            dcRole cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRole_SQLString());
                sb.Append(" AND tblRole.RoleID=@rollID");
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);
             

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcRole>(dbq, dc).FirstOrDefault();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcRole>()
                //                  where c.RoleID == pRoleID
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


        public static bool IsRoleHasRefrenece(int pRoleID)
        {
            return IsRoleHasRefrenece(pRoleID, null);
        }
        public static bool IsRoleHasRefrenece(int pRoleID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(RoleID) tcount from tblRole Where tblRole.RoleID=@rollID";
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcRole>()
                //                 where cObj.RoleID == pRoleID
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


        public static bool IsRoleExists(int AppID, string pRoleName)
        {
            return IsRoleExists(AppID, pRoleName, null);
        }
        public static bool IsRoleExists(int AppID, string pRoleName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(RoleID) tcount from tblRole Where tblRole.AppID=@appID AND tblRole.RoleName=@RoleName";
                cmdInfo.DBParametersInfo.Add("@appID", AppID);
                cmdInfo.DBParametersInfo.Add("@RoleName", pRoleName);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcRole>()
                //                 where cObj.AppID == AppID && cObj.RoleName == pRoleName
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
        public static bool IsRoleExists(int AppID, string pRoleName, int pRoleID)
        {
            return IsRoleExists(AppID, pRoleName, pRoleID, null);
        }
        public static bool IsRoleExists(int AppID, string pRoleName, int pRoleID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(RoleID) tcount from tblRole Where tblRole.RoleID<>@rollID AND tblRole.AppID=@appID AND tblRole.RoleName=@RoleName";
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);
                cmdInfo.DBParametersInfo.Add("@appID", AppID);
                cmdInfo.DBParametersInfo.Add("@RoleName", pRoleName);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcRole>()
                //                 where cObj.RoleID != pRoleID && cObj.AppID == AppID && cObj.RoleName == pRoleName
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

        public static int Insert(dcRole cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcRole cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int id = dc.DoInsert<dcRole>(cObj, true);
            if (id > 0) { cObj.RoleID = id; }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcRole cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcRole cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoUpdate<dcRole>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int AppID, int pRoleID)
        {
            return Delete(AppID, pRoleID, null);
        }
        public static bool Delete(int AppID, int pRoleID, DBContext dc)
        {
            dcRole cObj = new dcRole();
            cObj.AppID = AppID;
            cObj.RoleID = pRoleID;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoDelete<dcRole>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static List<dcRolePermission> GetRolePermissionList(int pRoleID, int pAppObjectID)
        {
            return GetRolePermissionList(pRoleID, pAppObjectID, null);
        }
        public static List<dcRolePermission> GetRolePermissionList(int pRoleID, int pAppObjectID, DBContext dc)
        {
            List<dcRolePermission> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select * from tblRolePermission Where tblRolePermission.RoleID=@rollID AND tblRolePermission.AppObjectID=@appObjectID";
                //StringBuilder sb = new StringBuilder(GetRole_SQLString());
                //sb.Append(" AND tblRole.RoleID=@rollID  AND tblRole. ");
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);
                cmdInfo.DBParametersInfo.Add("@appObjectID", pAppObjectID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRolePermission>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcRolePermission>()
                //                where c.RoleID == pRoleID && c.AppObjectID == pAppObjectID
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


    }
}
