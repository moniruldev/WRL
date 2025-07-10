using PG.Core.DBBase;
using PG.Core.Utility;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
    public class ITEM_STOCK_DETAILBL
    {
        public static string Item_Stock_Detail_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select * FROM INV_BIN ");
            return sb.ToString();
        }

        public static string Dept_ITC_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM INV_ITC_ALLOW_DEPT ");
            return sb.ToString();
        }

        public static DataLoadOptions Item_Stock_DetailLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcELECTROLYTE_GRAVITY>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcITEM_STOCK_DETAILS> Item_Stock_Detail_List()
        {
            return Item_Stock_Detail_List(null, null, null);
        }
        public static List<dcITEM_STOCK_DETAILS> Item_Stock_Detail_List(DBContext dc)
        {
            return Item_Stock_Detail_List(null, dc, null);
        }
        public static List<dcITEM_STOCK_DETAILS> Item_Stock_Detail_List(string rackId)
        {
            return Item_Stock_Detail_List(null, null, rackId);
        }
        public static List<dcITEM_STOCK_DETAILS> Item_Stock_Detail_List(DBQuery dbq, DBContext dc, string rackId)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(Item_Stock_Detail_SQLString());
                    if (!String.IsNullOrEmpty(rackId))
                    {
                        sb.Append("Where RACK_ID=@RackId");
                        cmdInfo.DBParametersInfo.Add("@RackId", rackId);
                    }

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcITEM_STOCK_DETAILS>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcITEM_STOCK_DETAILS Get_Stock_Detail_By_Item_ID_And_Transaction_Id(int itemid, string refNo)
        {
            return Get_Stock_Detail_By_Item_ID_And_Transaction_Id(itemid, refNo, null);
        }
        public static dcITEM_STOCK_DETAILS Get_Stock_Detail_By_Item_ID_And_Transaction_Id(int itemid, string refNo, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Select * from ITEM_STOCK_DETAILS where 1=1 ");
                    sb.Append(" AND ITEM_ID=@P_ITEM_ID ");
                    sb.Append(" AND TRANS_REF_NO=@P_REF_NO ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemid);
                    cmdInfo.DBParametersInfo.Add("@P_REF_NO", refNo);
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcITEM_STOCK_DETAILS>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList.FirstOrDefault();
        }

        public static int Insert(dcITEM_STOCK_DETAILS cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcITEM_STOCK_DETAILS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcITEM_STOCK_DETAILS>(cObj, false);
                //if (id > 0) { cObj.ELECTROLYTE_GRAVITYID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcITEM_STOCK_DETAILS cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcITEM_STOCK_DETAILS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcITEM_STOCK_DETAILS key = new dcITEM_STOCK_DETAILS();
            key.ITEM_STK_DET_ID = cObj.ITEM_STK_DET_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcITEM_STOCK_DETAILS>(cObj, key);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(Int64 id)
        {
            return Delete(id, null);
        }
        public static bool Delete(Int64 id, DBContext dc)
        {
            dcITEM_STOCK_DETAILS cObj = new dcITEM_STOCK_DETAILS();
            cObj.ITEM_STK_DET_ID = id;

            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcITEM_STOCK_DETAILS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }


        public static int Save(dcITEM_STOCK_DETAILS cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcITEM_STOCK_DETAILS cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcITEM_STOCK_DETAILS cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcITEM_STOCK_DETAILS cObj, DBContext dc)
        {
            int newID = 0;
            bool isDCInit = false;
            bool isTransInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                using (DataContext dataContext = dc.NewDataContext())
                {

                    switch (cObj._RecordState)
                    {
                        /*
                    case Interwave.Core.DBClass.RecordStateEnum.Added:
                        newID = Insert(cObj, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        if (Update(cObj, dc))
                        {
                            newID = cObj.ELECTROLYTE_GRAVITYID;
                        }
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        if (Delete(cObj.ELECTROLYTE_GRAVITYID, dc))
                        {
                            newID = 1;
                        }
                        break; */
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;

                        ///code list save logic here

                        bStatus = true;
                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                    }
                }
            }
            catch
            {
                dc.RollbackTransaction(isTransInit);
                throw;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return newID;
        }

        public static bool SaveList(List<dcITEM_STOCK_DETAILS> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcITEM_STOCK_DETAILS> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcITEM_STOCK_DETAILS oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    /* case Interwave.Core.DBClass.RecordStateEnum.Added:
                         int a = Insert(oDet, dc);
                         break;
                     case Interwave.Core.DBClass.RecordStateEnum.Edited:
                         bool e = Update(oDet, dc);
                         break;
                     case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                         bool d = Delete(oDet.ELECTROLYTE_GRAVITYID, dc);
                         break; */
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }

        public static string Get_Item_Stock_Detail_By_Id(string storeId, DBContext dc)
        {
            bool isDCInit = false;
            string bin_no = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_BIN_ID(@RackId) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@RackId", storeId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                bin_no = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return bin_no;
        }

        public static decimal GET_DEPT_CLOSING_QTY(int itemId,int deptId,int? specId, DBContext dc)
        {
            bool isDCInit = false;
            decimal closingQty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT GET_DEPT_CLOSING_QTY(@P_ITEM_ID,@P_DEPT_ID,@P_SPEC_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);
                cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", deptId);
                cmdInfo.DBParametersInfo.Add("@P_SPEC_ID", specId);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closingQty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closingQty;
        }

        public static decimal GET_DEPT_CLOSING_QTY_BY_TYPE(int itemId, int deptId, int? specId,int? itemtypeid, DBContext dc)
        {
            bool isDCInit = false;
            decimal closingQty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT GET_DEPT_CLOSING_QTY_BY_TYPE(@P_ITEM_ID,@P_DEPT_ID,@P_SPEC_ID,@P_ITEM_TYPE_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);
                cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", deptId);
                cmdInfo.DBParametersInfo.Add("@P_SPEC_ID", specId);
                cmdInfo.DBParametersInfo.Add("@P_ITEM_TYPE_ID", itemtypeid);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closingQty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closingQty;
        }

        public static decimal GET_SOLAR_LOAD_AVAILABLE_QTY(int itemId, int deptId, int? specId, DBContext dc)
        {
            bool isDCInit = false;
            decimal closingQty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT GET_SOLAR_LOAD_AVAILABLE_QTY(@P_ITEM_ID,@P_DEPT_ID,@P_SPEC_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);
                cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", deptId);
                cmdInfo.DBParametersInfo.Add("@P_SPEC_ID", specId);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closingQty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closingQty;
        }

        public static decimal GET_SOLAR_AVAILABLE_PACKING_QTY(int itemId, DBContext dc)
        {
            bool isDCInit = false;
            decimal closingQty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT F_GET_AVAILABLE_PACKING_QTY(@P_ITEM_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);               
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closingQty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closingQty;
        }


        public static decimal GET_SOLAR_AVAILABLE_CHARGING_QTY(int itemId,int? specId, DBContext dc)
        {
            bool isDCInit = false;
            decimal closingQty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT F_GET_AVAILABLE_CHARGING_QTY(@P_ITEM_ID,@P_SPEC_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);
                cmdInfo.DBParametersInfo.Add("@P_SPEC_ID", specId);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closingQty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closingQty;
        }

        public static decimal Get_Dept_Item_Closing_Stock(int deptId, int itemId, DBContext dc)
        {
            bool isDCInit = false;
            decimal closing_qty = 0;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT GET_DEPARTMENT_CLOSING_QTY(@P_ITEM_ID,@P_DEPT_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);
                cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", deptId);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closing_qty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closing_qty;
        }

        public static decimal Get_Dept_Reject_Closing_Stock(int deptId, int itemId, DBContext dc)
        {
            bool isDCInit = false;
            decimal closing_qty = 0;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT GET_DEPT_REJECT_CLOSING(@P_ITEM_ID,@P_DEPT_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);
                cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", deptId);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closing_qty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closing_qty;
        }



        public static string Get_Specification_Issue_receive_Id(string storeId, DBContext dc)
        {
            bool isDCInit = false;
            string bin_no = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_BIN_ID(@RackId) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@RackId", storeId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                bin_no = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return bin_no;
        }


        public static string Get_Allow_Dept_Id(int DeptID, DBContext dc)
        {
            bool isDCInit = false;
            string deptID = String.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT COUNT(*) FROM INV_ITC_ALLOW_DEPT WHERE DEPT_ID=@DeptID ";
                cmdInfo.DBParametersInfo.Add("@DeptID", DeptID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                deptID = Convert.ToString(  DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return deptID;
        }

        public static decimal Get_PureLead_Used_Stock( int itemId, DateTime? itcDate,string P_MONTH_INNER,  DBContext dc)
        {
            bool isDCInit = false;
            decimal closing_qty = 0;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT GET_CUR_MONPURELEADUSED_QTY_V2(@P_ITEM_ID,@P_ITC_DATE,@P_MONTH_INNER) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", itemId);
                cmdInfo.DBParametersInfo.Add("@P_ITC_DATE", itcDate);
                cmdInfo.DBParametersInfo.Add("@P_MONTH_INNER", P_MONTH_INNER);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closing_qty = Conversion.StringToDecimal(DBQuery.ExecuteDBScalar(dbq, dc).ToString());
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closing_qty;
        }




        #region ****************************************************IGR_ITC_IRR********************************************


        public static string GetIGR_ITC_IRR_List_SQLString(ReportParameterClass rptClass)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("  SELECT RM.REQ_NO ");
            sb.Append("  ,RM.REQ_ID ");
            sb.Append("  ,RM.REQ_DATE,RM.REQ_TIME,DF.DEPARTMENT_NAME FROM_DEPT,DT.DEPARTMENT_NAME TO_DEPT ");
            sb.Append("  , RIM.REQ_ISSUE_NO ITC_NO ");
            sb.Append("  ,RIM.REQ_ISSUE_ID ");
            sb.Append("  ,RIM.REQ_ISSUE_TIME ");
            sb.Append("  ,IRM.ISSUE_RECEIVE_NO IRR_NO ");
            sb.Append("  ,IRM.ISSUE_RECEIVE_ID ");
            sb.Append("  ,IRM.ISSUE_RECEIVE_TIME ");
            sb.Append("  FROM REQ_MASTER RM   ");
            sb.Append("  INNER JOIN DEPARTMENT_INFO DF ON RM.FROM_DEPARTMENT_ID=DF.DEPARTMENT_ID   ");
            sb.Append("  INNER JOIN DEPARTMENT_INFO DT ON RM.TO_DEPERATMENT_ID=DT.DEPARTMENT_ID  "); 
            sb.Append("  INNER JOIN REQ_ISSUE_MASTER RIM ON RM.REQ_ID=RIM.REQ_ID   ");
            sb.Append("  LEFT JOIN ISSUE_RECEIVE_MASTER IRM ON RIM.REQ_ISSUE_ID=IRM.REQ_ISSUE_ID   ");
  
            sb.Append(" WHERE 1=1 ");

            if (rptClass.DepartmentId != "0")
            {
                sb.Append(" AND RM.FROM_DEPARTMENT_ID ='" + rptClass.DepartmentId + "'");
            }
            if (rptClass.ToDatet != "01-Jan-0001")
            {
                sb.Append(" AND (RM.REQ_DATE BETWEEN '" + rptClass.FromDatef + "' AND '" + rptClass.ToDatet + "') ");
            }
            sb.Append(" ORDER BY RM.REQ_NO,rm.REQ_DATE ");
            return sb.ToString();
        }

        public static List<dcInternalReceiveReport> GetIGR_ITC_IRR_List(ReportParameterClass rptClass)
        {
            return GetIGR_ITC_IRR_List(rptClass, null);
        }
        public static List<dcInternalReceiveReport> GetIGR_ITC_IRR_List(ReportParameterClass rptClass, DBContext dc)
        {
            List<dcInternalReceiveReport> cObjList = new List<dcInternalReceiveReport>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetIGR_ITC_IRR_List_SQLString(rptClass));
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcInternalReceiveReport>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return cObjList;
        }


        public static string Delete_ITC_IRR_By_ID(ReportParameterClass pObj, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
               


                //***********************************  Delete IRR *********************************************//
               if(pObj.IRRNo!="")
               {
                   DBCommandInfo cmdInfoRSTK = new DBCommandInfo();
                   string irrStk = " Delete from ITEM_STOCK_DETAILS where TRANS_REF_NO=@pISSUE_RECEIVE_NO ";
                   cmdInfoRSTK.DBParametersInfo.Add("@pISSUE_RECEIVE_NO", pObj.IRRNo);
                   DBQuery dbqRSTK = new DBQuery();
                   dbqRSTK.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                   cmdInfoRSTK.CommandText = irrStk;
                   cmdInfoRSTK.CommandType = CommandType.Text;
                   dbqRSTK.DBCommandInfo = cmdInfoRSTK;
                   int i = DBQuery.ExecuteDBNonQuery(dbqRSTK, dc);


                   DBCommandInfo cmdInfoRD = new DBCommandInfo();
                   string irrDTL = " Delete from ISSUE_RECEIVE_DETAILS where ISSUE_RECEIVE_ID=@pISSUE_RECEIVE_ID ";
                   cmdInfoRD.DBParametersInfo.Add("@pISSUE_RECEIVE_ID", pObj.ISSUE_RECEIVE_ID);
                   DBQuery dbqRD = new DBQuery();
                   dbqRD.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                   cmdInfoRD.CommandText = irrDTL;
                   cmdInfoRD.CommandType = CommandType.Text;
                   dbqRD.DBCommandInfo = cmdInfoRD;
                   int j = DBQuery.ExecuteDBNonQuery(dbqRD, dc);


                   DBCommandInfo cmdInfoRM = new DBCommandInfo();
                   string irrmst = " Delete from ISSUE_RECEIVE_MASTER where ISSUE_RECEIVE_ID=@pISSUE_RECEIVE_ID ";
                   cmdInfoRM.DBParametersInfo.Add("@pISSUE_RECEIVE_ID", pObj.ISSUE_RECEIVE_ID);
                   DBQuery dbqRM = new DBQuery();
                   dbqRM.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                   cmdInfoRM.CommandText = irrmst;
                   cmdInfoRM.CommandType = CommandType.Text;
                   dbqRM.DBCommandInfo = cmdInfoRM;
                   int k = DBQuery.ExecuteDBNonQuery(dbqRM, dc);

               }
               //************************************ END iRR ***********************************************//



               //************************************ Start ITC ***********************************************//


               if (pObj.ITC_NO != "")
               {
                   DBCommandInfo cmdInfoISSTK = new DBCommandInfo();
                   string issStk = " Delete from ITEM_STOCK_DETAILS where TRANS_REF_NO=@pITC_NO ";
                   cmdInfoISSTK.DBParametersInfo.Add("@pITC_NO", pObj.ITC_NO);
                   DBQuery dbqISSTK = new DBQuery();
                   dbqISSTK.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                   cmdInfoISSTK.CommandText = issStk;
                   cmdInfoISSTK.CommandType = CommandType.Text;
                   dbqISSTK.DBCommandInfo = cmdInfoISSTK;
                   int i = DBQuery.ExecuteDBNonQuery(dbqISSTK, dc);


                   DBCommandInfo cmdInfoissD = new DBCommandInfo();
                   string issDTL = " Delete from REQ_ISSUE_DETAILS where REQ_ISSUE_ID=@pREQ_ISSUE_ID ";
                   cmdInfoissD.DBParametersInfo.Add("@pREQ_ISSUE_ID", pObj.REQ_ISSUE_ID);
                   DBQuery dbqissD = new DBQuery();
                   dbqissD.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                   cmdInfoissD.CommandText = issDTL;
                   cmdInfoissD.CommandType = CommandType.Text;
                   dbqissD.DBCommandInfo = cmdInfoissD;
                   int j = DBQuery.ExecuteDBNonQuery(dbqissD, dc);


                   DBCommandInfo cmdInfoissM = new DBCommandInfo();
                   string issmst = " Delete from REQ_ISSUE_MASTER where REQ_ISSUE_ID=@pREQ_ISSUE_ID ";
                   cmdInfoissM.DBParametersInfo.Add("@pREQ_ISSUE_ID", pObj.REQ_ISSUE_ID);
                   DBQuery dbqissM = new DBQuery();
                   dbqissM.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                   cmdInfoissM.CommandText = issmst;
                   cmdInfoissM.CommandType = CommandType.Text;
                   dbqissM.DBCommandInfo = cmdInfoissM;
                   int k = DBQuery.ExecuteDBNonQuery(dbqissM, dc);

               }
                
                
                
             


                //************************************ END ITC ***********************************************//






            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }
        #endregion
    }
}
