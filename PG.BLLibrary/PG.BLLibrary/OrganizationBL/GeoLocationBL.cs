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


namespace PG.BLLibrary.OrganizationBL
{
    /// <summary>
    /// AppObjectsBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    
    public class GeoLocationBL
    {
        public static string GetGeoLocation_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblGeoLocation.* ");
            sb.Append(" FROM tblGeoLocation ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcGeoLocation> GetLocationList(int pCompanyID, int pGeoLoactionTypeID)
        {
            return GetLocationList(pCompanyID, pGeoLoactionTypeID, null);
        }
        public static List<dcGeoLocation> GetLocationList(int pCompanyID, int pGeoLoactionTypeID, DBContext dc)
        {
            List<dcGeoLocation> cObjList = new List<dcGeoLocation>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGeoLocation_SQLString());
                sb.Append(" AND tblGeoLocation.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (pGeoLoactionTypeID > 0)
                {
                    sb.Append(" AND tblGeoLocation.GeoLocationTypeID=@pGeoLoactionTypeID ");
                    cmdInfo.DBParametersInfo.Add("@pGeoLoactionTypeID", pGeoLoactionTypeID);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcGeoLocation>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcGeoLocation GetGeoLocaionByID(int pGeoLocationID)
        {
            return GetGeoLocaionByID(pGeoLocationID, null);
        }
        public static dcGeoLocation GetGeoLocaionByID(int pGeoLocationID, DBContext dc)
        {
            dcGeoLocation cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGeoLocation_SQLString());
                sb.Append(" AND tblGeoLocation.GeoLocationID=@GeoLocationID ");
                cmdInfo.DBParametersInfo.Add("@GeoLocationID", pGeoLocationID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcGeoLocation>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static int Insert(dcGeoLocation cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcGeoLocation cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcGeoLocation>(cObj, true);
                if (id > 0) { cObj.GeoLocationID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcGeoLocation cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcGeoLocation cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcGeoLocation>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pGeoLocationID)
        {
            return Delete(pGeoLocationID, null);
        }

        public static bool Delete(int pGeoLocationID, DBContext dc)
        {
            dcGeoLocation cObj = new dcGeoLocation();
            cObj.GeoLocationID = pGeoLocationID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcGeoLocation>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

    }
}
