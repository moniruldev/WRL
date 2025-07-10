using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "QUOTATION_FILE_UPLOAD")]
    public partial class dcQUOTATION_FILE_UPLOAD : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_QUOTATION_FILE_ID = 0;
        private decimal m_QUOTATION_MST_ID = 0;
        private string m_QUOTATION_NO = string.Empty;
        private string m_FILE_NAME = string.Empty;
        private string m_CONTENT_TYPE = string.Empty;
        private string m_FILE_DATA1 = string.Empty;

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


        [DBColumn(Name = "QUOTATION_FILE_ID", Storage = "m_QUOTATION_FILE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal QUOTATION_FILE_ID
        {
            get { return this.m_QUOTATION_FILE_ID; }
            set
            {
                this.m_QUOTATION_FILE_ID = value;
                this.NotifyPropertyChanged("QUOTATION_FILE_ID");
            }
        }

        [DBColumn(Name = "QUOTATION_MST_ID", Storage = "m_QUOTATION_MST_ID", DbType = "107")]
        public decimal QUOTATION_MST_ID
        {
            get { return this.m_QUOTATION_MST_ID; }
            set
            {
                this.m_QUOTATION_MST_ID = value;
                this.NotifyPropertyChanged("QUOTATION_MST_ID");
            }
        }

        [DBColumn(Name = "QUOTATION_NO", Storage = "m_QUOTATION_NO", DbType = "126")]
        public string QUOTATION_NO
        {
            get { return this.m_QUOTATION_NO; }
            set
            {
                this.m_QUOTATION_NO = value;
                this.NotifyPropertyChanged("QUOTATION_NO");
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

        [DBColumn(Name = "CONTENT_TYPE", Storage = "m_CONTENT_TYPE", DbType = "126")]
        public string CONTENT_TYPE
        {
            get { return this.m_CONTENT_TYPE; }
            set
            {
                this.m_CONTENT_TYPE = value;
                this.NotifyPropertyChanged("CONTENT_TYPE");
            }
        }

        [DBColumn(Name = "FILE_DATA1", Storage = "m_FILE_DATA1", DbType = "102")]
        public string FILE_DATA1
        {
            get { return this.m_FILE_DATA1; }
            set
            {
                this.m_FILE_DATA1 = value;
                this.NotifyPropertyChanged("FILE_DATA1");
            }
        }

        #endregion //properties
    }
}
