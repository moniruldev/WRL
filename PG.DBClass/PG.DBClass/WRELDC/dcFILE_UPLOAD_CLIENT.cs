using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "FILE_UPLOAD_CLIENT")]
    public partial class dcFILE_UPLOAD_CLIENT : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_UPLOAD_ID = 0;
        private int m_CLIENT_ID = 0;
        private string m_FILE_NAME = string.Empty;
        private string m_FILE_PATH = string.Empty;
        private DateTime? m_UPLOAD_DATE = null;
        private string m_UPLOAD_BY = string.Empty;

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


        [DBColumn(Name = "UPLOAD_ID", Storage = "m_UPLOAD_ID", DbType = "107", IsPrimaryKey = true)]
        public int UPLOAD_ID
        {
            get { return this.m_UPLOAD_ID; }
            set
            {
                this.m_UPLOAD_ID = value;
                this.NotifyPropertyChanged("UPLOAD_ID");
            }
        }

        [DBColumn(Name = "CLIENT_ID", Storage = "m_CLIENT_ID", DbType = "107")]
        public int CLIENT_ID
        {
            get { return this.m_CLIENT_ID; }
            set
            {
                this.m_CLIENT_ID = value;
                this.NotifyPropertyChanged("CLIENT_ID");
            }
        }

        [DBColumn(Name = "FILE_NAME", Storage = "m_FILE_NAME", DbType = "126")]
        public string FILE_NAME
        {
            get { return this.m_FILE_NAME; }
            set
            {
                this.m_FILE_NAME = value;
                this.NotifyPropertyChanged("FILE_NAME");
            }
        }

        [DBColumn(Name = "FILE_PATH", Storage = "m_FILE_PATH", DbType = "126")]
        public string FILE_PATH
        {
            get { return this.m_FILE_PATH; }
            set
            {
                this.m_FILE_PATH = value;
                this.NotifyPropertyChanged("FILE_PATH");
            }
        }

        [DBColumn(Name = "UPLOAD_DATE", Storage = "m_UPLOAD_DATE", DbType = "106")]
        public DateTime? UPLOAD_DATE
        {
            get { return this.m_UPLOAD_DATE; }
            set
            {
                this.m_UPLOAD_DATE = value;
                this.NotifyPropertyChanged("UPLOAD_DATE");
            }
        }

        [DBColumn(Name = "UPLOAD_BY", Storage = "m_UPLOAD_BY", DbType = "126")]
        public string UPLOAD_BY
        {
            get { return this.m_UPLOAD_BY; }
            set
            {
                this.m_UPLOAD_BY = value;
                this.NotifyPropertyChanged("UPLOAD_BY");
            }
        }

        #endregion //properties
    }
}
