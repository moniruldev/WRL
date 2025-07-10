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
    
    public class GeoLocationTypeBL
    {
        public static string GetGeoLocationType_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblGeoLocationType.* ");
            sb.Append(" FROM tblGeoLocationType ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcGeoLocationType> GetGeoLocationTypeList(int pCompanyID)
        {
            return GetGeoLocationTypeList(pCompanyID, null);
        }
        public static List<dcGeoLocationType> GetGeoLocationTypeList(int pCompanyID, DBContext dc)
        {
            List<dcGeoLocationType> cObjList = new List<dcGeoLocationType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGeoLocationType_SQLString());
                sb.Append(" AND tblGeoLocationType.CompanyID=@pCompanyID ");
                cmdInfo.DBParametersInfo.Add("@pCompanyID", pCompanyID);
          


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcGeoLocationType>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcGeoLocationType GetGeoLocaionTypeByID(int pGeoLocationTypeID)
        {
            return GetGeoLocaionTypeByID(pGeoLocationTypeID, null);
        }
        public static dcGeoLocationType GetGeoLocaionTypeByID(int pGeoLocationTypeID, DBContext dc)
        {
            dcGeoLocationType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGeoLocationType_SQLString());
                sb.Append(" AND tblGeoLocationType.GeoLocationTypeID=@GeoLocationTypeID ");
                cmdInfo.DBParametersInfo.Add("@GeoLocationTypeID", pGeoLocationTypeID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcGeoLocationType>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }
    }
}
