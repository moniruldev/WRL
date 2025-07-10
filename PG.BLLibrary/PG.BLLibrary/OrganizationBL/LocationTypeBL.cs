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
    
    public class LocationTypeBL
    {
        public static string GetLocationType_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblLocationType.* ");
            sb.Append(" FROM tblLocationType ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcLocationType> GetLocationTypeList(int pCompanyID)
        {
            return GetLocationTypeList(pCompanyID, null);
        }
        public static List<dcLocationType> GetLocationTypeList(int pCompanyID, DBContext dc)
        {
            List<dcLocationType> cObjList = new List<dcLocationType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationType_SQLString());
                          
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcLocationType>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcLocationType GetLocaionTypeByID(int pLocationID)
        {
            return GetLocaionTypeByID(pLocationID, null);
        }
        public static dcLocationType GetLocaionTypeByID(int pLocationTypeID, DBContext dc)
        {
            dcLocationType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationType_SQLString());
                sb.Append(" AND tblLocationType.LocationTypeID=@LocationTypeID ");
                cmdInfo.DBParametersInfo.Add("@LocationTypeID", pLocationTypeID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcLocationType>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }
    }
}
