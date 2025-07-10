using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "RECEIVE_CHEQUE")]
    public partial class dcRECEIVE_CHEQUE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RCV_CHEQUE_ID = 0;
        private decimal m_RECEIVE_ID = 0;
        private decimal m_BANK_ID = 0;
        private decimal m_BRANCH_ID = 0;
        private decimal m_ACC_HOLDER_DTL_ID = 0;
        private string m_CHEQUE_NO = string.Empty;
        private DateTime? m_CHEQUE_DATE = null;
        private decimal m_CHEQUE_AMT = 0;
        private string m_CHEQUE_STATUS = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private string m_CREATE_DATE = string.Empty;
        private string m_UPDATE_BY = string.Empty;
        private string m_UPDATE_DATE = string.Empty;
        private string m_REC_STATUS = string.Empty;
        private string m_VFLAG = string.Empty;
        private string m_VPOST = string.Empty;
        private decimal m_CHEQUE_AMT_VAT = 0;
        private string m_ACC_NO = string.Empty;
        private decimal m_CHE_ALTU = 0;
        private decimal m_ACTUAL_CHEQUE_AMT = 0;

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


        [DBColumn(Name = "RCV_CHEQUE_ID", Storage = "m_RCV_CHEQUE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RCV_CHEQUE_ID
        {
            get { return this.m_RCV_CHEQUE_ID; }
            set
            {
                this.m_RCV_CHEQUE_ID = value;
                this.NotifyPropertyChanged("RCV_CHEQUE_ID");
            }
        }

        [DBColumn(Name = "RECEIVE_ID", Storage = "m_RECEIVE_ID", DbType = "107")]
        public decimal RECEIVE_ID
        {
            get { return this.m_RECEIVE_ID; }
            set
            {
                this.m_RECEIVE_ID = value;
                this.NotifyPropertyChanged("RECEIVE_ID");
            }
        }

        [DBColumn(Name = "BANK_ID", Storage = "m_BANK_ID", DbType = "107")]
        public decimal BANK_ID
        {
            get { return this.m_BANK_ID; }
            set
            {
                this.m_BANK_ID = value;
                this.NotifyPropertyChanged("BANK_ID");
            }
        }

        [DBColumn(Name = "BRANCH_ID", Storage = "m_BRANCH_ID", DbType = "107")]
        public decimal BRANCH_ID
        {
            get { return this.m_BRANCH_ID; }
            set
            {
                this.m_BRANCH_ID = value;
                this.NotifyPropertyChanged("BRANCH_ID");
            }
        }

        [DBColumn(Name = "ACC_HOLDER_DTL_ID", Storage = "m_ACC_HOLDER_DTL_ID", DbType = "107")]
        public decimal ACC_HOLDER_DTL_ID
        {
            get { return this.m_ACC_HOLDER_DTL_ID; }
            set
            {
                this.m_ACC_HOLDER_DTL_ID = value;
                this.NotifyPropertyChanged("ACC_HOLDER_DTL_ID");
            }
        }

        [DBColumn(Name = "CHEQUE_NO", Storage = "m_CHEQUE_NO", DbType = "126")]
        public string CHEQUE_NO
        {
            get { return this.m_CHEQUE_NO; }
            set
            {
                this.m_CHEQUE_NO = value;
                this.NotifyPropertyChanged("CHEQUE_NO");
            }
        }

        [DBColumn(Name = "CHEQUE_DATE", Storage = "m_CHEQUE_DATE", DbType = "106")]
        public DateTime? CHEQUE_DATE
        {
            get { return this.m_CHEQUE_DATE; }
            set
            {
                this.m_CHEQUE_DATE = value;
                this.NotifyPropertyChanged("CHEQUE_DATE");
            }
        }

        [DBColumn(Name = "CHEQUE_AMT", Storage = "m_CHEQUE_AMT", DbType = "107")]
        public decimal CHEQUE_AMT
        {
            get { return this.m_CHEQUE_AMT; }
            set
            {
                this.m_CHEQUE_AMT = value;
                this.NotifyPropertyChanged("CHEQUE_AMT");
            }
        }

        [DBColumn(Name = "CHEQUE_STATUS", Storage = "m_CHEQUE_STATUS", DbType = "126")]
        public string CHEQUE_STATUS
        {
            get { return this.m_CHEQUE_STATUS; }
            set
            {
                this.m_CHEQUE_STATUS = value;
                this.NotifyPropertyChanged("CHEQUE_STATUS");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "126")]
        public string CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "126")]
        public string UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "REC_STATUS", Storage = "m_REC_STATUS", DbType = "126")]
        public string REC_STATUS
        {
            get { return this.m_REC_STATUS; }
            set
            {
                this.m_REC_STATUS = value;
                this.NotifyPropertyChanged("REC_STATUS");
            }
        }

        [DBColumn(Name = "VFLAG", Storage = "m_VFLAG", DbType = "126")]
        public string VFLAG
        {
            get { return this.m_VFLAG; }
            set
            {
                this.m_VFLAG = value;
                this.NotifyPropertyChanged("VFLAG");
            }
        }

        [DBColumn(Name = "VPOST", Storage = "m_VPOST", DbType = "126")]
        public string VPOST
        {
            get { return this.m_VPOST; }
            set
            {
                this.m_VPOST = value;
                this.NotifyPropertyChanged("VPOST");
            }
        }

        [DBColumn(Name = "CHEQUE_AMT_VAT", Storage = "m_CHEQUE_AMT_VAT", DbType = "107")]
        public decimal CHEQUE_AMT_VAT
        {
            get { return this.m_CHEQUE_AMT_VAT; }
            set
            {
                this.m_CHEQUE_AMT_VAT = value;
                this.NotifyPropertyChanged("CHEQUE_AMT_VAT");
            }
        }

        [DBColumn(Name = "ACC_NO", Storage = "m_ACC_NO", DbType = "126")]
        public string ACC_NO
        {
            get { return this.m_ACC_NO; }
            set
            {
                this.m_ACC_NO = value;
                this.NotifyPropertyChanged("ACC_NO");
            }
        }

        [DBColumn(Name = "CHE_ALTU", Storage = "m_CHE_ALTU", DbType = "107")]
        public decimal CHE_ALTU
        {
            get { return this.m_CHE_ALTU; }
            set
            {
                this.m_CHE_ALTU = value;
                this.NotifyPropertyChanged("CHE_ALTU");
            }
        }

        [DBColumn(Name = "ACTUAL_CHEQUE_AMT", Storage = "m_ACTUAL_CHEQUE_AMT", DbType = "107")]
        public decimal ACTUAL_CHEQUE_AMT
        {
            get { return this.m_ACTUAL_CHEQUE_AMT; }
            set
            {
                this.m_ACTUAL_CHEQUE_AMT = value;
                this.NotifyPropertyChanged("ACTUAL_CHEQUE_AMT");
            }
        }

        #endregion //properties
    }
}
