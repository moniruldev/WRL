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
    public class JournalAdjustTypeBL
    {
        public static string GetJournalAdjustTypeList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalAdjustType.* ");
            sb.Append(" FROM tblJournalAdjustType ");
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
        public static List<dcJournalAdjustType> GetJournalAdjustTypeList()
        {
            return GetJournalAdjustTypeList(null);
        }
        public static List<dcJournalAdjustType> GetJournalAdjustTypeList(DBContext dc)
        {
            List<dcJournalAdjustType> cObjList = new List<dcJournalAdjustType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalAdjustTypeList_SQLString());
                sb.Append(" AND tblJournalAdjustType.IsVisible=@isVisible");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
                sb.Append(" order by JournalAdjustTypeSLNo");
                


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcJournalAdjustType>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcJournalAdjustType>()
                //                where c.IsVisible == true
                //                orderby c.JournalAdjustTypeSLNo
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcJournalAdjustType GetJournalAdjustTypeByID(int pJournalAdjustTypeID)
        {
            return GetJournalAdjustTypeByID(pJournalAdjustTypeID, null);
        }
        public static dcJournalAdjustType GetJournalAdjustTypeByID(int pJournalAdjustTypeID, DBContext dc)
        {
            dcJournalAdjustType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalAdjustTypeList_SQLString());
                sb.Append(" AND tblJournalAdjustType.IsVisible=@isVisible AND tblJournalAdjustType.JournalAdjustTypeID=@journalAdjustTypeID ");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
                cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", pJournalAdjustTypeID);
                sb.Append(" order by JournalAdjustTypeSLNo");



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcJournalAdjustType>(dbq, dc).FirstOrDefault();
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
