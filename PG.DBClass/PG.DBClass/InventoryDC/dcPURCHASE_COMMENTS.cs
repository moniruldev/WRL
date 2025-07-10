using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "PURCHASE_COMMENTS")]
    public partial class dcPURCHASE_COMMENTS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PURCHASE_COMMENTS_ID = 0;
        private string m_PURCHASE_NO = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_COMMENTS_DESC = string.Empty;

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


        [DBColumn(Name = "PURCHASE_COMMENTS_ID", Storage = "m_PURCHASE_COMMENTS_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PURCHASE_COMMENTS_ID
        {
            get { return this.m_PURCHASE_COMMENTS_ID; }
            set
            {
                this.m_PURCHASE_COMMENTS_ID = value;
                this.NotifyPropertyChanged("PURCHASE_COMMENTS_ID");
            }
        }

        [DBColumn(Name = "PURCHASE_NO", Storage = "m_PURCHASE_NO", DbType = "126")]
        public string PURCHASE_NO
        {
            get { return this.m_PURCHASE_NO; }
            set
            {
                this.m_PURCHASE_NO = value;
                this.NotifyPropertyChanged("PURCHASE_NO");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
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
        public int UPDATE_BY
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

        [DBColumn(Name = "COMMENTS_DESC", Storage = "m_COMMENTS_DESC", DbType = "126")]
        public string COMMENTS_DESC
        {
            get { return this.m_COMMENTS_DESC; }
            set
            {
                this.m_COMMENTS_DESC = value;
                this.NotifyPropertyChanged("COMMENTS_DESC");
            }
        }

        #endregion //properties
    }

    public partial class dcPURCHASE_COMMENTS
    {
        public string CREATE_BY_NAME { set; get; }
    }
}
