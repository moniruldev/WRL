using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.HMSDC
{
    [DBTable(Name = "HMRESERVATION_DTL")]
    public partial class dcHMRESERVATION_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RESERVATION_DTL_ID = 0;
        private int m_RESERVATION_ID = 0;
        private int m_ROOM_TYPE_ID = 0;
        private decimal m_ROOM_QTY = 0;

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


        [DBColumn(Name = "RESERVATION_DTL_ID", Storage = "m_RESERVATION_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RESERVATION_DTL_ID
        {
            get { return this.m_RESERVATION_DTL_ID; }
            set
            {
                this.m_RESERVATION_DTL_ID = value;
                this.NotifyPropertyChanged("RESERVATION_DTL_ID");
            }
        }

        [DBColumn(Name = "RESERVATION_ID", Storage = "m_RESERVATION_ID", DbType = "107")]
        public int RESERVATION_ID
        {
            get { return this.m_RESERVATION_ID; }
            set
            {
                this.m_RESERVATION_ID = value;
                this.NotifyPropertyChanged("RESERVATION_ID");
            }
        }

        [DBColumn(Name = "ROOM_TYPE_ID", Storage = "m_ROOM_TYPE_ID", DbType = "107")]
        public int ROOM_TYPE_ID
        {
            get { return this.m_ROOM_TYPE_ID; }
            set
            {
                this.m_ROOM_TYPE_ID = value;
                this.NotifyPropertyChanged("ROOM_TYPE_ID");
            }
        }

        [DBColumn(Name = "ROOM_QTY", Storage = "m_ROOM_QTY", DbType = "107")]
        public decimal ROOM_QTY
        {
            get { return this.m_ROOM_QTY; }
            set
            {
                this.m_ROOM_QTY = value;
                this.NotifyPropertyChanged("ROOM_QTY");
            }
        }

        #endregion //properties
    }

    public partial class dcHMRESERVATION_DTL
    {
        public string ROOM_TYPE_NAME { get; set; }
        public string ROOM_DESCRIPTION { get; set; }
        public decimal NORMAL_RATE { get; set; }
        public decimal DISCOUNTED_RATE { get; set; }
        public decimal MAX_PERSON { get; set; }
        public decimal NO_OF_ROOM { get; set; }
        public int ROOM_ID { get; set; }



    }
}
