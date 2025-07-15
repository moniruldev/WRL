using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CARGO_CREATION_DETAIL")]
    public partial class dcCARGO_CREATION_DETAIL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CARGO_DETAIL_ID = 0;
        private int m_CARGO_ID = 0;
        private int m_CN_ID = 0;

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


        [DBColumn(Name = "CARGO_DETAIL_ID", Storage = "m_CARGO_DETAIL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CARGO_DETAIL_ID
        {
            get { return this.m_CARGO_DETAIL_ID; }
            set
            {
                this.m_CARGO_DETAIL_ID = value;
                this.NotifyPropertyChanged("CARGO_DETAIL_ID");
            }
        }

        [DBColumn(Name = "CARGO_ID", Storage = "m_CARGO_ID", DbType = "107")]
        public int CARGO_ID
        {
            get { return this.m_CARGO_ID; }
            set
            {
                this.m_CARGO_ID = value;
                this.NotifyPropertyChanged("CARGO_ID");
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

        #endregion //properties
    }

     public partial class dcCARGO_CREATION_DETAIL
     {
         public string CN_NUMBER { get; set; }
     }
}
