using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "CS_MST")]
    public partial class dcCS_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_CS_MST_ID = 0;
        private string m_CS_NO = string.Empty;
        private DateTime? m_CS_DATE = null;
        private decimal m_CURRENCY_ID = 0;
        private decimal m_CONVERSION_RATE = 0;
        private decimal m_CS_TOTAL_FC = 0;
        private decimal m_CS_TOTAL_BDT = 0;
        private string m_REF_PR_NO = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_REMARKS = string.Empty;

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


        [DBColumn(Name = "CS_MST_ID", Storage = "m_CS_MST_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal CS_MST_ID
        {
            get { return this.m_CS_MST_ID; }
            set
            {
                this.m_CS_MST_ID = value;
                this.NotifyPropertyChanged("CS_MST_ID");
            }
        }

        [DBColumn(Name = "CS_NO", Storage = "m_CS_NO", DbType = "126")]
        public string CS_NO
        {
            get { return this.m_CS_NO; }
            set
            {
                this.m_CS_NO = value;
                this.NotifyPropertyChanged("CS_NO");
            }
        }

        [DBColumn(Name = "CS_DATE", Storage = "m_CS_DATE", DbType = "106")]
        public DateTime? CS_DATE
        {
            get { return this.m_CS_DATE; }
            set
            {
                this.m_CS_DATE = value;
                this.NotifyPropertyChanged("CS_DATE");
            }
        }

        [DBColumn(Name = "CURRENCY_ID", Storage = "m_CURRENCY_ID", DbType = "107")]
        public decimal CURRENCY_ID
        {
            get { return this.m_CURRENCY_ID; }
            set
            {
                this.m_CURRENCY_ID = value;
                this.NotifyPropertyChanged("CURRENCY_ID");
            }
        }

        [DBColumn(Name = "CONVERSION_RATE", Storage = "m_CONVERSION_RATE", DbType = "107")]
        public decimal CONVERSION_RATE
        {
            get { return this.m_CONVERSION_RATE; }
            set
            {
                this.m_CONVERSION_RATE = value;
                this.NotifyPropertyChanged("CONVERSION_RATE");
            }
        }

        [DBColumn(Name = "CS_TOTAL_FC", Storage = "m_CS_TOTAL_FC", DbType = "107")]
        public decimal CS_TOTAL_FC
        {
            get { return this.m_CS_TOTAL_FC; }
            set
            {
                this.m_CS_TOTAL_FC = value;
                this.NotifyPropertyChanged("CS_TOTAL_FC");
            }
        }

        [DBColumn(Name = "CS_TOTAL_BDT", Storage = "m_CS_TOTAL_BDT", DbType = "107")]
        public decimal CS_TOTAL_BDT
        {
            get { return this.m_CS_TOTAL_BDT; }
            set
            {
                this.m_CS_TOTAL_BDT = value;
                this.NotifyPropertyChanged("CS_TOTAL_BDT");
            }
        }

        [DBColumn(Name = "REF_PR_NO", Storage = "m_REF_PR_NO", DbType = "126")]
        public string REF_PR_NO
        {
            get { return this.m_REF_PR_NO; }
            set
            {
                this.m_REF_PR_NO = value;
                this.NotifyPropertyChanged("REF_PR_NO");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public decimal CREATE_BY
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
        public decimal UPDATE_BY
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

        #endregion //properties
    }
}
