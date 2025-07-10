using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INDT_COMMENT_DTL")]
    public partial class dcINDT_COMMENT_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_INDT_COMMENT_DTL_ID = 0;
        private int m_INDT_ID = 0;
        private string m_INDT_NO = string.Empty;
        private string m_COMMENT_DESC = string.Empty;
        private string m_COMMENT_BY = string.Empty;
        private DateTime? m_COMMENT_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;

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


        [DBColumn(Name = "INDT_COMMENT_DTL_ID", Storage = "m_INDT_COMMENT_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int INDT_COMMENT_DTL_ID
        {
            get { return this.m_INDT_COMMENT_DTL_ID; }
            set
            {
                this.m_INDT_COMMENT_DTL_ID = value;
                this.NotifyPropertyChanged("INDT_COMMENT_DTL_ID");
            }
        }

        [DBColumn(Name = "INDT_ID", Storage = "m_INDT_ID", DbType = "107")]
        public int INDT_ID
        {
            get { return this.m_INDT_ID; }
            set
            {
                this.m_INDT_ID = value;
                this.NotifyPropertyChanged("INDT_ID");
            }
        }

        [DBColumn(Name = "INDT_NO", Storage = "m_INDT_NO", DbType = "126")]
        public string INDT_NO
        {
            get { return this.m_INDT_NO; }
            set
            {
                this.m_INDT_NO = value;
                this.NotifyPropertyChanged("INDT_NO");
            }
        }

        [DBColumn(Name = "COMMENT_DESC", Storage = "m_COMMENT_DESC", DbType = "126")]
        public string COMMENT_DESC
        {
            get { return this.m_COMMENT_DESC; }
            set
            {
                this.m_COMMENT_DESC = value;
                this.NotifyPropertyChanged("COMMENT_DESC");
            }
        }

        [DBColumn(Name = "COMMENT_BY", Storage = "m_COMMENT_BY", DbType = "126")]
        public string COMMENT_BY
        {
            get { return this.m_COMMENT_BY; }
            set
            {
                this.m_COMMENT_BY = value;
                this.NotifyPropertyChanged("COMMENT_BY");
            }
        }

        [DBColumn(Name = "COMMENT_DATE", Storage = "m_COMMENT_DATE", DbType = "106")]
        public DateTime? COMMENT_DATE
        {
            get { return this.m_COMMENT_DATE; }
            set
            {
                this.m_COMMENT_DATE = value;
                this.NotifyPropertyChanged("COMMENT_DATE");
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

        #endregion //properties
    }
}
