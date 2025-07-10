using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBBase
{
    public class dcDataSelection
    {

        private string m_ID = string.Empty;
        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
       
        private string m_Desc = string.Empty;
        public string Desc
        {
            get { return m_Desc; }
            set { m_Desc = value; }
        }
    }
}
