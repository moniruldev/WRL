using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CN_ASSIGNMENT")]
    public partial class dcCN_ASSIGNMENT : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CN_ASSIGN_ID = 0;
        private DateTime? m_ASSIGN_DATE = null;
        private int m_DELIVERY_MAN_ID = 0;
        private int m_CN_ID = 0;
        private string m_IS_DELIVERED = string.Empty;
        private string m_IS_REFUND = string.Empty;
        private decimal m_REF_CARGO_ID = 0;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;

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


        [DBColumn(Name = "CN_ASSIGN_ID", Storage = "m_CN_ASSIGN_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CN_ASSIGN_ID
        {
            get { return this.m_CN_ASSIGN_ID; }
            set
            {
                this.m_CN_ASSIGN_ID = value;
                this.NotifyPropertyChanged("CN_ASSIGN_ID");
            }
        }

        [DBColumn(Name = "ASSIGN_DATE", Storage = "m_ASSIGN_DATE", DbType = "106")]
        public DateTime? ASSIGN_DATE
        {
            get { return this.m_ASSIGN_DATE; }
            set
            {
                this.m_ASSIGN_DATE = value;
                this.NotifyPropertyChanged("ASSIGN_DATE");
            }
        }

        [DBColumn(Name = "DELIVERY_MAN_ID", Storage = "m_DELIVERY_MAN_ID", DbType = "107")]
        public int DELIVERY_MAN_ID
        {
            get { return this.m_DELIVERY_MAN_ID; }
            set
            {
                this.m_DELIVERY_MAN_ID = value;
                this.NotifyPropertyChanged("DELIVERY_MAN_ID");
            }
        }

        [DBColumn(Name = "CN_ID", Storage = "m_CN_ID", DbType = "107")]
        public int CN_ID
        {
            get { return this.m_CN_ID; }
            set
            {
                this.m_CN_ID = value;
                this.NotifyPropertyChanged("CN_ID");
            }
        }

        [DBColumn(Name = "IS_DELIVERED", Storage = "m_IS_DELIVERED", DbType = "126")]
        public string IS_DELIVERED
        {
            get { return this.m_IS_DELIVERED; }
            set
            {
                this.m_IS_DELIVERED = value;
                this.NotifyPropertyChanged("IS_DELIVERED");
            }
        }

        [DBColumn(Name = "IS_REFUND", Storage = "m_IS_REFUND", DbType = "126")]
        public string IS_REFUND
        {
            get { return this.m_IS_REFUND; }
            set
            {
                this.m_IS_REFUND = value;
                this.NotifyPropertyChanged("IS_REFUND");
            }
        }

        [DBColumn(Name = "REF_CARGO_ID", Storage = "m_REF_CARGO_ID", DbType = "107")]
        public decimal REF_CARGO_ID
        {
            get { return this.m_REF_CARGO_ID; }
            set
            {
                this.m_REF_CARGO_ID = value;
                this.NotifyPropertyChanged("REF_CARGO_ID");
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

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        #endregion //properties
    }
}
