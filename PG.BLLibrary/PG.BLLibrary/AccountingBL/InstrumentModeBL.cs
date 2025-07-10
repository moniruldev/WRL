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
    public class InstrumentModeBL
    {

        public static string GetInstrumentModeList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblInstrumentMode.* ");
            sb.Append(" FROM tblInstrumentMode ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions InstrumentModeLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcInstrumentMode> GetInstrumentModeList()
        {
            return GetInstrumentModeList(null);
        }
        public static List<dcInstrumentMode> GetInstrumentModeList(DBContext dc)
        {
            List<dcInstrumentMode> cObjList = new List<dcInstrumentMode>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInstrumentModeList_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcInstrumentMode>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcInstrumentMode>() 
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcInstrumentMode GetInstrumentModeByID(int pInstrumentModeID)
        {
            return GetInstrumentModeByID(pInstrumentModeID, null);
        }
        public static dcInstrumentMode GetInstrumentModeByID(int pInstrumentModeID, DBContext dc)
        {
            dcInstrumentMode cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInstrumentModeList_SQLString());
                sb.Append(" AND tblInstrumentMode.InstrumentModeID=@instrumentModeID ");
                cmdInfo.DBParametersInfo.Add("@instrumentModeID", pInstrumentModeID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInstrumentMode>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcInstrumentMode>()
                //                  where c.InstrumentModeID == pInstrumentModeID
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
