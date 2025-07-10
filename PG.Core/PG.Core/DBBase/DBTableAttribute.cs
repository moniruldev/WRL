using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBBase
{
    public class DBTableAttribute : Attribute
    {
        private string m_Name = string.Empty;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private string m_Schema = string.Empty;
        public string Schema
        {
            get { return m_Schema; }
            set { m_Schema = value; }
        }

        private string m_TableName = string.Empty;
        public string TableName
        {
            get { return m_TableName; }
            set { m_TableName = value; }
        }

        private string m_SequenceName = string.Empty;
        public string SequenceName
        {
            get { return m_SequenceName; }
            set { m_SequenceName = value; }
        }


        private bool m_LogChange = false;
        public bool LogChange
        {
            get { return m_LogChange; }
            set { m_LogChange = value; }
        }


        private string m_Comments = string.Empty;
        public string Comments
        {
            get { return m_Comments; }
            set { m_Comments = value; }
        }

        private bool m_IsDBTableLink = false;
        public bool IsDBTableLink
        {
            get { return m_IsDBTableLink; }
            set { m_IsDBTableLink = value; }
        }
    }
}
