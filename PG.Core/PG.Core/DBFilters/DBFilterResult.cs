using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBFilters
{
    public class DBFilterResult
    {
        private string m_FilterString = string.Empty;
        private DBParameterInfoCollection m_DBParametersInfo = new DBParameterInfoCollection();

        public string FilterString
        { 
            get {return m_FilterString;}
            set { m_FilterString = value;} 
        }
        public DBParameterInfoCollection DBParametersInfo
        {
            get { return m_DBParametersInfo; }
            set { m_DBParametersInfo = value; }
        }
    }
}
