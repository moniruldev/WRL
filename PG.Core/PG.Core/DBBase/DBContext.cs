using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Text;
using System.Reflection;
using System.Data.Linq.Mapping;
//using System.Linq.Dynamic;
using Oracle.ManagedDataAccess.Client;
using PG.Core.Utility;


namespace PG.Core.DBBase
{
	public partial class DBContext
	{

		#region private members

		private string m_DBContextID = string.Empty;
		private string m_DBContextRef = string.Empty;

		//private DataContext m_DBContext = null;
		private DbConnection m_Connection = null;
		private DbTransaction m_DbTrans = null;
		private DBContextSettings m_DbContextSettings = null;

		//private DatabaseTypeEnum m_DatabaseType = DatabaseTypeEnum.SQLServer;
		//private string m_ApplicationName = string.Empty;
		//private string m_ConnectionString = string.Empty;
		//private int m_ConnectionTimeOut = 15;
		//private bool m_IsWindowsAuthentication = false;
		//private bool m_IsCredantialInternal = false;
		//private bool m_IsConfigEncrypted = false;
		//private string m_EncryptionKey = string.Empty;
		//private string m_UserID = string.Empty;
		//private string m_Password = string.Empty;
		//private string m_AccessFilePassword = string.Empty;
		//private bool m_IsSqlAppRole = false;
		//private string m_SqlAppRoleName = string.Empty;
		//private string m_SqlAppRolePassword = string.Empty;
		//private bool m_DeferredLoadingEnabled = false;
		//private bool m_ObjectTrackingEnabled = false;
		//private string m_xUpdateNoFieldName = "xUpdateNo";

		private bool m_IsTransaction = false;
		private DBMap m_DBMap = new DBMap();

		#endregion


		#region public properties
		public string DBContextID
		{
			get { return m_DBContextID; }
			set { m_DBContextID = value; }
		}
		public string DBContextRef
		{
			get { return m_DBContextRef; }
			set { m_DBContextRef = value; }
		}

		public DBContextSettings DBContextSettings
		{
			get { return m_DbContextSettings; }
			//set {
				
			//    if (m_DbContextSettings == null)
			//    {
			//        UnbindEvents();
			//        m_DbContextSettings = value;
			//    }
			//    else
			//    {
			//        m_DbContextSettings = value;
			//        m_DBMap.DatabaseType = m_DbContextSettings.DatabaseType;
			//        m_DBMap.xUpdateNoFieldName = m_DbContextSettings.xUpdateNoFieldName;
			//        BindEvents();
			//    }
			//}
		}


		public bool IsTransaction
		{
			get { return m_IsTransaction; }
			//set { m_IsTransaction = value; }
		}

		public DbConnection Connection
		{
			get { return m_Connection; }
			//set { m_IsTransaction = value; }
		}

        //public OracleConnection ORACLEConnection
        //{
        //    get { return m_ORACLEConnection; }
        //    //set { m_IsTransaction = value; }
        //}

        //public OracleContext OracleContext
        //{
        //    get { return m_OracleContext; }
        //    set { m_OracleContext = value; }
        //}



		public DbTransaction Transaction
		{
			get { return m_DbTrans; }
			//set { m_IsTransaction = value; }
		}

		public DBMap DBMap
		{
			get { return m_DBMap; }
			//set { m_IsTransaction = value; }
		}

		#endregion


		#region constuctors

		//public DBContext()
		//{
		//    DBContextSettings pDBCSetttings = new DBContextSettings();
		//    SetDBContextSettings(pDBCSetttings);
		//}

		public DBContext(DBContextSettings pDBCSetttings)
		{
			//License.AppLicense.ValidateLicense();
			if (Internals.IsLicenseValidate)
			{
				License.AppLicense.ValidateLicense();
			}
			
			if (pDBCSetttings == null)
			{
				throw new ArgumentNullException("Null DBContext Settings not allowed!");
			}
			this.m_DbContextSettings = pDBCSetttings;
			this.m_DBMap.DatabaseType = pDBCSetttings.DatabaseType;
			this.m_DBMap.xUpdateNoFieldName = pDBCSetttings.xUpdateNoFieldName;
			BindEvents();
		}
		#endregion


		#region events function
		private void BindEvents()
		{
			this.m_DbContextSettings.OnDatabaseTypeChanged += new DatabaseTypeChangedEventHandler(this.DatabaseTypeChanged);
			this.m_DbContextSettings.OnxUpdateNoFieldNameChanged += new xUpdateNoFieldNameChangedEventHandler(this.xUpdateNoFieldNameChanged);
			//this.m_DbContextSettings.OnDeferredLoadingEnabledChanged += new DeferredLoadingEnabledChangedEventHandler(this.DeferredLoadingEnabledChanged);
		   // this.m_DbContextSettings.OnObjectTrackingEnabledChanged += new ObjectTrackingEnabledChangedEventHandler(this.ObjectTrackingEnabledChanged);
		}

		private void UnbindEvents()
		{
			this.m_DbContextSettings.OnDatabaseTypeChanged -= new DatabaseTypeChangedEventHandler(this.DatabaseTypeChanged);
			this.m_DbContextSettings.OnxUpdateNoFieldNameChanged -= new xUpdateNoFieldNameChangedEventHandler(this.xUpdateNoFieldNameChanged);
			//this.m_DbContextSettings.OnDeferredLoadingEnabledChanged -= new DeferredLoadingEnabledChangedEventHandler(this.DeferredLoadingEnabledChanged);
		   // this.m_DbContextSettings.OnObjectTrackingEnabledChanged -= new ObjectTrackingEnabledChangedEventHandler(this.ObjectTrackingEnabledChanged);


		}

		private void DatabaseTypeChanged(object sender, EventArgs e)
		{
			this.m_DBMap.DatabaseType = this.m_DbContextSettings.DatabaseType;
		}
		private void xUpdateNoFieldNameChanged(object sender, EventArgs e)
		{
			this.m_DBMap.xUpdateNoFieldName = this.m_DbContextSettings.xUpdateNoFieldName;
		}

		//private void DeferredLoadingEnabledChanged(object sender, EventArgs e)
		//{
		//    if (m_DBContext != null)
		//    {
		//        m_DBContext.DeferredLoadingEnabled = this.m_DbContextSettings.DeferredLoadingEnabled;
		//    }
		//}

		//private void ObjectTrackingEnabledChanged(object sender, EventArgs e)
		//{
		//    if (m_DBContext != null)
		//    {
		//        m_DBContext.ObjectTrackingEnabled = this.m_DbContextSettings.ObjectTrackingEnabled;
		//    }
		//}


		#endregion

		#region private functions



		#endregion

		#region public functions

		public void SetDBContextSettings(DBContextSettings pDBCSettings)
		{
			Utility.Helper.CopyPropertyValueByName(pDBCSettings,this);

			//this.DatabaseType = pDBCSettings.DatabaseType;
			//this.ApplicationName = pDBCSettings.ApplicationName;
			//this.ConnectionString = pDBCSettings.ConnectionString;
			//this.ConnectionTimeOut = pDBCSettings.ConnectionTimeOut;
			//this.IsWindowsAuthentication = pDBCSettings.IsWindowsAuthentication;
			//this.IsCredantialInternal = pDBCSettings.IsCredantialInternal;
			//this.IsConfigEncrypted = pDBCSettings.IsConfigEncrypted;
			//this.EncryptionKey = pDBCSettings.EncryptionKey;
			//this.UserID = pDBCSettings.UserID;
			//this.Password = pDBCSettings.Password;
			//this.AccessFilePassword = pDBCSettings.AccessFilePassword;
			//this.IsSqlAppRole = pDBCSettings.IsSqlAppRole;
			//this.SqlAppRoleName = pDBCSettings.SqlAppRoleName;


		}

		public string BuildConnectionString(string connString)
		{
			//DbConnectionStringBuilder sqlDB = new DbConnectionStringBuilder();

			string cString = string.Empty;

			switch (this.m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					System.Data.SqlClient.SqlConnectionStringBuilder sqlBD = new System.Data.SqlClient.SqlConnectionStringBuilder(connString);
					sqlBD.ApplicationName = m_DbContextSettings.ApplicationName;
					cString = sqlBD.ToString();
					break;
				case DatabaseTypeEnum.MSAccess:
					System.Data.OleDb.OleDbConnectionStringBuilder accessBD = new OleDbConnectionStringBuilder(connString);
					if (accessBD.Provider == string.Empty)
					{
						accessBD.Provider = "Microsoft.Jet.OLEDB.4.0";
					}
					//Jet OLEDB:Database Password=simpass942
					if (m_DbContextSettings.AccessFilePassword != string.Empty)
					{
						//accessBD.Add("Jet OLEDB:Database Password", "simpass942");
						accessBD.Add("Jet OLEDB:Database Password", m_DbContextSettings.AccessFilePassword);
					}
					cString = accessBD.ToString();
					break;
				case DatabaseTypeEnum.Oracle:
					//Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder oracleBD = new Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder(connString);
					//cString = oracleBD.ToString();
					cString = connString;
					break;
			}


			//if (DatabaseType == DatabaseTypeEnum.SQLServer)
			//{
			//    System.Data.SqlClient.SqlConnectionStringBuilder sqlBD = new System.Data.SqlClient.SqlConnectionStringBuilder(connString);
			//    sqlBD.ApplicationName = m_ApplicationName;
			//    cString = sqlBD.ToString();
			//}
			//else
			//{
			//    System.Data.OleDb.OleDbConnectionStringBuilder oleBD = new OleDbConnectionStringBuilder(connString);
			//    if (oleBD.Provider == string.Empty)
			//    {
			//        oleBD.Provider = "Microsoft.Jet.OLEDB.4.0";
			//    }
			//    //oleBD.DataSource = connString;


			//    accessBD.Add("Jet OLEDB:Database Password", "simpass942");
			//    cString = oleBD.ToString();
			//}
			return cString;
		}

		public bool InitConnection()
		{
			return InitConnection(m_DbContextSettings.ConnectionString);
		}
		public bool InitConnection(string pConnectionString)
		{
			bool isInit = false;
			if (string.IsNullOrEmpty(pConnectionString))
			{
				throw new ArgumentException("No Connection String!");
			}

			pConnectionString = BuildConnectionString(pConnectionString);


            if (m_Connection == null)
            {

                switch (m_DbContextSettings.DatabaseType)
                {
                    case DatabaseTypeEnum.SQLServer:
                        m_Connection = new SqlConnection(pConnectionString); ;
                        break;
                    case DatabaseTypeEnum.MSAccess:
                        m_Connection = new OleDbConnection(pConnectionString);
                        break;
                    case DatabaseTypeEnum.Oracle:
                        m_Connection = new OracleConnection(pConnectionString);
                        break;
                    default:
                        throw new ArgumentException("Database type not supported!");
                }
            }

            if (m_Connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    m_Connection.Open();
                    //m_DbContextSettings.ConnectionString = pConnectionString;
                    isInit = true;
                }
                catch { throw; }
            }

            if (isInit)
            {
                if (m_DbContextSettings.DatabaseType == DatabaseTypeEnum.Oracle)
                {
                    if (m_DbContextSettings.TextCaseInsensitive == true)
                    {
                        SetOracleCaseInsensitive();
                    }
                }

                if (m_DbContextSettings.AlterDBSchema)
                {
                    AlterDBSchema(m_DbContextSettings.DBSchemaName);
                }
            }

           


			return isInit;
		}

		public void CloseConnection(bool isConInit)
		{
			if (isConInit)
			{
				CloseConnection();
			}
		}
		public void CloseConnection()
		{

            if (m_Connection != null)
            {
                if (m_Connection.State != System.Data.ConnectionState.Closed)
                {
                    m_Connection.Close();
                }
                m_Connection.Dispose();
                m_Connection = null;
            }
		}


		//public DataContext DBDataContext
		//{
		//   get { return m_DBContext ;}
		//}

		//public DataContext GetDBContext()
		//{
		//    if (m_DBContext == null)
		//    {
		//        //InitDBContext();
		//        throw new Exception("Data context Not initialized!");
		//    }
		//    return m_DBContext;
		//}

		public bool InitDBContext()
		{
			bool isInit = false;

			if (m_DbContextSettings == null)
			{
				throw new NullReferenceException("No DBContext Settings found!");
			}

            if (m_Connection == null)
            {
                isInit = InitConnection();
            }

			//if (isInit)
			//{
			//    if (m_DBContext != null)
			//    {
			//        if (m_DBContext.Connection != null)
			//        {
			//            if (m_DBContext.Connection.State != ConnectionState.Closed)
			//            {
			//                m_DBContext.Connection.Close();
			//            }
			//        }
			//        m_DBContext = null;
			//    }

				
			//    m_DBContext = new DataContext(m_Connection);
			//    m_DBContext.DeferredLoadingEnabled = m_DbContextSettings.DeferredLoadingEnabled;
			//    m_DBContext.ObjectTrackingEnabled = m_DbContextSettings.ObjectTrackingEnabled;
			//    if (m_DbTrans != null)
			//    {
			//        m_DBContext.Transaction = m_DbTrans;
			//    }
			   
			//}
			return isInit;
		}
		//public bool CreateDataContext()
		//{
		//    bool isOpen = false;
		//    if (m_DBContext != null)
		//    {
		//        CloseDataContext();
		//    }
		//    m_DBContext = new DataContext(m_Connection);
		//    m_DBContext.DeferredLoadingEnabled = m_DbContextSettings.DeferredLoadingEnabled;
		//    m_DBContext.ObjectTrackingEnabled = m_DbContextSettings.ObjectTrackingEnabled;
		//    if (m_DbTrans != null)
		//    {
		//        m_DBContext.Transaction = m_DbTrans;
		//    }
		//    isOpen = true;
			
		//    return isOpen;
		//}

		public DataContext NewDataContext()
		{
			DataContext dc = new DataContext(m_Connection);
			dc.DeferredLoadingEnabled = m_DbContextSettings.DeferredLoadingEnabled;
			dc.ObjectTrackingEnabled = m_DbContextSettings.ObjectTrackingEnabled;
			if (m_DbTrans != null)
			{
				dc.Transaction = m_DbTrans;
			}
			return dc;
		}
		//public void CloseDataContext(bool isOpen)
		//{
		//    if (isOpen)
		//    {
		//        CloseDataContext();
		//    }
		//}
		//public void CloseDataContext()
		//{
		//    if (m_DBContext != null)
		//    {
		//        //m_DBContext.Dispose();
		//        m_DBContext = null;
		//    }
		//}

		public void ReleaseDBContext()
		{
			CloseConnection();
		   // CloseDataContext();
			//if (m_DBContext != null)
			//{
			//    m_DBContext.Dispose();
			//    m_DBContext = null;
			//    //CloseConnection();
			//}

		}
		public void ReleaseDBContext(bool isDCInit)
		{
			if (isDCInit)
			{
				ReleaseDBContext();
			}
		}
	 
		public bool StartTransaction()
		{
			return StartTransaction(m_DbContextSettings.IsolationLevel);
		}
		public bool StartTransaction(IsolationLevel pIsolationLevel)
		{
			if (m_Connection == null)
			{
				throw new Exception("Connection not initialized");
			}

			if (m_Connection.State != System.Data.ConnectionState.Open)
			{
				throw new Exception("Connection not open");
			}

			bool isTrans = false;

            if (m_DbTrans == null)
            {
                m_DbTrans = m_Connection.BeginTransaction(pIsolationLevel);

                // m_DBContext.Transaction = m_DbTrans;
                m_IsTransaction = true;
                isTrans = true;
            }
			return isTrans;
		}
	  
		public void CommitTransaction()
		{
            if (m_DbTrans != null)
            {
                m_DbTrans.Commit();
                m_DbTrans.Dispose();
                m_DbTrans = null;
                m_IsTransaction = false;
            }
		}
		public void CommitTransaction(bool isTrans)
		{
			if (isTrans)
			{
				CommitTransaction();
			}
		}

		public void RollbackTransaction()
		{
            if (m_DbTrans != null)
            {
                m_DbTrans.Rollback();
                m_DbTrans.Dispose();
                m_DbTrans = null;
                m_IsTransaction = false;
            }
		}
		public void RollbackTransaction(bool isTransInit)
		{
			if (isTransInit)
			{
				RollbackTransaction();
			}
		}

		public int GetLastIdentity_SQLServer()
		{
			bool isDCInit = false;
			isDCInit = InitDBContext();
			DbCommand cmd = m_Connection.CreateCommand();
			if (m_DbTrans != null)
			{
				cmd.Transaction = m_DbTrans;
			}
			cmd.CommandText = "SELECT @@IDENTITY";

			//Oracle
			//select id.currval from dual
			//Use the RETURNING clause:
			//insert into mytable (...) values (...)
			//returning id into v_id;

			int val = Convert.ToInt32(cmd.ExecuteScalar());
			ReleaseDBContext(isDCInit);
			return val;
		}
		public int GetLastIdentity_SQLServer(string pTableName)
		{
			if (this.m_DbContextSettings.DatabaseType != DatabaseTypeEnum.SQLServer)
			{
				throw new NotSupportedException("Supported Only MS SQL Server");
			}

			bool isDCInit = false;
			isDCInit = InitDBContext();
			DbCommand cmd = m_Connection.CreateCommand();
			if (m_DbTrans != null)
			{
				cmd.Transaction = m_DbTrans;
			}
			cmd.CommandText = "SELECT IDENT_CURRENT(@tblName)";

			((SqlCommand)cmd).Parameters.AddWithValue("@tblName", pTableName);

			int val = Convert.ToInt32(cmd.ExecuteScalar());
			ReleaseDBContext(isDCInit);
			return val;
		}


		private DbCommand CreateCommand()
		{
			DbCommand cmd = null;
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					cmd = new SqlCommand();
					break;
				case DatabaseTypeEnum.MSAccess:
					cmd = new OleDbCommand();
					break;
				case DatabaseTypeEnum.Oracle:
					cmd = new OracleCommand();
					break;
			}
			return cmd;
		}


		public DbCommand CreateDbCommand()
		{
			return CreateDbCommand(m_DbContextSettings.DatabaseType);
		}
		public DbCommand CreateDbCommand(DatabaseTypeEnum pDataBaseType)
		{
			DbCommand cmd = null;


			switch(pDataBaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					cmd = (SqlCommand)m_Connection.CreateCommand();
					if (m_DbTrans != null)
					{
                        cmd.Transaction = (SqlTransaction)m_DbTrans;
					}
					break;
				case DatabaseTypeEnum.Oracle:
                    cmd = (OracleCommand)m_Connection.CreateCommand();
                    if (m_DbTrans != null)
                    {
                        cmd.Transaction = (OracleTransaction)m_DbTrans;
                    }
					break;
				case DatabaseTypeEnum.MSAccess:
                    cmd = (OleDbCommand)m_Connection.CreateCommand();
                    if (m_DbTrans != null)
					{
                        cmd.Transaction = (OleDbTransaction)m_DbTrans;
					}
					break;
				default:
					throw new NotSupportedException("Database type not supporeted yet.");
			}

			return cmd;
		}

		//public bool SetDbCommandTransaction(DbCommand cmd)
		//{
		//    return SetDbCommandTransaction(cmd, m_DbContextSettings.DatabaseType);
		//}
		//public bool SetDbCommandTransaction(DbCommand cmd, DatabaseTypeEnum pDataBaseType)
		//{
		//    if (cmd == null)
		//    {
		//        throw new ArgumentNullException("Command object cannot be null");
		//    }

		//    bool isTransaction = false;
		//    if (pDataBaseType == DatabaseTypeEnum.SQLServer)
		//    {
		//        if (m_DBContext.Transaction != null)
		//        {
		//            cmd.Transaction = (SqlTransaction)m_DBContext.Transaction;
		//            isTransaction = true;
		//        }
		//    }
		//    else
		//    {
		//        if (m_DBContext.Transaction != null)
		//        {
		//            cmd.Transaction = (OleDbTransaction)m_DBContext.Transaction;
		//            isTransaction = true;
		//        }
		//    }
		//    return isTransaction;
		//}

		private void SetDBCommand(DbCommand cmd)
		{
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					if (cmd.Connection == null)
					{
						cmd.Connection = (SqlConnection)m_Connection;
					}
					if (cmd.Transaction == null && m_DbTrans != null)
					{
						cmd.Transaction = (SqlTransaction)m_DbTrans;
					}
					break;
				case DatabaseTypeEnum.MSAccess:
					if (cmd.Connection == null)
					{
						cmd.Connection = (OleDbConnection)m_Connection;
					}
					if (cmd.Transaction == null && m_Connection != null)
					{
						cmd.Transaction = (OleDbTransaction)m_DbTrans;
					}
					break;
				case DatabaseTypeEnum.Oracle:
                    if (cmd.Connection == null)
                    {
                        cmd.Connection = (OracleConnection)m_Connection;
                    }
                    if (cmd.Transaction == null && m_DbTrans != null)
                    {
                        cmd.Transaction = (OracleTransaction)m_DbTrans;
                    }

					break;
				default:
					throw new NotImplementedException("Not Implemented");
			}
		}

		public void SetDBCommandTransaction(DbCommand cmd)
		{
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					if (cmd.Transaction == null && m_DbTrans != null)
					{
						cmd.Transaction = (SqlTransaction)m_DbTrans;
					}
					break;
				case DatabaseTypeEnum.MSAccess:
					if (cmd.Transaction == null && m_Connection != null)
					{
						cmd.Transaction = (OleDbTransaction)m_DbTrans;
					}
					break;
				case DatabaseTypeEnum.Oracle:
                    if (cmd.Transaction == null && m_DbTrans != null)
                    {
                        cmd.Transaction = (OracleTransaction)m_DbTrans;
                    }
					break;
				default:
					throw new NotImplementedException("Not Implemented");
			}
		}


        public void SetOracleCommandTransaction(OracleCommand cmd)
        {
            switch (m_DbContextSettings.DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                    if (cmd.Transaction == null && m_DbTrans != null)
                    {
                        cmd.Transaction = (OracleTransaction)m_DbTrans;
                    }
                    break;
                default:
                    throw new NotImplementedException("Not Implemented");
            }
        }

		#endregion

		#region Do Insert functions

		public int DoInsert<T>(T item) where T : class
		{
			return DoInsert<T>(item, null, false);
		}

		public int DoInsert<T>(T item, bool IsRetrunID) where T : class
		{
			return DoInsert<T>(item, null, IsRetrunID);
		}

		public int DoInsert<T>(T item, List<string> changedList) where T : class
		{
			return DoInsert<T>(item, changedList, false);
		}

		public int DoInsert<T>(T item, List<string> changedList, bool IsRetrunID) where T : class
		{
			int retVal = 0;
			int cnt = 0;
			bool isDCInit = false;

			//List<string> cList = (List<string>)item.GetType().GetProperty("ChangedList").GetValue(item, null);
			bool isAllField = (bool)item.GetType().GetProperty("_IsAllField").GetValue(item, null);

		  //  _IsUpdateAllField

			if (changedList == null)
			{
				if (isAllField)
				{
					changedList = DBMap.GetMapPropertyNames<T>();
				}
				else
				{
					changedList = (List<string>)item.GetType().GetProperty("_ChangedList").GetValue(item, null);
				}
			}

			DbCommand cmd = CreateDBCommandInsert<T>(item, changedList);
			if (cmd.CommandText == string.Empty)
			{
				return 0;
			}
			try
			{
				isDCInit = InitDBContext();
				SetDBCommand(cmd);
				cnt = cmd.ExecuteNonQuery();
				if (cnt > 0)
				{
					bool isReturnIdentity = (bool)item.GetType().GetProperty("_IsReturnIdentity").GetValue(item, null);
					if  (IsRetrunID | isReturnIdentity)
					{
						int id = 0;
						if (m_DbContextSettings.DatabaseType == DatabaseTypeEnum.SQLServer)
						{
							id = GetLastIdentity_SQLServer();
						}
						if (m_DbContextSettings.DatabaseType == DatabaseTypeEnum.Oracle)
						{
							if (cmd.Parameters[":identityval"] == null)
							{
								throw new Exception("Identity Column Invlaid or No value.");
							}
							else
							{
								id = Convert.ToInt32(cmd.Parameters[":identityval"].Value.ToString());
							}
						}
						retVal = id;
					}                   
					else
					{
						retVal = cnt;
					}
				}
				else
				{
					retVal = cnt;
				}
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return retVal;
		}

		public int DoInsertFull<T>(T item) where T : class
		{
			return DoInsertFull<T>(item,false);
		}

		public int DoInsertFull<T>(T item, bool IsRetrunID) where T : class
		{
			List<string> cList = DBMap.GetMapPropertyNames<T>();
			return DoInsert<T>(item, cList, IsRetrunID);
		}

		public int DoInsertHT(Hashtable hTable, string sourceTableName)
		{
			return DoInsertHT(hTable, sourceTableName, false);
		}

		public int DoInsertHT(Hashtable hTable, string sourceTableName, bool isIdentity)
		{
			if (hTable == null)
			{
				throw new ArgumentNullException("hash table cannot be null");
			}

			bool isDCInit = false;
			int cnt = 0;
			try
			{
				isDCInit = InitDBContext();
				DbCommand cmd = CreateDbCommand();
				cmd.CommandText = "SELECT * FROM  " + sourceTableName + " WHERE 1=1 ";
				DataAdapter ADP = GetDataAdapter(cmd);

				//SqlDataAdapter ADP = new SqlDataAdapter(cmd);
				DataSet DS = new DataSet();
				ADP.Fill(DS);
				//ADP.Fill(DS, sourceTableName);

				DataRow DR_ADDROW = DS.Tables[0].NewRow();
				foreach (object OBJ in hTable.Keys)
				{
					string COLUMN_NAME = Convert.ToString(OBJ);
					DR_ADDROW[COLUMN_NAME] = hTable[OBJ];
				}

				DS.Tables[0].Rows.Add(DR_ADDROW);

				SetInsertCommand(ADP);


				//SqlCommandBuilder BLD = new SqlCommandBuilder(ADP);
				//ADP.InsertCommand = BLD.GetInsertCommand();
				//cnt = ADP.Update(DS, sourceTableName);
				cnt = ADP.Update(DS);
				if (isIdentity)
				{
					if (cnt > 0)
					{
						cmd.CommandText = "SELECT @@IDENTITY";
						cnt = Convert.ToInt32(cmd.ExecuteScalar());
					}
				}
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }

			return cnt;
		}


		public DbCommand CreateDBCommandInsert<T>(T item, List<string> lstChanged) where T : class
		{
			//string tblName = GetMapTableName<T>();
			DBMapTable mapTable = DBMapList.GetDBMapTableFromCache<T>();
			string tblName = mapTable.Name;
			if (tblName == string.Empty)
			{
				throw new Exception("No table name found for " + typeof(T).FullName);
			}
			if (lstChanged.Count == 0)
			{
				throw new Exception("No insert value found for " + typeof(T).FullName);
			}
			/////
			string sql = string.Empty;
			StringBuilder sbFld = new StringBuilder();
			StringBuilder sbVal = new StringBuilder();

			string fldName = string.Empty;
			bool isKey = false;
			bool isDBGen = false;
			string seqName = string.Empty;
			bool isIdentity = false;
			int cnt = 0;
			string comma = string.Empty;
			string paramName = string.Empty;

			DbCommand cmd = CreateCommand();
			List<DBMapField> listDBMap = DBMapList.GetDBMapFieldListFromCache<T>();
			foreach (string propName in lstChanged)
			{
				DBMapField cDBMap = listDBMap.Find(c => c.PropertyName == propName);
				fldName = cDBMap.FieldName;
				isKey = cDBMap.IsPrimaryKey;
				isDBGen = cDBMap.IsDBGenerated;
				seqName = cDBMap.SequenceName;
				isIdentity = cDBMap.IsIdentity;


				//fldName = GetMapFieldName(props, propName, out isKey, out isDBGen); 
				//paramName = "@" + fldName;
				paramName = DBContext.CreateParameterName(fldName,m_DbContextSettings.DatabaseType);
				object val = item.GetType().GetProperty(propName).GetValue(item, null);
				if (!isDBGen)
				{
					sbFld.Append(comma + fldName);
					sbVal.Append(comma + paramName);
					cmd.Parameters.Add(CreateDbParamWithValue(paramName, val));
					comma = ", ";
					cnt++;
				}
			}

			if (sbVal.Length == 0)
			{
				throw new Exception("No insert found for " + typeof(T).FullName);
			}

			if (m_DbContextSettings.DatabaseType == DatabaseTypeEnum.Oracle)
			{
				string strRetruning = string.Empty;

				DBMapField identityFld = listDBMap.Where(c => c.IsIdentity == true).FirstOrDefault();
				if (identityFld != null)
				{
					strRetruning = string.Format(" RETURNING {0} INTO :identityval ", identityFld.FieldName);
					OracleParameter oParam = new OracleParameter();
					oParam.ParameterName = ":identityval";
					oParam.OracleDbType = OracleDbType.Int32;
					oParam.Direction = ParameterDirection.Output;
					cmd.Parameters.Add(oParam);
				}


				sql = "INSERT INTO " + tblName + "(" + sbFld.ToString() + ") VALUES (" + sbVal.ToString() + ") " + strRetruning;
			}
			else
			{
				sql = "INSERT INTO " + tblName + "(" + sbFld.ToString() + ") VALUES (" + sbVal.ToString() + ")";
			}

			//sql = "INSERT INTO " + tblName + " t0 (" + sbFld.ToString() + ") VALUES (" + sbVal.ToString() + ")";

			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = sql;
			return cmd;
		}
	  
		
		
	  



		#endregion

		#region Do Update functions

		public int DoUpdate<T>(T item) where T : class
		{
			return DoUpdate<T>(item, null, null, false);
		}
		public int DoUpdate<T>(T item, bool IsXUpdate) where T : class
		{
			return DoUpdate<T>(item, null, null, IsXUpdate);
		}

		public int DoUpdate<T>(T item, T itemKey) where T : class
		{
			return DoUpdate<T>(item, null, itemKey, false);
		}

		public int DoUpdate<T>(T item, T itemKey, bool IsXUpdate) where T : class
		{
			return DoUpdate<T>(item, null, itemKey, IsXUpdate);
		}

		public int DoUpdate<T>(T item, List<string> changedList, T itemKey, bool IsXUpdate) where T : class
		{
			int cnt = 0;
			bool isDCInit = false;

			bool isAllField = (bool)item.GetType().GetProperty("_IsAllField").GetValue(item, null);
			if (changedList == null)
			{
				if (isAllField)
				{
					changedList = DBMap.GetMapPropertyNames<T>();
				}
				else
				{
					changedList = (List<string>)item.GetType().GetProperty("_ChangedList").GetValue(item, null);
				}
			}


			DbCommand cmd = null;
			if (itemKey == null)
			{
				cmd = CreateDBCommadUpdate<T>(item, changedList, IsXUpdate);
			}
			else
			{
				List<string> cListKey = (List<string>)itemKey.GetType().GetProperty("_ChangedList").GetValue(itemKey, null);
				cmd = CreateDBCommadUpdate<T>(item, changedList, itemKey, cListKey, IsXUpdate);
			}

			if (cmd == null)
			{
				return 0;
			}

			if (cmd.CommandText == string.Empty)
			{
				return 0;
			}
			try
			{
				isDCInit = InitDBContext();
				SetDBCommand(cmd);
				cnt = cmd.ExecuteNonQuery();
			}
			catch { throw; }
			finally 
			{ ReleaseDBContext(isDCInit); }
			return cnt;
		}

		public int DoUpdateFull<T>(T item) where T : class
		{
			return DoUpdateFull<T>(item, null, false);
		}

		public int DoUpdateFull<T>(T item, T itemKey) where T : class
		{
			return DoUpdateFull<T>(item, itemKey, false);
		}

		public int DoUpdateFull<T>(T item, T itemKey, bool IsXUpdate) where T : class
		{
			List<string> cList = DBMap.GetMapPropertyNames<T>();
			return DoUpdate<T>(item, cList, itemKey, IsXUpdate);
		}

		public int DoUpdateHT(Hashtable hTable, string sourceTableName, string Filter)
		{
			if (hTable == null)
			{
				throw new ArgumentNullException("hash table cannot be null");
			}

			bool isDCInit = false;
			int cnt = 0;
			try
			{
				isDCInit = InitDBContext();
				//SqlCommand cmd = GetDBCommand();
				DbCommand cmd = CreateDbCommand();
				cmd.CommandText = "SELECT * FROM " + sourceTableName + " WHERE " + Filter;
				DataAdapter ADP = GetDataAdapter(cmd);

				//SqlDataAdapter ADP = new SqlDataAdapter(cmd);
				DataSet DS = new DataSet();
				//ADP.Fill(DS, sourceTableName);
				ADP.Fill(DS);
				int rowNumber = 0;
				for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
				{
					DataRow DR_UPDATE = DS.Tables[0].Rows[rowNumber];
					foreach (object OBJ in hTable.Keys)
					{
						string COLUMN_NAME = Convert.ToString(OBJ);
						DR_UPDATE[COLUMN_NAME] = hTable[OBJ];
					}
					SetUpdateCommand(ADP);

					//SqlCommandBuilder BLD = new SqlCommandBuilder(ADP);
					//ADP.UpdateCommand = BLD.GetUpdateCommand();
					//ADP.Update(DS, sourceTableName);
					ADP.Update(DS);
					rowNumber++;
				}
				cnt = rowNumber;
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }

			return cnt;
		}

		public DbCommand CreateDBCommadUpdate<T>(T item, List<string> lstChanged) where T : class
		{
			return CreateDBCommadUpdate(item, lstChanged, null, null, false);
		}
		public DbCommand CreateDBCommadUpdate<T>(T item, List<string> lstChanged, bool IsXUpdate) where T : class
		{
			return CreateDBCommadUpdate(item, lstChanged, null, null, IsXUpdate);
		}
		public DbCommand CreateDBCommadUpdate<T>(T item, List<string> lstChanged, T itemKey, List<string> lstKeys) where T : class
		{
			return CreateDBCommadUpdate(item, lstChanged, itemKey, lstKeys, false);
		}
		public DbCommand CreateDBCommadUpdate<T>(T item, List<string> lstChanged, T itemKey, List<string> lstKeys, bool IsXUpdate) where T : class
		{
			//string tblName = GetMapTableName<T>();
			DBMapTable mapTable = DBMapList.GetDBMapTableFromCache<T>();
			string tblName = mapTable.Name;
			if (tblName == string.Empty)
			{
				throw new Exception("No table name found for " + typeof(T).FullName);
				//return string.Empty;
			}

			if (lstChanged.Count == 0)
			{
				return null;
				//throw new Exception("No update value for " + typeof(T).FullName);
				//return string.Empty;
			}
			/////
			Type cType = typeof(T);
			string sql = string.Empty;
			StringBuilder sbFld = new StringBuilder();
			StringBuilder sbWhere = new StringBuilder();

			string fldName = string.Empty;
			string paramName = string.Empty;
			string strAND = string.Empty;


			string sqlKey = string.Empty;
			List<string> lstKeysLocal = new List<string>();

			bool hasKey = false;
			bool isKey = false;
			bool isDBGen = false;

			if (itemKey != null)
			{
				hasKey = !(lstKeys == null || lstKeys.Count == 0);
			}


			int cnt = 0; //for listval array
			string comma = string.Empty;

			DbCommand cmd = CreateCommand();

			List<DBMapField> listDBMap = DBMapList.GetDBMapFieldListFromCache<T>();

			foreach (string propName in lstChanged)
			{
				DBMapField cDBMap = listDBMap.Find(c => c.PropertyName == propName);
				fldName = cDBMap.FieldName;
				isKey = cDBMap.IsPrimaryKey;
				isDBGen = cDBMap.IsDBGenerated;

				//fldName = GetMapFieldName(props,propName, out isKey, out isDBGen);
				//paramName = "@" + fldName;
				paramName = DBContext.CreateParameterName(fldName,m_DbContextSettings.DatabaseType);
				object val = item.GetType().GetProperty(propName).GetValue(item, null);
				if (hasKey)
				{
					if (!isDBGen)
					{
						sbFld.Append(comma + fldName + " = " + paramName);
						cmd.Parameters.Add(CreateDbParamWithValue(paramName, val));
						comma = ", ";
						cnt++;
					}
				}
				else
				{
					if (isKey)
					{
						//if key found then list that
						lstKeysLocal.Add(propName);
					}
					else
					{
						if (!isDBGen)
						{
							sbFld.Append(comma + fldName + " = " + paramName);
							cmd.Parameters.Add(CreateDbParamWithValue(paramName, val));
							comma = ", ";
							cnt++;
						}
					}
				}
			}
			if (IsXUpdate)
			{
				switch (m_DbContextSettings.DatabaseType)
				{
					case DatabaseTypeEnum.SQLServer:
						sbFld.Append(comma + m_DbContextSettings.xUpdateNoFieldName + " = ISNULL(" + m_DbContextSettings.xUpdateNoFieldName + ",0) + 1");
						break;
					case DatabaseTypeEnum.MSAccess:
						sbFld.Append(comma + m_DbContextSettings.xUpdateNoFieldName + " = IIF(ISNULL(" + m_DbContextSettings.xUpdateNoFieldName + "), 0, " + m_DbContextSettings.xUpdateNoFieldName + " )" + " 1");
						break;
					case DatabaseTypeEnum.Oracle:
						sbFld.Append(comma + m_DbContextSettings.xUpdateNoFieldName + " = NVL(" + m_DbContextSettings.xUpdateNoFieldName + ",0) + 1");
						break;
				}
				//sqlVal += comma + m_xUpdateNoFieldName + " = ISNULL(" + m_xUpdateNoFieldName + ",0) + 1";
			}

			if (sbFld.Length == 0)
			{
				throw new Exception("No update value found for " + typeof(T).FullName);
				//no values
				//return string.Empty;
			}
			if (hasKey)
			{
				//if keys from paramenter then use that, discarding any found keys
				lstKeysLocal = lstKeys;
			}
			else
			{
				if (lstKeysLocal.Count == 0)
				{
					//if no key specified or found then get table key names
					lstKeysLocal = DBMap.GetKeyNames(listDBMap);
					//lstKeysLocal = GetKeyNames<T>();
				}
			}
			if (lstKeysLocal.Count == 0)
			{
				throw new Exception("No key value for " + typeof(T).FullName);
			}

			//build where
			if (lstKeysLocal.Count > 0)
			{
				object val = null;
				foreach (string propName in lstKeysLocal)
				{
					//fldName = GetMapFieldName(props, propName, out isKey, out isDBGen);
					DBMapField cDBMap = listDBMap.Find(c => c.PropertyName == propName);
					fldName = cDBMap.FieldName;
					isKey = cDBMap.IsPrimaryKey;
					isDBGen = cDBMap.IsDBGenerated;
					if (hasKey)
					{
						val = itemKey.GetType().GetProperty(propName).GetValue(itemKey, null);
					}
					else
					{
						val = item.GetType().GetProperty(propName).GetValue(item, null);
					}
					//paramName = "@_K_" + fldName;
					paramName = CreateParameterName(fldName, m_DbContextSettings.DatabaseType, "OK_");
					sbWhere.Append(strAND + fldName + " = " + paramName);
					cmd.Parameters.Add(CreateDbParamWithValue(paramName, val));
					strAND = " AND ";
				}
				cnt++;
			}

			sql = "UPDATE " + tblName + " SET " + sbFld.ToString() + " WHERE " + sbWhere.ToString();
			//sql = "UPDATE " + tblName + " t0 " + " SET " + sbFld.ToString() + " WHERE " + sbWhere.ToString();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = sql;
			return cmd;
		}

		#endregion

		#region Do Delete Functions

		public int DoDelete<T>(T item) where T : class
		{
			int cnt = 0;
			bool isDCInit = false;
			List<string> cList = (List<string>)item.GetType().GetProperty("_ChangedList").GetValue(item, null);
			DbCommand cmd = CreateDBCommandDelete<T>(item, cList);
			if (cmd.CommandText == string.Empty)
			{
				return 0;
			}
			try
			{
				isDCInit = InitDBContext();
				SetDBCommand(cmd);
				cnt = cmd.ExecuteNonQuery();
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return cnt;
		}

		public int DoDelete(string sourceTableName, string Filter)
		{
			bool isDCInit = false;
			int cnt = 0;
			try
			{
				isDCInit = InitDBContext();
				DbCommand cmd = CreateDbCommand();
				cmd.CommandText = "DELETE FROM " + sourceTableName + " WHERE " + Filter;
				cnt = cmd.ExecuteNonQuery();
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }

			return cnt;
		}




		public DbCommand CreateDBCommandDelete<T>(T item, List<string> lstChanged) where T : class
		{
			//string tblName = GetMapTableName<T>();
			DBMapTable mapTable = DBMapList.GetDBMapTableFromCache<T>();
			string tblName = mapTable.Name;
			if (tblName == string.Empty)
			{
				throw new Exception("No table name found for " + typeof(T).FullName);
				//return string.Empty;
			}

			if (lstChanged.Count == 0)
			{
				throw new Exception("No delete key found for " + typeof(T).FullName);
				//return string.Empty;
			}
			/////

			string sql = string.Empty;
			string fldName = string.Empty;
			string paramName = string.Empty;
			StringBuilder sbFld = new StringBuilder();

			bool isKey = false;
			bool isDBGen = false;

			int cnt = 0;
			string comma = string.Empty;
			string strAND = string.Empty;

			DbCommand cmd = CreateCommand();
			List<DBMapField> listDBMap = DBMapList.GetDBMapFieldListFromCache<T>();
			foreach (string propName in lstChanged)
			{
				DBMapField cDBMap = listDBMap.Find(c => c.PropertyName == propName);
				fldName = cDBMap.FieldName;
				isKey = cDBMap.IsPrimaryKey;
				isDBGen = cDBMap.IsDBGenerated;

				//fldName = GetMapFieldName(props,propName, out isKey, out isDBGen);
				//paramName = "@" + fldName;
				paramName = CreateParameterName(fldName, m_DbContextSettings.DatabaseType);
				object val = item.GetType().GetProperty(propName).GetValue(item, null);
				//sbFld.Append(comma + fldName);
				sbFld.Append(strAND + " (" + fldName + " = " + paramName + ") ");
				//sbFld.Append(strAND + " ( t0." + fldName + " = " + paramName + ") ");
				cmd.Parameters.Add(CreateDbParamWithValue(paramName, val));
				strAND = " AND ";
				comma = ", ";
				cnt++;
			}
			if (sbFld.Length == 0)
			{
				//throw new Exception("No delete found for " + typeof(T).FullName);
				//no values
				//return null;
				cmd.CommandText = string.Empty;
			}
			else
			{
				sql = "DELETE FROM " + tblName + " WHERE " + sbFld.ToString();
				//sql = "DELETE FROM " + tblName + " t0 WHERE " + sbFld.ToString();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = sql;
			}

			return cmd;
		}





		#endregion


		#region Database Behaviour Settings


        public void CheckSetOralceCaseSensitve()
        {
            if (m_DbContextSettings.DatabaseType == DatabaseTypeEnum.Oracle)
            {
                if (m_DbContextSettings.TextCaseInsensitive == true)
                {
                    SetOracleCaseInsensitive();
                }
                else
                {
                    SetOracleCaseSensitive();
                }
            }
        }


		public void SetOracleCaseInsensitive()
		{
            if (m_Connection == null)
            {
                return;
            }

            if (m_Connection.State != ConnectionState.Open)
            {
                return;
            }

			DbCommand cmd = CreateDbCommand();
			cmd.CommandType = CommandType.Text;
			SetDBCommand(cmd);

			//to check
			//SELECT *
			//    FROM NLS_SESSION_PARAMETERS
			//    WHERE PARAMETER IN ('NLS_COMP', 'NLS_SORT');
			

			//to increase performance create index on table column
			//create index 
			//    nlsci1_gen_person 
			// on 
			//    MY_PERSON 
			//    (NLSSORT 
			//       (PERSON_LAST_NAME, 'NLS_SORT=BINARY_CI')
			//    )
			//;


			
			//cmd.CommandText = "alter session set NLS_COMP=ANSI";
			cmd.CommandText = "alter session set NLS_COMP=LINGUISTIC";
			cmd.ExecuteNonQuery();


			cmd.CommandText = "alter session set NLS_SORT=BINARY_CI";
			cmd.ExecuteNonQuery();

		}
		public void SetOracleCaseSensitive()
		{

            if (m_Connection == null)
            {
                return;
            }

            if (m_Connection.State != ConnectionState.Open)
            {
                return;
            }


			DbCommand cmd = CreateDbCommand();
			cmd.CommandType = CommandType.Text;
			SetDBCommand(cmd);

			cmd.CommandText = "alter session set NLS_COMP=BINARY";
			cmd.ExecuteNonQuery();


			//cmd.CommandText = "alter session set NLS_SORT=GENERIC_M_CI;";
			cmd.CommandText = "alter session set NLS_SORT=BINARY";
			cmd.ExecuteNonQuery();

		}
		



		public void AlterDBSchema(string schemaName)
		{
            if (m_Connection == null)
            {
                return;
            }

            if (m_Connection.State != ConnectionState.Open)
            {
                return;
            }

			if (schemaName.Trim() == string.Empty)
			{
				return;
			}

			DbCommand cmd = CreateDbCommand();
			cmd.CommandType = CommandType.Text;
			SetDBCommand(cmd);


			switch(m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					cmd.CommandText = "USE " + schemaName;
					break;
				case DatabaseTypeEnum.Oracle:
					cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA =" + schemaName;
					break;
				default:
					throw new Exception("AlterDBSchema: Datatabase Type Not Suppreted");
					//break;
			}

			cmd.ExecuteNonQuery();

		}




		#endregion


		#region utility functions


		public DbParameter CreateDbParamWithValue(string paramName, object paramValue)
		{
			DbParameter dbParam = null;
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:

					if (paramValue == null)
					{
						dbParam = new SqlParameter(paramName, DBNull.Value);
					}
					else
					{
						dbParam = new SqlParameter(paramName, paramValue);
					}
					break;
				case DatabaseTypeEnum.MSAccess:
					if (paramValue == null)
					{
						dbParam = new OleDbParameter(paramName, DBNull.Value);
					}
					else
					{
						//this is for bug in olebd datetime, truncate the miliseconds field(ff);
						if (paramValue.GetType() == typeof(DateTime))
						{
							paramValue = Convert.ToDateTime(Convert.ToDateTime(paramValue).ToString("dd-MMM-yyyy hh:mm:ss tt"));
						}
						dbParam = new OleDbParameter(paramName, paramValue);
					}
					break;
				case DatabaseTypeEnum.Oracle:
					//throw new Exception("Oracle Not supported in this function!");
					if (paramValue == null)
					{
						dbParam = new OracleParameter(paramName, DBNull.Value);
					}
					else
					{
						Type vType = paramValue.GetType();
						OracleDbType oType = DataTypeConverter.ConvertTypeToOracleDbType(vType);
						if (Type.GetTypeCode(vType) == TypeCode.Boolean)
						{
							if (this.DBContextSettings.ConvertBoolData)
							{
								string bDataType = this.DBContextSettings.BoolDataType.Trim().ToLower();
								switch (bDataType)
								{
									case "string":
									case "nvarchar":
									case "nvarchar2":
										oType = OracleDbType.NVarchar2;
										paramValue = Convert.ToBoolean(paramValue) ? this.DBContextSettings.BoolTrueValue : this.DBContextSettings.BoolFalseValue;
										break;
									case "nchar":
										oType = OracleDbType.NChar;
										paramValue = Convert.ToBoolean(paramValue) ? this.DBContextSettings.BoolTrueValue : this.DBContextSettings.BoolFalseValue;
										break;
									case "varchar":
									case "varchar2":
										oType = OracleDbType.Varchar2;
										paramValue = Convert.ToBoolean(paramValue) ? this.DBContextSettings.BoolTrueValue : this.DBContextSettings.BoolFalseValue;
										break;
									case "char":
										oType = OracleDbType.Char;
										paramValue = Convert.ToBoolean(paramValue) ? this.DBContextSettings.BoolTrueValue : this.DBContextSettings.BoolFalseValue;
										break;
									case "number":
									case "int":
									case "integer":
									case "bit":
										oType = OracleDbType.Char;
										paramValue = Convert.ToBoolean(paramValue) ? Convert.ToInt32(this.DBContextSettings.BoolTrueValue) : Convert.ToInt32(this.DBContextSettings.BoolFalseValue);
										break;
									default:
										oType = OracleDbType.Int32;
										paramValue = Convert.ToBoolean(paramValue) ? "0" : "1";
										break;
								}
							}
						}

						dbParam = new OracleParameter(paramName, oType);
						dbParam.Value = paramValue;
					}
					break;
				default:
					throw new Exception("Database Type Not Supported!");
				//break;
			}
			return dbParam;
		}

		public void AddDBParamCollectionParamValue(DbParameterCollection colParams, string paramName, object paramValue)
		{
			colParams.Add(CreateDbParamWithValue(paramName, paramValue));
		}


		public int GetSequence_NextVal(string pSeqName)
		{
			int sVal = 0;
			bool isDCInit = false;
			isDCInit = InitDBContext();
			DbCommand cmd = CreateDbCommand();
			string seqName = pSeqName + ".NEXTVAL";
			string strSql = string.Format("SELECT {0} FROM DUAL", seqName);
			cmd.CommandText = strSql;
			cmd.CommandType = System.Data.CommandType.Text;
			sVal = Convert.ToInt32(cmd.ExecuteScalar());
			ReleaseDBContext(isDCInit); 
			return sVal;
		}
		public int GetSequence_CurrVal(string pSeqName)
		{
			int sVal = 0;
			bool isDCInit = false;
			isDCInit = InitDBContext();
			DbCommand cmd = CreateDbCommand();
			string seqName = pSeqName + ".CURRVAL";
			string strSql = string.Format("SELECT {0} FROM DUAL", seqName);
			cmd.CommandText = strSql;
			cmd.CommandType = System.Data.CommandType.Text;
			sVal = Convert.ToInt32(cmd.ExecuteScalar());
			ReleaseDBContext(isDCInit);
			return sVal;
		}


		public bool IncreamentValue(string tblName, string fldName, string keyName, int keyValue)
		{
			return IncreamentValue(tblName, fldName, keyName, 1);
		}
		public bool IncreamentValue(string tblName, string fldName, string keyName, int keyValue, int factor)
		{
			if (tblName == string.Empty || fldName == string.Empty || keyName == string.Empty)
			{
				return false;
			}
			string strSql = string.Empty;
			string factorParamName = CreateParameterName("factor", m_DbContextSettings.DatabaseType);
			string keyValParamName = CreateParameterName("keyval", m_DbContextSettings.DatabaseType);
			switch(m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					strSql = " Update " + tblName + " SET " + fldName + " = ISNULL(" + fldName + ",0) + " + factorParamName;
					break;
				case DatabaseTypeEnum.MSAccess:
					strSql = " Update " + tblName + " SET " + fldName + " = IIF(ISNULL(" + fldName + "), 0, " + fldName + ") + " + factorParamName;
					break;
				case DatabaseTypeEnum.Oracle:
					strSql = " Update " + tblName + " SET " + fldName + " = NVL(" + fldName + ",0) + " + factorParamName;
					break;
			}


			strSql += " Where " + keyName + " = " + keyValParamName;
			bool isDCInit = false;
			int cnt = 0;
			try
			{
				isDCInit = InitDBContext();
				DbCommand cmd = CreateDbCommand();
				cmd.CommandText = strSql;
				cmd.Parameters.Add(CreateDbParamWithValue(factorParamName, factor));
				cmd.Parameters.Add(CreateDbParamWithValue(keyValParamName, keyValue));
				cnt = cmd.ExecuteNonQuery();
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return cnt > 0;
		}

		public bool IncreamentXUpdateNo<T>(string keyName, int keyValue, int factor) where T : class
		{
			string tblName = DBMap.GetDBMapTable<T>().Name;
			return IncreamentValue(tblName, m_DbContextSettings.xUpdateNoFieldName, keyName, keyValue, factor);
		}
		public bool IncreamentXUpdateNo<T>(string keyName, int keyValue) where T : class
		{
			string tblName = DBMap.GetDBMapTable<T>().Name;
			return IncreamentValue(tblName, m_DbContextSettings.xUpdateNoFieldName, keyName, keyValue, 1);
		}


        public long GetMaxNo(string tableName, string columnName)
        {
            long lngMaxno = 0;
            //bool isDCInit = false;
            //try
            {
                //isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                OracleCommand oCmd = new OracleCommand();

                string strSQL = string.Format("SELECT NVL(MAX({0}),0) FROM {1}", columnName, tableName);
                oCmd.CommandText = strSQL;
                oCmd.CommandType = CommandType.Text;
                oCmd.Connection = (OracleConnection)m_Connection;
                if (m_DbTrans != null)
                {
                    oCmd.Transaction = (OracleTransaction)m_DbTrans;
                }

                lngMaxno = Convert.ToInt64(oCmd.ExecuteScalar());

            }
            // catch { throw; }
            // finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return lngMaxno;

        }

		public long GetMaxNo<T>(string fieldName) where T : class
		{
			return GetMaxNo<T>(fieldName, string.Empty, null, string.Empty, null);
		}
		public long GetMaxNo<T>(string fieldName, string keyField, object keyVal) where T : class
		{
			return GetMaxNo<T>(fieldName, keyField, keyVal, string.Empty, null);
		}
		public long GetMaxNo<T>(string fieldName, string keyField, object keyVal, string keyField2, object keyVal2) where T : class
		{
			string tblName = DBMap.GetDBMapTable<T>().Name;
			if (tblName == string.Empty | fieldName == string.Empty)
			{
				return 0;
			}
			bool isDCInit = false;
			long maxNo = 0;
			try
			{
				isDCInit = InitDBContext();
				DbCommand cmd = CreateDbCommand();
				string strSql = " Select MAX(" + fieldName + ") From " + tblName + " Where 1=1 ";
				bool a;
				if (keyField != string.Empty)
				{
					string keyFieldName = DBMap.GetMapFieldName<T>(keyField, out a, out a);
					strSql += " AND " + keyFieldName + " = @keyVal";
					cmd.Parameters.Add(CreateDbParamWithValue("@keyVal", keyVal));
				}
				if (keyField2 != string.Empty)
				{
					string keyFieldName2 = DBMap.GetMapFieldName<T>(keyField2, out a, out a);
					strSql += " AND " + keyFieldName2 + " = @keyVal2";
					cmd.Parameters.Add(CreateDbParamWithValue("@keyVal2", keyVal2));
				}
				cmd.CommandText = strSql;
				cmd.CommandType = System.Data.CommandType.Text;
				object num = cmd.ExecuteScalar();
				if (num != DBNull.Value)
				{
					maxNo = Convert.ToInt64(num);
				}
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return maxNo;
		}

		public DataTable GetDataTable(SqlCommand cmd)
		{
			return GetDataTableInternal(cmd);
		}
		public DataTable GetDataTable(OleDbCommand cmd)
		{
			return GetDataTableInternal(cmd);
		}
		public DataTable GetDataTable(OracleCommand cmd)
		{
			return GetDataTableInternal(cmd);
		}

		public DataTable GetDataTable(DbCommand cmd)
		{
			return GetDataTableInternal(cmd);
		}
		public DataTable GetDataTable(string pCommandText)
		{
			DBCommandInfo dbCommandInfo = new DBCommandInfo();
			dbCommandInfo.CommandType = CommandType.Text;
			dbCommandInfo.CommandText = pCommandText;
			return GetDataTable(dbCommandInfo);
		}

		public DataTable GetDataTable(DBCommandInfo dbCommandInfo)
		{
			if (dbCommandInfo == null)
			{
				throw new ArgumentException("No Command Info Defined.");
			}

			if (dbCommandInfo.CommandText == string.Empty)
			{
				throw new ArgumentException("No Command Statement Defined.");
			}
			DbCommand cmd = CreateDbCommand();
			cmd.CommandType = dbCommandInfo.CommandType;
			cmd.CommandText = dbCommandInfo.CommandText;
			DBQuery.AddDBParametersToDbCommand(cmd, dbCommandInfo.DBParametersInfo, m_DbContextSettings);;
			return GetDataTableInternal(cmd);
		}

		private DataTable GetDataTableInternal(DbCommand cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("Command object cannot be null");
			}
			bool isDCInit = false;
			DataTable dtTable = new DataTable();

			
			try
			{
				isDCInit = InitDBContext();
				SetDBCommand(cmd);
				dtTable.Load(cmd.ExecuteReader());
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return dtTable;
		}

		public DataSet GetDataSet(SqlCommand cmd)
		{
			return GetDataSetInternal(cmd);
		}
		public DataSet GetDataSet(OleDbCommand cmd)
		{
			return GetDataSetInternal(cmd);
		}

		public DataSet GetDataSet(OracleCommand cmd)
		{
			return GetDataSetInternal(cmd);
		}

		public DataSet GetDataSet(DbCommand cmd)
		{
			return GetDataSetInternal(cmd);
		}
		public DataSet GetDataSet(string pCommandText)
		{
			DBCommandInfo dbCommandInfo = new DBCommandInfo();
			dbCommandInfo.CommandType = CommandType.Text;
			dbCommandInfo.CommandText = pCommandText;
			return GetDataSet(dbCommandInfo);
		}

		public DataSet GetDataSet(DBCommandInfo dbCommandInfo)
		{
			if (dbCommandInfo == null)
			{
				throw new ArgumentException("No Command Info Defined.");
			}

			if (dbCommandInfo.CommandText == string.Empty)
			{
				throw new ArgumentException("No Command Statement Defined.");
			}
			DbCommand cmd = CreateDbCommand();
			cmd.CommandType = dbCommandInfo.CommandType;
			cmd.CommandText = dbCommandInfo.CommandText;
			DBQuery.AddDBParametersToDbCommand(cmd, dbCommandInfo.DBParametersInfo, m_DbContextSettings);
			return GetDataSetInternal(cmd);
		}

		private DataSet GetDataSetInternal(DbCommand cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("Command object cannot be null");
			}
			bool isDCInit = false;
			DataSet dtSet = new DataSet();
		   
			DataTable dtTable = new DataTable();
			try
			{
				isDCInit = InitDBContext();
				SetDBCommand(cmd);
				dtTable.Load(cmd.ExecuteReader());
				dtSet.Tables.Add(dtTable);
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return dtSet;
		}


		public List<T> GetDataList<T>(SqlCommand cmd) where T : class
		{
			return GetDataListInternal<T>(cmd);
		}
		public List<T> GetDataList<T>(OracleCommand cmd) where T : class
		{
			return GetDataListInternal<T>(cmd);
		}

		public List<T> GetDataList<T>(OleDbCommand cmd) where T : class
		{
			return GetDataListInternal<T>(cmd);
		}
		public List<T> GetDataList<T>(DbCommand cmd) where T : class
		{
			return GetDataListInternal<T>(cmd);
		}
		public List<T> GetDataList<T>(string pCommandText) where T : class
		{
			DbCommand cmd = CreateDbCommand();
			cmd.CommandText = pCommandText;
			cmd.CommandType = CommandType.Text;
			return GetDataListInternal<T>(cmd);
		}
		private List<T> GetDataListInternal<T>(DbCommand cmd) where T: class
		{
			DataTable dtData = GetDataTable(cmd);
			List<T> lstData = new List<T>();
			PropertyInfo[] targetProperties = typeof(T).GetProperties();

			foreach (DataRow dRow in dtData.Rows)
			{
				lstData.Add(DBMap.ConvertDataRowToObject<T>(dRow, targetProperties, DBContextSettings.UseDBColumnMap, DBContextSettings.ConvertNullToDefault));
			}
			return lstData;
		}

		public T GetData<T>(SqlCommand cmd) where T : class
		{
			return GetDataInternal<T>(cmd);
		}
		public T GetData<T>(OracleCommand cmd) where T : class
		{
			return GetDataInternal<T>(cmd);
		}

		public T GetData<T>(OleDbCommand cmd) where T : class
		{
			return GetDataInternal<T>(cmd);
		}
		public T GetData<T>(DbCommand cmd) where T : class
		{
			return GetDataInternal<T>(cmd);
		}
		public T GetData<T>(string pCommandText) where T : class
		{
			DbCommand cmd = CreateDbCommand();
			cmd.CommandText = pCommandText;
			cmd.CommandType = CommandType.Text;
			return GetData<T>(cmd);
		}

		//public T GetData<T>(string pCommandText, List<DbParameter> pDbParameters) where T : class
		//{
		//    DbCommand cmd = CreateDbCommand();
		//    cmd.CommandText = pCommandText;
		//    cmd.CommandType = CommandType.Text;
		//    cmd.Parameters.AddRange(pDbParameters);
		//    return GetData<T>(cmd);
		//}

		private T GetDataInternal<T>(DbCommand cmd) where T : class
		{
			T data = null;
			List<T> lstData = GetDataList<T>(cmd);
			if (lstData.Count > 0)
			{
			   data = lstData[0];
			}
			return data;
		}

		public int ExecuteNonQuery(SqlCommand cmd)
		{
			return ExecuteNonQueryInternal(cmd);
		}
		public int ExecuteNonQuery(OleDbCommand cmd)
		{
			return ExecuteNonQueryInternal(cmd);
		}
		public int ExecuteNonQuery(OracleCommand cmd)
		{
			return ExecuteNonQueryInternal(cmd);
		}
		public int ExecuteNonQuery(DbCommand cmd)
		{
			return ExecuteNonQueryInternal(cmd);
		}
		public int ExecuteNonQuery(string pCommandText)
		{   
			DBCommandInfo dbCommandInfo = new DBCommandInfo();
			dbCommandInfo.CommandType = CommandType.Text;
			dbCommandInfo.CommandText = pCommandText;
			return ExecuteNonQuery(dbCommandInfo); 
		}

		public int ExecuteNonQuery(DBCommandInfo dbCommandInfo)
		{
			if (dbCommandInfo == null)
			{
				throw new ArgumentException("No Command Info Defined.");
			}

			if (dbCommandInfo.CommandText == string.Empty)
			{
				throw new ArgumentException("No Command Statement Defined.");
			}
			DbCommand cmd = CreateDbCommand();
			cmd.CommandType = dbCommandInfo.CommandType;
			cmd.CommandText = dbCommandInfo.CommandText;
			DBQuery.AddDBParametersToDbCommand(cmd, dbCommandInfo.DBParametersInfo, m_DbContextSettings);
			return ExecuteNonQueryInternal(cmd);
		}

		private int ExecuteNonQueryInternal(DbCommand cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("Command object cannot be null");
			}

			int iRow = 0;
			bool isDCInit = false;
			try
			{
				isDCInit = InitDBContext();
				SetDBCommand(cmd);
				iRow = cmd.ExecuteNonQuery();
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return iRow;
		}

		public object ExecuteScalar(SqlCommand cmd)
		{
			return ExecuteScalarInternal(cmd);
		}
		public object ExecuteScalar(OleDbCommand cmd)
		{
			return ExecuteScalarInternal(cmd);
		}
		public object ExecuteScalar(OracleCommand cmd)
		{
			return ExecuteScalarInternal(cmd);
		}
		public object ExecuteScalar(DbCommand cmd)
		{
			return ExecuteScalarInternal(cmd);
		}
		public object ExecuteScalar(string pCommandText)
		{
			DBCommandInfo dbCommandInfo = new DBCommandInfo();
			dbCommandInfo.CommandType = CommandType.Text;
			dbCommandInfo.CommandText = pCommandText;
			return ExecuteScalar(dbCommandInfo);
		}

		public object ExecuteScalar(DBCommandInfo dbCommandInfo)
		{
			if (dbCommandInfo == null)
			{
				throw new ArgumentException("No Command Info Defined.");
			}

			if (dbCommandInfo.CommandText == string.Empty)
			{
				throw new ArgumentException("No Command Statement Defined.");
			}
			DbCommand cmd = CreateDbCommand();
			cmd.CommandType = dbCommandInfo.CommandType;
			cmd.CommandText = dbCommandInfo.CommandText;
			DBQuery.AddDBParametersToDbCommand(cmd, dbCommandInfo.DBParametersInfo, m_DbContextSettings);
			return ExecuteScalarInternal(cmd);
		}


		private object ExecuteScalarInternal(DbCommand cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("Command object cannot be null");
			}

			object data = null;
			bool isDCInit = false;
			try
			{
				isDCInit = InitDBContext();
				SetDBCommand(cmd);
				data = cmd.ExecuteScalar();
			}
			catch { throw; }
			finally { ReleaseDBContext(isDCInit); }
			return data;
		}

		public static string GetParameterPrefix(DatabaseTypeEnum pDataBaseType)
		{
			string pPrefix = "@";

			switch (pDataBaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					pPrefix = "@";
					break;
				case DatabaseTypeEnum.MSAccess:
					pPrefix = "@";
					break;
				case DatabaseTypeEnum.Oracle:
					pPrefix = ":";
					break;
			}

			return pPrefix;
		}

		public static string CreateParameterName(string fldName, DatabaseTypeEnum pDataBaseType)
		{
			return CreateParameterName(fldName, pDataBaseType, string.Empty);
		}

		public static string CreateParameterName(string fldName, DatabaseTypeEnum pDataBaseType, string strMarker)
		{
			string paramName = "@" + strMarker + fldName;
			switch (pDataBaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					paramName = "@" + strMarker + fldName;
					break;
				case DatabaseTypeEnum.MSAccess:
					paramName = "@" + strMarker + fldName;
					break;
				case DatabaseTypeEnum.Oracle:
					paramName = ":" + strMarker + fldName;
					break;
				default:
					throw new Exception("Database Type not supported!");
				//break;

			}
			return paramName;
		}

		public static string RectifyDBParamName(string pDBParamName, DatabaseTypeEnum pDatabaseType)
		{
			string pParamName = pDBParamName;
			switch (pDatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					pParamName = pDBParamName.Replace(":", "@");
					pParamName = (pParamName.StartsWith("@") == false) ? "@" + pParamName : pParamName;
					//pParamName = pDBParamName.Replace(":", "@");
					break;
				case DatabaseTypeEnum.MSAccess:
					pParamName = pDBParamName.Replace(":", "@");
					pParamName = (pParamName.StartsWith("@") == false) ? "@" + pParamName : pParamName;
					//pParamName = pDBParamName.Replace(":", "@");
					break;
				case DatabaseTypeEnum.Oracle:
					pParamName = pDBParamName.Replace("@", ":");
					pParamName = (pParamName.StartsWith(":") == false) ? ":" + pParamName : pParamName;
					//pParamName = pDBParamName.Replace("@", ":");
					break;
				default:
					//throw new Exception("Database Type Not supported");
					break;
			}

			return pParamName;
		}


		public DbParameter CreateNewParameter(string pName, object pValue)
		{
			DbParameter dbParam = null;

			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					SqlParameter param = new SqlParameter();
					param.ParameterName = pName;
					param.Value = pValue;
					dbParam = param;
					break;
				case DatabaseTypeEnum.MSAccess:
					OleDbParameter param2 =  new OleDbParameter();
					param2.ParameterName = pName;
					param2.Value = pValue;
					dbParam = param2;
					break;
				case DatabaseTypeEnum.Oracle:
					OracleParameter param3 = new OracleParameter();
					param3.ParameterName = pName;
					param3.Value = pValue;
					dbParam = param3;
					break;

				default:
					throw new Exception("Database type not supported");
					//.break;

			}

			return dbParam;
		}


		public void AddParamWithValue(DbCommand cmd, string paramName, object paramValue)
		{

			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					((SqlCommand)cmd).Parameters.AddWithValue(paramName, paramValue);
					break;
				case DatabaseTypeEnum.MSAccess:
					((OleDbCommand)cmd).Parameters.AddWithValue(paramName, paramValue);
					break;
				case DatabaseTypeEnum.Oracle:
					((OracleCommand)cmd).Parameters.Add(paramName, paramValue);
					break;
				default:
					throw new NotSupportedException("option not supported");
			}
		}

		public DataAdapter GetDataAdapter(SqlCommand cmd)
		{
			return GetDataAdapterInternal(cmd);
		}
		public DataAdapter GetDataAdapter(OleDbCommand cmd)
		{
			return GetDataAdapterInternal(cmd);
		}
		public DataAdapter GetDataAdapter(DbCommand cmd)
		{
			return GetDataAdapterInternal(cmd);
		}
		private DataAdapter GetDataAdapterInternal(DbCommand cmd)
		{
			DataAdapter dap = null;
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					dap = new SqlDataAdapter((SqlCommand)cmd);
					break;
				case DatabaseTypeEnum.MSAccess:
					dap = new OleDbDataAdapter((OleDbCommand)cmd);
					break;
				case DatabaseTypeEnum.Oracle:
					dap = new OracleDataAdapter((OracleCommand)cmd);
					break;
				default:
					throw new NotSupportedException("option not supported");

			}
			return dap;
		}

		public void SetInsertCommand(DataAdapter dap)
		{
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					SqlCommandBuilder SCB = new SqlCommandBuilder((SqlDataAdapter)dap);
					((SqlDataAdapter)dap).InsertCommand = SCB.GetInsertCommand();
					break;
				case DatabaseTypeEnum.MSAccess:
					OleDbCommandBuilder OCB = new OleDbCommandBuilder((OleDbDataAdapter)dap);
					((OleDbDataAdapter)dap).InsertCommand = OCB.GetInsertCommand();
					break;
				case DatabaseTypeEnum.Oracle:
					OracleCommandBuilder OrCB = new OracleCommandBuilder((OracleDataAdapter)dap);
					((OracleDataAdapter)dap).InsertCommand = OrCB.GetInsertCommand();
					break;
				default:
					throw new NotSupportedException("option not supported");

			}
		}

		public void SetUpdateCommand(DataAdapter dap)
		{
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					SqlCommandBuilder SCB = new SqlCommandBuilder((SqlDataAdapter)dap);
					((SqlDataAdapter)dap).UpdateCommand = SCB.GetUpdateCommand();
					break;
				case DatabaseTypeEnum.MSAccess:
					OleDbCommandBuilder OCB = new OleDbCommandBuilder((OleDbDataAdapter)dap);
					((OleDbDataAdapter)dap).UpdateCommand = OCB.GetUpdateCommand();
					break;
				case DatabaseTypeEnum.Oracle:
					OracleCommandBuilder OrCB = new OracleCommandBuilder((OracleDataAdapter)dap);
					((OracleDataAdapter)dap).UpdateCommand = OrCB.GetUpdateCommand();
					break;
				default:
					throw new NotSupportedException("option not supported");

			}
		}

		public void SetDeleteCommand(DataAdapter dap)
		{
			switch (m_DbContextSettings.DatabaseType)
			{
				case DatabaseTypeEnum.SQLServer:
					SqlCommandBuilder SCB = new SqlCommandBuilder((SqlDataAdapter)dap);
					((SqlDataAdapter)dap).DeleteCommand = SCB.GetDeleteCommand();
					break;
				case DatabaseTypeEnum.MSAccess:
					OleDbCommandBuilder OCB = new OleDbCommandBuilder((OleDbDataAdapter)dap);
					((OleDbDataAdapter)dap).DeleteCommand = OCB.GetDeleteCommand();
					break;
				case DatabaseTypeEnum.Oracle:
					OracleCommandBuilder OrCB = new OracleCommandBuilder((OracleDataAdapter)dap);
					((OracleDataAdapter)dap).DeleteCommand = OrCB.GetDeleteCommand();
					break;
				default:
					throw new NotSupportedException("option not supported");
			}
		}

		#endregion


		#region DDL functions


		public string GetSQLStatementCreate<T>() where T : class
		{
			return GetSQLStatementCreate(typeof(T));
		}
		public string GetSQLStatementCreate(Type pType)
		{
			//string tblName = GetMapTableName<T>();
			DBMapTable mapTable = DBMap.GetDBMapTable(pType);
			string tblName = mapTable.Name;
			if (tblName == string.Empty)
			{
				throw new Exception("No table name found for " + pType.FullName);
			}
			/////
			string sql = string.Empty;
			StringBuilder sbCreate = new StringBuilder();

			StringBuilder sbFld = new StringBuilder();
			StringBuilder sbVal = new StringBuilder();

			string fldName = string.Empty;
			//bool isKey = false;
			//bool isDBGen = false;
			string seqName = string.Empty;
			//bool isIdentity = false;
			//int cnt = 0;
			string comma = string.Empty;
			string paramName = string.Empty;

			DbCommand cmd = CreateCommand();
			List<DBMapField> listDBMap = DBMap.GetDBMapFieldList(pType);

			foreach (DBMapField mapField in listDBMap)
			{

			}

			//foreach (string propName in lstChanged)
			//{
			//    DBMapField cDBMap = listDBMap.Find(c => c.PropertyName == propName);
			//    fldName = cDBMap.FieldName;
			//    isKey = cDBMap.IsPrimaryKey;
			//    isDBGen = cDBMap.IsDBGenerated;
			//    seqName = cDBMap.SequenceName;
			//    isIdentity = cDBMap.IsIdentity;


			//    //fldName = GetMapFieldName(props, propName, out isKey, out isDBGen); 
			//    //paramName = "@" + fldName;
			//    paramName = DBContext.CreateParameterName(fldName, m_DbContextSettings.DatabaseType);
			//    object val = item.GetType().GetProperty(propName).GetValue(item, null);
			//    if (!isDBGen)
			//    {
			//        sbFld.Append(comma + fldName);
			//        sbVal.Append(comma + paramName);
			//        cmd.Parameters.Add(CreateDbParamWithValue(paramName, val));
			//        comma = ", ";
			//        cnt++;
			//    }
			//}

			if (sbVal.Length == 0)
			{
				throw new Exception("No insert found for " + pType.FullName);
			}

			if (m_DbContextSettings.DatabaseType == DatabaseTypeEnum.Oracle)
			{
				string strRetruning = string.Empty;

				DBMapField identityFld = listDBMap.Where(c => c.IsIdentity == true).FirstOrDefault();
				if (identityFld != null)
				{
					strRetruning = string.Format(" RETURNING {0} INTO :identityval ", identityFld.FieldName);
					OracleParameter oParam = new OracleParameter();
					oParam.ParameterName = ":identityval";
					oParam.OracleDbType = OracleDbType.Int32;
					oParam.Direction = ParameterDirection.Output;
					cmd.Parameters.Add(oParam);
				}


				sql = "INSERT INTO " + tblName + "(" + sbFld.ToString() + ") VALUES (" + sbVal.ToString() + ") " + strRetruning;
			}
			else
			{
				sql = "INSERT INTO " + tblName + "(" + sbFld.ToString() + ") VALUES (" + sbVal.ToString() + ")";
			}

			//sql = "INSERT INTO " + tblName + " t0 (" + sbFld.ToString() + ") VALUES (" + sbVal.ToString() + ")";

			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = sql;
			return sbCreate.ToString();
		}

		
		
		#endregion
	}
}
