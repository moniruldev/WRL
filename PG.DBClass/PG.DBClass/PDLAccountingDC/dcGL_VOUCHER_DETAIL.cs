using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PG.Core.DBBase;

namespace PG.DBClass.PDLAccountingDC
{
    [DBTable(Name = "GL_VOUCHER_DETAIL")]
    public partial class dcGL_VOUCHER_DETAIL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_VC_NO = string.Empty;
        private string m_COA_CODE = string.Empty;
        private string m_RC_CODE = string.Empty;
        private string m_VD_DESCRIPTION = string.Empty;
        private string m_VD_REFERENCE = string.Empty;
        private decimal m_VD_UNPOSTED_DR_AMT = 0;
        private decimal m_VD_UNPOSTED_CR_AMT = 0;
        private decimal m_VD_ENTERED_DR_AMT = 0;
        private decimal m_VD_ENTERED_CR_AMT = 0;
        private decimal m_VD_POSTED_DR_AMT = 0;
        private decimal m_VD_POSTED_CR_AMT = 0;
        private string m_COA_SOURCE = string.Empty;
        private string m_SUP_CODE = string.Empty;
        private Int16 m_SLNO = 0;
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

        [DBColumn(Name = "COA_CODE", Storage = "m_COA_CODE", DbType = "126", IsPrimaryKey = true)]
        public string COA_CODE
        {
            get { return this.m_COA_CODE; }
            set
            {
                this.m_COA_CODE = value;
                this.NotifyPropertyChanged("COA_CODE");
            }
        }

        [DBColumn(Name = "RC_CODE", Storage = "m_RC_CODE", DbType = "126", IsPrimaryKey = true)]
        public string RC_CODE
        {
            get { return this.m_RC_CODE; }
            set
            {
                this.m_RC_CODE = value;
                this.NotifyPropertyChanged("RC_CODE");
            }
        }

        [DBColumn(Name = "VD_DESCRIPTION", Storage = "m_VD_DESCRIPTION", DbType = "126")]
        public string VD_DESCRIPTION
        {
            get { return this.m_VD_DESCRIPTION; }
            set
            {
                this.m_VD_DESCRIPTION = value;
                this.NotifyPropertyChanged("VD_DESCRIPTION");
            }
        }

        [DBColumn(Name = "VD_REFERENCE", Storage = "m_VD_REFERENCE", DbType = "126")]
        public string VD_REFERENCE
        {
            get { return this.m_VD_REFERENCE; }
            set
            {
                this.m_VD_REFERENCE = value;
                this.NotifyPropertyChanged("VD_REFERENCE");
            }
        }

        [DBColumn(Name = "VD_UNPOSTED_DR_AMT", Storage = "m_VD_UNPOSTED_DR_AMT", DbType = "107")]
        public decimal VD_UNPOSTED_DR_AMT
        {
            get { return this.m_VD_UNPOSTED_DR_AMT; }
            set
            {
                this.m_VD_UNPOSTED_DR_AMT = value;
                this.NotifyPropertyChanged("VD_UNPOSTED_DR_AMT");
            }
        }

        [DBColumn(Name = "VD_UNPOSTED_CR_AMT", Storage = "m_VD_UNPOSTED_CR_AMT", DbType = "107")]
        public decimal VD_UNPOSTED_CR_AMT
        {
            get { return this.m_VD_UNPOSTED_CR_AMT; }
            set
            {
                this.m_VD_UNPOSTED_CR_AMT = value;
                this.NotifyPropertyChanged("VD_UNPOSTED_CR_AMT");
            }
        }

        [DBColumn(Name = "VD_ENTERED_DR_AMT", Storage = "m_VD_ENTERED_DR_AMT", DbType = "107")]
        public decimal VD_ENTERED_DR_AMT
        {
            get { return this.m_VD_ENTERED_DR_AMT; }
            set
            {
                this.m_VD_ENTERED_DR_AMT = value;
                this.NotifyPropertyChanged("VD_ENTERED_DR_AMT");
            }
        }

        [DBColumn(Name = "VD_ENTERED_CR_AMT", Storage = "m_VD_ENTERED_CR_AMT", DbType = "107")]
        public decimal VD_ENTERED_CR_AMT
        {
            get { return this.m_VD_ENTERED_CR_AMT; }
            set
            {
                this.m_VD_ENTERED_CR_AMT = value;
                this.NotifyPropertyChanged("VD_ENTERED_CR_AMT");
            }
        }

        [DBColumn(Name = "VD_POSTED_DR_AMT", Storage = "m_VD_POSTED_DR_AMT", DbType = "107")]
        public decimal VD_POSTED_DR_AMT
        {
            get { return this.m_VD_POSTED_DR_AMT; }
            set
            {
                this.m_VD_POSTED_DR_AMT = value;
                this.NotifyPropertyChanged("VD_POSTED_DR_AMT");
            }
        }

        [DBColumn(Name = "VD_POSTED_CR_AMT", Storage = "m_VD_POSTED_CR_AMT", DbType = "107")]
        public decimal VD_POSTED_CR_AMT
        {
            get { return this.m_VD_POSTED_CR_AMT; }
            set
            {
                this.m_VD_POSTED_CR_AMT = value;
                this.NotifyPropertyChanged("VD_POSTED_CR_AMT");
            }
        }

        [DBColumn(Name = "COA_SOURCE", Storage = "m_COA_SOURCE", DbType = "126")]
        public string COA_SOURCE
        {
            get { return this.m_COA_SOURCE; }
            set
            {
                this.m_COA_SOURCE = value;
                this.NotifyPropertyChanged("COA_SOURCE");
            }
        }

        [DBColumn(Name = "SUP_CODE", Storage = "m_SUP_CODE", DbType = "104")]
        public string SUP_CODE
        {
            get { return this.m_SUP_CODE; }
            set
            {
                this.m_SUP_CODE = value;
                this.NotifyPropertyChanged("SUP_CODE");
            }
        }

        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "111", IsPrimaryKey = true)]
        public Int16 SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
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
