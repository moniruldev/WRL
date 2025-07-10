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
using PG.DBClass.InventoryDC;


namespace PG.BLLibrary.InventoryBL
{
    public class UOM_INFOBL
    {
        public static string GetUOM_INFO_List_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UOM_INFO.* ");
            sb.Append(" FROM UOM_INFO ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" ORDER BY UOM_CODE ");
            return sb.ToString();
        }

        public static List<dcUOM_INFO> GetUOMList()
        {
            return GetUOMList(null);
        }
        public static List<dcUOM_INFO> GetUOMList(DBContext dc)
        {
            List<dcUOM_INFO> cObjList = new List<dcUOM_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetUOM_INFO_List_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcUOM_INFO>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcInvSettings GetUOMByID(int pUOMID)
        {
            return GetUOMByID(pUOMID, null);
        }
        public static dcInvSettings GetUOMByID(int pUOMID, DBContext dc)
        {
            dcInvSettings cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetUOM_INFO_List_SQLString());


                sb.Append(" AND UOM_INFO.UOM_ID=@UOMID ");
                cmdInfo.DBParametersInfo.Add("@UOMID", pUOMID);
    

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInvSettings>(dbq, dc).FirstOrDefault();
  
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static List<dcUOM_INFO> GetUOMINFO_List(DBQuery dbq, DBContext dc)
        {
            List<dcUOM_INFO> cObjList = new List<dcUOM_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                        //dbq.OrderBy = "YearStartDate Desc";
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcUOM_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
