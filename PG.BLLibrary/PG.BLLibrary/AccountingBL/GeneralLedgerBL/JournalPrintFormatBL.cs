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
    public class JournalReportFormatBL
    {
        public static string GetJournalReportFormatList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblJournalReportFormat.* ");
            sb.Append(" FROM tblJournalReportFormat ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions GetJournalReportFormatOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcJournalReportFormat> GetJournalReportFormatList()
        {
            return GetJournalReportFormatList(null);
        }
        public static List<dcJournalReportFormat> GetJournalReportFormatList(DBContext dc)
        {
            List<dcJournalReportFormat> cObjList = new List<dcJournalReportFormat>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalReportFormatList_SQLString());
                sb.Append(" AND tblJournalReportFormat.IsVisible=@isVisible");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
              



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcJournalReportFormat>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcJournalReportFormat>()
                //                where c.IsVisible == true
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcJournalReportFormat GetJournalReportFormatByID(int pJournalReportFormatID)
        {
            return GetJournalReportFormatByID(pJournalReportFormatID, null);
        }
        public static dcJournalReportFormat GetJournalReportFormatByID(int pJournalReportFormatID, DBContext dc)
        {
            dcJournalReportFormat cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalReportFormatList_SQLString());
                sb.Append(" AND tblJournalReportFormat.IsVisible=@isVisible AND JournalReportFormatID=@journalReportFormatID");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
                cmdInfo.DBParametersInfo.Add("@journalReportFormatID", pJournalReportFormatID);




                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcJournalReportFormat>(dbq, dc).FirstOrDefault();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcJournalReportFormat>()
                //                  where c.JournalReportFormatID == pJournalReportFormatID
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

        public static int Insert(dcJournalReportFormat cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcJournalReportFormat cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcJournalReportFormat>(cObj, true);
                if (id > 0) { cObj.JournalReportFormatID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcJournalReportFormat cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcJournalReportFormat cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcJournalReportFormat>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pJournalReportFormatID)
        {
            return Delete(pJournalReportFormatID, null);
        }
        public static bool Delete(int pJournalReportFormatID, DBContext dc)
        {
            dcJournalReportFormat cObj = new dcJournalReportFormat();
            cObj.JournalReportFormatID = pJournalReportFormatID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcJournalReportFormat>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
    }
}
