using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBBase
{

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    [Serializable]
    public class DBColumnAttribute : Attribute
    {
        private string m_Name = string.Empty;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private string m_Storage = string.Empty;
        public string Storage
        {
            get { return m_Storage; }
            set { m_Storage = value; }
        }

        private string m_Schema = string.Empty;
        public string Schema
        {
            get { return m_Schema; }
            set { m_Schema = value; }
        }

        private string m_DbType = string.Empty;
        public string DbType
        {
            get { return m_DbType; }
            set { m_DbType = value; }
        }

        private string m_DbTypeGeneric = string.Empty;
        public string DbTypeGeneric
        {
            get { return m_DbTypeGeneric; }
            set { m_DbTypeGeneric = value; }
        }


        private string m_DbTypeSQL = string.Empty;
        public string DbTypeSQL
        {
            get { return m_DbTypeSQL; }
            set { m_DbTypeSQL = value; }
        }

        private string m_DbTypeOracle = string.Empty;
        public string DbTypeOracle
        {
            get { return m_DbTypeOracle; }
            set { m_DbTypeOracle = value; }
        }


        private int m_Length = 0;
        public int Length
        {
            get { return m_Length; }
            set { m_Length = value; }
        }

        private int m_Precision = 0;
        public int Precision
        {
            get { return m_Precision; }
            set { m_Precision = value; }
        }

        private int m_Scale = 0;
        public int Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }

        private bool m_Unicode= false;
        public bool Unicode
        {
            get { return m_Unicode; }
            set { m_Unicode = value; }
        }

        private bool m_IsPrimaryKey = false;
        public bool IsPrimaryKey
        {
            get { return m_IsPrimaryKey; }
            set { m_IsPrimaryKey = value; }
        }

        private bool m_IsDbGenerated = false;
        public bool IsDbGenerated
        {
            get { return m_IsDbGenerated; }
            set { m_IsDbGenerated = value; }
        }

        private bool m_IsIdentity = false;
        public bool IsIdentity
        {
            get { return m_IsIdentity; }
            set { m_IsIdentity = value; }
        }

        private bool m_RowGuid = false;
        public bool RowGuid
        {
            get { return m_RowGuid; }
            set { m_RowGuid = value; }
        }

        private bool m_Nullable = false;
        public bool Nullable
        {
            get { return m_Nullable; }
            set { m_Nullable = value; }
        }

        private bool m_SyncOnInsert = false;
        public bool SyncOnInsert
        {
            get { return m_SyncOnInsert; }
            set { m_SyncOnInsert = value; }
        }

        private bool m_SyncOnUpdate = false;
        public bool SyncOnUpdate
        {
            get { return m_SyncOnUpdate; }
            set { m_SyncOnUpdate = value; }
        }

        private string m_SequenceName = string.Empty;
        public string SequenceName
        {
            get { return m_SequenceName; }
            set { m_SequenceName = value; }
        }

        private int m_SLNo = 0;
        public int SLNo
        {
            get { return m_SLNo; }
            set { m_SLNo = value; }
        }

        private string m_Comments = string.Empty;
        public string Comments
        {
            get { return m_Comments; }
            set { m_Comments = value; }
        }

        private string m_DefaultValue = string.Empty;
        public string DefaultValue
        {
            get { return m_DefaultValue; }
            set { m_DefaultValue = value; }
        }


        private bool m_LogChange = false;
        public bool LogChange
        {
            get { return m_LogChange; }
            set { m_LogChange = value; }
        }

        public DBColumnAttribute()
        {

        }
    }
}
