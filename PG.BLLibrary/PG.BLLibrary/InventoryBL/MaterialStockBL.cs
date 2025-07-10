using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass.InventoryDC;

namespace PG.BLLibrary.InventoryBL
{
    public class MaterialStockBL
    {
        public static List<dcITEM_STOCK_DETAILS> GetItemStockforLocalitemtypePreview(ReportParameterClass rptClass, DBContext dc)
        {
            return GetItemStockforLocalitemtypePreview(0, rptClass, 0, dc);
        }
        public static List<dcITEM_STOCK_DETAILS> GetItemStockforLocalitemtypePreview(int pGLClassID, ReportParameterClass rptClass, int pActiveOption, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
               
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemStockforLocalitemtype_SQLString());
                if (rptClass.FromDatef != "01-Jan-0001")
                {
                    sb.Append("AND ITEM_STOCK_DETAILS.TRANS_DATE >='" + rptClass.FromDatef + "'");
                }
                if (rptClass.ToDatet != "01-Jan-0001")
                {
                    sb.Append("AND ITEM_STOCK_DETAILS.TRANS_DATE <='" + rptClass.ToDatet + "'");
                }
                if (rptClass.ItemGroupId>0)
                {
                    sb.Append("AND INV_ITEM_MASTER.ITEM_GROUP_ID  ='" + rptClass.ItemGroupId + "'");
                }
                if (rptClass.Item != "")
                {
                    sb.Append("AND INV_ITEM_MASTER.ITEM_ID ='" + rptClass.Item + "'");
                }
                if (rptClass.StoreId != "0")
                {
                    sb.Append("AND ITEM_STOCK_DETAILS.STORE_ID ='" + rptClass.StoreId + "'");
                }
                sb.Append(" ORDER BY INV_ITEM_MASTER.ITEM_NAME ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = GetItemStockforLocalitemtypeList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static string GetItemStockforLocalitemtype_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT INV_ITEM_MASTER.ITEM_CODE,INV_ITEM_MASTER.ITEM_NAME,INV_ITEM_MASTER.ITEM_GROUP_ID,UOM_INFO.UOM_NAME,OPENING_BALANCE.OPENING_QTY,ITEM_STOCK_DETAILS.TRANS_DATE,ITEM_STOCK_DETAILS.RCV_QTY,ITEM_STOCK_DETAILS.ISS_QTY ");
            sb.Append(" FROM ITEM_STOCK_DETAILS ");
            sb.Append(" left JOIN OPENING_BALANCE ON ITEM_STOCK_DETAILS.ITEM_ID = OPENING_BALANCE.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER ON ITEM_STOCK_DETAILS.ITEM_ID = INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON UOM_INFO.UOM_ID = INV_ITEM_MASTER.UOM_ID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        public static List<dcITEM_STOCK_DETAILS> GetItemStockforLocalitemtypeList(DBQuery dbq, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcITEM_STOCK_DETAILS>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

    }
}
