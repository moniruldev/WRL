using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "GRID_REJ_CONVERSION_MST")]
    public partial class dcGRID_REJ_CONVERSION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJ_CONVERSION_MST_ID = 0;
        private string m_CONVERSION_NO = string.Empty;
        private DateTime? m_CONVERSION_DATE = null;
        private int m_REJECTION_MST_ID = 0;
        private string m_REMARKS = string.Empty;
        private string m_IT_REMARKS = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_APPROVAL_STATUS = string.Empty;
        private int m_APPROVED_BY = 0;
        private DateTime? m_APPROVED_DATE = null;
        private string m_REJECTION_TYPE = string.Empty;
        private int m_DEPT_ID = 0;
        private int m_STLM_ID = 0;

        #endregion  //private members

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            _UpdateChangedList(info);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion //public events

        #region properties


        [DBColumn(Name = "REJ_CONVERSION_MST_ID", Storage = "m_REJ_CONVERSION_MST_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJ_CONVERSION_MST_ID
        {
            get { return this.m_REJ_CONVERSION_MST_ID; }
            set
            {
                this.m_REJ_CONVERSION_MST_ID = value;
                this.NotifyPropertyChanged("REJ_CONVERSION_MST_ID");
            }
        }

        [DBColumn(Name = "CONVERSION_NO", Storage = "m_CONVERSION_NO", DbType = "126")]
        public string CONVERSION_NO
        {
            get { return this.m_CONVERSION_NO; }
            set
            {
                this.m_CONVERSION_NO = value;
                this.NotifyPropertyChanged("CONVERSION_NO");
            }
        }

        [DBColumn(Name = "CONVERSION_DATE", Storage = "m_CONVERSION_DATE", DbType = "106")]
        public DateTime? CONVERSION_DATE
        {
            get { return this.m_CONVERSION_DATE; }
            set
            {
                this.m_CONVERSION_DATE = value;
                this.NotifyPropertyChanged("CONVERSION_DATE");
            }
        }

        [DBColumn(Name = "REJECTION_MST_ID", Storage = "m_REJECTION_MST_ID", DbType = "107")]
        public int REJECTION_MST_ID
        {
            get { return this.m_REJECTION_MST_ID; }
            set
            {
                this.m_REJECTION_MST_ID = value;
                this.NotifyPropertyChanged("REJECTION_MST_ID");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }

        [DBColumn(Name = "IT_REMARKS", Storage = "m_IT_REMARKS", DbType = "126")]
        public string IT_REMARKS
        {
            get { return this.m_IT_REMARKS; }
            set
            {
                this.m_IT_REMARKS = value;
                this.NotifyPropertyChanged("IT_REMARKS");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "APPROVAL_STATUS", Storage = "m_APPROVAL_STATUS", DbType = "126")]
        public string APPROVAL_STATUS
        {
            get { return this.m_APPROVAL_STATUS; }
            set
            {
                this.m_APPROVAL_STATUS = value;
                this.NotifyPropertyChanged("APPROVAL_STATUS");
            }
        }

        [DBColumn(Name = "APPROVED_BY", Storage = "m_APPROVED_BY", DbType = "107")]
        public int APPROVED_BY
        {
            get { return this.m_APPROVED_BY; }
            set
            {
                this.m_APPROVED_BY = value;
                this.NotifyPropertyChanged("APPROVED_BY");
            }
        }

        [DBColumn(Name = "APPROVED_DATE", Storage = "m_APPROVED_DATE", DbType = "106")]
        public DateTime? APPROVED_DATE
        {
            get { return this.m_APPROVED_DATE; }
            set
            {
                this.m_APPROVED_DATE = value;
                this.NotifyPropertyChanged("APPROVED_DATE");
            }
        }

        [DBColumn(Name = "REJECTION_TYPE", Storage = "m_REJECTION_TYPE", DbType = "126")]
        public string REJECTION_TYPE
        {
            get { return this.m_REJECTION_TYPE; }
            set
            {
                this.m_REJECTION_TYPE = value;
                this.NotifyPropertyChanged("REJECTION_TYPE");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "107")]
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        #endregion //properties
    }

    public partial class dcGRID_REJ_CONVERSION_MST
    {
       public List<dcGRID_REJ_CONVERSION_DTL> ConversionDtlList = new List<dcGRID_REJ_CONVERSION_DTL>();
       public string STLM_NAME { get; set; }
       public string DEPT_NAME { get; set; }
       public string PROD_REJECTION_NO { get; set; }
    }
}
