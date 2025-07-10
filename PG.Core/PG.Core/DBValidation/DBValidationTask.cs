using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBValidation
{
    public class DBValidationTask
    {
        private int m_DBValidationType = 0;
        private bool m_ValidateALL = false;
        private bool m_FromWebUI = false;

        public int DBValidationType
        {
            get { return m_DBValidationType; }
            set { m_DBValidationType = value; }
        }

        public bool FromWebUI
        {
            get { return m_FromWebUI; }
            set { m_FromWebUI = value; }
        }

        public bool ValidateALL
        {
            get { return m_ValidateALL; }
            set { m_ValidateALL = value; }
        }

    }

}
