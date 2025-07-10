using PG.Core.DBBase;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionBL
{
    public class BATERY_CATEGORYBL
    {
        public static string SND_GetBattery_Category_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select BATERY_CAT_ID,BATERY_CAT_DESCR FROM BATERY_CATEGORY Where MST_CAT_ID='BAT01' ");
            sb.Append(" ORDER BY BATERY_CAT_DESCR asc ");
            return sb.ToString();
        }

        public static string SND_GetBattery_Category_SQLString_Info()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select BATERY_CAT_ID,BATERY_CAT_DESCR FROM BATERY_CATEGORY Where MST_CAT_ID='BAT01' ");
            
            return sb.ToString();
        }

        public static List<dcBATERY_CATEGORY> SND_GetBattery_CategoryList()
        {
            return SND_GetBattery_CategoryList(null);
        }
        public static List<dcBATERY_CATEGORY> SND_GetBattery_CategoryList(DBContext dc)
        {
            List<dcBATERY_CATEGORY> cObjList = new List<dcBATERY_CATEGORY>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(SND_GetBattery_Category_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcBATERY_CATEGORY>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcCompany>()
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcBATERY_CATEGORY> GetBattery_CategoryList(DBQuery dbq, DBContext dc)
        {
            List<dcBATERY_CATEGORY> cObjList = new List<dcBATERY_CATEGORY>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBATERY_CATEGORY>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

    }
}
