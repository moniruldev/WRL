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


namespace PG.Core.DBBase
{
	public enum DBQueryModeEnum
	{
		Table = 0,
		SQLStatement = 1,
		DBCommand = 2,
		DBCommandInfo = 3,
	}

	public class DBQuery
	{

		#region private members

		private DBContextSettings m_DBContextSettings = null;

		private DBQueryModeEnum m_DBQueryMode = DBQueryModeEnum.DBCommandInfo;
		private List<DBFilter> m_DBFilterList = new List<DBFilter>();
		private string m_Fields = string.Empty;
		private string m_OrderBy = string.Empty;
		private bool m_LoadAssociation = false;
		private DataLoadOptions m_DataLoadOption = null;
		private string m_SQLStatement = string.Empty;
		private DbCommand m_DBCommand = null;
        private int m_CommandTimeout = 30;
		private List<DbParameter> m_DBParameters = new List<DbParameter>();
		
		private bool m_UseDBColumnMap = false;
		private bool m_ConvertNullToDefault = false;

		private DBCommandInfo m_DBCommandInfo = null;
		private DBParameterInfoCollection m_DBParametersInfo = new DBParameterInfoCollection();
		private DBFilterSettings m_DBQFilterSettings = DBFilterManager.DefaultFilterSettings;

		private bool m_IsPaging = false;
		private int m_PageNo = 1;
		private int m_RowCount = DBFilterManager.DefaultFilterSettings.PagingRowCount;

		private int m_TotalRecord = 0;

		#endregion

		#region public properties


		public DBContextSettings DBContextSettings
		{
			get { return m_DBContextSettings; }
			set { m_DBContextSettings = value; }
		}

		public DBQueryModeEnum DBQueryMode
		{
			get { return m_DBQueryMode; }
			set { m_DBQueryMode = value; }
		}
		public List<DBFilters.DBFilter> DBFilterList
		{
			get { return m_DBFilterList; }
			set { m_DBFilterList = value; }
		}

		public DBFilterSettings DBQFilterSettings
		{
			get { return m_DBQFilterSettings; }
			set { m_DBQFilterSettings = value; }
		}

		public string Fields
		{
			get { return m_Fields; }
			set { m_Fields = value; }
		}

		public string OrderBy
		{
			get { return m_OrderBy; }
			set { m_OrderBy = value; }
		}

		public bool IsPaging
		{
			get { return m_IsPaging; }
			set { m_IsPaging = value; }
		}

		public int PageNo
		{
			get { return m_PageNo; }
			set { m_PageNo = value; }
		}

		public int RowCount
		{
			get { return m_RowCount; }
			set { m_RowCount = value; }
		}

		public int TotalRecord
		{
			get { return m_TotalRecord; }
		}

		public int TotalPage
		{
			get {
				int totPage = 0;
				if (m_IsPaging)
				{
					if (m_RowCount > 0 && m_TotalRecord > 0)
					{
						totPage =Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(m_TotalRecord) / m_RowCount));
					}
				}
				return totPage;
			}
		}



		public bool LoadAssociation
		{
			get { return m_LoadAssociation; }
			set { m_LoadAssociation = value; }
		}
		public DataLoadOptions DataLoadOption
		{
			get { return m_DataLoadOption; }
			set { m_DataLoadOption = value; }
		}

		public string SQLStatement
		{
			get { return m_SQLStatement; }
			set { m_SQLStatement = value; }
		}

		public DbCommand DBCommand
		{
			get { return m_DBCommand; }
			set { m_DBCommand = value; }
		}

        public int CommandTimeout
        {
            get { return m_CommandTimeout; }
            set { m_CommandTimeout = value; }
        }



		public List<DbParameter> DBPatameters
		{
			get { return m_DBParameters; }
			set { m_DBParameters = value; }
		}

		public DBCommandInfo DBCommandInfo
		{
			get { return m_DBCommandInfo; }
			set { m_DBCommandInfo = value; }
		}

		public DBParameterInfoCollection DBParametersInfo
		{
			get { return m_DBParametersInfo; }
			set { m_DBParametersInfo = value; }
		}

		public bool ConvertNullToDefault
		{
			get { return m_ConvertNullToDefault; }
			set { m_ConvertNullToDefault = value; }
		}


		public bool UseDBColumnMap
		{
			get { return m_UseDBColumnMap; }
			set { m_UseDBColumnMap = value; }
		}


		public static DBQuery PresetForStatement
		{
			get
			{
				DBQuery dbq = new DBQuery();
				dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
				dbq.UseDBColumnMap = false;
				dbq.ConvertNullToDefault = true;
				return dbq;
			}
		}

		public static DBQuery PresetForCommand
		{
			get
			{
				DBQuery dbq = new DBQuery();
				dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
				dbq.UseDBColumnMap = false;
				dbq.ConvertNullToDefault = true;
				return dbq;
			}
		}


		#endregion

		public DBQuery()
		{
			this.m_DBContextSettings = DBContextManager.GetDBContextSettings();
			this.m_ConvertNullToDefault =  this.m_DBContextSettings.ConvertNullToDefault;
			this.UseDBColumnMap = this.m_DBContextSettings.UseDBColumnMap;
			this.m_DBQueryMode = this.m_DBContextSettings.DBQueryMode;

			this.m_LoadAssociation = this.m_DBContextSettings.LoadAssociation;
			this.m_DataLoadOption = this.m_DBContextSettings.DataLoadOption;
		}
		
		public DBQuery(DBContextSettings pDBCSettings)
		{
			this.m_DBContextSettings = pDBCSettings;
			this.m_ConvertNullToDefault = pDBCSettings.ConvertNullToDefault;
			this.UseDBColumnMap = pDBCSettings.UseDBColumnMap;
			this.m_DBQueryMode = pDBCSettings.DBQueryMode;

			this.m_LoadAssociation = pDBCSettings.LoadAssociation;
			this.m_DataLoadOption = pDBCSettings.DataLoadOption;
		}

		private int GetRecordCount(DbCommand dbcmd, DBContext dc)
		{
			int totRec = 0;
  
			DbCommand cmd = dc.CreateDbCommand();
			cmd.CommandType = CommandType.Text;

			StringBuilder sb = new StringBuilder(dbcmd.CommandText);
			int selectPos = dbcmd.CommandText.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase);
			int insertPos = 0;
			if (selectPos != -1)
			{
				insertPos = selectPos + 7;
			}
			int fromPos = dbcmd.CommandText.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
			//int fromPos = dbcmd.CommandText.IndexOf(" FROM ", StringComparison.OrdinalIgnoreCase);


			if (selectPos != -1 && fromPos != -1)
			{
				sb.Remove(insertPos, fromPos - insertPos - 1);
				sb.Insert(insertPos, " Count(*) As TotalRecord ");
				cmd.CommandText = sb.ToString();
				foreach (DbParameter param in dbcmd.Parameters)
				{
					AddDbCommandParamWithValue(cmd, param.ParameterName, param.Value, this.DBContextSettings.DatabaseType);
				}
				totRec = Convert.ToInt32(cmd.ExecuteScalar());
			}
			else
			{
				cmd.CommandText = sb.ToString();
				foreach (DbParameter param in dbcmd.Parameters)
				{
					AddDbCommandParamWithValue(cmd, param.ParameterName, param.Value, this.DBContextSettings.DatabaseType);
				}
				DataTable dtData1 = new DataTable();
				dtData1.Load(cmd.ExecuteReader());
				totRec = dtData1.Rows.Count;
			}

			return totRec;
		}
		public static List<T> ExecuteDBQuery<T>() where T : class
		{
			return ExecuteDBQuery<T>(null, null, null);
		}
		public static List<T> ExecuteDBQuery<T>(DBQuery dbq) where T : class
		{
			return ExecuteDBQuery<T>(dbq, null, null);
		}
		public static List<T> ExecuteDBQuery<T>(DBContext dc) where T : class
		{
			return ExecuteDBQuery<T>(null, null, dc);
		}
		public static List<T> ExecuteDBQuery<T>(DBQuery dbq, DBContext dc) where T : class
		{
			return ExecuteDBQuery<T>(dbq, null, dc);
		}
		public static List<T> ExecuteDBQuery<T>(DBQuery dbq, IQueryable<T> query) where T : class
		{
			return ExecuteDBQuery<T>(dbq, query, null);
		}
		public static List<T> ExecuteDBQuery<T>(DBQuery dbq, IQueryable<T> query, DBContext dc) where T : class
		{
			if (dbq.IsPaging)
			{
				if (dbq.OrderBy == string.Empty)
				{
					throw new ArgumentException("Please Specifiy at least one column at DBQUERY.OrderBy for paging.");
					//dbq.OrderBy = dbq.m_DBFilterList[0].FieldName;
				}
			}

			bool isDCInit = false;
			List<T> cList = new List<T>();
			//if (dbq == null)
			//{

			//    dbq = new DBQuery(DBContextManager.GetDBContextSettings());
			//}
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
				//DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);
				if (dbq == null)
				{
					dbq = new DBQuery(dc.DBContextSettings);
				}
				if (isDCInit)
				{
					dbq.DBContextSettings = dc.DBContextSettings;
				}

				switch (dbq.m_DBQueryMode)
				{
					case DBQueryModeEnum.Table:
						if (dbq.SQLStatement == string.Empty)
						{
							throw new ArgumentException("No Table Name Defined.");
						}
						DbCommand cmdT = dc.CreateDbCommand();
                        cmdT.CommandTimeout = dbq.CommandTimeout;
						cmdT.CommandType = CommandType.TableDirect;
						cmdT.CommandText = dbq.SQLStatement;
						DataTable dtDataT = new DataTable();
						dtDataT.Load(cmdT.ExecuteReader());
						if (dtDataT.Rows.Count > 0)
						{
							cList = DBMap.ConvertDataTableToObjectList<T>(dtDataT, dbq.UseDBColumnMap, dbq.ConvertNullToDefault);
						}
						break;
					case DBQueryModeEnum.SQLStatement:
						if (dbq.SQLStatement == string.Empty)
						{
							throw new ArgumentException("No SQL Statement Defined.");
						}
						DbCommand cmd = dc.CreateDbCommand();
                        cmd.CommandTimeout = dbq.CommandTimeout;
						cmd.CommandType = CommandType.Text;
						cmd.CommandText = dbq.SQLStatement;

						AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.m_DBContextSettings);

						if (dbq.DBFilterList != null && dbq.DBFilterList.Count > 0)
						{
							dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
							dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
							DBFilterManager.ApplyFilterListToCommand(dbq.DBFilterList, cmd, dbq.DBQFilterSettings);
						}

						if (dbq.IsPaging)
						{

							dbq.PageNo = dbq.PageNo > 0 ? dbq.PageNo : 1;
							dbq.RowCount = dbq.RowCount > 0 ? dbq.RowCount : dbq.DBQFilterSettings.PagingRowCount;
							dbq.m_TotalRecord = dbq.GetRecordCount(cmd, dc);
						}


						if (dbq.IsPaging == false &&  dbq.OrderBy != string.Empty)
						{

							cmd.CommandText += " ORDER BY " + dbq.OrderBy;
						}

						if (dbq.IsPaging)
						{
							dbq.ApplyPagingToCommand(cmd);
						}

						DataTable dtData = new DataTable();
						dtData.Load(cmd.ExecuteReader());
						if (dtData.Rows.Count > 0)
						{
							cList = DBMap.ConvertDataTableToObjectList<T>(dtData, dbq.UseDBColumnMap, dbq.ConvertNullToDefault);
						}
						break;
					case DBQueryModeEnum.DBCommand:
						if (dbq.DBCommand == null)
						{
							throw new ArgumentException("No Command Defined.");
						}

						if (dbq.DBCommand.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}


						if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
						{
							foreach (DbParameter param in dbq.m_DBParameters)
							{
								dbq.m_DBCommand.Parameters.Add(param);
							}
						}
						if (dbq.m_DBFilterList != null && dbq.m_DBFilterList.Count > 0)
						{

							dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
							dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
							DBFilterManager.ApplyFilterListToCommand(dbq.m_DBFilterList, dbq.m_DBCommand, dbq.m_DBQFilterSettings);
						}

						if (dbq.IsPaging)
						{
							dbq.PageNo = dbq.PageNo > 0 ? dbq.PageNo : 1;
							dbq.RowCount = dbq.RowCount > 0 ? dbq.RowCount : dbq.DBQFilterSettings.PagingRowCount;
							dbq.m_TotalRecord = dbq.GetRecordCount(dbq.DBCommand, dc);
						}

						if (dbq.IsPaging == false && dbq.OrderBy != string.Empty)
						{
							dbq.DBCommand.CommandText += " ORDER BY " + dbq.OrderBy;
						}


						if (dbq.IsPaging)
						{
							dbq.ApplyPagingToCommand(dbq.DBCommand);
						}


						dbq.DBCommand.Connection = dc.Connection;
						dc.SetDBCommandTransaction(dbq.DBCommand);


						DataTable dtData1 = new DataTable();
						dtData1.Load(dbq.DBCommand.ExecuteReader());
						if (dtData1.Rows.Count > 0)
						{
							cList = DBMap.ConvertDataTableToObjectList<T>(dtData1, dbq.UseDBColumnMap, dbq.ConvertNullToDefault);
						}
						break;

					case DBQueryModeEnum.DBCommandInfo:
						if (dbq.DBCommandInfo == null)
						{
							throw new ArgumentException("No Command Info Defined.");
						}

						if (dbq.DBCommandInfo.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}


						if (dbq.IsPaging)
						{
							dbq.PageNo = dbq.PageNo > 0 ? dbq.PageNo : 1;
							dbq.RowCount = dbq.RowCount > 0 ? dbq.RowCount : dbq.DBQFilterSettings.PagingRowCount;
							dbq.m_TotalRecord = dbq.GetRecordCount(dbq.DBCommand, dc);
						}

						if (dbq.IsPaging == false && dbq.OrderBy != string.Empty)
						{
							dbq.DBCommand.CommandText += " ORDER BY " + dbq.OrderBy;
						}


						if (dbq.IsPaging)
						{
							dbq.ApplyPagingToCommand(dbq.DBCommand);
						}


						DbCommand cmd1 = dc.CreateDbCommand();
						cmd1.CommandType = dbq.DBCommandInfo.CommandType;
						cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;


                        if (dbq.m_DBFilterList != null && dbq.m_DBFilterList.Count > 0)
                        {

                            dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
                            dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
                            DBFilterManager.ApplyFilterListToCommand(dbq.m_DBFilterList, cmd1, dbq.m_DBQFilterSettings);
                        }


						AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);
						
					  //dc.SetDBCommandTransaction(cmd1);
						cmd1.Connection = dc.Connection;
						dc.SetDBCommandTransaction(cmd1);


						DataTable dtData11 = new DataTable();
						dtData11.Load(cmd1.ExecuteReader());


						//TODO: Change close Data base connection
						if (dtData11.Rows.Count > 0)
						{
							cList = DBMap.ConvertDataTableToObjectList<T>(dtData11, dbq.UseDBColumnMap, dbq.ConvertNullToDefault);
						}
						break;
				}
			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			//MyProp<DBClass.DBClassBase> c = new MyProp<ZCore.DBClass.DBClassBase>();
			return cList;
		}


		public static DataTable ExecuteDBQuery(DBQuery dbq)
		{
			return ExecuteDBQuery(dbq, null);
		}
		public static DataTable ExecuteDBQuery(DBQuery dbq, DBContext dc)
		{
			DataTable rObj = new DataTable();
			bool isDCInit = false;
			if (dbq == null)
			{
				throw new ArgumentException("DBQuery is null!");
			}
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
				//DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);

				switch (dbq.m_DBQueryMode)
				{
					case DBQueryModeEnum.Table:
						throw new ArgumentException("DBQ Excute Scalar not supported for Table mode!");
					//break;
					case DBQueryModeEnum.SQLStatement:
						if (dbq.SQLStatement == string.Empty)
						{
							throw new ArgumentException("No SQL Statement Defined.");
						}
						DbCommand cmd = dc.CreateDbCommand();
						cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = dbq.CommandTimeout;
						cmd.CommandText = dbq.SQLStatement;

						AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.DBContextSettings);

						if (dbq.DBFilterList != null && dbq.DBFilterList.Count > 0)
						{
							dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
							dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
							DBFilterManager.ApplyFilterListToCommand(dbq.DBFilterList, cmd, dbq.DBQFilterSettings);
						}
						rObj.Load(dbq.DBCommand.ExecuteReader());

						break;

					case DBQueryModeEnum.DBCommand:
						if (dbq.DBCommand == null)
						{
							throw new ArgumentException("No Command Defined.");
						}

						if (dbq.DBCommand.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}

						if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
						{
							foreach (DbParameter param in dbq.m_DBParameters)
							{
								dbq.m_DBCommand.Parameters.Add(param);
							}
						}

						if (dbq.m_DBFilterList != null && dbq.m_DBFilterList.Count > 0)
						{

							dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
							dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
							DBFilterManager.ApplyFilterListToCommand(dbq.m_DBFilterList, dbq.m_DBCommand, dbq.m_DBQFilterSettings);
						}

                        dbq.DBCommand.Connection = dc.Connection;
						dc.SetDBCommandTransaction(dbq.DBCommand);

						rObj.Load(dbq.DBCommand.ExecuteReader());


						break;
					case DBQueryModeEnum.DBCommandInfo:
						if (dbq.DBCommandInfo == null)
						{
							throw new ArgumentException("No Command Info Defined.");
						}

						if (dbq.DBCommandInfo.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}
						DbCommand cmd1 = dc.CreateDbCommand();
						cmd1.CommandType = dbq.DBCommandInfo.CommandType;
						cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;

                        
						AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);

						//dc.SetDBCommandTransaction(cmd1);
 
                        cmd1.Connection = dc.Connection;
						dc.SetDBCommandTransaction(cmd1);
						rObj.Load(cmd1.ExecuteReader());

						break;
				}




			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

			return rObj;
		}

        public static DataTable GetDataTableByQuery(string query)
        {
            return GetDataTableByQuery(query, null);
        }
        public static DataTable GetDataTableByQuery(string query, DBContext dc)
        {
            DBQuery dbq = new DBQuery();
            DBCommandInfo cmdInfo = new DBCommandInfo();

            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = query;
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            DataTable rObj = new DataTable();
            bool isDCInit = false;

            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);

                switch (dbq.m_DBQueryMode)
                {
                    case DBQueryModeEnum.Table:
                        throw new ArgumentException("DBQ Excute Scalar not supported for Table mode!");
                    //break;
                    case DBQueryModeEnum.SQLStatement:



                        if (dbq.SQLStatement == string.Empty)
                        {
                            throw new ArgumentException("No SQL Statement Defined.");
                        }
                        DbCommand cmd = dc.CreateDbCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = dbq.CommandTimeout;
                        cmd.CommandText = dbq.SQLStatement;

                        AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.DBContextSettings);

                        if (dbq.DBFilterList != null && dbq.DBFilterList.Count > 0)
                        {
                            dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
                            dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
                            DBFilterManager.ApplyFilterListToCommand(dbq.DBFilterList, cmd, dbq.DBQFilterSettings);
                        }
                        rObj.Load(dbq.DBCommand.ExecuteReader());

                        break;

                    case DBQueryModeEnum.DBCommand:
                        if (dbq.DBCommand == null)
                        {
                            throw new ArgumentException("No Command Defined.");
                        }

                        if (dbq.DBCommand.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }

                        if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
                        {
                            foreach (DbParameter param in dbq.m_DBParameters)
                            {
                                dbq.m_DBCommand.Parameters.Add(param);
                            }
                        }

                        if (dbq.m_DBFilterList != null && dbq.m_DBFilterList.Count > 0)
                        {

                            dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
                            dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
                            DBFilterManager.ApplyFilterListToCommand(dbq.m_DBFilterList, dbq.m_DBCommand, dbq.m_DBQFilterSettings);
                        }

                        dbq.DBCommand.Connection = dc.Connection;
                        dc.SetDBCommandTransaction(dbq.DBCommand);

                        rObj.Load(dbq.DBCommand.ExecuteReader());


                        break;
                    case DBQueryModeEnum.DBCommandInfo:
                        if (dbq.DBCommandInfo == null)
                        {
                            throw new ArgumentException("No Command Info Defined.");
                        }

                        if (dbq.DBCommandInfo.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }
                        DbCommand cmd1 = dc.CreateDbCommand();
                        cmd1.CommandType = dbq.DBCommandInfo.CommandType;
                        cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;


                        AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);

                        //dc.SetDBCommandTransaction(cmd1);

                        cmd1.Connection = dc.Connection;
                        dc.SetDBCommandTransaction(cmd1);
                        rObj.Load(cmd1.ExecuteReader());

                        break;
                }




            }
            catch (Exception ex) { throw ex; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return rObj;
        }
        public static DataTable ExecuteDBQuerySP(DBQuery dbq)
        {
            return ExecuteDBQuerySP(dbq, null);
        }
        public static DataTable ExecuteDBQuerySP(DBQuery dbq, DBContext dc)
        {
            DataTable rObj = new DataTable();
            bool isDCInit = false;
            if (dbq == null)
            {
                throw new ArgumentException("DBQuery is null!");
            }
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);

                switch (dbq.m_DBQueryMode)
                {
                    case DBQueryModeEnum.Table:
                        throw new ArgumentException("DBQ Excute Scalar not supported for Table mode!");
                    //break;
                    case DBQueryModeEnum.SQLStatement:



                        if (dbq.SQLStatement == string.Empty)
                        {
                            throw new ArgumentException("No SQL Statement Defined.");
                        }
                        DbCommand cmd = dc.CreateDbCommand();
                        cmd.CommandTimeout = dbq.CommandTimeout;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = dbq.SQLStatement;

                        AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.DBContextSettings);

                        if (dbq.DBFilterList != null && dbq.DBFilterList.Count > 0)
                        {
                            dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
                            dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
                            DBFilterManager.ApplyFilterListToCommand(dbq.DBFilterList, cmd, dbq.DBQFilterSettings);
                        }
                        rObj.Load(dbq.DBCommand.ExecuteReader());

                        break;

                    case DBQueryModeEnum.DBCommand:
                        if (dbq.DBCommand == null)
                        {
                            throw new ArgumentException("No Command Defined.");
                        }

                        if (dbq.DBCommand.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }

                        if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
                        {
                            foreach (DbParameter param in dbq.m_DBParameters)
                            {
                                dbq.m_DBCommand.Parameters.Add(param);
                            }
                        }

                        if (dbq.m_DBFilterList != null && dbq.m_DBFilterList.Count > 0)
                        {

                            dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
                            dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
                            DBFilterManager.ApplyFilterListToCommand(dbq.m_DBFilterList, dbq.m_DBCommand, dbq.m_DBQFilterSettings);
                        }

                        dbq.DBCommand.Connection = dc.Connection;
                        dc.SetDBCommandTransaction(dbq.DBCommand);

                        rObj.Load(dbq.DBCommand.ExecuteReader());


                        break;
                    case DBQueryModeEnum.DBCommandInfo:
                        if (dbq.DBCommandInfo == null)
                        {
                            throw new ArgumentException("No Command Info Defined.");
                        }

                        if (dbq.DBCommandInfo.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }
                        DbCommand cmd1 = dc.CreateDbCommand();
                        cmd1.CommandType = dbq.DBCommandInfo.CommandType;
                        cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;

                        AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);

                        //dc.SetDBCommandTransaction(cmd1);
                        cmd1.Connection = dc.Connection;
                        dc.SetDBCommandTransaction(cmd1);
                        rObj.Load(cmd1.ExecuteReader());

                        break;
                }




            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return rObj;
        }



		public static object ExecuteDBScalar(DBQuery dbq)
		{
			return ExecuteDBScalar(dbq, null);
		}
		public static object ExecuteDBScalar(DBQuery dbq, DBContext dc)
		{
			object rObj = null;
			bool isDCInit = false;
			if (dbq == null)
			{
				throw new ArgumentException("DBQuery is null!");
			}
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
				//DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);

				switch (dbq.m_DBQueryMode)
				{
					case DBQueryModeEnum.Table:
						throw new ArgumentException("DBQ Excute Scalar not supported for Table mode!");
						//break;
					case DBQueryModeEnum.SQLStatement:
						
						

						if (dbq.SQLStatement == string.Empty)
						{
							throw new ArgumentException("No SQL Statement Defined.");
						}
						DbCommand cmd = dc.CreateDbCommand();
						cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = dbq.CommandTimeout;
						cmd.CommandText = dbq.SQLStatement;

						AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.DBContextSettings);

						if (dbq.DBFilterList != null && dbq.DBFilterList.Count > 0)
						{
							dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
							dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
							DBFilterManager.ApplyFilterListToCommand(dbq.DBFilterList, cmd, dbq.DBQFilterSettings);
						}
						rObj = dbq.DBCommand.ExecuteScalar();
						break;

					case DBQueryModeEnum.DBCommand:
						if (dbq.DBCommand == null)
						{
							throw new ArgumentException("No Command Defined.");
						}

						if (dbq.DBCommand.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}

						if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
						{
							foreach (DbParameter param in dbq.m_DBParameters)
							{
								dbq.m_DBCommand.Parameters.Add(param);
							}
						}

						if (dbq.m_DBFilterList != null && dbq.m_DBFilterList.Count > 0)
						{

							dbq.DBQFilterSettings.DBContextSettings = dbq.DBContextSettings;
							dbq.DBQFilterSettings.FilterStyle = DBFilterStyleEnum.DirectString;
							DBFilterManager.ApplyFilterListToCommand(dbq.m_DBFilterList, dbq.m_DBCommand, dbq.m_DBQFilterSettings);
						}

                        dbq.DBCommand.Connection = dc.Connection;
						dc.SetDBCommandTransaction(dbq.DBCommand);

						rObj = dbq.DBCommand.ExecuteScalar();


						break;
					case DBQueryModeEnum.DBCommandInfo:
						if (dbq.DBCommandInfo == null)
						{
							throw new ArgumentException("No Command Info Defined.");
						}

						if (dbq.DBCommandInfo.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}
						DbCommand cmd1 = dc.CreateDbCommand();
						cmd1.CommandType = dbq.DBCommandInfo.CommandType;
						cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;


						AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);

						//dc.SetDBCommandTransaction(cmd1);
  
                        cmd1.Connection = dc.Connection;
						dc.SetDBCommandTransaction(cmd1);
						rObj = cmd1.ExecuteScalar();

						break;
				}
				
			   


			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

			return rObj;
		}


		public static int ExecuteDBNonQuery(DBQuery dbq)
		{
			return ExecuteDBNonQuery(dbq, null);
		}
		public static int ExecuteDBNonQuery(DBQuery dbq, DBContext dc)
		{
			int ret = 0;
			bool isDCInit = false;
			if (dbq == null)
			{
				throw new ArgumentException("DBQuery is null!");
			}
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
				//DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);

				switch (dbq.m_DBQueryMode)
				{
					case DBQueryModeEnum.Table:
						throw new ArgumentException("DBQ Excute Non Query not supported for Table mode!");
					//break;
					case DBQueryModeEnum.SQLStatement:



						if (dbq.SQLStatement == string.Empty)
						{
							throw new ArgumentException("No SQL Statement Defined.");
						}
						DbCommand cmd = dc.CreateDbCommand();
                        cmd.CommandTimeout = dbq.CommandTimeout;
						cmd.CommandType = CommandType.Text;
						cmd.CommandText = dbq.SQLStatement;

						AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.DBContextSettings);

                        dbq.DBCommand = cmd;
						ret = dbq.DBCommand.ExecuteNonQuery();
						break;

					case DBQueryModeEnum.DBCommand:
						if (dbq.DBCommand == null)
						{
							throw new ArgumentException("No Command Defined.");
						}

						if (dbq.DBCommand.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}

						if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
						{
							foreach (DbParameter param in dbq.m_DBParameters)
							{
								dbq.m_DBCommand.Parameters.Add(param);
							}
						}

                        dbq.DBCommand.Connection = dc.Connection;
						dc.SetDBCommandTransaction(dbq.DBCommand);

						ret = dbq.DBCommand.ExecuteNonQuery();

						break;
					case DBQueryModeEnum.DBCommandInfo:
						if (dbq.DBCommandInfo == null)
						{
							throw new ArgumentException("No Command Info Defined.");
						}

						if (dbq.DBCommandInfo.CommandText == string.Empty)
						{
							throw new ArgumentException("No Command Statement Defined.");
						}
						DbCommand cmd1 = dc.CreateDbCommand();
						cmd1.CommandType = dbq.DBCommandInfo.CommandType;
						cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;

						AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);

						//dc.SetDBCommandTransaction(cmd1);
                        cmd1.Connection = dc.Connection;
						dc.SetDBCommandTransaction(cmd1);
						ret = cmd1.ExecuteNonQuery();

						break;
				}




			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

			return ret;
		}


        public static int ExecuteDBNonQuerySP(DBQuery dbq)
        {
            return ExecuteDBNonQuerySP(dbq, null);
        }
        public static int ExecuteDBNonQuerySP(DBQuery dbq, DBContext dc)
        {
            int ret = 0;
            bool isDCInit = false;
            if (dbq == null)
            {
                throw new ArgumentException("DBQuery is null!");
            }
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);

                switch (dbq.m_DBQueryMode)
                {
                    case DBQueryModeEnum.Table:
                        throw new ArgumentException("DBQ Excute Non Query not supported for Table mode!");
                    //break;
                    case DBQueryModeEnum.SQLStatement:



                        if (dbq.SQLStatement == string.Empty)
                        {
                            throw new ArgumentException("No SQL Statement Defined.");
                        }
                        DbCommand cmd = dc.CreateDbCommand();
                        cmd.CommandTimeout = dbq.CommandTimeout;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = dbq.SQLStatement;

                        AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.DBContextSettings);

                        ret = dbq.DBCommand.ExecuteNonQuery();
                        break;

                    case DBQueryModeEnum.DBCommand:
                        if (dbq.DBCommand == null)
                        {
                            throw new ArgumentException("No Command Defined.");
                        }

                        if (dbq.DBCommand.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }

                        if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
                        {
                            foreach (DbParameter param in dbq.m_DBParameters)
                            {
                                dbq.m_DBCommand.Parameters.Add(param);
                            }
                        }
                        dc.SetOracleCaseInsensitive();

                        dbq.DBCommand.Connection = dc.Connection;
                        dc.SetDBCommandTransaction(dbq.DBCommand);

                        ret = dbq.DBCommand.ExecuteNonQuery();

                        dc.CheckSetOralceCaseSensitve();
                        break;
                    case DBQueryModeEnum.DBCommandInfo:
                        if (dbq.DBCommandInfo == null)
                        {
                            throw new ArgumentException("No Command Info Defined.");
                        }

                        if (dbq.DBCommandInfo.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }

                        dc.SetOracleCaseSensitive();

                        DbCommand cmd1 = dc.CreateDbCommand();
                        cmd1.CommandType = dbq.DBCommandInfo.CommandType;
                        cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;

                        AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);
                        cmd1.Connection = dc.Connection;
                        dc.SetDBCommandTransaction(cmd1);

                        ret = cmd1.ExecuteNonQuery();

                        dbq.DBCommandInfo.DBParametersInfo = GetDBParametersDbCommand(cmd1);

                        dc.CheckSetOralceCaseSensitve();
                        break;
                }




            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return ret;
        }

        public static int ExecuteDBNonQuerySPOracle(DBQuery dbq, DBContext dc)
        {
            int ret = 0;
            bool isDCInit = false;
            if (dbq == null)
            {
                throw new ArgumentException("DBQuery is null!");
            }
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //DBQuery.SetContextSettingFromDBContext(dbq, dc.DBContextSettings);




                switch (dbq.m_DBQueryMode)
                {
                    case DBQueryModeEnum.Table:
                        throw new ArgumentException("DBQ Excute Non Query not supported for Table mode!");
                    //break;
                    case DBQueryModeEnum.SQLStatement:



                        if (dbq.SQLStatement == string.Empty)
                        {
                            throw new ArgumentException("No SQL Statement Defined.");
                        }
                        DbCommand cmd = dc.CreateDbCommand();
                        cmd.CommandTimeout = dbq.CommandTimeout;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = dbq.SQLStatement;

                        AddDBParametersToDbCommand(cmd, dbq.DBParametersInfo, dbq.DBContextSettings);

                        ret = dbq.DBCommand.ExecuteNonQuery();
                        break;

                    case DBQueryModeEnum.DBCommand:
                        if (dbq.DBCommand == null)
                        {
                            throw new ArgumentException("No Command Defined.");
                        }

                        if (dbq.DBCommand.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }

                        if (dbq.m_DBParameters != null && dbq.m_DBParameters.Count > 0)
                        {
                            foreach (DbParameter param in dbq.m_DBParameters)
                            {
                                dbq.m_DBCommand.Parameters.Add(param);
                            }
                        }



                        dbq.DBCommand.Connection = dc.Connection;
                        dc.SetDBCommandTransaction(dbq.DBCommand);

                        ret = dbq.DBCommand.ExecuteNonQuery();

                        break;
                    case DBQueryModeEnum.DBCommandInfo:
                        if (dbq.DBCommandInfo == null)
                        {
                            throw new ArgumentException("No Command Info Defined.");
                        }

                        if (dbq.DBCommandInfo.CommandText == string.Empty)
                        {
                            throw new ArgumentException("No Command Statement Defined.");
                        }

                        OracleContext oCon = new OracleContext();
                        oCon.InitConnection(dc.DBContextSettings.ConnectionString);


                        DbCommand cmd1 = dc.CreateDbCommand();
                        cmd1.CommandType = dbq.DBCommandInfo.CommandType;
                        cmd1.CommandText = dbq.DBCommandInfo.CommandText;
                        cmd1.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;

                        AddDBParametersToDbCommand(cmd1, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);


                        ret = oCon.ExecuteNonQuery((OracleCommand)cmd1);

                        //dc.SetDBCommandTransaction(cmd1);
                       // cmd1.Connection = dc.Connection;
                        //dc.SetDBCommandTransaction(cmd1);
                        //ret = cmd1.ExecuteNonQuery();


                        //OracleConnection oraCon = new OracleConnection();
                        //oraCon.ConnectionString = dc.DBContextSettings.ConnectionString;
                        //oraCon.Open();

                        //OracleContext oCon = new OracleContext();
                       // oCon.InitConnection(dc.DBContextSettings.ConnectionString);



                        //OracleCommand cmd2 = new OracleCommand();
                        //cmd2.CommandType = dbq.DBCommandInfo.CommandType;
                        //cmd2.CommandText = dbq.DBCommandInfo.CommandText;
                        //cmd2.CommandTimeout = dbq.DBCommandInfo.CommandTimeout;

                        //AddDBParametersToOracleCommand(cmd2, dbq.m_DBCommandInfo.DBParametersInfo, dbq.DBContextSettings);

                        //dc.SetDBCommandTransaction(cmd1);
                        //cmd2.Connection = dc.ORACLEConnection;
                        //cmd2.Connection = (OracleConnection)dc.Connection;
                        //cmd2.Connection = oraCon;
                        //cmd2.Connection = oCon.Connection;
                        //dc.SetOracleCommandTransaction(cmd2);
                        //ret = cmd2.ExecuteNonQuery();

                        //ret = oCon.ExecuteNonQuery(cmd2);

                        //ret = oCon.ExecuteNonQuery((OracleCommand)cmd1);

                        //ret = dc.ExecuteNonQuery(cmd1);
                        //oraCon.Close();

                        oCon.CloseConnection();

                        dbq.DBCommandInfo.DBParametersInfo = GetDBParametersDbCommand(cmd1);


                        //dbq.DBCommandInfo.DBParametersInfo = GetDBParametersOracleCommand(cmd2);


                        break;
                }




            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return ret;
        }


		private void AddDBParameterWithValue(string paramName, object paramValue, DBContext dc)
		{
			//dc.AddParamWithValue();



		}

		public static void AddDbCommandParamWithValue(DbCommand cmd, string paramName, object paramValue)
		{
			AddDbCommandParamWithValue(cmd, paramName, paramValue, DatabaseTypeEnum.SQLServer);
		}
		public static void AddDbCommandParamWithValue(DbCommand cmd, string paramName, object paramValue, DatabaseTypeEnum pDataBaseType)
		{


			paramName = DBContext.RectifyDBParamName(paramName, pDataBaseType);

			switch (pDataBaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					if (paramValue == null)
					{
						((SqlCommand)cmd).Parameters.AddWithValue(paramName, DBNull.Value);
					}
					else
					{
						((SqlCommand)cmd).Parameters.AddWithValue(paramName, paramValue);
					}
					break;
				case DatabaseTypeEnum.MSAccess:
					if (paramValue == null)
					{
						((OleDbCommand)cmd).Parameters.AddWithValue(paramName, DBNull.Value);
					}
					else
					{
						//this is for bug in olebd datetime, truncate the miliseconds field(ff);
						if (paramValue.GetType() == typeof(DateTime))
						{
							paramValue = Convert.ToDateTime(Convert.ToDateTime(paramValue).ToString("dd-MMM-yyyy hh:mm:ss tt"));
						}
						((OleDbCommand)cmd).Parameters.AddWithValue(paramName, paramValue);
					}
					break;
				case DatabaseTypeEnum.Oracle:
					if (paramValue == null)
					{
						((OracleCommand)cmd).Parameters.Add(paramName, DBNull.Value);
					}
					else
					{
						((OracleCommand)cmd).Parameters.Add(paramName, paramValue);
					}
					break;
			}
		}

		public static void AddDBParamCollectionParamValue(DbParameterCollection colParams, string paramName, object paramValue)
		{
			AddDBParamCollectionParamValue(colParams, paramName, paramValue, DatabaseTypeEnum.SQLServer);
		}
		public static void AddDBParamCollectionParamValue(DbParameterCollection colParams, string paramName, object paramValue, DatabaseTypeEnum pDataBaseType)
		{
			DbParameter dbparam = null;
			paramName = DBContext.RectifyDBParamName(paramName, pDataBaseType);
			switch (pDataBaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					dbparam = new SqlParameter(paramName, paramValue);
					//((SqlCommand)cmd).Parameters.AddWithValue(paramName, paramValue);
					break;
				case DatabaseTypeEnum.MSAccess:
					//this is for bug in olebd datetime, truncate the miliseconds field(ff);
					if (paramValue.GetType() == typeof(DateTime))
					{
						paramValue = Convert.ToDateTime(Convert.ToDateTime(paramValue).ToString("dd-MMM-yyyy hh:mm:ss tt"));
					}
					//((OleDbCommand)cmd).Parameters.AddWithValue(paramName, paramValue);
					dbparam = new OleDbParameter(paramName, paramValue);
					break;
				case DatabaseTypeEnum.Oracle:
					dbparam = new OracleParameter(paramName, paramValue);
					//((System.Data.OracleClient.OracleCommand)cmd).Parameters.AddWithValue(paramName, paramValue);
					break;
			}
			colParams.Add(dbparam);


			//if (m_DatabaseType == DatabaseTypeEnum.SQLServer)
			//{
			//    ((SqlCommand)cmd).Parameters.AddWithValue(paramName, val);
			//}
			//else
			//{
			//    ((OleDbCommand)cmd).Parameters.AddWithValue(paramName, val);
			//}
		}

		public void ApplyPagingToCommand(DbCommand dbCommand)
		{
			switch(DBContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					StringBuilder sb = new StringBuilder(dbCommand.CommandText);
					int selectPos = dbCommand.CommandText.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase);
					int insertPos = selectPos + 7;

					sb.Insert(insertPos, " ROW_NUMBER() OVER (ORDER BY " + this.m_OrderBy + ") AS _RowNumber, ");

					int rowNumStart = ((m_PageNo - 1 ) * m_RowCount)  + 1;
					int rowNumEnd = rowNumStart +  m_RowCount - 1;
					//int rowNumEnd = (m_PageNo + 1) * m_RowCount;

					StringBuilder sb2 = new StringBuilder();
					sb2.Append("WITH DataList AS");
					sb2.Append("(");
					sb2.Append(sb.ToString());
					sb2.Append(")");
					sb2.Append(" SELECT * FROM DataList ");
					sb2.Append(string.Format(" WHERE _RowNumber BETWEEN {0} AND {1} ", rowNumStart, rowNumEnd));
					dbCommand.CommandText = sb2.ToString();
					break;
				case DatabaseTypeEnum.Oracle:
					//throw new Exception("Data base type Not supported");
					StringBuilder sbOr = new StringBuilder(dbCommand.CommandText);
					int selectPos2 = dbCommand.CommandText.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase);
					int insertPos2 = selectPos2 + 7;

					sbOr.Insert(insertPos2, " ROW_NUMBER() OVER (ORDER BY " + this.m_OrderBy + ")  RowNumber, ");

					int rowNumStart2 = ((m_PageNo - 1 ) * m_RowCount)  + 1;
					int rowNumEnd2 = rowNumStart2 +  m_RowCount - 1;
					//int rowNumEnd = (m_PageNo + 1) * m_RowCount;

					StringBuilder sbOr2 = new StringBuilder();
					sbOr2.Append("Select * FROM");
					sbOr2.Append("(");
					sbOr2.Append(sbOr.ToString());
					sbOr2.Append(")");
					sbOr2.Append(string.Format(" WHERE RowNumber BETWEEN {0} AND {1} ", rowNumStart2, rowNumEnd2));
					dbCommand.CommandText = sbOr2.ToString();
				
					break;
				default:
					throw new Exception("Data base type Not supported");
					//break;
			}
		}

		public static void AddDBParametersToDbCommand(DbCommand cmd, DBParameterInfoCollection dbParametersInfo, DBContextSettings pDBContextSettings)
		{
			StringBuilder sb = new StringBuilder(cmd.CommandText);
			if (dbParametersInfo != null && dbParametersInfo.Count > 0)
			{
				dbParametersInfo.Validate(pDBContextSettings);
				foreach (DBParameterInfo param in dbParametersInfo)
				{
					DbParameter dbParam = cmd.CreateParameter();
					if (param.IsNameChanged)
					{
						sb.Replace(param.ParameterName, param.ParameterName_Changed);
					}

					dbParam.ParameterName = param.IsNameChanged ? param.ParameterName_Changed : param.ParameterName;
					dbParam.Value = param.IsValueChanged ? param.Value_Changed : param.Value;
					dbParam.DbType = param.IsDataTypeChanged ? param.DataType_Changed : param.DataType;
					dbParam.Direction = param.Direction;

					cmd.Parameters.Add(dbParam);
				}
			}
			DataTypeConverter.CheckAndAlterSQLString_ISNULL(sb, pDBContextSettings.DatabaseType);
			cmd.CommandText = sb.ToString();

		}

        public static void AddDBParametersToOracleCommand(OracleCommand cmd, DBParameterInfoCollection dbParametersInfo, DBContextSettings pDBContextSettings)
        {
            StringBuilder sb = new StringBuilder(cmd.CommandText);
            if (dbParametersInfo != null && dbParametersInfo.Count > 0)
            {
                dbParametersInfo.Validate(pDBContextSettings);
                foreach (DBParameterInfo param in dbParametersInfo)
                {
                    DbParameter dbParam = cmd.CreateParameter();
                    if (param.IsNameChanged)
                    {
                        sb.Replace(param.ParameterName, param.ParameterName_Changed);
                    }

                    dbParam.ParameterName = param.IsNameChanged ? param.ParameterName_Changed : param.ParameterName;
                    dbParam.Value = param.IsValueChanged ? param.Value_Changed : param.Value;
                    dbParam.DbType = param.IsDataTypeChanged ? param.DataType_Changed : param.DataType;
                    dbParam.Direction = param.Direction;

                    cmd.Parameters.Add(dbParam);
                }
            }
            DataTypeConverter.CheckAndAlterSQLString_ISNULL(sb, pDBContextSettings.DatabaseType);
            cmd.CommandText = sb.ToString();

        }


        public static DBParameterInfoCollection GetDBParametersDbCommand(DbCommand cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentException("No Command Defined.");
            }

            DBParameterInfoCollection dbParametersInfo = new DBParameterInfoCollection();

            if (cmd.Parameters.Count > 0)
            {
                foreach (DbParameter param in cmd.Parameters)
                {
                    DBParameterInfo paramInfo = new DBParameterInfo();
                    paramInfo.ParameterName = param.ParameterName;
                    paramInfo.Value = param.Value;
                    paramInfo.DataType = param.DbType;
                    paramInfo.Direction = param.Direction;
                    dbParametersInfo.Add(paramInfo);
                }
            }
            return dbParametersInfo;
        }

        public static DBParameterInfoCollection GetDBParametersOracleCommand(OracleCommand cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentException("No Command Defined.");
            }

            DBParameterInfoCollection dbParametersInfo = new DBParameterInfoCollection();

            if (cmd.Parameters.Count > 0)
            {
                foreach (DbParameter param in cmd.Parameters)
                {
                    DBParameterInfo paramInfo = new DBParameterInfo();
                    paramInfo.ParameterName = param.ParameterName;
                    paramInfo.Value = param.Value;
                    paramInfo.DataType = param.DbType;
                    paramInfo.Direction = param.Direction;
                    dbParametersInfo.Add(paramInfo);
                }
            }
            return dbParametersInfo;
        }

	}
}
