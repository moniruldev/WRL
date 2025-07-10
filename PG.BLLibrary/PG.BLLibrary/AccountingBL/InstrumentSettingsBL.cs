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
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL
{
    public class InstrumentSettingsBL
    {

        public static string InstrumentSettings_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblInstrumentSettings.* ");
            sb.Append(" FROM tblInstrumentSettings ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcInstrumentSettings> GetInstrumentSettingsList()
        {
            return GetInstrumentSettingsList(null);
        }
        public static List<dcInstrumentSettings> GetInstrumentSettingsList(DBContext dc)
        {
            List<dcInstrumentSettings> cObjList = new List<dcInstrumentSettings>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(InstrumentSettings_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcInstrumentSettings>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcInstrumentSettings GetInstrumentTypeByID(int pInstrumentSettingsID)
        {
            return GetInstrumentTypeByID(pInstrumentSettingsID, null);
        }
        public static dcInstrumentSettings GetInstrumentTypeByID(int pInstrumentSettingsID, DBContext dc)
        {
            dcInstrumentSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(InstrumentSettings_SQLString());
                sb.Append(" AND tblInstrumentSettings.InstrumentSettingsID=@instrumentSettingsID ");
                cmdInfo.DBParametersInfo.Add("@instrumentSettingsID", pInstrumentSettingsID);
                //sb.Append(" Order By tblInstrumentType.InstrumentTypeSLNo");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInstrumentSettings>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }


        public static dcInstrumentSettings GetInstrumentTypeByCompanyID(int pCompanyID)
        {
            return GetInstrumentTypeByCompanyID(pCompanyID, null);
        }
        public static dcInstrumentSettings GetInstrumentTypeByCompanyID(int pCompanyID, DBContext dc)
        {
            dcInstrumentSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(InstrumentSettings_SQLString());
                sb.Append(" AND tblInstrumentSettings.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                //sb.Append(" Order By tblInstrumentType.InstrumentTypeSLNo");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInstrumentSettings>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }
    }
}
