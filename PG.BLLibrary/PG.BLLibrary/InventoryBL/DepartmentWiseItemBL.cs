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
    public class DepartmentWiseItemBL
    {
        public static List<dcDEPARTMENT_INFO> GetDepartment()
        {
            return GetDepartmentList(null);
        }
        public static List<dcDEPARTMENT_INFO> GetDepartmentList(DBContext dc)
        {
            List<dcDEPARTMENT_INFO> cObjList = new List<dcDEPARTMENT_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetDepartment_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static string GetDepartment_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT *  FROM DEPARTMENT_INFO ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" order by DEPARTMENT_NAME ");
            return sb.ToString();
        }

        public static List<dcINV_ITEM_GROUP> GetItemGroup()
        {
            return GetItemGroupList(null);
        }
        public static List<dcINV_ITEM_GROUP> GetItemGroupList(DBContext dc)
        {
            List<dcINV_ITEM_GROUP> cObjList = new List<dcINV_ITEM_GROUP>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemGroup_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_GROUP>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static string GetItemGroup_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT *  FROM INV_ITEM_GROUP");
            sb.Append(" WHERE 1=1 order by ITEM_GROUP_NAME ");
            return sb.ToString();
        }

        public static List<dcINV_ITEM_MASTER> GetItemByItemGroup(ReportParameterClass rptClass)
        {
            return GetItemByItemGroupList(rptClass, null);
        }
        public static List<dcINV_ITEM_MASTER> GetItemByItemGroupList(ReportParameterClass rptClass, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemByItemGroup_SQLString());
                if (rptClass.ItemGroupId>0)
                {
                    sb.Append(" and INV_ITEM_MASTER.ITEM_GROUP_ID=@GroupId ");
                    cmdInfo.DBParametersInfo.Add("@GroupId", rptClass.ItemGroupId);
                }
                
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return cObjList;
        }
        public static string GetItemByItemGroup_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" Where 1=1 "); ;

            //sb.Append(" SELECT *  FROM INV_ITEM_MASTER");
            //sb.Append(" WHERE ITEM_GROUP_ID='" + rptClass.ItemGroupId + "'");
            //sb.Append(" and ITEM_ID IS  not null ");
            //sb.Append(" order by ITEM_NAME");
            return sb.ToString();
        }
       
        public static bool UpdateItem(dcDEPARTMENT_ITEM cObj)
        {
            return UpdateItem(cObj, null);
        }

        public static bool UpdateItem(dcDEPARTMENT_ITEM cObj, DBContext dc)
        {
           
            if (cObj.DEPT_ID =="0")
            {
                return false;
            }
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            if (cObj != null)
            {
                dcDEPARTMENT_ITEM objItem = new dcDEPARTMENT_ITEM();
                objItem.DEPT_ID = cObj.DEPT_ID;
                objItem.ITEM_ID = cObj.ITEM_ID;
                cnt = dc.DoInsert<dcDEPARTMENT_ITEM>(objItem);
            }
            
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool SaveList(List<dcDEPARTMENT_ITEM> cList)
        {
            return SaveList(cList, null);
        }
        public static bool SaveList(List<dcDEPARTMENT_ITEM> cList, DBContext dc)
        {
            bool bSataus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);           
        
            foreach (dcDEPARTMENT_ITEM cObj in cList)
            {
                bSataus = UpdateItem(cObj, dc);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bSataus = true;
            return bSataus;
        }

        public static dcDEPARTMENT_ITEM GetItemByDepartmentID(string DeptId,int ItemId)
        {
            return GetItemByDepartmentID(DeptId,ItemId, null);
        }
        public static dcDEPARTMENT_ITEM GetItemByDepartmentID(string DeptId,int ItemId, DBContext dc)
        {
            dcDEPARTMENT_ITEM cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemByDepartment_SQLString(DeptId, ItemId));
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcDEPARTMENT_ITEM>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static string GetItemByDepartment_SQLString(string DeptId, int ItemId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT *  FROM DEPARTMENT_ITEM ");
            sb.Append(" WHERE 1=1 ");
            if (!String.IsNullOrEmpty(DeptId))
            {
                sb.Append(" AND DEPT_ID ='" + DeptId + "' ");
            }
            if (ItemId != 0)
            {
                sb.Append(" AND ITEM_ID ='" + ItemId + "' ");
            }
            return sb.ToString();
        }




        public static List<dcDEPARTMENT_ITEM> GetItemByDepartmentCode(string deptId)
        {
            return GetItemByDepartmentCode(deptId, null);
        }
        public static List<dcDEPARTMENT_ITEM> GetItemByDepartmentCode(string deptId, DBContext dc)
        {
            List<dcDEPARTMENT_ITEM> cObjList = new List<dcDEPARTMENT_ITEM>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemByDepartment_SQLString(deptId));

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_ITEM>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string GetItemByDepartment_SQLString(string deptId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DEPARTMENT_ITEM.* ");
            sb.Append(" ,INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" FROM DEPARTMENT_ITEM ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER ON DEPARTMENT_ITEM.ITEM_ID = INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" WHERE 1=1 ");
            if (!String.IsNullOrEmpty(deptId))
            {
                sb.Append("AND DEPT_ID ='" + deptId + "'");
            }
           
            return sb.ToString();
        }

     

        public static bool Delete(int itemId,int deptId, DBContext dc)
        {
            int i = 0;

          
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(" Delete from DEPARTMENT_ITEM where DEPT_ID=@deptId and ITEM_ID=@itemId ");
                cmdInfo.DBParametersInfo.Add("@deptId",deptId);
                cmdInfo.DBParametersInfo.Add("@itemId", itemId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                i = DBQuery.ExecuteDBNonQuery(dbq, dc);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }


            return i > 0;
        }
    }
}
