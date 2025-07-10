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
using PG.DBClass.OrganiztionDC;
using PG.DBClass.SecurityDC;
using PG.BLLibrary.SecurityBL;


namespace PG.BLLibrary.OrganizationBL
{
    /// <summary>
    /// AppObjectsBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    
    public class LocationUserBL
    {
        public static string GetLocationUser_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT tblLocationUser.* ");
            sb.Append(" ,tblLocation.LocationCode, tblLocation.LocationName , tblUser.UserName  ");
            sb.Append(" FROM tblLocationUser ");
            sb.Append(" INNER JOIN tblLocation ON tblLocationUser.LocationID = tblLocation.LocationID ");
            sb.Append(" INNER JOIN tblUser ON tblLocationUser.UserID = tblUser.UserID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        //public static string GetLocationUserName_SQLString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" select * from tblLocation ");
        //    sb.Append(" INNER JOIN tblLocationUser ON tblLocation.LocationID=tblLocationUser.LocationID ");
        //    sb.Append(" INNER JOIN tblUser ON tblLocationUser.UserID=tblUser.UserID ");
        //    sb.Append(" WHERE 1=1 ");
        //    return sb.ToString();
        //}

        public static List<dcLocationUser> GetLocationList(int pCompanyID)
        {
            return GetLocationList(pCompanyID, null);
        }
        public static List<dcLocationUser> GetLocationList(int pCompanyID, DBContext dc)
        {
            List<dcLocationUser> cObjList = new List<dcLocationUser>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationUser_SQLString());
                //sb.Append(" AND tblLocationUser.CompanyID=@companyID ");
                sb.Append(" AND tblLocation.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcLocationUser>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcLocationUser GetLocaionUserByID(int pLocationUserID)
        {
            return GetLocaionUserByID(pLocationUserID, null);
        }
        public static dcLocationUser GetLocaionUserByID(int pLocationUserID, DBContext dc)
        {
            dcLocationUser cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationUser_SQLString());
                sb.Append(" AND tblLocationUser.LocationUserID=@pLocationUserID ");
                cmdInfo.DBParametersInfo.Add("@pLocationUserID", pLocationUserID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcLocationUser>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }



        public static List<dcLocationUser> GetLocationListByUserID(int pUserID, int pCompanyID)
        {
            return GetLocationListByUserID(pUserID, pCompanyID, null);
        }
        public static List<dcLocationUser> GetLocationListByUserID(int pUserID, int pCompanyID, DBContext dc)
        {
            List<dcLocationUser> cObjList = new List<dcLocationUser>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationUser_SQLString());
                //sb.Append(" AND tblLocationUser.CompanyID=@companyID ");
                //cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                sb.Append(" AND tblLocationUser.UserID=@userID ");
                cmdInfo.DBParametersInfo.Add("@userID", pUserID);

                if (pCompanyID > 0)
                {
                    sb.Append(" AND tblLocation.CompanyID=@companyID ");
                    cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcLocationUser>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcLocationUser GetLocaionUserByLocationID(int pLocationID, int pUserID)
        {
            return GetLocaionUserByLocationID(pLocationID, pUserID, null);
        }
        public static dcLocationUser GetLocaionUserByLocationID(int pLocationID, int pUserID, DBContext dc)
        {
            dcLocationUser cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationUser_SQLString());
                sb.Append(" AND tblLocationUser.LocationID=@locationID ");
                cmdInfo.DBParametersInfo.Add("@locationID", pLocationID);

                sb.Append(" AND tblLocationUser.UserID=@UserID ");
                cmdInfo.DBParametersInfo.Add("@UserID", pUserID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcLocationUser>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static bool IsLocationUserLoginAllowed(int pLocationID, int pUserID)
        {
            return IsLocationUserLoginAllowed(pLocationID, pUserID, null);
        }

        public static bool IsLocationUserLoginAllowed(int pLocationID, int pUserID, DBContext dc)
        {
            bool bStatus = false;
            dcUser user = UserBL.GetUserByUserID(pUserID, dc);
            if (user.IsUserAdmin)
            {
                bStatus = true;
            }
            else
            {
                dcLocationUser locUser = GetLocaionUserByLocationID(pLocationID, pUserID, dc);
                if (locUser != null)
                {
                    bStatus = locUser.AllowLogin;
                }
            }

            return bStatus;

        }


        public static int Insert(dcLocationUser cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcLocationUser cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcLocationUser>(cObj, true);
                if (id > 0) { cObj.LocationUserID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcLocationUser cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcLocationUser cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcLocationUser>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pLocationUserID)
        {
            return Delete(pLocationUserID, null);
        }

        public static bool Delete(int pLocationUserID, DBContext dc)
        {
            dcLocationUser cObj = new dcLocationUser();
            cObj.LocationUserID = pLocationUserID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcLocationUser>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool UpdateLocationUser(dcLocationUser cObj)
        {
            return UpdateLocationUser(cObj, null);
        }

        public static bool UpdateLocationUser(dcLocationUser cObj, DBContext dc)
        {
            if (cObj.LocationID == 0 || cObj.UserID == 0)
            {
                return false;
            }
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            dcLocationUser cObjN = GetLocaionUserByLocationID(cObj.LocationID, cObj.UserID, dc);

            if (cObjN == null)
            {
                cObjN = new dcLocationUser();
                cObjN.LocationID = cObj.LocationID;
                cObjN.UserID = cObj.UserID;
                cObjN.AllowLogin = cObj.AllowLogin;
                cnt = dc.DoInsert<dcLocationUser>(cObjN);
            }
            else
            {
                cObjN.AllowLogin = cObj.AllowLogin;
                cnt = dc.DoUpdate<dcLocationUser>(cObjN);
            }

            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }


        public static bool UpdateLocationUser(List<dcLocationUser> cList)
        {
            return UpdateLocationUser(cList, null);
        }
        public static bool UpdateLocationUser(List<dcLocationUser> cList, DBContext dc)
        {
            bool bSataus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            foreach (dcLocationUser cObj in cList)
            {
                bSataus = UpdateLocationUser(cObj, dc);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bSataus = true;
            return bSataus;
        }

        //public static dcLocationUser GetLocaionUserByLocationIDUserName(int pLocationID, string pUserID)
        //{
        //    return GetLocaionUserByLocationIDUserName(pLocationID, pUserID, null);
        //}
        //public static dcLocationUser GetLocaionUserByLocationIDUserName(int pLocationID, string pUserID, DBContext dc)
        //{
        //    dcLocationUser cObj = null;
        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        //        DBCommandInfo cmdInfo = new DBCommandInfo();
        //        StringBuilder sb = new StringBuilder(GetLocationUserName_SQLString());
        //        sb.Append(" AND tblLocationUser.LocationID=@locationID ");
        //        cmdInfo.DBParametersInfo.Add("@locationID", pLocationID);

        //        sb.Append(" AND tblUser.UserName=@UserID ");
        //        cmdInfo.DBParametersInfo.Add("@UserID", pUserID);

        //        sb.Append(" AND tblLocationUser.AllowLogin=@AllowLogin ");
        //        cmdInfo.DBParametersInfo.Add("@AllowLogin", true);

        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        //        cmdInfo.CommandText = sb.ToString();
        //        cmdInfo.CommandType = CommandType.Text;
        //        dbq.DBCommandInfo = cmdInfo;

        //        cObj = DBQuery.ExecuteDBQuery<dcLocationUser>(dbq, dc).FirstOrDefault();
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObj;
        //}

    }
}
