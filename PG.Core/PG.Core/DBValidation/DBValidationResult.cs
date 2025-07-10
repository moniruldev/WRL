using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBValidation
{
    public class DBValidationResult
    {
        private bool m_IsError = false;
        private string m_ErrorString = string.Empty;
        private string m_TaskString = string.Empty;

        private bool m_StopContinue = false;
        //private bool m_IsDCInit = false;

        private DBValidationTask m_ValidationTask = null;

        private List<DBValidationError> m_ValidationErrorList = new List<DBValidationError>();

        public bool IsError
        {
            get { return m_IsError; }
            set { m_IsError = value; }
        }

        public string ErrorString
        {
            get { return m_ErrorString; }
            set { m_ErrorString = value; }
        }

        public string TaskString
        {
            get { return m_TaskString; }
            set { m_TaskString = value; }
        }

        public bool StopContinue
        {
            get { return m_StopContinue; }
            set { m_StopContinue = value; }
        }



        public DBValidationTask ValidationTask
        {
            get { return m_ValidationTask; }
            set { m_ValidationTask = value; }
        }

        public List<DBValidationError> ValidationErrorList
        {
            get { return m_ValidationErrorList; }
            set { m_ValidationErrorList = value; }
        }

        public bool CheckErrorReturn()
        {
            bool isReturn = false;
            if (this.m_StopContinue || this.m_ValidationTask.ValidateALL == false)
            {
                isReturn = true;
            }
            return isReturn;
        }

        public bool CheckErrorReturnDC(ref DBContext dc, bool pIsDCInit)
        {
            bool isReturn = false;
            if (this.m_StopContinue || this.m_ValidationTask.ValidateALL==false)
            {
                isReturn = true;
                DBContextManager.ReleaseDBContext(ref dc, pIsDCInit);
            }
            return isReturn;
        }

        public bool AddErrorCheckReturn(string errString)
        {
            bool isReturn = false;
            this.m_IsError = true;
            this.m_ErrorString = errString;
            this.ValidationErrorList.Add(new DBValidationError(errString));

            if (this.m_StopContinue || this.m_ValidationTask.ValidateALL == false)
            {
                isReturn = true;
            }
            return isReturn;
        }


        public bool AddErrorCheckReturnDC(string errString, ref DBContext dc, bool pIsDCInit)
        {
            bool isReturn = false;
            this.m_IsError = true;
            this.m_ErrorString = errString;
            this.ValidationErrorList.Add(new DBValidationError(errString));
                 
            if (this.m_StopContinue || this.m_ValidationTask.ValidateALL == false)
            {
                isReturn = true;
                DBContextManager.ReleaseDBContext(ref dc, pIsDCInit);
            }
            return isReturn;
        }
    }
}
