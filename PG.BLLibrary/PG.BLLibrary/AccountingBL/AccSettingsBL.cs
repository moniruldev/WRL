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

namespace PG.BLLibrary.AccountingBL
{

    /// <summary>
    /// AppAppInfoBL
    /// Last update By Moni, Date 10-03-2015
    /// </summary>
    public class AccSettingsBL
    {
        public static string GetAccsetting_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAccSettings.* ");
            sb.Append(" FROM tblAccSettings ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions AccSettingsLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType); 
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
            return dlo;
        }
        public static List<dcAccSettings> GetAccSettingsList()
        {
            return GetAccSettingsList(null);
        }
        public static List<dcAccSettings> GetAccSettingsList(DBContext dc)
        {
            List<dcAccSettings> cObjList = new List<dcAccSettings>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccsetting_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAccSettings>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcAccSettings>()
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAccSettings GetAccSettingID(int pAccSettingsID)
        {
            return GetAccSettingID(pAccSettingsID, null);
        }
        public static dcAccSettings GetAccSettingID(int pAccSettingsID, DBContext dc)
        {
            dcAccSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccsetting_SQLString());
                sb.Append(" AND tblAccSettings.AccSettingsID=@accSettingsID");
                cmdInfo.DBParametersInfo.Add("@accSettingsID", pAccSettingsID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAccSettings>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcAccSettings>()
                //                  where c.AccSettingsID == pAccSettingsID
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

        public static dcAccSettings GetAccSettingByCompanyID(int pCompanyID)
        {
            return GetAccSettingByCompanyID(pCompanyID, null);
        }
        public static dcAccSettings GetAccSettingByCompanyID(int pCompanyID, DBContext dc)
        {
            dcAccSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccsetting_SQLString());
                sb.Append(" AND tblAccSettings.CompanyID=@companyID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAccSettings>(dbq, dc).FirstOrDefault();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcAccSettings>()
                //                  where c.CompanyID == pCompanyID
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


        public static int Insert(dcAccSettings cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAccSettings cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAccSettings>(cObj, true);
                if (id > 0) { cObj.AccSettingsID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAccSettings cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAccSettings cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAccSettings>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAccSettingsID)
        {
            return Delete(pAccSettingsID, null);
        }
        public static bool Delete(int pAccSettingsID, DBContext dc)
        {
            dcAccSettings cObj = new dcAccSettings();
            cObj.AccSettingsID = pAccSettingsID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAccSettings>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
    }
}
