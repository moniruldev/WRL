using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.SecurityDC
{
    [DBTable(Name = "DATATRANSFER_TABLE_MAP")]
    public partial class dcDATATRANSFER_TABLE_MAP : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ID = 0;
        private string m_TABLENAME = string.Empty;
        private decimal m_PERCENT = 0;
        private string m_IS_ACTIVE = String.Empty;

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


        [DBColumn(Name = "ID", Storage = "m_ID", DbType = "107")]
        public int ID
        {
            get { return this.m_ID; }
            set
            {
                this.m_ID = value;
                this.NotifyPropertyChanged("ID");
            }
        }

        [DBColumn(Name = "TABLENAME", Storage = "m_TABLENAME", DbType = "126")]
        public string TABLENAME
        {
            get { return this.m_TABLENAME; }
            set
            {
                this.m_TABLENAME = value;
                this.NotifyPropertyChanged("TABLENAME");
            }
        }

        [DBColumn(Name = "PERCENT", Storage = "m_PERCENT", DbType = "107")]
        public decimal PERCENT
        {
            get { return this.m_PERCENT; }
            set
            {
                this.m_PERCENT = value;
                this.NotifyPropertyChanged("PERCENT");
            }
        }



              [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public string IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
            }
        }

        #endregion //properties
    }
    public partial class dcDATATRANSFER_TABLE_MAP
    {
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
        public string fromProdDate = null;
        public string toProdDate = null;
        public string IS_SUCCESS { set; get; }
    }
}
