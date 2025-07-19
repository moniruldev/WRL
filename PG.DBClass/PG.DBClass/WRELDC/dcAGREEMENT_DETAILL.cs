using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "AGREEMENT_DETAILL")]
    public partial class dcAGREEMENT_DETAILL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AGR_DETAIL_ID = 0;
        private int m_AGR_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_DISTANCE_TYPE_ID = 0;
        private decimal m_SERVICE_AMOUNT = 0;
        private string m_REMARKS = string.Empty;
        private decimal m_RETURN_PRICE = 0;
        private string m_IS_OTP_SERVICE = string.Empty;
        

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


        [DBColumn(Name = "AGR_DETAIL_ID", Storage = "m_AGR_DETAIL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int AGR_DETAIL_ID
        {
            get { return this.m_AGR_DETAIL_ID; }
            set
            {
                this.m_AGR_DETAIL_ID = value;
                this.NotifyPropertyChanged("AGR_DETAIL_ID");
            }
        }

        [DBColumn(Name = "AGR_ID", Storage = "m_AGR_ID", DbType = "107")]
        public int AGR_ID
        {
            get { return this.m_AGR_ID; }
            set
            {
                this.m_AGR_ID = value;
                this.NotifyPropertyChanged("AGR_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "DISTANCE_TYPE_ID", Storage = "m_DISTANCE_TYPE_ID", DbType = "107")]
        public int DISTANCE_TYPE_ID
        {
            get { return this.m_DISTANCE_TYPE_ID; }
            set
            {
                this.m_DISTANCE_TYPE_ID = value;
                this.NotifyPropertyChanged("DISTANCE_TYPE_ID");
            }
        }

        [DBColumn(Name = "SERVICE_AMOUNT", Storage = "m_SERVICE_AMOUNT", DbType = "107")]
        public decimal SERVICE_AMOUNT
        {
            get { return this.m_SERVICE_AMOUNT; }
            set
            {
                this.m_SERVICE_AMOUNT = value;
                this.NotifyPropertyChanged("SERVICE_AMOUNT");
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

         [DBColumn(Name = "RETURN_PRICE", Storage = "m_RETURN_PRICE", DbType = "107")]
        public decimal RETURN_PRICE
        {
            get { return this.m_RETURN_PRICE; }
            set
            {
                this.m_RETURN_PRICE = value;
                this.NotifyPropertyChanged("RETURN_PRICE");
            }
        }

         [DBColumn(Name = "IS_OTP_SERVICE", Storage = "m_IS_OTP_SERVICE", DbType = "126")]
         public string IS_OTP_SERVICE
         {
             get { return this.m_IS_OTP_SERVICE; }
             set
             {
                 this.m_IS_OTP_SERVICE = value;
                 this.NotifyPropertyChanged("IS_OTP_SERVICE");
             }
         }

        #endregion //properties
    }

    public partial class dcAGREEMENT_DETAILL
    {
        public string ITEM_NAME { get; set; }
        public string TYPE_NAME { get; set; }
        
    }
}
