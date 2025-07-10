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


namespace PG.BLLibrary.InventoryBL
{
    public class InvSettingsBL
    {
        public static string GetInvSettingsList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblInvSettings.* ");
            sb.Append(" FROM tblInvSettings ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        //public static DataLoadOptions JournalAdjustTypeBLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcInvSettings> GetInvSettingsList()
        {
            return GetInvSettingsList(null);
        }
        public static List<dcInvSettings> GetInvSettingsList(DBContext dc)
        {
            List<dcInvSettings> cObjList = new List<dcInvSettings>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInvSettingsList_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcInvSettings>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcInvSettings GetInvSettingsByAppID()
        {
            return GetInvSettingsByAppID(1, null);
        }

        public static dcInvSettings GetInvSettingsByAppID(int pAppID)
        {
            return GetInvSettingsByAppID(pAppID, null);
        }
        public static dcInvSettings GetInvSettingsByAppID(int pAppID, DBContext dc)
        {
            dcInvSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInvSettingsList_SQLString());


                sb.Append(" AND tblInvSettings.AppID=@appID ");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);
              



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInvSettings>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcJournalAdjustType>()
                //                  where c.JournalAdjustTypeID == pJournalAdjustTypeID
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
    }
}
