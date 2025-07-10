using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Linq;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Reflection;
//using System.Linq.Dynamic;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using Oracle.ManagedDataAccess.Client;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.DBClass.SecurityDC;

namespace PG.BLLibrary.SecurityBL
{
    public class DataTransferProcessBL
    {

        public static string DataTransferMasterDetail_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DATATRANSFER_MASTER_DETAIL.* ");
            sb.Append(" FROM DATATRANSFER_MASTER_DETAIL");
            sb.Append(" WHERE IS_ACTIVE='Y' ");

            return sb.ToString();
        }

        public static List<dcDATATRANSFER_MASTER_DETAIL> GetDataTransferMasterDetail()
        {
            return GetDataTransferMasterDetail(null);
        }
        public static List<dcDATATRANSFER_MASTER_DETAIL> GetDataTransferMasterDetail(DBContext dc)
        {
            List<dcDATATRANSFER_MASTER_DETAIL> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(DataTransferMasterDetail_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcDATATRANSFER_MASTER_DETAIL>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string DataTransferMasterTableColumnPer_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DATATRANSFER_MSTR_TAB_COL_PER.* ");
            sb.Append(" FROM DATATRANSFER_MSTR_TAB_COL_PER");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static List<dcDATATRANSFER_MSTR_TAB_COL_PER> GetDataTransferMasterTableColumnPer()
        {
            return GetDataTransferMasterTableColumnPer(null);
        }
        public static List<dcDATATRANSFER_MSTR_TAB_COL_PER> GetDataTransferMasterTableColumnPer(DBContext dc)
        {
            List<dcDATATRANSFER_MSTR_TAB_COL_PER> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(DataTransferMasterTableColumnPer_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcDATATRANSFER_MSTR_TAB_COL_PER>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static string DataTransferDetailTableColumnPer_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DATATRANSFER_DTL_TAB_COL_PER.* ");
            sb.Append(" FROM DATATRANSFER_DTL_TAB_COL_PER");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static List<dcDATATRANSFER_DTL_TAB_COL_PER> GetDataTransferDetailTableColumnPer()
        {
            return GetDataTransferDetailTableColumnPer(null);
        }
        public static List<dcDATATRANSFER_DTL_TAB_COL_PER> GetDataTransferDetailTableColumnPer(DBContext dc)
        {
            List<dcDATATRANSFER_DTL_TAB_COL_PER> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(DataTransferDetailTableColumnPer_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcDATATRANSFER_DTL_TAB_COL_PER>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static bool ProcessDataTransfer(string LinkedServer, string fromDate, string toDate, out string savedStatus, DBContext dc)
        {
            savedStatus = string.Empty;

            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            List<dcDATATRANSFER_MASTER_DETAIL> dataTransferTableList = new List<dcDATATRANSFER_MASTER_DETAIL>();

            List<dcDATATRANSFER_MSTR_TAB_COL_PER> MasterTableColumnPercentageList = new List<dcDATATRANSFER_MSTR_TAB_COL_PER>();

            List<dcDATATRANSFER_DTL_TAB_COL_PER> DetailTableColumnPercentageList = new List<dcDATATRANSFER_DTL_TAB_COL_PER>();

            dataTransferTableList = DataTransferProcessBL.GetDataTransferMasterDetail();

            MasterTableColumnPercentageList = DataTransferProcessBL.GetDataTransferMasterTableColumnPer();

            DetailTableColumnPercentageList = DataTransferProcessBL.GetDataTransferDetailTableColumnPer();

            string commaSeparatedColumns = string.Empty;


            foreach (dcDATATRANSFER_MASTER_DETAIL dataTransferTable in dataTransferTableList)
            {
                dcDATATRANSFER_TABLE_LOG transferTableLog = new dcDATATRANSFER_TABLE_LOG();
                string tableName = string.Empty;

                try
                {
                    dc.StartTransaction();

                    commaSeparatedColumns = string.Empty;

                    //Master Table

                    //delete target table by date range

                    DeleteTargetMasterTableByDateRange(LinkedServer, dataTransferTable.MASTER_TABLE, dataTransferTable.MASTER_TABLE_FILTER, fromDate, toDate, dc);

                    // select from source table
                    // insert to source table

                    List<dcDATATRANSFER_MSTR_TAB_COL_PER> masterList = MasterTableColumnPercentageList.Where(w => w.DT_MSTR_DTL_ID == dataTransferTable.ID).ToList();

                    commaSeparatedColumns = GetCommaSeparatedColumnFromTable(LinkedServer, dataTransferTable.MASTER_TABLE, masterList, null, null);

                    if (!commaSeparatedColumns.Equals(string.Empty))
                        InsertToMasterTableByDateRange(LinkedServer, dataTransferTable.MASTER_TABLE, dataTransferTable.MASTER_TABLE_FILTER, commaSeparatedColumns, fromDate, toDate, dc);

                    //Detail Table 

                    if (!dataTransferTable.DETAIL_TABLE.Equals(string.Empty))
                    {

                        List<dcDATATRANSFER_DTL_TAB_COL_PER> detailList = DetailTableColumnPercentageList.Where(w => w.DT_MSTR_DTL_ID == dataTransferTable.ID).ToList();

                        //delete target table by date range

                        DeleteTargetDetailTableByDateRange(LinkedServer, dataTransferTable.MASTER_TABLE, dataTransferTable.MASTER_TABLE_FILTER, dataTransferTable.MASTER_TO_DETAIL, dataTransferTable.DETAIL_TABLE, dataTransferTable.DETAIL_TO_MASTER, fromDate, toDate, dc);

                        // select from source table
                        // insert to source table

                        commaSeparatedColumns = GetCommaSeparatedColumnFromTable(LinkedServer, dataTransferTable.DETAIL_TABLE, null, detailList, null);

                        if (!commaSeparatedColumns.Equals(string.Empty))
                            InsertToDetailTableByDateRange(LinkedServer, dataTransferTable.MASTER_TABLE, dataTransferTable.MASTER_TABLE_FILTER, dataTransferTable.MASTER_TO_DETAIL, dataTransferTable.DETAIL_TABLE, dataTransferTable.DETAIL_TO_MASTER, commaSeparatedColumns, fromDate, toDate, dc);

                    }

                    dc.CommitTransaction();
                    transferTableLog.IS_SUCCESS = "Y";
                }
                catch (Exception ex)
                {
                    dc.RollbackTransaction();
                    transferTableLog.IS_SUCCESS = "N";
                    savedStatus = ex.Message.ToString();
                }

                string TablesName = string.Empty;

                TablesName = dataTransferTable.MASTER_TABLE;

                if (!dataTransferTable.DETAIL_TABLE.Equals(string.Empty))
                    TablesName += ", " + dataTransferTable.DETAIL_TABLE;


                transferTableLog.TABLENAME = TablesName;
                transferTableLog.FROM_DATE = Convert.ToDateTime(fromDate);
                transferTableLog.TO_DATE = Convert.ToDateTime(toDate);
                transferTableLog.TRANSFERDATETIME = DateTime.Now;
                transferTableLog._RecordState = RecordStateEnum.Added;


                DataProcessLog(transferTableLog);
            }


            return true;
        }

        private static string GetCommaSeparatedColumnFromTable(string LinkedServer, string tableName, List<dcDATATRANSFER_MSTR_TAB_COL_PER> masterList, List<dcDATATRANSFER_DTL_TAB_COL_PER> detailList, DBContext dc)
        {
            bool isDCInit = false;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            DbCommand cmdT = dc.CreateDbCommand();
            //cmdT.CommandTimeout = dbq.CommandTimeout;
            cmdT.CommandType = CommandType.Text;
            cmdT.CommandText = "SELECT * FROM " + tableName + "@" + LinkedServer + " WHERE 1=2";

            DbDataReader dataReader = cmdT.ExecuteReader();
            string commaSeparatedColumns = string.Empty;

            string columnName = string.Empty;

            if (dataReader != null)
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    columnName = dataReader.GetName(i);

                    if (masterList != null)
                    {
                        if (masterList.Count(w => w.COLUMN_NAME.Equals(columnName, StringComparison.OrdinalIgnoreCase)) > 0)
                            columnName = "ROUND(" + columnName + "*" + "(SELECT PERCENTAGE FROM PBL_PSP.INV_ITEM_MASTER WHERE ITEM_ID=" + tableName + ".ITEM_ID)" + ")";

                        // columnName = "ROUND(" +  columnName + "*" + Convert.ToString(masterList.Where(w => w.COLUMN_NAME.Equals(columnName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().PERCENTAGE) + ")";
                    }

                    if (detailList != null)
                    {
                        if (detailList.Count(w => w.COLUMN_NAME.Equals(columnName, StringComparison.OrdinalIgnoreCase)) > 0)
                            columnName = "ROUND(" + columnName + "*" + "(SELECT PERCENTAGE FROM PBL_PSP.INV_ITEM_MASTER WHERE ITEM_ID=" + tableName + ".ITEM_ID)" + ")";

                        // columnName = "ROUND(" + columnName + "*" + Convert.ToString(detailList.Where(w => w.COLUMN_NAME.Equals(columnName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().PERCENTAGE) + ")";
                    }

                    if (i.Equals(0))
                        commaSeparatedColumns = columnName;
                    else
                        commaSeparatedColumns += "," + columnName;
                }
            }

            return commaSeparatedColumns;
        }
        private static bool DeleteTargetMasterTableByDateRange(string LinkedServer, string TableName, string ColumnName, string fromDate, string toDate, DBContext dc)
        {
            int rCount = 0;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" DELETE FROM " + TableName + "@" + LinkedServer);
                sb.Append(" WHERE 1=1 ");

                if (!fromDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + ColumnName + ")>='" + fromDate + "'");

                if (!toDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + ColumnName + ")<='" + toDate + "'");


                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;

                rCount = dc.ExecuteNonQuery(cmdInfo);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return rCount > 0;
        }


        private static bool DeleteTargetDetailTableByDateRange(string LinkedServer, string MasterTableName, string MasterColumnName, string MasterToDetailJoinColumn, string DetailTableName, string DetailToMasterJoinColumn, string fromDate, string toDate, DBContext dc)
        {
            int rCount = 0;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" DELETE FROM " + DetailTableName + "@" + LinkedServer);
                sb.Append(" WHERE 1=1 ");
                sb.Append(" AND  " + DetailToMasterJoinColumn + " IN ");
                sb.Append(" ( ");
                sb.Append(" SELECT " + MasterToDetailJoinColumn + " FROM " + MasterTableName);
                sb.Append(" WHERE 1=1 ");

                if (!fromDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + MasterColumnName + ") >='" + fromDate + "'");

                if (!toDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + MasterColumnName + ") <='" + toDate + "'");

                sb.Append(" ) ");


                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;

                rCount = dc.ExecuteNonQuery(cmdInfo);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return rCount > 0;
        }
        private static bool InsertToMasterTableByDateRange(string LinkedServer, string TableName, string ColumnName, string commaSeparatedColumns, string fromDate, string toDate, DBContext dc)
        {
            int rCount = 0;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" INSERT INTO " + TableName + "@" + LinkedServer);
                sb.Append(" SELECT " + commaSeparatedColumns + " FROM PBL_PSP." + TableName);
                sb.Append(" WHERE 1=1 ");

                if (!fromDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + ColumnName + ") >='" + fromDate + "'");

                if (!toDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + ColumnName + ") <='" + toDate + "'");


                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;

                rCount = dc.ExecuteNonQuery(cmdInfo);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return rCount > 0;
        }


        private static bool InsertToDetailTableByDateRange(string LinkedServer, string MasterTableName, string MasterColumnName, string MasterToDetailJoinColumn, string DetailTableName, string DetailToMasterJoinColumn, string commaSeparatedColumns, string fromDate, string toDate, DBContext dc)
        {
            int rCount = 0;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" INSERT INTO " + DetailTableName + "@" + LinkedServer);
                sb.Append(" SELECT " + commaSeparatedColumns + " FROM PBL_PSP." + DetailTableName);
                sb.Append(" WHERE 1=1 ");
                sb.Append(" AND  " + DetailToMasterJoinColumn + " IN ");
                sb.Append(" ( ");
                sb.Append(" SELECT " + MasterToDetailJoinColumn + " FROM " + MasterTableName);
                sb.Append(" WHERE 1=1 ");

                if (!fromDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + MasterColumnName + ") >='" + fromDate + "'");

                if (!toDate.Equals(string.Empty))
                    sb.Append(" AND TRUNC(" + MasterColumnName + ") <='" + toDate + "'");

                sb.Append(" ) ");

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;

                rCount = dc.ExecuteNonQuery(cmdInfo);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return rCount > 0;
        }

        protected static void DataProcessLog(dcDATATRANSFER_TABLE_LOG transferTableLog)
        {
            DATATRANSFER_TABLE_LOGBL.Save(transferTableLog);
        }


    }
}
