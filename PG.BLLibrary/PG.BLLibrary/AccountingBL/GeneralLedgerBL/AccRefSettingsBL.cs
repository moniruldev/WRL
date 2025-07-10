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
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{

    /// <summary>
    /// AppAppInfoBL
    /// Last update By Moni, Date 10-03-2015
    /// </summary>
    public class AccRefSettingsBL
    {
        public static string GetAccRefSetting_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAccRefSettings.* ");
            sb.Append(" FROM tblAccRefSettings ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcAccRefSettings> GetAccRefSettingsList(int pCompanyID)
        {
            return GetAccRefSettingsList(pCompanyID, null);
        }
        public static List<dcAccRefSettings> GetAccRefSettingsList(int pCompanyID, DBContext dc)
        {
            List<dcAccRefSettings> cObjList = new List<dcAccRefSettings>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);



                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefSetting_SQLString());
                sb.Append(" AND tblAccRefSettings.CompanyID=@companyID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAccRefSettings>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcAccRefSettings> GetAccRefSettingsList(DBQuery dbq, DBContext dc)
        {
            List<dcAccRefSettings> cObjList = new List<dcAccRefSettings>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcAccRefSettings>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcAccRefSettings GetAccRefSettingID(int pAccRefSettingsID)
        {
            return GetAccRefSettingID(pAccRefSettingsID, null);
        }
        public static dcAccRefSettings GetAccRefSettingID(int pAccRefSettingsID, DBContext dc)
        {
            dcAccRefSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefSetting_SQLString());
                sb.Append(" AND tblAccRefSettings.AccRefSettingsID=@accRefSettingsID");
                cmdInfo.DBParametersInfo.Add("@accRefSettingsID", pAccRefSettingsID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAccRefSettings>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }


        public static dcAccRefSettings GetAccRefSettingByType(int pCompanyID, int pAccRefTypeID)
        {
            return GetAccRefSettingByType(pCompanyID, pAccRefTypeID, null);
        }
        public static dcAccRefSettings GetAccRefSettingByType(int pCompanyID, int pAccRefTypeID, DBContext dc)
        {
            dcAccRefSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefSetting_SQLString());
                sb.Append(" AND tblAccRefSettings.CompanyID=@CompanyID");
                cmdInfo.DBParametersInfo.Add("@pCompanyID", pCompanyID);


                sb.Append(" AND tblAccRefSettings.AccRefTypeID=@accRefTypeID");
                cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAccRefSettings>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }



        public static int Insert(dcAccRefSettings cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAccRefSettings cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAccRefSettings>(cObj, true);
                if (id > 0) { cObj.AccRefSettingsID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAccRefSettings cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAccRefSettings cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAccRefSettings>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAccRefSettingsID)
        {
            return Delete(pAccRefSettingsID, null);
        }
        public static bool Delete(int pAccRefSettingsID, DBContext dc)
        {
            dcAccRefSettings cObj = new dcAccRefSettings();
            cObj.AccRefSettingsID = pAccRefSettingsID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAccRefSettings>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
    }
}
