using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "RECEIVE_MR_MST")]
    public partial class dcRECEIVE_MR_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RECEIVE_ID = 0;
        private string m_RECEIVE_NO = string.Empty;
        private DateTime m_RECEIVE_DATE = DateTime.Now;
        private int m_CUST_ID = 0;
        private decimal m_SE_ID = 0;
        private string m_REF_NO = string.Empty;
        private double m_RECV_AMT = 0;
        private string m_PAYMENT_MODE = string.Empty;
        private decimal m_DEPT_ID = 0;
        private string m_LOC_CODE = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private string m_CREATE_DATE = string.Empty;
        private string m_UPDATE_BY = string.Empty;
        private string m_UPDATE_DATE = string.Empty;
        private string m_AUTHO_BY = string.Empty;
        private string m_AUTHO_DATE = string.Empty;
        private string m_AUTHO_STATUS = string.Empty;
        private string m_REC_STATUS = string.Empty;
        private string m_REMARKS = string.Empty;
        private string m_CHALLAN_STATUS = string.Empty;
        private string m_CHALLAN_STATUS_BY = string.Empty;
        private decimal m_ADJUSTED_AMT = 0;
        private string m_FULL_ADJUSTED = string.Empty;
        private string m_VFLAG = string.Empty;
        private string m_VPOST = string.Empty;
        private double m_RECV_AMT_VAT = 0;
        private string m_BPOST = string.Empty;
        private string m_PAID_FOR = string.Empty;
        private string m_ACC_RCV_VC_NO = string.Empty;
        private string m_UPDATE_STATUS = string.Empty;
        private string m_PAYMENT_VC_NO = string.Empty;
        private string m_CASH_MEDIA = string.Empty;
        private DateTime? m_RECEIVE_TIME = null;
        private decimal m_ITEM_SEGMENT_ID = 0;
        private string m_DEPOSITED_BY = string.Empty;
        private decimal m_ACTUAL_RCV_AMT = 0;
        private string m_IT_REMARKS = string.Empty;

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


        [DBColumn(Name = "RECEIVE_ID", Storage = "m_RECEIVE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RECEIVE_ID
        {
            get { return this.m_RECEIVE_ID; }
            set
            {
                this.m_RECEIVE_ID = value;
                this.NotifyPropertyChanged("RECEIVE_ID");
            }
        }

        [DBColumn(Name = "RECEIVE_NO", Storage = "m_RECEIVE_NO", DbType = "126")]
        public string RECEIVE_NO
        {
            get { return this.m_RECEIVE_NO; }
            set
            {
                this.m_RECEIVE_NO = value;
                this.NotifyPropertyChanged("RECEIVE_NO");
            }
        }

        [DBColumn(Name = "RECEIVE_DATE", Storage = "m_RECEIVE_DATE", DbType = "106")]
        public DateTime RECEIVE_DATE
        {
            get { return this.m_RECEIVE_DATE; }
            set
            {
                this.m_RECEIVE_DATE = value;
                this.NotifyPropertyChanged("RECEIVE_DATE");
            }
        }

        [DBColumn(Name = "CUST_ID", Storage = "m_CUST_ID", DbType = "107")]
        public int CUST_ID
        {
            get { return this.m_CUST_ID; }
            set
            {
                this.m_CUST_ID = value;
                this.NotifyPropertyChanged("CUST_ID");
            }
        }

        [DBColumn(Name = "SE_ID", Storage = "m_SE_ID", DbType = "107")]
        public decimal SE_ID
        {
            get { return this.m_SE_ID; }
            set
            {
                this.m_SE_ID = value;
                this.NotifyPropertyChanged("SE_ID");
            }
        }

        [DBColumn(Name = "REF_NO", Storage = "m_REF_NO", DbType = "126")]
        public string REF_NO
        {
            get { return this.m_REF_NO; }
            set
            {
                this.m_REF_NO = value;
                this.NotifyPropertyChanged("REF_NO");
            }
        }

        [DBColumn(Name = "RECV_AMT", Storage = "m_RECV_AMT", DbType = "108")]
        public double RECV_AMT
        {
            get { return this.m_RECV_AMT; }
            set
            {
                this.m_RECV_AMT = value;
                this.NotifyPropertyChanged("RECV_AMT");
            }
        }

        [DBColumn(Name = "PAYMENT_MODE", Storage = "m_PAYMENT_MODE", DbType = "126")]
        public string PAYMENT_MODE
        {
            get { return this.m_PAYMENT_MODE; }
            set
            {
                this.m_PAYMENT_MODE = value;
                this.NotifyPropertyChanged("PAYMENT_MODE");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public decimal DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "LOC_CODE", Storage = "m_LOC_CODE", DbType = "126")]
        public string LOC_CODE
        {
            get { return this.m_LOC_CODE; }
            set
            {
                this.m_LOC_CODE = value;
                this.NotifyPropertyChanged("LOC_CODE");
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

        [DBColumn(Name = "AUTHO_BY", Storage = "m_AUTHO_BY", DbType = "126")]
        public string AUTHO_BY
        {
            get { return this.m_AUTHO_BY; }
            set
            {
                this.m_AUTHO_BY = value;
                this.NotifyPropertyChanged("AUTHO_BY");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "126")]
        public string AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
            }
        }

        [DBColumn(Name = "AUTHO_STATUS", Storage = "m_AUTHO_STATUS", DbType = "126")]
        public string AUTHO_STATUS
        {
            get { return this.m_AUTHO_STATUS; }
            set
            {
                this.m_AUTHO_STATUS = value;
                this.NotifyPropertyChanged("AUTHO_STATUS");
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

        [DBColumn(Name = "CHALLAN_STATUS", Storage = "m_CHALLAN_STATUS", DbType = "126")]
        public string CHALLAN_STATUS
        {
            get { return this.m_CHALLAN_STATUS; }
            set
            {
                this.m_CHALLAN_STATUS = value;
                this.NotifyPropertyChanged("CHALLAN_STATUS");
            }
        }

        [DBColumn(Name = "CHALLAN_STATUS_BY", Storage = "m_CHALLAN_STATUS_BY", DbType = "126")]
        public string CHALLAN_STATUS_BY
        {
            get { return this.m_CHALLAN_STATUS_BY; }
            set
            {
                this.m_CHALLAN_STATUS_BY = value;
                this.NotifyPropertyChanged("CHALLAN_STATUS_BY");
            }
        }

        [DBColumn(Name = "ADJUSTED_AMT", Storage = "m_ADJUSTED_AMT", DbType = "107")]
        public decimal ADJUSTED_AMT
        {
            get { return this.m_ADJUSTED_AMT; }
            set
            {
                this.m_ADJUSTED_AMT = value;
                this.NotifyPropertyChanged("ADJUSTED_AMT");
            }
        }

        [DBColumn(Name = "FULL_ADJUSTED", Storage = "m_FULL_ADJUSTED", DbType = "126")]
        public string FULL_ADJUSTED
        {
            get { return this.m_FULL_ADJUSTED; }
            set
            {
                this.m_FULL_ADJUSTED = value;
                this.NotifyPropertyChanged("FULL_ADJUSTED");
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

        [DBColumn(Name = "RECV_AMT_VAT", Storage = "m_RECV_AMT_VAT", DbType = "108")]
        public double RECV_AMT_VAT
        {
            get { return this.m_RECV_AMT_VAT; }
            set
            {
                this.m_RECV_AMT_VAT = value;
                this.NotifyPropertyChanged("RECV_AMT_VAT");
            }
        }

        [DBColumn(Name = "BPOST", Storage = "m_BPOST", DbType = "126")]
        public string BPOST
        {
            get { return this.m_BPOST; }
            set
            {
                this.m_BPOST = value;
                this.NotifyPropertyChanged("BPOST");
            }
        }

        [DBColumn(Name = "PAID_FOR", Storage = "m_PAID_FOR", DbType = "126")]
        public string PAID_FOR
        {
            get { return this.m_PAID_FOR; }
            set
            {
                this.m_PAID_FOR = value;
                this.NotifyPropertyChanged("PAID_FOR");
            }
        }

        [DBColumn(Name = "ACC_RCV_VC_NO", Storage = "m_ACC_RCV_VC_NO", DbType = "126")]
        public string ACC_RCV_VC_NO
        {
            get { return this.m_ACC_RCV_VC_NO; }
            set
            {
                this.m_ACC_RCV_VC_NO = value;
                this.NotifyPropertyChanged("ACC_RCV_VC_NO");
            }
        }

        [DBColumn(Name = "UPDATE_STATUS", Storage = "m_UPDATE_STATUS", DbType = "126")]
        public string UPDATE_STATUS
        {
            get { return this.m_UPDATE_STATUS; }
            set
            {
                this.m_UPDATE_STATUS = value;
                this.NotifyPropertyChanged("UPDATE_STATUS");
            }
        }

        [DBColumn(Name = "PAYMENT_VC_NO", Storage = "m_PAYMENT_VC_NO", DbType = "126")]
        public string PAYMENT_VC_NO
        {
            get { return this.m_PAYMENT_VC_NO; }
            set
            {
                this.m_PAYMENT_VC_NO = value;
                this.NotifyPropertyChanged("PAYMENT_VC_NO");
            }
        }

        [DBColumn(Name = "CASH_MEDIA", Storage = "m_CASH_MEDIA", DbType = "126")]
        public string CASH_MEDIA
        {
            get { return this.m_CASH_MEDIA; }
            set
            {
                this.m_CASH_MEDIA = value;
                this.NotifyPropertyChanged("CASH_MEDIA");
            }
        }

        [DBColumn(Name = "RECEIVE_TIME", Storage = "m_RECEIVE_TIME", DbType = "106")]
        public DateTime? RECEIVE_TIME
        {
            get { return this.m_RECEIVE_TIME; }
            set
            {
                this.m_RECEIVE_TIME = value;
                this.NotifyPropertyChanged("RECEIVE_TIME");
            }
        }

        [DBColumn(Name = "ITEM_SEGMENT_ID", Storage = "m_ITEM_SEGMENT_ID", DbType = "107")]
        public decimal ITEM_SEGMENT_ID
        {
            get { return this.m_ITEM_SEGMENT_ID; }
            set
            {
                this.m_ITEM_SEGMENT_ID = value;
                this.NotifyPropertyChanged("ITEM_SEGMENT_ID");
            }
        }

        [DBColumn(Name = "DEPOSITED_BY", Storage = "m_DEPOSITED_BY", DbType = "126")]
        public string DEPOSITED_BY
        {
            get { return this.m_DEPOSITED_BY; }
            set
            {
                this.m_DEPOSITED_BY = value;
                this.NotifyPropertyChanged("DEPOSITED_BY");
            }
        }

        [DBColumn(Name = "ACTUAL_RCV_AMT", Storage = "m_ACTUAL_RCV_AMT", DbType = "107")]
        public decimal ACTUAL_RCV_AMT
        {
            get { return this.m_ACTUAL_RCV_AMT; }
            set
            {
                this.m_ACTUAL_RCV_AMT = value;
                this.NotifyPropertyChanged("ACTUAL_RCV_AMT");
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

        #endregion //properties
    }

    public partial class dcRECEIVE_MR_MST
    {
        public string DEALER_NAME { get; set; }
        public string SE_NAME { get; set; }
        public double ONLINE_AMT { get; set; }
        public string BANK_DESC { get; set; }
        public string BRANCH_DESC { get; set; }

        public string TYPE { get; set; }
        public string ITEM_SEGMENT_NAME { get; set; }
        public string RECEIVE_LOC_DP { get; set; }

        public string cheque_NO { get; set; }
        public string chqACC_NO { get; set; }
        public string HolderACC_NO { get; set; }
        public string DEALER_ADDRESS { get; set; }
        public string DEALER_CONTACT { get; set; }

        public string DEARLERDIVISION { get; set; }

        public string DEARLERTHANA { get; set; }
        public string DealerDistrict { get; set; }
        //public string SE_ID { get; set; }
        public string SE_MOBILE { get; set; }

        public string ChkBankCode { get; set; }

        public string ChkAccNo { get; set; }

        public decimal CHE_ALTU { get; set; }
        public decimal ACTUAL_CHEQUE_AMT { get; set; }

        public string ChkAccHolderName { get; set; }
        public string ChkAccHolderCOMP_ABBR { get; set; }
        public string ITEM_SEGMENT_DESC { get; set; }
        public string DEALER_TYPE { get; set; }

        public string ChkBankName { get; set; }

        public string ChkBranchCode { get; set; }
        public string ChkBranchName { get; set; }
        public decimal ChkChequeAmt { get; set; }
        public DateTime? ChkCreditDate { get; set; }
        public string ChkStatus { get; set; }
        public string TTNo { get; set; }

        public decimal OnlineALTU { get; set; }
        public string OnlineAccHolderName { get; set; }
        public string OnlineAccHolderNameDtl { get; set; }
        public string OnLineAccountNo { get; set; }
        public string OnlineBankCode { get; set; }
        public string OnlineBankName { get; set; }
        public string OnlineAccHolderId { get; set; }
        public string OnlineBranchCode { get; set; }
        public string OnlineBranchName { get; set; }
        public decimal OnlineAmount { get; set; }

        public decimal Act_Online_AMT { get; set; }
        public string AUTH_STATUS { get; set; }

        public string MR_NO { get; set; }

        public DateTime? MR_DATE { get; set; }

        public string Depositor_ID { get; set; }
        public string Depositor_Name { get; set; }
        public string BANK_CODE { get; set; }

        public string BRANCH_CODE { get; set; }
        public string AccHolderBankCode { get; set; }
        public string AccHolderBankName { get; set; }
        public string AccHolderBranchCode { get; set; }
        public string AccHolderBranchName { get; set; }

        //public decimal ACTUAL_RCV_AMT { get; set; }

        //public string DEPOSITED_BY { get; set; }
        public string DEPOSITED_NAME { get; set; }
        public string ACC_HOLDER_NAME { get; set; }
        public string ACC_HOLDER_ID { get; set; }
        public string ACC_HOLDER_DTL_ID { get; set; }
        public string REG_ID { get; set; }
        public decimal MR_AMT { get; set; }

        public int BANK_ID { get; set; }
        public int BRANCH_ID { get; set; }
        public string CUST_NAME { get; set; }
        
            public string CUST_ADDRESS { get; set; }  
         public string CUST_PHONE { get; set; }
         public string CUST_CODE { get; set; }
         //public string RECEIVE_NO { get; set; }  
           

    }
}
