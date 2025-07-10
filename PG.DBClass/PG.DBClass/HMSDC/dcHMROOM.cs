using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.HMSDC
{
    [DBTable(Name = "HMROOM")]
    public partial class dcHMROOM : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ROOM_ID = 0;
        private string m_ROOM_NO = string.Empty;
        private int m_ROOM_TYPE_ID = 0;
        private int m_FLOOR_ID = 0;
        private byte[] m_THUMBNAILSIMAGE_NAME = null;
        private string m_FULL_IMAGE_NAME = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private decimal m_ORDER_NO = 0;
        private string m_CHECKED_IN = string.Empty;
        private int m_ROOM_STATUS_ID = 0;
        private string m_REMARKS = string.Empty;
        private string m_RESPONSIBLE = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;

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


        [DBColumn(Name = "ROOM_ID", Storage = "m_ROOM_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ROOM_ID
        {
            get { return this.m_ROOM_ID; }
            set
            {
                this.m_ROOM_ID = value;
                this.NotifyPropertyChanged("ROOM_ID");
            }
        }

        [DBColumn(Name = "ROOM_NO", Storage = "m_ROOM_NO", DbType = "126")]
        public string ROOM_NO
        {
            get { return this.m_ROOM_NO; }
            set
            {
                this.m_ROOM_NO = value;
                this.NotifyPropertyChanged("ROOM_NO");
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

        [DBColumn(Name = "FLOOR_ID", Storage = "m_FLOOR_ID", DbType = "107")]
        public int FLOOR_ID
        {
            get { return this.m_FLOOR_ID; }
            set
            {
                this.m_FLOOR_ID = value;
                this.NotifyPropertyChanged("FLOOR_ID");
            }
        }

        [DBColumn(Name = "THUMBNAILSIMAGE_NAME", Storage = "m_THUMBNAILSIMAGE_NAME", DbType = "102")]
        public byte[] THUMBNAILSIMAGE_NAME
        {
            get { return this.m_THUMBNAILSIMAGE_NAME; }
            set
            {
                this.m_THUMBNAILSIMAGE_NAME = value;
                this.NotifyPropertyChanged("THUMBNAILSIMAGE_NAME");
            }
        }

        [DBColumn(Name = "FULL_IMAGE_NAME", Storage = "m_FULL_IMAGE_NAME", DbType = "126")]
        public string FULL_IMAGE_NAME
        {
            get { return this.m_FULL_IMAGE_NAME; }
            set
            {
                this.m_FULL_IMAGE_NAME = value;
                this.NotifyPropertyChanged("FULL_IMAGE_NAME");
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

        [DBColumn(Name = "ORDER_NO", Storage = "m_ORDER_NO", DbType = "107")]
        public decimal ORDER_NO
        {
            get { return this.m_ORDER_NO; }
            set
            {
                this.m_ORDER_NO = value;
                this.NotifyPropertyChanged("ORDER_NO");
            }
        }

        [DBColumn(Name = "CHECKED_IN", Storage = "m_CHECKED_IN", DbType = "126")]
        public string CHECKED_IN
        {
            get { return this.m_CHECKED_IN; }
            set
            {
                this.m_CHECKED_IN = value;
                this.NotifyPropertyChanged("CHECKED_IN");
            }
        }

        [DBColumn(Name = "ROOM_STATUS_ID", Storage = "m_ROOM_STATUS_ID", DbType = "107")]
        public int ROOM_STATUS_ID
        {
            get { return this.m_ROOM_STATUS_ID; }
            set
            {
                this.m_ROOM_STATUS_ID = value;
                this.NotifyPropertyChanged("ROOM_STATUS_ID");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }

        [DBColumn(Name = "RESPONSIBLE", Storage = "m_RESPONSIBLE", DbType = "126")]
        public string RESPONSIBLE
        {
            get { return this.m_RESPONSIBLE; }
            set
            {
                this.m_RESPONSIBLE = value;
                this.NotifyPropertyChanged("RESPONSIBLE");
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

        #endregion //properties
    }

    public partial class dcHMROOM
    {
        public string ROOM_TYPE_NAME { get; set; }
        public string FLOOR_NAME { get; set; }
        public string ROOM_STATUS { get; set; }
        public string ROOM_DESCRIPTION { get; set; }
        public decimal NORMAL_RATE { get; set; }
        public decimal DISCOUNTED_RATE { get; set; }
        public decimal MAX_PERSON { get; set; }
        public decimal NO_OF_ROOM { get; set; }
       
        

    }
}
