using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "ACCOUNT_HOLDER")]
    public partial class dcACCOUNT_HOLDER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_ACC_ID = 0;
        private string m_ACC_HOLDER_ID = string.Empty;
        private string m_ACC_HOLDER_NAME = string.Empty;
        private string m_COMP_ABBR = string.Empty;

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


        [DBColumn(Name = "ACC_ID", Storage = "m_ACC_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal ACC_ID
        {
            get { return this.m_ACC_ID; }
            set
            {
                this.m_ACC_ID = value;
                this.NotifyPropertyChanged("ACC_ID");
            }
        }

        [DBColumn(Name = "ACC_HOLDER_ID", Storage = "m_ACC_HOLDER_ID", DbType = "126")]
        public string ACC_HOLDER_ID
        {
            get { return this.m_ACC_HOLDER_ID; }
            set
            {
                this.m_ACC_HOLDER_ID = value;
                this.NotifyPropertyChanged("ACC_HOLDER_ID");
            }
        }

        [DBColumn(Name = "ACC_HOLDER_NAME", Storage = "m_ACC_HOLDER_NAME", DbType = "126")]
        public string ACC_HOLDER_NAME
        {
            get { return this.m_ACC_HOLDER_NAME; }
            set
            {
                this.m_ACC_HOLDER_NAME = value;
                this.NotifyPropertyChanged("ACC_HOLDER_NAME");
            }
        }

        [DBColumn(Name = "COMP_ABBR", Storage = "m_COMP_ABBR", DbType = "126")]
        public string COMP_ABBR
        {
            get { return this.m_COMP_ABBR; }
            set
            {
                this.m_COMP_ABBR = value;
                this.NotifyPropertyChanged("COMP_ABBR");
            }
        }

        #endregion //properties
    }

    public partial class dcACCOUNT_HOLDER
    {

        public string bank_desc { get; set; }
        public string branch_desc { get; set; }
        public string acc_no { get; set; }
        public int BANK_ID { get; set; }
        public int BRANCH_ID { get; set; }
        
            
        public string BANK_CODE { get; set; }

        public string BRANCH_CODE { get; set; }

        public string ACC_HOLDER_DTL_ID { get; set; }

    }
}
