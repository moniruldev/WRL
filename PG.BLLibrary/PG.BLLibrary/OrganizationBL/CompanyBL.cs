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
using PG.DBClass.InventoryDC;
using PG.DBClass.OrganiztionDC;


namespace PG.BLLibrary.OrganizationBL
{
    /// <summary>
    /// AppObjectsBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    
    public class CompanyBL
    {
        public static string GetCompany_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblCompany.* ");
            sb.Append(" FROM tblCompany ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcCompany> GetCompanyList()
        {
            return GetCompanyList(null);
        }
        public static List<dcCompany> GetCompanyList(DBContext dc)
        {
            List<dcCompany> cObjList = new List<dcCompany>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCompany_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCompany>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcCompany>()
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCompany GetCompanyByID(int pCompanyID)
        {
            return GetCompanyByID(pCompanyID, null);
        }
        public static dcCompany GetCompanyByID(int pCompanyID, DBContext dc)
        {
            dcCompany cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCompany_SQLString());
                sb.Append(" AND tblCompany.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcCompany>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcTBLCOMPANY GetCompanyInfoByID(int pCompanyID)
        {
            return GetCompanyInfoByID(pCompanyID, null);
        }
        public static dcTBLCOMPANY GetCompanyInfoByID(int pCompanyID, DBContext dc)
        {
            dcTBLCOMPANY cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCompany_SQLString());
                sb.Append(" AND tblCompany.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcTBLCOMPANY>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

    }
}
