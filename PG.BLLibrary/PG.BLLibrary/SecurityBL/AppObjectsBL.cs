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
using PG.DBClass.SecurityDC;

namespace PG.BLLibrary.SecurityBL
{

    /// <summary>
    /// AppObjectsBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    
    public class AppObjectsBL
    {

        public static string GetAppObject_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAppObject.* ");
            sb.Append(" FROM tblAppObject ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions AppObjectsLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.SystemDC.dcAppObject>(obj => obj.AppObjType);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
            return dlo;
        }


        public static List<dcAppObject> GetAppObjectList(int pAppID)
        {
            return GetAppObjectList(pAppID, false, null);
        }
        public static List<dcAppObject> GetAppObjectList(int pAppID, bool pLoadAssociaiton)
        {
            return GetAppObjectList(pAppID, pLoadAssociaiton, null);
        }
        public static List<dcAppObject> GetAppObjectList(int pAppID, bool pLoadAssociaiton, DBContext dc)
        {
            List<dcAppObject> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAppObject_SQLString());
                sb.Append(" AND tblAppObject.AppID=@appID ");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAppObject>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (pLoadAssociaiton)
                //    {
                //        dataContext.LoadOptions = AppObjectsLoadOptions();
                //    }

                //    cObjList = (from c in dataContext.GetTable<dcAppObject>()
                //                where c.AppID == pAppID
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
