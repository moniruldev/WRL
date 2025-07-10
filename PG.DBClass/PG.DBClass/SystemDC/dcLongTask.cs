using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.SystemDC
{
    public enum LongTaskEnum
    {
        Undefined = 0,
        TaskByID = 1,
        SalaryProcess = 2,
        EMailSend = 3,
        IncomeTaxProcess = 4,
        SalayPFAmountPost = 5,
        PFProfitDistribute = 6,
    }

    public enum LongTaskStateEnum
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3,
        CancelledByUser = 4,  
        Puased = 5
    }

    [Serializable]
    public class dcLongTask
    {
        private LongTaskEnum m_Task = LongTaskEnum.Undefined;
        private string m_TaskID = string.Empty;
        private string m_TaskName = string.Empty;

        private LongTaskStateEnum m_TaskState = LongTaskStateEnum.NotStarted;

        private int m_TotalUnit = 0;
        private int m_CompletedUnit = 0;

        private int m_ProcessedUnit = 0;
        private int m_SuccessUnit = 0;
        private int m_FailedUnit = 0;


        private int m_TotalErrors = 0;
        private string m_LastError = string.Empty;

        private DateTime m_StartTime = DateTime.Now;
        private int m_RemainHour = 0;
        private int m_RemainMinuite = 0;
        private int m_RemainTotalMinuite = 0;
       
        private DateTime? m_CompleteTime = null;

        private bool m_IsCancelled = false;
        private DateTime? m_CancelTime = null;

        private string m_UserName = string.Empty;

        private Object m_TaskData = null;

        public string TaskID
        {
            get { return m_TaskID; }
            set 
            {
                lock (this)
                {
                    m_TaskID = value;
                }
            }
        }
        public LongTaskEnum Task
        {
            get { return m_Task; }
            set { m_Task = value; }
        }
        public LongTaskStateEnum TaskState
        {
            get { return m_TaskState; }
            set
            {
                lock (this)
                {
                    m_TaskState = value;
                }
            }
        }

        public string TaskName
        {
            get { return m_TaskName; }
            set { m_TaskName = value; }
        }

        public int TotalUnit
        {
            get { return m_TotalUnit; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Total unit value must be greater then zero!");
                }
                lock (this)
                {
                    m_TotalUnit = value;
                }
            }
        }

        public int CompletedUnit
        {
            get { return m_CompletedUnit; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Completed unit value must be greater then zero!");
                }

                if (value > m_TotalUnit)
                {
                    throw new ArgumentOutOfRangeException("Completed unit cannot set greater then Total unit!");
                }

                lock (this)
                {
                    m_CompletedUnit = value;
                }
            }
        }




        public int CompletedPercent
        {
            get 
            {
                int percent = 0;
                if (m_TotalUnit > 0 && m_CompletedUnit > 0)
                {
                    percent = Convert.ToInt32( (Convert.ToDecimal(m_CompletedUnit) / Convert.ToDecimal(m_TotalUnit)) * 100);
                }
                return percent; 
            }
        }

        public int ProcessedUnit
        {
            get { return m_ProcessedUnit; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Processed unit value must be greater then zero!");
                }

                if (value > m_TotalUnit)
                {
                    throw new ArgumentOutOfRangeException("Processed unit cannot set greater then Total unit!");
                }

                lock (this)
                {
                    m_ProcessedUnit = value;
                }
            }
        }


        public int SuccessUnit
        {
            get { return m_SuccessUnit; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Sucess unit value must be greater then zero!");
                }

                if (value > m_TotalUnit)
                {
                    throw new ArgumentOutOfRangeException("Sucess unit cannot set greater then Total unit!");
                }

                lock (this)
                {
                    m_SuccessUnit = value;
                }
            }
        }

        public int FailedUnit
        {
            get { return m_FailedUnit; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Failed unit value must be greater then zero!");
                }

                if (value > m_TotalUnit)
                {
                    throw new ArgumentOutOfRangeException("Failed unit cannot set greater then Total unit!");
                }

                lock (this)
                {
                    m_FailedUnit = value;
                }
            }
        }



        public int TotalErrors
        {
            get { return m_TotalErrors; }
            set 
            {
                lock (this)
                {
                    m_TotalErrors = value;
                }
            }
        }

        public string LastError
        {
            get { return m_LastError; }
            set { m_LastError = value; }
        }

        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }

        public int RemainHour
        {
            get { return m_RemainHour; }
            set { m_RemainHour = value; }
        }

        public int RemainMinuite
        {
            get { return m_RemainMinuite; }
            set { m_RemainMinuite = value; }
        }

        public int RemainTotalMinuite
        {
            get { return m_RemainTotalMinuite; }
            set { m_RemainTotalMinuite = value; }
        }

        public DateTime? CompleteTime
        {
            get { return m_CompleteTime; }
            set 
            {
                lock (this)
                {
                    m_CompleteTime = value;
                }
            }
        }

        public bool IsCancelled
        {
            get { return m_IsCancelled; }
            set
            {
                lock (this)
                {
                    m_IsCancelled = value;
                }
            }
        }



        public DateTime? CancelTime
        {
            get { return m_CancelTime; }
            set { m_CancelTime = value; }
        }


        public string UserName
        {
            get { return m_UserName; }
            set 
            {
                lock (this)
                {
                    m_UserName = value;
                }
            }
        }

        public Object TaskData
        {
            get { return m_TaskData; }
            set 
            {
                lock (this)
                {
                    m_TaskData = value;
                }
            }
        }

    }
}
