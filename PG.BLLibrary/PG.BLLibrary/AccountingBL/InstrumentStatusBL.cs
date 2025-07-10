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
    public class InstrumentStatusBL
    {

        public static string GetInstrumentStatusList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblInstrumentStatus.* ");
            sb.Append(" FROM tblInstrumentStatus ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions InstrumentStatusLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcInstrumentStatus> GetInstrumentStatusList()
        {
            return GetInstrumentStatusList(null);
        }
        public static List<dcInstrumentStatus> GetInstrumentStatusList(DBContext dc)
        {
            List<dcInstrumentStatus> cObjList = new List<dcInstrumentStatus>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInstrumentStatusList_SQLString());
                sb.Append(" Order By tblInstrumentStatus.InstrumentStatusSLNo");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcInstrumentStatus>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcInstrumentStatus>()
                //                orderby c.InstrumentStatusSLNo 
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcInstrumentStatus GetInstrumentStatusByID(int pInstrumentStatusID)
        {
            return GetInstrumentStatusByID(pInstrumentStatusID, null);
        }
        public static dcInstrumentStatus GetInstrumentStatusByID(int pInstrumentStatusID, DBContext dc)
        {
            dcInstrumentStatus cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInstrumentStatusList_SQLString());
                sb.Append(" AND tblInstrumentStatus.InstrumentStatusID=@instrumentTypeID ");
                cmdInfo.DBParametersInfo.Add("@instrumentStatusID", pInstrumentStatusID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInstrumentStatus>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcInstrumentStatus>()
                //                  where c.InstrumentStatusID == pInstrumentStatusID
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
