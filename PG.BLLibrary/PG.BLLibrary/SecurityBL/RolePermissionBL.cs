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

    public class RolePermissionBL
    {

        public static string GetRolePermission_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from tblRole  INNER JOIN ");
            sb.Append("tblRolePermission  ON tblRole.RoleID=tblRolePermission.RoleID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcRolePermission> GetRolePermissionList(int pAppID)
        {
            return GetRolePermissionList(pAppID, null);
        }
        public static List<dcRolePermission> GetRolePermissionList(int pAppID, DBContext dc)
        {
            List<dcRolePermission> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder(GetRolePermission_SQLString());
                sb.Append(" AND tblRolePermission.RoleID=tblRole.RoleID AND tblRole.AppID=@appID");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRolePermission>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{

                //    cObjList = (from c in dataContext.GetTable<dcRolePermission>()
                //                from r in dataContext.GetTable<dcRole>()
                //                where c.RoleID == r.RoleID && r.AppID == pAppID
                //                select c).ToList();
                 
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcRolePermission> GetRolePermissionListByRole(int pRoleID)
        {
            return GetRolePermissionListByRole(pRoleID, null);
        }
        public static List<dcRolePermission> GetRolePermissionListByRole(int pRoleID, DBContext dc)
        {

            List<dcRolePermission> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select * from tblRolePermission Where tblRolePermission.RoleID=@rollID ";
                //StringBuilder sb = new StringBuilder(GetRole_SQLString());
                //sb.Append(" AND tblRole.RoleID=@rollID  AND tblRole. ");
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);
                

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRolePermission>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcRolePermission>()
                //                where c.RoleID == pRoleID
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcRolePermission> GetRolePermissionList(DBQuery dbq, DBContext dc)
        {
            List<dcRolePermission> cObjList = new List<dcRolePermission>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select * from tblRolePermission ";
                //StringBuilder sb = new StringBuilder(GetRole_SQLString());
                //sb.Append(" AND tblRole.RoleID=@rollID  AND tblRole. ");
                //cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);


                //DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRolePermission>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "PeriodStartDate";
                //    }
                //    cObjList = DBQuery.ExecuteDBQuery<dcRolePermission>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcRolePermission GetRolePermission(int pRoleKey, int pObjectKey)
        {
            return GetRolePermission(pRoleKey, pObjectKey, null);
        }
        public static dcRolePermission GetRolePermission(int pRoleID, int pAppObjectID, DBContext dc)
        {
            dcRolePermission cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select * from tblRolePermission Where tblRolePermission.RoleID=@rollID AND tblRolePermission.AppObjectID=@appObjectID";
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);
                cmdInfo.DBParametersInfo.Add("@appObjectID", pAppObjectID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcRolePermission>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcRolePermission>()
                //                  where c.RoleID == pRoleID && c.AppObjectID == pAppObjectID
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

        public static List<dcRolePermission> GetRolePermissionListWithAppObject(int pAppID, int pRoleKey, DBContext dc)
        {
            List<dcRolePermission> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                List<dcAppObject> objList = AppObjectsBL.GetAppObjectList(pAppID,true, dc);
                List<dcRolePermission> roleList = GetRolePermissionList(pRoleKey,dc);
                if (objList.Count > 0)
                {
                    cObjList = new List<dcRolePermission>();
                    foreach (dcAppObject obj in objList)
                    {

                        //DBClass.SystemDC.dcRolePermission role = new DBClass.SystemDC.dcRolePermission();
                        //role.a


                    }

                }



                //if (pAppID > 0)
                //{
                //    cObjList = (from c in dc.DBDataContext.GetTable<DBClass.SystemDC.dcRolePermission>()
                //                from r in dc.DBDataContext.GetTable<DBClass.SystemDC.dcRoles>()
                //                where c.RoleKey == r.RoleKey && r.AppID == pAppID
                //                select c).ToList();
                //}
                //else
                //{
                //    cObjList = (from c in dc.DBDataContext.GetTable<DBClass.SystemDC.dcRolePermission>()
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static bool UpdateRolePermission(dcRolePermission cObj)
        {
            return UpdateRolePermission(cObj, null);
        }

        public static bool UpdateRolePermission(dcRolePermission cObj, DBContext dc)
        {
            if (cObj.RoleID == 0 || cObj.AppObjectID == 0)
            {
                return false;
            }
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            dcRolePermission cObjN = GetRolePermission(cObj.RoleID, cObj.AppObjectID, dc);

            if (cObjN == null)
            {
                cObjN = new dcRolePermission();
                cObjN.RoleID = cObj.RoleID;
                cObjN.AppObjectID = cObj.AppObjectID;
                cObjN.Permission = cObj.Permission;
                cnt = dc.DoInsert<dcRolePermission>(cObjN);
            }
            else
            {
                cObjN.Permission = cObj.Permission;
                cnt = dc.DoUpdate<dcRolePermission>(cObjN);
            }

            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }


        public static bool UpdateRolePermission(List<dcRolePermission> cList)
        {
            return UpdateRolePermission(cList, null);
        }
        public static bool UpdateRolePermission(List<dcRolePermission> cList,  DBContext dc)
        {
            bool bSataus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            foreach (dcRolePermission cObj in cList)
            {
                bSataus = UpdateRolePermission(cObj, dc);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bSataus = true;
            return bSataus;
        }

        public static bool CopyRolePermission(int pFromRoleID, int pToRoleID)
        {
            return CopyRolePermission(pFromRoleID, pToRoleID, null);
        }
        public static bool CopyRolePermission(int pFromRoleID, int pToRoleID, DBContext dc)
        {
            bool bSataus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            List<dcRolePermission> frList = GetRolePermissionListByRole(pFromRoleID,dc);

            List<dcRolePermission> toList = new List<dcRolePermission>();

            foreach (dcRolePermission fObj in frList)
            {
                dcRolePermission tObj = new dcRolePermission();
                tObj.RoleID = pToRoleID;
                tObj.AppObjectID = fObj.AppObjectID;
                tObj.Permission = fObj.Permission;
                toList.Add(tObj);
            }
            UpdateRolePermission(toList, dc);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bSataus = true;
            return bSataus;
        }

        public static bool ClearRolePermission(int pRoleID, DBContext dc)
        {
            bool bSataus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);


            dcRolePermission updObj = new dcRolePermission();
            updObj.Permission = 0;

            dcRolePermission whreObj = new dcRolePermission();
            whreObj.RoleID = pRoleID;

            int cnt = dc.DoUpdate<dcRolePermission>(updObj, whreObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bSataus = true;
            return bSataus;
        }

       

    }
}
