using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.SecurityDC
{
    [Serializable]
    [DBTable(Name = "DATATRANSFER_DTL_TAB_COL_PER")]
    public partial class dcDATATRANSFER_DTL_TAB_COL_PER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_DETAIL_ID = 0;
        private int m_DT_MSTR_DTL_ID = 0;
        private string m_COLUMN_NAME = string.Empty;
        //private decimal m_PERCENTAGE = 0;

        #endregion

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

        [DBColumn(Name = "DETAIL_ID", Storage = "m_DETAIL_ID", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true, DbType = "Int NOT NULL IDENTITY")]
        public int DETAIL_ID
        {
            get { return this.m_DETAIL_ID; }
            set
            {
                this.m_DETAIL_ID = value;
                this.NotifyPropertyChanged("DETAIL_ID");
            }
        }

        [DBColumn(Name = "DT_MSTR_DTL_ID", Storage = "m_DT_MSTR_DTL_ID", DbType = "126")]
        public int DT_MSTR_DTL_ID
        {
            get { return this.m_DT_MSTR_DTL_ID; }
            set
            {
                this.m_DT_MSTR_DTL_ID = value;
                this.NotifyPropertyChanged("DT_MSTR_DTL_ID");
            }
        }

        [DBColumn(Name = "COLUMN_NAME", Storage = "m_COLUMN_NAME", DbType = "126")]
        public string COLUMN_NAME
        {
            get { return this.m_COLUMN_NAME; }
            set
            {
                this.m_COLUMN_NAME = value;
                this.NotifyPropertyChanged("COLUMN_NAME");
            }
        }


        //[DBColumn(Name = "PERCENTAGE", Storage = "m_PERCENTAGE", DbType = "126")]
        //public decimal PERCENTAGE
        //{
        //    get { return this.m_PERCENTAGE; }
        //    set
        //    {
        //        this.m_PERCENTAGE = value;
        //        this.NotifyPropertyChanged("PERCENTAGE");
        //    }
        //}

        #endregion
    }
}
