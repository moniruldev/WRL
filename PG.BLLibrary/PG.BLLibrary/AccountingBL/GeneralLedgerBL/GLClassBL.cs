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
    public class GLClassBL
    {
        public static string GetGLClass_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblGLClass.* ");
            sb.Append(" FROM tblGLClass ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions GLClassLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
            return dlo;
        }
        public static List<dcGLClass> GetGLClassList()
        {
            return GetGLClassList(null);
        }
        public static List<dcGLClass> GetGLClassList(DBContext dc)
        {
            List<dcGLClass> cObjList = new List<dcGLClass>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLClass_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcGLClass>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcGLClass>()
                //                orderby c.GLClassSLNo
                //                 select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcGLClass GetGLClassByID(int pGLClassID)
        {
            return GetGLClassByID(pGLClassID, null);
        }
        public static dcGLClass GetGLClassByID(int pGLClassID, DBContext dc)
        {
            dcGLClass cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLClass_SQLString());
                sb.Append(" AND tblGLClass.GLClassID=@gLClassID ");
                cmdInfo.DBParametersInfo.Add("@gLClassID", pGLClassID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcGLClass>(dbq, dc).FirstOrDefault();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcGLClass>()
                //                  where c.GLClassID == pGLClassID
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
