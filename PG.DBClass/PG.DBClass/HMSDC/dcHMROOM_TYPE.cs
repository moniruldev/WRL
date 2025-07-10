using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.HMSDC
{
    [DBTable(Name = "HMROOM_TYPE")]
    public partial class dcHMROOM_TYPE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ROOM_TYPE_ID = 0;
        private string m_TITLE = string.Empty;
        private string m_DESCRIPTION = string.Empty;
        private int m_MAX_PERSON = 0;
        private decimal m_NORMAL_RATE = 0;
        private decimal m_DISCOUNTED_RATE = 0;
        private string m_IS_ACTIVE = string.Empty;
        private byte[] m_THUMBNAILS_IMAGE;
        private string m_FULL_IMAGE_NAME = string.Empty;
        private int m_ORDER_NO = 0;
        private DateTime? m_START_DATE = null;
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


        [DBColumn(Name = "ROOM_TYPE_ID", Storage = "m_ROOM_TYPE_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ROOM_TYPE_ID
        {
            get { return this.m_ROOM_TYPE_ID; }
            set
            {
                this.m_ROOM_TYPE_ID = value;
                this.NotifyPropertyChanged("ROOM_TYPE_ID");
            }
        }

        [DBColumn(Name = "TITLE", Storage = "m_TITLE", DbType = "126")]
        public string TITLE
        {
            get { return this.m_TITLE; }
            set
            {
                this.m_TITLE = value;
                this.NotifyPropertyChanged("TITLE");
            }
        }

        [DBColumn(Name = "DESCRIPTION", Storage = "m_DESCRIPTION", DbType = "126")]
        public string DESCRIPTION
        {
            get { return this.m_DESCRIPTION; }
            set
            {
                this.m_DESCRIPTION = value;
                this.NotifyPropertyChanged("DESCRIPTION");
            }
        }

        [DBColumn(Name = "MAX_PERSON", Storage = "m_MAX_PERSON", DbType = "107")]
        public int MAX_PERSON
        {
            get { return this.m_MAX_PERSON; }
            set
            {
                this.m_MAX_PERSON = value;
                this.NotifyPropertyChanged("MAX_PERSON");
            }
        }

        [DBColumn(Name = "NORMAL_RATE", Storage = "m_NORMAL_RATE", DbType = "107")]
        public decimal NORMAL_RATE
        {
            get { return this.m_NORMAL_RATE; }
            set
            {
                this.m_NORMAL_RATE = value;
                this.NotifyPropertyChanged("NORMAL_RATE");
            }
        }

        [DBColumn(Name = "DISCOUNTED_RATE", Storage = "m_DISCOUNTED_RATE", DbType = "107")]
        public decimal DISCOUNTED_RATE
        {
            get { return this.m_DISCOUNTED_RATE; }
            set
            {
                this.m_DISCOUNTED_RATE = value;
                this.NotifyPropertyChanged("DISCOUNTED_RATE");
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

        [DBColumn(Name = "THUMBNAILS_IMAGE", Storage = "m_THUMBNAILS_IMAGE")]
        public byte[] THUMBNAILS_IMAGE
        {
            get { return this.m_THUMBNAILS_IMAGE; }
            set
            {
                this.m_THUMBNAILS_IMAGE = value;
                this.NotifyPropertyChanged("THUMBNAILS_IMAGE");
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

        [DBColumn(Name = "ORDER_NO", Storage = "m_ORDER_NO", DbType = "107")]
        public int ORDER_NO
        {
            get { return this.m_ORDER_NO; }
            set
            {
                this.m_ORDER_NO = value;
                this.NotifyPropertyChanged("ORDER_NO");
            }
        }

        [DBColumn(Name = "START_DATE", Storage = "m_START_DATE", DbType = "106")]
        public DateTime? START_DATE
        {
            get { return this.m_START_DATE; }
            set
            {
                this.m_START_DATE = value;
                this.NotifyPropertyChanged("START_DATE");
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
}
