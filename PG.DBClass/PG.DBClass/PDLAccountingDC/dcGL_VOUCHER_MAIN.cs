using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PG.Core.DBBase;

namespace PG.DBClass.PDLAccountingDC
{
    [DBTable(Name = "GL_VOUCHER_MAIN")]
    public partial class dcGL_VOUCHER_MAIN : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_VC_NO = string.Empty;
        private DateTime? m_VC_DATE = null;
        private DateTime? m_VC_ENTERED = null;
        private string m_VJ_CODE = string.Empty;
        private string m_VC_ENTERED_BY = string.Empty;
        private string m_VC_POSTED_BY = string.Empty;
        private string m_VC_RECEIVED_BY = string.Empty;
        private string m_VC_PREPARED_BY = string.Empty;
        private string m_VC_CHECKED_BY = string.Empty;
        private string m_VC_APPROVED_BY = string.Empty;
        private DateTime? m_VC_POSTED_DATE = null;
        private string m_CUR_CODE = string.Empty;
        private double m_VC_CONV_RATE = 0;
        private string m_VC_IS_POSTED = string.Empty;
        private string m_PAYEE_CODE = string.Empty;
        private string m_IS_CHEQUE = string.Empty;
        private string m_PM_CODE = string.Empty;
        private string m_VC_LETTER_REF_NO = string.Empty;
        private string m_IS_SYSTEM = string.Empty;
        private string m_COMP_ID = string.Empty;

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


        [DBColumn(Name = "VC_NO", Storage = "m_VC_NO", DbType = "126", IsPrimaryKey = true)]
        public string VC_NO
        {
            get { return this.m_VC_NO; }
            set
            {
                this.m_VC_NO = value;
                this.NotifyPropertyChanged("VC_NO");
            }
        }

        [DBColumn(Name = "VC_DATE", Storage = "m_VC_DATE", DbType = "106")]
        public DateTime? VC_DATE
        {
            get { return this.m_VC_DATE; }
            set
            {
                this.m_VC_DATE = value;
                this.NotifyPropertyChanged("VC_DATE");
            }
        }

        [DBColumn(Name = "VC_ENTERED", Storage = "m_VC_ENTERED", DbType = "106")]
        public DateTime? VC_ENTERED
        {
            get { return this.m_VC_ENTERED; }
            set
            {
                this.m_VC_ENTERED = value;
                this.NotifyPropertyChanged("VC_ENTERED");
            }
        }

        [DBColumn(Name = "VJ_CODE", Storage = "m_VJ_CODE", DbType = "126")]
        public string VJ_CODE
        {
            get { return this.m_VJ_CODE; }
            set
            {
                this.m_VJ_CODE = value;
                this.NotifyPropertyChanged("VJ_CODE");
            }
        }

        [DBColumn(Name = "VC_ENTERED_BY", Storage = "m_VC_ENTERED_BY", DbType = "126")]
        public string VC_ENTERED_BY
        {
            get { return this.m_VC_ENTERED_BY; }
            set
            {
                this.m_VC_ENTERED_BY = value;
                this.NotifyPropertyChanged("VC_ENTERED_BY");
            }
        }

        [DBColumn(Name = "VC_POSTED_BY", Storage = "m_VC_POSTED_BY", DbType = "126")]
        public string VC_POSTED_BY
        {
            get { return this.m_VC_POSTED_BY; }
            set
            {
                this.m_VC_POSTED_BY = value;
                this.NotifyPropertyChanged("VC_POSTED_BY");
            }
        }

        [DBColumn(Name = "VC_RECEIVED_BY", Storage = "m_VC_RECEIVED_BY", DbType = "126")]
        public string VC_RECEIVED_BY
        {
            get { return this.m_VC_RECEIVED_BY; }
            set
            {
                this.m_VC_RECEIVED_BY = value;
                this.NotifyPropertyChanged("VC_RECEIVED_BY");
            }
        }

        [DBColumn(Name = "VC_PREPARED_BY", Storage = "m_VC_PREPARED_BY", DbType = "126")]
        public string VC_PREPARED_BY
        {
            get { return this.m_VC_PREPARED_BY; }
            set
            {
                this.m_VC_PREPARED_BY = value;
                this.NotifyPropertyChanged("VC_PREPARED_BY");
            }
        }

        [DBColumn(Name = "VC_CHECKED_BY", Storage = "m_VC_CHECKED_BY", DbType = "126")]
        public string VC_CHECKED_BY
        {
            get { return this.m_VC_CHECKED_BY; }
            set
            {
                this.m_VC_CHECKED_BY = value;
                this.NotifyPropertyChanged("VC_CHECKED_BY");
            }
        }

        [DBColumn(Name = "VC_APPROVED_BY", Storage = "m_VC_APPROVED_BY", DbType = "126")]
        public string VC_APPROVED_BY
        {
            get { return this.m_VC_APPROVED_BY; }
            set
            {
                this.m_VC_APPROVED_BY = value;
                this.NotifyPropertyChanged("VC_APPROVED_BY");
            }
        }

        [DBColumn(Name = "VC_POSTED_DATE", Storage = "m_VC_POSTED_DATE", DbType = "106")]
        public DateTime? VC_POSTED_DATE
        {
            get { return this.m_VC_POSTED_DATE; }
            set
            {
                this.m_VC_POSTED_DATE = value;
                this.NotifyPropertyChanged("VC_POSTED_DATE");
            }
        }

        [DBColumn(Name = "CUR_CODE", Storage = "m_CUR_CODE", DbType = "126")]
        public string CUR_CODE
        {
            get { return this.m_CUR_CODE; }
            set
            {
                this.m_CUR_CODE = value;
                this.NotifyPropertyChanged("CUR_CODE");
            }
        }

        [DBColumn(Name = "VC_CONV_RATE", Storage = "m_VC_CONV_RATE", DbType = "108")]
        public double VC_CONV_RATE
        {
            get { return this.m_VC_CONV_RATE; }
            set
            {
                this.m_VC_CONV_RATE = value;
                this.NotifyPropertyChanged("VC_CONV_RATE");
            }
        }

        [DBColumn(Name = "VC_IS_POSTED", Storage = "m_VC_IS_POSTED", DbType = "126")]
        public string VC_IS_POSTED
        {
            get { return this.m_VC_IS_POSTED; }
            set
            {
                this.m_VC_IS_POSTED = value;
                this.NotifyPropertyChanged("VC_IS_POSTED");
            }
        }

        [DBColumn(Name = "PAYEE_CODE", Storage = "m_PAYEE_CODE", DbType = "126")]
        public string PAYEE_CODE
        {
            get { return this.m_PAYEE_CODE; }
            set
            {
                this.m_PAYEE_CODE = value;
                this.NotifyPropertyChanged("PAYEE_CODE");
            }
        }

        [DBColumn(Name = "IS_CHEQUE", Storage = "m_IS_CHEQUE", DbType = "126")]
        public string IS_CHEQUE
        {
            get { return this.m_IS_CHEQUE; }
            set
            {
                this.m_IS_CHEQUE = value;
                this.NotifyPropertyChanged("IS_CHEQUE");
            }
        }

        [DBColumn(Name = "PM_CODE", Storage = "m_PM_CODE", DbType = "126")]
        public string PM_CODE
        {
            get { return this.m_PM_CODE; }
            set
            {
                this.m_PM_CODE = value;
                this.NotifyPropertyChanged("PM_CODE");
            }
        }

        [DBColumn(Name = "VC_LETTER_REF_NO", Storage = "m_VC_LETTER_REF_NO", DbType = "126")]
        public string VC_LETTER_REF_NO
        {
            get { return this.m_VC_LETTER_REF_NO; }
            set
            {
                this.m_VC_LETTER_REF_NO = value;
                this.NotifyPropertyChanged("VC_LETTER_REF_NO");
            }
        }

        [DBColumn(Name = "IS_SYSTEM", Storage = "m_IS_SYSTEM", DbType = "126")]
        public string IS_SYSTEM
        {
            get { return this.m_IS_SYSTEM; }
            set
            {
                this.m_IS_SYSTEM = value;
                this.NotifyPropertyChanged("IS_SYSTEM");
            }
        }

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "126", IsPrimaryKey = true)]
        public string COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
            }
        }

        #endregion //properties
    }
}
