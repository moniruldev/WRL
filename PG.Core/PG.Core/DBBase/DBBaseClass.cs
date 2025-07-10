using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Data.Linq;
using PG.Core.DBValidation;

namespace PG.Core.DBBase
{
    public enum RecordStateEnum
    {
        Read = 0,
        Added = 1,
        Edited = 2,
        Deleted = 3
    }

    [CLSCompliant(false)]
    [Serializable]
    public abstract partial class DBBaseClass
    {
        private RecordStateEnum m_RecordState = RecordStateEnum.Read;

        private List<string> m_ChangedList = new List<string>();
        private List<DBValidationError> m_ValidationErrorList = new List<DBValidationError>();

        //private Dictionary<string, string> m_TextValues = new Dictionary<string, string>();

        private NameValueCollection m_TextValues = new NameValueCollection();



        private bool m_IsInsert = false;
        private bool m_IsReturnIdentity = false;
        private bool m_IsAllField = false;

        private bool m_CheckDataValidation = false;
        private bool m_IsDataValid = false;


        public RecordStateEnum _RecordState
        {
            get { return m_RecordState; }
            set { m_RecordState = value; }
        }

        public int _RecordStateInt
        {
            get { return (int)m_RecordState; }
            set { m_RecordState = (RecordStateEnum)value; }
        }


        public bool _IsInsert
        {
            get { return m_IsInsert; }
            set { m_IsInsert = value; }
        }

        public bool _IsReturnIdentity
        {
            get { return m_IsReturnIdentity; }
            set { m_IsReturnIdentity = value; }
        }

        public bool _IsAllField
        {
            get { return m_IsAllField; }
            set { m_IsAllField = value; }
        }
        
        public List<string> _ChangedList
        {
            get { return m_ChangedList; }
        }

        protected void _UpdateChangedList(string propName)
        {
            
            if (m_ChangedList == null)
            {
                m_ChangedList = new List<string>();
            }

            if (!m_ChangedList.Contains(propName))
            {
                m_ChangedList.Add(propName);
            }
        }

        protected void _RemoveChangedList(string propName)
        {
            if (m_ChangedList != null)
            {
                if (!m_ChangedList.Contains(propName))
                {
                    m_ChangedList.Remove(propName);
                }
            }
        }

        public void _ClearChangedList()
        {
            m_ChangedList.Clear();
        }

        public bool _CheckDataValidation
        {
            get { return m_CheckDataValidation; }
            set { m_CheckDataValidation = value; }
        }

        public bool _IsDataValid
        {
            get { return m_IsDataValid; }
            set { m_IsDataValid = value; }
        }

        public List<DBValidationError> _ValidationErrorList
        {
            get { return m_ValidationErrorList; }
            set { m_ValidationErrorList = value; }
        }

        public DBBaseClass()
        {
            if (Internals.IsLicenseValidate)
            {
                License.AppLicense.ValidateLicense();
            }
        }

        public void ClearValidationError()
        {
            m_ValidationErrorList.Clear();
            m_IsDataValid = true;
        }

        public DBValidationError _AddValidationError(string errString)
        {
            return _AddValidationError(errString, string.Empty);
        }

        public DBValidationError _AddValidationError(string errString, string errNo)
        {
            DBValidationError valError = new DBValidationError();
            valError.SLNo = m_ValidationErrorList.Count + 1;
            valError.ErrorString = errString;
            valError.ErrorNo = errNo;
            m_ValidationErrorList.Add(valError);
            return valError;
        }


    }
}
