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
    public class UserBL
    {
        public static DataLoadOptions UserLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
            return dlo;
        }



        public static List<dcUser> GetUserList()
        {
            return GetUserList(0, 0, 0, string.Empty, null);
        }
        public static List<dcUser> GetUserList(int pAppID)
        {
            return GetUserList(pAppID,0, 0, string.Empty, null);
        }

        public static List<dcUser> GetUserList(int pAppID, int pRoleID)
        {
            return GetUserList(pAppID, pRoleID,0,string.Empty, null);
        }

        public static List<dcUser> GetUserList(int pAppID, int pRoleID, DBContext dc)
        {
            return GetUserList(pAppID, pRoleID, 0, string.Empty, dc);
        }

        public static List<dcUser> GetUserList(int pAppID, int pRoleID, int pUserID, string pUserName)
        {
            return GetUserList(pAppID, pRoleID, pUserID, pUserName, null);
        }

        public static List<dcUser> GetUserList(int pAppID, int pRoleID, int pUserID, string pUserName, DBContext dc)
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();

            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblUser.* ");
            sb.Append(" , tblRole.RoleName, tblRole.IsAdmin ");
            //below line is newly added by mamun
            //sb.Append(" ,EMP_INFO.DEPARTMENT_ID ");
            sb.Append(" FROM tblUser ");
            sb.Append(" INNER JOIN tblRole ON tblRole.RoleID = tblUser.RoleID ");
            //below line is newly added by mamun(here left join should be omited)
            //sb.Append(" INNER JOIN EMP_INFO ON tblUser.EMPID=EMP_INFO.EMP_KEY ");
            sb.Append(" WHERE 1=1 ");

            if (pAppID > 0)
            {
                sb.Append(" AND tblUser.AppID=@pAppID ");
                //cmd.Parameters.AddWithValue("@pAppID", pAppID);

                cmdInfo.DBParametersInfo.Add("@pAppID", pAppID);
            }

            if (pRoleID > 0)
            {
                sb.Append(" AND tblUser.RoleID=@pRoleID ");
                //cmd.Parameters.AddWithValue("@pRoleID", pRoleID);

                cmdInfo.DBParametersInfo.Add("@pRoleID", pRoleID);
            }

            if (pUserID > 0)
            {
                sb.Append(" AND tblUser.UserID=@pUserID ");
                //cmd.Parameters.AddWithValue("@pUserID", pUserID);
                cmdInfo.DBParametersInfo.Add("@pUserID", pUserID);
            }


            if (pUserName != string.Empty)
            {
                sb.Append(" AND Upper(tblUser.UserName)= Upper(@pUserName) ");
                //cmd.Parameters.AddWithValue("@pUserName", pUserName);
                cmdInfo.DBParametersInfo.Add("@pUserName", pUserName);
            }

            sb.Append(" ORDER BY tblUser.AppID, tblUser.UserName");

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;


            return GetUserList(dbq, dc);

            //DBCommandInfo cmdInfo = new DBCommandInfo();

            ////System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //StringBuilder sb = new StringBuilder();

            //sb.Append("SELECT tblUser.* ");
            ////sb.Append(" , tblRole.RoleName ");
            //sb.Append(" FROM tblUser ");
            ////sb.Append(" INNER JOIN tblRole ON tblRole.RoleID = tblUser.RoleID ");
            //sb.Append(" WHERE 1=1 ");

            //if (pAppID > 0)
            //{
            //    sb.Append(" AND tblUser.AppID=@pAppID ");
            //    //cmd.Parameters.AddWithValue("@pAppID", pAppID);

            //    cmdInfo.DBParametersInfo.Add("@pAppID", pAppID);
            //}

            //if (pRoleID > 0)
            //{
            //    sb.Append(" AND tblUser.RoleID=@pRoleID ");
            //    //cmd.Parameters.AddWithValue("@pRoleID", pRoleID);

            //    cmdInfo.DBParametersInfo.Add("@pRoleID", pRoleID);
            //}

            //if (pUserID > 0)
            //{
            //    sb.Append(" AND tblUser.UserID=@pUserID ");
            //    //cmd.Parameters.AddWithValue("@pUserID", pUserID);
            //    cmdInfo.DBParametersInfo.Add("@pUserID", pUserID);
            //}


            //if (pUserName != string.Empty)
            //{
            //    sb.Append(" AND Upper(tblUser.UserName)= Upper(@pUserName) ");
            //    //sb.Append(" AND tblUser.UserName=@pUserName ");
            //    //cmd.Parameters.AddWithValue("@pUserName", pUserName);
            //    cmdInfo.DBParametersInfo.Add("@pUserName", pUserName);
            //}

            //sb.Append(" ORDER BY tblUser.AppID, tblUser.UserName");

            ////cmd.CommandType = CommandType.Text;
            ////cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            ////dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            ////dbq.DBCommand = cmd;

            //dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            //cmdInfo.CommandText = sb.ToString();
            //cmdInfo.CommandType = CommandType.Text;
            //dbq.DBCommandInfo = cmdInfo;


            //return GetUserList(dbq, dc);


        }

        public static List<dcUser> GetUserList(DBQuery dbq, DBContext dc)
        {
            List<dcUser> cObjList = new List<dcUser>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                }
                cObjList = DBQuery.ExecuteDBQuery<dcUser>(dbq, dc);
                
                //using (DataContext dataContext = dc.NewDataContext())
                //{

                //}
            }
            //catch { throw; }
            //finally 
            { 
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return cObjList;
        }
     
        public static dcUser GetUserByUserID(int pUserID)
        {
            return GetUserByUserID(pUserID, null);
        }
        public static dcUser GetUserByUserID(int pUserID, DBContext dc)
        {
            return GetUserList(0, 0, pUserID, string.Empty, dc).FirstOrDefault();
        }


        public static dcUser GetUserByUserName(int pAppID, string pUserName)
        {

            return GetUserByUserName(pAppID, pUserName, null);
        }
        public static dcUser GetUserByUserName(int pAppID, string pUserName, DBContext dc)
        {

            return GetUserList(pAppID, 0, 0, pUserName, dc).FirstOrDefault();

        
        }

        public static bool IsUserHasRefrenece(int pUserID)
        {
            return IsUserHasRefrenece(pUserID, null);
        }
        public static bool IsUserHasRefrenece(int pUserID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(UserID) tID From tblUser Where UserID=@userID";
                cmdInfo.DBParametersInfo.Add("@userID", pUserID);
             

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
                //    var result = from cObj in dataContext.GetTable<dcUser>()
                //                 where cObj.UserID == pUserID
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


        //TODO Change by Monir 
        public static bool IsUserExists(int AppID, string pUserName)
        {
            return IsUserExists(AppID, pUserName, null);
        }

        public static bool IsUserExists(int AppID, string pUserName, DBContext dc)
        {
            StringBuilder sb = new StringBuilder();
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //TODO Change 9-04-2015
                //string sql = "select Count(UserID) tID From tblUser Where UserID=@userID AND UserName=@UserName";
                sb.Append(" select Count(UserID) tID From tblUser Where UserName=@UserName ");
                cmdInfo.DBParametersInfo.Add("@UserName", pUserName);
                sb.Append(" AND AppID=@appID ");
                cmdInfo.DBParametersInfo.Add("@appID", AppID);
                


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcUser>()
                //                 where cObj.AppID == AppID && cObj.UserName == pUserName
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
        public static bool IsUserExists(int AppID, string pUserName, int pUserID)
        {
            return IsUserExists(AppID, pUserName, pUserID, null);
        }
        public static bool IsUserExists(int AppID, string pUserName, int pUserID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = from cObj in dataContext.GetTable<dcUser>()
                                 where cObj.UserID != pUserID && cObj.AppID == AppID && cObj.UserName == pUserName
                                 select cObj;
                    isData = result.Count() > 0;
                }
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static int Insert(dcUser cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcUser cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int id = dc.DoInsert<dcUser>(cObj, true);
            if (id > 0) { cObj.UserID = id; }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcUser cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcUser cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoUpdate<dcUser>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int AppID, string pUserID)
        {
            return Delete(AppID, pUserID, null);
        }
        public static bool Delete(int AppID, string pUserName, DBContext dc)
        {
            dcUser cObj = new dcUser();
            cObj.AppID = AppID;
            cObj.UserName = pUserName;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoDelete<dcUser>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Login(int AppID, string pUserName, string pPassword, out int oUserID)
        {
            return Login(AppID, pUserName, pPassword, out oUserID, false, null);
        }
        public static bool Login(int AppID, string pUserName, string pPassword, out int oUserID, DBContext dc)
        {
            return Login(AppID, pUserName, pPassword, out oUserID, false, dc);
        }
        public static bool Login(int AppID, string pUserName, string pPassword, out int oUserID, bool pIsPassCaseInsensitive)
        {
            return Login(AppID, pUserName, pPassword, out oUserID, pIsPassCaseInsensitive, null);
        }
        public static bool Login(int AppID, string pUserName, string pPassword, out int oUserID, bool pIsPassCaseInsensitive, DBContext dc)
        {
            bool isAuthinticate = false;
            int userID = 0;
            dcUser cUser = GetUserByUserName(AppID, pUserName, dc);
            if (cUser != null)
            {
                if (pIsPassCaseInsensitive)
                {
                    if (cUser.Password.ToUpper() == pPassword.ToUpper())
                    {
                        userID = cUser.UserID;
                        isAuthinticate = true;
                    }
                }
                else
                    if (string.CompareOrdinal(cUser.Password, pPassword) == 0)
                    {
                        userID = cUser.UserID;
                        isAuthinticate = true;
                    }
            }
            oUserID = userID;
            return isAuthinticate;

        }

        public static bool CheckPasswordByID(int pUserID, string pPassword)
        {
            return CheckPasswordID(pUserID, pPassword, null);
        }
        public static bool CheckPasswordID(int pUserID, string pPassword, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            dcUser cObj = GetUserByUserID(pUserID, dc);
            if (cObj.Password == pPassword)
            {
                bStatus = true;
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);


            return bStatus;
        }

        public static bool CheckPasswordByName(int pAppID, string pUserName, string pPassword , bool pIsPassCaseInsensitive)
        {
            return CheckPasswordByName(pAppID, pUserName, pPassword, pIsPassCaseInsensitive, null);
        }
        public static bool CheckPasswordByName(int pAppID, string pUserName, string pPassword, bool pIsPassCaseInsensitive, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            dcUser cUser = GetUserByUserName(pAppID, pUserName, dc);
            if (cUser != null)
            {
                if (pIsPassCaseInsensitive)
                {
                    if (cUser.Password.ToUpper() == pPassword.ToUpper())
                    {
                        bStatus = true;
                    }
                }
                else
                    if (string.CompareOrdinal(cUser.Password, pPassword) == 0)
                    {
                        bStatus = true;
                    }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);


            return bStatus;
        }

        public static bool ChangePasswordByID(int pUserID, string pPassword)
        {
            return ChangePasswordByID(pUserID, pPassword, null);
        }
        public static bool ChangePasswordByID(int pUserID, string pPassword, DBContext dc)
        {
            bool isDCInit = false;

            dcUser cObj = new dcUser();
            cObj.UserID = pUserID;
            cObj.Password = pPassword;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoUpdate<dcUser>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool ChangePasswordByName(int pAppID, string pUserName, string pPassword)
        {
            return ChangePasswordByName(pAppID, pUserName, pPassword, null);
        }
        public static bool ChangePasswordByName(int pAppID, string pUserName, string pPassword, DBContext dc)
        {
            bool isDCInit = false;

            dcUser cObj = GetUserByUserName(pAppID, pUserName, dc);
            cObj.UserID = cObj.UserID;
            cObj.Password = pPassword;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoUpdate<dcUser>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
    }
}
