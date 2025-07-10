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
    public class GLAccountTypeBL
    {
        public static string GetAccType_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblGLAccountType.* ");
            sb.Append(" FROM tblGLAccountType ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions AccYearLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
            return dlo;
        }
        public static List<dcGLAccountType> GetGLAccountTypeList()
        {
            return GetGLAccountTypeList(null);
        }
        public static List<dcGLAccountType> GetGLAccountTypeList(DBContext dc)
        {
            List<dcGLAccountType> cObjList = new List<dcGLAccountType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccType_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcGLAccountType>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcGLAccountType>()
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcGLAccountType GetAccYearByID(int pGLAccountTypeID)
        {
            return GetAccYearByID(pGLAccountTypeID, null);
        }
        public static dcGLAccountType GetAccYearByID(int pGLAccountTypeID, DBContext dc)
        {
            dcGLAccountType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccType_SQLString());
                sb.Append(" AND tblGLAccountType.GLAccountTypeID=@gLAccountTypeID");
                cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", pGLAccountTypeID);
               

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcGLAccountType>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcGLAccountType>()
                //                  where c.GLAccountTypeID == pGLAccountTypeID
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

        public static int Insert(dcGLAccountType cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcGLAccountType cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcGLAccountType>(cObj, true);
                if (id > 0) { cObj.GLAccountTypeID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcGLAccountType cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcGLAccountType cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcGLAccountType>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pGLAccountTypeID)
        {
            return Delete(pGLAccountTypeID, null);
        }
        public static bool Delete(int pGLAccountTypeID, DBContext dc)
        {
            dcGLAccountType cObj = new dcGLAccountType();
            cObj.GLAccountTypeID = pGLAccountTypeID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcGLAccountType>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
    }
}
