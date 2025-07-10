using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PG.Core.DBBase
{
    public class OracleContext
    {
        public OracleConnection m_Connection = null;
        public OracleTransaction m_Transaction = null;

        public OracleConnection Connection
        {
            get { return m_Connection; }
            //set { m_IsTransaction = value; }
        }

        public OracleTransaction Transaction
        {
            get { return m_Transaction; }
            //set { m_IsTransaction = value; }
        }


        public bool InitConnection(string strConnection)
        {
            bool bStatus = false;

            m_Connection = new OracleConnection(strConnection);
            m_Connection.Open();

            return bStatus;
        }

        public bool CloseConnection()
        {
            bool bStatus = false;

            if (m_Connection != null)
            {
                if (m_Connection.State != ConnectionState.Closed)
                {
                    m_Connection.Close();
                }
                m_Connection.Dispose();
                m_Connection = null;
            }

            return bStatus;
        }

        public bool StartTransaction()
        {
            bool isTrans = false;
            if (m_Transaction == null)
            {
                m_Transaction = m_Connection.BeginTransaction();
                isTrans = true;
            }
            return isTrans;
        }


        public void CommitTransaction()
        {
            if (m_Transaction != null)
            {
                m_Transaction.Commit();
                m_Transaction.Dispose();
                m_Transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (m_Transaction != null)
            {
                m_Transaction.Rollback();
                m_Transaction.Dispose();
                m_Transaction = null;
            }
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
                oCmd.Connection = m_Connection;
                if (m_Transaction != null)
                {
                    oCmd.Transaction = m_Transaction;
                }

                lngMaxno = Convert.ToInt64(oCmd.ExecuteScalar());

            }
            // catch { throw; }
            // finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return lngMaxno;

        }

        public DataTable GetDataTableByCommand(OracleCommand cmd)
        {
            if (m_Connection != null)
            {
                cmd.Connection = m_Connection;
            }

            if (m_Transaction != null)
            {
                cmd.Transaction = m_Transaction;
            }
            DataTable tbl = new DataTable();
            tbl.Load(cmd.ExecuteReader());
            return tbl;
        }

        public object ExecuteScalar(OracleCommand cmd)
        {
             if (m_Connection != null)
            {
                cmd.Connection = m_Connection;
            }

            if (m_Transaction != null)
            {
                cmd.Transaction = m_Transaction;
            }

            return cmd.ExecuteScalar();
        }

        public int ExecuteNonQuery(OracleCommand cmd)
        {
             if (m_Connection != null)
            {
                cmd.Connection = m_Connection;
            }

            if (m_Transaction != null)
            {
                cmd.Transaction = m_Transaction;
            }
            return cmd.ExecuteNonQuery();
        }
    }
}
