using System;
using System.Data;
using System.Configuration;
using System.Linq;
//using System.Linq.Dynamic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using PG.Core.Web;
using PG.DBClass.SecurityDC;
using PG.BLLibrary.SecurityBL;
using PG.BLLibrary.OrganizationBL;
using PG.DBClass.OrganiztionDC;
using PG.Core.DBBase;
using PG.Common;

namespace PG.Web
{

    public class AppSecurity
    {
        public static string SessionKey_UserInfo = "UserInfo";
        public static string SessionKey_UserLocation = "UserLocation";
       


        public static List<dcRolePermission> GetRolePermissionList()
        {
            return GetRolePermissionList(AppGlobals.LoginInfoAppID);
        }
        public static List<dcRolePermission> GetRolePermissionList(int pAppID)
        {
            List<dcRolePermission> cList = null;
            object data;
            if (AppCache.CheckAndGetData(AppCache.CacheKey_RolesPermission, out data))
            {
                cList = (List<dcRolePermission>)data;
            }
            else
            {
                cList = RolePermissionBL.GetRolePermissionList(pAppID);
                AppCache.Insert(AppCache.CacheKey_RolesPermission, cList);
            }
            return cList;   
        }

        public static List<dcUser> GetUserList()
        {
            return GetUserList(AppGlobals.LoginInfoAppID);
        }
        public static List<dcUser> GetUserList(int pAppID)
        {
            List<dcUser> cList = null;
            object data;
            if (AppCache.CheckAndGetData(AppCache.CacheKey_Users, out data))
            {
                cList = (List<dcUser>)data;
            }
            else
            {
                cList =  UserBL.GetUserList(pAppID);
                AppCache.Insert(AppCache.CacheKey_Users, cList);
            }
            return cList;
        }

        public static List<dcRole> GetRoleList()
        {
            return GetRoleList(AppGlobals.LoginInfoAppID);
        }
        public static List<dcRole> GetRoleList(int pAppID)
        {
            List<dcRole> cList = null;
            object data;
            if (AppCache.CheckAndGetData(AppCache.CacheKey_Roles, out data))
            {
                cList = (List<dcRole>)data;
            }
            else
            {
                cList = RoleBL.GetRoleList(pAppID);
                AppCache.Insert(AppCache.CacheKey_Roles, cList);
            }
            return cList;
        }

        public static dcUser GetUser(int pUserID)
        {
            dcUser user = null;
            var result = from c in GetUserList()
                         where c.UserID == pUserID
                         select c;
            if (result.Count() > 0)
            {
                user = result.First();
            }
            return user;
        }

        public static dcUser GetUser(int pAppID, string pUserName)
        {
            dcUser user = null;
            var result = from c in GetUserList()
                         where c.AppID == pAppID && c.UserName.ToLower() == pUserName.ToLower()
                         select c;
            if (result.Count() > 0)
            {
                user = result.First();
            }
            return user;
        }


        public static dcRole GetRole(int pRoleID)
        {
            dcRole role = null;
            var result = from c in GetRoleList()
                         where c.RoleID == pRoleID
                         select c;
            if (result.Count() > 0)
            {
                role = result.First();
            }
            return role;
        }

        public static dcRole GetRole(int pAppID, string pRoleName)
        {
            dcRole role = null;
            var result = from c in GetRoleList()
                         where c.AppID == pAppID && c.RoleName.ToLower() == pRoleName.ToLower()
                         select c;
            if (result.Count() > 0)
            {
                role = result.First();
            }
            return role;
        }



        public static PermissionEnum GetObjectPermissionByRoleID(int pRoleID, AppObjectEnum pAppObjectID)
        {
            return GetObjectPermissionByRoleID(AppInfo.AppID, pRoleID, pAppObjectID);
        }

        public static PermissionEnum GetObjectPermissionByRoleID(int pRoleID, int pAppObjectID)
        {
            return GetObjectPermissionByRoleID(AppInfo.AppID, pRoleID, pAppObjectID);
        }

        public static PermissionEnum GetObjectPermissionByRoleID(int pAppID, int pRoleID, AppObjectEnum pAppObjectID)
        {
            return GetObjectPermissionByRoleID(pAppID, pRoleID, (int)pAppObjectID);
        }

        public static PermissionEnum GetObjectPermissionByRoleID(int pAppID, int pRoleID, int pAppObjectID)
        {
            PermissionEnum permission = PermissionEnum.None;
            if (pRoleID == 0)
            {
                return permission;
            }
            var result = from c in GetRolePermissionList()
                         where c.RoleID == pRoleID && c.AppObjectID == pAppObjectID
                         select c;
            if (result.Count() > 0)
            {
                permission = (PermissionEnum)result.First().Permission;
            }
            return permission;
        }


        public static int GetUserRoleID(int pUserID)
        {
            return GetUserRoleID(AppInfo.AppID, pUserID);
        }
        public static int GetUserRoleID(int pAppID, int pUserID)
        {

            int roleID = 0;
            var result = from c in GetUserList(pAppID)
                         where c.UserID == pUserID
                         select c;
            if (result.Count() > 0)
            {
                roleID = result.First().RoleID;
            }
            return roleID;
        }

        public static bool CheckObjectPermissionByRoleID(int pRoleID, AppObjectEnum pAppObjectID, PermissionEnum pSeek)
        {
           return CheckObjectPermissionByRoleID(pRoleID, (int)pAppObjectID, (int)pSeek);
        }

        public static bool CheckObjectPermissionByRoleID(int pRoleID, int pAppObjectID, int pSeek)
        {
            //((per & PermissionEnum.Add) == PermissionEnum.Add)

            PermissionEnum permission = GetObjectPermissionByRoleID(pRoleID, pAppObjectID);
            PermissionEnum seekPerm =  (PermissionEnum)pSeek;

            if ((permission & seekPerm) == seekPerm)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool CheckObjectPermissionByUserID(int pUserID, AppObjectEnum pAppObjectID, PermissionEnum pSeek)
        {
            return CheckObjectPermissionByUserID(AppInfo.AppID, pUserID, pAppObjectID, pSeek);
        }

        public static bool CheckObjectPermissionByUserID(int pAppID, int pUserID, AppObjectEnum pAppObjectID, PermissionEnum pSeek)
        {
            return CheckObjectPermissionByUserID(pAppID, pUserID, (int)pAppObjectID, (int)pSeek);
        }

        public static bool CheckObjectPermissionByUserID(int pUserID, int pAppObjectID, int pSeek)
        {
            return CheckObjectPermissionByUserID(AppInfo.AppID, pUserID, (int)pAppObjectID, (int)pSeek);
        }

        public static bool CheckObjectPermissionByUserID(int pAppID, int pUserID, int pAppObjectID, int pSeek)
        {
            if (IsUserAdmin(pUserID))
            {
                return true;
            }

            int userRoleID = GetUserRoleID(pAppID, pUserID);

            return CheckObjectPermissionByRoleID(userRoleID, pAppObjectID, pSeek);
        }

        public static bool IsUserAdmin(int pUserID)
        {
            bool isAdmin = false;
            dcUser user = GetUser(pUserID);
            if (user != null)
            {
                dcRole role = GetRole(user.RoleID);
                if (role != null)
                {
                    if (role.RoleName.ToUpper().Trim() == "ADMINS" | role.RoleID == 1)
                    {
                        isAdmin = true;
                    }
                }
            }
            return isAdmin;
        }



        public static void SetUserInfoToSession(int pUserID, int pComapnyID, int pLoginLocationID)
        {
            SetUserInfoToSession(pUserID, pComapnyID, pLoginLocationID, HttpContext.Current);
        }

        public static void SetUserInfoToSession(int pUserID, int pComapnyID, int pLoginLocationID, HttpContext context)
        {
            dcUser user = UserBL.GetUserByUserID(pUserID);
            user.CompanyId = pComapnyID;
            if (user.IsUserAdmin)
            {

                List<dcLocationUser> list = (from c in LocationBL.GetLocationList(pComapnyID)
                                                select new dcLocationUser
                                                {
                                                    LocationID =  c.LocationID,
                                                    LocationCode = c.LocationCode,
                                                    LocationName = c.LocationName,
                                                    UserID = pUserID,
                                                    AllowLogin = true
                                                }).ToList();
                user.LocationUserList = list;
            }
            else
            {
                user.LocationUserList = LocationUserBL.GetLocationListByUserID(pUserID, pComapnyID);
            }

            dcLocationUser locUser = user.LocationUserList.FirstOrDefault(c => c.LocationID == pLoginLocationID);
            if (locUser != null)
            {
                user.LoginLocationID = locUser.LocationID;
                user.LoginLocationName = locUser.LocationName;
                user.LoginLocationCode = locUser.LocationCode;
            }


            SetUserInfoToSession(user);
        }

        public static void SetUserInfoToSession(dcUser pUser)
        {
            SetUserInfoToSession(pUser, HttpContext.Current);
        }

        public static void SetUserInfoToSession(dcUser pUser, HttpContext context)
        {
            context.Session[SessionKey_UserInfo] = pUser;
        }

        public static dcUser GetUserInfoFromSession()
        {
            return GetUserInfoFromSession(HttpContext.Current);
        }

        public static dcUser GetUserInfoFromSession(HttpContext context)
        {
            dcUser user = null;
            if (context.Session[SessionKey_UserInfo] != null)
            {
                user = context.Session[SessionKey_UserInfo] as dcUser;
            }
            return user;
        }

        public static List<dcLocationUser> GetValidLocationUserList()
        {
            return GetValidLocationUserList(HttpContext.Current);
        }
        public static List<dcLocationUser> GetValidLocationUserList(HttpContext context)
        {
            return AppSecurity.GetUserInfoFromSession().LocationUserList.Where(c => c.AllowLogin).ToList();
        }

        //here 

    }
}
