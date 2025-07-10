using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBValidation
{
    [Serializable]
    public class DBValidationError
    {
        private int m_SLNo = 0;
        private string m_ErrorNo = string.Empty;
        private string m_ErrorString = string.Empty;
        private string m_BindingPropertyName = string.Empty;
        private string m_BindingControlID = string.Empty;
        private string m_BindingControlClientID = string.Empty;
        private string m_BindingControlName = string.Empty;
        
        private Exception m_ErrorException = null;


        public DBValidationError()
        {
            
            
        }

        public DBValidationError(string errorString)
        {
            this.m_ErrorString = errorString;
        }

        public DBValidationError(string errorString, string bindingPropertyName)
        {
            this.m_ErrorString = errorString;
            this.m_BindingPropertyName = bindingPropertyName;
        }


        public int SLNo
        {
            get {return m_SLNo;}
            set { m_SLNo = value; }
        }

        public string ErrorNo
        {
            get { return m_ErrorNo; }
            set { m_ErrorNo = value; }
        }


        public string ErrorString
        {
            get { return m_ErrorString; }
            set { m_ErrorString = value; }
        }

        public string BindingControlID
        {
            get { return m_BindingControlID; }
            set { m_BindingControlID = value; }
        }

        public string BindingControlClientID
        {
            get { return m_BindingControlClientID; }
            set { m_BindingControlID = value; }
        }

        public string BindingControlName
        {
            get { return m_BindingControlName; }
            set { m_BindingControlName = value; }
        }

        public Exception ErrorException
        {
            get { return m_ErrorException; }
            set { m_ErrorException = value; }
        }



    }
}
