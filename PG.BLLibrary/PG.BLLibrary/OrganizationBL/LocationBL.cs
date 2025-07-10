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
    
    public class LocationBL
    {
        public static string GetLocation_SQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblLocation.* ");
            sb.Append(", tblLocationType.LocationTypeCode, tblLocationType.LocationTypeName "); 
            sb.Append(" FROM tblLocation ");
            sb.Append(" INNER JOIN tblLocationType ON tblLocation.LocationTypeID = tblLocationType.LocationTypeID "); 
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcLocation> GetLocationList(int pCompanyID)
        {
            return GetLocationList(pCompanyID, null);
        }
        public static List<dcLocation> GetLocationList(int pCompanyID, DBContext dc)
        {
            List<dcLocation> cObjList = new List<dcLocation>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocation_SQLString());
                sb.Append(" AND tblLocation.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcLocation>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcLocation> GetLocationList(DBQuery dbq, DBContext dc)
        {
            List<dcLocation> cObjList = new List<dcLocation>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcLocation>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        
        
        public static dcLocation GetLocaionByID(int pLocationID)
        {
            return GetLocaionByID(pLocationID, null);
        }
        public static dcLocation GetLocaionByID(int pLocationID, DBContext dc)
        {
            dcLocation cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocation_SQLString());
                sb.Append(" AND tblLocation.LocationID=@LocationID ");
                cmdInfo.DBParametersInfo.Add("@LocationID", pLocationID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcLocation>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcLocation GetLocaionByCode(string pLocationCode, int pCompanyID)
        {
            return GetLocaionByCode(pLocationCode, pCompanyID, null);
        }
        public static dcLocation GetLocaionByCode(string pLocationCode, int pCompanyID, DBContext dc)
        {
            dcLocation cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocation_SQLString());

                sb.Append(" AND tblLocation.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                sb.Append(" AND tblLocation.LocationCode=@locationCode ");
                cmdInfo.DBParametersInfo.Add("@locationCode", pLocationCode);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcLocation>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }


        public static bool IsLocationCodeExists(int pCompanyID, string pLocationCode)
        {
            return IsLocationCodeExists(pCompanyID, pLocationCode, null);
        }
        public static bool IsLocationCodeExists(int pCompanyID, string pLocationCode, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocation_SQLString());
                sb.Append(" AND tblLocation.CompanyID=@companyID AND  tblLocation.LocationCodeCode=@locationCode");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@locationCode", pLocationCode);

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
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsLocationCodeExists(int pCompanyID, string pLocationCode, int pLocationID)
        {
            return IsLocationCodeExists(pCompanyID, pLocationCode, pLocationID, null);
        }
        public static bool IsLocationCodeExists(int pCompanyID, string pLocationCode, int pLocationID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocation_SQLString());
                sb.Append(" AND tblLocation.CompanyID=@companyID AND  tblLocation.LocationCode = @locationCode AND tblLocation.LocationID <> @locationID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@locationCode", pLocationCode);
                cmdInfo.DBParametersInfo.Add("@locationID", pLocationID);

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
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static int Insert(dcLocation cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcLocation cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcLocation>(cObj, true);
                if (id > 0) { cObj.LocationID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcLocation cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcLocation cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcLocation>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pLocationID)
        {
            return Delete(pLocationID, null);
        }

        public static bool Delete(int pLocationID, DBContext dc)
        {
            dcLocation cObj = new dcLocation();
            cObj.LocationID = pLocationID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcLocation>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

    }
}
