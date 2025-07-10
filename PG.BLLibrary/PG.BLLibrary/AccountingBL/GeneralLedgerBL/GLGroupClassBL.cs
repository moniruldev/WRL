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
    public class GLGroupClassBL
    {
        public static string GetGLGroupClassList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblGLGroupClass.* ");
            sb.Append(" FROM tblGLGroupClass ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions GLGroupClassLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcGLGroupClass> GetGLGroupClassList()
        {
            return GetGLGroupClassList(null);
        }
        public static List<dcGLGroupClass> GetGLGroupClassList(DBContext dc)
        {
            List<dcGLGroupClass> cObjList = new List<dcGLGroupClass>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLGroupClassList_SQLString());
                sb.Append(" orderby tblGLGroupClass.GLClassID, tblGLGroupClass.GLGroupClassSLNo ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcGLGroupClass>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcGLGroupClass>()
                //                orderby c.GLClassID, c.GLGroupClassSLNo
                //                 select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcGLGroupClass GetGLGroupClassByID(int pGLGroupClassID)
        {
            return GetGLGroupClassByID(pGLGroupClassID, null);
        }
        public static dcGLGroupClass GetGLGroupClassByID(int pGLGroupClassID, DBContext dc)
        {
            dcGLGroupClass cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLGroupClassList_SQLString());
                sb.Append(" AND tblGLGroupClass.GLGroupClassID=@isVisible");
                cmdInfo.DBParametersInfo.Add("@gLGroupClassID", pGLGroupClassID);
                

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcGLGroupClass>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcGLGroupClass>()
                //                  where c.GLGroupClassID == pGLGroupClassID
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

        public static bool IsGLClassMatchedForGLGroup(int pCompanyID, int pGLGroupID, int pGLGroupClassID)
        {
            return IsGLClassMatchedForGLGroup(pCompanyID, pGLGroupID, pGLGroupClassID, null);
        }

        public static bool IsGLClassMatchedForGLGroup(int pCompanyID, int pGLGroupID, int pGLGroupClassID, DBContext dc)
        {
            bool bStatus = false;
            dcGLGroupClass grpClass = GetGLGroupClassByID(pGLGroupClassID, dc);
            dcGLGroup glGroup = GLGroupBL.GetGLGroupByID(pCompanyID, pGLGroupID, dc);
            bStatus = grpClass.GLClassID == glGroup.GLClassID;
            return bStatus;
        }


    }
}
