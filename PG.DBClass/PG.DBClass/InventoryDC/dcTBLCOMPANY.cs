using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "TBLCOMPANY")]
    public partial class dcTBLCOMPANY : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_COMPANYID = 0;
        private decimal m_APPCLIENTID = 0;
        private string m_COMPANYNAME = string.Empty;
        private string m_COMPANYADDRESS = string.Empty;
        private decimal m_COMPANYIDPARENT = 0;
        private decimal m_COMPANYTYPEID = 0;
        private decimal m_LOCATIONID = 0;
        private int m_MS_PRC = 0;
        private string m_MS_RPT_MARK = string.Empty;
        private decimal m_IRR_QTY_RESTRICTION = 0;
        private DateTime? m_MAD_DATE = null;
        private string m_IS_OUT_SALE_IN_CASH = string.Empty;
        private string m_IS_STORE_PRICE_VIEWABLE_BY_ACC = string.Empty;

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


        [DBColumn(Name = "COMPANYID", Storage = "m_COMPANYID", DbType = "107")]
        public decimal COMPANYID
        {
            get { return this.m_COMPANYID; }
            set
            {
                this.m_COMPANYID = value;
                this.NotifyPropertyChanged("COMPANYID");
            }
        }

        [DBColumn(Name = "APPCLIENTID", Storage = "m_APPCLIENTID", DbType = "107")]
        public decimal APPCLIENTID
        {
            get { return this.m_APPCLIENTID; }
            set
            {
                this.m_APPCLIENTID = value;
                this.NotifyPropertyChanged("APPCLIENTID");
            }
        }

        [DBColumn(Name = "COMPANYNAME", Storage = "m_COMPANYNAME", DbType = "119")]
        public string COMPANYNAME
        {
            get { return this.m_COMPANYNAME; }
            set
            {
                this.m_COMPANYNAME = value;
                this.NotifyPropertyChanged("COMPANYNAME");
            }
        }

        [DBColumn(Name = "COMPANYADDRESS", Storage = "m_COMPANYADDRESS", DbType = "119")]
        public string COMPANYADDRESS
        {
            get { return this.m_COMPANYADDRESS; }
            set
            {
                this.m_COMPANYADDRESS = value;
                this.NotifyPropertyChanged("COMPANYADDRESS");
            }
        }

        [DBColumn(Name = "COMPANYIDPARENT", Storage = "m_COMPANYIDPARENT", DbType = "107")]
        public decimal COMPANYIDPARENT
        {
            get { return this.m_COMPANYIDPARENT; }
            set
            {
                this.m_COMPANYIDPARENT = value;
                this.NotifyPropertyChanged("COMPANYIDPARENT");
            }
        }

        [DBColumn(Name = "COMPANYTYPEID", Storage = "m_COMPANYTYPEID", DbType = "107")]
        public decimal COMPANYTYPEID
        {
            get { return this.m_COMPANYTYPEID; }
            set
            {
                this.m_COMPANYTYPEID = value;
                this.NotifyPropertyChanged("COMPANYTYPEID");
            }
        }

        [DBColumn(Name = "LOCATIONID", Storage = "m_LOCATIONID", DbType = "107")]
        public decimal LOCATIONID
        {
            get { return this.m_LOCATIONID; }
            set
            {
                this.m_LOCATIONID = value;
                this.NotifyPropertyChanged("LOCATIONID");
            }
        }

        [DBColumn(Name = "MS_PRC", Storage = "m_MS_PRC", DbType = "107")]
        public int MS_PRC
        {
            get { return this.m_MS_PRC; }
            set
            {
                this.m_MS_PRC = value;
                this.NotifyPropertyChanged("MS_PRC");
            }
        }

        [DBColumn(Name = "MS_RPT_MARK", Storage = "m_MS_RPT_MARK", DbType = "126")]
        public string MS_RPT_MARK
        {
            get { return this.m_MS_RPT_MARK; }
            set
            {
                this.m_MS_RPT_MARK = value;
                this.NotifyPropertyChanged("MS_RPT_MARK");
            }
        }


        [DBColumn(Name = "IRR_QTY_RESTRICTION", Storage = "m_IRR_QTY_RESTRICTION", DbType = "107")]
        public decimal IRR_QTY_RESTRICTION
        {
            get { return this.m_IRR_QTY_RESTRICTION; }
            set
            {
                this.m_IRR_QTY_RESTRICTION = value;
                this.NotifyPropertyChanged("IRR_QTY_RESTRICTION");
            }
        }


        [DBColumn(Name = "MAD_DATE", Storage = "m_MAD_DATE", DbType = "107")]
        public DateTime? MAD_DATE
        {
            get { return this.m_MAD_DATE; }
            set
            {
                this.m_MAD_DATE = value;
                this.NotifyPropertyChanged("MAD_DATE");
            }
        }


        [DBColumn(Name = "IS_OUT_SALE_IN_CASH", Storage = "m_IS_OUT_SALE_IN_CASH", DbType = "107")]
        public string IS_OUT_SALE_IN_CASH
        {
            get { return this.m_IS_OUT_SALE_IN_CASH; }
            set
            {
                this.m_IS_OUT_SALE_IN_CASH = value;
                this.NotifyPropertyChanged("IS_OUT_SALE_IN_CASH");
            }
        }



      [DBColumn(Name = "IS_STORE_PRICE_VIEWABLE_BY_ACC", Storage = "m_IS_STORE_PRICE_VIEWABLE_BY_ACC", DbType = "107")]
        public string IS_STORE_PRICE_VIEWABLE_BY_ACC
        {
            get { return this.m_IS_STORE_PRICE_VIEWABLE_BY_ACC; }
            set
            {
                this.m_IS_STORE_PRICE_VIEWABLE_BY_ACC = value;
                this.NotifyPropertyChanged("IS_STORE_PRICE_VIEWABLE_BY_ACC");
            }
        }


        #endregion //properties
    }
}
