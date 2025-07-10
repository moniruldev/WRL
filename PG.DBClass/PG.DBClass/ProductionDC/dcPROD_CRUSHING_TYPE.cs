using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_CRUSHING_TYPE")]
    public partial class dcPROD_CRUSHING_TYPE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_MIXED_CRUSHING_TYPE_ID = 0;
        private string m_ITEM_TYPE_CODE = string.Empty;
        private string m_ITEM_TYPE_NAME = string.Empty;
        private string m_ITEM_TYPE_DESC = string.Empty;
        private string m_ITEM_TYPE_SHORT_NAME = string.Empty;

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


        [DBColumn(Name = "MIXED_CRUSHING_TYPE_ID", Storage = "m_MIXED_CRUSHING_TYPE_ID", DbType = "107")]
        public decimal MIXED_CRUSHING_TYPE_ID
        {
            get { return this.m_MIXED_CRUSHING_TYPE_ID; }
            set
            {
                this.m_MIXED_CRUSHING_TYPE_ID = value;
                this.NotifyPropertyChanged("MIXED_CRUSHING_TYPE_ID");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_CODE", Storage = "m_ITEM_TYPE_CODE", DbType = "126")]
        public string ITEM_TYPE_CODE
        {
            get { return this.m_ITEM_TYPE_CODE; }
            set
            {
                this.m_ITEM_TYPE_CODE = value;
                this.NotifyPropertyChanged("ITEM_TYPE_CODE");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_NAME", Storage = "m_ITEM_TYPE_NAME", DbType = "126")]
        public string ITEM_TYPE_NAME
        {
            get { return this.m_ITEM_TYPE_NAME; }
            set
            {
                this.m_ITEM_TYPE_NAME = value;
                this.NotifyPropertyChanged("ITEM_TYPE_NAME");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_DESC", Storage = "m_ITEM_TYPE_DESC", DbType = "126")]
        public string ITEM_TYPE_DESC
        {
            get { return this.m_ITEM_TYPE_DESC; }
            set
            {
                this.m_ITEM_TYPE_DESC = value;
                this.NotifyPropertyChanged("ITEM_TYPE_DESC");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_SHORT_NAME", Storage = "m_ITEM_TYPE_SHORT_NAME", DbType = "126")]
        public string ITEM_TYPE_SHORT_NAME
        {
            get { return this.m_ITEM_TYPE_SHORT_NAME; }
            set
            {
                this.m_ITEM_TYPE_SHORT_NAME = value;
                this.NotifyPropertyChanged("ITEM_TYPE_SHORT_NAME");
            }
        }

        #endregion //properties
    }
}
