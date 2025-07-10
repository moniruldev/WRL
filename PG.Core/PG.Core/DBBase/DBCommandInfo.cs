using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace PG.Core.DBBase
{
    public class DBCommandInfo
    {
        private CommandType m_CommandType = CommandType.Text;
        private DbConnection m_DBConnection = null;
        private DbTransaction m_DBTransaction = null;
        private DBParameterInfoCollection m_DBParametersInfo = new DBParameterInfoCollection();
        private string m_CommandText = string.Empty;
        private int m_CommandTimeout = 30;

        public CommandType CommandType
        {
            get { return m_CommandType; }
            set { m_CommandType = value; }
        }

        public DbConnection DBConnection
        {
            get { return m_DBConnection; }
            set { m_DBConnection = value; }
        }

        public DbTransaction DBTransaction
        {
            get { return m_DBTransaction; }
            set { m_DBTransaction = value; }
        }

        public string CommandText
        {
            get { return m_CommandText; }
            set { m_CommandText = value; }
        }
        public int CommandTimeout
        {
            get { return m_CommandTimeout; }
            set { m_CommandTimeout = value; }
        }



        public DBParameterInfoCollection DBParametersInfo
        {
            get { return m_DBParametersInfo; }
            set { m_DBParametersInfo = value; }
        }

    }
}
