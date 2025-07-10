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
    /// <summary>
    /// GeneralLedgerBL
    /// Last update By Moni, Date 15-03-2015
    /// </summary>
    public class JournalTypeClassBL
    {

        public static string GetJournalTypeClassList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalTypeClass.* ");
            sb.Append(" FROM tblJournalTypeClass ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions JournalTypeClassLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcJournalTypeClass> GetJournalTypeClassList()
        {
            return GetJournalTypeClassList(null);
        }
        public static List<dcJournalTypeClass> GetJournalTypeClassList(DBContext dc)
        {
            List<dcJournalTypeClass> cObjList = new List<dcJournalTypeClass>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalTypeClassList_SQLString());
                sb.Append(" AND tblJournalTypeClass.IsVisible=@isVisible");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
                sb.Append(" orderby tblJournalTypeClass.JournalTypeClassName ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcJournalTypeClass>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcJournalTypeClass>()
                //                where c.IsVisible == true
                //                orderby c.JournalTypeClassName 
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcJournalTypeClass GetJournalTypeClassByID(int pJournalTypeClassID)
        {
            return GetJournalTypeClassByID(pJournalTypeClassID, null);
        }
        public static dcJournalTypeClass GetJournalTypeClassByID(int pJournalTypeClassID, DBContext dc)
        {
            dcJournalTypeClass cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalTypeClassList_SQLString());
                sb.Append(" AND tblJournalTypeClass.IsVisible=@isVisible AND tblJournalTypeClass.JournalTypeClassID=@journalTypeClassID");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
                cmdInfo.DBParametersInfo.Add("@journalTypeClassID", pJournalTypeClassID);
                sb.Append(" orderby tblJournalTypeClass.JournalTypeClassName ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcJournalTypeClass>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcJournalTypeClass>()
                //                  where c.JournalTypeClassID == pJournalTypeClassID
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
