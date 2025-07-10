using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "ITC_TEMP_BATCH_INFO")]
    public partial class dcITC_TEMP_BATCH_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REQ_ISSUE_ID = 0;
        private string m_REQ_ISSUE_NO = string.Empty;
        private int m_ITEM_ID = 0;
        private int m_DEPT_ID = 0;
        private int m_STLM_ID = 0;
        private string m_PROD_BATCH_NO = string.Empty;
        private decimal m_CLOSING_QTY = 0;
        private int m_UOM_ID = 0;
        private decimal m_USED_QTY = 0;
        private int m_SL_NO = 0;

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


        [DBColumn(Name = "REQ_ISSUE_ID", Storage = "m_REQ_ISSUE_ID", DbType = "107")]
        public int REQ_ISSUE_ID
        {
            get { return this.m_REQ_ISSUE_ID; }
            set
            {
                this.m_REQ_ISSUE_ID = value;
                this.NotifyPropertyChanged("REQ_ISSUE_ID");
            }
        }

        [DBColumn(Name = "REQ_ISSUE_NO", Storage = "m_REQ_ISSUE_NO", DbType = "126")]
        public string REQ_ISSUE_NO
        {
            get { return this.m_REQ_ISSUE_NO; }
            set
            {
                this.m_REQ_ISSUE_NO = value;
                this.NotifyPropertyChanged("REQ_ISSUE_NO");
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

        [DBColumn(Name = "PROD_BATCH_NO", Storage = "m_PROD_BATCH_NO", DbType = "126")]
        public string PROD_BATCH_NO
        {
            get { return this.m_PROD_BATCH_NO; }
            set
            {
                this.m_PROD_BATCH_NO = value;
                this.NotifyPropertyChanged("PROD_BATCH_NO");
            }
        }

        [DBColumn(Name = "CLOSING_QTY", Storage = "m_CLOSING_QTY", DbType = "107")]
        public decimal CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set
            {
                this.m_CLOSING_QTY = value;
                this.NotifyPropertyChanged("CLOSING_QTY");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "USED_QTY", Storage = "m_USED_QTY", DbType = "107")]
        public decimal USED_QTY
        {
            get { return this.m_USED_QTY; }
            set
            {
                this.m_USED_QTY = value;
                this.NotifyPropertyChanged("USED_QTY");
            }
        }

        [DBColumn(Name = "SL_NO", Storage = "m_SL_NO", DbType = "107")]
        public int SL_NO
        {
            get { return this.m_SL_NO; }
            set
            {
                this.m_SL_NO = value;
                this.NotifyPropertyChanged("SL_NO");
            }
        }

        #endregion //properties
    }
}
