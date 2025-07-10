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

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class JournalTranTypeBL
    {
        public static string GetJournalTranTypeList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalTranType.* ");
            sb.Append(" FROM tblJournalTranType ");
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
        public static List<dcJournalTranType> GetJournalTranTypeList()
        {
            return GetJournalTranTypeList(null);
        }
        public static List<dcJournalTranType> GetJournalTranTypeList(DBContext dc)
        {
            List<dcJournalTranType> cObjList = new List<dcJournalTranType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalTranTypeList_SQLString());
                sb.Append(" AND tblJournalTranType.IsVisible=@isVisible");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
                sb.Append(" ORDER BY JournalTranTypeSLNo");
                
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcJournalTranType>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcJournalTranType GetJournalTranTypeByID(int pJournalTranTypeID)
        {
            return GetJournalTranTypeByID(pJournalTranTypeID, null);
        }
        public static dcJournalTranType GetJournalTranTypeByID(int pJournalTranTypeID, DBContext dc)
        {
            dcJournalTranType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalTranTypeList_SQLString());
                sb.Append(" AND tblJournalTranType.JournalTranTypeID=@journalTranTypeID");
                cmdInfo.DBParametersInfo.Add("@journalTranTypeID", pJournalTranTypeID);
                sb.Append(" ORDER BY tblJournalTranType.JournalTranTypeSLNo");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcJournalTranType>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }
    }
}
